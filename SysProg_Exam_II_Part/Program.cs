using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WhatsAppApi;

namespace SysProg_Exam_II_Part
{
    class Program
    {
        static void ShowMenu(int countOfTasks)
        {
            string commandToInput = "Write number of task to check";
            Console.WriteLine();
            string splitPlus = "";
            string split = "";

            for (int i = 0; i < commandToInput.Length; i++)
                splitPlus += "+";

            Console.WriteLine(" " + splitPlus);

            for (int i = 1; i <= 9; i++)
                Console.WriteLine($" Task_{i} - [{i}]");

            Console.WriteLine(" " + splitPlus);

            for (int i = 0; i < commandToInput.Length; i++)
                split += "-";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" " + split);
            Console.WriteLine(" " + commandToInput);
            Console.WriteLine(" " + split);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {
            ShowMenu:
            ShowMenu(9);
            // ReSharper disable once InlineOutVariableDeclaration
            int defTask;
            defTask = 1;
            int.TryParse(Console.ReadLine(), out defTask);
            try
            {
                switch (defTask)
                {
                    case 1:
                        {
                            Console.Write("Write process name here :");
                            SysProgTasks.Task_1(Console.ReadLine());
                            break;
                        }
                    case 2:
                        {
                            string[] commands =
                                {"CREATE PROCESS", "KILL PROCESS BY NAME", "KILL PROCESS BY ID", "RESTART CURRENT PROCESS"};
                            Console.WriteLine("Commands :");

                            foreach (string s in commands)
                                Console.WriteLine(" " + s);

                            Console.WriteLine("Write Command Here");

                            SysProgTasks.Task_2(Console.ReadLine());
                            break;
                        }
                    case 3:
                        {

                            Console.WriteLine("Write count of rows in matrix :");
                            int rows = int.Parse(Console.ReadLine());
                            Console.WriteLine("Write count of columns in mstrix :");
                            int columns = int.Parse(Console.ReadLine());
                            int[,] matrix = new int[rows, columns];
                            Random rnd = new Random();
                            for (int i = 0; i < matrix.GetLength(0); i++)
                            {
                                for (int j = 0; j < matrix.GetLength(1); j++)
                                {
                                    matrix[i, j] = rnd.Next(0, 10);
                                }
                            }

                            SysProgTasks.Task_3(matrix);

                            break;
                        }
                    case 4:
                        {
                            SysProgTasks.Task_4();
                            break;
                        }
                    case 5:
                        {
                            SysProgTasks.Task_5();
                            break;
                        }
                    case 6:
                        {
                            Task_6();
                            break;
                        }

                    default:
                        Console.WriteLine("SomeThing Went Wrong");
                        Console.ReadLine();
                        Console.Clear();
                        goto ShowMenu;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.ReadLine();
                Console.Clear();
            }
            goto ShowMenu;
        }
        public static void Task_6()
        {
            foreach (Process p in Process.GetProcesses())
            {
                Console.WriteLine($"Name - {p.ProcessName}");
            }
            Mutex m = new Mutex();
            Console.WriteLine("Cin Process Name");
            string ProcName = Console.ReadLine();
            var ProcByName = Process.GetProcesses().Where(w => w.ProcessName.Contains(ProcName)).ToList();
            foreach (Process p in ProcByName)
            {
                Console.WriteLine($"name - {p.ProcessName},id - {p.Id}");
            }
        }
    }
}
