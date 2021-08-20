using System;

namespace LogParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Version 0.5.1");

            for (int i = 0; i < Log.Copyright().Length; i++)
            {
                Console.WriteLine(Log.Copyright()[i]);
            }

            Loader ldr = new Loader();

            ldr.LoadConfigs();

            ldr.LoadKeyWords();

            string[] logs = ldr.GetLogs();

            ldr.SaveLogs(true);

            Console.WriteLine("Press any button to exit parser...");
            Console.ReadLine();
        }
    }
}
