using System;
using System.IO;
using System.Text;

// TODO: Make guess comparison work
// TODO: Get Delete Word Working
// TODO: 3 tests
// NOTE: Use .Contains (does word contain users guess. return bool)

namespace WordGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "WordFile.txt";

            Menu(path);
            DisplayMenu();
            CreateFile(path);
            ReadWord(path);
            string input = null;
            UpdateWord(path, input);
            //DeleteWord(path);
        }

        public static void Menu(string path)
        {
            bool loop = true;
            
            while(loop)
            {
                int choice = DisplayMenu();
                
                switch(choice)
                {
                    case 1:
                       // StartGame();
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
                        Console.WriteLine("Enter word you wish to delete:");
                        string deleteInput = Console.ReadLine();
                        DeleteWord(path, deleteInput);
                        break;
                    case 5:
                       loop = false;
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
        /// Creates file with list of strings
        /// </summary>
        static void CreateFile(string path)
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

        // outputs list of file contents
        static void ReadWord(string path)
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

        // updates word through user input
        static void UpdateWord(string path, string input)
        {

             using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);
            }
        }

        // read from file
        // create new file without deleted word
        // delete old file

        // Deletes the word from input
        static void DeleteWord(string path, string deleteInput)
        {
            File.Delete(deleteInput);
        }
    }
}
