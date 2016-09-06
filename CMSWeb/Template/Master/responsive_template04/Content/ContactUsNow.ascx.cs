using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class ContactUsNow : System.Web.UI.UserControl
    {

        #region Public Variables
        public string path;
        public string link;
        public bool los;
        #endregion

        // Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Path.TemplatePath();
            link = URL.GetLink();
            los = Consultants.HasConsultant;
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

        // Concat Address
        private static string ConcatAddress(params string[] parameters)
        {
            if (parameters.Length > 0)
            {
                if (!String.IsNullOrEmpty(parameters[0]))
                {
                    parameters[0] = parameters[0] + ",";
                }
                return String.Join(" ", parameters);
            }
            return "";
        }

        // Full Address
        public static string FullAddress()
        {
            if (Consultants.HasConsultant)
            {
                string AddressBasedOnBranch = ConcatAddress(Consultants.Data().BranchAddress, Consultants.Data().BranchCity, Consultants.Data().BranchState, Consultants.Data().BranchZip);
                if (!String.IsNullOrWhiteSpace(AddressBasedOnBranch))
                {
                    return AddressBasedOnBranch;
                }
                return Consultants.Data().Address;
            }
            else if (Branchs.HasBranch)
            {
                return ConcatAddress(Branchs.Data().Address, Branchs.Data().City, Branchs.Data().State, Branchs.Data().Zip);
            }
            else
            {
                return ConcatAddress(Companys.Data().CompanyAddress, Companys.Data().CompanyCity, Companys.Data().CompanyState, Companys.Data().CompanyZip);
            }
        }
    }
}