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
		public static string currentAuthor = "";

		static List<string> textList = new();
		static List<string> authorList = new();

		public static void Initialize()
		{
			textList = File.ReadAllLines("quotes.txt").ToList();
			authorList = File.ReadAllLines("authors.txt").ToList();
			GetNewText();
		}

		public static void TextWritten()
		{
			GetNewText();
		}

		private static void GetNewText()
		{
			int newIndex = GetRandomIndex();
			currentText = textList[newIndex];
			currentAuthor = authorList[newIndex];
		}
		
		private static int GetRandomIndex()
		{
			int randIndex = random.Next(0, textList.Count);
			while (textList[randIndex] == currentText)
			{
				randIndex = random.Next(0, textList.Count);
			}
			return randIndex;
		}
	}
}
