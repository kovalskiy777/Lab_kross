using System;
using System.IO;
using Xunit;
using TaskProcessorLib;

namespace Lab3.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ValidInput()
        {
            string[] lines = { "3 4", "1 2", "2 3", "3 1", "3 2" };

            var ex = Record.Exception(() => Program.Validate(lines));

            Assert.Null(ex); 
        }

        [Fact]
        public void Test_InputExceedsLimit_N()
        {
            string[] lines = { "104 5", "1 2", "2 3", "3 4", "4 1", "2 4" };

            var ex = Record.Exception(() => Program.Validate(lines));

            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("The value of n must be between 1 and 103.", ex.Message);
        }

        [Fact]
        public void Test_InputExceedsLimit_M()
        {
            string[] lines = { "3 105001", "1 2", "2 3", "3 1" };

            var ex = Record.Exception(() => Program.Validate(lines));

            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("The value of m must be between 0 and 105.", ex.Message);
        }

        [Fact]
        public void Test_InvalidRoadEntry_SelfLoop()
        {
            string[] lines = { "3 3", "1 1", "2 3", "3 2" };

            var ex = Record.Exception(() => Program.Validate(lines));

            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("Invalid road entry on line 2: u and v must be in range [1, n] and not equal.", ex.Message);
        }

        [Fact]
        public void Test_InvalidNumberFormat()
        {
            string[] lines = { "abc def", "1 2", "2 3" };

            var ex = Record.Exception(() => Program.Validate(lines));

            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("The values of n and m must be integers.", ex.Message);
        }

        [Fact]
        public void Test_Processing_ValidCycle()
        {
            string[] lines = { "3 4", "1 2", "2 3", "3 1", "3 2" };
            string expected = "YES";

            string result = TaskProcessor.ProcessTask(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Processing_NoCycle()
        {
            string[] lines = { "2 3", "1 2", "2 1", "2 1" };
            string expected = "NO";

            string result = TaskProcessor.ProcessTask(lines);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Processing_EdgeCase_SingleIntersection()
        {
            string[] lines = { "1 0" };
            string expected = "NO";

            string result = TaskProcessor.ProcessTask(lines);

            Assert.Equal(expected, result);
        }
    }
}
