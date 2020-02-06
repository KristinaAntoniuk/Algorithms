using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Algorithms.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Algorithms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DynamicConnectivity()
        {
            return View();
        }

        public ActionResult Image()
        {
            var result = new InlineImageResult(200, 50);
            result.Graphics.DrawString("Hello, World!", new Font("Verdana", 20, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(Color.Blue), 0, 0);
            return result;
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

        public override void ExecuteResult(ActionContext context)
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
