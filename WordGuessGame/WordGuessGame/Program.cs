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
            // Will continue to display the menu options until Exit is selected
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
                // 2: To print all words in word bank to console
                case "2":
                    PrintWordBank(path);
                    break;
                // 3: To add a new word to the word bank
                case "3":
                    Console.Write("Enter word to add to word bank: ");
                    userInput = Console.ReadLine();
                    AppendToFile(path, userInput);
                    break;
                // 4: to delete a word from the word bank
                case "4":
                    PrintWordBank(path);
                    Console.Write("Type the word to delete from the word bank: ");
                    userInput = Console.ReadLine();
                    UpdateFile(path, userInput);
                    break;
                // 5: to exit from the program
                case "5":
                    break;
                default:
                    isValidInput = false;
                    Console.WriteLine("Input not valid! Try again.");
                    break;
            }
            // Will display main menu again if user did not select a proper menu item.
            return (isValidInput) ? userInput : MainMenu();
        }

        /// <summary>
        /// When Start Game is selected from the menu, this will initialize the game by getting a random word from the word bank, and create an array of underscores that will fill in during the round. Calls PlayRound() with these two pieces of data.
        /// </summary>
        /// <param name="path">Full File Path</param>
        public static void StartGame(string path)
        {
            // Will get the number of words in the word bank in order to generate a random word. Will exit game if word bank is empty.
            int wordBankLength = GetWordBankLength(path);
            if (wordBankLength == 0) return;
            // Get a random word from the word bank.
            Random r = new Random();
            int rIndex = r.Next(0, wordBankLength);
            string wordToGuess= GetWord(path, rIndex);
            // Create an array of underscores to keep track of character guessing progress.
            char[] wordProgress = new char[wordToGuess.Length];
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                wordProgress[i] = '_';
            }

            PlayRound(wordProgress, wordToGuess);
        }

        /// <summary>
        /// Controls the main loop of the current game in play. Won't exit until word has been guessed. Keeps track of total guesses.
        /// </summary>
        /// <param name="wordProgress">array of underscores, eventually fills in with chars with correct guesses</param>
        /// <param name="wordToGuess">the random word grabbed from word bank</param>
        public static void PlayRound(char[] wordProgress, string wordToGuess)
        {
            int guessesLeft = wordProgress.Length;
            int totalGuesses = 0;
            char keyPress = ' ';
            string history = "";

            // main loop will continue until every character in the word has been correctly guessed.
            while (guessesLeft != 0)
            {
                // Prints progress of the word to guess, total guess count, and history of characters tried to the screen.
                Console.WriteLine("");
                foreach (char c in wordProgress) Console.Write(c + " ");
                Console.WriteLine(" Total Guesses: " + totalGuesses);
                Console.Write("History: ");
                foreach (char c in history) Console.Write(c + " ");
                // Takes a single key press, adds to the history.
                Console.Write("\nEnter a letter to guess: ");
                keyPress = char.ToUpper(Console.ReadKey().KeyChar);
                history += keyPress;
                Console.WriteLine("");
                // Will check every character in the word, if the guessed char exists in the word and hasn't already been guessed, will fill in the empty spot with the char and reduce chars left to guess by 1.
                for (int i=0; i < wordProgress.Length; i++)
                {
                    if(char.ToUpper(wordToGuess[i]) == keyPress && wordProgress[i] != keyPress)
                    {
                        wordProgress[i] = wordToGuess[i];
                        guessesLeft--;
                    }
                }
                totalGuesses++;
            }
            Console.WriteLine("\nYou Win! Total Guesses: " + totalGuesses);
        }

        /// <summary>
        /// Will read every line in the local file to determine how many words are in it. If local File doesn't exist, will create file and return size of zero.
        /// </summary>
        /// <param name="path">Full file path</param>
        /// <returns>Number of words in the file</returns>
        public static int GetWordBankLength(string path)
        {
            try
            {
                // Will create file if it doesn't exist, but tell you its empty and return size of zero.
                if (!File.Exists(path))
                {
                    CreateFile(path);
                    Console.WriteLine("Word Bank is empty.");
                    return 0;
                }
                // Read file line by line to find number of words.
                else
                {
                    int wordBankLength = 0;
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            wordBankLength++;
                        }
                    }
                    return wordBankLength;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed reading file... " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Will get return a string from a particular line from the word bank to be used in the guessing game.
        /// </summary>
        /// <param name="path">Full file path</param>
        /// <param name="index">Random number used as a line number to grab word</param>
        /// <returns>The word to be used in the game</returns>
        public static string GetWord(string path, int index)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string s = "";
                    int i = 0;
                    // Will check every line in the file until it reaches the line number passed into method and return that string.
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (index == i) return s;
                        else i++;
                    }
                    return "logic error";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed reading file..." + e.Message);
                throw;
            }
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
        public static void PrintWordBank(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    CreateFile(path);
                    Console.WriteLine("Word Bank is empty.");
                }
                else
                {
                    // Prints every line in the word bank to screen.
                    Console.WriteLine("Current Word Bank:");
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    Console.WriteLine("");
                }
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
                // Replaces original file with new file without the word wanting to be removed.
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
