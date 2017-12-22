/* The program (should) contain the following:
 * Methods for each action (Home navigation, View words in the text file, add a word to the text file, remove words from a text file, exit the game, start a new game)
 * When playing a game, you should bring in all the words that exist in the text file, and randomly select one of the words to output to the console for the user to guess
 * You should have a record the letters they have attempted so far. If they guess a correct letter, display that letter in the console for them to refer back to when making guesses (i.e. C _ T S )
 * Errors should be handled through Exception handling
 * You may use any shortcuts or 'helper' methods in this project. Do not create external classes to accomplish this task.
*/

using System;
using System.IO;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string option = "";
            Console.WriteLine("Program starting...");
            do
            {
                option = MainMenu();
                if (option == "5") continue;
            } while (option != "5");
            Console.Write("Program terminating... press Enter to exit...");
            Console.ReadLine();
        }

        public static string MainMenu()
        {
            Console.Write(
                "\n1. Start Game " +
                "\n2. View Word Bank " +
                "\n3. Add to Word Bank " +
                "\n4. Remove from Word Bank " +
                "\n5. Exit Game" +
                "\nEnter selection: "
                );
            string userInput = Console.ReadLine();
            bool isValidInput = true;
            string path = "wordbank.txt";
            switch (userInput)
            {
                case "1":
                    StartGame(path);
                    break;
                case "2":
                    ReadFile(path);
                    break;
                case "3":
                    Console.WriteLine("Enter word to add to dictionary");
                    userInput = Console.ReadLine();
                    WriteToFile(path, userInput);
                    break;
                case "4":
                    ReadFile(path);
                    Console.WriteLine("Type the word to delete from the dictionary");
                    userInput = Console.ReadLine();
                    UpdateFile(path, userInput);
                    break;
                case "5":
                    break;
                default:
                    isValidInput = false;
                    Console.WriteLine("Input not valid! Try again.");
                    break;
            }
            return (isValidInput) ? userInput : MainMenu();
        }

        public static void StartGame(string path)
        {

        }

        private static void CreateFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.Write("--- Start of File ---");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed creating file...");
                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        public static void ReadFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("Can't read file because it doesn't exist. Creating file now...");
                    CreateFile(path);
                }
                using(StreamReader sr = new StreamReader(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed reading file...");
                throw;
            }
            finally
            {
                Console.WriteLine("Read File Operation Complete.");
            }
        }

        public static void WriteToFile(string path, string userInput)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(userInput);
            }
            Console.WriteLine("Write File Operation Complete.");
        }

        public static void UpdateFile(string path, string userInput)
        {
            string tempPath = "temp.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if (userInput.ToLower() == s.ToLower()) continue;
                    else
                    {
                        WriteToFile(tempPath, s);
                    }
                }
            }
            try
            {
                File.Copy(tempPath, path, true);
                File.Delete(tempPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to overwrite word bank or delete temp file.");
            }
        }
    }
}
