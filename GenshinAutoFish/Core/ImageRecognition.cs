using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenshinAutoFish.Core
{
    class ImageRecognition
    {
        public static Bitmap GetRect(Bitmap img, out List<Rect> rects, bool enableImShow)
        {
            using (Mat mask = new Mat())
            using (Mat rgbMat = new Mat())
            using (Mat src = img.ToMat())
            {
                Cv2.CvtColor(src, rgbMat, ColorConversionCodes.BGR2RGB);
                var lowPurple = new Scalar(255, 255, 192);
                var highPurple = new Scalar(255, 255, 192);
                Cv2.InRange(rgbMat, lowPurple, highPurple, mask);
                Cv2.Threshold(mask, mask, 0, 255, ThresholdTypes.Binary); //二值化

                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple, null);
                if (contours.Length > 0)
                {
                    var imgTar = src.Clone();
                    var boxes = contours.Select(Cv2.BoundingRect).Where(w => w.Height >= 10);
                    rects = boxes.ToList();
                    foreach (Rect rect in rects)
                    {
                        Cv2.Rectangle(imgTar, new OpenCvSharp.Point(rect.X, rect.Y), new OpenCvSharp.Point(rect.X + rect.Width, rect.Y + rect.Height), Scalar.Red, 2);
                    }
                    if (enableImShow)
                    {
                        Cv2.ImShow("钓鱼条识别窗口", imgTar);
                    }
                    return imgTar.ToBitmap();
                }
                else
                {
                    rects = null;
                    return src.ToBitmap();
                }
            }

        }

        public static Rect MatchWords(Bitmap img, ImageCapture capture, bool enableImShow)
        {
            using (Mat src = img.ToMat())
            using (Mat result = new Mat())
            {
                Cv2.CvtColor(src, src, ColorConversionCodes.BGR2RGB);
                var lowPurple = new Scalar(253, 253, 253);
                var highPurple = new Scalar(255, 255, 255);
                Cv2.InRange(src, lowPurple, highPurple, src);
                Cv2.Threshold(src, src, 0, 255, ThresholdTypes.Binary);
                var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(20, 20), new OpenCvSharp.Point(-1, -1));
                Cv2.Dilate(src, src, kernel); //膨胀

                Scalar color = new Scalar(0, 0, 255);
                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(src, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple, null);
                if (contours.Length > 0)
                {
                    var imgTar = img.ToMat();
                    var boxes = contours.Select(Cv2.BoundingRect);
                    List<Rect> rects = boxes.ToList();
                    if (rects.Count > 1)
                    {
                        rects.Sort((a, b) => b.Height.CompareTo(a.Height));
                    }
                    if (rects[0].Height < src.Height
                        && rects[0].Width * 1.0 / rects[0].Height >= 3 // 长宽比判断
                        && capture.W > rects[0].Width * 3  // 文字范围3倍小于钓鱼条范围的
                        && capture.W * 1.0 / 2 > rects[0].X  // 中轴线判断左
                        && capture.W * 1.0 / 2 < rects[0].X + rects[0].Width)  // 中轴线判断右
                    {
                        foreach (Rect rect in rects)
                        {
                            Cv2.Rectangle(imgTar, new OpenCvSharp.Point(rect.X, rect.Y), new OpenCvSharp.Point(rect.X + rect.Width, rect.Y + rect.Height), Scalar.Red, 2);
                        }
                        if (enableImShow)
                        {
                            Cv2.ImShow("自动提杆识别窗口", imgTar);
                        }
                        return rects[0];
                    }
                }
            }
            return Rect.Empty;
        }

    }
}
