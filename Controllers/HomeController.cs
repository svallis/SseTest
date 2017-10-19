using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SseTest.Models;

namespace SseTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("/sse-test")]
        public IActionResult SseTest()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            var total = 5;
            for (var i = 1; i <= total; ++i)
            {
                var message = $"{i} of {total}\r\r";
                var bytes = Encoding.ASCII.GetBytes(message);
                Response.Body.Write(bytes, 0, bytes.Length);
                Response.Body.Flush();
                System.Threading.Thread.Sleep(1000);
            }

            return StatusCode(200);
        }
    }
}
