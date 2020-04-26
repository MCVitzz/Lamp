using Lamp.GUI.Texting.Basics;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lamp.GUI.Texting.Loading
{
    public class MetaDataLoader
    {

        private static readonly char QuoteChar = '^';
        private static readonly int QuoteReplace = QuoteChar;
        private static readonly int QuoteAscii = 34;
        private static readonly int SpaceAscii = 32;

        private static readonly int PadTop = 0;
        private static readonly int PadLeft = 1;
        private static readonly int PadBottom = 2;
        private static readonly int PadRight = 3;

        private static readonly int DesiredPadding = 1;

        private static readonly char Splitter = ' ';
        private static readonly char NumberSeparator = ',';

        private float ScaleConvertion;
        private int[] Padding;
        private int PaddingWidth;
        private int PaddingHeight;

        private float SpaceWidth;

        private StreamReader reader;
        private readonly Dictionary<string, string> Values = new Dictionary<string, string>();

        private readonly FileInfo MetaFile;

        public MetaDataLoader(FileInfo metaDataFile)
        {
            MetaFile = metaDataFile;
        }

        public TextGenerator LoadMetaData()
        {
            OpenFile();
            LoadPaddingData();
            LoadLineSize();
            int imageWidth = GetValueOfVariable("scaleW");
            Dictionary<int, Character> charData = LoadCharacterData(imageWidth);
            Close();
            return new TextGenerator(new TextStructureGenerator(charData, SpaceWidth), new TextMeshGenerator(SpaceWidth));
        }

        /**
		 * Read in the next line and store the variable values.
		 * 
		 * @return {@code true} if the end of the file hasn't been reached.
		 */
        private bool ProcessNextLine()
        {
            Values.Clear();
            string line = null;
            try
            {
                line = reader.ReadLine();
            }
            catch (IOException)
            {
            }
            if (line == null)
            {
                return false;
            }
            foreach (string part in line.Split(Splitter))
            {
                string[] valuePairs = part.Split('=');
                if (valuePairs.Length == 2)
                {
                    Values.Add(valuePairs[0], valuePairs[1]);
                }
            }
            return true;
        }

        private int GetValueOfVariable(string variable)
        {
            return int.Parse(Values[variable]);
        }

        private int[] GetValuesOfVariable(String variable)
        {
            string[] numbers = Values[variable].Split(NumberSeparator);
            int[] actualValues = new int[numbers.Length];
            for (int i = 0; i < actualValues.Length; i++)
            {
                actualValues[i] = int.Parse(numbers[i]);
            }
            return actualValues;
        }

        /**
		 * Closes the font file after finishing reading.
		 */
        private void Close()
        {
            try
            {
                reader.Close();
            }
            catch (IOException e)
            {
                Log.Error(e.StackTrace);
            }
        }

        private void OpenFile()
        {
            try
            {
                reader = MetaFile.OpenText();
            }
            catch (Exception e)
            {
                Log.Error("Font Loading Error", "Couldn't load meta data for font.", e);
            }
        }

        private void LoadPaddingData()
        {
            ProcessNextLine();
            Padding = GetValuesOfVariable("padding");
            PaddingWidth = Padding[PadLeft] + Padding[PadRight];
            PaddingHeight = Padding[PadTop] + Padding[PadBottom];
        }

        private void LoadLineSize()
        {
            ProcessNextLine();
            int lineHeight = GetValueOfVariable("lineHeight") - PaddingHeight;
            ScaleConvertion = Text.LineHeightPixels / (float)lineHeight;
        }

        private Dictionary<int, Character> LoadCharacterData(int imageWidth)
        {
            ProcessNextLine();
            ProcessNextLine();
            int count = GetValueOfVariable("count");
            Dictionary<int, Character> charData = new Dictionary<int, Character>();
            for (int i = 0; i < count; i++)
            {
                ProcessNextLine();
                ProcessNextCharacter(imageWidth, charData);
            }
            return charData;
        }

        private void ProcessNextCharacter(int imageSize, Dictionary<int, Character> charData)
        {
            Character c = LoadCharacter(imageSize);
            if (c == null || c.Id == QuoteReplace)
            {
                return;
            }
            if (c.Id == QuoteAscii)
            {
                charData.Add(QuoteReplace, c);
            }
            charData.Add(c.Id, c);
        }

        private Character LoadCharacter(int imageSize)
        {
            int id = GetValueOfVariable("id");
            if (id == SpaceAscii)
            {
                SpaceWidth = (GetValueOfVariable("xadvance") - PaddingWidth) * ScaleConvertion;
                return null;
            }
            double xTex = ((double)GetValueOfVariable("x") + (Padding[PadLeft] - DesiredPadding)) / imageSize;
            double yTex = ((double)GetValueOfVariable("y") + (Padding[PadTop] - DesiredPadding)) / imageSize;
            int width = GetValueOfVariable("width") - (PaddingWidth - (2 * DesiredPadding));
            int height = GetValueOfVariable("height") - ((PaddingHeight) - (2 * DesiredPadding));
            double xTexSize = (double)width / imageSize;
            double yTexSize = (double)height / imageSize;

            double xOff = (GetValueOfVariable("xoffset") + Padding[PadLeft] - DesiredPadding) * ScaleConvertion;
            double yOff = (GetValueOfVariable("yoffset") + Padding[PadTop] - DesiredPadding) * ScaleConvertion;
            double quadWidth = width * ScaleConvertion;
            double quadHeight = height * ScaleConvertion;
            double xAdvance = (GetValueOfVariable("xadvance") - PaddingWidth) * ScaleConvertion;
            return new Character(id, xTex, yTex, xTexSize, yTexSize, xOff, yOff, quadWidth, quadHeight, xAdvance);
        }

    }
}