using System;
using System.IO;
using System.Text;

namespace Lab2
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = Path.Combine("Lab2", "INPUT.TXT");
                string outputFilePath = Path.Combine("Lab2", "OUTPUT.TXT");

                string[] lines = File.ReadAllLines(inputFilePath);
                Validate(lines);

                StringBuilder result = new StringBuilder();
                foreach (var line in lines)
                {
                    result.AppendLine(ProcessLab1(line));
                }

                File.WriteAllText(outputFilePath, result.ToString().Trim());

                Console.WriteLine("Lab2");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine('\n');
        }

        public static void Validate(string[] lines)
        {
            foreach (var line in lines)
            {
                string[] parts = line.Split();
                if (parts.Length != 2)
                    throw new InvalidOperationException("Each line must contain exactly two numbers N and Q.");

                if (!int.TryParse(parts[0], out int N) || !int.TryParse(parts[1], out int Q))
                    throw new InvalidOperationException("N and Q must be valid integers.");

                if (N < 1 || N > 500 || Q < N || Q > 3000)
                    throw new InvalidOperationException("N must be between 1 and 500, and Q must be between N and 6*N.");
            }
        }

        public static string ProcessLab1(string line)
        {
            string[] parts = line.Split();
            int N = int.Parse(parts[0]);
            int Q = int.Parse(parts[1]);

            return CalculateProbability(N, Q).ToString("G15");
        }

        public static double CalculateProbability(int N, int Q)
        {
            double[,] dp = new double[N + 1, Q + 1];
            dp[0, 0] = 1.0; 
            for (int i = 1; i <= N; i++)
            {
                for (int j = i; j <= Math.Min(Q, 6 * i); j++)
                {
                    dp[i, j] = 0.0;
                    for (int k = 1; k <= 6; k++)
                    {
                        if (j >= k)
                        {
                            dp[i, j] += dp[i - 1, j - k];
                        }
                    }
                }
            }

            double totalOutcomes = Math.Pow(6, N);
            double probability = dp[N, Q] / totalOutcomes;
            return probability;
        }
    }
}
