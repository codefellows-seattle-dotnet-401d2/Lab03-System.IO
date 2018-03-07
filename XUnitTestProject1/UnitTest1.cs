using System;
using Xunit;
using WordGuesser;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string path = "words.txt";

            Program.CreateFile(path, "Cat\nFish\nCow\nDog\nCheese\nOrange\nApple\nPear\nTree\nFlower");

            string[] words = Program.ReadFileToLineArray(path);

            Assert.Matches("Flower", words[words.Length - 1]);
        }
    }
}
