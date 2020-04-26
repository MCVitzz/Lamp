using System.Collections.Generic;

namespace Lamp.GUI.Texting.Loading
{
	public class Word
	{

		public readonly double FontSize;

		public List<Character> Characters = new List<Character>();
		public double PixelWidth = 0;

		public Word(double fontSize)
		{
			FontSize = fontSize;
		}

		public void AddCharacter(Character character)
		{
			Characters.Add(character);
			PixelWidth += character.XAdvance * FontSize;
		}

		public override string ToString()
		{
			string s = "";
			foreach (Character c in Characters)
			{
				s += (char)c.Id;
			}
			return s;
		}
	}
}
