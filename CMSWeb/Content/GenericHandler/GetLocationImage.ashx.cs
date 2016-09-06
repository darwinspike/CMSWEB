using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using CMSWeb.Models;
using CMSWeb.Models.Tools;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;


namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for GetLocationImage
    /// </summary>
    public class GetLocationImage : IHttpHandler
    {
        private static Connections cnn = new Connections();

        public class dataResult
        {
            public dataResult()
            {
            }

            public string name { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string cellphone { get; set; }
            public string url { get; set; }
        }

        public string TestJSON()
        {
            dataResult[] data = new dataResult[1];
            data[0] = new dataResult();
            string site_id = MasterGlobal.SiteID();

            try
            {
                using (SqlDataReader reader = cnn.ExecuteReader("select top 1 c.full_site_name CompanyName, (sa.[Address] + ', ' + sa.City + ', ' + sa.[State] + ', ' + sa.Zip) FullAddress, sa.Phone, sa.CellPhone from tblclientsites c LEFT JOIN [tblSiteAttributes] sa ON c.site_id=sa.site_id WHERE sa.SITE_ID=@SITE_ID",
                    new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", site_id)}))
                {
                    if (reader.Read())
                    {
                        data[0].name = reader[0].ToString();
                        data[0].address = reader[1].ToString();
                        data[0].phone = reader[2].ToString();
                        data[0].cellphone = reader[3].ToString();
                    }
                    data[0].url = string.Format("{1}Controls/clientlogo/ShowUploadedLogo.aspx?SITE_ID={0}", site_id, MasterGlobal.GeneralParameter("CRMAdminURL"));
                }
            }
            catch (Exception) { throw; }
            return new JavaScriptSerializer().Serialize(data);
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Write(TestJSON());

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