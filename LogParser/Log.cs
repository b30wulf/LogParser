using System;
using System.Collections.Generic;
using System.Text;

namespace LogParser
{
    public class Log
    {
        public static void Normal(string msg, bool IsShowingTime)
        {
            if (IsShowingTime)
                LogTime();

            Console.WriteLine(msg);
        }

        public static void Error(string msg, bool IsShowingTime)
        {
            if (IsShowingTime)
                LogTime();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string msg, bool IsShowingTime)
        {
            if (IsShowingTime)
                LogTime();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Success(string msg, bool IsShowingTime)
        {
            if (IsShowingTime)
                LogTime();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogTime()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[" + DateTime.Now.ToString() + "] ");
        }

        public static string[] Copyright()
        {
            string[] author = { "===========================", 
                                "  Made by CosmoKotik", 
                                "  All rights are reserved", 
                                "  Version: 0.7.45",
                                "===========================",
                                " "};

            return author;
        }
    }
}
