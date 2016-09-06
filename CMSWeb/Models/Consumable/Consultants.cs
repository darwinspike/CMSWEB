using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using System.Runtime.Serialization;


namespace CMSWeb.Models.Consumable
{
    public class Consultants
    {
        [DataContract]
        public class Consultant
        {
            [DataMember]
            public string ID { get; set; }
            [DataMember]
            public string SiteID { get; set; }
            [DataMember]
            public string FullName { get; set; }
            [DataMember]
            public string Image { get; set; }
            [DataMember]
            public string Title { get; set; }
            [DataMember]
            public string NMLS { get; set; }
            [DataMember]
            public string NMLSprefix { get; set; }
            [DataMember]
            public string Order { get; set; }
            [DataMember]
            public string Role { get; set; }
            [DataMember]
            public string Email { get; set; }
            [DataMember]
            public string Phone { get; set; }
            [DataMember]
            public string Fax { get; set; }
            [DataMember]
            public string CellPhone { get; set; }
            [DataMember]
            public string Address { get; set; }
            [DataMember]
            public string Biography { get; set; }
            [DataMember]
            public string Testimonial { get; set; }
            [DataMember]
            public string Content { get; set; }
            [DataMember]
            public string FaceBook { get; set; }
            [DataMember]
            public string Twitter { get; set; }
            [DataMember]
            public string Linkedin { get; set; }
            [DataMember]
            public string PersonalWebSite { get; set; }
            [DataMember]
            public string Manager { get; set; }
            [DataMember]
            public string LoanApplicationURL { get; set; }
            [DataMember]
            public string Username { get; set; }
            [DataMember]
            public string BranchID { get; set; }
            [DataMember]
            public string BranchName { get; set; }
            [DataMember]
            public string BranchAddress { get; set; }
            [DataMember]
            public string BranchZip { get; set; }
            [DataMember]
            public string BranchCity { get; set; }
            [DataMember]
            public string BranchState { get; set; }
            [DataMember]
            public string BranchPhone { get; set; }
            [DataMember]
            public string BranchFax { get; set; }
            [DataMember]
            public string BranchNMLS { get; set; }
            [DataMember]
            public string BranchCell { get; set; }
            [DataMember]
            public string BranchWebSite { get; set; }
            [DataMember]
            public string BranchDescription { get; set; }
            [DataMember]
            public string Error { get; set; }

            public Consultant()
            {
                this.SiteID = "";
                this.ID = "";
                this.FullName = "";
                this.Image = "";
                this.Title = "";
                this.NMLS = "";
                this.NMLSprefix = "";
                this.Role = "";
                this.Order = "";
                this.Email = "";
                this.Phone = "";
                this.Fax = "";
                this.CellPhone = "";
                this.Address = "";
                this.Biography = "";
                this.Testimonial = "";
                this.Content = "";
                this.FaceBook = "";
                this.Twitter = "";
                this.Linkedin = "";
                this.PersonalWebSite = "";
                this.Manager = "";
                this.LoanApplicationURL = "";
                this.Username = "";
                this.BranchID = "";
                this.BranchName = "";
                this.BranchAddress = "";
                this.BranchZip = "";
                this.BranchCity = "";
                this.BranchState = "";
                this.BranchPhone = "";
                this.BranchFax = "";
                this.BranchNMLS = "";
                this.BranchCell = "";
                this.BranchWebSite = "";
                this.BranchDescription = "";
                this.Error = "";

            }
        }

        #region function

        /// <summary>
        /// Return Total Of Loan Officers
        /// </summary>
        /// <param name="id">Site ID</param>
        /// <returns>Total Of Loan Officers</returns>
        public static int CountBySiteID(string id)
        {
            int total = 0;
            string ids = id;
            try
            {
                total = Select.LoanOfficer.CountBySiteID(ids);
            }
            catch (Exception)
            {
                throw;
            }
            return total;
        }

        /// <summary>
        /// Return Signature from Specific Site ID
        /// </summary>
        /// <param name="id">Consultant ID</param>
        /// <returns>Return Signature</returns>
        public static string SignatureByID(string id)
        {
            string Signature = "";
            try
            {
                Signature = Select.LoanOfficer.SignatureByID(id);
            }
            catch (Exception)
            {
                throw;
            }
            return Signature;
        }

        /// <summary>
        /// If It's An Consultant Environment
        /// </summary>
        public static bool HasConsultant{ 
            get {
                return Select.LoanOfficer.HasConsultant(URL.GetVarURL()); 
            } 
        }
        #endregion

        #region Data Consultant

        /// <summary>
        /// Return Loan Officer Infomation From Current Site
        /// </summary>
        /// <returns>Loan Officer Infomation From Current Site</returns>
        public static Consultant Data() 
        {
            return Select.LoanOfficer.consultantData(MasterGlobal.SiteID(false), 1);
        }

        /// <summary>
        /// Return Loan Office Infomation By Specific Loan Officer Name
        /// </summary>
        /// <param name="name">Loan Officer Name</param>
        /// <returns>Loan Office Infomation By Specific Loan Officer Name</returns>
        public static Consultant DataByConsultantName(string name)
        {
            return Select.LoanOfficer.consultantData(name, 2);
        }

        /// <summary>
        /// Return Loan Office Infomation By Specific Loan Officer ID
        /// </summary>
        /// <param name="id">Loan Officer ID</param>
        /// <returns>Loan Office Infomation By Specific Loan Officer ID</returns>
        public static Consultant DataByConsultantID(string id)
        {
            return Select.LoanOfficer.consultantData(id, 3);
        }


        #endregion

        #region All Data Consultant

        /// <summary>
        /// Return All Loan Officer From Current Site
        /// </summary>
        /// <returns>All Loan Officer From Current Site</returns>
        public static List<Consultants.Consultant> DataAll()
        {
            return Select.LoanOfficer.consultantDataAll(MasterGlobal.SiteID());
        }

        /// <summary>
        /// Return All Loan Officer By Specific Site ID
        /// </summary>
        /// <param name="id">Site ID</param>
        /// <returns>All Loan Officer By Specific Site ID</returns>
        public static List<Consultants.Consultant> DataAll(string id)
        {
            return Select.LoanOfficer.consultantDataAll(id);
        }

        /// <summary>
        /// Return All Loan Officer By Specific Site Name
        /// </summary>
        /// <param name="name">Site Name</param> 
        /// <returns>All Loan Officer By Specific Site Name</returns>
        public static List<Consultants.Consultant> DataAllBySiteName(string name)
        {
            string id = MasterGlobal.SiteIDBySiteName(name);
            return Select.LoanOfficer.consultantDataAll(id);
        }


        #endregion

    }
}