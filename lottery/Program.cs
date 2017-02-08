using System;
using System.Threading;

namespace lottery
{
    class Program
    {
        static void Main(string[] args)
        {
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
