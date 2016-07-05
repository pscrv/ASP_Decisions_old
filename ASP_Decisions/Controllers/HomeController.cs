using ASP_Decisions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ASP_Decisions.Controllers
{
    public class HomeController : Controller
    {
        private DecisionDbContext _dbContext = new DecisionDbContext();

        public async Task<ActionResult> Index()
        {
            await Epo_facade.DailyUpdate.TryUpdate();

            List<Decision> decisions = _dbContext.Decisions
                .OrderByDescending(d => d.OnlineDate)
                .Take(10)
                .ToList();

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