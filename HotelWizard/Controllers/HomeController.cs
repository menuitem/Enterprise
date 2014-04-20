using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelWizard.Models;

namespace HotelWizard.Controllers
{
    public class HomeController : Controller
    {
        string hotelName = ConfigSingleton.Instance.hotelName;
        string hotelAddress = ConfigSingleton.Instance.hotelAddress;
        public ActionResult Index()
        {
            ViewBag.hotelName = hotelName;
      
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Enterprise Frameworks Project - NCI MSc in WebTech";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.hotelName = hotelName;
            ViewBag.hotelAddress = hotelAddress;

            return View();
        }
    }
}