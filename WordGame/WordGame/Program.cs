using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

// TODO: 3 TESTS

namespace WordGame
{
    public class GuessGame
    {

        public static void Main(string[] args)
        {
            string path = "WordFile.txt";

            Menu(path);
            DisplayMenu();
            CreateFile(path);
            ReadWord(path);
            string input = null;
            UpdateWord(path, input);
            DeleteWord(path);
            
        }

        public static string Testing()
        {
             Console.Write("Hey");
            string x = Console.ReadLine();
            return x;
        }

        /// <summary>
        /// Allows the menu displayed to call relative individual functions
        /// </summary>
        public static void Menu(string path)
        {
            bool loop = true;

            while (loop)
            {
                int choice = DisplayMenu();

                switch (choice)
                {
                    case 1:
                        StartGame(path);
                        Console.ReadLine();
                        break;
                    case 2:
                        ReadWord(path);
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Please input new word:");
                        string input = Console.ReadLine();
                        UpdateWord(path, input);
                        break;
                    case 4:
                        DeleteWord(path);
                        Console.ReadLine();
                        break;
                    case 5:
                        loop = false;
                        Console.ReadLine();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays menu that takes user input
        /// </summary>
        /// <returns>List of Strings</returns>
        public static int DisplayMenu()
        {
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. View Word List");
            Console.WriteLine("3. Add A New Word");
            Console.WriteLine("4. Delete A Word");
            Console.WriteLine("5. Exit Game");
            string result = Console.ReadLine();
            int numChoice = Convert.ToInt32(result);
            return numChoice;
        }

        /// <summary>
        /// Randomizes a word to be chosen for the game
        /// </summary>
        /// <returns>A string of a word</returns>
        public static string RandomWord(string path)
        {

            string[] lines = File.ReadAllLines(path);
            Random rand = new Random();
            string word = lines[rand.Next(lines.Length)];
            Console.WriteLine(word);

            return word;
        }


        /// <summary>
        /// Starts gameplay 
        /// </summary>
        public static void StartGame(string path)
        {

            Console.WriteLine("Let's Play!");
            Console.Write("Here is your word: ");
            string word = RandomWord(path);
            Console.WriteLine("Guess a letter to complete the word: ");

            // a collection of wrong guesses
            char[] history = new char[26];
            int counter = 0;

            // a collection of correct guesses
            char[] progress = new char[word.Length];
            int progCounter = 0;

            // loop to continue game until all words are correct
            for (int i = 0; i < word.Length; i++)
            {
                char userGuess = Console.ReadKey().KeyChar;
                bool correctGuess = word.Contains(userGuess);


                // condition statements that add chars to their collective arrays
                if (correctGuess)
                {
                    Console.WriteLine("Correct!");
                    progress[progCounter++] = userGuess;
                }
                else
                {
                    Console.WriteLine("Guess Again!");
                    history[counter++] = userGuess;
                }

                Console.Write("Word: ");
                Console.WriteLine(progress);
                Console.Write("History: ");
                Console.WriteLine(history);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Creates file with list of strings
        /// </summary>
        public static void CreateFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.WriteLine("EXCEPTION");
                    sw.WriteLine("STREAM");
                    sw.WriteLine("CATCH");
                    sw.WriteLine("TRY");
                    sw.WriteLine("THROW");
                }
                catch (Exception e)
                {
                    string message = (e.Message);
                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Outputs list of file contents
        /// </summary>
        public static void ReadWord(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Updates word through user input
        /// </summary>
        /// <param name="path">stream of txt file</param>
        /// <param name="input">input from user to file</param>
        public static void UpdateWord(string path, string input)
        {

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);
            }
        }

        /// <summary>
        /// Deletes a word from the existing list through user input
        /// </summary>
        public static void DeleteWord(string path)
        {
            Console.WriteLine("Enter word you wish to remove: ");

            string word = Console.ReadLine();

            // Creates temporary file
            string tempFile = Path.GetTempFileName();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    string line;

                    // Matches line with user input
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != word)
                            sw.WriteLine(line);
                    }
                }

                File.Delete(path);

                //Replace file
                File.Move(tempFile, path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        
    }
}
