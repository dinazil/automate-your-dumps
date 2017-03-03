using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleCrasher
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new EasyConsole.Menu()
                .Add("Crash", Crash)
                .Add("Quit", () => Environment.Exit(0));

            while (true)
            {
                menu.Display();
                Console.WriteLine();
                Console.WriteLine("------------");
            }
        }

        private static void Crash()
        {
            const string root = @"Z:\";
            var list = new List<string>();
            var directories = Directory.GetDirectories(root);
            var tasks = new Task[directories.Length];
            for (var i = 0; i < directories.Length; ++i)
            {
                tasks[i] = Task.Factory.StartNew(d =>
                {
                    lock (list)
                    {
                        list.AddRange(Directory.EnumerateFiles((string)d));
                    }
                    Thread.Sleep(10); // Some more intesnive work
                }, directories[i]);
            }

            Thread.Sleep(10);
            foreach (var element in list)
            {
                Console.WriteLine(element);
                Thread.Sleep(10);
            }

            Task.WaitAll(tasks);
        }
    }
}
