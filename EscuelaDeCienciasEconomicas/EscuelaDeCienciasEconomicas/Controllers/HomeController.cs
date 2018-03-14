using EscuelaDeCienciasEconomicas.ActionFilters;
using EscuelaDeCienciasEconomicas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//CONTROLERS 

namespace EscuelaDeCienciasEconomicas.Controllers
{
    public class HomeController : Controller
    {

        [RBAC]
        public ActionResult Index()
        {
            if (RaptorAppContext.getSessionVAR("mnCollapse") == null || RaptorAppContext.getSessionVAR("mnCollapse").Equals("")) 
            {
                RaptorAppContext.setSessionVAR("mnCollapse", "expandit");
                RaptorAppContext.setSessionVAR("mnCollapseAux", "");
            }
            else
            {
                RaptorAppContext.setSessionVAR("mnCollapse", RaptorAppContext.getSessionVAR("mnCollapse"));
                RaptorAppContext.setSessionVAR("mnCollapseAux", RaptorAppContext.getSessionVAR("mnCollapseAux"));
            }
            return View();
        }

        [RBAC]
        [HttpPost]
        public ActionResult Index(string mnCollapse, string rawURL)
        {
            if (mnCollapse == null || mnCollapse.Equals(""))
            {
                RaptorAppContext.setSessionVAR("mnCollapse", "expandit");
                RaptorAppContext.setSessionVAR("mnCollapseAux", "");
            }
            else if (mnCollapse.Equals("expandit"))
            {
                RaptorAppContext.setSessionVAR("mnCollapse", "collapseit");
                RaptorAppContext.setSessionVAR("mnCollapseAux", "sidebar_shift");
            }
                else if (mnCollapse.Equals("collapseit"))
            {
                RaptorAppContext.setSessionVAR("mnCollapse", "expandit");
                RaptorAppContext.setSessionVAR("mnCollapseAux", "");
            }
            return RedirectToAction("../"+rawURL);
            //return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}