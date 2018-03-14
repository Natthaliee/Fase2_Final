using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EscuelaDeCienciasEconomicas.DAL;
using EscuelaDeCienciasEconomicas.Models;
using System.Text.RegularExpressions;
using EscuelaDeCienciasEconomicas.ActionFilters;

namespace EscuelaDeCienciasEconomicas.Controllers
{
    public class UserController : Controller
    {

        int Paginacion=10;

        private RaptorContext db = new RaptorContext();

        // GET: User/
        [RBAC]
        public ActionResult Index()
        {
            try
            {
                if (Request.Form["grid-size"] != null)
                {
                    Paginacion = int.Parse(Request["grid-size"]);
                }
                else if (this.Session["Paginacion"] != null)
                {
                    Paginacion = int.Parse(this.Session["Paginacion"].ToString());
                }
                this.Session["Paginacion"] = Paginacion;
                return View(db.User.ToList());
            } catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: User/Details/
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.SOLICITUD_ERRONEA;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.REGISTRO_NO_ENCONTRADO;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: User/Create
        [RBAC]
        public ActionResult Create()
        {
            try
            {
                ViewBag.role_id = new SelectList(db.Role.OrderBy(c => c.name), "id", "name");
                return View();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,password,is_superuser,username,first_name,last_name,email,is_staff,is_active,date_joined,role_id")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.password.Length < 8 || user.password.Length > 15)
                    {
                        ModelState.AddModelError("password", " La contraseña debe tener entre 8 y 15 carácteres");
                        return View(user);
                    }
                    string regExp = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,15})$";
                    if (!Regex.IsMatch(user.password, regExp))
                    {
                        ModelState.AddModelError("password", " * No válida");
                        return View(user);
                    }
                    bool userExist = db.User.Any(x => x.username == user.username);
                    if (!userExist) {
                        string base64Pass = RaptorAppContext.Base64Encode(user.password);
                        user.password = base64Pass;
                        db.User.Add(user);
                        db.SaveChanges();
                        ViewBag.role_id = new SelectList(db.Role.OrderBy(c => c.name), "id", "name");
                        TempData["Msg"] = "Creado correctamente";
                        return RedirectToAction("Create");
                    }else
                    {
                        TempData["MsgErr"] = "El nombre de usuario ya existe";
                        return View(user);
                    }
                }
                return View(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: User/Edit/
        [RBAC]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.SOLICITUD_ERRONEA;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.REGISTRO_NO_ENCONTRADO;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                this.Session["dataEdit"] = user.username;
                if (RaptorAppContext.IsBase64String(user.password))
                {
                    user.password = RaptorAppContext.Base64Decode(user.password);
                }
                ViewBag.role_id = new SelectList(db.Role.OrderBy(c => c.name), "id", "name", user.role_id);
                return View(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // POST: User/Edit/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,password,is_superuser,username,first_name,last_name,email,is_staff,is_active,date_joined,role_id")] User user)
        {
            try {
                if (ModelState.IsValid)
                {
                    if (user.password.Length < 8 || user.password.Length > 15)
                    {
                        ModelState.AddModelError("password", " La contraseña debe tener entre 8 y 15 carácteres");
                        return View(user);
                    }
                    string regExp = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,15})$";
                    if (!Regex.IsMatch(user.password, regExp))
                    {
                        ModelState.AddModelError("password", " * No válida");
                        return View(user);
                    }
                    bool userExist = db.User.Any(x => x.username == user.username);
                    String usuario = this.Session["dataEdit"].ToString();
                    if (!userExist || usuario==user.username)
                    {
                        string base64Pass = RaptorAppContext.Base64Encode(user.password);
                        user.password = base64Pass;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.role_id = new SelectList(db.Role.OrderBy(c => c.name), "id", "name", user.role_id);
                        TempData["Msg"] = "Modificado correctamente";
                        return View(user);
                    }
                    else
                    {
                        TempData["MsgErr"] = "El nombre de usuario ya existe";
                        return View(user);
                    }
                }
                return View(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: User/Delete/
        [RBAC]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.SOLICITUD_ERRONEA;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                User user = db.User.Find(id);
                if (user == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.REGISTRO_NO_ENCONTRADO;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                user.password = RaptorAppContext.MaskString(RaptorAppContext.Base64Encode(user.password));
                return View(user);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // POST: User/Delete/
        [RBAC]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                User user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                TempData["Msg"] = "Eliminado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
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
