using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger_w_Backups
{
    internal class Starter
    {
        public List<string> Logs = new List<string>();
        Logger Logger = new Logger();

        public int Iteration = 0;

        public void Run()
        {
            Console.WriteLine("The program is running");
            Console.WriteLine("----------------------");

            Task.WaitAll(WriteLogA(), WriteLogB());

        }

        public async Task WriteLogA()
        {
            await Task.Run(() => WriteLog("Log A"));
        }

        public async Task WriteLogB()
        {
            await Task.Run(() => WriteLog("Log B"));
        }

        public void WriteLog(string logName)
        {
                for (int i = 0; i < 50; i++)
                {
                    AddLog();
                }

                void AddLog()
                {
                    Iteration++;
                    var line = LogLine(Iteration, logName);
                    Console.WriteLine(line);
                    Logs.Add(line);
                    Logger.EventMethod(Iteration, Logs, logName);
                }

                string LogLine(int iter, string log)
                {
                    var logString = $"Iteration {iter} - {log}";
                    return logString;
                }            
        }
    }
}
