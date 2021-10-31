using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Whatso.Models;

namespace Whatso.App
{
    public class BasicUIController : Controller
    {
        private readonly ILogger<BasicUIController> _logger;

        public BasicUIController(ILogger<BasicUIController> logger)
        {
            _logger = logger;
        }

        public IActionResult Accordions()
        {
            return View();
        }

        public IActionResult Animation()
        {
            return View();
        }

        public IActionResult Cards()
        {
            return View();
        }

        public IActionResult Carousel()
        {
            return View();
        }

        public IActionResult Countdown()
        {
            return View();
        }

        public IActionResult Counter()
        {
            return View();
        }

        public IActionResult DragItems()
        {
            return View();
        }

        public IActionResult Lightbox()
        {
            return View();
        }

        public IActionResult LightboxSideOpen()
        {
            return View();
        }

        public IActionResult ListGroup()
        {
            return View();
        }

        public IActionResult MediaObject()
        {
            return View();
        }

        public IActionResult Modals()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult Scrollspy()
        {
            return View();
        }

        public IActionResult SessionTimeout()
        {
            return View();
        }

        public IActionResult SweetAlerts()
        {
            return View();
        }

        public IActionResult Tabs()
        {
            return View();
        }

        public IActionResult TourTutorial()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
