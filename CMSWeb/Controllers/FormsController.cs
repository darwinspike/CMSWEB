using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSWeb.Controllers
{
    public class FormsController : Controller
    {
        //
        // GET: /Forms/

        public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult QuickApp()
        {
            return View();
        }

        public ActionResult MiniApp()
        {
            return View();
        }
        
    }
}
