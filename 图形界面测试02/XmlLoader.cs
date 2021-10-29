using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace 图形界面测试02
{
    public class XmlLoader
    {
        public List<Element> ElementList = new List<Element>();
        public XmlLoader() { }

        private string filePath = "";
        public Bitmap Background = null;// 背景图片
        private XmlDocument doc;
        public void LoadXML(string path)
        {
            filePath = path;
            ReloadXML();
        }
        public void ReloadXML()
        {
            if (filePath == "") return;
            // TODO 判断文件是否被占用
            ElementList.Clear();
            doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(filePath, settings);
            doc.Load(filePath);

            try
            {
                // 获取根节点
                XmlNode xn = doc.SelectSingleNode("element");
                // 获取全部子节点
                XmlNodeList xnl = xn.ChildNodes;

                foreach (XmlNode inside_xn in xnl)
                {
                    Element e = new Element();

                    XmlElement xe = (XmlElement)inside_xn;

                    e.Id = xe.GetAttribute("ID").ToString();
                    e.Type = xe.GetAttribute("Type").ToString();

                    XmlNodeList inside_xnl = xe.ChildNodes;
                    e.Name = inside_xnl.Item(0).InnerText;
                    e.SetXY(inside_xnl.Item(1).InnerText);
                    e.SetAngle(inside_xnl.Item(2).InnerText);
                    e.SetFlip(inside_xnl.Item(3).InnerText);
                    e.SetCondition(inside_xnl.Item(4).InnerText);
                    e.ParentSwitch = inside_xnl.Item(5).InnerText;

                    e.SetPic();

                    ElementList.Add(e);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("XML文件损坏，无法读取。\n错误原因：" + error.Message);
            }
            reader.Close();
        }
        public void RewriteXML()
        {

        }
        public bool CheckLoaded()
        {
            if (ElementList.Count > 0) return true;
            return false;
        }

        // 修改某一元件的属性
        public void ModifyAttribute(Element element, string attribute, string value)
        {
            foreach (Element e in ElementList)
            {
                if (e.Id == element.Id)
                {
                    // Attribute当前只有Type或ID，此两项内容目前均不可更改
                }
            }
        }
        // 修改某一元件子结点的内容
        public void ModifyChildNode(Element element, string nodeName, string value)
        {
            foreach (Element e in ElementList)
            {
                if (e == element)
                {
                    switch (nodeName)
                    {
                        case "name":
                            e.Name = value;
                            break;
                        case "location":
                            e.SetXY(value);
                            break;
                        case "angle":
                            e.SetAngle(value);
                            break;
                        case "flip":
                            e.SetFlip(value);
                            break;
                        case "condition":
                            if (value == "True") value = "close";
                            else value = "open";
                            e.SetCondition(value);
                            // 此处开关的外形已经改变，需要重新定义Pic
                            e.SetPic();
                            break;
                        case "parent":
                            e.ParentSwitch = value;
                            break;
                        case "child":
                            e.ChildSwitch = value;
                            break;
                    }
                    // TODO 更新外部文件
                    //XmlElement xe = doc.DocumentElement;
                    //string strPath = string.Format("/element/component[@Type=\"{0}\"][@Name=\"{1}\"]",
                    //    element.Type, element.Name);
                    //XmlElement selectXe = (XmlElement)xe.SelectSingleNode(strPath);
                    //selectXe.GetElementsByTagName("condition").Item(0).InnerText = ele.Condition;
                    //doc.Save(@"ElementList.xml");

                    break;
                }
            }
        }


        // 判断某一点是否在某一元素所在区域中，如果是，则返回这个元素
        public Element GetElement(Point p)
        {
            foreach (Element e in ElementList)
            {
                if (e.GetLocation().Contains(p))
                {
                    return e;
                }
            }
            return null;
        }
        // 判断某一元件所在矩形是否包含在一个区域中，如果是，则返回选中的元件的列表
        public List<Element> GetElement(Rectangle r, bool select)
        {
            List<Element> list = new List<Element>();
            foreach (Element e in ElementList)
            {
                if (r.Contains(e.GetLocation()))
                {
                    if (select) e.IsSelected = true;
                    list.Add(e);
                }
            }
            return list;
        }
        public void SelectElement(Element element, bool condition)
        {
            foreach (Element e in ElementList)
            {
                // 此处仅通过ID来判断按钮是否相同，暂不考虑意外情况
                if (e.Id == element.Id)
                {
                    e.IsSelected = condition;
                    break;// 直接跳出，减少循环次数
                }
            }
        }
        public void CheckSingleSelect(Element element)
        {
            foreach (Element e in ElementList)
            {
                if (e.Id == element.Id) continue;
                if (e.IsSelected) e.IsSelected = false;
            }
        }
        public void CheckMultiSelects(List<Element> list)
        {
            SelectNone();
            foreach (Element e1 in list)
            {
                foreach (Element e2 in ElementList)
                {
                    if (e2.Id == e1.Id)
                    {
                        e2.IsSelected = true;
                        break;// 跳出内循环，节省次数
                    }
                }
            }
        }
        public string GetSelectedElementNames()
        {
            string temp = "";
            foreach (Element e in ElementList)
            {
                if (e.IsSelected)
                {
                    temp += e.Name + ", ";
                }
            }
            return temp;
        }
        public List<Element> GetSelectedElement()
        {
            List<Element> list = new List<Element>();
            foreach (Element e in ElementList)
            {
                if (e.IsSelected)
                    list.Add(e);
            }
            return list;
        }
        public void SelectNone()
        {
            foreach (Element e in ElementList)
            {
                e.IsSelected = false;
            }
        }
        public void SelectAll()
        {
            foreach (Element e in ElementList)
            {
                e.IsSelected = true;
            }
        }

        public bool LoadBmp(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            Background = new Bitmap(filePath);
            // 此处应该写成try catch
            return true;
        }

        // 获取当前电路图的最大面积
        public Size GetSize()
        {
            //int width = 0, height = 0;
            //// TODO 需要考虑背景图片的大小
            //// 找到最右边和最下边的元件
            //foreach (Element e in ElementList)
            //{
            //    if (e.X > width)
            //        width = e.X;
            //    if (e.Y > height)
            //        height = e.Y;
            //}
            //// 找到最左上角
            //int left = width, top = height;
            //foreach (Element e in ElementList)
            //{
            //    if (e.X < left)
            //        left = e.X;
            //    if (e.Y < top)
            //        top = e.Y;
            //}
            //// 为屏幕右下边增加额外的空间
            //width += left > 0 ? left : -left + 60;
            //height += top > 0 ? top : -top + 60;
            //// 考虑背景图片
            //if (Background != null)
            //{
            //    width = Background.Width;
            //    height = Background.Height;
            //}
            int left = int.MaxValue, top = int.MaxValue, right = 0, down = 0;
            foreach (Element e in ElementList)
            {
                if (e.X > right) right = e.X;
                if (e.X < left) left = e.X;
                if (e.Y > down) down = e.Y;
                if (e.Y < top) top = e.Y;
            }
            top -= 50;
            left -= 50;
            down += 200;
            right += 200;
            int width = right - left, height = down - top;
            if (Background != null)
            {
                if (Background.Width > width) width = Background.Width;
                if (Background.Height > height) height = Background.Height;
            }

            return new Size(width, height);
        }
        public Rectangle GetArea()
        {
            int left = int.MaxValue, top = int.MaxValue, right = 0, down = 0;
            foreach (Element e in ElementList)
            {
                if (e.X > right) right = e.X;
                if (e.X < left) left = e.X;
                if (e.Y > down) down = e.Y;
                if (e.Y < top) top = e.Y;
            }
            top -= 50;
            left -= 50;
            down += 200;
            right += 200;
            int width = right - left, height = down - top;
            if (Background != null)
            {
                if (Background.Width > width) width = Background.Width;
                if (Background.Height > height) height = Background.Height;
            }

            return new Rectangle(0, 0, width, height);
        }
        public void DrawPic(Bitmap bufferImg)
        {
            Graphics g = Graphics.FromImage(bufferImg);
            g.FillRectangle(new SolidBrush(Color.Black),GetArea());
            // 绘制背景图片
            if (Background != null)
            {
                g.DrawImage(Background, new PointF(0f, 0f));
            }
            foreach (Element e in ElementList)
            {
                // 绘制一个矩形，用于遮挡原电路图上面的内容
                g.FillRectangle(new SolidBrush(Color.Black), e.GetExactLocation());
                // 绘制元件的图片
                g.DrawImage(e.Pic, e.X, e.Y);
                // 绘制元件的文字
                // TODO 根据图片的旋转角度设置文字的显示位置等
                SolidBrush brush = new SolidBrush(Color.White);
                g.DrawString(e.Name, new Font("Times New Roman", 12, FontStyle.Bold), brush,
                    new Point(e.X + 20, e.Y + 20));
            }
            // 此处暂无更好的办法，只能循环两次
            foreach (Element e in ElementList)
            {
                // 绘制选择框
                if (e.IsSelected)
                    g.DrawRectangle(new Pen(new SolidBrush(Color.Red), 3), e.GetLocation());
            }
        }

        public void SaveXML()
        {
            if (ElementList.Count == 0) return;
            AutoXmlWriter w = new AutoXmlWriter();
            w.CreateXmlFile(ElementList, @"test.xml");
        }
        public void SaveXML(string filePath)
        {
            if (ElementList.Count == 0) return;
            AutoXmlWriter w = new AutoXmlWriter();
            w.CreateXmlFile(ElementList, filePath);
        }

        public string GetNewID()
        {
            // 这里默认ID号是按顺序排列的，每次递增1
            // 而且在最后保存文件时，也是按照顺序完成的操作
            int c = 0;
            foreach (Element e in ElementList)
            {
                if (e.IdNumber - c > 1)
                {
                    break;
                }
                else c = e.IdNumber;
            }
            return (c + 1).ToString();
        }
    }
}
