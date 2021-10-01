using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenshinAutoFish.Utils
{
    class Native
    {

        [DllImport("GDI32.DLL", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest, int nDestWidth, int nDestHeight,
        IntPtr hdcSrc, int nXSrc, int nYSrc, int nSrcWidth, int nSrcHeight, CopyPixelOperation dwRop);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        ///<summary>
        ///Specifies a raster-operation code. These codes define how the color data for the
        ///source rectangle is to be combined with the color data for the destination
        ///rectangle to achieve the final color.
        ///</summary>
        [Flags]
        internal enum CopyPixelOperation
        {
            NoMirrorBitmap = -2147483648,

            /// <summary>dest = BLACK, 0x00000042</summary>
            Blackness = 66,

            ///<summary>dest = (NOT src) AND (NOT dest), 0x001100A6</summary>
            NotSourceErase = 1114278,

            ///<summary>dest = (NOT source), 0x00330008</summary>
            NotSourceCopy = 3342344,

            ///<summary>dest = source AND (NOT dest), 0x00440328</summary>
            SourceErase = 4457256,

            /// <summary>dest = (NOT dest), 0x00550009</summary>
            DestinationInvert = 5570569,

            /// <summary>dest = pattern XOR dest, 0x005A0049</summary>
            PatInvert = 5898313,

            ///<summary>dest = source XOR dest, 0x00660046</summary>
            SourceInvert = 6684742,

            ///<summary>dest = source AND dest, 0x008800C6</summary>
            SourceAnd = 8913094,

            /// <summary>dest = (NOT source) OR dest, 0x00BB0226</summary>
            MergePaint = 12255782,

            ///<summary>dest = (source AND pattern), 0x00C000CA</summary>
            MergeCopy = 12583114,

            ///<summary>dest = source, 0x00CC0020</summary>
            SourceCopy = 13369376,

            /// <summary>dest = source OR dest, 0x00EE0086</summary>
            SourcePaint = 15597702,

            /// <summary>dest = pattern, 0x00F00021</summary>
            PatCopy = 15728673,

            /// <summary>dest = DPSnoo, 0x00FB0A09</summary>
            PatPaint = 16452105,

            /// <summary>dest = WHITE, 0x00FF0062</summary>
            Whiteness = 16711778,

            /// <summary>
            /// Capture window as seen on screen.  This includes layered windows 
            /// such as WPF windows with AllowsTransparency="true", 0x40000000
            /// </summary>
            CaptureBlt = 1073741824,
        }


        [DllImport("user32")]
        public static extern int mouse_event(MouseEventFlag dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                          //最左坐标
            public int Top;                           //最上坐标
            public int Right;                         //最右坐标
            public int Bottom;                        //最下坐标
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        internal static extern bool HideCaret(IntPtr controlHandle);
    }
}
