using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMSWeb.Models.Handler;
using CMSWeb.Models.Consumable;

namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class GetPages : System.Web.UI.UserControl
    {
        #region Public Variables
        public string path;
        public string link;
        private Control c1 = null;
        private string errorPage = "./ErrorPage.ascx";
        #endregion

        #region Protected Variables
        protected System.Web.UI.WebControls.PlaceHolder PlaceHolderGetPages;
        #endregion

        #region Local Variables
        string getPages;
        #endregion

        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            path = Path.TemplatePath();
            link = URL.GetLink();

            try
            {
                getPages = !String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Content"]) ? HttpContext.Current.Request.QueryString["Content"].ToString().ToLower() : String.Empty;
                if (getPages.Length > 0)
                {
                    string page = Implementations.LoanPageInternal(getPages);
                    c1 = LoadControl(page);
                    this.PlaceHolderGetPages.Controls.Add(c1);
                }
                else
                {
                    this.PlaceHolderGetPages.Controls.Add(LoadControl(errorPage));
                }
            }
            catch (Exception)
            {
                this.PlaceHolderGetPages.Controls.Add(LoadControl(errorPage));
            }
        }

    }
}