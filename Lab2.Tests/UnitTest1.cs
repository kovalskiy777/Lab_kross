using System;
using System.IO;
using Xunit;

namespace Lab2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ValidInput()
        {
            string[] lines = { "1 6" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.Null(ex); 
        }

        [Fact]
        public void Test_InputExceedsLimit_N()
        {
            string[] lines = { "0 5" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("N must be between 1 and 500, and Q must be between N and 6*N.", ex.Message);
        }

        [Fact]
        public void Test_InputExceedsLimit_Q()
        {
            string[] lines = { "2 3001" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("N must be between 1 and 500, and Q must be between N and 6*N.", ex.Message);
        }

        [Fact]
        public void Test_InputExceedsLimit_Q2()
        {
            string[] lines = { "5 3" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("N must be between 1 and 500, and Q must be between N and 6*N.", ex.Message);
        }

        [Fact]
        public void Test_InvalidNumberFormat()
        {
            string[] lines = { "abc def" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("N and Q must be valid integers.", ex.Message); 
        }

        [Fact]
        public void Test_ProcessingWithEdgeCase()
        {
            string lines = "500 500";
            string expected = "0";

            string result = Program.ProcessLab1(lines);

            Assert.Equal(expected, result);
        }
    }
}
