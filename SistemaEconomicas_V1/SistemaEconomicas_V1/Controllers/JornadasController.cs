using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelBD;

namespace SistemaEconomicas_V1.Controllers
{
    public class JornadasController : Controller
    {
        private TestContext db = new TestContext();

        // GET: Jornadas
        public ActionResult Index()
        {
            //se retorna la lista de jornadas
            return View(db.Jornada.ToList());
        }

        // GET: Jornadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // GET: Jornadas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jornadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JornadaId,JornadaNombre")] Jornada jornada)
        {
            if (ModelState.IsValid)
            {
                db.Jornada.Add(jornada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jornada);
        }

        // GET: Jornadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // POST: Jornadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JornadaId,JornadaNombre")] Jornada jornada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jornada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jornada);
        }

        // GET: Jornadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // POST: Jornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jornada jornada = db.Jornada.Find(id);
            db.Jornada.Remove(jornada);
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
