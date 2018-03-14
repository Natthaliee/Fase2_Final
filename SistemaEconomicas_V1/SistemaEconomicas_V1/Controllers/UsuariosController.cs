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
    public class UsuariosController : Controller
    {
        private TestContext db = new TestContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Carrera).Include(u => u.Jornada).Include(u => u.Rol);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "CarreraNombre");
            ViewBag.JornadaId = new SelectList(db.Jornada, "JornadaId", "JornadaNombre");
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "RolNombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserPass,RolId,CarreraId,JornadaId,FechaCreacion")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "CarreraNombre", usuario.CarreraId);
            ViewBag.JornadaId = new SelectList(db.Jornada, "JornadaId", "JornadaNombre", usuario.JornadaId);
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "RolNombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "CarreraNombre", usuario.CarreraId);
            ViewBag.JornadaId = new SelectList(db.Jornada, "JornadaId", "JornadaNombre", usuario.JornadaId);
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "RolNombre", usuario.RolId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,UserPass,RolId,CarreraId,JornadaId,FechaCreacion")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "CarreraNombre", usuario.CarreraId);
            ViewBag.JornadaId = new SelectList(db.Jornada, "JornadaId", "JornadaNombre", usuario.JornadaId);
            ViewBag.RolId = new SelectList(db.Rol, "RolId", "RolNombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
