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
            GetNumberOfLines();
            //BusiestHour();
            IterateThroughLogFile();
            Console.ReadLine();

        }

        public static void ReadLines()
        {
            var readLines = File.ReadLines(logFile);
        }

        private static void GetNumberOfLines()
        {
            var numberOfLines = File.ReadAllLines(logFile).Count();
            Console.WriteLine($"there are {numberOfLines} lines in this file ");

        }

        public static void BusiestHour()
        {
            var readLines = File.ReadLines(logFile);
            List<string> allDatesInLogFile = new List<string>();
            List<string> allTimesInLogFile = new List<string>();
            List<string> allHoursInLogFile = new List<string>();

            foreach (var line in readLines)
            {
                if (line.StartsWith("2018"))
                {

                    string[] splitBySpace = line.Split(' ');
                    string date = splitBySpace[0];
                    string time = splitBySpace[1];
                    allDatesInLogFile.Add(date);
                    allTimesInLogFile.Add(time);
                    string[] splitByColon = time.Split(':');
                    string hourLogged = splitByColon[0];
                    allHoursInLogFile.Add(hourLogged);

                    Console.WriteLine($"there are {date} lines in this file at {hourLogged} ");
                }



            }

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
                Console.WriteLine($"{date.Key} has {logEntriesOnThisDate.Count()} number of entries in total:");
                var times = logEntriesOnThisDate.GroupBy(log => log.time).ToList();
                
                foreach (var time in times)
                {
                    var hoursLoggedPerDate = time.Key;
                    var countOfLogsByHour = time.Count();
                   
                   // var counter = Enumerable.Repeat('#', time.Count());
                    
                        

                 //   Console.WriteLine($"for the hour {hoursLoggedPerDate} there were {countOfLogsByHour} number of Logs");
                    Console.WriteLine($"{hoursLoggedPerDate}:  {string.Concat(Enumerable.Repeat('#', time.Count()))}  {countOfLogsByHour}");
                 

                 

                }
               
            }
        }
    }
}
