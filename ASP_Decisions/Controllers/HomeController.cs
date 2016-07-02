using ASP_Decisions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ASP_Decisions.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<Decision> decisions = new List<Decision>();
            decisions = await Epo_facade.EpoSearch.SearchLatestAsync();

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