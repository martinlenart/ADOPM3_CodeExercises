using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public class AppLogItem
    {        
        public DateTime Time {get; set;}
        public string Type { get; set; }
        public string[] Info { get; set; }
        public override string ToString()
        {
            var sReturn = $"{Type} at {Time}:\n";
            foreach (var item in Info)
            {
                sReturn += $"{item}\n";
            }
            return sReturn;
        }
    }

    //Example of a logger using Singleton design pattern
    public sealed class AppLog
    {
        const string LoggerFile = "CodeExercise_Logger.text";
        private static AppLog _instance = null;

        private static Stack<AppLogItem> _logStack = null;

        //This is key for Singleton
        private AppLog()
        {
            _logStack = new Stack<AppLogItem>();
        }

        //This is the key for Singelton
        public static AppLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppLog();
                }
                return _instance;
            }
        }

        public string WriteToDisk()
        {
            string logFile = fname(LoggerFile);
            using (Stream s = File.Create(logFile))
            using (TextWriter w = new StreamWriter(s))
            {
                foreach(var item in this.ToList())
                {
                    w.WriteLine(item);
                }
             }

            return logFile;
        }

        public void LogInformation(params string[] info)
        {
            _logStack.Push(new AppLogItem { Time = DateTime.Now, Type = "Information", Info = info });
        }

        public void LogException(Exception ex)
        {
            var item = new AppLogItem();
            item.Time = DateTime.Now;
            item.Type = "Exception";
            item.Info = new string[3];
            item.Info[0] = ex?.GetType().Name;
            item.Info[1] = ex?.Message;
            item.Info[2] = ex?.InnerException?.Message;
            _logStack.Push(item);
        }

        public List<AppLogItem> ToList()
        {
            return _logStack.ToList<AppLogItem>();
        }

        string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = Path.Combine(documentPath, "ADOP", "Examples");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        } 
    }
}

