using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lucykids_v2.Models.ViewModels;

namespace Lucykids_v2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult About()
        {
            //ViewData["Message"] = "My message from controller.";

            return View();
        }

        public IActionResult AboutOurClothes()
        {
            return View();
        }

        public IActionResult OrderingAndDelivery()
        {
            return View();
        }

        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            //ViewData["Message"] = "My contact message from controller.";

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel vm )
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
