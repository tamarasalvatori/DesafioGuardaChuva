using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinamentoAutomacao
{
    public class Log
    {
        private string filePath;
        private string logContents;

        public Log(string logDirectory, string fileName)
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            this.filePath = logDirectory + fileName;
        }

        public void Write(string message)
        {
            logContents += "[" + DateTime.Now.ToString("yyyMMdd_hhmmss") + "]" + message + Environment.NewLine;
        }

        public void Save()
        {
            File.WriteAllText(this.filePath + ".txt", logContents);
        }
    }
}
