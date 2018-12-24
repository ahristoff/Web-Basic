
namespace _1_Even_Numbers_Thread
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var min = int.Parse(Console.ReadLine());
            var max = int.Parse(Console.ReadLine());

            var thread = new Thread(() => PrintEvenNumbers(min, max));
            thread.Start();
            thread.Join();
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }            
            }
        }
    }
}
