using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Decisions_with_admin.Models;

namespace Decisions_with_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private DecisionDbContext db = new DecisionDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Decisions.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CaseNumber,MetaDownloaded,DecisionDate,OnlineDate,Applicant,Opponents,Appellants,Respondents,ApplicationNumber,Ipc,Title,ProcedureLanguage,Board,Keywords,Articles,Rules,Ecli,CitedCases,Distribution,Headword,Catchwords,DecisionLanguage,Link,PdfLink,TextDownloaded,HasSplitText,FactsHeader,ReasonsHeader,OrderHeader,Facts,Reasons,Order")] Decision decision)
        {
            if (ModelState.IsValid)
            {
                db.Decisions.Add(decision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(decision);
        }

        // GET: Admin/Edit/5
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

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CaseNumber,MetaDownloaded,DecisionDate,OnlineDate,Applicant,Opponents,Appellants,Respondents,ApplicationNumber,Ipc,Title,ProcedureLanguage,Board,Keywords,Articles,Rules,Ecli,CitedCases,Distribution,Headword,Catchwords,DecisionLanguage,Link,PdfLink,TextDownloaded,HasSplitText,FactsHeader,ReasonsHeader,OrderHeader,Facts,Reasons,Order")] Decision decision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(decision);
        }

        // GET: Admin/Delete/5
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

        // POST: Admin/Delete/5
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
    }
}
