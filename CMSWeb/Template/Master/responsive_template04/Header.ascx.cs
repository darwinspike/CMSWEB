using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMSWeb.Models.Handler;
using CMSWeb.Models.Consumable;
using System.Web.Mvc;

namespace CMSWeb.Template.Master.responsive_template04
{
    public partial class Header : ViewUserControl 
    {
        #region Public Variables
        public string path;
        public string link;
        public string ThemePath;
        public string urlApplyNow;

        public Consultants.Consultant ct;
        public Branchs.Branch bc;
        public Companys.Company cp;
        public Sites.site st;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            link = URL.GetLink();
            urlApplyNow = link + "&content=ApplyNow&type=short";
            path = Path.TemplatePath();
            ThemePath = Path.ThemeBuilderPathAll();

            ct = Consultants.Data();
            bc = Branchs.Data();
            cp = Companys.Data();
            st = Sites.Data();
        }

        // Search Button
        protected void search_Click(object sender, EventArgs e)
        {
//           HttpContext.Current.Response.Redirect(string.Format("{0}&name={1}&email={2}", urlApplyNow, FirstName.Value.Trim(), EmailForm.Value.Trim()));
        }

        // Iterate Conjuction
        public static string IterateConjuction()
        {
            if (Consultants.HasConsultant)
            {
                return "Me";
            }
            return "Us";
        }

    }
}