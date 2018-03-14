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
                    TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                }
                else
                {
                    Usuario usr = db.Usuario.Where(c => c.UserName == UserId && c.RolId == 1).SingleOrDefault();                    
                    if (usr == null) {
                        Session["Rolesss"] = "";
                        usr = db.Usuario.Where(c => c.UserName == UserId && c.RolId == 2).SingleOrDefault();                        
                            if (usr == null)
                            {
                                Session["Rolesss"] = "";
                                usr = db.Usuario.Where(c => c.UserName == UserId && c.RolId == 3).SingleOrDefault();                                
                                    if (usr == null)
                                    {
                                    TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                                    }
                                    else
                                    {
                                        Session["Rolesss"] = "Catedratico";
                                         flagLogin = true;
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
                if(flagLogin)
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
            catch
            {
                return View();
            }
        }
    }


}