using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using CMSWeb.Models;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Consumable;


namespace CMSWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (String.IsNullOrEmpty(URL.GetVarURL()))
                return View("ErrorPage");
            else
            {
                if (!Implementations.IfExistsImplementation(MasterGlobal.SiteID()))
                    return View("ErrorPage");
                else
                {
                    ViewBag.nowrap = (!String.IsNullOrEmpty(URL.GetVarURL("nowrap")) ? true : false);
                    ViewBag.UrlHeader = "~/" + Implementations.LoadPage("HEADER", true);
                    ViewBag.UrlHomePage = "~/" + Implementations.LoadPage("HOMEPAGE", true);
                    ViewBag.UrlFooter = "~/" + Implementations.LoadPage("FOOTER", true);

                    return View();
                }
            }
        }
         
        public ActionResult ErrorPage() {
            return View();        
        }

      

    }
}
