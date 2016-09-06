using CMSWeb.Models.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSWeb.Models.Consumable;


namespace CMSWeb.Content.Consumable
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        public string path;
        public string link;
        public bool los;
        public string phone;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Path.TemplatePath();
            link = URL.GetLink();
            los = Consultants.HasConsultant;
            phone = Consultants.Data().Phone;
        }

    }
}