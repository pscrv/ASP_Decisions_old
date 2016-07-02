using ASP_Decisions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASP_Decisions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Decision> decisions = new List<Decision>();
            for (int i = 0; i < 5; i++)
            {
                Decision dec = new Decision();
                dec.CaseNumber = "T 000" + i + "/00";
                dec.Board = "3.5.01";
                dec.Title = "Decision " + i.ToString();
                decisions.Add(dec);
            }

            ViewBag.decisions = decisions;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}