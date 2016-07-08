using ASP_Decisions.Epo_facade;
using ASP_Decisions.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASP_Decisions.Controllers
{
    public class DecisionController : Controller
    {
        private DecisionDbContext db = new DecisionDbContext();

        // GET: Decisions
        public ActionResult Index()
        {
            return View(db.Decisions.ToList());
        }

        // GET: Decisions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decision decision = db.Decisions.Find(id);
            if (decision == null)
            {
                return HttpNotFound();
            }

            List<Decision> citedDecisions = new List<Decision>();
            if (decision.CitedCases != "")
                citedDecisions = await _getCited(decision);
            ViewBag.CitedDecisions = citedDecisions;

            if (!decision.TextDownloaded)
            {
                EpoSearch.GetDecisionText(decision);
                db.SaveChanges();
            }

            ViewBag.Facts = decision.Facts.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            ViewBag.Reasons = decision.Reasons.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            ViewBag.Order = decision.Order.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            return View(decision);
        }

        // GET: Decisions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Decisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CaseNumber,MetaDownloaded,DecisionDate,OnlineDate,Applicant,Opponents,Appellants,Respondents,ApplicationNumber,Ipc,Title,ProcedureLanguage,Board,Keywords,Articles,Rules,Ecli,CitedCases,Distribution,Headword,Catchwords,DecisionLanguage,Link,PdfLink,TextDownloaded,HasSplitText,FactHeader,ReasonsHeader,OrderHeader,Facts,Reasons,Order")] Decision decision)
        {
            if (ModelState.IsValid)
            {
                db.Decisions.Add(decision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(decision);
        }

        // GET: Decisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decision decision = db.Decisions.Find(id);
            if (decision == null)
            {
                return HttpNotFound();
            }
            return View(decision);
        }

        // POST: Decisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CaseNumber,MetaDownloaded,DecisionDate,OnlineDate,Applicant,Opponents,Appellants,Respondents,ApplicationNumber,Ipc,Title,ProcedureLanguage,Board,Keywords,Articles,Rules,Ecli,CitedCases,Distribution,Headword,Catchwords,DecisionLanguage,Link,PdfLink,TextDownloaded,HasSplitText,FactHeader,ReasonsHeader,OrderHeader,Facts,Reasons,Order")] Decision decision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(decision);
        }

        // GET: Decisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decision decision = db.Decisions.Find(id);
            if (decision == null)
            {
                return HttpNotFound();
            }
            return View(decision);
        }

        // POST: Decisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Decision decision = db.Decisions.Find(id);
            db.Decisions.Remove(decision);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region helper methods
        private async Task<List<Decision>> _getCited(Decision decision)
        {
            List<Decision> citedDecisions = new List<Decision>();

            foreach (string cited in decision.CitedCases.Split(','))
            {
                string ctd = cited.Trim();
                Decision inDB = db.Decisions.FirstOrDefault(
                    dec => dec.CaseNumber == ctd
                            && dec.DecisionLanguage == dec.ProcedureLanguage);

                if (inDB != null)
                {
                    citedDecisions.Add(inDB);
                }
                else
                {
                    // we don't have this decision yet
                    // or decision has a typo?
                    // try to find it on the EPO site; if we do,
                    // add it to the DB

                    List<Decision> fromEPO = await Epo_facade.EpoSearch.SearchCaseNumberAsync(ctd);
                    if (fromEPO == null || fromEPO.Count == 0)
                    {
                        citedDecisions.Add(new Decision() { CaseNumber = ctd, Title = "Not found" });
                        continue;
                    }
                    
                    bool added = false;
                    foreach (Decision dec in fromEPO)
                    {
                        db.AddOrUpdate(dec);
                        if (dec.DecisionLanguage == dec.ProcedureLanguage && !added)
                        {
                            citedDecisions.Add(dec);
                            added = true;
                        }
                    }
                    if (!added)
                        citedDecisions.Add(fromEPO.First());
                }
            }

            return citedDecisions;
        }
        #endregion

    }
}
