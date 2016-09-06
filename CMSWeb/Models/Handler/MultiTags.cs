using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMSWeb.Models;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace CMSWeb.Models.Handler
{
    public class MultiTags
    {

        #region globals var
        private static string path = Path.TemplatePath();
        private static string link = URL.GetLink();
        private static string Secure = URL.SecureClientPost();
        private static string Unsecure = URL.UnsecureClientPost();
        private static string result = "";
        #endregion

        #region getSystemTag

        /// <summary>
        /// Return An String With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="tags">String with tags</param>
        /// <returns>String With Tags</returns>
        public static string SystemTag(string tags)
        {
            return st(tags, MasterGlobal.SiteID());
        }

        /// <summary>
        /// Return An String With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="tags">String with tags</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>String With Tags</returns>
        public static string SystemTag(string tags, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);
            if (String.IsNullOrEmpty(erro))
            {
                return st(tags, ids);
            }
            else
            {
                return erro;
            }
        }

        /// <summary>
        /// Get System Tags
        /// </summary>
        private static string st(string tags, string siteID)
        {

            if (!String.IsNullOrEmpty(tags))
            {
                return Content(tags, siteID);
            }
            else
            {
                return "We can't find the information you are try to get";
            }
        }

        #endregion

        #region Multi Tags

        #region Content Data Tags

        /// <summary>
        /// To Switch The Tag Format To Normal String In Content Data 
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string Content(string body)
        {
            return multiTags(body, "contentdata", "", "", null, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Content Data 
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="id">Site ID</param>
        /// <returns>Return The Tags As A Normal String</returns>
        public static string Content(string body, string id)
        {
            return multiTags(body, "contentdata", "", "", null, id);
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Content Data 
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="ConsultantID">Consultad Specific ID</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string ContentByConsultantID(string body, string ConsultantID)
        {
            return multiTags(body, "contentdata", ConsultantID, "", null, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Content Data 
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="BranchID">Branch Specific ID</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string ContentByBranchID(string body, Guid BranchID)
        {
            return multiTags(body, "contentdata", "", BranchID.ToString(), null, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Content Data 
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="ConsultantID">Consultad Specific ID</param>
        /// <param name="BranchID">Branch Specific ID</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string ContentByConsultandBranchID(string body, string ConsultantID, string BranchID)
        {
            return multiTags(body, "contentdata", ConsultantID, BranchID, null, "");
        }

        #endregion

        #region Autoresponder Tags
        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string Autoresponder(string body)
        {
            return multiTags(body, "autoresponder", "", "", null, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrower(string body, Autoresponders.BorrowerCo BorrowerCo)
        {
            return multiTags(body, "autoresponder", "", "", BorrowerCo, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="ConsultantID">Consultad Specific ID</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrowerByConsultantID(string body, string ConsultantID, Autoresponders.BorrowerCo BorrowerCo)
        {
            return multiTags(body, "autoresponder", ConsultantID, "", BorrowerCo, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="ConsultantID">Consultad Specific ID</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrowerByConsultantID(string body, string ConsultantID, Autoresponders.BorrowerCo BorrowerCo, string siteID)
        {
            return multiTags(body, "autoresponder", ConsultantID, "", BorrowerCo, siteID);
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="BranchID">Branch Specific ID</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrowerByBranchID(string body, Guid BranchID, Autoresponders.BorrowerCo BorrowerCo)
        {
            return multiTags(body, "autoresponder", "", BranchID.ToString(), BorrowerCo, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="BranchID">Branch Specific ID</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrowerByBranchID(string body, Guid BranchID, Autoresponders.BorrowerCo BorrowerCo, string SiteID)
        {
            return multiTags(body, "autoresponder", "", BranchID.ToString(), BorrowerCo, SiteID);
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="ConsultantID">Consultad Specific ID</param>
        /// <param name="BranchID">Branch Specific ID</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrowerByConsultantBranchID(string body, string ConsultantID, string BranchID, Autoresponders.BorrowerCo BorrowerCo)
        {
            return multiTags(body, "autoresponder", ConsultantID, BranchID, BorrowerCo, "");
        }

        /// <summary>
        /// To Switch The Tag Format To Normal String In Autoresponder
        /// </summary>
        /// <param name="body">Tags String</param>
        /// <param name="ConsultantID">Consultad Specific ID</param>
        /// <param name="BranchID">Branch Specific ID</param>
        /// <param name="BorrowerCo">Borrower OR CoBorrower Information</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Return The Tags As A Normal String </returns>
        public static string AutoresponderBorrowerByConsultantBranchID(string body, string ConsultantID, string BranchID, Autoresponders.BorrowerCo BorrowerCo, string SiteID)
        {
            return multiTags(body, "autoresponder", ConsultantID, BranchID, BorrowerCo, SiteID);
        }

        #endregion

        #region ClearTags

        /// <summary>
        /// Return String clear of Tags To specific Stirng
        /// </summary> 
        /// <param name="res">String</param>
        /// <returns>String Clear of Tags</returns>
        public static string GeneralClearTags(string res)
        {
            string we1 = @"\{\%(.*?)\%\}";
            Regex regex = new Regex(we1);
            MatchCollection match = regex.Matches(res);

            for (int p = 0; match.Count > p; p++)
            {
                res = res.Replace(match[p].Value, "");
            }
            return res;
        }

        /// <summary>
        /// Return To String Clear By Specific Site ID
        /// </summary>
        /// <param name="BodyExt">To String</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>To String Clear By Specific Site ID</returns>
        public static string multiTagsClear(string BodyExt, string siteID)
        {
            result = "";
            result = multiTags(BodyExt, "contentdata", String.Empty, String.Empty, new Autoresponders.BorrowerCo(), siteID);
            return result;
        }

        #endregion

        // Function Multi Tags
        private static string multiTags(string body, string type, string ConsultantID, string BranchID, Autoresponders.BorrowerCo values, string siteID)
        {
            #region Global Variables
            string id = !String.IsNullOrEmpty(siteID) ? MasterGlobal.ValidGuidID(siteID) : MasterGlobal.SiteID();
            string erro = MasterGlobal.ValidGuidIDError(id);
            string result = "";
            #endregion

            if (String.IsNullOrEmpty(erro))
            {

                #region Global Variables
                result = body;
                string fullAddress = "";
                string fullAddress2 = "";
                string currentURL = URL.getHostClient() + "/";
                string signature = "";
                string photoBranch = "";
                string[] names = new string[1];
                string photoConsultant = "";
                Consultants.Consultant ConsultantTags = new Consultants.Consultant(); // LO
                Branchs.Branch ConsultantBranch = new Branchs.Branch(); //LO Branch
                Branchs.Branch Branches = new Branchs.Branch(); // Branch


                //Get Other Information
                Autoresponders.BorrowerCo dataBCo = values;

                // Get Company Information
                Companys.Company companyTags = Companys.Data(id);
                string ImageCompany = MasterGlobal.GeneralParameter("CRMAdminURL").ToString() + "/Controls/clientlogo/ShowUploadedLogo.aspx?SITE_ID=" + MasterGlobal.SiteID();
                string photoCompany = "<img src='" + ImageCompany + "' alt='Consultant'>";


                // Get Consultant Information 
                if (Consultants.HasConsultant || !String.IsNullOrEmpty(ConsultantID))
                {
                    string c_id = !String.IsNullOrEmpty(ConsultantID) ? ConsultantID : ( Consultants.HasConsultant ?  Consultants.Data().ID : "00000000-0000-0000-0000-000000000000");
                    string branchConsultanID = "00000000-0000-0000-0000-000000000000";
                    ConsultantTags = Consultants.DataByConsultantID(c_id);
                    branchConsultanID = (ConsultantTags.BranchID.Length > 5 ? ConsultantTags.BranchID.ToString() : branchConsultanID);
                    ConsultantBranch = Branchs.DataByID(branchConsultanID);
                    signature = Consultants.SignatureByID(c_id);
                    string fullName = ConsultantTags.FullName.ToString().Trim();
                    names = new string[fullName.Split(' ').Length];
                    names = fullName.Split(' ');
                    photoConsultant = "<img src='" + string.Format("{0}GetConsultantPhoto.aspx?id={1}", currentURL, c_id) + "' alt='Consultant'>";
                }

                // Get Branch Information
                if (Branchs.HasBranch || !String.IsNullOrEmpty(BranchID))
                {
                    string b_id = !String.IsNullOrEmpty(BranchID) ? BranchID : ( Branchs.HasBranch ? Branchs.Data().BranchID : "00000000-0000-0000-0000-000000000000");
                    Branches = Branchs.DataByID(b_id);
                    photoBranch = "<img src='" + string.Format("{0}GetBranchPhoto.aspx?id={1}", currentURL, b_id) + "' alt='Consultant'>";
                }

                #endregion

                #region tags templates Information
                result = result
                    .Replace("{%path%}", path)
                    .Replace("{%link%}", link)
                    .Replace("{%secure%}", Secure)
                    .Replace("{%unsecure%}", Unsecure);
                #endregion

                #region Menu Information tags (only Content Data)
                //Menu General tags
                if (type.ToLower() == "contentdata")
                {
                    result = result
                        .Replace("{%menuHeader%}", MenuBuilders.menuBuilderHeader(siteID))
                        .Replace("{%menuFooter%}", MenuBuilders.menuBuilderFooter(siteID))
                        .Replace("{%menuSidebar%}", MenuBuilders.menuBuilderSidebar(siteID))
                        .Replace("{%menuOther%}", MenuBuilders.menuBuilderOther(siteID));
                }
                #endregion

                #region Company Information tags
                // Company General tags
                result = result
                    .Replace("{%CompanyAddress%}", companyTags.CompanyAddress)
                    .Replace("{%CompanyCity%}", companyTags.CompanyCity)
                    .Replace("{%CompanyState%}", companyTags.CompanyState)
                    .Replace("{%CompanyZip%}", companyTags.CompanyZip)
                    .Replace("{%CompanyPhone%}", companyTags.CompanyPhone)
                    .Replace("{%CompanyCellPhone%}", companyTags.CompanyCellPhone)
                    .Replace("{%CompanyURL%}", companyTags.CompanyURL)
                    .Replace("{%CompanyName%}", companyTags.CompanyName)
                    .Replace("{%CompanyLogo%}", photoCompany)
                    .Replace("{%notificationEmail%}", companyTags.CompanyEmailNotifications)
                    ;


                // Company Contentdata
                if (type.ToLower() == "contentdata")
                    result = result.Replace("{%CompanyID%}", companyTags.CompanyID);

                #endregion

                #region Consultant Information tags

                // Lo Autoresponder
                if (type.ToLower() == "autoresponder")
                    result = result.Replace("{%Signature%}", signature);

                // Consultant General tags
                result = result
                    .Replace("{%LOFullname%}", ConsultantTags.FullName.ToString())
                    .Replace("{%LONMLS%}", ConsultantTags.NMLS.ToString())
                    .Replace("{%LOPhone%}", ConsultantTags.Phone.ToString())
                    .Replace("{%LOPhoto%}", photoConsultant)
                    .Replace("{%LOCell%}", ConsultantTags.CellPhone.ToString().Trim())
                    .Replace("{%LOFax%}", ConsultantTags.Fax.ToString().Trim())
                    .Replace("{%LOEmail%}", ConsultantTags.Email.ToString().Trim())
                    .Replace("{%LOTitle%}", ConsultantTags.Title.ToString().Trim())
                    .Replace("{%LOBiography%}", ConsultantTags.Biography.ToString().Trim())
                    .Replace("{%LOTestimonials%}", ConsultantTags.Testimonial.ToString())
                    .Replace("{%LORole%}", ConsultantTags.Role.ToString().Trim())
                    .Replace("{%LOAddress%}", ConsultantTags.Address.ToString().Trim())
                    .Replace("{%LOFacebook%}", ConsultantTags.FaceBook.ToString())
                    .Replace("{%LOTwitter%}", ConsultantTags.Twitter.ToString())
                    .Replace("{%LOLinkedin%}", ConsultantTags.Linkedin.ToString())
                    .Replace("{%LOBranchName%}", ConsultantBranch.Name)
                    .Replace("{%LOBranchPhone%}", ConsultantTags.BranchPhone.ToString().Trim())
                    .Replace("{%LOBranchFax%}", ConsultantTags.BranchFax.ToString().Trim())
                    .Replace("{%LOBranchNMLS%}", ConsultantTags.BranchNMLS.ToString().Trim())
                    .Replace("{%LOBranchCell%}", ConsultantTags.BranchCell.ToString().Trim())
                    .Replace("{%LOBranchAddress%}", ConsultantTags.BranchAddress.ToString().Trim())
                    .Replace("{%LOBranchCity%}", ConsultantTags.BranchCity.ToString().Trim())
                    .Replace("{%LOBranchState%}", ConsultantTags.BranchState.ToString().Trim())
                    .Replace("{%LOBranchZip%}", ConsultantTags.BranchZip.ToString().Trim())
                    .Replace("{%LOBranchDescription%}", ConsultantTags.BranchDescription.ToString().Trim())
                    .Replace("{%LOBranchWebSite%}", ConsultantTags.BranchWebSite.ToString().Trim())
                    .Replace("{%LOPersonalWebSite%}", ConsultantTags.PersonalWebSite.ToString())
                    .Replace("{%LOLoanApplicationURL%}", ConsultantTags.LoanApplicationURL.ToString())
                    .Replace("{%LOusername%}", ConsultantTags.Username.ToString());


                // LO and Branch contentdata
                if (type.ToLower() == "contentdata")
                {
                    result = result
                        //LO
                        .Replace("{%LOSiteID%}", ConsultantTags.SiteID.ToString())
                        .Replace("{%LOConsultantID%}", ConsultantTags.ID.ToString())
                        .Replace("{%LONMLSprefix%}", ConsultantTags.NMLSprefix.ToString())
                        .Replace("{%LOOrder%}", ConsultantTags.Order.ToString())
                        //Branch
                        .Replace("{%LOBranchBranchID%}", ConsultantBranch.BranchID)
                        .Replace("{%LOBranchBranchSiteID%}", ConsultantBranch.BranchSiteID)
                        .Replace("{%LOBranchEmail%}", ConsultantBranch.Email)
                        .Replace("{%LOBranchOrder%}", ConsultantBranch.SortOrder)
                        .Replace("{%LOBranchLoanApplicationURL%}", ConsultantBranch.LoanApplicationURL)
                        .Replace("{%LOBranchUserName%}", ConsultantBranch.UserName)
                        .Replace("{%LOBranchBiography%}", ConsultantBranch.Biography)
                        .Replace("{%LOBranchSiteName%}", ConsultantBranch.SiteName);
                }

                // LO autoresponder
                if (type.ToLower() == "autoresponder")
                {
                    result = result
                        .Replace("{%LOFirstname%}", names[0])
                        .Replace("{%LOLastname%}", names[names.Length - 1])
                        .Replace("{%LOCity%}", ConsultantTags.BranchCity.ToString().Trim())
                        .Replace("{%LOState%}", ConsultantTags.BranchState.ToString().Trim())
                        .Replace("{%LOZip%}", ConsultantTags.BranchZip.ToString().Trim());
                }
                #endregion

                #region Branch Information tags

                // Branch General tags
                result = result
                .Replace("{%BranchID%}", Branches.BranchID)
                .Replace("{%BranchSiteID%}", Branches.BranchSiteID)
                .Replace("{%BranchPhoto%}", photoBranch)
                .Replace("{%BranchName%}", Branches.Name)
                .Replace("{%BranchNMLS%}", Branches.NMLS)
                .Replace("{%BranchEmail%}", Branches.Email)
                .Replace("{%BranchWebSite%}", Branches.WebSite)
                .Replace("{%BranchSortOrder%}", Branches.SortOrder)
                .Replace("{%BranchCity%}", Branches.City)
                .Replace("{%BranchState%}", Branches.State)
                .Replace("{%BranchZip%}", Branches.Zip)
                .Replace("{%BranchAddress%}", Branches.Address)
                .Replace("{%BranchPhone%}", Branches.Phone)
                .Replace("{%BranchCellPhone%}", Branches.CellPhone)
                .Replace("{%BranchFax%}", Branches.Fax)
                .Replace("{%BranchLoanApplicationURL%}", Branches.LoanApplicationURL)
                .Replace("{%BranchUserName%}", Branches.UserName)
                .Replace("{%BranchDescription%}", Branches.Biography)
                .Replace("{%BranchBiography%}", Branches.Description)
                .Replace("{%BranchSiteName%}", Branches.SiteName);

                #endregion

                #region Borrower and CoBorrower Information tags
                // Company General tags
                if (type.ToLower() == "autoresponder")
                {

                    result = result
                        //Borrower autoresponder
                    .Replace("{%BorrowerAddress%}", dataBCo.BorrowerAddress)
                    .Replace("{%BorrowerCellPhone%}", dataBCo.BorrowerCellPhone)
                    .Replace("{%BorrowerCity%}", dataBCo.BorrowerCity)
                    .Replace("{%BorrowerDOB%}", dataBCo.BorrowerDOB)
                    .Replace("{%BorrowerEmail%}", dataBCo.BorrowerEmail)
                    .Replace("{%BorrowerFirstName%}", dataBCo.BorrowerFirstName)
                    .Replace("{%BorrowerHomePhone%}", dataBCo.BorrowerHomePhone)
                    .Replace("{%BorrowerLastName%}", dataBCo.BorrowerLastName)
                    .Replace("{%BorrowerLoanAmount%}", dataBCo.BorrowerLoanAmount)
                    .Replace("{%BorrowerState%}", dataBCo.BorrowerState)
                    .Replace("{%BorrowerZip%}", dataBCo.BorrowerZip)
                    .Replace("{%BorrowerPassword%}", dataBCo.BorrowerPassword)
                        //Co-Borrower autoresponder
                    .Replace("{%CoBorrowerAddress%}", dataBCo.CoBorrowerAddress)
                    .Replace("{%CoBorrowerCellPhone%}", dataBCo.CoBorrowerCellPhone)
                    .Replace("{%CoBorrowerCity%}", dataBCo.CoBorrowerCity)
                    .Replace("{%CoBorrowerDOB%}", dataBCo.CoBorrowerDOB)
                    .Replace("{%CoBorrowerEmail%}", dataBCo.CoBorrowerEmail)
                    .Replace("{%CoBorrowerFirstName%}", dataBCo.CoBorrowerFirstName)
                    .Replace("{%CoBorrowerHomePhone%}", dataBCo.CoBorrowerHomePhone)
                    .Replace("{%CoBorrowerLastName%}", dataBCo.CoBorrowerLastName)
                    .Replace("{%CoBorrowerState%}", dataBCo.CoBorrowerState)
                    .Replace("{%CoBorrowerZip%}", dataBCo.CoBorrowerZip);
                }
                #endregion

                 
                #region Set Address
                if (Consultants.HasConsultant)
                {
                    if (ConsultantTags.Address != "")
                        fullAddress2 += ConsultantTags.Address;
                    else
                        fullAddress2 += (ConsultantBranch.Address != "" ? ConsultantBranch.Address : companyTags.CompanyAddress) + (ConsultantBranch.City != "" ? " " + ConsultantBranch.City : " " + companyTags.CompanyCity) + (ConsultantBranch.State != "" ? " , " + ConsultantBranch.State : " , " + companyTags.CompanyState) + (ConsultantBranch.Zip != "" ? " " + ConsultantBranch.Zip : companyTags.CompanyZip);

                    fullAddress += (ConsultantBranch.Address != "" ? ConsultantBranch.Address : (ConsultantTags.Address != "" ? ConsultantTags.Address : companyTags.CompanyAddress)) + (ConsultantBranch.City != "" ? " " + ConsultantBranch.City : " " + companyTags.CompanyCity) + (ConsultantBranch.State != "" ? " , " + ConsultantBranch.State : " , " + companyTags.CompanyState) + (ConsultantBranch.Zip != "" ? " " + ConsultantBranch.Zip : companyTags.CompanyZip);
                    result = result.Replace("{%Us%}", "Me");
                }
                else if (Branchs.HasBranch)
                {
                    fullAddress += (Branchs.Data().Address != "" ? Branchs.Data().Address : " " + companyTags.CompanyAddress) + (Branchs.Data().City != "" ? " " + Branchs.Data().City : " " + companyTags.CompanyCity) + (Branchs.Data().State != "" ? " " + Branchs.Data().State : " " + companyTags.CompanyState) + (Branchs.Data().Zip != "" ? " " + Branchs.Data().Zip : " " + companyTags.CompanyZip);
                    fullAddress2 += (Branchs.Data().Address != "" ? Branchs.Data().Address : " " + companyTags.CompanyAddress) + (Branchs.Data().City != "" ? " " + Branchs.Data().City : " " + companyTags.CompanyCity) + (Branchs.Data().State != "" ? " " + Branchs.Data().State : " " + companyTags.CompanyState) + (Branchs.Data().Zip != "" ? " " + Branchs.Data().Zip : " " + companyTags.CompanyZip);
                }
                else
                {
                    result = result.Replace("{%Us%}", "Us");
                    fullAddress = companyTags.CompanyAddress + " " + companyTags.CompanyCity + ", " + companyTags.CompanyState + " " + companyTags.CompanyZip;
                    fullAddress2 = companyTags.CompanyAddress + " " + companyTags.CompanyCity + ", " + companyTags.CompanyState + " " + companyTags.CompanyZip;
                }
                result = result.Replace("{%FullAddress%}", fullAddress).Replace("{%FullAddress2%}", fullAddress2);
                #endregion

                #region Clear Tags (AutoResponder)
                if (type.ToLower() == "autoresponder")
                    result = GeneralClearTags(result);
                #endregion
                 
                return result;
            }
            else
            {
                return erro;
            }
        }

        #endregion

    }
}