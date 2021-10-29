using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 图形界面测试02
{
    public class ResourceLoader
    {
        public static Bitmap Switch_open;
        public static Bitmap Switch_close;
        public static Bitmap Knife_open;
        public static Bitmap Knife_close;
        public static bool LoadBitmap()
        {
            try
            {
                Switch_open = new Bitmap(@"Resources/S.png");
                Switch_close = new Bitmap(@"Resources/S_2.png");
                Knife_open = new Bitmap(@"Resources/K.png");
                Knife_close = new Bitmap(@"Resources/K_2.png");
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public static void Dispose()
        {
            try
            {
                Switch_open.Dispose();
                Switch_close.Dispose();
                Knife_open.Dispose();
                Knife_close.Dispose();
            }
            catch(Exception e)
            {
                // 进入此处的原因是图片文件缺失，Bitmap当前为null，无法调用Dispose()
            }
        }
    }
}
