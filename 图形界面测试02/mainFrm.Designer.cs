namespace 图形界面测试02
{
    partial class mainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.断开闭合ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.详细信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读取XMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读取坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开根目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.关闭XMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片位置还原ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增元件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旋转ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.改变类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存XMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为XMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.详细信息ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.断开闭合ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.改变类型ToolStripMenuItem,
            this.旋转ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 142);
            this.contextMenuStrip1.Text = "                   ";
            // 
            // 断开闭合ToolStripMenuItem
            // 
            this.断开闭合ToolStripMenuItem.Name = "断开闭合ToolStripMenuItem";
            this.断开闭合ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.断开闭合ToolStripMenuItem.Text = "断开/闭合";
            this.断开闭合ToolStripMenuItem.Click += new System.EventHandler(this.断开闭合ToolStripMenuItem_Click);
            // 
            // 详细信息ToolStripMenuItem
            // 
            this.详细信息ToolStripMenuItem.Name = "详细信息ToolStripMenuItem";
            this.详细信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.详细信息ToolStripMenuItem.Text = "详细信息";
            this.详细信息ToolStripMenuItem.Click += new System.EventHandler(this.详细信息ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.视图ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(663, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.读取XMLToolStripMenuItem,
            this.读取坐标ToolStripMenuItem,
            this.打开根目录ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.保存XMLToolStripMenuItem,
            this.另存为XMLToolStripMenuItem,
            this.关闭XMLToolStripMenuItem,
            this.toolStripMenuItem2,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 读取XMLToolStripMenuItem
            // 
            this.读取XMLToolStripMenuItem.Name = "读取XMLToolStripMenuItem";
            this.读取XMLToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.读取XMLToolStripMenuItem.Text = "读取XML";
            this.读取XMLToolStripMenuItem.Click += new System.EventHandler(this.读取XMLToolStripMenuItem_Click);
            // 
            // 读取坐标ToolStripMenuItem
            // 
            this.读取坐标ToolStripMenuItem.Name = "读取坐标ToolStripMenuItem";
            this.读取坐标ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.读取坐标ToolStripMenuItem.Text = "读取坐标";
            this.读取坐标ToolStripMenuItem.Click += new System.EventHandler(this.读取坐标ToolStripMenuItem_Click);
            // 
            // 打开根目录ToolStripMenuItem
            // 
            this.打开根目录ToolStripMenuItem.Name = "打开根目录ToolStripMenuItem";
            this.打开根目录ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.打开根目录ToolStripMenuItem.Text = "打开根目录";
            this.打开根目录ToolStripMenuItem.Click += new System.EventHandler(this.打开根目录ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // 关闭XMLToolStripMenuItem
            // 
            this.关闭XMLToolStripMenuItem.Name = "关闭XMLToolStripMenuItem";
            this.关闭XMLToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.关闭XMLToolStripMenuItem.Text = "关闭XML";
            this.关闭XMLToolStripMenuItem.Click += new System.EventHandler(this.关闭XMLToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图片位置还原ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 图片位置还原ToolStripMenuItem
            // 
            this.图片位置还原ToolStripMenuItem.Name = "图片位置还原ToolStripMenuItem";
            this.图片位置还原ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.图片位置还原ToolStripMenuItem.Text = "图片位置还原";
            this.图片位置还原ToolStripMenuItem.Click += new System.EventHandler(this.图片位置还原ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(663, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "无信息";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增元件ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            // 
            // 新增元件ToolStripMenuItem
            // 
            this.新增元件ToolStripMenuItem.Name = "新增元件ToolStripMenuItem";
            this.新增元件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新增元件ToolStripMenuItem.Text = "新增元件";
            this.新增元件ToolStripMenuItem.Click += new System.EventHandler(this.新增元件ToolStripMenuItem_Click);
            // 
            // 旋转ToolStripMenuItem
            // 
            this.旋转ToolStripMenuItem.Name = "旋转ToolStripMenuItem";
            this.旋转ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.旋转ToolStripMenuItem.Text = "旋转";
            this.旋转ToolStripMenuItem.Click += new System.EventHandler(this.旋转ToolStripMenuItem_Click);
            // 
            // 改变类型ToolStripMenuItem
            // 
            this.改变类型ToolStripMenuItem.Name = "改变类型ToolStripMenuItem";
            this.改变类型ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.改变类型ToolStripMenuItem.Text = "改变类型";
            this.改变类型ToolStripMenuItem.Click += new System.EventHandler(this.改变类型ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重命名ToolStripMenuItem.Text = "重命名";
            this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
            // 
            // 保存XMLToolStripMenuItem
            // 
            this.保存XMLToolStripMenuItem.Name = "保存XMLToolStripMenuItem";
            this.保存XMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存XMLToolStripMenuItem.Text = "保存XML";
            this.保存XMLToolStripMenuItem.Click += new System.EventHandler(this.保存XMLToolStripMenuItem_Click);
            // 
            // 另存为XMLToolStripMenuItem
            // 
            this.另存为XMLToolStripMenuItem.Name = "另存为XMLToolStripMenuItem";
            this.另存为XMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.另存为XMLToolStripMenuItem.Text = "另存为XML";
            this.另存为XMLToolStripMenuItem.Click += new System.EventHandler(this.另存为XMLToolStripMenuItem_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(663, 437);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainFrm";
            this.Text = "图形界面测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFrm_FormClosing);
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.mainFrm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainFrm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mainFrm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainFrm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainFrm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainFrm_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mainFrm_OnMouseWheel);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem 读取XMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开根目录ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图片位置还原ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 详细信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 断开闭合ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读取坐标ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 关闭XMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增元件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 改变类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旋转ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存XMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为XMLToolStripMenuItem;
    }
}

