using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Consumable;


namespace CMSWeb.Content.Consumable
{
    public partial class BranchDirectory : System.Web.UI.UserControl
    {
        public string path;
        public string link;
        public string site;
        public string ws;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Path.TemplatePath();
            link = URL.GetLink();
            site = Sites.Data().TemName;
            ws = Companys.Data().CompanyFullAddress;

        }
    }
}