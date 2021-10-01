using GenshinAutoFish.Core;
using GenshinAutoFish.Forms.Hotkey;
using GenshinAutoFish.Utils;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinAutoFish
{
    public partial class FormMain : Form
    {
        private ImageCapture capture = new ImageCapture();

        private FormMotionArea strainBarArea;

        private YuanShenWindow window = new YuanShenWindow();

        private bool isFishing = false;

        private string thisVersion;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 标题加上版本号
            string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (currentVersion.Length > 3)
            {
                thisVersion = currentVersion.Substring(0, 3);
                currentVersion = " v" + thisVersion;
            }
            this.Text += currentVersion;
            GAHelper.Instance.RequestPageView($"/main/{thisVersion}", $"进入{thisVersion}版本主界面");

            InitAreaWindows();

            try
            {
                RegisterHotKey("F11");
            }
            catch (Exception ex)
            {
                PrintMsg(ex.Message);
                MessageBox.Show(ex.Message, "热键注册失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void InitAreaWindows()
        {
            // 读取配置信息
            nudFrameRate.Value = Properties.Settings.Default.FrameRate;
            chkAutoPullUp.Checked = Properties.Settings.Default.AutoPullUpChecked;
            chkTopMost.Checked = Properties.Settings.Default.TopMostChecked;
            chkDisplayDetectForm.Checked = Properties.Settings.Default.DisplayDetectChecked;
            chkAlwaysHideArea.Checked = Properties.Settings.Default.AlwaysHideAreaChecked;

            // 主窗口位置
            if (Properties.Settings.Default.FormMainLocation.X >= 0 && Properties.Settings.Default.FormMainLocation.Y >= 0)
            {
                Location = Properties.Settings.Default.FormMainLocation;
            }

            // 钓鱼条捕获窗口
            strainBarArea = new FormMotionArea(FormMotionArea.DetectType.StrainBar);
            strainBarArea.FormMainInstance = this;
            strainBarArea.Show();
            if (chkAlwaysHideArea.Checked)
            {
                strainBarArea.Hide();
            }
            if (Properties.Settings.Default.StrainBarAreaLocation.X >= 0 && Properties.Settings.Default.StrainBarAreaLocation.Y >= 0)
            {
                strainBarArea.Location = Properties.Settings.Default.StrainBarAreaLocation;
            }
            strainBarArea.Size = Properties.Settings.Default.StrainBarAreaSize;


            // 收杆按钮捕获窗口
            //pullUpRodArea = new FormMotionArea(FormMotionArea.DetectType.PullUpRod);
            //pullUpRodArea.FormMainInstance = this;
            //pullUpRodArea.Show();

            //if (Properties.Settings.Default.PullUpRodAreaLocation.X != 0 && Properties.Settings.Default.PullUpRodAreaLocation.Y != 0)
            //{
            //    pullUpRodArea.Location = Properties.Settings.Default.PullUpRodAreaLocation;
            //}
            //pullUpRodArea.Size = Properties.Settings.Default.PullUpRodAreaSize;
        }

        private void TestScreen()
        {
            PrintMsg($"获取真实设置的桌面分辨率大小 {PrimaryScreen.DESKTOP.Width} x {PrimaryScreen.DESKTOP.Height}");
            PrintMsg($"获取屏幕分辨率当前物理大小 {PrimaryScreen.WorkingArea.Width} x {PrimaryScreen.WorkingArea.Height}");

            PrintMsg($"获取缩放百分比 {PrimaryScreen.ScaleX} x {PrimaryScreen.ScaleY}");
            PrintMsg($"当前系统DPI {PrimaryScreen.DpiX} x {PrimaryScreen.DpiY}");

            if (!window.GetHWND())
            {
                PrintMsg("未找到原神进程，请先启动原神！");
            }
            Rectangle rc = window.GetSize();
            PrintMsg($"原神窗口 {rc.Width} x {rc.Height}");
            PrintMsg($"原神窗口 {rc.Width * PrimaryScreen.ScaleX} x {rc.Height * PrimaryScreen.ScaleY}");
            //strainBarArea.Location = new System.Drawing.Point((int)((rc.X + 300) * PrimaryScreen.ScaleX), (int)(rc.Y * PrimaryScreen.ScaleY + 16));
        }


        List<Rect> rects;
        Rect cur, left, right;
        uint prevMouseEvent = 0x0;
        int noRectsCount = 0;
        bool findFishBoxTips = false;
        bool isFishingProcess = false;
        Rect baseHookTips = Rect.Empty;
        int hookTipsExitCount = 0; // 钓鱼提示持续时间
        int notFishingAfterHookCount = 0; // 提竿后没有钓鱼的时间
        private void timerCapture_Tick(object sender, EventArgs e)
        {
            Bitmap rodWordsAreaBitmap;
            Bitmap pic = capture.Capture(!isFishingProcess && chkAutoPullUp.Checked, out rodWordsAreaBitmap);
            pictureBox1.Image = ImageRecognition.GetRect(pic, out rects, chkDisplayDetectForm.Checked);

            // 提竿判断
            if (rodWordsAreaBitmap != null)
            {
                Rect currHookTips = ImageRecognition.MatchWords(rodWordsAreaBitmap, capture, chkDisplayDetectForm.Checked);
                if (currHookTips != Rect.Empty)
                {
                    if (baseHookTips == Rect.Empty)
                    {
                        baseHookTips = currHookTips;
                    }
                    else
                    {
                        if (Math.Abs(baseHookTips.X - currHookTips.X) < 10
                            && Math.Abs(baseHookTips.Y - currHookTips.Y) < 10
                            && Math.Abs(baseHookTips.Width - currHookTips.Width) < 10
                            && Math.Abs(baseHookTips.Height - currHookTips.Height) < 10)
                        {
                            hookTipsExitCount++;
                        }
                        else
                        {
                            hookTipsExitCount = 0;
                            baseHookTips = currHookTips;
                        }
                        if (hookTipsExitCount >= decimal.ToDouble(nudFrameRate.Value) / 2)
                        {
                            window.MouseClick(0, 0);
                            if (chkAutoPullUp.Checked)
                            {
                                PrintMsg(@"┌------------------------┐");

                            }
                            PrintMsg("  自动提竿");
                            isFishingProcess = true;
                            hookTipsExitCount = 0;
                            baseHookTips = Rect.Empty;
                        }
                    }
                }
            }

            if (rects != null && rects.Count > 0)
            {
                if (rects.Count >= 2 && prevMouseEvent == 0x0 && !findFishBoxTips)
                {
                    findFishBoxTips = true;
                    PrintMsg("  识别到钓鱼框，自动拉扯中...");
                }
                // 超过3个矩形是异常情况，取高度最高的三个矩形进行识别
                if (rects.Count > 3)
                {
                    rects.Sort((a, b) => b.Height.CompareTo(a.Height));
                    rects.RemoveRange(3, rects.Count - 3);
                }

                //Console.WriteLine($"识别到{rects.Count} 个矩形");
                if (rects.Count == 2)
                {
                    if (rects[0].Width < rects[1].Width)
                    {
                        cur = rects[0];
                        left = rects[1];
                    }
                    else
                    {
                        cur = rects[1];
                        left = rects[0];
                    }

                    if (cur.X < left.X)
                    {
                        if (prevMouseEvent != YuanShenWindow.WM_LBUTTONDOWN)
                        {
                            window.MouseLeftButtonDown();
                            prevMouseEvent = YuanShenWindow.WM_LBUTTONDOWN;
                            Console.WriteLine("进度不到 左键按下");
                        }
                    }
                    else
                    {
                        if (prevMouseEvent == YuanShenWindow.WM_LBUTTONDOWN)
                        {
                            window.MouseLeftButtonUp();
                            prevMouseEvent = YuanShenWindow.WM_LBUTTONUP;
                            Console.WriteLine("进度超出 左键松开");
                        }
                    }
                }
                else if (rects.Count == 3)
                {
                    rects.Sort((a, b) => a.X.CompareTo(b.X));
                    left = rects[0];
                    cur = rects[1];
                    right = rects[2];

                    if (right.X + right.Width - (cur.X + cur.Width) <= cur.X - left.X)
                    {
                        if (prevMouseEvent == YuanShenWindow.WM_LBUTTONDOWN)
                        {
                            window.MouseLeftButtonUp();
                            prevMouseEvent = YuanShenWindow.WM_LBUTTONUP;
                            Console.WriteLine("进入框内中间 左键松开");
                        }
                    }
                    else
                    {
                        if (prevMouseEvent != YuanShenWindow.WM_LBUTTONDOWN)
                        {
                            window.MouseLeftButtonDown();
                            prevMouseEvent = YuanShenWindow.WM_LBUTTONDOWN;
                            Console.WriteLine("未到框内中间 左键按下");
                        }
                    }
                }
            }
            else
            {
                noRectsCount++;
                // 2s 没有矩形视为已经完成钓鱼
                if (noRectsCount >= nudFrameRate.Value * 2 && prevMouseEvent != 0x0)
                {
                    findFishBoxTips = false;
                    isFishingProcess = false;
                    prevMouseEvent = 0x0;
                    PrintMsg("  钓鱼结束");
                    if (chkAutoPullUp.Checked)
                    {
                        PrintMsg(@"└------------------------┘");
                    }
                }
            }

            // 提竿后没有钓鱼的情况
            if (isFishingProcess && !findFishBoxTips)
            {
                notFishingAfterHookCount++;
                if (notFishingAfterHookCount >= decimal.ToDouble(nudFrameRate.Value) * 2)
                {
                    isFishingProcess = false;
                    notFishingAfterHookCount = 0;
                    PrintMsg("  X 提竿后没有钓鱼，重置!");
                    if (chkAutoPullUp.Checked)
                    {
                        PrintMsg(@"└------------------------┘");
                    }
                }
            } 
            else
            {
                notFishingAfterHookCount = 0;
            }

        }

        private void PrintMsg(string msg)
        {
            msg = DateTime.Now + " " + msg;
            Console.WriteLine(msg);
            rtbConsole.Text += msg + Environment.NewLine;
            this.rtbConsole.SelectionStart = rtbConsole.TextLength;
            this.rtbConsole.ScrollToCaret();
        }

        private void StartFishing()
        {
            nudFrameRate.Enabled = false;

            if (!window.GetHWND())
            {
                PrintMsg("未找到原神进程，请先启动原神！");
            }

            int x = (int)Math.Ceiling(strainBarArea.Location.X * PrimaryScreen.ScaleX);
            int y = (int)Math.Ceiling(strainBarArea.Location.Y * PrimaryScreen.ScaleY);
            int w = (int)Math.Ceiling(strainBarArea.Width * PrimaryScreen.ScaleX);
            int h = (int)Math.Ceiling(strainBarArea.Height * PrimaryScreen.ScaleY);
            strainBarArea.DragEnabled = false;
            strainBarArea.Hide();
            capture.Start(x, y, w, h);
            timerCapture.Interval = Convert.ToInt32(1000 / nudFrameRate.Value);
            timerCapture.Start();

        }

        private void StopFishing()
        {
            findFishBoxTips = false;
            isFishingProcess = false;

            nudFrameRate.Enabled = true;

            strainBarArea.DragEnabled = true;
            if (!chkAlwaysHideArea.Checked)
            {
                strainBarArea.Show();
            }
            timerCapture.Stop();
            capture.Stop();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (!isFishing)
            {
                StartFishing();
                isFishing = true;
                btnSwitch.Text = "关闭自动钓鱼(F11)";
            }
            else
            {
                StopFishing();
                isFishing = false;
                btnSwitch.Text = "启动自动钓鱼(F11)";
            }
        }

        private void chkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chkTopMost.Checked;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.FrameRate = nudFrameRate.Value;
            Properties.Settings.Default.AutoPullUpChecked = chkAutoPullUp.Checked;
            Properties.Settings.Default.TopMostChecked = chkTopMost.Checked;
            Properties.Settings.Default.DisplayDetectChecked = chkDisplayDetectForm.Checked;
            Properties.Settings.Default.AlwaysHideAreaChecked = chkAlwaysHideArea.Checked;

            Properties.Settings.Default.FormMainLocation = Location;
            Properties.Settings.Default.StrainBarAreaLocation = strainBarArea.Location;
            Properties.Settings.Default.StrainBarAreaSize = strainBarArea.Size;
            //Properties.Settings.Default.PullUpRodAreaLocation = pullUpRodArea.Location;
            //Properties.Settings.Default.PullUpRodAreaSize = pullUpRodArea.Size;
            Properties.Settings.Default.Save();
            strainBarArea.Close();
            //pullUpRodArea.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/babalae/genshin-fishing-toy");
        }

        private void btnResetArea_Click(object sender, EventArgs e)
        {
            if (isFishing)
            {
                MessageBox.Show("钓鱼期间不能重置识别框位置", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (strainBarArea == null || strainBarArea.IsDisposed)
            {
                strainBarArea = new FormMotionArea(FormMotionArea.DetectType.StrainBar);
            }
            if (!chkAlwaysHideArea.Checked)
            {
                strainBarArea.Show();
            }
            Properties.Settings.Default.StrainBarAreaLocation = new System.Drawing.Point(0, 0);
            strainBarArea.Location = Properties.Settings.Default.StrainBarAreaLocation;
        }

        private void chkAlwaysHideArea_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlwaysHideArea.Checked)
            {
                strainBarArea?.Hide();
            }
            else
            {
                if (!isFishing)
                {
                    strainBarArea?.Show();
                }
            }
        }

        #region Hotkey
        private Hotkey hotkey;
        private HotkeyHook hotkeyHook;

        public void RegisterHotKey(string hotkeyStr)
        {
            if (string.IsNullOrEmpty(hotkeyStr))
            {
                UnregisterHotKey();
                return;
            }

            hotkey = new Hotkey(hotkeyStr);

            if (hotkeyHook != null)
            {
                hotkeyHook.Dispose();
            }
            hotkeyHook = new HotkeyHook();
            // register the event that is fired after the key press.
            hotkeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(btnSwitch_Click);
            hotkeyHook.RegisterHotKey(hotkey.ModifierKey, hotkey.Key);
        }

        public void UnregisterHotKey()
        {
            if (hotkeyHook != null)
            {
                hotkeyHook.UnregisterHotKey();
                hotkeyHook.Dispose();
            }
        }
        #endregion
    }
}
