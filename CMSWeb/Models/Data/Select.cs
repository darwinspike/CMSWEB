using CMSWeb.Models.Handler;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;
using System.Drawing;

namespace CMSWeb.Models.Data
{
    public class Select
    {
        private static Connections cnn = new Connections();
        private static string SITE_ID;
        private static string sql = "";
        private static string result = "";
        private static int total = 0;

        private static int menu = 1;
        private static string PathBranchImage = "/Content/GenericHandler/getImage.ashx?type=2&ID="; //"/Content/WebServices/getImage.svc/BranchPhoto/";
        private static string PathCompanyImage = "/Content/GenericHandler/getImage.ashx?type=3&ID="; //"/Content/WebServices/getImage.svc/CompanyLogo/";
        private static string PathConsultantImage = "/Content/GenericHandler/getImage.ashx?type=1&ID="; //"/Content/WebServices/getImage.svc/ConsultantPhoto/";
        #region Consumable 

        #region Branch

        public class Branches
        {
            /// <summary>
            /// Return If It's An Branch Environment
            /// </summary>
            /// <param name="site">Site</param>
            /// <returns>If It's An Branch Environment</returns>
            public static bool HasBranch(string site)
            {
                bool f = false;
                try
                {
                    sql = @"SELECT Count(Branch_ID) AS Total FROM tblBranches AS bc INNER JOIN tblSiteAlias AS sa ON bc.BRANCH_SITE_ID = sa.SITE_ID WHERE sa.SITE_ALIASE_NAME = @SITE_ALIASE_NAME";
                    object reader = cnn.ExecuteScalar(sql,
                                new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ALIASE_NAME", site)});
                    if (reader.ToString() != "0")
                        f = true;
                }
                catch
                {
                    throw;
                }

                return f;
            }

            /// <summary>
            /// Return Total Of Branch By Specific Site ID
            /// </summary>
            /// <param name="id">Site ID</param>
            /// <returns>Total Of Branch</returns>
            public static int CountBySiteID(string id)
            {
                int f = 0;
                try
                {
                    sql = @"SELECT TOP 1 COUNT(*) AS total FROM tblBranches WHERE Site_ID=@SITE_ID AND IS_ACTIVE='1'";
                    object reader = cnn.ExecuteScalar(sql,
                                new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", id)});
                    if (reader.ToString() != "0")
                        f = Convert.ToInt32(reader.ToString());
                }
                catch
                {
                    throw;
                }

                return f;
            }

            /// <summary>
            /// Return Branch Infomation By Specific Branch ID
            /// </summary>
            /// <param name="id">Branch ID</param>
            /// <param name="Case"></param>
            /// <returns>Branch Infomation By Specific Branch ID</returns>
            public static Branchs.Branch BranchData(string id, int Case) 
            {
                Branchs.Branch dcb;
                try
                {
                    sql = @"
                        SELECT 
                        TOP 1 Branch_ID, Name, SORT_ORDER AS SORTORDER, EMAIL, txtPhone AS Phone, txtFax AS Fax, txtCellPhone AS CellPhone, txtTestimonial AS Testimonial, 
                        txtBiography AS Biography, txtWebSite AS WebSite, BRANCH_SITE_ID AS BRANCHSITEID, txtAddress1 AS Address,
                        txtCity AS City, txtState AS State, txtZip AS Zip, NMLS, SiteName, ApplyNowLink, LosUsername 
                        FROM tblBranches 
                        WHERE 
                        Branch_ID = CASE WHEN @case = 1 THEN @value else Branch_ID END AND 
                        Name = CASE WHEN @case = 2 THEN @value2 else Name END AND  
                        BRANCH_SITE_ID = CASE WHEN @case = 3 THEN @value else BRANCH_SITE_ID END 
                        AND IS_ACTIVE='1'
                        ";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                        new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@value",  id),
                            new System.Data.SqlClient.SqlParameter("@value2", (Case == 2 ? id : "")),
                            new System.Data.SqlClient.SqlParameter("@case", Case)                        
                    }))
                    {
                        if (reader.Read())
                        {
                            dcb = new Branchs.Branch();
                            dcb.BranchID = reader["Branch_ID"].ToString();
                            dcb.Name = reader["Name"].ToString();
                            dcb.SortOrder = reader["SORTORDER"].ToString();
                            dcb.Image = "<img src='" + PathBranchImage + reader["Branch_ID"].ToString() + "/'>";
                            dcb.Email = reader["EMAIL"].ToString();
                            dcb.Phone = reader["Phone"].ToString();
                            dcb.Fax = reader["Fax"].ToString();
                            dcb.CellPhone = reader["CellPhone"].ToString();
                            dcb.Description = reader["Testimonial"].ToString();
                            dcb.Biography = reader["Biography"].ToString();
                            dcb.WebSite = reader["WebSite"].ToString();
                            dcb.BranchSiteID = reader["BRANCHSITEID"].ToString();
                            dcb.Address = reader["Address"].ToString();
                            dcb.City = reader["City"].ToString();
                            dcb.State = reader["State"].ToString();
                            dcb.Zip = reader["Zip"].ToString();
                            dcb.FullAddress = reader["Address"].ToString() + ", " + reader["City"].ToString() + ", " + reader["State"].ToString() + ", " + reader["Zip"].ToString();
                            dcb.NMLS = reader["NMLS"].ToString();
                            dcb.SiteName = reader["SiteName"].ToString();
                            dcb.LoanApplicationURL = reader["ApplyNowLink"].ToString();
                            dcb.UserName = reader["LosUsername"].ToString();
                        }
                        else
                        {
                            dcb = new Branchs.Branch();
                            dcb.Error = "We can't find the information you are try to get";
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                return dcb;
            }

            /// <summary>
            /// All Branches Information By Specific Site ID 
            /// </summary>
            /// <param name="SiteID">Site ID</param>
            /// <returns></returns>
            public static List<Branchs.Branch> DataBranchAll(string SiteID)
            {
                List<Branchs.Branch> dcb = new List<Branchs.Branch>();

                try
                {
                    sql = @"SELECT Branch_ID, Name, SORT_ORDER AS SORTORDER, EMAIL, txtPhone AS Phone, txtFax AS Fax, txtCellPhone AS CellPhone, txtTestimonial AS Testimonial, txtBiography AS Biography, txtWebSite AS WebSite, BRANCH_SITE_ID AS BRANCHSITEID, 
                            txtAddress1 AS Address, txtCity AS City, txtState AS State, txtZip AS Zip, NMLS, SiteName, ApplyNowLink, LosUsername FROM tblBranches  WHERE Site_ID = @SITE_ID AND IS_ACTIVE='1'";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", SiteID)}))
                    {                      
                        while (reader.Read())
                        {
                            dcb.Add(new Branchs.Branch(){

                            BranchID = reader["Branch_ID"].ToString(),
                            Name = reader["Name"].ToString(),
                            Image = "<img src='" + PathBranchImage + reader["Branch_ID"].ToString() + "/'>",
                            SortOrder = reader["SORTORDER"].ToString(),
                            Email = reader["EMAIL"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Fax = reader["Fax"].ToString(),
                            CellPhone = reader["CellPhone"].ToString(),
                            Description = reader["Testimonial"].ToString(),
                            Biography = reader["Biography"].ToString(),
                            WebSite = reader["WebSite"].ToString(),
                            BranchSiteID = reader["BRANCHSITEID"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Zip = reader["Zip"].ToString(),
                            FullAddress = reader["Address"].ToString() + ", " + reader["City"].ToString() + ", " + reader["State"].ToString() + ", " + reader["Zip"].ToString(),
                            NMLS = reader["NMLS"].ToString(),
                            SiteName = reader["SiteName"].ToString(),
                            LoanApplicationURL = reader["ApplyNowLink"].ToString(),
                            UserName = reader["LosUsername"].ToString()
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return dcb;

            }


        }

        #endregion

        #region Company

        public class Companies {

            /// <summary>
            /// Return Company Information By Specific Site ID
            /// </summary>
            /// <param name="SiteID"></param>
            /// <returns>Company Information By Specific Site ID</returns>
            public static Companys.Company DataCompany(string SiteID)
            {
                Companys.Company dc;
                try
                {
                    sql = @"SELECT TOP 1 c.site_id AS CompanyID, c.notification_email AS notificationEmail, cpc.content_value AS CompanyURL, c.full_site_name AS CompanyName, sa.[Address] AS CompanyAddress, sa.City AS CompanyCity, sa.[State] AS CompanyState, sa.Zip AS CompanyZip, 
                        sa.Phone AS CompanyPhone, sa.CellPhone AS CompanyCellPhone 
                        FROM tblclientsites AS c LEFT JOIN [tblSiteAttributes] AS sa ON c.site_id = sa.site_id LEFT JOIN tblClientPagesContent AS cpc ON cpc.site_id = c.site_id WHERE c.SITE_ID = @site_id AND content_type_name = @EMAIL_SITE_URL";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                    new System.Data.SqlClient.SqlParameter("@SITE_ID", SiteID),
                    new System.Data.SqlClient.SqlParameter("@EMAIL_SITE_URL", "EMAIL_SITE_URL" )    }))
                    {
                        if (reader.Read())
                        {
                            dc = new Companys.Company();
                            dc.CompanyID = reader["CompanyID"].ToString();
                            dc.CompanyImage = "<img src='" + PathCompanyImage + reader["CompanyID"].ToString() + "/'>";
                            dc.CompanyEmailNotifications = reader["notificationEmail"].ToString();
                            dc.CompanyURL = reader["CompanyURL"].ToString();
                            dc.CompanyName = reader["CompanyName"].ToString();
                            dc.CompanyAddress = reader["CompanyAddress"].ToString();
                            dc.CompanyCity = reader["CompanyCity"].ToString();
                            dc.CompanyState = reader["CompanyState"].ToString();
                            dc.CompanyZip = reader["CompanyZip"].ToString();
                            dc.CompanyFullAddress = reader["CompanyAddress"].ToString() + ", " + reader["CompanyCity"].ToString() + ", " + reader["CompanyState"].ToString() + ", " + reader["CompanyZip"].ToString();
                            dc.CompanyPhone = reader["CompanyPhone"].ToString();
                            dc.CompanyCellPhone = reader["CompanyCellPhone"].ToString();
                        }
                        else
                        {
                            dc = new Companys.Company();
                            dc.Error = "We can't find the information you are try to get";
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return dc;
            }

            /// <summary>
            /// Return Corporate Site Name By Any Type Of Search 
            /// </summary>
            /// <param name="search"></param>
            /// <param name="ISSiteID"></param>
            /// <returns>Corporate Site Name By Any Type Of Search </returns>
            public static string corporateSiteName(string search, bool ISSiteID = false)
            {
                string name = "";
                search = !String.IsNullOrEmpty(search) ? search : String.Empty;

                if (ISSiteID)
                    sql = "select SITE_ALIASE_NAME from tblSiteAlias where SITE_ID=@S_ID";
                else
                    sql = "select SITE_ALIASE_NAME from tblSiteAlias where SITE_ALIASE_NAME=@S_ID";

                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] {
                        new System.Data.SqlClient.SqlParameter("@S_ID", search )}))
                {
                    if (reader.Read())
                    {
                        name = reader["SITE_ALIASE_NAME"].ToString();
                    }
                }
                return name;
            }

        }

        #endregion

        #region Consultant

        public class LoanOfficer
        {
            /// <summary>
            /// Return If It's An Consultant Environment
            /// </summary>
            /// <param name="s">Site ID</param>
            /// <returns></returns>
            public static bool HasConsultant(string s)
            {
                bool f = false;
                sql = @"SELECT Count(Consultant_ID) AS Total FROM tblConsultants AS ct INNER JOIN tblSiteAlias AS sa ON ct.LO_SITE_ID = sa.SITE_ID WHERE sa.SITE_ALIASE_NAME = @SITE_ALIASE_NAME";
                object reader = cnn.ExecuteScalar(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ALIASE_NAME", s)});
                if (reader.ToString() != "0")
                    f = true;

                return f;
            }

            /// <summary>
            /// Return Total Of Loan Officers
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static int CountBySiteID(string id)
            {
                sql = @"SELECT TOP 1 COUNT(*) AS total FROM tblConsultants WHERE Site_ID = @SITE_ID AND IS_ACTIVE='1'";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                        new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@SITE_ID", id)}))
                {
                    if (reader.Read())
                    {
                        total = Convert.ToInt32(reader["total"].ToString());
                    }
                }
                return total;
            }

            /// <summary>
            /// Return Signature from Specific Site ID
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static string SignatureByID(string id)
            {
                sql = @"SELECT EESignature FROM tblEmailSignatureUsers WHERE Consultant_ID=@C_ID";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@C_ID", id)}))
                {
                    if (reader.Read())
                    {
                        result = reader["EESignature"].ToString();
                    }
                }

                return result;
            }

            /// <summary>
            /// Return Loan Office Infomation By Specific Loan Officer ID
            /// </summary>
            /// <param name="value"></param>
            /// <param name="Case"></param>
            /// <returns>Loan Office Infomation By Specific Loan Officer ID</returns>
            public static Consultants.Consultant consultantData(string value, int Case)
            {
                Consultants.Consultant dc;
                try
                {
                    sql = @"SELECT TOP 1 c.Consultant_ID AS Consultant_ID, c.LO_SITE_ID AS LOSITEID, c.FullName AS FullName, c.SORT_ORDER AS SORTORDER, c.[ROLE] AS [ROLE], c.EMAIL AS EMAIL, 
                        c.txtPhone AS Phone, 
                        c.txtCellPhone AS CellPhone, c.txtTestimonial AS Testimonial, c.txtBiography AS Biography, c.txtWebSite AS WebSite, c.txtFax AS Fax, c.txtTitle AS Title, 
                        c.txtAddress1 AS AddressLO, 
                        c.NMLS AS NMLS, c.Branch_ID AS BranchID, c.nmlsPrefix AS nmlsPrefix, c.social_facebook AS socialFacebook, c.social_linkedin AS socialLinkedin, 
                        c.social_twitter AS socialTwitter, 
                        c.ApplyNowLink AS ApplyNowLink, c.LosUsername AS LosUsername, b.Name AS BranchName, b.txtAddress1 AS BranchAddress, b.txtZip AS BranchZip, b.txtCity AS BranchCity, b.txtState AS BranchState,
                        b.txtPhone AS BranchPhone, b.txtFax AS BranchFax, b.txtCellPhone AS BranchCellPhone, b.NMLS AS BranchNMLS,
                        b.txtWebSite AS BranchWebSite, b.txtBiography as BranchDescription
                        FROM tblConsultants AS c 
                        LEFT JOIN tblBranches AS b ON c.Branch_ID = b.Branch_ID 
                        WHERE 
                        c.LO_SITE_ID = CASE WHEN @case = 1 THEN @value else c.LO_SITE_ID END AND 
                        c.FullName = CASE WHEN @case = 2  THEN @value2 else c.FullName END AND 
                        c.Consultant_ID = CASE WHEN @case = 3  THEN @value else c.Consultant_ID END
                        AND c.IS_ACTIVE = '1' ";


                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@value",  value),
                            new System.Data.SqlClient.SqlParameter("@value2", (Case == 2 ? value : "" )),
                            new System.Data.SqlClient.SqlParameter("@case", Case)                        
                        }))
                    {
                        if (reader.Read())
                        {
                            dc = new Consultants.Consultant();
                            dc.ID = reader["Consultant_ID"].ToString();
                            dc.SiteID = reader["LOSITEID"].ToString();
                            dc.FullName = reader["FullName"].ToString();
                            dc.Order = reader["SORTORDER"].ToString();
                            dc.Image = "<img src='" + PathConsultantImage + reader["Consultant_ID"].ToString() + "/'>";
                            dc.Role = reader["ROLE"].ToString();
                            dc.Email = reader["EMAIL"].ToString();
                            dc.Phone = reader["Phone"].ToString();
                            dc.CellPhone = reader["CellPhone"].ToString();
                            dc.Testimonial = reader["Testimonial"].ToString();
                            dc.Biography = reader["Biography"].ToString();
                            dc.PersonalWebSite = reader["WebSite"].ToString();
                            dc.Fax = reader["Fax"].ToString();
                            dc.Title = reader["Title"].ToString();
                            dc.Address = reader["AddressLO"].ToString();
                            dc.NMLS = reader["NMLS"].ToString();
                            dc.NMLSprefix = reader["nmlsPrefix"].ToString();
                            dc.FaceBook = reader["socialFacebook"].ToString();
                            dc.Linkedin = reader["socialLinkedin"].ToString();
                            dc.Twitter = reader["socialTwitter"].ToString();
                            dc.LoanApplicationURL = reader["ApplyNowLink"].ToString();
                            dc.Username = reader["LosUsername"].ToString();
                            dc.BranchID = reader["BranchID"].ToString();
                            dc.BranchName = reader["BranchName"].ToString();
                            dc.BranchAddress = reader["BranchAddress"].ToString();
                            dc.BranchZip = reader["BranchZip"].ToString();
                            dc.BranchCity = reader["BranchCity"].ToString();
                            dc.BranchState = reader["BranchState"].ToString();

                            dc.BranchPhone = reader["BranchPhone"].ToString();
                            dc.BranchFax = reader["BranchFax"].ToString();
                            dc.BranchCell = reader["BranchCellPhone"].ToString();
                            dc.BranchNMLS = reader["BranchNMLS"].ToString();
                            dc.BranchWebSite = reader["BranchWebSite"].ToString();
                            dc.BranchDescription = reader["BranchDescription"].ToString();



                        }
                        else
                        {
                            dc = new Consultants.Consultant();
                            dc.Error = "We can't find the information you are try to get";
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return dc;
            }

            /// <summary>
            /// Return All Loan Officer By Specific Site ID
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static List<Consultants.Consultant> consultantDataAll(string id)
            {
                List<Consultants.Consultant> dc2 = new List<Consultants.Consultant>();
                try
                {
                    sql = @"SELECT c.Consultant_ID AS Consultant_ID, c.LO_SITE_ID AS LOSITEID, c.FullName AS FullName, c.SORT_ORDER AS SORTORDER, c.[ROLE] AS [ROLE], c.EMAIL AS EMAIL, c.txtPhone AS Phone, 
                            c.txtCellPhone AS CellPhone, c.txtTestimonial AS Testimonial, c.txtBiography AS Biography, c.txtWebSite AS WebSite, c.txtFax AS Fax, c.txtTitle AS Title, c.txtAddress1 AS AddressLO, 
                            c.NMLS AS NMLS, c.Branch_ID AS BranchID, c.nmlsPrefix AS nmlsPrefix, c.social_facebook AS socialFacebook, c.social_linkedin AS socialLinkedin, c.social_twitter AS socialTwitter, 
                            c.ApplyNowLink AS ApplyNowLink, c.LosUsername AS LosUsername, b.Name AS BranchName, b.txtAddress1 AS BranchAddress, b.txtZip AS BranchZip, b.txtCity AS BranchCity, 
                            b.txtState AS BranchState,
						    b.txtPhone AS BranchPhone, b.txtFax AS BranchFax, b.txtCellPhone AS BranchCellPhone, b.NMLS AS BranchNMLS,
						    b.txtWebSite AS BranchWebSite, b.txtBiography as BranchDescription
                            FROM tblConsultants AS c LEFT JOIN tblBranches AS b ON c.Branch_ID = b.Branch_ID WHERE c.Site_ID = @SITE_ID AND c.IS_ACTIVE = '1'";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@SITE_ID", id)}))
                    {
                        while (reader.Read())
                        {
                            dc2.Add(new Consultants.Consultant()
                            {
                                ID = reader["Consultant_ID"].ToString(),
                                SiteID = reader["LOSITEID"].ToString(),
                                Image = "<img src='" + PathConsultantImage + reader["Consultant_ID"].ToString() + "/'>",
                                FullName = reader["FullName"].ToString(),
                                Order = reader["SORTORDER"].ToString(),
                                Role = reader["ROLE"].ToString(),
                                Email = reader["EMAIL"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                CellPhone = reader["CellPhone"].ToString(),
                                Testimonial = reader["Testimonial"].ToString(),
                                Biography = reader["Biography"].ToString(),
                                PersonalWebSite = reader["WebSite"].ToString(),
                                Fax = reader["Fax"].ToString(),
                                Title = reader["Title"].ToString(),
                                Address = reader["AddressLO"].ToString(),
                                NMLS = reader["NMLS"].ToString(),
                                NMLSprefix = reader["nmlsPrefix"].ToString(),
                                FaceBook = reader["socialFacebook"].ToString(),
                                Linkedin = reader["socialLinkedin"].ToString(),
                                Twitter = reader["socialTwitter"].ToString(),
                                LoanApplicationURL = reader["ApplyNowLink"].ToString(),
                                Username = reader["LosUsername"].ToString(),
                                BranchID = reader["BranchID"].ToString(),
                                BranchName = reader["BranchName"].ToString(),
                                BranchAddress = reader["BranchAddress"].ToString(),
                                BranchZip = reader["BranchZip"].ToString(),
                                BranchCity = reader["BranchCity"].ToString(),
                                BranchState = reader["BranchState"].ToString(),
                                BranchPhone = reader["BranchPhone"].ToString(),
                                BranchFax = reader["BranchFax"].ToString(),
                                BranchCell = reader["BranchCellPhone"].ToString(),
                                BranchNMLS = reader["BranchNMLS"].ToString(),
                                BranchWebSite = reader["BranchWebSite"].ToString(),
                                BranchDescription = reader["BranchDescription"].ToString()
                            });
                        }
                    }
                    return dc2;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            /// <summary>
            /// Return All Loan Officer By Specific Site ID
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static List<Consultants.Consultant> consultantDataByBranch(string branchid)
            {
                List<Consultants.Consultant> dc2 = new List<Consultants.Consultant>();
                try 
                {
                    sql = @"SELECT c.Consultant_ID AS Consultant_ID, c.LO_SITE_ID AS LOSITEID, c.FullName AS FullName, c.SORT_ORDER AS SORTORDER, c.[ROLE] AS [ROLE], c.EMAIL AS EMAIL, c.txtPhone AS Phone, 
                            c.txtCellPhone AS CellPhone, c.txtTestimonial AS Testimonial, c.txtBiography AS Biography, c.txtWebSite AS WebSite, c.txtFax AS Fax, c.txtTitle AS Title, c.txtAddress1 AS AddressLO, 
                            c.NMLS AS NMLS, c.Branch_ID AS BranchID, c.nmlsPrefix AS nmlsPrefix, c.social_facebook AS socialFacebook, c.social_linkedin AS socialLinkedin, c.social_twitter AS socialTwitter, 
                            c.ApplyNowLink AS ApplyNowLink, c.LosUsername AS LosUsername, b.Name AS BranchName, b.txtAddress1 AS BranchAddress, b.txtZip AS BranchZip, b.txtCity AS BranchCity, 
                            b.txtState AS BranchState,
						    b.txtPhone AS BranchPhone, b.txtFax AS BranchFax, b.txtCellPhone AS BranchCellPhone, b.NMLS AS BranchNMLS,
						    b.txtWebSite AS BranchWebSite, b.txtBiography as BranchDescription
                            FROM tblConsultants AS c LEFT JOIN tblBranches AS b ON c.Branch_ID = b.Branch_ID 
                            WHERE c.Branch_ID = @BranchID AND c.IS_ACTIVE = '1'";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@BranchID", branchid)}))
                    {
                        while (reader.Read())
                        {
                            dc2.Add(new Consultants.Consultant()
                            {
                                ID = reader["Consultant_ID"].ToString(),
                                SiteID = reader["LOSITEID"].ToString(),
                                Image = "<img src='" + PathConsultantImage + reader["Consultant_ID"].ToString() + "/'>",
                                FullName = reader["FullName"].ToString(),
                                Order = reader["SORTORDER"].ToString(),
                                Role = reader["ROLE"].ToString(),
                                Email = reader["EMAIL"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                CellPhone = reader["CellPhone"].ToString(),
                                Testimonial = reader["Testimonial"].ToString(),
                                Biography = reader["Biography"].ToString(),
                                PersonalWebSite = reader["WebSite"].ToString(),
                                Fax = reader["Fax"].ToString(),
                                Title = reader["Title"].ToString(),
                                Address = reader["AddressLO"].ToString(),
                                NMLS = reader["NMLS"].ToString(),
                                NMLSprefix = reader["nmlsPrefix"].ToString(),
                                FaceBook = reader["socialFacebook"].ToString(),
                                Linkedin = reader["socialLinkedin"].ToString(),
                                Twitter = reader["socialTwitter"].ToString(),
                                LoanApplicationURL = reader["ApplyNowLink"].ToString(),
                                Username = reader["LosUsername"].ToString(),
                                BranchID = reader["BranchID"].ToString(),
                                BranchName = reader["BranchName"].ToString(),
                                BranchAddress = reader["BranchAddress"].ToString(),
                                BranchZip = reader["BranchZip"].ToString(),
                                BranchCity = reader["BranchCity"].ToString(),
                                BranchState = reader["BranchState"].ToString(),
                                BranchPhone = reader["BranchPhone"].ToString(),
                                BranchFax = reader["BranchFax"].ToString(),
                                BranchCell = reader["BranchCellPhone"].ToString(),
                                BranchNMLS = reader["BranchNMLS"].ToString(),
                                BranchWebSite = reader["BranchWebSite"].ToString(),
                                BranchDescription = reader["BranchDescription"].ToString()
                            });
                        }
                    }
                    return dc2;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion
         
        #region Master Global

        public class MastersGlobal
        {

            /// <summary>
            /// Return Site ID By Specific Environment
            /// </summary>
            /// <param name="s">Site Name</param>
            /// <returns>Site ID By Company</returns>
            public static string CorporativeSiteID(string s) {

                sql = @"SELECT CASE WHEN PARENT_SITE_ID IS NULL THEN sa.SITE_ID ELSE PARENT_SITE_ID END AS SITEID
                        FROM tblClientSites AS cs INNER JOIN tblSiteAlias AS sa ON cs.SITE_ID = sa.SITE_ID 
                        WHERE (sa.SITE_ALIASE_NAME = @SITE_ALIASE_NAME)";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ALIASE_NAME", s)}))
                {
                    if (reader.Read())
                    {
                        result = reader["SITEID"].ToString();
                    }
                }

                return result;
            }

            /// <summary>
            /// Return Site ID By Specific Environment
            /// </summary>
            /// <param name="s">Site Name</param>
            /// <returns>Site ID By Specific Environment</returns>
            public static string SiteID(string s)
            {

                sql = @"SELECT cs.SITE_ID AS SITEID 
                        FROM tblClientSites AS cs INNER JOIN tblSiteAlias AS sa ON cs.SITE_ID = sa.SITE_ID 
                        WHERE (sa.SITE_ALIASE_NAME = @SITE_ALIASE_NAME)";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ALIASE_NAME", s)}))
                {
                    if (reader.Read())
                    {
                        result = reader["SITEID"].ToString();
                    }
                }

                return result;
            }

            /// <summary>
            /// Return Site ID By Specific Site Name
            /// </summary>
            /// <param name="name">Site Name</param>
            /// <returns>Site ID By Specific Site Name</returns>
            public static string SiteIDBySiteName(string name)
            {
                try
                {
                    sql = "SELECT TOP 1 SITE_ID FROM tblSiteAlias WHERE SITE_ALIASE_NAME=@name";

                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@name", name)}))
                    {
                        if (reader.Read())
                            result = reader["SITE_ID"].ToString();
                        else
                            result = "Sorry, something went wrong";
                    }

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            /// <summary>
            /// Return General Paramenter By Specific Server Name
            /// </summary>
            /// <param name="serverName">Server Name</param>
            /// <returns>General Paramenter By Specific Server Name</returns>
            public static string GeneralParameter(string serverName)
            {
                string serverURL = String.Empty;
                string name;
                sql = "SELECT ServerURL FROM tblGlobalServer WHERE serverName=@serverName";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] {
                        new System.Data.SqlClient.SqlParameter("@serverName", serverName )}))
                {
                    if (reader.Read())
                    {
                        name = reader["ServerURL"].ToString();
                    }
                }

                return serverURL;
            }



        }

        #endregion

        #region Site

        public class SiteInformation
        {

            private static string urlAdmin = "";

            /// <summary>
            /// Return Site Information By Specific Company ID
            /// </summary>
            /// <param name="value"></param>
            /// <param name="Case"></param>
            /// <returns>Site Information By Specific Company ID</returns>
            public static Sites.site DataSite(string value, int Case = 1)
            {
                Sites.site st;
                try
                {
                    sql = @"
                        SELECT TOP 1 tas.SITE_ID AS siteID, sa.SITE_ALIASE_NAME AS SITE_ALIASE_NAME, sa.TEMPLATE_NAME AS TEMPLATE_NAME, ad.txtCellPhone AS CellPhone, ad.txtEmail AS email, 
                        ad.txtFax AS fax, ad.txtFirstName AS FirstName, ad.txtLastName AS LastName,
                        ad.txtPhone AS Phone, ad.txtTitle AS title FROM tblSiteAlias sa INNER JOIN tblAdmin_Sites tas ON sa.SITE_ID = tas.SITE_ID 
                        INNER JOIN tblAdminDetails ad ON tas.ADMIN_ID = ad.ADMIN_ID WHERE 
                        sa.SITE_ID = CASE WHEN @case = 1 THEN @value else sa.SITE_ID END AND
                        sa.SITE_ALIASE_NAME = CASE WHEN @case = 2 THEN @value2 else sa.SITE_ALIASE_NAME END ";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@value",  value),
                            new System.Data.SqlClient.SqlParameter("@value2", (Case == 2 ? value : "" )),
                            new System.Data.SqlClient.SqlParameter("@case", Case)                        
                        }))
                    {
                        if (reader.Read())
                        {
                            st = new Sites.site();
                            st.SiteAlias = reader["SITE_ALIASE_NAME"].ToString();
                            st.Image = "<img src='" + urlAdmin + PathCompanyImage + reader["siteID"].ToString() + "/'>";
                            st.TemName = reader["TEMPLATE_NAME"].ToString();
                            st.CellPhone = reader["CellPhone"].ToString();
                            st.Email = reader["email"].ToString();
                            st.Fax = reader["fax"].ToString();
                            st.FirtName = reader["FirstName"].ToString();
                            st.LastName = reader["LastName"].ToString();
                            st.Phone = reader["Phone"].ToString();
                            st.Title = reader["title"].ToString();
                        }
                        else
                        {
                            st = new Sites.site();
                            st.Error = "We can't fint the information you are try to get";
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return st;
            }
        }

        #endregion

        #endregion

        #region Handler

        #region Implementations

        public class Implement {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="SiteID"></param>
            /// <returns></returns>
            public static Implementations.Implementation implemen(string SiteID)
            {
                Implementations.Implementation imh = new Implementations.Implementation();
                try
                {
                    sql = @"SELECT TCI.ID AS ImplementationID, TCI.TemplateName AS TemplateName, TCI.TemplatePath AS TemplatePath, 
                            TCI.TemplateTypeID AS TemplateID, TTT.TemplateName AS TemplateTypeName, TTT.TemplateOrder AS TemplateOrder ,TCI.[Catalog] AS Catalogs, TCI.Active AS Active, 
                            TCI.CreateDate AS CreateDate, TCI.UpdateDate AS updateDate, TCI.DeleteDate AS DeleteDate
                            FROM tblClientImp AS TCI INNER JOIN tblTemplateType AS TTT ON TTT.ID = TCI.TemplateTypeID 
                            WHERE SiteID = @SiteID AND TCI.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID)
                           }))
                    {
                        if (reader.Read())
                        {
                            imh.ID = reader["ImplementationID"].ToString();
                            imh.SiteID = SiteID;
                            imh.TemplateName = reader["TemplateName"].ToString();
                            imh.TemplatePath = reader["TemplatePath"].ToString();
                            imh.TemplateType = reader["TemplateTypeName"].ToString();
                            imh.TemplateTypeOrder = Convert.ToInt32(reader["TemplateOrder"].ToString());
                            imh.Catalogs = Convert.ToBoolean(reader["Catalogs"].ToString());
                            imh.Active = reader["Active"].ToString();
                            imh.CreateDate = reader["CreateDate"].ToString();
                            imh.UpdateDate = reader["updateDate"].ToString();
                            imh.DeleteDate = reader["DeleteDate"].ToString();
                        }
                    }
                }
                catch
                {
                    throw;
                }

                return imh;

            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="SiteID"></param>
            /// <param name="PageName"></param>
            /// <returns></returns>
            public static Implementations.CustomPage cusPage(string SiteID, string PageName)
            {
                Implementations.CustomPage cp = new Implementations.CustomPage();

                try
                {
                    sql = @"
                        SELECT 
                        TCPs.ID AS CustomPageID, TCPs.ClientImpID AS ImplementationID, TCPs.PageName AS PageName, TCPs.[Path] AS CustomPath, 
                        TCPs.Value AS CustomValue, TCPs.Active AS CustomActive, TCPs.CreateDate AS CustomCreateDate, TCPs.UpdateDate AS CustomUpdate, 
                        TCPs.DeleteDate AS CustomDeleteDate 
                        FROM tblCustomPages AS TCPs INNER JOIN tblClientImp AS TCI ON TCI.ID = TCPs.ClientImpID 
                        WHERE TCI.SiteID = @SiteID AND TCPs.PageName = @PageName AND TCPs.Active = 1  
                        ";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@PageName", PageName)
                           }))
                    {
                        if (reader.Read())
                        {
                            cp.CustomPageID = reader["CustomPageID"].ToString();
                            cp.ImplementationID = reader["ImplementationID"].ToString();
                            cp.PageName = reader["PageName"].ToString();
                            cp.Path = reader["CustomPath"].ToString();
                            cp.Value = reader["CustomValue"].ToString();
                            cp.Active = reader["CustomActive"].ToString();
                            cp.CreateDate = reader["CustomCreateDate"].ToString();
                            cp.UpdateDate = reader["CustomUpdate"].ToString();
                            cp.DeleteDate = reader["CustomDeleteDate"].ToString();
                        }
                    }
                }
                catch
                {
                    throw;
                }


                return cp;
            }

        }

        #endregion

        #region Path and URL

        public class Paths
        {

            public static List<string> toolsPath(int toolOrder)
            {
                List<string> t = new List<string>();
                try
                {
                    sql = @"SELECT t.ToolPath, t.ToolFolder FROM tblTools AS t WHERE t.ToolOrder = @toolOrder AND t.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@toolOrder", toolOrder)
                           }))
                    {
                        if (reader.Read())
                        {
                            t.Add(reader["ToolPath"].ToString());
                            t.Add(reader["ToolFolder"].ToString());

                        }
                        else
                        {
                            t.Add("");
                            t.Add("");

                        }
                    }
                }
                catch
                {
                    throw;
                }

                return t;
            }

        }

        public class URLs
        {
            
        }
        #endregion

        #endregion

        #region Generic
        public class Generic
        {
            #region Logos
            public class logos 
            {
                public static byte[] Logo(string SiteID, int types)
                {
                    byte[] b = new byte[0];
                    switch (types)
                    {
                        case 1:
                            sql = "select top 1 imgPhoto AS Photo from tblConsultants  WITH(NOLOCK) where Consultant_ID = @ID";
                            break;
                        case 2:
                            sql = "select top 1 imgPhoto AS Photo from tblBranches  WITH(NOLOCK) where Branch_ID = @ID";
                            break;
                        case 3:
                            sql = "SELECT socialImage, socialImageSize, socialImageContentType FROM tblSocialMediaSite WHERE socialID = @ID";
                            break;
                        case 4:
                            sql = "select top 1 CLIENT_LOGO AS Photo from tblClientSites  WITH(NOLOCK) where SITE_ID= @ID";
                            break;
                    }

                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@ID", SiteID)
                    }))
                    {
                        if (reader.Read())
                        {
                            b = (byte[])reader["Photo"];
                        }
                    }

                    return b;
                }
            }
            #endregion
        }
        #endregion

        #region Tools

        #region MenuBuilders

        public class Menu
        {
            /// <summary>
            /// Return Catalog ID By Spefic Site ID and Order Tools
            /// </summary>
            /// <param name="siteID">Site ID</param>
            /// <returns>Catalog ID By Spefic Site ID and Order Tools</returns>
            public static string catID(string siteID, int order)
            {
                try
                {
                    sql = @"SELECT TCT.ID AS CatalogeToolID FROM tblClientImp AS TCI INNER JOIN tblCatalogTool AS TCT ON TCI.ID = TCT.ClientImpID INNER JOIN tblTools AS TT ON TCT.ToolID = TT.ID
                            WHERE TCI.[Catalog] = 1 AND TCI.SiteID = @SiteID AND TT.ToolOrder = @ToolOrder AND TCT.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@SiteID", siteID),
                                new System.Data.SqlClient.SqlParameter("@ToolOrder",order)
                           }))
                    {
                        if (reader.Read())
                            result = reader["CatalogeToolID"].ToString();
                        else
                            result = Guid.Empty.ToString();
                    }
                }
                catch
                {
                    throw;
                }

                return result;
            }

            /// <summary>
            /// Return All Menu Properties By Specific Catalog ID and Meny type
            /// </summary>
            /// <param name="type">Menu Type</param>
            /// <param name="catalogID">Catalog ID</param>
            /// <returns>All Menu Properties By Specific Catalog ID and Meny type</returns>
            public static List<MenuBuilders.MenuProperties> MenuBuilderPropertyAll(string type, string catalogID)
            {
                List<MenuBuilders.MenuProperties> MenuProperty = new List<MenuBuilders.MenuProperties>();
                sql = @"SELECT 
                        tmb.ID AS BuilderID,
                        tmp.ID AS PropertyID, tmp.[Index] AS [Index], tmp.MenuBuilderID AS MenuBuilderID, tmp.PropertieName AS Name, 
                        tmp.OriginalTitle AS OriginalTitle, tmp.ParentID AS ParentID, tmp.ShowBranch AS ShowBranch, tmp.ShowCorporate AS ShowCorporate,
                        tmp.ShowLo AS ShowLo, tmp.TargetBlank AS TargetBlank, tmp.[Type] AS [Type], tmp.URL AS URL, tmp.Value AS Value
                        FROM tblMenuBuilder AS tmb INNER JOIN tblMenuProperties AS tmp ON tmb.ID = tmp.MenuBuilderID 
                        INNER JOIN tblMenu AS tm ON tmb.ID = tm.MenuBuilderID WHERE tm.CatalogToolID = @catalogID
                        AND tmb.MenuType = @MenuType and tmb.Published = '1' AND tmp.Active = 1 order by ParentID, [index] ";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@MenuType", type),
                                new System.Data.SqlClient.SqlParameter("@catalogID", catalogID)                           
                           }))
                {
                    while (reader.Read())
                    {
                        MenuProperty.Add(new MenuBuilders.MenuProperties()
                        {
                            name = reader["Name"].ToString().Trim(),
                            url = reader["URL"].ToString().Trim(),
                            target = Convert.ToBoolean(reader["TargetBlank"].ToString().Trim()),
                            id = reader["PropertyID"].ToString().Trim(),
                            showcorporate = Convert.ToBoolean(reader["ShowCorporate"].ToString().Trim()),
                            showbranch = Convert.ToBoolean(reader["ShowBranch"].ToString().Trim()),
                            showlosite = Convert.ToBoolean(reader["ShowLo"].ToString().Trim()),
                            type = reader["Type"].ToString().Trim(),
                            original_title = reader["OriginalTitle"].ToString().Trim(),
                            value = reader["Value"].ToString().Trim(),
                            parent_id = reader["ParentID"].ToString().Trim(),
                            subtree = new string[0],
                            index = Convert.ToInt32(reader["Index"].ToString().Trim()),
                            MenuBuilderID = reader["MenuBuilderID"].ToString().Trim()
                        });
                    }
                }

                return MenuProperty;
            }

            /// <summary>
            /// Return Count All Menu Builder By Specific Menu Builder ID
            /// </summary>
            /// <param name="MenuBuilderID">Menu Builder ID</param>
            /// <returns>Count All Menu Builder By Specific Menu Builder ID</returns>
            public static string CountMenuAllByMenuBuilderID(string MenuBuilderID) // MenuID
            {
                try
                {
                    sql = " SELECT count(*) AS total from tblMenu WHERE MenuBuilderID = @MenuBuilderID AND  Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@MenuBuilderID", MenuBuilderID) 
                       }))
                    {
                        if (reader.Read())
                            result = reader["Total"].ToString().Trim();
                    }
                }
                catch (Exception)
                {
                }

                return result;
            }

            /// <summary>
            /// Return The Total Of All Menu
            /// </summary>
            /// <returns>The Total Of All Menu</returns>
            public static int CountMenuBuilderAllByCatalogID(string catalogID) // MenuBuilderCountAll
            {
                int result = 0;
                try
                {
                    sql = @"SELECT COUNT(*) AS TOTAL FROM tblMenuBuilder AS MB INNER JOIN tblMenu AS M ON MB.ID = M.MenuBuilderID WHERE M.CatalogToolID = @catalogID AND M.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@catalogID", catalogID) 
                       }))
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToInt32(reader["TOTAL"].ToString().Trim());
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return If The Menu Exist  
            /// </summary>
            /// <param name="menuBuilder">MenuBuilder ID</param>
            /// <returns>True = 1 or False = 0</returns>
            public static int CountMenuBuilderByMenuBuilderID(string menuBuilder) // MenuBuilderCount
            {
                int result = 0;
                try
                {
                    sql = " SELECT COUNT(*) AS TOTAL FROM tblMenuBuilder WHERE ID = @menuBuilder AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@menuBuilder", menuBuilder)                       
                       }))
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToInt32(reader["TOTAL"].ToString().Trim());
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return If The Menu Exist 
            /// </summary>
            /// <param name="menuBuilder">MenuBuilder ID</param>
            /// <returns>True = 1 or False = 0</returns>
            public static int CountMenuBuilderByMenuType(string CatalogToolID, string type) // MenuBuilderCountType
            {
                int result = 0;
                try
                {
                    sql = "SELECT COUNT(*) AS TOTAL FROM tblMenuBuilder AS tmb INNER JOIN tblMenu AS tm ON tm.MenuBuilderID = tmb.ID WHERE tmb.MenuType = @MenuType AND tm.CatalogToolID = @CatalogToolID AND tmb.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@MenuType", type),
                            new System.Data.SqlClient.SqlParameter("@CatalogToolID", CatalogToolID),

                       }))
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToInt32(reader["TOTAL"].ToString().Trim());
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return If The Menu Exist 
            /// </summary>
            /// <param name="menuBuilder">MenuBuilder ID</param>
            /// <returns>True = 1 or False = 0</returns>
            public static int CountMenuBuilderByMenuTypeAndPublic(string type, string CatalogToolID) // MenuBuilderCountTypePublic
            {
                string Published = "1";
                int result = 0;
                try
                {
                    sql = @"SELECT COUNT(*) AS TOTAL FROM tblMenuBuilder AS tmb INNER JOIN tblMenu AS tm ON tm.MenuBuilderID = tmb.ID WHERE tmb.MenuType = @MenuType AND tmb.Published = @Published AND tm.CatalogToolID = @CatalogToolID AND tmb.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@MenuType", type),
                            new System.Data.SqlClient.SqlParameter("@CatalogToolID", CatalogToolID),
                            new System.Data.SqlClient.SqlParameter("@Published", Published)                                              
                       }))
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToInt32(reader["TOTAL"].ToString().Trim());
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return Total Items Of a Menu By Specific Menu ID
            /// </summary>
            /// <param name="MenuBuilderID">Specific Menu ID</param>
            /// <returns>Total Items</returns>
            public static int CountMenuPropertyAllByMenuBuilderID(string MenuBuilderID) // MenuBuilderCountPropertyAll
            {
                int result = 0;
                try
                {
                    sql = " SELECT COUNT(*) AS TOTAL FROM tblMenuProperties WHERE MenuBuilderID = @MenuBuilderID AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@MenuBuilderID", MenuBuilderID)
                       }))
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToInt32(reader["TOTAL"].ToString().Trim());
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return IF The Property Of Specific Property Menu ID
            /// </summary>
            /// <param name="menuPropertyID">Specific Property Menu ID</param>
            /// <returns>True = 1 or False = 0</returns>
            public static int CountMenuPropertyByMenuProperty(string menuPropertyID) // MenuBuilderCountProperty
            {
                int result = 0;
                try
                {
                    sql = " SELECT COUNT(*) AS TOTAL FROM tblMenuProperties WHERE ID = @menuPropertyID AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@menuPropertyID", menuPropertyID)
                       }))
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToInt32(reader["TOTAL"].ToString().Trim());
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }


            public static List<MenuBuilders.MenuBuil> information(string catalogID)
            {
                List<MenuBuilders.MenuBuil> menuBuil = new List<MenuBuilders.MenuBuil>();
                List<MenuBuilders.MenuProperties> MenuProperty = new List<MenuBuilders.MenuProperties>();
                int totalMenuBuild = 0; int totalMenuProperty = 0; int i = 0; int j = 0;
                if (!String.IsNullOrEmpty(catalogID))
                {
                    totalMenuBuild = MenuBuilders.CountMenuBuilderAllByCatalogID(catalogID);
                    if (totalMenuBuild > 0)
                    {
                        i = 0;
                        string BuilderID = "";
                        sql = @"
                        SELECT 
                        tmb.ID AS BuilderID, tmb.MenuName AS MenuName, tmb.MenuType AS MenuType, tmb.UpdateDate AS ModifyDate, tmb.Published AS Published, 
                        tmb.Active AS ActiveBuilder, tmb.CreateDate AS CreateBuilder, tmb.DeleteDate AS deleteBuilder,
                        tmp.ID AS PropertyID, tmp.[Index] AS [Index], tmp.MenuBuilderID AS MenuBuilderID, tmp.PropertieName AS Name, 
                        tmp.OriginalTitle AS OriginalTitle, tmp.ParentID AS ParentID, tmp.ShowBranch AS ShowBranch, tmp.ShowCorporate AS ShowCorporate,
                        tmp.Active AS ActivePropertie, tmp.CreateDate AS createPropertie, tmp.UpdateDate AS UpdatePropertie,
                        tmp.DeleteDate AS deletePropertie,
                        tmp.ShowLo AS ShowLo, tmp.TargetBlank AS TargetBlank, tmp.[Type] AS [Type], tmp.URL AS URL, tmp.Value AS Value
                        FROM tblMenuBuilder AS tmb LEFT JOIN tblMenuProperties AS tmp ON tmb.ID = tmp.MenuBuilderID 
                        INNER JOIN tblMenu AS tm ON tmb.ID = tm.MenuBuilderID 
                        WHERE tm.CatalogToolID = @catalogID AND tmp.Active = 1
                        ORDER BY BuilderID 
                        ";
                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@catalogID", catalogID)                                
                               }))
                        {
                            while (reader.Read())
                            {
                                if (BuilderID != reader["BuilderID"].ToString().Trim())
                                {
                                    menuBuil.Add(new MenuBuilders.MenuBuil()
                                    {
                                        name = reader["MenuName"].ToString().Trim(),
                                        id = reader["BuilderID"].ToString().Trim(),
                                        type = reader["MenuType"].ToString().Trim(),
                                        Active = Convert.ToBoolean(reader["ActiveBuilder"].ToString().Trim()),
                                        CreateData = reader["CreateBuilder"].ToString().Trim(),
                                        DeleteData = reader["deleteBuilder"].ToString().Trim(),
                                        ModifyDate = reader["ModifyDate"].ToString().Trim(),
                                        published = Convert.ToBoolean(reader["Published"].ToString().Trim())
                                    });
                                    totalMenuProperty = MenuBuilders.CountMenuPropertyAllByMenuBuilderID(reader["BuilderID"].ToString().Trim());
                                }
                                if (totalMenuProperty > 0)
                                {
                                    if (BuilderID != reader["BuilderID"].ToString().Trim())
                                        MenuProperty = new List<MenuBuilders.MenuProperties>();

                                    MenuProperty.Add(new MenuBuilders.MenuProperties()
                                    {
                                        name = reader["Name"].ToString().Trim(),
                                        url = reader["URL"].ToString().Trim(),
                                        target = Convert.ToBoolean(reader["TargetBlank"].ToString().Trim()),
                                        id = reader["PropertyID"].ToString().Trim(),
                                        showcorporate = Convert.ToBoolean(reader["ShowCorporate"].ToString().Trim()),
                                        showbranch = Convert.ToBoolean(reader["ShowBranch"].ToString().Trim()),
                                        showlosite = Convert.ToBoolean(reader["ShowLo"].ToString().Trim()),
                                        type = reader["Type"].ToString().Trim(),
                                        original_title = reader["OriginalTitle"].ToString().Trim(),
                                        value = reader["Value"].ToString().Trim(),
                                        Active = Convert.ToBoolean(reader["ActivePropertie"].ToString().Trim()),
                                        CreateData = reader["createPropertie"].ToString().Trim(),
                                        UpdateData = reader["UpdatePropertie"].ToString().Trim(),
                                        DeleteData = reader["deletePropertie"].ToString().Trim(),
                                        parent_id = (reader["ParentID"].ToString().Trim() != Guid.Empty.ToString() ? reader["ParentID"].ToString().Trim() : ""),
                                        subtree = new string[0],
                                        index = Convert.ToInt32(reader["Index"].ToString().Trim()),
                                        MenuBuilderID = reader["MenuBuilderID"].ToString().Trim()
                                    });

                                    j++;
                                }

                                if (BuilderID != reader["BuilderID"].ToString().Trim())
                                {
                                    BuilderID = reader["BuilderID"].ToString().Trim();

                                    if (totalMenuProperty > 0)
                                        menuBuil[i].data = MenuProperty;
                                    else
                                        menuBuil[i].data = new List<MenuBuilders.MenuProperties>();

                                    i++;
                                }
                            }
                        }
                    }
                }
                return menuBuil;
            }
        }
        #endregion

        #region ThemeBuilders

        public class Theme
        {

            /// <summary>
            /// Return Count Theme By Specific Theme ID
            /// </summary>
            /// <param name="ThemeID">Theme Builder ID</param>
            /// <returns>Count All Theme By Specific Theme ID</returns>
            public static string CountThemeAllByThemeBuilderID(string ThemeID) // ThemeID
            {
                try
                {
                    sql = "SELECT count(*) AS total from tblTheme WHERE ThemeBuilderID = @ThemeBuilderID  AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@ThemeBuilderID", ThemeID) 
                       }))
                    {
                        if (reader.Read())
                        {
                            result = reader["Total"].ToString().Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }

                return result;
            }

            /// <summary>
            /// Return Count THeme Builder By Specific Theme ID
            /// </summary>
            /// <param name="ThemeID">Theme ID</param>
            /// <returns>Count THeme Builder By Specific Theme ID</returns>
            public static string CountThemeBuilderByID(string ThemeID) // ThemeBuilderCountID
            {
                try
                {
                    sql = " SELECT COUNT(*) AS TOTAL FROM tblThemeBuilder WHERE ID = @ID AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@ID", ThemeID)                       
                       }))
                    {
                        if (reader.Read())
                        {
                            result = reader["TOTAL"].ToString().Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return Count Theme Builder By Catalog ID
            /// </summary>
            /// <param name="catalogID">Catalog ID</param>
            /// <returns>Count Theme Builder By Catalog ID</returns>
            public static string CountThemeBuilderallByCatalogID(string catalogID) // ThemeBuilderCountCatalogID
            {
                try
                {
                    sql = "SELECT COUNT(*) AS TOTAL FROM tblThemeBuilder AS TB INNER JOIN tblTheme AS T ON TB.ID = T.ThemeBuilderID WHERE T.CatalogToolID = @catalogID AND TB.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@catalogID", catalogID) 
                       }))
                    {
                        if (reader.Read())
                        {
                            result = reader["TOTAL"].ToString().Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return Count Theme Properties By Specific Theme ID 
            /// </summary>
            /// <param name="ThemeID">Theme ID</param>
            /// <returns>Count Theme Properties By Specific Theme ID</returns>
            public static string CountThemePropertiesAll(string ThemeID) // ThemeBuilderCountAllProperties
            {
                try
                {
                    sql = " SELECT COUNT(*) AS TOTAL FROM tblThemeProperties WHERE ThemeBuilderID = @ThemeBuilderID AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@ThemeBuilderID", ThemeID)
                       }))
                    {
                        if (reader.Read())
                        {
                            result = reader["TOTAL"].ToString().Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return Count Theme Properties By Spefic Theme Propertties ID
            /// </summary>
            /// <param name="ThemePropertyID"></param>
            /// <returns>Count Theme Properties By Spefic Theme Properties ID</returns>
            public static string CountThemePropertyByID(string ThemePropertyID) // ThemeBuilderCountPropertyID
            {
                try
                {
                    sql = " SELECT COUNT(*) AS TOTAL FROM tblThemeProperties WHERE ID = @ThemePropertyID AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@ThemePropertyID", ThemePropertyID)
                       }))
                    {
                        if (reader.Read())
                        {
                            result = reader["TOTAL"].ToString().Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }
                return result;
            }

            /// <summary>
            /// Return Count Theeme Properues By Theme Builder ID 
            /// </summary>
            /// <param name="ThemeID">Theme ID</param>
            /// <returns>Count Theeme Properues By Theme Builder ID </returns>
            public static int CountThemePropertyAllByThemeBuilderID(string ThemeID) // ThemeBuilderCountAll
            {
                string ids = MasterGlobal.ValidGuidID(ThemeID);
                string erro = MasterGlobal.ValidGuidIDError(ThemeID);
                string err = String.IsNullOrEmpty(erro) ? "We can't find the information you are try to get" : erro;
                int total = 0;
                if (String.IsNullOrEmpty(erro))
                {
                    try
                    {
                        sql = @"SELECT COUNT(*) AS TOTAL FROM tblThemeBuilder AS ttb INNER JOIN tblThemeProperties AS ttp ON ttb.ID = ttp.ThemeBuilderID WHERE ttb.ID = @ThemeID AND ttp.Active = 1";
                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@ThemeID", ThemeID)                           
                           }))
                        {
                            if (reader.Read())
                                total = Convert.ToInt32(reader["TOTAL"].ToString());
                            else
                                total = 0;
                        }
                    }
                    catch
                    {
                        throw;
                    }

                }
                else
                    total = 0;

                return total;
            }

            /// <summary>
            /// Return True or False If Exists Theme Builder By Specific Site ID
            /// </summary>
            /// <returns>True or False If Exists Theme Builder By Specific Site ID</returns>
            public static string ThemeBuiderIsSupported(string SiteID, int tools)
            {
                try
                {

                    sql = @"SELECT TCI.[Catalog] AS TOTAL FROM tblClientImp AS TCI INNER JOIN tblCatalogTool AS TCT ON TCI.ID = TCT.ClientImpID INNER JOIN tblTools AS TT ON TCT.ToolID = TT.ID WHERE TCI.SiteID = @siteID AND TT.ToolOrder = @ToolOrder AND TT.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                        new System.Data.SqlClient.SqlParameter[] { 
                    new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                    new System.Data.SqlClient.SqlParameter("@ToolOrder", tools)
                    }))
                    {
                        if (reader.Read())
                        {
                            result = reader["TOTAL"].ToString().Trim();
                        }
                    }
                }
                catch (Exception)
                {
                }

                return result;
            }

            public static ThemeBuilders.ThemeBuilder datas(string catalogID)
            {
                ThemeBuilders.ThemeBuilder dat = new ThemeBuilders.ThemeBuilder();
                try
                {
                    sql = @"
                                SELECT  TOP 1 
                                TTB.ID AS ThemeBuiderID, TTB.Advanced AS Advanced, TTB.BaseColor AS BaseColor, 
                                TTB.StyleSheet AS StyleSheet, 
                                TTB.UpdateDate AS ModifyDate, TTB.FileType AS FileType, TTB.StyleFile AS StyleFile,
                                TTB.Active AS ActiveTheme, TTB.CreateDate AS CreateTheme, TTB.DeleteDate AS DeleteThemme,
                                TTP.ID AS ThemePropertiesID , TTP.[Key] AS Keys, TTP.Name AS Name, TTP.Value AS Value,
                                TTP.Active AS ActivePropertie, TTP.CreateDate AS CreatePropertie, TTP.UpdateDate AS UpdateProperties,
                                TTP.DeleteDate AS deleteProperties
                                FROM tblThemeBuilder AS TTB LEFT JOIN tblThemeProperties AS TTP ON TTB.ID = TTP.ThemeBuilderID
                                INNER JOIN tblTheme AS TT ON TTB.ID = TT.ThemeBuilderID WHERE TT.CatalogToolID = @catalogID                                
                                ";

                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@catalogID", catalogID)                                
                               }))
                    {
                        if (reader.Read())
                        {
                            dat.id = reader["ThemeBuiderID"].ToString();
                            dat.advanced = Convert.ToBoolean(reader["Advanced"].ToString());
                            dat.baseColor = reader["BaseColor"].ToString();
                            dat.stylesheet = reader["StyleSheet"].ToString();
                            dat.modifydate = reader["ModifyDate"].ToString();
                            dat.filetype = reader["FileType"].ToString();
                            dat.stylefile = reader["StyleFile"].ToString();
                        }
                    }
                }
                catch { }
                return dat;
            }

            public static List<ThemeBuilders.ThemeBuilder> information(string catalogID)
            {
                List<ThemeBuilders.ThemeBuilder> Theme = new List<ThemeBuilders.ThemeBuilder>();
                List<ThemeBuilders.ThemeProperties> ThemePropertie = new List<ThemeBuilders.ThemeProperties>();
                int totalThemeProperties = 0;
                int i = 0;
                string BuilderID = "";
                sql = @"
                                SELECT 
                                TTB.ID AS ThemeBuiderID, TTB.Advanced AS Advanced, TTB.BaseColor AS BaseColor, TTB.StyleSheet AS StyleSheet, 
                                TTB.UpdateDate AS ModifyDate, TTB.FileType AS FileType, TTB.StyleFile AS StyleFile,
                                TTB.Active AS ActiveTheme, TTB.CreateDate AS CreateTheme, TTB.DeleteDate AS DeleteThemme,
                                TTP.ID AS ThemePropertiesID , TTP.[Key] AS Keys, TTP.Name AS Name, TTP.Value AS Value,
                                TTP.Active AS ActivePropertie, TTP.CreateDate AS CreatePropertie, TTP.UpdateDate AS UpdateProperties,
                                TTP.DeleteDate AS deleteProperties
                                FROM tblThemeBuilder AS TTB LEFT JOIN tblThemeProperties AS TTP ON TTB.ID = TTP.ThemeBuilderID
                                INNER JOIN tblTheme AS TT ON TTB.ID = TT.ThemeBuilderID WHERE TT.CatalogToolID = @catalogID                                
                                ";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@catalogID", catalogID)                                
                               }))
                {
                    while (reader.Read())
                    {
                        if (BuilderID != reader["ThemeBuiderID"].ToString().Trim())
                        {
                            Theme.Add(new ThemeBuilders.ThemeBuilder()
                            {
                                id = reader["ThemeBuiderID"].ToString().Trim(),
                                advanced = Convert.ToBoolean(reader["Advanced"].ToString().Trim()),
                                baseColor = reader["BaseColor"].ToString().Trim(),
                                stylesheet = reader["StyleSheet"].ToString().Trim(),
                                modifydate = reader["ModifyDate"].ToString().Trim(),
                                stylefile = reader["StyleFile"].ToString().Trim(),
                                filetype = reader["FileType"].ToString().Trim(),
                                Active = Convert.ToBoolean(reader["ActiveTheme"].ToString().Trim()),
                                createData = reader["CreateTheme"].ToString().Trim(),
                                deleteData = reader["DeleteThemme"].ToString().Trim()
                            });
                            totalThemeProperties = ThemeBuilders.CountThemePropertiesAll(reader["ThemeBuiderID"].ToString().Trim());
                        }
                        if (totalThemeProperties > 0)
                        {
                            ThemePropertie.Add(new ThemeBuilders.ThemeProperties()
                            {
                                id = reader["ThemePropertiesID"].ToString().Trim(),
                                ThemeBuilderID = reader["ThemeBuiderID"].ToString().Trim(),
                                name = reader["Name"].ToString().Trim(),
                                key = reader["Keys"].ToString().Trim(),
                                Active = reader["ActivePropertie"].ToString().Trim(),
                                createDate = reader["CreatePropertie"].ToString().Trim(),
                                deleteDate = reader["UpdateProperties"].ToString().Trim(),
                                updateDate = reader["deleteProperties"].ToString().Trim(),
                                value = reader["Value"].ToString().Trim()
                            }
                            );
                        }

                        if (BuilderID != reader["ThemeBuiderID"].ToString().Trim())
                        {
                            BuilderID = reader["ThemeBuiderID"].ToString().Trim();

                            if (totalThemeProperties > 0)
                                Theme[i].variables = ThemePropertie;
                            else
                                Theme[i].variables = new List<ThemeBuilders.ThemeProperties>();

                            i++;
                        }



                    }
                }


                return Theme;

            }
        }

        #endregion

        #region Autoresponders

        public class Autorespond
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="SiteID"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            public static string getHTML_DATA(string SiteID, string type)
            {
                try
                {
                    /*******************************************************************************
                    * in this section we get the Value Subject or Body for the 
                    * email, at the beginning we set site_id, and the content_value to search (type)
                    *******************************************************************************/
                    using (SqlDataReader reader = cnn.ExecuteReader("SELECT CONTENT_VALUE FROM tblClientPagesContent WHERE SITE_ID=@SITE_ID AND CONTENT_TYPE_NAME=@type",
                                new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", SiteID),
                        new System.Data.SqlClient.SqlParameter("@type", type)}))
                    {
                        /******************************************************************
                        * after the result, we check the values from the database
                        * if it has it'll add the rest of data and start to replace tags
                        ******************************************************************/
                        if (reader.Read())
                        {
                            //Set the result of the previous query
                            //  body = multiTagAutoresponder(reader["CONTENT_VALUE"].ToString(), this.C_ID, this.dataBCo);
                            result = reader["CONTENT_VALUE"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;

            }
        }

        #endregion

        #region ContentBuilders

        public class ContentBuil
        {

            /// <summary>
            /// Return Content Data By Specific Site ID and Content Number
            /// </summary>
            /// <param name="SITE_ID">Site ID</param>
            /// <param name="numberContent">Content Number</param>
            /// <returns>Content Data By Specific Site ID and Content Number</returns>
            public static ContentBuilders.ContentEditor Contents(string SITE_ID, string numberContent)
            {
                ContentBuilders.ContentEditor ce = new ContentBuilders.ContentEditor();
                try
                {
                    sql = @"
                        SELECT 
                        cc.ID AS ContentID, cc.ContentID AS NumContent, cc.Title AS ContentTitle, cc.Body AS ContentBody, 
                        cc.Active AS Active, cc.CreateDate AS ContentCreate, cc.UpdateDate AS ContentUpdate, cc.DeleteDate AS ContentDelete
                        FROM tblCustomContent AS CC 
                        WHERE cc.SiteID = @SiteID AND cc.ContentID = @ContentID
                        ";

                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SiteID", SITE_ID),
                        new System.Data.SqlClient.SqlParameter("@ContentID", numberContent)}))
                    {
                        if (reader.Read())
                        {
                            ce.id = reader["ContentID"].ToString().Trim();
                            ce.NumberContent = reader["NumContent"].ToString().Trim();
                            ce.title = reader["ContentTitle"].ToString().Trim();
                            ce.body = reader["ContentBody"].ToString().Trim();
                            ce.Active = reader["Active"].ToString().Trim();
                            ce.created_at = reader["ContentCreate"].ToString().Trim();
                            ce.updated_at = reader["ContentUpdate"].ToString().Trim();
                            ce.deleted_at = reader["ContentDelete"].ToString().Trim();
                        }
                    }
                }
                catch (Exception) { throw; }
                return ce;
            }

            /// <summary>
            /// Return Count To Custom Page By Specific Site ID
            /// </summary>
            /// <param name="SiteID">Site ID</param>
            /// <returns>Count To Custom Page By Specific Site ID</returns>
            public static string CountCustomPageBySiteID(string SiteID)
            {
                try
                {
                    string sql = "SELECT COUNT(*) AS TOTAL FROM tblCustomContent WHERE SITEID = @SiteID";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID)
                           }))
                    {
                        if (reader.Read())
                            result =  reader["TOTAL"].ToString();
                    }
                }
                catch
                {
                    throw;
                }

                return result;
            }

            /// <summary>
            /// Return True Or False If Exist Any Content By Specific Content ID and Site ID
            /// </summary>
            /// <param name="id">Content ID</param>
            /// <param name="SiteID">Site ID</param>
            /// <returns>True Or False If Exist Any Content By Specific Content ID and Site ID</returns>
            public static string IfExitContent(string id, string SiteID)
            {
                try
                {
                    string sql = @"SELECT ISNULL(COUNT(*),0) AS TOTAL FROM tblCustomContent WHERE SiteID = @SiteID AND ContentID = @ContentID";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@ContentID", id)
                           }))
                    {
                        if (reader.Read())
                        {
                            result = reader["TOTAL"].ToString();                         
                        }
                    }
                }
                catch
                {
                    throw;
                }

                return result;
            }

            /// <summary>
            /// Return SEO ID By Specific Site ID and Content ID
            /// </summary>
            /// <param name="id">Content ID</param>
            /// <param name="SiteID">Site ID</param>
            /// <returns>SEO ID By Specific Site ID and Content ID</returns>
            public static string SeoIDByContentID(string id, string SiteID)
            {
                try
                {
                    string sql = @"
                    SELECT CCS.ID AS CustomSeoID FROM tblCustomContentSEO AS CCS INNER JOIN 
                    tblCustomContent AS CC ON CCS.CustomContentID = CC.ID WHERE CC.SiteID = @SiteID AND 
                    CC.ContentID = @ContentID
                    ";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@ContentID", id)
                           }))
                    {
                        if (reader.Read())
                            return reader["CustomSeoID"].ToString();
                        else
                            return "";
                    }
                }
                catch
                {
                    throw;
                }
            }


            // WebServices
            public static List<ContentBuilders.ContentEditor> ContentAll(string SiteID, string id = "")
            {
                string KeyWord, TitleTags, BodyTags;
                ArrayList arrayKeyWord = new ArrayList(); ;
                List<ContentBuilders.ContentEditor> content = new List<ContentBuilders.ContentEditor>();
                sql = @"SELECT 
                            CC.ID AS CustomContentID, CC.ContentID AS ContentID, CC.Title AS TitleContent, CC.Body, CC.Active AS ActiveContent, 
                            CONVERT(varchar(10),CC.CreateDate,121) AS CreateDate, CONVERT(varchar(10),CC.UpdateDate,121) AS UpdateDate, 
                            CONVERT(varchar(10),CC.DeleteDate,121) AS DeleteDate, 
                            CCS.ID, CCS.Title AS TitleSEO, CCS.[Description] AS Des, CCS.Keyword
                            FROM tblCustomContent AS CC INNER JOIN tblCustomContentSEO AS CCS ON CC.ID = CCS.CustomContentID
                            WHERE CC.SiteID = @SITEID ";
                if (!String.IsNullOrEmpty(id))
                {
                    sql += " AND CC.ContentID = @id";
                }

                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@SITEID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@id", id)
                               }))
                {
                    while (reader.Read())
                    {
                        TitleTags = "";
                        TitleTags = MultiTags.multiTagsClear(reader["TitleContent"].ToString(), SiteID);
                        BodyTags = "";
                        BodyTags = MultiTags.multiTagsClear(reader["Body"].ToString(), SiteID);

                        KeyWord = "";
                        KeyWord = reader["Keyword"].ToString();
                        if (!string.IsNullOrEmpty(KeyWord))
                        {
                            string[] words = KeyWord.Split(',');
                            foreach (string word in words)
                            {
                                arrayKeyWord.Add(word.Trim());
                            }
                        }

                        content.Add(new ContentBuilders.ContentEditor()
                        {
                            id = reader["CustomContentID"].ToString(),
                            title = TitleTags,
                            body = BodyTags,
                            NumberContent = reader["ContentID"].ToString(),
                            seo = (new ContentBuilders.ContentEditorSeo()
                            {
                                title = reader["TitleSEO"].ToString(),
                                description = reader["Des"].ToString(),
                                keywords = arrayKeyWord

                            }),
                            Active = reader["ActiveContent"].ToString(),
                            created_at = reader["CreateDate"].ToString(),
                            updated_at = reader["UpdateDate"].ToString(),
                            deleted_at = reader["DeleteDate"].ToString()
                        });
                    }
                }

                return content;
            }



        }

        #endregion

        #endregion

    }
}