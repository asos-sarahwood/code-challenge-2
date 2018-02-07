using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CodeChallenge2Solution
{

    public class BusiestHour
    {
        public int hour { get; set; }
        public int count { get; set; }
    }


    public class Program
    {
        public static string logFile = @"C:\git\Code_Challenge2\log.file";


        public static void Main()
        {
            IterateThroughLogFile();
            Console.ReadLine();
        }
        public class EventLogEntry
        {
            public string date { get; set; }
            public string time { get; set; }
        }

        public static void IterateThroughLogFile()
        {
            var readLines = File.ReadLines(logFile);
            var logEntries = new List<EventLogEntry>();
            foreach (var line in readLines)
            {
                if (line.StartsWith("2018"))
                {
                    string[] splitBySpace = line.Split(' ');
                    string[] splitByColon = splitBySpace[1].Split(':');
                    logEntries.Add(
                            new EventLogEntry { date = splitBySpace[0], time = splitByColon[0]}
                        );
                }
            }
            var dates = logEntries.GroupBy(log => log.date).ToList();

            foreach (var date in dates)
            {
                var logEntriesOnThisDate = logEntries.Where(x => x.date == date.Key);
                var times = logEntriesOnThisDate.GroupBy(log => log.time).ToList();
             
                foreach (var time in times)
                {
                    var hourOfGroupedLogs = time.Key;
                    var countOfLogsByHour = time.Count();
                    var counter = string.Concat(Enumerable.Repeat('#', time.Count()));
                    Console.WriteLine($"{hourOfGroupedLogs}:  {counter}  {countOfLogsByHour}");
                }
            }

            foreach (var date in dates)
            {
                var logEntriesOnThisDate = logEntries.Where(x => x.date == date.Key);
                var times = logEntriesOnThisDate.GroupBy(log => log.time).ToList();
                var counter = string.Concat(Enumerable.Repeat('.', logEntriesOnThisDate.Count()));
                Console.WriteLine($"On the {date.Key} there were:  {counter}  ");
            }

        }
    }
}
