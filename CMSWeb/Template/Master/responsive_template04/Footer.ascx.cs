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
    public partial class Footer : ViewUserControl
    {

        #region Public Variables
        public string path = Path.GetPathAssets();
        public string link = URL.GetLink();

        public string fulladdressLOBrnach;
        public string fulladdressBrnach;
        public string fulladdressCompany;
        public string master_path;
        public string ThemePath;
        public string direction;

        public Consultants.Consultant ct;
        public Branchs.Branch bc;
        public Companys.Company cp;
        #endregion
         

        protected void Page_Load(object sender, EventArgs e)
        {
            ct = Consultants.Data();
            fulladdressLOBrnach = ct.BranchAddress + ", " + ct.BranchCity + ", " + ct.BranchState + ", " + ct.BranchZip;
            bc = Branchs.Data();
            fulladdressBrnach = bc.Address + ", " + bc.City + ", " + bc.State + ", " + bc.Zip;
            cp = Companys.Data();
            fulladdressCompany = cp.CompanyAddress + ", " + cp.CompanyCity + ", " + cp.CompanyState + ", " + cp.CompanyZip;

            master_path = Path.TemplatePath();
            ThemePath = Path.ThemeBuilderPathAll();

            direction = "";
            if (Consultants.HasConsultant)
            {
                direction = ct.FullName + "<br />" + ct.Address + " <br />Office: " + ct.Phone;
            }
            else if (Branchs.HasBranch)
            {
                direction = bc.Name + "<br />" + fulladdressBrnach + " <br />Office: " + bc.Phone;
            }
            else
            {
                direction = cp.CompanyName = "<br />" + fulladdressCompany + " <br />Office: " + cp.CompanyPhone;
            }



        }
    }
}