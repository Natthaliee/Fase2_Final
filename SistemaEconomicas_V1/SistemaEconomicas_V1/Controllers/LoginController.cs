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
                    //string hashPass = TestContext.Base64Encode(PassId);   
                    if (usr == null)
                        {
                            TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                        }
                    //else if (!usr.UserPass.Equals(hashPass))
                    //    {
                    //    TempData["MsgErr"] = "El nombre de usuario o contraseña no son válidos";
                    //    }
                    else
                        {
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