using Algorithms.Algorithms;
using Algorithms.Models;
using Microsoft.AspNetCore.Mvc;

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
}