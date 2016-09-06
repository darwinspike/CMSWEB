using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSWeb.Models.Handler;

namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class SiteMap : System.Web.UI.UserControl
    {

        public string path;
        public string link;

        protected void Page_Load(object sender, EventArgs e)
        {
            link = URL.GetLink();
            path = Path.TemplatePath();

        }
    }
}