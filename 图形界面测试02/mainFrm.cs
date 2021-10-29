using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Xml;
using System.Drawing.Drawing2D;

namespace 图形界面测试02
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        public static bool IsFileInUse(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                FileShare.None);
                fs.Close();
            }
            catch (Exception e)
            {
                return true;
            }
            return false;
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(IsFileInUse(@"D:\桌面\文档\Introduction.docx").ToString());

            //设置显示图元控件的几个属性，双缓存相关，防止画面闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            // 打开文件对话框的默认路径
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;

            // 读取图片资源
            if (!ResourceLoader.LoadBitmap())
            {
                MessageBox.Show("图片资源缺失或正在被占用，读取失败。", "错误");
                this.Close();
            }

            // 主窗口画笔
            bufferImg = new Bitmap(this.Size.Width, this.Size.Height);
            gImg = Graphics.FromImage(bufferImg);
        }

        private bool isMovingZone = false;// 是否正在移动图纸
        private bool isControlDown = false;// Ctrl键是否按下
        private bool isSelectingZone = false;// 是否正在选择区域
        private bool isMovingElement = false;// 是否正在移动元件
        private void mainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isMovingZone = true;
                Cursor = Cursors.SizeAll;
                // TODO 显示详细的偏移坐标
                toolStripStatusLabel1.Text = "移动图纸：偏移量 = ("
                    + imgLocation.X.ToString() + ", " + imgLocation.Y.ToString() + ")";
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                isControlDown = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (xmlLoader.GetSelectedElement().Count == 0) return;
                foreach (Element element in xmlLoader.ElementList)
                {
                    if (element.IsSelected)
                    {
                        element.X--;
                    }
                }
                xmlLoader.DrawPic(bufferImg);
                isMovingElement = true;
                // 移动之后，重新绘制图片
                this.Invalidate();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (xmlLoader.GetSelectedElement().Count == 0) return;
                foreach (Element element in xmlLoader.ElementList)
                {
                    if (element.IsSelected)
                    {
                        element.X++;
                    }
                }
                xmlLoader.DrawPic(bufferImg);
                isMovingElement = true;
                // 移动之后，重新绘制图片
                this.Invalidate();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (xmlLoader.GetSelectedElement().Count == 0) return;
                foreach (Element element in xmlLoader.ElementList)
                {
                    if (element.IsSelected)
                    {
                        element.Y--;
                    }
                }
                xmlLoader.DrawPic(bufferImg);
                isMovingElement = true;
                // 移动之后，重新绘制图片
                this.Invalidate();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (xmlLoader.GetSelectedElement().Count == 0) return;
                foreach (Element element in xmlLoader.ElementList)
                {
                    if (element.IsSelected)
                    {
                        element.Y++;
                    }
                }
                xmlLoader.DrawPic(bufferImg);
                isMovingElement = true;
                // 移动之后，重新绘制图片
                this.Invalidate();
            }
        }
        private void mainFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isMovingZone = false;
                Cursor = Cursors.Default;
                toolStripStatusLabel1.Text = "无信息";
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                isControlDown = false;
                toolStripStatusLabel1.Text = "无信息";
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.L)
            {
                xmlLoader.LoadXML(@"ElementList.xml");
                // bufferImg的初始化
                bufferImg = new Bitmap(xmlLoader.GetSize().Width, xmlLoader.GetSize().Height);
                gImg = Graphics.FromImage(bufferImg);// 貌似bufferImg的改变会导致gImg丧失绘图板，所以再次赋值
                // 绘制图纸
                xmlLoader.DrawPic(bufferImg);
                this.Invalidate();
            }
        }

        // ================================================================================== 主窗体的鼠标相关事件
        Point offset = new Point(0, 0);// 记录图片的偏移量
        // 存储当前获得焦点的元件
        Element currentElement = new Element();
        List<Element> currentElements = new List<Element>();// 没啥用，只是用来刷新显示
        // 记录选择框的起始位置
        Point startPoint = new Point(-100, -100);
        Point leftTop, rightBottom = new Point(0, 0);
        private void mainFrm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!xmlLoader.CheckLoaded()) return;
            this.Focus();// 为了 MouseWheel事件
            if (e.Button == MouseButtons.Left)
            {
                // 表示将要开始移动图片
                if (isMovingZone)
                {
                    Point cur = e.Location;
                    offset = cur;
                    //offset = new Point(cur.X - imgLocation.X, cur.Y - imgLocation.Y);
                }
                // 表示将要开始拖动元件
                else if (currentElement != null)
                {
                    Point cur = e.Location;
                    offset = cur;
                }
                else
                {
                    // 绘制选择窗口，定义起点
                    startPoint = e.Location;
                    startPoint.Offset(-imgLocation.X, -imgLocation.Y);
                }
            }
        }
        private void mainFrm_MouseUp(object sender, MouseEventArgs e)
        {
            if (!xmlLoader.CheckLoaded()) return;
            if (e.Button == MouseButtons.Left)
            {
                // 如果正在移动，则直接无视其他情况
                if (isMovingZone)
                {
                    return;
                }
                else if (currentElement != null)
                {
                    if (currentElement.IsSelected)
                    {
                        if (!isMovingElement)
                            xmlLoader.SelectElement(currentElement, false);
                        // 此处应当考虑情况，如果Ctrl没有按下，则取消所有选择
                        if (!isControlDown && !isMovingElement)
                            xmlLoader.CheckSingleSelect(currentElement);
                    }
                    else
                    {
                        xmlLoader.SelectElement(currentElement, true);
                        if (!isControlDown)
                            xmlLoader.CheckSingleSelect(currentElement);
                    }
                }
                else
                {
                    if (isSelectingZone && startPoint.X >= -10)
                    {
                        isSelectingZone = false;
                        currentElements = xmlLoader.GetElement(new Rectangle(leftTop.X, leftTop.Y,
                            rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y), true);
                        // 如果没有按下Ctrl键，则视为放弃当前的所有选择
                        if (!isControlDown)
                            xmlLoader.CheckMultiSelects(currentElements);
                    }
                    else
                    {
                        // 如果没有按下Ctrl键，则视为放弃当前的所有选择
                        if (!isControlDown)
                            xmlLoader.SelectNone();
                    }
                }
                xmlLoader.DrawPic(bufferImg);
                this.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (currentElement != null)
                {
                    if (!currentElement.IsSelected)
                    {
                        xmlLoader.SelectElement(currentElement, true);
                        // 此处应当考虑情况，如果Ctrl没有按下，则取消所有选择
                        if (!isControlDown)
                            xmlLoader.CheckSingleSelect(currentElement);
                    }
                    xmlLoader.DrawPic(bufferImg);
                    this.Invalidate();
                }
                else
                {
                    // 如果没有按下Ctrl键，则视为放弃当前的所有选择
                    if (!isControlDown)
                        xmlLoader.SelectNone();
                }
            }
            isMovingElement = false;
        }
        private void mainFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!xmlLoader.CheckLoaded()) return;
            // 鼠标左键事件
            // 可能是拖动图片，或点选元件
            if (e.Button == MouseButtons.Left)
            {
                // 表明空格键按下，正在拖动图片
                if (isMovingZone)
                {
                    Point cur = e.Location;
                    //imgLocation = new Point(cur.X - offset.X, cur.Y - offset.Y);
                    imgLocation.X += (int)((cur.X - offset.X) / scaleRatio);
                    imgLocation.Y += (int)((cur.Y - offset.Y) / scaleRatio);
                    offset = cur;
                    // 移动之后，重新绘制图片
                    this.Invalidate();
                }
                // 表明鼠标没有在某一元件上方，打算使用选择框
                else if (currentElement == null)
                {
                    // 只有鼠标左键按下而且移动，才表明正在使用选择框
                    // 补充一个条件，xmlLoader必须已经读取xml完毕，否则不认为处于选择状态
                    if (xmlLoader.CheckLoaded())
                    {
                        isSelectingZone = true;
                    }
                    // 如果并没有初始化起始点，则认为是鼠标误触，不绘制选择框
                    if (startPoint.X < -10) return;
                    // 绘制选择窗口
                    Point stopPoint = e.Location;
                    stopPoint.Offset(-imgLocation.X, -imgLocation.Y);
                    Point start, stop = new Point(0, 0);
                    // 起点和终点的位置分以下四种情况
                    if (startPoint.X < stopPoint.X && startPoint.Y < stopPoint.Y)
                    {
                        start = startPoint;
                        stop = stopPoint;
                    }
                    else if (startPoint.X < stopPoint.X && startPoint.Y > stopPoint.Y)
                    {
                        start = new Point(startPoint.X, stopPoint.Y);
                        stop = new Point(stopPoint.X, startPoint.Y);
                    }
                    else if (startPoint.X > stopPoint.X && startPoint.Y < stopPoint.Y)
                    {
                        start = new Point(stopPoint.X, startPoint.Y);
                        stop = new Point(startPoint.X, stopPoint.Y);
                    }
                    else
                    {
                        start = stopPoint;
                        stop = startPoint;
                    }
                    leftTop = start;
                    rightBottom = stop;
                    Pen pen = new Pen(Color.Blue, 2);
                    pen.DashStyle = DashStyle.DashDot;

                    if (!isMovingZone)
                    {
                        xmlLoader.DrawPic(bufferImg);
                        List<Element> list = xmlLoader.GetElement(new
                            Rectangle(start.X, start.Y, stop.X - start.X, stop.Y - start.Y), false);
                        foreach (Element element in list)
                        {
                            if (!element.IsSelected)
                                gImg.DrawRectangle(new Pen(new SolidBrush(Color.LightGreen), 3), element.GetLocation());
                        }
                    }
                    gImg.DrawRectangle(pen,
                        new Rectangle(start.X, start.Y, stop.X - start.X, stop.Y - start.Y));

                    this.Invalidate();
                }
                // 表明鼠标选中了某个元件，开始拖动元件
                else if (currentElement != null)
                {
                    if (xmlLoader.GetSelectedElement().Count == 0) return;
                    Point cur = e.Location;
                    //imgLocation = new Point(cur.X - offset.X, cur.Y - offset.Y);
                    foreach (Element element in xmlLoader.ElementList)
                    {
                        if (element.IsSelected)
                        {
                            element.X += (int)((cur.X - offset.X) / scaleRatio);
                            element.Y += (int)((cur.Y - offset.Y) / scaleRatio);
                        }
                    }
                    toolStripStatusLabel1.Text = currentElement.GetSimpleInfo();
                    offset = cur;
                    // 此处开关的位置发生变化，可能会导致窗口大小的变化，故重新定义bufferImg
                    bufferImg = new Bitmap(xmlLoader.GetSize().Width, xmlLoader.GetSize().Height);
                    gImg = Graphics.FromImage(bufferImg);// 貌似bufferImg的改变会导致gImg丧失绘图板，所以再次赋值

                    xmlLoader.DrawPic(bufferImg);
                    isMovingElement = true;
                    // 移动之后，重新绘制图片
                    this.Invalidate();
                }
            }
            // 无鼠标按键按下
            // 可能是划过元件，从而获得焦点
            else if (e.Button == MouseButtons.None)
            {
                if (isMovingZone) return;
                Point currentPoint = new Point(e.Location.X - imgLocation.X, e.Location.Y - imgLocation.Y);
                currentElement = xmlLoader.GetElement(currentPoint);
                if (currentElement != null)
                {
                    // 主窗体的右键菜单
                    this.ContextMenuStrip = contextMenuStrip1;

                    xmlLoader.DrawPic(bufferImg);
                    if (!currentElement.IsSelected)
                        gImg.DrawRectangle(new Pen(new SolidBrush(Color.Green), 3), currentElement.GetLocation());

                    if (!isControlDown)
                        toolStripStatusLabel1.Text = currentElement.GetSimpleInfo();
                    this.Invalidate();
                    if (!currentElement.IsSelected)
                    {
                    }
                }
                else
                {
                    if (xmlLoader.ElementList.Count == 0)
                    {
                        toolStripStatusLabel1.Text = "未读取文件";
                    }
                    else
                    {
                        string info = xmlLoader.GetSelectedElementNames();
                        if (info == "")
                        {
                            if (!isControlDown)
                                toolStripStatusLabel1.Text = "无选中元件";
                        }
                        else
                            toolStripStatusLabel1.Text = "选中元件：" + info;
                    }
                    // 主窗体的右键菜单
                    if (xmlLoader.CheckLoaded())
                        this.ContextMenuStrip = contextMenuStrip2;
                    else
                        this.ContextMenuStrip = null;

                    xmlLoader.DrawPic(bufferImg);
                    this.Invalidate();
                }
            }
        }
        private void mainFrm_OnMouseWheel(object sender, MouseEventArgs e)
        {
            // 暂时不启用缩放功能
            return;
            if (!xmlLoader.CheckLoaded()) return;
            if (!isControlDown) return;
            if (e.Delta > 0)
            {
                scaleRatio += 0.1f;
            }
            else if (e.Delta < 0)
            {
                scaleRatio -= 0.1f;
            }
            if (scaleRatio < 0.1f) scaleRatio = 0.1f;
            if (scaleRatio > 2f) scaleRatio = 2f;
            toolStripStatusLabel1.Text = "缩放比率 = " + scaleRatio.ToString("0.0");
            this.Invalidate();
        }

        // ================================================================================== 主窗体的Paint函数
        public Bitmap bufferImg = null;// 缓存图片
        public Point imgLocation = new Point(0, 0);// 偏移坐标
        public float scaleRatio = 1f;// 缩放比率
        public Graphics gImg = null;
        private void mainFrm_Paint(object sender, PaintEventArgs e)
        {
            // 背景颜色
            Rectangle r = new Rectangle(new Point(0, 0), this.Size);
            Color color;
            //if (xmlLoader.ElementList.Count == 0) color = Color.LightGray;
            //else color = Color.White;
            color = Color.Gray;
            e.Graphics.FillRectangle(new SolidBrush(color), r);
            // 绘制电路图
            if (xmlLoader.CheckLoaded())
            {
                e.Graphics.ScaleTransform(scaleRatio, scaleRatio);
                e.Graphics.DrawImage(bufferImg, imgLocation);
            }
        }

        public LogReader logReader = new LogReader();
        public XmlLoader xmlLoader = new XmlLoader();
        public TxtReader txtReader = new TxtReader();
        private string xmlFilePath = "test.xml";
        private void 读取XMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FileName = "";
            openFileDialog1.Title = "打开坐标数据";
            openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "xml文件(*.xml)|*.xml";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.openFileDialog1.FileName;
                xmlLoader.LoadXML(FileName);
                // bufferImg的初始化
                bufferImg = new Bitmap(xmlLoader.GetSize().Width, xmlLoader.GetSize().Height);
                gImg = Graphics.FromImage(bufferImg);// 貌似bufferImg的改变会导致gImg丧失绘图板，所以再次赋值
                // 绘制图纸
                imgLocation = new Point(0, 0);
                xmlLoader.DrawPic(bufferImg);
                this.Invalidate();
                //MessageBox.Show(FileName, "XML文件路径");
                xmlFilePath = FileName;

                // 日志文件
                logReader.WriteNewLine("读入xml文件：" + FileName);
            }
            else return;

            openFileDialog1.Title = "打开背景图片";
            openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "bmp文件(*.bmp)|*.bmp";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.openFileDialog1.FileName;
                xmlLoader.LoadBmp(FileName);
                // bufferImg的初始化
                bufferImg = new Bitmap(xmlLoader.GetSize().Width, xmlLoader.GetSize().Height);
                gImg = Graphics.FromImage(bufferImg);// 貌似bufferImg的改变会导致gImg丧失绘图板，所以再次赋值
                // 重新绘制
                xmlLoader.DrawPic(bufferImg);
                this.Invalidate();
                //MessageBox.Show(FileName, "XML文件路径");
            }
        }
        private void 打开根目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;
            Process.Start("explorer.exe", path);
        }
        private void 读取坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Element> list = new List<Element>();
            string FileName = "";

            openFileDialog1.Title = "打开坐标文件";
            openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "txt文件(*.txt)|*.txt";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.openFileDialog1.FileName;
                if (txtReader.LoadTxt(FileName))
                {
                    list = txtReader.ElementList;
                }
            }
            else return;
            if (list.Count == 0) return;

            saveFileDialog1.Title = "保存坐标数据";
            saveFileDialog1.FileName = "";
            this.saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.saveFileDialog1.FileName;
                AutoXmlWriter writer = new AutoXmlWriter();
                writer.CreateXmlFile(list, FileName);
            }
            else return;

        }
        private void 关闭XMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (xmlLoader.CheckLoaded())
                {
                    xmlLoader.SaveXML();

                    // 日志文件
                    logReader.WriteNewLine("关闭文件：" + xmlFilePath);
                }
                xmlLoader.ElementList.Clear();
                if (xmlLoader.Background != null)
                {
                    xmlLoader.Background.Dispose();
                    xmlLoader.Background = null;
                }
                this.Invalidate();
            }
            catch (Exception err) { }
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void 图片位置还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgLocation = new Point(0, 0);
            this.Invalidate();
        }
        private void 保存XMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!xmlLoader.CheckLoaded()) return;
            xmlLoader.SaveXML(xmlFilePath);
        }
        private void 另存为XMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!xmlLoader.CheckLoaded()) return;
            string FileName = "";
            saveFileDialog1.Title = "保存坐标数据";
            saveFileDialog1.FileName = "test.xml";
            this.saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.saveFileDialog1.FileName;
                xmlLoader.SaveXML(FileName);
            }
            else return;
        }

        // 窗口关闭，释放所有内存
        private void mainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 测试XML文件的保存效果
            try
            {
                // 只有在xml文件没有关闭时才保存
                if (xmlLoader.CheckLoaded())
                {
                    xmlLoader.SaveXML();

                    // 日志文件
                    logReader.WriteNewLine("关闭文件：" + xmlFilePath);
                }
                logReader.CloseFile();
            }
            catch (Exception err)
            {
                MessageBox.Show("保存XML文件失败。\n错误原因：" + err.Message);
                return;
            }
            //MessageBox.Show("Xml文件保存成功。");
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentElement == null)
            {
                MessageBox.Show("未选中元件。", "错误");
            }
            else
            {
                List<Element> list = xmlLoader.GetSelectedElement();
                foreach (Element ele in list)
                {
                    MessageBox.Show(ele.GetInfo(), "信息");
                }
            }
        }
        private void 断开闭合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentElement == null)
            {
                MessageBox.Show("未选中元件。", "错误");
            }
            else
            {
                List<Element> list = xmlLoader.GetSelectedElement();
                foreach (Element ele in list)
                {
                    xmlLoader.ModifyChildNode(ele, "condition", (!ele.Condition).ToString());

                    // 日志文件
                    logReader.WriteNewLine("更改" + ele.Type + " " + ele.Name + "状态为：" + ele.GetCondition);
                }
                xmlLoader.DrawPic(bufferImg);
                this.Invalidate();
            }
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < xmlLoader.ElementList.Count; i++)
                {
                    if (xmlLoader.ElementList[i].IsSelected)
                    {
                        Element ele = xmlLoader.ElementList[i];
                        xmlLoader.ElementList.RemoveAt(i);

                        // 日志文件
                        logReader.WriteNewLine("删除" + ele.Type + "：" + ele.Name);

                        i--;
                    }
                }
                // 此处开关的数量发生变化，可能会导致窗口大小的变化，故重新定义bufferImg
                bufferImg = new Bitmap(xmlLoader.GetSize().Width, xmlLoader.GetSize().Height);
                gImg = Graphics.FromImage(bufferImg);// 貌似bufferImg的改变会导致gImg丧失绘图板，所以再次赋值

                xmlLoader.DrawPic(bufferImg);
                this.Invalidate();
            }
        }
        private void 新增元件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element element = new Element();
            int id = Convert.ToInt32(xmlLoader.GetNewID());
            Point cur = this.PointToClient(MousePosition);
            cur.Offset(new Point(-imgLocation.X, -imgLocation.Y));
            element.X = cur.X;
            element.Y = cur.Y;
            element.Type = "Switch";
            element.Name = element.Type[0] + "_" + id.ToString();
            element.Id = id.ToString();
            element.Angle = 0;
            element.Flip = false;
            element.ParentSwitch = "none";
            element.ChildSwitch = "none";
            element.Condition = false;
            element.SetPic();

            // 日志文件
            logReader.WriteNewLine("新增" + element.Type + ": " + element.Name);

            for (int i = 0; i < xmlLoader.ElementList.Count; i++)
            {
                if (id > xmlLoader.ElementList[i].IdNumber)
                {
                    xmlLoader.ElementList.Insert(i + 1, element);
                    break;
                }
            }

            xmlLoader.DrawPic(bufferImg);
            this.Invalidate();
        }
        private void 旋转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Element element in xmlLoader.ElementList)
            {
                if (element.Id == currentElement.Id)
                {
                    int r = element.Angle;
                    r += 90;
                    if (r >= 360) r -= 360;
                    element.Angle = r;
                    element.SetPic();
                    xmlLoader.DrawPic(bufferImg);
                    this.Invalidate();
                    break;
                }
            }
        }
        private void 改变类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Element element in xmlLoader.ElementList)
            {
                if (element.Id == currentElement.Id)
                {
                    string type = element.Type;
                    if (type == "Switch") element.Type = "Knife";
                    else if (type == "Knife") element.Type = "Switch";
                    element.SetPic();
                    xmlLoader.DrawPic(bufferImg);
                    this.Invalidate();
                    break;
                }
            }
        }
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Element element in xmlLoader.ElementList)
            {
                if (element.Id == currentElement.Id)
                {
                    string name = Microsoft.VisualBasic.Interaction.InputBox("请输入新名称", "输入框", element.Name);

                    // 表示刚刚点击的是取消或者什么都没有填写
                    if (name != "")
                    {
                        // 日志文件
                        logReader.WriteNewLine("重命名" + element.Type + ": " + element.Name + " -> " + name);

                        element.Name = name;
                    }

                    xmlLoader.DrawPic(bufferImg);
                    this.Invalidate();
                    break;
                }
            }
        }
    }
}
