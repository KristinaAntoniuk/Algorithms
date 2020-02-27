using Algorithms.Algorithms;
using Algorithms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Algorithms.Controllers
{
    public class DynamicConnectivityController : Controller
    {
        public IActionResult Index(DynamicConnectivityModel model)
        {
            if (ModelState.IsValid && model.elementsCount > 0)
            {
                QuickFind quickFind = new QuickFind(model.elementsCount);

                if (model.unions != null)
                {
                    string[] pairStrings = model.unions.Split("\r\n");
                    string[] pair = new string[2];

                    for (int i = 0; i < pairStrings.Length; i++)
                    {
                        pair = pairStrings[i].Split(" ");
                        int.TryParse(pair[0], out int a);
                        int.TryParse(pair[1], out int b);
                        quickFind.Union(a, b);
                    }
                }

                model.collection = quickFind.array;
            }
            else model.elementsCount = 10;

            return View(model);
        }


        
    }

    public sealed class InlineImageResult : ActionResult
    {
        private readonly Image bmp;

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

        public Graphics Graphics { get; private set; }

        public void ExecuteResult(ControllerContext context)
        {
            using (this.bmp)
            using (this.Graphics)

            using (var stream = new MemoryStream())
            {

                //PNG because of transparency
                var format = ImageFormat.Png;
                this.bmp.Save(stream, format);
                var img = String.Format("<img src=\"data:image/{0};base64,{1}\"/>", format.ToString().ToLower(), Convert.ToBase64String(stream.ToArray()));
                byte[] data = System.Text.Encoding.UTF8.GetBytes(img);
                context.HttpContext.Response.Body.Write(data);
            }

        }

    }
}