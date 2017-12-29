using System;
using Xunit;
using WordGuessGame;

namespace XUnitTest_WordGuessGame
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("wordbanktest.txt", "WordOne", 0)]
        [InlineData("wordbanktest.txt", "WordTwo", 1)]
        [InlineData("wordbanktest.txt", "WordThree", 2)]
        // Will have to delete file from path after each test. Needs to run in order too.
        public void TestGetWord(string path, string word, int lineNum)
        {
            if (lineNum == 0) Program.CreateFile(path);
            Program.AppendToFile(path, word);
            Assert.Equal(word, Program.GetWord(path, lineNum));
        }
    }
}
