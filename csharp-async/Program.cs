using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncBreakfast
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var timeStart = DateTime.Now;
        //    var cup = PourCoffee();
        //    Console.WriteLine("Coffee is ready");

        //    var eggs = FryEggs(2);
        //    Console.WriteLine("Eggs are ready");

        //    var bacon = FryBacon(3);
        //    Console.WriteLine("Bacon is ready");

        //    var toast = ToastBread(2);
        //    ApplyButter(toast);
        //    ApplyJam(toast);
        //    Console.WriteLine("Toast is ready");

        //    var oj = PourOJ();
        //    Console.WriteLine("OJ is ready");
        //    Console.WriteLine("Breakfast is ready!");

        //    var timeEnd = DateTime.Now;
        //    Console.WriteLine("Time Taken: " + (timeEnd - timeStart).TotalSeconds);
        //}

        static async Task Main(string[] args)
        {
            var timeStart = DateTime.Now;

            int cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<int> taskEgg = FryEggsAsync(2);
            Task<int> taskBacon = FryBaconAsync(3);
            Task<int> taskToast = MakeToastWithButterAndJamAsync(2);
            //Task<int> taskToast = MakeToastWithButterAndJamAsyncException(2);

            try
            {
                //int eggs = await taskEgg;
                //Console.WriteLine("eggs are ready");

                //int bacon = await taskBacon;
                //Console.WriteLine("bacon is ready");

                //int toast = await taskToast;
                //Console.WriteLine("toast is ready");

                await Task.WhenAll(taskEgg, taskBacon, taskToast);
                Console.WriteLine("eggs are ready");
                Console.WriteLine("bacon is ready");
                Console.WriteLine("toast is ready");

                var tasks = new List<Task> { taskEgg, taskBacon, taskToast };
                while (tasks.Count > 0)
                {
                    var finishedTask = await Task.WhenAny(tasks);
                    if (finishedTask == taskEgg)
                    {
                        Console.WriteLine("eggs are ready");
                    }
                    else if (finishedTask == taskBacon)
                    {
                        Console.WriteLine("bacon is ready");
                    }
                    else if (finishedTask == taskToast)
                    {
                        Console.WriteLine("toast is ready");
                    }
                    tasks.Remove(finishedTask);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            int oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");

            var timeEnd = DateTime.Now;
            Console.WriteLine("Time Taken: " + (timeEnd - timeStart).TotalSeconds);
        }

        private static int PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return 0;
        }

        private static void ApplyJam(int toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(int toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static int ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Remove toast from toaster");

            return 0;
        }

        private static int FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put bacon on plate");

            return 0;
        }

        private static int FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return 0;
        }

        private static int PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return 0;
        }

        private static async Task<int> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return 0;
        }

        private static async Task<int> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return 0;
        }

        private static async Task<int> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return 0;
        }

        private static async Task<int> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private static async Task<int> MakeToastWithButterAndJamAsyncException(int number)
        {
            var toast = await ToastBreadAsync(number);
            Console.WriteLine("Fire! Toast is ruined!");
            throw new InvalidOperationException("The toaster is on fire");
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }
    }
}