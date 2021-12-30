using System;
using System.Collections.Generic;

namespace FirstProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //string name = "Hello Vedraj";
            //Console.WriteLine(name);

            Program program = new Program();

            //program.ExploreIf();
            //program.ExploreLoops();

            //var sum = program.BranchLoopExercise();
            //Console.WriteLine($"Sum: {sum}");

            //program.ExploreList();
            program.Fibonacci();
        }

        internal void Fibonacci()
        {
            var fibonacciNumbers = new List<int> { 1, 1 };

            while (fibonacciNumbers.Count < 20)
            {
                var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
                var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

                fibonacciNumbers.Add(previous + previous2);
            }

            foreach (var item in fibonacciNumbers)
                Console.WriteLine(item);
        }

        internal void ExploreList()
        {
            var names = new List<string> { "Name", "Vedraj", "Natasha", "Someone else" };

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("==========");
            names.Remove("Name");
            names.RemoveAt(names.Count - 1);    //"Someone else"
            names.Add("Blu");
            names.Add("Niia");
            names.Add("Nam");
            foreach (var name in names)
            {
                Console.WriteLine($"{name.ToUpper()}!");
            }

            Console.WriteLine("==========");
            Console.WriteLine("'Natasha' at index: " + names.IndexOf("Natasha"));
            Console.WriteLine("'Nothing' at index: " + names.IndexOf("Nothing"));

            Console.WriteLine("==========");
            names.Sort();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }

        internal int BranchLoopExercise()
        {
            int sum = 0;
            for (int i = 1; i <= 20; i++)
            {
                if (0 == (i % 3))
                    sum += i;
            }
            return sum;
        }

        internal void ExploreIf()
        {
            int a = 5;
            int b = 3;
            if (a + b > 10)
            {
                Console.WriteLine("The answer is greater than 10");
            }
            else
            {
                Console.WriteLine("The answer is not greater than 10");
            }

            int c = 4;
            if ((a + b + c > 10) && (a > b))
            {
                Console.WriteLine("The answer is greater than 10");
                Console.WriteLine("And the first number is greater than the second");
            }
            else
            {
                Console.WriteLine("The answer is not greater than 10");
                Console.WriteLine("Or the first number is not greater than the second");
            }

            if ((a + b + c > 10) || (a > b))
            {
                Console.WriteLine("The answer is greater than 10");
                Console.WriteLine("Or the first number is greater than the second");
            }
            else
            {
                Console.WriteLine("The answer is not greater than 10");
                Console.WriteLine("And the first number is not greater than the second");
            }
        }

        internal void ExploreLoops()
        {
            for (int row = 1; row < 11; row++)
            {
                for (char column = 'a'; column < 'k'; column++)
                {
                    Console.WriteLine($"The cell is ({row}, {column})");
                }
            }
        }
    }
}
