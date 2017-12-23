using System;
using System.IO;

namespace WordGuessGame
{
    class Game
    {
        public static void Main(string[] args)
        {
            string path = "WordFile.txt";

            Console.WriteLine("What is your name stranger?");
            string userName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"Welcome to my word guessing game {userName}");
            Console.ReadLine();
            CreateFile(path);
            ReadFile(path);
            //UpdateFile();
            //DeleteFile();
        }

        public static void WordSetUp()
        {
            int Guess = 0;
            int Target = 5;
            Random ran = new Random();
        }

        public static void CreateFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.Write("Word Bank");
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message, "Where do you think you are going? Get your ass back here.");

                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
            Console.ReadLine();
        }


        public static void ReadFile(string path)
        {

            using (StreamReader sr = File.OpenText(path))
            {
                string[] myWords = File.ReadAllLines("WordFile.txt");

                string s = " ";
                Console.WriteLine(s);

            }
            Console.ReadLine();
        }
    }
}