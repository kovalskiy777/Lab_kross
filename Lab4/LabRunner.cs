using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1;
using Lab2;
using TaskProcessorLib;

namespace Lab4
{
    internal class LabRunner
    {
        public void RunLab1(string inputFile, string outputFile)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string[] lines = File.ReadAllLines(inputFile);

                string result = Lab1.Program.ProcessLab1(lines); 

                File.WriteAllText(outputFile, result.Trim());

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
        }

        public void RunLab2(string inputFile, string outputFile)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string[] lines = File.ReadAllLines(inputFile);

                StringBuilder result = new StringBuilder();
                foreach (var line in lines)
                {
                    result.AppendLine(Lab2.Program.ProcessLab2(line));
                }

                File.WriteAllText(outputFile, result.ToString().Trim()); 

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
        }
        public void RunLab3(string inputFile, string outputFile)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string[] lines = File.ReadAllLines(inputFile);

                string result = TaskProcessorLib.TaskProcessor.ProcessTask(lines); 

                File.WriteAllText(outputFile, result.Trim()); 

                Console.WriteLine("File OUTPUT.TXT successfully created");
                Console.WriteLine("LAB #1");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(result.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
