using System;
using System.Collections.Generic;
using System.IO;

namespace 图形界面测试02
{
    public class LogReader
    {
        //StreamReader reader;
        StreamWriter writer;
        FileStream fs;
        public LogReader()
        {
            if (!File.Exists(@"log.txt"))
            {
                fs = File.Create(@"log.txt");
            }
            else
                fs = new FileStream(@"log.txt", FileMode.Append);
            writer = new StreamWriter(fs);
            //reader = new StreamReader(@"log.txt");
            writer.WriteLine(DateTime.Now.ToLongDateString());
        }

        public void WriteNewLine(string str)
        {
            writer.WriteLine(DateTime.Now.ToLongTimeString() + " -> " + str);
            writer.Flush();
        }

        public void CloseFile()
        {
            //reader.Close();
            writer.Close();
            fs.Close();
        }
    }
}
