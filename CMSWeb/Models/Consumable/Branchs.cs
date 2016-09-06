using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;


namespace CMSWeb.Models.Consumable
{
    public class Branchs
    {

        #region Class Branch and function

        /// <summary>
        /// Data Branches
        /// </summary>
        public class Branch
        {
            public string BranchID { get; set; }
            public string BranchSiteID { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public string NMLS { get; set; }
            public string Email { get; set; }
            public string WebSite { get; set; }
            public string SortOrder { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Address { get; set; }
            public string FullAddress { get; set; }
            public string SiteID { get; set; }
            public string Phone { get; set; }
            public string CellPhone { get; set; }
            public string Fax { get; set; }
            public string LoanApplicationURL { get; set; }
            public string UserName { get; set; }
            public string Description { get; set; }
            public string Biography { get; set; }
            public string SiteName { get; set; }
            public string Error { get; set; }

            //Data Consultant Branch List
            public Branch()
            {
                this.BranchID = "";
                this.BranchSiteID = "";
                this.Image = "";
                this.Name = "";
                this.NMLS = "";
                this.Email = "";
                this.WebSite = "";
                this.SortOrder = "";
                this.City = "";
                this.State = "";
                this.Zip = "";
                this.SiteID = "";
                this.Address = "";
                this.FullAddress = "";
                this.Phone = "";
                this.CellPhone = "";
                this.Fax = "";
                this.LoanApplicationURL = "";
                this.UserName = "";
                this.Description = "";
                this.Biography = "";
                this.SiteName = "";
                this.Error = "";
            }
        }

        /// <summary>
        /// Return Total Of Branch By Specific Site ID
        /// </summary>
        /// <param name="id">Site ID</param>
        /// <returns>Total Of Branch</returns>
        public static int CountBySiteID(string id)
        {
            int total = 0;
            try
            {
                total = Select.Branches.CountBySiteID(id);
            }
            catch (Exception)
            {
                throw;
            }
            return total;
        }

        /// <summary>
        /// If It's An Branch Environment
        /// </summary>
        public static bool HasBranch
        {
            get { return Select.Branches.HasBranch(URL.GetVarURL()); }
        } 
        #endregion

        #region Branch Information
        /// <summary>
        /// Return Branch Infomation From Current Site 
        /// </summary>
        /// <returns>All Branch Information</returns>
        public static Branch Data()
        {
            return Select.Branches.BranchData(MasterGlobal.SiteID(false), 1);
        }

        /// <summary>
        /// Return Branch Infomation By Specific Branch Name
        /// </summary>
        /// <param name="name">Branch Name</param>
        /// <returns>All Branch Information</returns>
        public static Branch DataByName(string name)
        {
            return Select.Branches.BranchData(name, 2);
        }

        /// <summary>
        /// Return Branch Infomation By Specific Branch ID
        /// </summary>
        /// <param name="id">Branch ID</param>
        /// <returns>All Branch Information</returns>
        public static Branch DataByID(string id)
        {
            return Select.Branches.BranchData(id, 3);
        }

        #endregion

        #region All Branch Information
        /// <summary>
        /// Return All Branches Information From Current Site
        /// </summary>
        /// <returns>All Branches Information From Current Site</returns>
        public static List<Branch> DataAll()
        {
            string id = MasterGlobal.SiteID();
            return Select.Branches.DataBranchAll(id);
        }

        /// <summary>
        /// Return All Branches Information By Specific Site ID
        /// </summary>
        /// <param name="id">Site ID</param>
        /// <returns>All Branches Information By Specific Site ID</returns>
        public static List<Branch> DataAll(string id)
        {
            return Select.Branches.DataBranchAll(id);
        }

        /// <summary>
        /// Return All Branchs Information By Specific Site Name
        /// </summary>
        /// <param name="name">Site Name</param>
        /// <returns>All Branchs Information By Specific Site Name</returns>
        public static List<Branch> DataAllBySiteName(string name)
        {
            string id = MasterGlobal.SiteIDBySiteName(name);
            return Select.Branches.DataBranchAll(id);
        }

       #endregion

    }
}