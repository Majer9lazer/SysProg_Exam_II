using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SysProg_Exam_II_Part
{
    public abstract class SysProgTasks
    {
        public static void Task_1(string processName = "system")
        {
            var processesFilteredByName = Process.GetProcessesByName(processName).Select(s => new
            {
                s.ProcessName,
                s.Threads
            }).ToList();
            if (processesFilteredByName.Any())
            {
                foreach (var proc in processesFilteredByName)
                {
                    Console.WriteLine($"Process name - {proc.ProcessName} , count of threads - {proc.Threads.Count}");
                }
            }
            else
                Console.WriteLine("We didn't find anything");
        }
        public static void Task_2(string commnad = "default")
        {
            switch (commnad.ToUpper())
            {
                case "CREATE PROCESS":
                    {
                        Console.Write("Process name :");
                        string procName = Console.ReadLine();
                        Console.Write("Optional arguments if exist");
                        string arguments = Console.ReadLine();
                        Process.Start(procName, arguments);
                        break;
                    }
                case "KILL PROCESS BY NAME":
                    {
                        Console.Write("Process name :");
                        string procName = Console.ReadLine();
                        Process.GetProcessesByName(procName).ToList().ForEach(f => f.Kill());
                        break;
                    }
                case "KILL PROCESS BY ID":
                    {
                        Console.Write("Process ID :");
                        string id = Console.ReadLine();
                        Regex e = new Regex("[0...9]");
                        if (e.IsMatch(id)) Process.GetProcessById(Int32.Parse(id)).Kill();
                        else Console.WriteLine("Your input string doesn't contain integers");
                        break;
                    }
                case "RESTART CURRENT PROCESS":
                    {
                        Console.Write("Process name :");
                        string procName = Console.ReadLine();
                        Process.GetProcessesByName(procName).ToList().ForEach(f => f.Kill());
                        Process.Start(procName);
                        break;
                    }
                default: { Console.WriteLine("We didnt't find any good command)"); break; }
            }
        }
        public static void Task_3<T>(T[,] matrix)
        {
            switch (matrix.GetLength(0) > 250 && matrix.GetLength(1) > 500)
            {
                case true:
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            int i1 = i;
                            dynamic sum = 0;
                            Thread t = new Thread(delegate ()
                            {
                                for (int j = 0; j < matrix.GetLength(1); j++)
                                    sum += matrix[i1, j];

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Sum of {i1} row - {sum}");
                                Console.ForegroundColor = ConsoleColor.White;
                            });
                            t.Start();
                        }
                        break;
                    }

                case false:
                    {
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            int i1 = i;
                            dynamic sum = 0;
                            Thread t = new Thread(delegate ()
                            {
                                for (int j = 0; j < matrix.GetLength(1); j++)
                                {
                                    Console.Write(matrix[i1, j] + " ");
                                    sum += matrix[i1, j];
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Sum of {i1} row - {sum}");
                                Console.ForegroundColor = ConsoleColor.White;
                            });
                            t.Start();
                            t.Join();
                        }
                        break;
                    }
            }
        }

        public static void Task_4()
        {
            Random rnd = new Random();
            Thread[] threads = new Thread[rnd.Next(10, 15)];

            for (int i = 0; i < threads.Length; i++)
            {
                int i1 = i;
                threads[i] = new Thread(() =>
                {
                    Console.WriteLine($"Thread[{i1}] Priority - {threads[i1].Priority}");
                    int a = rnd.Next(0, 4), b = rnd.Next(0, 1);
                    Console.WriteLine($"a - {a} , b - {b} of {i1}");
                    int res = AkkermanFunction(a, b);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"result of {i1} akkFunction - {res}");
                    Console.ForegroundColor = ConsoleColor.White;
                })
                {
                    Priority = (ThreadPriority)rnd.Next(0, 5)
                };
                threads[i].Start();
            }
        }
        private static int AkkermanFunction(int a, int b)
        {
            if (a == 0)
                return b + 1;
            if (b == 0)
                return AkkermanFunction(a - 1, 1);

            return AkkermanFunction(a - 1, AkkermanFunction(a, b - 1));
        }

        public static void Task_5()
        {
            Random rnd = new Random();
            Semaphore semaphore = new Semaphore(3, 3);
            Thread[] threads = new Thread[6];
            for (int i = 0; i < threads.Length; i++)
            {
                int count = 3;
                int i1 = i;
                threads[i] = new Thread(delegate ()
                    {
                        while (count > 0)
                        {
                            semaphore.WaitOne();
                            Console.WriteLine($"Coming in - {Thread.CurrentThread.Name}");
                            Console.WriteLine($"he is doing something - {Thread.CurrentThread.Name}");
                            Thread.Sleep(700);
                            Console.WriteLine($"He is going out - {Thread.CurrentThread.Name}");
                            semaphore.Release();
                            count--;
                            Thread.Sleep(700);
                        }
                    })
                    { Name = $"Thread[{i}]" };
                threads[i].Start();
            }
        }
    }
}
