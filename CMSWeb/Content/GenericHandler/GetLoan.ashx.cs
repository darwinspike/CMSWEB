using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;


namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for GetLoan
    /// </summary>
    public class GetLoan : IHttpHandler
    {
        private static Connections cnn = new Connections();
        string SITE_ID = "";
        string BRANCH_ID = "";
        string BRANCH_ALL = "";
        string NAME = "";
        string BRANCH_NAME = "";


        public class Branch
        {
            public string name { get; set; }
        }

        public class error
        {

            public string name { get; set; }
            public string value { get; set; }
        }

        public class LOs
        {
            public string id { get; set; }
            public string type { get; set; }
            public string fullName { get; set; }
            public string position { get; set; }
            public string phone { get; set; }
            public string nmls { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zipcode { get; set; }
            public string website { get; set; }
            public string email { get; set; }
            public string photo { get; set; }
            public string biography { get; set; }
            public string sortOrder { get; set; }
            public Branch[] branch { get; set; }
        }

        public string returnJSON()
        {
            try
            {
                LOs[] dataResult;
                int getSize = returnTotalValues() + ExtraBranchTotal();
                if (getSize == 0)
                {
                    error[] errorResult = new error[1];
                    errorResult[0] = new error();
                    errorResult[0].name = "error";
                    errorResult[0].value = "data null";
                    return new JavaScriptSerializer().Serialize(errorResult);
                }
                else
                {
                    try
                    {
                        int i = 0;
                        dataResult = new LOs[getSize];
                        try
                        {
                            using (SqlDataReader reader = cnn.ExecuteReader(getNewQuery("SELECT C.Consultant_ID,C.FullName,C.SORT_ORDER, C.EMAIL Email, C.txtTitle, C.txtPassword [Password],C.LosUsername,  C.txtPhone, C.txtCellPhone, C.txtFax, C.txtWebSite, (SELECT FullName FROM tblConsultants WHERE consultant_id=c.consultant_id) Manager, C.IS_MANAGER, C.[Role], CASE WHEN C.imgPhoto IS NOT NULL THEN ('http://cs-test.arginteractive.com/test_build_2/Templates/MortgageCEO_Forms/GetConsultantPhoto.aspx?id='+CONVERT(nvarchar(50), C.Consultant_ID)) ELSE null END Photo, C.txtAddress1 [Address], B.txtCity, B.txtState, B.txtZip, C.NMLS, C.NMLSprefix, B.Name Branch, CASE WHEN C.social_facebook IS NOT NULL THEN C.social_facebook ELSE null END Facebook, CASE WHEN C.social_linkedin IS NOT NULL THEN C.social_linkedin ELSE null END LinkedIn, CASE WHEN C.social_twitter IS NOT NULL THEN C.social_twitter ELSE null END Twitter, c.txtBiography, Content FROM tblconsultants C LEFT JOIN tblBranches B ON C.Branch_ID=B.Branch_ID LEFT JOIN tblLOContent LC ON LC.Consultant_ID=C.Consultant_ID WHERE CBRANCHID CSITEID LONAME AND C.IS_ACTIVE = 1 AND @dummyvar = @dummyvar"),
                                new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@dummyvar", 1)}))
                            {
                                while (reader.Read())
                                {
                                    dataResult[i] = new LOs();
                                    dataResult[i].id = reader[0].ToString();
                                    dataResult[i].address = reader["Address"].ToString().Trim();
                                    dataResult[i].biography = reader["txtBiography"].ToString().Trim();
                                    dataResult[i].city = reader["txtCity"].ToString().Trim();
                                    dataResult[i].state = reader["txtState"].ToString().Trim();
                                    dataResult[i].zipcode = reader["txtZip"].ToString().Trim();
                                    dataResult[i].email = reader["Email"].ToString().Trim();
                                    dataResult[i].fullName = reader["FullName"].ToString().Trim();
                                    dataResult[i].nmls = reader["NMLS"].ToString().Trim();
                                    dataResult[i].sortOrder = reader["SORT_ORDER"].ToString().Trim();
                                    dataResult[i].phone = reader["txtPhone"].ToString().Trim();
                                    dataResult[i].position = reader["txtTitle"].ToString().Trim();
                                    dataResult[i].type = reader["Role"].ToString().Trim();
                                    dataResult[i].website = reader["txtWebSite"].ToString().Trim();

                                    #region Branches
                                    Branch[] dataBranches;
                                    int total = 0;

                                    //get Real Branch
                                    string branchR = reader["Branch"].ToString().Trim();

                                    //get Extra Branches
                                    string content = reader["Content"].ToString().Trim();

                                    int j = 0;
                                    if (!String.IsNullOrEmpty(branchR))
                                    {
                                        total++;
                                    }
                                    if (!String.IsNullOrEmpty(BRANCH_ALL))
                                    {
                                        if (!String.IsNullOrEmpty(content))
                                        {
                                            string[] result = content.Split(';');
                                            foreach (string R in result)
                                            {
                                                total++;
                                            }
                                        }
                                    }
                                    dataBranches = new Branch[total];
                                    if (total > 0)
                                    {
                                        if (!String.IsNullOrEmpty(branchR))
                                        {
                                            dataBranches[j] = new Branch();
                                            dataBranches[j].name = branchR;
                                            j++;
                                        }
                                        if (!String.IsNullOrEmpty(BRANCH_ALL))
                                        {
                                            if (!String.IsNullOrEmpty(content))
                                            {
                                                string[] result = content.Split(';');
                                                foreach (string R in result)
                                                {
                                                    dataBranches[j] = new Branch();
                                                    dataBranches[j].name = R;
                                                    j++;
                                                }
                                            }
                                        }
                                    }
                                    dataResult[i].branch = dataBranches;
                                    #endregion

                                    i++;
                                }
                            }

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        if (!String.IsNullOrEmpty(BRANCH_NAME))
                        {
                            try
                            {
                                using (SqlDataReader reader = cnn.ExecuteReader("SELECT C.Consultant_ID,C.FullName,C.SORT_ORDER, C.EMAIL Email, C.txtTitle, C.txtPassword [Password],C.LosUsername,  C.txtPhone, C.txtCellPhone, C.txtFax, C.txtWebSite, (SELECT FullName FROM tblConsultants WHERE consultant_id=c.consultant_id) Manager, C.IS_MANAGER, C.[Role], CASE WHEN C.imgPhoto IS NOT NULL THEN ('http://cs-test.arginteractive.com/test_build_2/Templates/MortgageCEO_Forms/GetConsultantPhoto.aspx?id='+CONVERT(nvarchar(50), C.Consultant_ID)) ELSE null END Photo, C.txtAddress1 [Address], B.txtCity, B.txtState, B.txtZip, C.NMLS, C.NMLSprefix, B.Name Branch, CASE WHEN C.social_facebook IS NOT NULL THEN C.social_facebook ELSE null END Facebook, CASE WHEN C.social_linkedin IS NOT NULL THEN C.social_linkedin ELSE null END LinkedIn, CASE WHEN C.social_twitter IS NOT NULL THEN C.social_twitter ELSE null END Twitter, c.txtBiography, Content FROM tblconsultants C LEFT JOIN tblBranches B ON C.Branch_ID=B.Branch_ID LEFT JOIN tblLOContent LC ON LC.Consultant_ID=C.Consultant_ID WHERE C.SITE_ID=@SITE_ID AND C.IS_ACTIVE = 1 AND Content like '%@BRANCH_NAME%'",
                                    new System.Data.SqlClient.SqlParameter[] {
                                        new System.Data.SqlClient.SqlParameter("@BRANCH_NAME", BRANCH_NAME),
                                        new System.Data.SqlClient.SqlParameter("@SITE_ID", SITE_ID)}))
                                {
                                    while (reader.Read())
                                    {
                                        dataResult[i] = new LOs();
                                        dataResult[i].id = reader[0].ToString();
                                        dataResult[i].address = reader["Address"].ToString().Trim();
                                        dataResult[i].biography = reader["txtBiography"].ToString().Trim();
                                        dataResult[i].city = reader["txtCity"].ToString().Trim();
                                        dataResult[i].state = reader["txtState"].ToString().Trim();
                                        dataResult[i].zipcode = reader["txtZip"].ToString().Trim();
                                        dataResult[i].fullName = reader["FullName"].ToString().Trim();
                                        dataResult[i].nmls = reader["NMLS"].ToString().Trim();
                                        dataResult[i].email = reader["Email"].ToString().Trim();
                                        dataResult[i].phone = reader["txtPhone"].ToString().Trim();
                                        dataResult[i].sortOrder = reader["SORT_ORDER"].ToString().Trim();
                                        dataResult[i].position = reader["txtTitle"].ToString().Trim();
                                        dataResult[i].type = reader["Role"].ToString().Trim();
                                        dataResult[i].website = reader["txtWebSite"].ToString().Trim();

                                        #region Branches
                                        Branch[] dataBranches;
                                        int total = 0;

                                        //get Real Branch
                                        string branchR = reader["Branch"].ToString().Trim();

                                        //get Extra Branches
                                        string content = reader["Content"].ToString().Trim();

                                        int j = 0;
                                        if (!String.IsNullOrEmpty(branchR))
                                        {
                                            total++;
                                        }
                                        if (!String.IsNullOrEmpty(BRANCH_ALL))
                                        {
                                            if (!String.IsNullOrEmpty(content))
                                            {
                                                string[] result = content.Split(';');
                                                foreach (string R in result)
                                                {
                                                    total++;
                                                }
                                            }
                                        }
                                        dataBranches = new Branch[total];
                                        if (total > 0)
                                        {
                                            if (!String.IsNullOrEmpty(branchR))
                                            {
                                                dataBranches[j] = new Branch();
                                                dataBranches[j].name = branchR;
                                                j++;
                                            }
                                            if (!String.IsNullOrEmpty(BRANCH_ALL))
                                            {
                                                if (!String.IsNullOrEmpty(content))
                                                {
                                                    string[] result = content.Split(';');
                                                    foreach (string R in result)
                                                    {
                                                        dataBranches[j] = new Branch();
                                                        dataBranches[j].name = R;
                                                        j++;
                                                    }
                                                }
                                            }
                                        }
                                        dataResult[i].branch = dataBranches;
                                        #endregion

                                        i++;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        return new JavaScriptSerializer().Serialize(dataResult);
                    }
                    catch (Exception ex)
                    {
                        error[] errorResult = new error[1];
                        errorResult[0] = new error();
                        errorResult[0].name = "error";
                        errorResult[0].value = ex.ToString();
                        return new JavaScriptSerializer().Serialize(errorResult);
                    }
                }
            }
            catch (Exception ex)
            {
                error[] errorResult = new error[1];
                errorResult[0] = new error();
                errorResult[0].name = "error";
                errorResult[0].value = "Something is wrong: " + ex.ToString();
                return new JavaScriptSerializer().Serialize(errorResult);
            }
        }

        private int returnTotalValues()
        {
            try
            {
                using (SqlDataReader reader = cnn.ExecuteReader(getNewQuery("SELECT COUNT(C.Consultant_ID) FROM tblconsultants C LEFT JOIN tblBranches B ON C.Branch_ID=B.Branch_ID LEFT JOIN tblLOContent LC ON LC.Consultant_ID=C.Consultant_ID WHERE CBRANCHID CSITEID LONAME AND C.IS_ACTIVE = 1 AND @dummyvar = @dummyvar"),
                        new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@dummyvar", 1)}))
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader[0].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string getNewQuery(string SQL)
        {
            if (!String.IsNullOrEmpty(BRANCH_ID))
            {
                SQL = SQL.Replace("CBRANCHID", "B.BRANCH_ID='" + BRANCH_ID + "'");
                SQL = SQL.Replace("CSITEID", "");
                SQL = SQL.Replace("LONAME", "");
            }
            if (!String.IsNullOrEmpty(SITE_ID) && String.IsNullOrEmpty(BRANCH_ID))
            {
                SQL = SQL.Replace("CSITEID", "C.SITE_ID='" + SITE_ID + "'");
                SQL = SQL.Replace("CBRANCHID", "");
                SQL = SQL.Replace("LONAME", "");
            }
            if (!String.IsNullOrEmpty(BRANCH_ID) && !String.IsNullOrEmpty(NAME))
            {
                SQL = SQL.Replace("CSITEID", "C.SITE_ID='" + SITE_ID + "'");
                SQL = SQL.Replace("CBRANCHID", "");
                SQL = SQL.Replace("LONAME", " AND C.FullName='%" + NAME + "%'");
            }
            return SQL;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            SITE_ID = MasterGlobal.SiteIDBySiteName(context.Request.QueryString["SiteID"]).ToString();
            BRANCH_ID = context.Request.QueryString["BranchID"];
            if (!String.IsNullOrEmpty(BRANCH_ID))
            {
                BRANCH_NAME = BranchName();
            }
            BRANCH_ALL = context.Request.QueryString["BranchALL"];
            NAME = context.Request.QueryString["name"];
            context.Response.Write(returnJSON());
        }

        public string BranchName()
        {
            try
            {
                using (SqlDataReader reader = cnn.ExecuteReader("SELECT TOP 1 Name FROM tblBranches WHERE Branch_ID=@BRANCH_ID",
                        new System.Data.SqlClient.SqlParameter[] {
                            new System.Data.SqlClient.SqlParameter("@BRANCH_ID", BRANCH_ID)}))
                {
                    if (reader.Read())
                    {
                        return reader[0].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ExtraBranchTotal()
        {
            if (!String.IsNullOrEmpty(BRANCH_ID))
            {
                try
                {
                    using (SqlDataReader reader = cnn.ExecuteReader("SELECT COUNT(Content) FROM tblconsultants C LEFT JOIN tblBranches B ON C.Branch_ID=B.Branch_ID LEFT JOIN tblLOContent LC ON LC.Consultant_ID=C.Consultant_ID WHERE B.Site_ID=@SITE_ID AND C.IS_ACTIVE = 1 AND Content like @BRANCH_NAME",
                           new System.Data.SqlClient.SqlParameter[] {
                               new System.Data.SqlClient.SqlParameter("@SITE_ID", this.SITE_ID),
                            new System.Data.SqlClient.SqlParameter("@BRANCH_NAME", "%" + BRANCH_NAME + "%")}))
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader[0].ToString());
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}