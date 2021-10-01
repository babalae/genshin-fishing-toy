using GenshinAutoFish.Utils;
using System;
using System.Drawing;

namespace GenshinAutoFish.Core
{
    public class ImageCapture
    {
        IntPtr hwnd;
        IntPtr hdc;

        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }

        public void Start(int x, int y, int w, int h)
        {
            hwnd = Native.GetDesktopWindow();
            hdc = Native.GetDC(hwnd);
            this.X = x;
            this.Y = y;
            this.W = w;
            this.H = h;
        }

        public Bitmap Capture(bool extend, out Bitmap rodWordsAreaBitmap)
        {
            Bitmap bmp = new Bitmap(W, H);
            Graphics bmpGraphic = Graphics.FromImage(bmp);
            //get handle to source graphic
            IntPtr bmpHdc = bmpGraphic.GetHdc();

            //copy it
            bool res = Native.StretchBlt(bmpHdc, 0, 0, W, H,
                hdc, X, Y, W, H, Native.CopyPixelOperation.SourceCopy);
            bmpGraphic.ReleaseHdc();

            // 非钓鱼期间不需要这个图片
            if (extend)
            {
                rodWordsAreaBitmap = new Bitmap(W, H * 2);
                Graphics bmpGraphic2 = Graphics.FromImage(rodWordsAreaBitmap);
                IntPtr bmpHdc2 = bmpGraphic2.GetHdc();
                Native.StretchBlt(bmpHdc2, 0, 0, W, H * 2,
                    hdc, X, Y + H, W, H * 2, Native.CopyPixelOperation.SourceCopy);
                bmpGraphic2.ReleaseHdc();

            }
            else
            {
                rodWordsAreaBitmap = null;
            }
            return bmp;
        }

        public void Stop()
        {
            Native.ReleaseDC(hwnd, hdc);
        }
    }
}
