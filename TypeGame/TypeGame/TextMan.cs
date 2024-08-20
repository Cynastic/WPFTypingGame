using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TypeGame
{
	static class TextManager 
	{
		static Random random = new();
		public static string currentText = "";

		static List<string> textList = new();

		public static void Initialize()
		{
			textList = File.ReadAllLines("quotes.txt").ToList();
			currentText = GetRandomText();
		}

		public static void TextWritten()
		{
			currentText = GetRandomText();
		}
		
		private static string GetRandomText()
		{
			int randIndex = random.Next(0, textList.Count);
			while (textList[randIndex] == currentText)
			{
				randIndex = random.Next(0, textList.Count);
			}
			return textList[randIndex];
		}
	}
}
