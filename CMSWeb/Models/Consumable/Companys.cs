using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;


namespace CMSWeb.Models.Consumable
{
    public class Companys
    {

        #region class to company and site
        public class Company
        {

            public string CompanyURL { get; set; }
            public string CompanyName { get; set; }
            public string CompanyAddress { get; set; }
            public string CompanyCity { get; set; }
            public string CompanyState { get; set; }
            public string CompanyZip { get; set; }
            public string CompanyFullAddress { get; set; }
            public string CompanyPhone { get; set; }
            public string CompanyCellPhone { get; set; }
            public string CompanyEmailNotifications { get; set; }
            public string CompanyEmailTemplateApproval { get; set; }
            public string CompanyID { get; set; }
            public string Error { get; set; }
            public string CompanyImage { get; set; }

            //Data Company List
            public Company()
            {
                this.CompanyURL = "";
                this.CompanyName = "";
                this.CompanyAddress = "";
                this.CompanyCity = "";
                this.CompanyState = "";
                this.CompanyZip = "";
                this.CompanyFullAddress = "";
                this.CompanyPhone = "";
                this.CompanyCellPhone = "";
                this.CompanyEmailNotifications = "";
                this.CompanyEmailTemplateApproval = "";
                this.CompanyID = "";
                this.Error = "";
                this.CompanyImage = "";
            }
        }

        /// <summary>
        /// Return If It's Corporative Site
        /// </summary>
        /// <returns>True or False</returns>
        public static bool HasCorporate
        {
            get
            {
                if (Consultants.HasConsultant)
                    return false;
                else if (Branchs.HasBranch)
                    return false;
                else
                    return true;
            }
        }


        #endregion

        #region Data Company

        /// <summary>
        /// Return Company Information From Current Site
        /// </summary>
        /// <returns>Company Information</returns>
        public static Company Data()
        {
            return Select.Companies.DataCompany(MasterGlobal.SiteID());
        }

        /// <summary>
        /// Return Company Information By Specific Site ID
        /// </summary>
        /// <param name="id"><Site ID/param>
        /// <returns>Company Information By Specific Site ID</returns>
        public static Company Data(string id)
        {
            return Select.Companies.DataCompany(id);

        }

        /// <summary>
        /// Return Company Information By Specific Company Name
        /// </summary>
        /// <param name="name">Site Name</param>
        /// <returns>Company Information By Specific Company Name</returns>
        public static Company DataByName(string name)
        {

            string id = MasterGlobal.SiteIDBySiteName(name);
            return Select.Companies.DataCompany(id);
        }


        #endregion


    }
}