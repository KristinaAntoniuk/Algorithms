using Microsoft.AspNetCore.Html;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Encodings.Web;

namespace Algorithms.ViewComponents
{
    public class GraphicViewComponent : IDisposable
    {
        SolidBrush ellipseBrush = new SolidBrush(Color.White);
        SolidBrush nodeBrush = new SolidBrush(Color.Black);
        Pen pen = new Pen(Color.Black, 1);

        public void Dispose()
        {
            ellipseBrush.Dispose();
            nodeBrush.Dispose();
            pen.Dispose();
        }

        public IHtmlContent Invoke(int[] nodes)
        {
            Point[] points = new Point[nodes.Length];
            int x = 6;
            int y = 15;

            var result = new InlineImageResult(1000, 200);
            for (int i = 0; i < points.Length; i++)
            {
                if (i % 4 == 0)
                {
                    y += 50;
                    x = 30;
                }

                points[i].X += x;
                points[i].Y = y;

                x += 100;
            }

            result.Graphics.DrawLines(pen, points);

            for (int i = 0; i < points.Length; i++)
            {
                result.Graphics.DrawEllipse(pen, points[i].X - 6, points[i].Y - 15, 29, 29);
                result.Graphics.FillEllipse(ellipseBrush, points[i].X - 6, points[i].Y - 15, 29, 29);
                result.Graphics.DrawString(i.ToString(), new Font("Verdana", 20, FontStyle.Regular, GraphicsUnit.Pixel), nodeBrush, points[i].X, points[i].Y - 15);
            }


            return result;
        }
    }

    public sealed class InlineImageResult : IHtmlContent
    {
        private readonly Image bmp;
        public Graphics Graphics { get; private set; }

        public InlineImageResult(Int32 width, Int32 height)
        {
            //the PixelFormat argument is required if we want to have transparency
            this.bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            this.Graphics = Graphics.FromImage(this.bmp);

            //for higher quality
            this.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            this.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            this.Graphics.InterpolationMode = InterpolationMode.High;

            //make the background transparent
            this.Graphics.Clear(Color.Transparent);
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            using (this.bmp)
            using (this.Graphics)

            using (var stream = new MemoryStream())
            {
                var format = ImageFormat.Png;
                this.bmp.Save(stream, format);
                var img = String.Format("<img src=\"data:image/{0};base64,{1}\"/>", format.ToString().ToLower(), Convert.ToBase64String(stream.ToArray()));
                writer.Write(img);
            }
        }
    }
}
