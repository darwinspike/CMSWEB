using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMSWeb.Models;
using CMSWeb.Models.Tools;
using System.Data.SqlClient;
using System.Web.Script.Serialization;


using CMSWeb.Models;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System.Data.SqlClient;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for GetBranch
    /// </summary>
    public class GetBranch : IHttpHandler
    {
        private static Connections cnn = new Connections();
        string SITE_ID = "";
        string NAME = "";
        string POST_CODE = "";
        string STATE = "";

        public class Branch
        {
            public string BranchID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string NMLS { get; set; }
        }

        public class error
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            SITE_ID = MasterGlobal.SiteIDBySiteName(context.Request.QueryString["SiteID"]).ToString();
            NAME = context.Request.QueryString["NAME"];
            POST_CODE = context.Request.QueryString["POSTCODE"];
            STATE = context.Request.QueryString["STATE"];
            context.Response.Write(returnJSON());
        }

        public string returnJSON()
        {
            try
            {
                Branch[] dataResult;
                int getSize = returnTotalValues();
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
                        dataResult = new Branch[getSize];
                        try
                        {
                            using (SqlDataReader reader = cnn.ExecuteReader(getNewQuery("SELECT Branch_ID, Name, txtPhone, txtAddress1, txtCity, txtState, txtZip, NMLS FROM tblBranches WHERE SITE_ID=@SITE_ID AND IS_ACTIVE = 1 NAME POSTCODE STATE"),
                               new System.Data.SqlClient.SqlParameter[] {
                                   new System.Data.SqlClient.SqlParameter("@SITE_ID", SITE_ID)}))
                            {
                                int i = 0;
                                while (reader.Read())
                                {
                                    dataResult[i] = new Branch();
                                    dataResult[i].BranchID = reader[0].ToString();
                                    dataResult[i].Name = reader[1].ToString();
                                    dataResult[i].Phone = reader[2].ToString();
                                    dataResult[i].Address = reader[3].ToString();
                                    dataResult[i].City = reader[4].ToString();
                                    dataResult[i].State = reader[5].ToString();
                                    dataResult[i].Zip = reader[6].ToString();
                                    dataResult[i].NMLS = reader[7].ToString();
                                    i++;
                                }
                                return new JavaScriptSerializer().Serialize(dataResult);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
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

        private string getNewQuery(string SQL)
        {

            if (String.IsNullOrEmpty(NAME))
            {
                SQL = SQL.Replace("NAME", "");
            }
            else
            {
                SQL = SQL.Replace("NAME", " AND Name LIKE '%" + NAME + "%'");
            }
            if (String.IsNullOrEmpty(POST_CODE))
            {
                SQL = SQL.Replace("POSTCODE", "");
            }
            else
            {
                SQL = SQL.Replace("POSTCODE", " AND txtZip=" + POST_CODE);
            }
            if (String.IsNullOrEmpty(STATE))
            {
                SQL = SQL.Replace("STATE", "");
            }
            else
            {
                SQL = SQL.Replace("STATE", " AND txtState='" + STATE + "'");
            }
            return SQL;
        }

        private int returnTotalValues()
        {
            try
            {
                using (SqlDataReader reader = cnn.ExecuteReader(getNewQuery("SELECT COUNT(Branch_ID) AS Branches FROM tblBranches WHERE SITE_ID=@SITE_ID AND IS_ACTIVE = 1 NAME POSTCODE STATE"),
                       new System.Data.SqlClient.SqlParameter[] {
                            new System.Data.SqlClient.SqlParameter("@SITE_ID", SITE_ID)}))
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


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}