using System;
using System.IO;
using Xunit;

namespace Lab1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ValidInput()
        {
            string[] lines = { "1", "2", "3" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.Null(ex);
        }

        [Fact]
        public void Test_InputExceedsLimit()
        {
            string[] lines = { "0" }; ; 

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("The number must be greater than 1 and less than 2147483647", ex.Message);
        }

        [Fact]
        public void Test_InputExceedsLimit2()
        {
            string[] lines = { "21474836471" }; ;

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("The number must be greater than 1 and less than 2147483647", ex.Message);
        }

        [Fact]
        public void Test_InvalidNumberFormat()
        {
            string[] lines = { "sometext" };

            var ex = Record.Exception(() => Program.Validate(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("'sometext' is not a real number.", ex.Message);
        }

        [Fact]
        public void Test_ValidProcessing()
        {
            string[] lines = { "1", "11", "239" };
            string expected = "1\n12\n1135\n";

            string result = Program.ProcessLab1(lines);

            Assert.Equal(expected, result);
        }
    }
}
