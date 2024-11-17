using System;
using System.Collections.Generic;
using System.IO;
using TaskProcessorLib;

namespace Lab3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string inputFilePath = Path.Combine("Lab3", "INPUT.TXT");  
                string outputFilePath = Path.Combine("Lab3", "OUTPUT.TXT"); 

                string[] lines = File.ReadAllLines(inputFilePath);

                Validate(lines);

                string result = TaskProcessor.ProcessTask(lines);

                File.WriteAllText(outputFilePath, result);
                Console.WriteLine("Lab3");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(result.ToString().Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void Validate(string[] lines)
        {
            if (lines.Length < 1)
                throw new InvalidOperationException("Input must contain at least one line for n and m.");

            string[] firstLine = lines[0].Split();
            if (firstLine.Length != 2)
                throw new InvalidOperationException("The first line must contain exactly two integers n and m.");

            if (!int.TryParse(firstLine[0], out int n) || !int.TryParse(firstLine[1], out int m))
                throw new InvalidOperationException("The values of n and m must be integers.");

            if (n < 1 || n > 103)
                throw new InvalidOperationException("The value of n must be between 1 and 103.");

            if (m < 0 || m > 105)
                throw new InvalidOperationException("The value of m must be between 0 and 105.");

            if (lines.Length != m + 1)
                throw new InvalidOperationException("The number of road entries does not match m.");

            for (int i = 1; i <= m; i++)
            {
                string[] road = lines[i].Split();
                if (road.Length != 2)
                    throw new InvalidOperationException($"Line {i + 1} must contain exactly two integers u and v.");

                if (!int.TryParse(road[0], out int u) || !int.TryParse(road[1], out int v))
                    throw new InvalidOperationException($"The values of u and v on line {i + 1} must be integers.");

                if (u < 1 || u > n || v < 1 || v > n || u == v)
                    throw new InvalidOperationException($"Invalid road entry on line {i + 1}: u and v must be in range [1, n] and not equal.");
            }
        }
    }
}
