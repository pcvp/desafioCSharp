using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
     
        private void SetSession(int X, int Y)
        {
            HttpContext.Session.SetInt32("X", X);
            HttpContext.Session.SetInt32("Y", Y);
        }

        private int SessionGetIntValue(string key)
        {
            return HttpContext.Session.GetInt32(key).GetValueOrDefault(); 
        }

        public IActionResult Index()
        {
            ViewData["Message"] = "Posição inicial";
            ViewData["TextButton"] = "Iniciar";

            return View();
        }

        [HttpPost]
        public ActionResult changePosition(string X, string Y) {
            if (int.TryParse(X, out _))
            {
                SetSession(int.Parse(X), int.Parse(Y));
            }
            else
            {
                int x = SessionGetIntValue("X");
                int y = SessionGetIntValue("Y");
                if (X.ToUpper() == "N")
                {
                    y += int.Parse(Y);
                } else if (X.ToUpper() == "S")
                {
                    y -= int.Parse(Y);
                }else if (X.ToUpper() == "L")
                {
                    x += int.Parse(Y);
                }
                else if (X.ToUpper() == "O")
                {
                    x -= int.Parse(Y);
                }
                SetSession(x, y);
            }

            ViewData["X"] = SessionGetIntValue("X");
            ViewData["Y"] = SessionGetIntValue("Y");
            ViewData["Message"] = "Posição atual é " + ViewData["X"] + "," + ViewData["Y"];
            ViewData["TextButton"] = "Alterar Posição";

            
            return View();
        }
        
     
    }
}
