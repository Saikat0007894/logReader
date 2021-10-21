using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace logReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var allLogs = new List<LogModel>();
            DirectoryInfo d = new DirectoryInfo(@"D:\Team\Saikat\logReader");
            FileInfo[] Files = d.GetFiles("*.txt");

            foreach (FileInfo file in Files)
            {
                using (StreamReader sr = new StreamReader(file.OpenRead()))
                {
                    string content = string.Empty;
                    while ((content = sr.ReadLine()) != null)
                    {
                        string[] contentArr = content.Split(" | ");

                        allLogs.Add(new LogModel()
                        {
                            Date = DateTime.Parse(contentArr[0]),
                            Code = int.Parse(contentArr[1]),
                            Message = contentArr[2]
                        });
                    }
                }
            }
             
            var code400Logs = allLogs.Where(x => x.Code == 400).OrderBy(x => x.Date);
            var code404Logs = allLogs.Where(x => x.Code == 404).OrderBy(x => x.Date);
            var code500Logs = allLogs.Where(x => x.Code == 500).OrderBy(x => x.Date);
            var code502Logs = allLogs.Where(x => x.Code == 502).OrderBy(x => x.Date);

            Console.WriteLine("Code | Last Occured On | Occurance");
            Console.WriteLine("400 | " + code400Logs.LastOrDefault()?.Date + " | " + code400Logs.Count());
            Console.WriteLine("404 | " + code404Logs.LastOrDefault()?.Date + " | " + code404Logs.Count());
            Console.WriteLine("500 | " + code500Logs.LastOrDefault()?.Date + " | " + code500Logs.Count());
            Console.WriteLine("502 | " + code502Logs.LastOrDefault()?.Date + " | " + code502Logs.Count());
        }
    }


    public class LogModel
    {
        public DateTime Date { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

    }
}

