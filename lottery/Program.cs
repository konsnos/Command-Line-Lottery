using System;
using System.Threading;

namespace lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Error....");
            Console.WriteLine();
            
            while (!Console.KeyAvailable)
                Thread.Sleep(250);
            Console.ReadKey(true);
            Console.WriteLine("Error λέμε....");
            Console.WriteLine();

            while (!Console.KeyAvailable)
                Thread.Sleep(250);
            Console.ReadKey(true);
            Console.WriteLine("Καλά....");
            Console.WriteLine();

            while (!Console.KeyAvailable)
                Thread.Sleep(250);
            Console.ReadKey(true);

            Lottery lot = new Lottery();

            Console.WriteLine("Press ESC to close");
            do
            {
                while (!Console.KeyAvailable)
                    Thread.Sleep(250);
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
