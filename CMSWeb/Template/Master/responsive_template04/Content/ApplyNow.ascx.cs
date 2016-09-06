using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSWeb.Models.Handler;

namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class ApplyNow : System.Web.UI.UserControl
    {

        #region Public Variables
        public string path;
        public string link;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Path.GetPath();
            link = URL.GetLink();

        }
    }
}