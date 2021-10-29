using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace 图形界面测试02
{
    public class TxtReader
    {
        public TxtReader() { }
        public List<Element> ElementList = new List<Element>();
        public bool LoadTxt(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            StreamReader reader = new StreamReader(filePath);

            string type = "null";
            Point offset = new Point(0, 0);
            int index = 1;
            int angle = 0;
            while (!reader.EndOfStream)
            {
                string temp = reader.ReadLine();
                // 表示读到了注释
                if (temp[0] == '#') continue;
                // 表示读到了结尾
                if (temp.Length == 0) break;
                // 表示读到了类型参数
                if (temp[0] == '[' && temp[temp.Length - 1] == ']')
                {
                    type = temp.Substring(1, temp.Length - 2);

                    string[] xy = reader.ReadLine().Split(',');
                    offset = new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));

                    angle = Convert.ToInt32(reader.ReadLine());
                    continue;
                }
                // 表示读到了坐标
                Element e = new Element();
                e.Type = type;
                e.Name = type[0] + "_" + index.ToString();
                e.Id = index.ToString();
                e.SetXY(temp);
                e.X -= offset.X; e.Y -= offset.Y;
                e.Flip = false;
                e.Angle = angle;
                e.Condition = false;
                e.ParentSwitch = "none";
                e.ChildSwitch = "none";

                ElementList.Add(e);

                index++;
            }

            reader.Close();

            return true;
        }
    }
}
