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
using System.Web.Helpers;
using System.IO;
using EscuelaDeCienciasEconomicas.ActionFilters;

namespace EscuelaDeCienciasEconomicas.Controllers
{
    public class RoleController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: Roles
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
                return View(db.Role.ToList());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: Role/Details/
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.SOLICITUD_ERRONEA;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                Role role = db.Role.Find(id);
                if (role == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.REGISTRO_NO_ENCONTRADO;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                return View(role);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: Role/Create
        [RBAC]
        public ActionResult Create()
        {
            try
            {
                var m = db.Permission.OrderBy(c => c.Order).Select(c => new {
                        c.id,
                        Description = c.Description
                    })
                    .ToList();
                ViewBag.permission_id = new SelectList(m, "id", "Description");
                Role role = new Role();
                return View(role);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,isAdmin")] Role role, List<String> permissions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (permissions != null)
                    {
                        db.Role.Add(role);
                        db.SaveChanges();
                        foreach (String permission in permissions)
                        {
                            RolePermission role_permiso = new RolePermission();
                            role_permiso.role_id = role.id;
                            role_permiso.permission_id = Int32.Parse(permission);
                            db.RolePermission.Add(role_permiso);
                            db.SaveChanges();
                        }
                        var m = db.Permission.OrderBy(c => c.Order).Select(c => new {
                                        c.id,
                                        Description = c.Description
                                    }).ToList();
                        ViewBag.permission_id = new SelectList(m, "id", "Description");
                        TempData["Msg"] = "Rol creado correctamente";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        var m = db.Permission.OrderBy(c => c.Order).Select(c => new {
                            c.id,
                            Description = c.Description
                        }).ToList();
                        ViewBag.permission_id = new SelectList(m, "id", "Description");
                        TempData["MsgErr"] = "Debe seleccionar por lo menos un permiso";
                        return View(role);
                    }
                }
                return View(role);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: Role/Edit/
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
                Role role = db.Role.Find(id);
                if (role == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.REGISTRO_NO_ENCONTRADO;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                var m = db.Permission.OrderBy(c => c.Order)
                        .Where(p => !db.RolePermission.Any(sp => sp.permission_id == p.id && sp.role_id == role.id))
                        .Select(c => new {
                            c.id,
                            Description = c.Description
                        }).ToList();
                var m2 = db.Permission.OrderBy(c => c.Order)
                        .Where(p => db.RolePermission.Any(sp => sp.permission_id == p.id && sp.role_id == role.id))
                        .Select(c => new {
                            c.id,
                            Description = c.Description
                        }).ToList();
                ViewBag.permission_id = new SelectList(m, "id", "Description");
                ViewBag.permissions = new SelectList(m2, "id", "Description");
                return View(role);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // POST: Role/Edit/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,isAdmin")] Role role, List<String> permissions)
        {
            try
            {
                if (permissions != null)
                {
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    db.RolePermission.RemoveRange(db.RolePermission.Where(c => !permissions.Contains(c.permission_id.ToString()) && c.role_id == role.id));
                    db.SaveChanges();
                    foreach (string permission in permissions)
                    {
                        int idPermission = Int32.Parse(permission);
                        if (!db.RolePermission.Any(sp => sp.permission_id == idPermission && sp.role_id == role.id))
                        {
                            RolePermission rolePermission = new RolePermission();
                            rolePermission.permission_id = Int32.Parse(permission);
                            rolePermission.role_id = role.id;
                            db.RolePermission.Add(rolePermission);
                            db.SaveChanges();
                        }
                    }
                    TempData["Msg"] = "Modificado correctamente";
                }
                else
                {
                    TempData["MsgErr"] = "Debe seleccionar por lo menos un permiso";
                }
                var m = db.Permission.OrderBy(c => c.Order)
                        .Where(p => !db.RolePermission.Any(sp => sp.permission_id == p.id && sp.role_id == role.id))
                        .Select(c => new {
                            c.id,
                            Description = c.Description
                        }).ToList();
                var m2 = db.Permission.OrderBy(c => c.Order)
                        .Where(p => db.RolePermission.Any(sp => sp.permission_id == p.id && sp.role_id == role.id))
                        .Select(c => new {
                            c.id,
                            Description = c.Description
                        }).ToList();
                ViewBag.permission_id = new SelectList(m, "id", "Description");
                ViewBag.permissions = new SelectList(m2, "id", "Description");
                return View(role);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // GET: Role/Delete/
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
                Role role = db.Role.Find(id);
                if (role == null)
                {
                    TempData["MsgErr"] = RaptorAppContext.REGISTRO_NO_ENCONTRADO;
                    return View(RaptorAppContext.PAGINA_ERROR);
                }
                var m2 = db.Permission.OrderBy(c => c.Order)
                        .Where(p => db.RolePermission.Any(sp => sp.permission_id == p.id && sp.role_id == role.id))
                        .Select(c => new {
                            c.id,
                            Description = c.Description
                        }).ToList();
                ViewBag.permissions = new SelectList(m2, "id", "Description");
                return View(role);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                TempData["MsgErr"] = RaptorAppContext.ERROR_INTERNO;
                return View(RaptorAppContext.PAGINA_ERROR);
            }
        }

        // POST: Role/Delete/
        [RBAC]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Role role = db.Role.Find(id);
                db.RolePermission.RemoveRange(db.RolePermission.Where(c => c.role_id == role.id));
                db.SaveChanges();
                db.Role.Remove(role);
                db.SaveChanges();
                TempData["Msg"] = "Rol eliminado correctamente";
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
