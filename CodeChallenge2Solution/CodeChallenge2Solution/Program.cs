using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CodeChallenge2Solution
{
    using System.Threading;

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

                    var time = splitByColon[0];
                    if (time.StartsWith("0"))
                    {
                        time = time.Remove(0, 1);
                    }
                    logEntries.Add(
                            new EventLogEntry { date = splitBySpace[0], time = time}
                        );
                }
            }
            var dates = logEntries.GroupBy(log => log.date).ToList();

            foreach (var date in dates)
            {
                var logEntriesOnThisDate = logEntries.Where(x => x.date == date.Key).ToList();
                Console.WriteLine($"{date.Key}");

                //first loop through all of the hours
                for (int hour = 0; hour < 24; hour++)
                {
                    var logEntriesForThisHour = logEntriesOnThisDate.Where(x => x.time == hour.ToString()).ToList();
                    if (logEntriesForThisHour.Any())
                    {
                        var countOfLogsByHour = logEntriesForThisHour.Count;
                        var counter = string.Concat(Enumerable.Repeat('#', logEntriesForThisHour.Count));
                        Console.WriteLine($"{hour}:  {counter}  {countOfLogsByHour}");
                    }
                    else
                    {
                        Console.WriteLine($"{hour}: no logs");
                    }
                }
            }

            foreach (var date in dates)
            {
                var logEntriesOnThisDate = logEntries.Where(x => x.date == date.Key);
                var numberOfLogsPerDate = logEntriesOnThisDate.Count();
                var counter = string.Concat(Enumerable.Repeat('.', logEntriesOnThisDate.Count()));

                Console.WriteLine($"On {date.Key}:  {counter} - {numberOfLogsPerDate} ");
            }

        }
    }
}
