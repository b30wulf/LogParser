using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace LogParser
{
    public class Loader
    {
        public Config cfg;
        public enum cfgType { SearchIP, ShowLogs, ShowTime, LogURL, LogUsername, LogPassword }

        private List<string> words = new List<string>();

        List<string> founded = new List<string>();

        public string[] GetLogs()
        {
            List<string> lines = new List<string>();
            int totalFiles = 0;

            for (int p = 0; p < cfg.LogPaths.Count; p++)
            {
                Log.Warning("Starting folder " + p, cfg.ShowTime);

                string folder = cfg.LogPaths[p];

                for (int i = 0; i < Directory.GetDirectories(folder).Length; i++)
                {
                    List<string> currentFounded = new List<string>();

                    string path = Directory.GetDirectories(folder)[i];
                    string filename = "Passwords.txt";

                    if (!File.Exists(path + @"\" + filename))
                    {
                        Log.Error("File " + path + @"\" + filename + " does exists", cfg.ShowTime);
                        totalFiles++;
                        continue;
                    }


                    bool hasFound = false;

                    using (StreamReader reader = new StreamReader(path + @"\" + filename))
                    {
                        while (reader.Peek() >= 0)
                        {
                            lines.Add(reader.ReadLine());
                        }

                        for (int y = 0; y < lines.Count; y++)
                        {
                            for (int x = 0; x < words.Count; x++)
                            {
                                if (lines[y].ToLower().Contains(words[x]) && !currentFounded.Contains(lines[y].ToLower()))
                                {
                                    //if (cfg.DeleteClone && founded.FindAll(zx => zx.IndexOf(lines[y], StringComparison.OrdinalIgnoreCase) >= 0).Count > 0
                                    //        && founded.FindAll(zy => zy.IndexOf(lines[y + 1], StringComparison.OrdinalIgnoreCase) >= 0).Count > 0
                                    //        && founded.FindAll(zz => zz.IndexOf(lines[y + 2], StringComparison.OrdinalIgnoreCase) >= 0).Count > 0)
                                    //{
                                    //    Log.Warning("Founded clone in " + path + @"\" + filename + "  [" + totalFiles + "]", cfg.ShowTime);
                                    //    
                                    //    continue;
                                    //}

                                    //if (cfg.DeleteClone && !currentFounded.Contains(lines[y].ToLower()))
                                    //{
                                    //    if (cfg.ShowCloneWarningLog)
                                    //        Log.Warning("Founded clone in " + path + @"\" + filename + "  [" + totalFiles + "]", cfg.ShowTime);
                                    //
                                    //    Log.Normal(lines[y] + " " + lines[y + 1] + " " + lines[y + 2], cfg.ShowTime);
                                    //    Log.Error(currentFounded.Count.ToString(), cfg.ShowTime);
                                    //
                                    //    continue;
                                    //}

                                    currentFounded.Add(lines[y].ToLower());

                                    founded.Add(lines[y]);
                                    founded.Add(lines[y + 1]);
                                    founded.Add(lines[y + 2]);
                                    founded.Add("========================================");

                                    Log.Success("Successfully added from " + path + @"\" + filename + " => " + lines[y].Split(" ")[1] + " => " + lines[y + 1].Split(" ")[1] + ":" + lines[y + 2].Split(" ")[1] + "  [" + totalFiles + "]", cfg.ShowTime);

                                    totalFiles++;
                                    hasFound = true;
                                }
                            }
                        }

                        if (!hasFound)
                        {
                            Log.Error(@"File " + path + @"\" + filename + " does not contain any key words" + "  [" + totalFiles + "]", cfg.ShowTime);
                        }

                        if (cfg.SaveFastLogs)
                            SaveLogs(false);

                        reader.Close();
                    }
                }

                Log.Warning("Finished folder " + p, cfg.ShowTime);
            }

            Log.Normal(totalFiles + " has been checked", cfg.ShowTime);
            return founded.ToArray();
        }

        public void LoadKeyWords()
        {
            string filename = "Words.ini";

            using (StreamReader reader = new StreamReader(filename))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    //Console.WriteLine(reader.ReadLine());

                    words.Add(line.ToLower());
                    //Console.WriteLine(words.Count);
                }

                if (cfg.SearchIP)
                { 
                    
                }

                reader.Close();
                //string line = reader.ReadLine();
            }
        }

        public void LoadConfigs()
        {
            //List<string> lines = new List<string>();
            string filename = "config.ini";

            using (StreamReader reader = new StreamReader(filename))
            {
                string json = reader.ReadToEnd();

                cfg = JsonConvert.DeserializeObject<Config>(json);

                reader.Close();
                //string line = reader.ReadLine();
            }
        }

        public bool SaveLogs(bool ShowLogs)
        {
            string filename = "Logs.txt";

            if (ShowLogs)
                Log.Normal("Saving...", cfg.ShowTime);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                /*for (int i = 0;  i < Log.Copyright().Length; i++)
                {
                    writer.WriteLine(Log.Copyright()[i]);
                }*/

                for (int i = 0; i < founded.Count; i++)
                {
                    writer.WriteLine(founded[i]);
                }

                writer.Flush();

                writer.Close();

                if (ShowLogs)
                    Log.Normal("Successfully saved to Logs.txt", cfg.ShowTime);

                return true;
            }
        }

        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            return true;
        }
    }

    public class Config
    {
        public bool SearchIP = false;
        
        public bool ShowTime = true;
        
        public bool SaveFastLogs = true;

        public bool DeleteClone = true;

        public bool ShowCloneWarningLog = false;
        
        public bool OptimizedLogPaths = false;

        public bool allowMultiThreads = true;

        public List<string> LogPaths = new List<string>();
    }
}
