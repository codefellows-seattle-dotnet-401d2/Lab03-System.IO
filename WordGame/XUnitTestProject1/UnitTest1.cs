using WordGame;
using System;
using Xunit;


namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void CanReturnNumber()
        {
            Assert.InRange(GuessGame.DisplayMenu(), 1, 5);
        }

    }
}
