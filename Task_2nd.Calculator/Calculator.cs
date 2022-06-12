namespace Task_2nd.Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"Choose a math operation.  To select an operation, press one of the following keys{Environment.NewLine}");

                Console.WriteLine($"/ - division{Environment.NewLine}" +
                                  $"* - multiplication{Environment.NewLine}" +
                                  $"+ - sum{Environment.NewLine}" +
                                  $"- - difference{Environment.NewLine}" +
                                  $"^ - raising to power!{Environment.NewLine}" +
                                  $"! - factorial{Environment.NewLine}" +
                                  $"esc - exit{Environment.NewLine}");

                ConsoleKey choice = Console.ReadKey().Key;

                if (choice == ConsoleKey.Escape)
                {
                    break;
                }

                Console.Clear();

                ChooseTheMathOperation(choice);
            }
        }

        static void ChooseTheMathOperation(ConsoleKey operation)
        {
            switch (operation)
            {
                case ConsoleKey.D1:
                    GetTheFactorial();
                    break;

                case ConsoleKey.D6:
                    Exponentiate();
                    break;

                case ConsoleKey.D8:
                case ConsoleKey.Multiply:
                    Multiply(GetOperands(OperationNames.multiplication));
                    break;

                case ConsoleKey.Add:
                case ConsoleKey.OemPlus:
                    Summarize(GetOperands(OperationNames.summation));
                    break;

                case ConsoleKey.Subtract:
                case ConsoleKey.OemMinus:
                    Subtract(GetOperands(OperationNames.subtraction));
                    break;

                case ConsoleKey.Divide:
                case ConsoleKey.Oem5:
                    Divide(GetOperands(OperationNames.division));
                    break;

                default:
                    Console.WriteLine($"Input Error. Please make the right choice.{Environment.NewLine}");
                    break;
            }

        }

        static double[] GetOperands(OperationNames operationName)
        {
            Console.WriteLine($"Enter the number of operands for {operationName} and press Entre");

            var isNumber = uint.TryParse(Console.ReadLine(), out var numberOfOperands);

            while (!isNumber)
            {
                Console.WriteLine("You introduced a invalid value");
                Console.WriteLine("Enter the number of operands");

                isNumber = uint.TryParse(Console.ReadLine(), out numberOfOperands);
            }

            double[] arrayOfOperands;

            if (numberOfOperands == 0)
            {
                return Array.Empty<double>();
            }
            else
            {
                arrayOfOperands = new double[numberOfOperands];

                var random = new Random();

                for (uint i = 0; i < numberOfOperands; i++)
                {
                    arrayOfOperands[i] = (double)random.Next(int.MinValue, int.MaxValue);
                }
                return arrayOfOperands;
            }
        }

        static void PrintResult(OperationNames operationName, double result, int number)
        {
            Console.Clear();
            Console.WriteLine($"Operation: {operationName}{Environment.NewLine}Result: {result}{Environment.NewLine}Random namber for operation: {number}{Environment.NewLine}");
        }

        static void PrintResult(OperationNames operationName, double result, double[] numbers)
        {
            if (operationName == OperationNames.exponentiation)
            {
                Console.Clear();
                Console.WriteLine($"Operation: {operationName}{Environment.NewLine}Result: {result}{Environment.NewLine}Random namber(s) for operation: {numbers[0]}, exponent:{numbers[1]}{Environment.NewLine}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Operation: {operationName}{Environment.NewLine}Result: {result}{Environment.NewLine}Random namber(s) for operation:");

                foreach (var namber in numbers)
                {
                    Console.WriteLine($"\t{namber}");
                }
                Console.WriteLine($"{Environment.NewLine}");
            }
        }

        static void Multiply(double[] arrayOfNumbers)
        {
            if (arrayOfNumbers == Array.Empty<double>())
            {
                PrintResult(OperationNames.multiplication, 0, arrayOfNumbers);
            }
            else
            {
                double result = 1;

                foreach (var item in arrayOfNumbers)
                {
                    result *= item;
                }

                if (double.IsInfinity(result))
                {
                    Console.WriteLine($"{Environment.NewLine}The result obtained is out of bounds of type double.");
                }
                else
                {
                    PrintResult(OperationNames.multiplication, result, arrayOfNumbers);
                }
            }
        }

        static void Divide(double[] arrayOfNumbers)
        {
            if (arrayOfNumbers == Array.Empty<double>())
            {
                PrintResult(OperationNames.division, 0, arrayOfNumbers);
            }

            else
            {
                double result = arrayOfNumbers[0];

                bool ivalidOperand = false;

                for (int i = 1; i < arrayOfNumbers.Length; i++)
                {
                    if (arrayOfNumbers[i] == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("One of the elements is 0. The division by 0 is prohibited.");

                        foreach (var item in arrayOfNumbers)
                        {
                            Console.WriteLine($"\t{item}");
                        }

                        ivalidOperand = true;

                        break;
                    }
                    else
                    {
                        result /= arrayOfNumbers[i];

                        if (double.IsInfinity(result))
                        {
                            Console.WriteLine($"{Environment.NewLine}The result obtained is out of bounds of type double.");
                        }
                    }
                }

                if (!ivalidOperand)
                {
                    PrintResult(OperationNames.division, result, arrayOfNumbers);
                }
            }
        }

        static void Summarize(double[] arrayOfNumbers)
        {
            if (arrayOfNumbers == Array.Empty<double>())
            {
                PrintResult(OperationNames.summation, 0, arrayOfNumbers);
            }
            else
            {
                double result = 0;

                bool isException = false;

                foreach (var item in arrayOfNumbers)
                {
                    if ((result < 0 && item < 0 && double.MinValue - result > item) || (result > 0 && item > 0 && double.MaxValue - result < item))
                    {
                        Console.WriteLine($"{Environment.NewLine}The result obtained is out of bounds of type double.");

                        isException = true;

                        break;
                    }
                    else
                    {
                        result += item;
                    }
                }

                if (!isException)
                {
                    PrintResult(OperationNames.summation, result, arrayOfNumbers);
                }
            }
        }

        static void Subtract(double[] arrayOfNumbers)
        {
            if (arrayOfNumbers == Array.Empty<double>())
            {
                PrintResult(OperationNames.subtraction, 0, arrayOfNumbers);
            }
            else
            {
                double result = arrayOfNumbers[0];

                bool isException = false;

                for (int i = 1; i < arrayOfNumbers.Length; i++)
                {
                    if ((result < 0 && arrayOfNumbers[i] > 0 && Math.Abs(double.MinValue - result) < arrayOfNumbers[i])
                        || (result > 0 && arrayOfNumbers[i] < 0 && double.MaxValue - result < Math.Abs(arrayOfNumbers[i])))
                    {
                        Console.WriteLine($"{Environment.NewLine}The result obtained is out of bounds of type double.");

                        isException = true;

                        break;
                    }
                    else
                    {
                        result -= arrayOfNumbers[i];
                    }
                }

                if (!isException)
                {
                    PrintResult(OperationNames.subtraction, result, arrayOfNumbers);
                }
            }
        }

        static void Exponentiate()
        {
            var random = new Random();

            double number = (double)random.Next(0, 1000);

            double powerOfNumber = (double)random.Next(-10, 100);

            double result = Math.Pow(number, powerOfNumber);

            if (double.IsInfinity(result))
            {
                Console.WriteLine($"{Environment.NewLine}The result obtained is out of bounds of type double.");
            }
            else
            {
                double[] numbers = { number, powerOfNumber };

                PrintResult(OperationNames.exponentiation, result, numbers);
            }
        }

        static void GetTheFactorial()
        {
            var random = new Random();

            int number = random.Next(0, 100);

            int result = 1;

            if (number < 0)
            {
                Console.WriteLine($"{Environment.NewLine}In mathematics, the factorial of a non-negative integer.");
            }
            if (number != 0)
            {
                for (int i = 1; i <= number; i++)
                {
                    try
                    {
                        result = checked(result * i);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("The result obtained is out of bounds of type int.");

                        break;
                    }
                }
                PrintResult(OperationNames.factorial, result, number);
            }
        }
    }
}

