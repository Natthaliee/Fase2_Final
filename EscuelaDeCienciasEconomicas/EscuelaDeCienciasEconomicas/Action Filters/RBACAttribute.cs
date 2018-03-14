using EscuelaDeCienciasEconomicas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EscuelaDeCienciasEconomicas.ActionFilters
{
    public class RBACAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                RBACUser session = new RBACUser().loadSession();
                //if (!filterContext.HttpContext.Request.IsAuthenticated)
                if (session == null)
                {
                    //Redirect user to login page if not yet authenticated.  
                    //This is a protected resource!
                    filterContext.Result = 
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Login", action = "Index" }));
                }
                else
                {
                    /*
                     * Create permission string based on the requested controller name 
                     * and action name in the format
                     * Check if the requesting user has the permission to run the controller's action
                     */
                    if (!session.HasPermission(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName))
                    {
                        filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Unauthorised", action = RaptorAppContext.PAGINA_PERMISOS_INSUFICIENTES }));
                    }
                    /*
                     * If the user has the permission to run the controller's action, then 
                     * filterContext.Result will be uninitialized and executing the controller's  
                     * action is dependant on whether filterContext.Result is uninitialized.
                     */
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Unauthorised", action = RaptorAppContext.PAGINA_ERROR }));
            }
        }
    }
}