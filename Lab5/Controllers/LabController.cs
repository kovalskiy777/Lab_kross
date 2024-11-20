using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Lab1;
using Lab2;
using Lab3;
using System.Text;
using TaskProcessorLib;


namespace Lab.Controllers
{
    public class LabController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LabController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Lab1_()
        {
            var model = new LabViewModel
            {
                TaskNumber = "1",
                TaskVariant = "24",
                TaskDescription = "Назвемо число гладким, якщо його цифри, починаючи зі старшого розряду, утворюють послідовність. \n Упорядкуємо усі такі числа у зростаючому порядку та надамо кожному номер.",
                InputDescription = "У вхідному файлі INPUT.TXT міститься номер N (1 ≤ N ≤ 2147483647).",
                OutputDescription = "Вихідний файл OUTPUT.TXT повинен містити шукане N гладке число.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "1", Output = "1" },
                new TestCase { Input = "11", Output = "12" },
                new TestCase { Input = "239", Output = "1135" }
            }
            };
            return View(model);
        }

        public IActionResult Lab2_()
        {
            var model = new LabViewModel
            {
                TaskNumber = "2",
                TaskVariant = "24",
                TaskDescription = "Кубик, грані якого позначені цифрами від 1 до 6, кидають N разів. \nПотрібно знайти ймовірність того, що сума чисел, що випали, дорівнюватиме Q.",
                InputDescription = "Вхідний файл INPUT.TXT містить натуральні числа N та Q (N ≤ 500, Q ≤ 3000).",
                OutputDescription = "У вихідний файл OUTPUT.TXT виведіть єдине речове число - ймовірність, яка повинна відрізнятися від справжнього значення не більше ніж на 10-6.",
                TestCases = new List<TestCase>
            {
                new TestCase { Input = "1 6", Output = "0.166667" },
                new TestCase { Input = "1 7", Output = "0" },
                new TestCase { Input = "4 14", Output = "0.112654" },
                new TestCase { Input = "100 100", Output = "1.530647E-78" }
            }
            };
            return View(model);
        }

        public IActionResult Lab3_()
        {
            var model = new LabViewModel
            {
                TaskNumber = "3",
                TaskVariant = "24",
                TaskDescription = "У місті N найближчим часом відбудеться етап чемпіонату світу з автоперегонів серед автомобілів класу Формула-0. Оскільки спеціальний автодром для цих змагань організатори збудувати не встигли, вирішили організувати трасу на вулицях міста.\r\n" +
                "У місті N є n перехресть, деякі пари яких з'єднані дорогами, рух якими можливий в обох напрямках. При цьому будь-які два перехрестя з'єднані не більш ніж однією дорогою, і є можливість доїхати дорогами від будь-якого перехрестя до іншого.\r\nТраса, на якій будуть проводитися змагання, повинна бути круговою (тобто повинна починатися і закінчуватися на тому самому перехресті), при цьому в процесі руху по ній ніяке перехрестя не повинно зустрічатися більше одного разу.\r\n" +
                "На попередньому етапі підготовки оргкомітетом було створено перелік усіх доріг міста. Тепер настав час його використати. Перше питання, яке необхідно вирішити, питання про існування в місті необхідної кругової траси (зрозуміло, якщо відповідь буде негативною, організаторам доведеться терміново побудувати ще кілька доріг). Єдина проблема полягає в тому, що організатори мають підозру, що, оскільки список складався не дуже уважно, в ньому деякі дороги вказані більше одного разу.\r\n" +
                "Напишіть програму, яка за заданим списком доріг міста визначить, чи можлива організація у місті колової траси.\r\n",
                InputDescription = "Перший рядок вхідного файлу INPUT.TXT містить два цілих числа: n (1 ≤ n ≤ 103) – кількість перехресть у місті N та m (0 ≤ m ≤ 105) – кількість доріг у складеному списку.",
                OutputDescription = "Наступні m рядків описують дороги. Кожна дорога описується двома числами: u та v (1 ≤ u, v ≤ n, u ≠ v) номерами перехресть, які вона з'єднує. Так як дороги двосторонні, то пара чисел (u, v) і пара чисел (v, u) описують ту саму дорогу.",
                TestCases = new List<TestCase>
            {
                new TestCase
                {
                    Input = "3 4\r\n1 2\r\n2 3\r\n3 1\r\n3 2\r\n",
                    Output = "YES"
                },
                 new TestCase
                {
                    Input = "2 3\r\n1 2\r\n2 1\r\n2 1\r\n",
                    Output = "NO"
                }
            }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLab(int labNumber, IFormFile inputFile)
        {
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest("Please upload a file");

            // Read file contents into a string array
            string[] lines;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                lines = fileContent.Split(Environment.NewLine); // Split into lines
            }

            // Variable to store the processed result
            string output;
            StringBuilder result = new StringBuilder();

            // Execute the lab processing method based on lab number
            switch (labNumber)
            {
                case 1:
                    output = Lab1.Program.ProcessLab1(lines);
                    break;
                case 2:
                    foreach (var line in lines)
                    {
                        result.AppendLine(Lab2.Program.ProcessLab2(line));
                    }
                    output = result.ToString().Trim();
                    break;
                case 3:
                    output = TaskProcessor.ProcessTask(lines);
                    break;
                default:
                    return BadRequest("Invalid lab number");
            }

            // Return result as JSON
            var result_ = new { output = output };
            return Json(result_);
        }

    }
}
