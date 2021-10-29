using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace 图形界面测试02
{
    public class Element
    {
        // 坐标位置暂且不考虑浮点数，如果将来在绘图效果上有需要再作修改
        private int x = 0;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y = 0;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public string Location
        {
            get { return X.ToString() + "," + Y.ToString(); }
        }
        private int angle = 0;
        public int Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        private bool flip = false;// 水平翻转，在旋转之后翻转
        public bool Flip
        {
            get { return flip; }
            set { flip = value; }
        }
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string id = "";// ID号，用于绑定开关
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public int IdNumber
        {
            get { return Convert.ToInt32(Id); }
        }
        private string type = "Switch";
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private bool condition = false;// false指的是断开，true指的是闭合
        public bool Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public string GetCondition
        {
            get
            {
                if (Condition) return "close";
                else return "open";
            }
        }
        // 赋值语句
        public void SetXY(string number)
        {
            // 此处暂时先不判断数值是否可用，默认XML内容格式正确
            string[] xy = number.Split(',');
            X = Convert.ToInt32(xy[0]);
            Y = Convert.ToInt32(xy[1]);
        }
        public void SetAngle(string number)
        {
            Angle = Convert.ToInt32(number);
        }
        public void SetFlip(string value)
        {
            if (value == "false" || value == "False") Flip = false;
            else Flip = true;
        }
        public void SetCondition(string value)
        {
            // 此处默认XML文件格式正确，只可能有true或false
            if (value == "open") Condition = false;
            else Condition = true;
        }
        // 关联开关
        private string parentSwitch = "None";
        public string ParentSwitch
        {
            get { return parentSwitch; }
            set { parentSwitch = value; }
        }
        private string childSwitch = "None";
        public string ChildSwitch
        {
            get { return childSwitch; }
            set { childSwitch = value; }
        }
        // 相关操作
        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public Element() { }
        ~Element()
        {
            // 释放图片资源
            if (Pic != null) Pic.Dispose();
        }

        // 获取图片所在长方形
        // TODO 此处的长方形的位置及长宽需要适当的调整，方便进行点击
        public Rectangle GetLocation()
        {
            int dw = 5;
            return new Rectangle(x - dw, y - dw, Pic.Width + 2 * dw, Pic.Height + 2 * dw);
        }
        public Rectangle GetExactLocation()
        {
            return new Rectangle(x, y, Pic.Width, Pic.Height);
        }

        public Bitmap Pic = null;
        public void SetPic()
        {
            Pic = GetPic();
        }
        public Bitmap GetPic()
        {
            Bitmap temp = null;
            // TODO 考虑更多种样式的开关
            if (Type == "Switch")
            {
                if (Condition)
                    temp = new Bitmap(ResourceLoader.Switch_close);
                else
                    temp = new Bitmap(ResourceLoader.Switch_open);
            }
            else if (Type == "Knife")
            {
                if (Condition)
                    temp = new Bitmap(ResourceLoader.Knife_close);
                else
                    temp = new Bitmap(ResourceLoader.Knife_open);
            }
            // 判断：如果读取失败，则跳出
            if (temp == null)
                return null;
            // 旋转图片
            switch (Angle)
            {
                case 0: temp.RotateFlip(RotateFlipType.RotateNoneFlipNone); break;
                case 90: temp.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                case 180: temp.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                case 270: temp.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                default: break;
            }
            // 翻转图片
            if (Flip)
                temp.RotateFlip(RotateFlipType.RotateNoneFlipX);

            return temp;
        }

        public string GetInfo()
        {
            string temp = "";

            temp += "名称：" + Name + "，ID：" + Id + "\n";
            temp += "类型：" + Type + "，状态：" + Condition.ToString() + "\n";
            temp += "坐标：(" + X + ", " + Y + ")\n";
            temp += "角度：" + Angle + "，是否翻转：" + Flip.ToString() + "\n";
            temp += "关联开关：父级->" + ParentSwitch + "，子级->" + ChildSwitch + "\n";

            return temp;
        }
        public string GetSimpleInfo()
        {
            string temp = "";
            temp += Type + " ";
            temp += Name + " ";
            temp += "坐标：(" + X.ToString() + ", " + Y.ToString() + ")";
            return temp;
        }
    }
}
