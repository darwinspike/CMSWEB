using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for GetContents
    /// </summary>
    public class GetContents : IHttpHandler
    {

        private static Connections cnn = new Connections();

        public class content
        {
            public int Number { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }

            public content()
            {
                this.ID = "";
                this.Name = "";
                this.Number = 0;
            }

        }

        public string TestJSON(string p)
        {
            string pattern = p;
            string patternStringn = "";
            int cont = 0;
            content[] ct;
            int i = 0;

            Guid s_id = new Guid();
            s_id = new Guid( MasterGlobal.SiteID() );

            string SQL = "SELECT COUNT(CONTENT_ID) Total FROM tblClientPagesContent WHERE content_type_name LIKE '%PAGE_TEXT_CONTENT_NAME%' AND site_id=@SITE_ID AND NOT (CONTENT_VALUE IS NULL OR CAST(CONTENT_VALUE AS nvarchar(max)) = '')";
            if (!String.IsNullOrEmpty(pattern))
            {
                patternStringn = "%" + pattern + "%";
                SQL += " AND CAST(CONTENT_VALUE AS nvarchar(max)) like @parrent";
            }

            using (SqlDataReader reader = cnn.ExecuteReader(SQL,
                    new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", s_id),
                        new System.Data.SqlClient.SqlParameter("@parrent", patternStringn)                        
                    }))
            {
                if (reader.Read())
                    cont = Convert.ToInt32(reader["Total"]);

            }

            if (cont > 0)
            {
                ct = new content[cont];
                SQL = "SELECT CONTENT_ID, CONTENT_VALUE, CAST(REPLACE(content_type_name,'PAGE_TEXT_CONTENT_NAME','') AS INTEGER) AS CONTENT_NUMBER FROM tblClientPagesContent WHERE content_type_name LIKE '%PAGE_TEXT_CONTENT_NAME%' AND site_id=@SITE_ID AND NOT (CONTENT_VALUE IS NULL OR CAST(CONTENT_VALUE AS nvarchar(max)) = '')";
                if (!String.IsNullOrEmpty(pattern))
                {
                    patternStringn = "%" + pattern + "%";
                    SQL += " AND CAST(CONTENT_VALUE AS nvarchar(max)) like @parrent ";
                }
                SQL += " ORDER BY CAST(REPLACE(content_type_name,'PAGE_TEXT_CONTENT_NAME','') AS INTEGER)";

                using (SqlDataReader reader = cnn.ExecuteReader(SQL,
                        new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", s_id),
                        new System.Data.SqlClient.SqlParameter("@parrent", patternStringn)                        
                    }))
                {
                    while (reader.Read())
                    {
                        ct[i] = new content();
                        ct[i].ID = reader["CONTENT_ID"].ToString();
                        ct[i].Name = reader["CONTENT_VALUE"].ToString();
                        ct[i].Number = Convert.ToInt32(reader["CONTENT_NUMBER"].ToString());
                        i++;
                    }
                }
            }
            else
                return "";

            return new JavaScriptSerializer().Serialize(ct);
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string pattern = "";
            try
            {
                pattern = context.Request.QueryString["q"];
            }
            catch (Exception e)
            {
                pattern = e.Message.ToString();
            }

            context.Response.Write(TestJSON(pattern));
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