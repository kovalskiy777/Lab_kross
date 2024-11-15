using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lab1
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = Path.Combine("Lab1", "INPUT.TXT");
                string outputFilePath = Path.Combine("Lab1", "OUTPUT.TXT");

                string[] lines = File.ReadAllLines(inputFilePath);

                Validate(lines);

                string result = ProcessLab1(lines);
                File.WriteAllText(outputFilePath, result.Trim());

                Console.WriteLine("Lab1");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(result.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine('\n');

        }

        public static void Validate(string[] lines)
        {

            foreach (string line in lines)
            {
        
                if (string.IsNullOrWhiteSpace(line) || line.Contains(" "))
                {
                    throw new InvalidOperationException("There can be only one number in one line.");
                }

                if (!long.TryParse(line.Trim(), out long N))
                {
                    throw new InvalidOperationException($"'{line}' is not a real number.");
                }

                if (N < 1 || N > 2147483647)
                {
                    throw new InvalidOperationException("The number must be greater than 1 and less than 2147483647");
                }
            }
        }

        public static string ProcessLab1(string[] lines)
        {
            StringBuilder result = new StringBuilder();

            foreach (string line in lines)
            {
                if (ulong.TryParse(line.Trim(), out ulong n) && n > 0)
                {
                    result.AppendLine(FindNthSequence(n));
                }
            }

            return result.ToString().Replace("\r\n", "\n");
        }

        private static string FindNthSequence(ulong n)
        {
            var f = new List<List<ulong>> { new List<ulong>() }; 
            f.Add(new List<ulong>(new ulong[10]));       
            for (int i = 0; i < 10; i++) f[1][i] = 1;

            int m = 1;

            while (true)
            {
                f.Add(new List<ulong>(new ulong[10]));
                ulong sum = 0;

                for (int digit = 9; digit >= 0; --digit)
                {
                    sum += f[m][digit];
                    f[m + 1][digit] = sum;
                }

                if (f[m + 1][0] > n)
                    break;

                ++m;
            }

            StringBuilder sequence = new StringBuilder();
            int currentDigit = 0;

            for (int k = m; k > 0; --k)
            {
                for (int nextDigit = currentDigit; nextDigit < 10; ++nextDigit)
                {
                    if (f[k][nextDigit] > n)
                    {
                        currentDigit = nextDigit;
                        sequence.Append(currentDigit);
                        break;
                    }
                    n -= f[k][nextDigit];
                }
            }

            return sequence.ToString();
        }
    }

}
