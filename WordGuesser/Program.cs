using System;
using System.IO;
using System.Text;

namespace WordGuesser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "words.txt";

            CreateFile(path, "Cat\nFish\nCow\nDog\nCheese\nOrange\nApple\nPear\nTree\nFlower");

            string[] words = ReadFileToLineArray(path);

            Console.Read();

        }

        public static bool CreateFile(string path, string contents)
        {
            if (!File.Exists(path)) File.Delete(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.Write(contents);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        public static string[] ReadFileToLineArray(string path)
        {
            using (StreamReader sr = File.OpenText(path)) {
                int lineCount = 0;
                while(sr.ReadLine() != null)
                {
                    lineCount++
                }
                string[] lineArray = new string[lineCount];

                short index = 0;
                string lineText = "";
                while((lineText = sr.ReadLine()) != null)
                {
                    lineArray[index] = lineText;
                }

                return (lineArray);
            }
        }
    }
}
