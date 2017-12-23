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
        /// <summary>
        /// Handles main loop of program, only selecting "5" will exit the program.
        /// </summary>
        /// <param name="args"></param>
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

        /// <summary>
        /// Displays a list of options, then waits for user input, checks that its valid, then calls appropriate method.
        /// </summary>
        /// <returns>Returns the menu option the user selected</returns>
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
                    Console.Write("Enter word to add to word bank: ");
                    userInput = Console.ReadLine();
                    AppendToFile(path, userInput);
                    break;
                case "4":
                    ReadFile(path);
                    Console.Write("Type the word to delete from the word bank: ");
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

        /// <summary>
        /// Creates an empty file at path.
        /// </summary>
        /// <param name="path">full file path</param>
        private static void CreateFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                try
                {
                    sw.Write("");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed creating file... " + e.Message);
                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Reads from a file and prints to the screen. Will check if file exists first, and if not, creates an empty file.
        /// </summary>
        /// <param name="path">Full file path</param>
        public static void ReadFile(string path)
        {
            try
            {
                if (!File.Exists(path)) CreateFile(path);
                Console.WriteLine("Current Word Bank:");
                using(StreamReader sr = new StreamReader(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
                Console.WriteLine("");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed reading file... " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Appends passed in userInput to file at path.
        /// </summary>
        /// <param name="path">Full file path</param>
        /// <param name="userInput">user input passed from main menu to add to word bank</param>
        public static void AppendToFile(string path, string userInput)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(userInput);
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Removes a word from the word bank by creating a temp file with the missing word, then overwrites the original file.
        /// </summary>
        /// <param name="path">Full file path</param>
        /// <param name="userInput">user input passed from main menu to remove from word bank</param>
        public static void UpdateFile(string path, string userInput)
        {
            string tempPath = "temp.txt";
            CreateFile(tempPath);
            using (StreamReader sr = new StreamReader(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    // Appends list of words to temp file unless its the word being deleted.
                    if (userInput.ToLower() == s.ToLower()) continue;
                    else
                    {
                        AppendToFile(tempPath, s);
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
                Console.WriteLine("Failed to overwrite word bank or delete temp file. " + e.Message);
            }
        }
    }
}
