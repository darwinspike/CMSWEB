using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class GenerateTemplateLess : System.Web.UI.Page
    {
        public string pathTheme = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/less";

            //tur = NewClientSites.webTools.ThemeBuilderPathAll() + "sass/template.aspx";
            pathTheme = "../../../../Content/Tools/ThemeBuilder/sass/template.aspx";

        }

        public static string Base64Encode(string url)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
        }

        public static string Base64Decode(string url)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(url));
        }


        public string EncodeURI(string data)
        {
            return HttpUtility.UrlEncode(data);
        }

        public string DecodeURI(string data)
        {
            return HttpUtility.UrlDecode(data);
        }




    }
}