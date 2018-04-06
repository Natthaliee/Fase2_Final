using ModelBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SistemaEconomicas_V1.Controllers
{
    public class LoginController : Controller
    {
        private TestContext db = new TestContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/index
        [HttpPost]
        public ActionResult Index(string UserId,string PassId)
        {
            TempData["MsgTitle"] = "";
            TempData["MsgErr"] = "";
            TempData["Msg"] = "";
            Session["Rolesss"] = "";

            var value2 = Request["PassId"];
            bool flagLogin = false;
            try
            {
                // TODO: Add insert logic here
                
            bool userExist = db.Usuario.Any(x => x.UserName == UserId);
                if (!userExist)
                {
                    TempData["MsgErr"] = "El nombre de usuario no es válido";
                }
                else
                {
                    Usuario usr = db.Usuario.Where(c => c.UserName == UserId && c.RolId == 1 && c.UserPass == PassId).SingleOrDefault();
                    if (usr == null) {
                        Session["Rolesss"] = "";
                        usr = db.Usuario.Where(c => c.UserName == UserId && c.RolId == 2 && c.UserPass == PassId).SingleOrDefault();                        
                            if (usr == null)
                            {
                                Session["Rolesss"] = "";
                                usr = db.Usuario.Where(c => c.UserName == UserId && c.RolId == 3 && c.UserPass == PassId).SingleOrDefault();                                
                                    if (usr != null)
                                    {
                                        Session["Rolesss"] = "Catedratico";
                                        flagLogin = true;
                                    }else
                                    {
                                    TempData["MsgErr"] = "La contraseña es inválida";
                                    }
                            }
                            else
                            {
                            Session["Rolesss"] = "Administrador";
                            flagLogin = true;
                        }
                    }
                    else{
                        Session["Rolesss"] = "Estudiante";
                        flagLogin = true;
                    }
                }


                PassId = "";
                if (flagLogin)
                {
                    flagLogin = false;

                    return new RedirectToRouteResult(
                                new RouteValueDictionary(
                                    new { controller = "Usuarios", action = "Index" }));
                }
                else
                {
                    flagLogin = false;
                    return View();
                }


            }
            catch(Exception e)
            {
                return View();
            }
        }
    }


}