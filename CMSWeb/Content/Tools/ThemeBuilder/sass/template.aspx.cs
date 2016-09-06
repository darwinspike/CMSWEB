using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewClientSites.UserControl.MortgageCEO_Forms.Tools.ThemeBuilder.sass
{
    public partial class template : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/less";
            Response.AddHeader("Access-Control-Allow-Origin", "*");
        }
    }
}