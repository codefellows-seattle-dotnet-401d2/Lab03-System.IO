using System;
using System.Collections.Generic;
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

            int wordcount = words.Length;

            Random generator = new Random();

            bool playing = true;

            int index = generator.Next(0, wordcount);

            string word = words[index];

            char firstchar = word.ToCharArray()[0];

            List<string> guesses = new List<string>();

            int guesscount = 0;

            while (playing)
            {

                Console.WriteLine($"Guess a word starting with {firstchar}!");

                string guess = Console.ReadLine();

                if(word.ToLower() == guess.ToLower())
                {
                    Console.WriteLine("You got it!");

                    playing = false;

                    Console.ReadLine();

                }
                else
                {
                    guesses.Add(guess);
                    guesscount++;

                    if (guesscount == 5)
                    {
                        Console.WriteLine("Out of guesses :(");

                        Console.WriteLine("You have guessed:");
                        
                        foreach (string guess_ in guesses)
                        {
                            Console.WriteLine(guess_);
                        }

                        Console.WriteLine($"The word was {word}.");

                        playing = false;

                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Nice try, but nope!");

                        Console.WriteLine("You have guessed:");

                        foreach(string guess_ in guesses)
                        {
                            Console.WriteLine(guess_);
                        }

                        Console.WriteLine();
                    }

                }

            }


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
                Queue<string> lines = new Queue<string>();
                string line = "";
                bool blank = true;
                while(line != null)
                {
                    if (!blank)
                    {
                        lines.Enqueue(line);
                        lineCount++;
                    }
                    else
                    {
                        blank = false;
                    }
                    line = sr.ReadLine();
                }
                string[] lineArray = new string[lineCount];

                for (int i = 0; i < lineArray.Length; i++)
                {
                    lineArray[i] = lines.Dequeue();
                }

                return (lineArray);
            }
        }
    }
}
