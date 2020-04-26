using System.Collections.Generic;

namespace Lamp.GUI.Texting.Loading
{
	public class Line
	{

		public readonly double MaxPixelLength;
		public readonly double SpacePixelWidth;

		public List<Word> Words = new List<Word>();
		public double LineLengthPixels = 0;

		public int CharacterCount = 0;

		public Line(double spaceWidth, double fontSize, double maxLength)
		{
			SpacePixelWidth = spaceWidth * fontSize;
			MaxPixelLength = maxLength;
		}

		public bool AttemptToAddWord(Word word)
		{
			double additionalLength = word.PixelWidth;
			additionalLength += Words.Count != 0 ? SpacePixelWidth : 0;
			if (LineLengthPixels + additionalLength <= MaxPixelLength)
			{
				Words.Add(word);
				CharacterCount += word.Characters.Count;
				LineLengthPixels += additionalLength;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}