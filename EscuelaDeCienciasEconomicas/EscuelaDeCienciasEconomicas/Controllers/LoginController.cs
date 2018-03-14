using EscuelaDeCienciasEconomicas.ActionFilters;
using EscuelaDeCienciasEconomicas.DAL;
using EscuelaDeCienciasEconomicas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EscuelaDeCienciasEconomicas.Controllers
{
    public class LoginController : Controller
    {
        private RaptorContext db = new RaptorContext();

        // GET: Login/
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/index
        [HttpPost]
        public ActionResult Index([Bind(Include = "username,password")] Login user)
        {
            TempData["MsgTitle"] = "";
            TempData["MsgErr"] = "";
            TempData["Msg"] = "";
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    bool userExist = db.User.Any(x => x.username == user.username);
                    if (!userExist)
                    {
                        TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                    }
                    else
                    {
                        User usr = db.User.Where(c => c.username == user.username).SingleOrDefault();
                        string hashPass = RaptorAppContext.Base64Encode(user.password);
                        if (usr == null)
                        {
                            TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                        }
                        else if (!usr.password.Equals(hashPass))
                        {
                            TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                        }
                        else
                        {
                            new RBACUser(usr.id);
                            //return RedirectToAction("../User");
                            return new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Home", action = "Index" }));
                        }
                    }
                    
                }
                user.password = "";
                return View(user);
            }
            catch
            {
                return View();
            }
        }

        // GET: Logout
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            //return RedirectToAction("../Login");
            return new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Login", action = "Index" }));
        }

        // GET: Restore
        public ActionResult Restore()
        {
            return View();
        }

        // POST: Login/Restore
        [HttpPost]
        public ActionResult Restore([Bind(Include = "email")] Login user)
        {
            TempData["MsgTitle"] = "";
            TempData["MsgErr"] = "";
            TempData["Msg"] = "";
            try
            {
                if (ModelState.IsValid)
                {
                    bool userExist = db.User.Any(x => x.email == user.email);
                    
                    if (userExist)
                    {
                        User usr = db.User.Where(c => c.email == user.email).SingleOrDefault();

                        string key = RaptorAppContext.Base64Encode(usr.id.ToString());
                        string Url = "http://localhost:1111/Login/Config?key=" + key;

                        RaptorAppContext.sendMailTest(Url);

                        TempData["MsgTitle"] = "Restablecimiento de contraseña enviado";
                        TempData["Msg"] = "Las instrucciones para reestablecer su contraseña se han enviado por correo electrónico, <br />" +
                            "Si no recibe las instrucciones en su correo asegurese de que ha introducido <br />" +
                            "el correo con que se registro y verifique su carpeta de spam. <br />" +
                            "<a href=\"" + Url + "\">restablecer</a>";
                        TempData["MsgErr"] = "";
                    }
                    return RedirectToAction("Success");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Success
        public ActionResult Success()
        {
            return View();
        }

        // GET: Config
        public ActionResult Config()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        public ActionResult Config([Bind(Include = "id,password")] User user)
        {
            TempData["MsgTitle"] = "";
            TempData["MsgErr"] = "";
            TempData["Msg"] = "";
            try
            {
                if (Request.Form["id"] != null && !Request.Form["id"].Equals(""))
                {
                    string key = (string)Request.Form["id"];
                    string newPassword = (string)Request.Form["password"];

                    int id = Int16.Parse(RaptorAppContext.Base64Decode(key));

                    bool userExist = db.User.Any(x => x.id == id);

                    if (userExist)
                    {
                        user = db.User.Find(id);
                        user.password = RaptorAppContext.Base64Encode(newPassword);

                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["MsgTitle"] = "Restablecimiento de contraseña realizado";
                        TempData["Msg"] = "Su nueva contraseña se a registrado exitosamente. <br />" +
                            "Regrese a la pantalla de inicio he ingrese sus nuevas credenciales <br />" +
                            "para iniciar sesión.";
                        return RedirectToAction("Success");
                    }
                    else
                    {
                        TempData["MsgErr"] = "Los datos para restablecer la contraseña son incorrectos.";
                    }
                } else
                {
                    TempData["MsgErr"] = "La contraseña no se puede restablecer por falta de datos.";
                }
                return Redirect("Config?key=" + Request.Form["id"]);

                //return RedirectToAction("../Config?key=" + Request.Form["id"]);
            }
            catch
            {
                return View();
            }
        }

    }
}
