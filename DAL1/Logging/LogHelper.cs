using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Logging
{
    [ExcludeFromCodeCoverage]
    public class LogHelper
    {
        public static void Log(string text)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\Log\\";
            string filename = "WebAppLog_" + DateTime.Now.ToShortDateString() + ".txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter w = File.AppendText(path + filename))
            {
                //w.Write(DateTime.Now.ToLongDateString() + " -> ");
                w.WriteLine(text);
            }
        }

        public static void Log(Exception e)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\Log\\";
            string filename = "WebAppErrorLog_" + DateTime.Now.ToShortDateString() + ".txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter w = File.AppendText(path + filename))
            {
                //w.Write(DateTime.Now.ToLongDateString() + " -> ");
                w.WriteLine(e.ToString());
            }
        }
    }
}
