using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Logger_w_Backups
{
    internal class Logger
    {
        public delegate void EventHandler(int iteration, List<string> logs, string log);
        public event EventHandler DoBackup;

        string ConfigPath = @"D:\Projects\C# learning\Code\Logger_w_Backups_M3_P4\Logger_w_Backups\Configs\config.json";


        public Logger()
        {
            DoBackup += MakeBackup;
        }

        public void EventMethod(int iteration, List<string> logs, string log)
        {
            DoBackup(iteration, logs, log);
        }

        private void MakeBackup(int iteration, List<string> logs, string log)
        {
            var backupAt = DoBackupAt(ConfigPath);
            if (iteration % backupAt == 0)
            {
                Console.WriteLine("Backup!");
                ListToFile(logs, iteration, log);
            }
        }


        private int DoBackupAt(string configPath)
        {
            // Reads backup condition from json file
            string json = File.ReadAllText(ConfigPath);
            var logConfig = JsonConvert.DeserializeObject<ConfigService>(json);
            int backupAt = logConfig.BackupAt;
            return backupAt;
        }

        public void ListToFile(List<string> logs, int iteration, string log)
        {
            var dateTime = DateTime.UtcNow;
            var fileName = TimeToName(dateTime);

            string backupPath = @$"D:\Projects\C# learning\Code\Logger_w_Backups_M3_P4\Logger_w_Backups\Backups\{iteration}{log}-{fileName}.txt";
            File.WriteAllLines(backupPath, logs);


            string TimeToName(DateTime time)
            {                
                var mils = time.Millisecond.ToString();
                var fileName = time.ToString() + mils;
                char[] analyse = fileName.ToCharArray();
                for (int i = 0; i < analyse.Length; i++)
                {
                    if (Char.IsDigit(analyse[i]))
                    {
                        continue;
                    }
                    else
                    {
                        analyse[i] = '_';
                    }
                }
                fileName = new string(analyse);

                Console.WriteLine(fileName);
                return fileName;
                
            }
        }
    }
}
