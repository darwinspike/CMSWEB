using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSWeb.Models.Tools;
using CMSWeb.Models.Handler;

namespace NewClientSites.UserControl.MortgageCEO_Forms.ThemeBuilder
{
    public partial class getStylesheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string SiteID = !string.IsNullOrEmpty(Request.QueryString["SiteID"].ToString()) ? Request.QueryString["SiteID"].ToString() : Guid.Empty.ToString();
            string file = !string.IsNullOrEmpty(Request.QueryString["file"].ToString()) ? Request.QueryString["file"].ToString() : "";
            string stylesheet = ThemeBuilders.stylesheet(SiteID).ToString();
            string url = "";

            if (!string.IsNullOrEmpty(stylesheet))
            {
                Response.ContentType = "text/css";
                Response.Write(stylesheet);
            }
            else
            {
                Response.ContentType = "text/css";
                url = "/" + Path.TemplatePath(SiteID) + file;
//                url = "/" + "" + file;
                Response.WriteFile(url);
            }

        }
    }
}