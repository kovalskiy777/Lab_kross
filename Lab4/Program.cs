using System.Runtime.InteropServices;
using McMaster.Extensions.CommandLineUtils;

namespace Lab4
{

    [Command(Name = "Lab4", Description = "Console app for labs")]
    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication<Program>();
            app.Conventions.UseDefaultConventions();

            try
            {
                return app.Execute(args);
            }
            catch (CommandParsingException)
            {
                ShowUsageGuide();
                return 0;
            }
        }

        private int OnExecute(CommandLineApplication app)
        {
            ShowUsageGuide();
            return 0;
        }

        private static void ShowUsageGuide()
        {
            Console.WriteLine("Unknown command. Use one of the following:");
            Console.WriteLine(" - version: Displays app version and author");
            Console.WriteLine(" - run: Run a specific lab");
            Console.WriteLine(" - set-path: Set input/output path");
        }

        [Command("version", Description = "Displays app version and author")]
        class VersionCommand
        {
            private int OnExecute()
            {
                Console.WriteLine("Author: Artem Kovalskyi IPЗ-33");
                Console.WriteLine("Version: 1.0.0");
                return 0;
            }
        }

        [Command("run", Description = "Run a specific lab")]
        class RunCommand
        {
            [Argument(0, "lab", "Specify lab to run (lab1, lab2, lab3)")]
            public string Lab { get; set; }

            [Option("-i|--input", "Input file", CommandOptionType.SingleValue)]
            public string InputFile { get; set; }

            [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
            public string OutputFile { get; set; }

            private int OnExecute()
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
                string inputPath = InputFile ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                string outputPath = OutputFile ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                inputPath = Path.Combine(inputPath, "INPUT.txt");
                outputPath = Path.Combine(outputPath, "OUTPUT.txt");
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"Файл {inputPath} не знайдено.");
                    return 1;
                }

                var runner = new LabRunner();

                switch (Lab?.ToLower())
                {
                    case "lab1":
                        runner.RunLab1(inputPath, outputPath);
                        break;
                    case "lab2":
                        runner.RunLab2(inputPath, outputPath);
                        break;
                    case "lab3":
                        runner.RunLab3(inputPath, outputPath);
                        break;
                    default:
                        return 1;
                }

                Console.WriteLine($"Lab {Lab} processed. Output saved to {outputPath}");
                return 0;
            }
        }

        [Command("set-path", Description = "Set input/output path")]
        class SetPathCommand
        {
            [Option("-p|--path", "Шлях до папки", CommandOptionType.SingleValue)]
            public string Path { get; set; }

            private int OnExecute()
            {
                if (string.IsNullOrEmpty(Path))
                {
                    Console.WriteLine("Шлях не вказано.");
                    return 1;
                }

                try
                {
                    SetEnvironmentVariable("LAB_PATH", Path);
                    Console.WriteLine($"Змінна LAB_PATH встановлена на: {Path}");
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не вдалося встановити змінну середовища: {ex.Message}");
                    return 1;
                }
            }

            private void SetEnvironmentVariable(string variable, string value)
            {
                if (OperatingSystem.IsWindows())
                {
                    Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Machine);
                }
                else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    string profilePath = OperatingSystem.IsLinux() ? "/etc/environment" : "/etc/paths";

                    if (File.Exists(profilePath))
                    {
                        using (StreamWriter sw = File.AppendText(profilePath))
                        {
                            sw.WriteLine($"{variable}={value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Системний файл для змінних середовища не знайдено.");
                        throw new InvalidOperationException("Невдале встановлення змінної середовища.");
                    }
                }
            }
        }

    }



}