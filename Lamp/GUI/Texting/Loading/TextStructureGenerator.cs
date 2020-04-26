using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Lamp.GUI.Texting.Loading
{
    public class TextStructureGenerator
    {

        protected static readonly int SpaceAscii = 32;

        private readonly Dictionary<int, Character> MetaData;
        private readonly float SpaceWidth;

        private List<Line> Lines;
        private Line CurrentLine;
        private Word CurrentWord;

        public TextStructureGenerator(Dictionary<int, Character> metaData, float spaceWidth)
        {
            MetaData = metaData;
            SpaceWidth = spaceWidth;
        }

        public List<Line> CreateStructure(string text, float fontSize, float maxLineLength)
        {
            char[] chars = text.ToArray();
            InitStructure(fontSize, maxLineLength);
            foreach (char c in chars)
            {
                ProcessChar(c, fontSize, maxLineLength);
            }
            CompleteStructure(fontSize, maxLineLength);
            return Lines;
        }

        private void InitStructure(float fontSize, float maxLineLength)
        {
            Lines = new List<Line>();
            CurrentLine = new Line(SpaceWidth, fontSize, maxLineLength);
            CurrentWord = new Word(fontSize);
        }

        private void ProcessChar(char c, float fontSize, float maxLineLength)
        {
            int ascii = c;
            if (ascii == SpaceAscii)
            {
                MoveToNextWord(fontSize, maxLineLength);
            }
            else
            {
                AddCharToWord(ascii);
            }
        }

        private void MoveToNextWord(float fontSize, float maxLineLength)
        {
            bool added = CurrentLine.AttemptToAddWord(CurrentWord);
            if (!added)
            {
                Lines.Add(CurrentLine);
                CurrentLine = new Line(SpaceWidth, fontSize, maxLineLength);
                CurrentLine.AttemptToAddWord(CurrentWord);
            }
            CurrentWord = new Word(fontSize);
        }

        private void AddCharToWord(int ascii)
        {
            Character character = MetaData[ascii];
            if (character != null)
            {
                CurrentWord.AddCharacter(character);
            }
            else
            {
                Log.Error("ERROR CHAR: " + ascii);
            }
        }

        private void CompleteStructure(float fontSize, float maxLineLength)
        {
            MoveToNextWord(fontSize, maxLineLength);
            Lines.Add(CurrentLine);
        }


    }

}
