using System;
using System.Linq;
using System.Collections.Generic;

namespace linq_101
{
    internal class Program
    {
        private void LinqWhere()
        {
            List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums = from num in numbers
                          where num < 5
                          select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }

            var lowNums2 = numbers.Where(num => num < 5);

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums2)
            {
                Console.WriteLine(x);
            }
        }

        private void LinqSelect()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var numsInPlace = numbers.Select((num, index) => (Num: num, InPlace: (num == index)));

            Console.WriteLine("Number: In-place?");
            foreach (var n in numsInPlace)
            {
                Console.WriteLine($"{n.Num}: {n.InPlace}");
            }

            var numLess5 = from n in numbers
                           where n < 5
                           select digits[n];
            Console.WriteLine("Numbers < 5:");
            foreach (var num in numLess5)
            {
                Console.WriteLine(num);
            }
        }

        private void LinqGroupBy()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numberGroups = from n in numbers
                               group n by n % 5 into g
                               select (Remainder: g.Key, Numbers: g);

            foreach (var g in numberGroups)
            {
                Console.WriteLine($"Numbers with a remainder of {g.Remainder} when divided by 5:");
                foreach (var n in g.Numbers)
                {
                    Console.WriteLine(n);
                }
            }

            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };

            var wordGroups = from w in words
                             group w by w[0] into g
                             select (FirstLetter: g.Key, Words: g);

            foreach (var g in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", g.FirstLetter);
                foreach (var w in g.Words)
                {
                    Console.WriteLine(w);
                }
            }
        }

        private void LinqConvert()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            var sortedDoubles = from d in doubles
                                orderby d descending
                                select d;
            var doublesArray = sortedDoubles.ToArray();

            Console.WriteLine(doubles.GetType() + " ==> " + sortedDoubles.GetType());

            Console.WriteLine("Every other double from highest to lowest:");
            for (int d = 0; d < doublesArray.Length; d += 2)
            {
                Console.WriteLine(doublesArray[d]);
            }

            var scoreRecords = new[] { new {Name = "Alice", Score = 50},
                                new {Name = "Bob"  , Score = 40},
                                new {Name = "Cathy", Score = 45}
                            };

            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);
            Console.WriteLine(scoreRecords.GetType() + " ==> " + scoreRecordsDict.GetType());

            Console.WriteLine("Bob's score: {0}", scoreRecordsDict["Bob"]);
        }

        private void LinqFirst()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            //string startsWithO = strings.First(s => s[0] == 'o');
            string startsWithO = strings.First(s => s.StartsWith('o'));

            Console.WriteLine($"A string starting with 'o': {startsWithO}");

            int[] numbers = { };

            int firstNumOrDefault = numbers.FirstOrDefault();

            Console.WriteLine(firstNumOrDefault);
        }

        private void LinqRange()
        {
            var numbers = from n in Enumerable.Range(100, 50)
                          select (Number: n, OddEven: n % 2 == 1 ? "odd" : "even");

            foreach (var n in numbers)
            {
                Console.WriteLine("The number {0} is {1}.", n.Number, n.OddEven);
            }
        }

        private void LinqAggregate()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Console.WriteLine($"Total product of all numbers: {product}");

            double startBalance = 100.0;

            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };

            double endBalance =
                attemptedWithdrawals.Aggregate(startBalance,
                    (balance, nextWithdrawal) => {
                        var bal = ((nextWithdrawal <= balance) ? (balance - nextWithdrawal) : balance);
                        Console.WriteLine(nextWithdrawal + " : " + bal);
                        return bal;
                        });

            Console.WriteLine($"Ending balance: {endBalance}");
        }

        private void LinqZip()
        {
            int[] vectorA = { 0, 2, 4, 5, 6, 10, 200 };
            int[] vectorB = { 1, 3, 5, 7, 8, 10 };

            int dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();

            Console.WriteLine($"Dot product: {dotProduct}");
        }

        private void LinqLazyAndEagerQuery()
        {
            // Sequence operators form first-class queries that
            // are not executed until you enumerate over them.

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var q = from n in numbers
                    select ++i;
            Console.WriteLine(i);

            // Note, the local variable 'i' is not incremented
            // until each element is evaluated (as a side-effect):
            foreach (var v in q)
            {
                Console.WriteLine($"v = {v}, i = {i}");
            }

            // Methods like ToList() cause the query to be
            // executed immediately, caching the results.

            int[] numbers2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i2 = 0;
            var q2 = (from n in numbers
                     select ++i2)
                     .ToList();
            Console.WriteLine(i2);

            // The local variable i has already been fully
            // incremented before we iterate the results:
            foreach (var v in q2)
            {
                Console.WriteLine($"v = {v}, i = {i2}");
            }

            // Deferred execution lets us define a query once
            // and then reuse it later after data changes.

            int[] numbers3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var lowNumbers = from n in numbers
                             where n <= 3
                             select n;

            Console.WriteLine("First run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                Console.WriteLine(n);
            }

            for (int i3 = 0; i3 < 10; i3++)
            {
                numbers[i3] = -numbers[i3];
            }

            // During this second run, the same query object,
            // lowNumbers, will be iterating over the new state
            // of numbers[], producing different results:
            Console.WriteLine("Second run numbers <= 3:");
            foreach (int n in lowNumbers)
            {
                Console.WriteLine(n);
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.LinqWhere();
            //program.LinqSelect();
            //program.LinqGroupBy();
            //program.LinqConvert();
            //program.LinqFirst();
            //program.LinqRange();
            //program.LinqAggregate();
            //program.LinqZip();
            program.LinqLazyAndEagerQuery();
        }
    }
}
