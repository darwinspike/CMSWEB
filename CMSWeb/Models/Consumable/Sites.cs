using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using System.Data.SqlClient;


namespace CMSWeb.Models.Consumable
{
    public class Sites
    {


        #region class to company and site
        public class site
        {
            public string SiteAlias { get; set; }
            public string TemName { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public string Fax { get; set; }
            public string FirtName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string Error { get; set; }

            public site()
            {
                this.SiteAlias = "";
                this.TemName = "";
                this.CellPhone = "";
                this.Email = "";
                this.Fax = "";
                this.FirtName = "";
                this.LastName = "";
                this.Phone = "";
                this.Title = "";
                this.Image = "";
                this.Error = "";
            }
        }
        #endregion

        #region Data Site

        /// <summary>
        /// Return Site Information By Specific Company ID
        /// </summary>
        /// <param name="id">Site ID</param>
        /// <returns>Site Information By Specific Company ID</returns>
        public static site DataBySiteID(string id)
        {
            return Select.SiteInformation.DataSite(id);
        }

        /// <summary>
        /// Return Site Information From Current Site
        /// </summary>
        /// <returns>Site Information From Current Site</returns>
        public static site Data()
        {
            return Select.SiteInformation.DataSite(MasterGlobal.SiteID());

        }

        /// <summary>
        /// Return Site Information By Specific Company Name
        /// </summary>
        /// <param name="name">Site Name</param>
        /// <returns>Site Information By Specific Company Name</returns>
        public static site DataByName(string name)
        {
            return Select.SiteInformation.DataSite(name, 2);
        }

        #endregion



    }
}