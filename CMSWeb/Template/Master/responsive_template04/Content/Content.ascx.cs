using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using CMSWeb.Models.Handler;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Tools;
using System.Web.Mvc;

namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class Content : ViewUserControl
    {
        #region Public Variables
        public string path;
        public string link;

        public string contentTitle;
        public string contentBody;
        public string sidebarContent;
        #endregion

        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            path = Path.TemplatePath();
            link = URL.GetLink();

            contentTitle = "&nbsp;";
            contentBody = "...";
            sidebarContent = "";
            string site = Request.QueryString["s"];
            int numContent;
            if (Request.QueryString["pageid"] != null && Request.QueryString["pageid"].Length > 0)
            {
                numContent = Convert.ToInt32(Request.QueryString["pageid"]);
                
                contentTitle = CMSWeb.Models.Temporary.ContentBuilders.TitleWithTags(numContent);
                contentBody = CMSWeb.Models.Temporary.ContentBuilders.BodyWithTags(numContent);
                if ((((Request.QueryString["showsidebar"] ?? "").Length > 0 && Request.QueryString["showsidebar"] != "false") || Request.QueryString["showsidebar"] == "true") && (Request.QueryString["sidebar_id"] ?? "").Length > 0)
                {
                    sidebarContent = ContentBuilders.contentContent(Convert.ToInt32(Request.QueryString["sidebar_id"]), "Content");
                }
                else
                {
                    sidebarContent = ContentBuilders.contentContent(43, "Content");
                }
            }
        }
    }
}