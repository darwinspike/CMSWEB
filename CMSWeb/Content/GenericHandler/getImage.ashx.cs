using CMSWeb.Models.Handler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for getImage
    /// </summary>
    public class getImage : IHttpHandler
    {

        private static Connections cnn = new Connections();
        private static string imageDB = "";
        private static string sql = "";
        private static string socialID = "";
        private static string type = "";


        public static void img(string id, int types)
        {
            switch (types)
            {
                case 1:
                    sql = "select top 1 imgPhoto AS Photo from tblConsultants  WITH(NOLOCK) where Consultant_ID = @ID";
                    break;
                case 2:
                    sql = "select top 1 imgPhoto AS Photo from tblBranches  WITH(NOLOCK) where Branch_ID = @ID";
                    break;
                case 3:
                    sql = "select top 1 CLIENT_LOGO AS Photo from tblClientSites  WITH(NOLOCK) where SITE_ID= @ID";

                    break;
                case 4:
                    sql = "SELECT socialImage, socialImageSize, socialImageContentType FROM tblSocialMediaSite WHERE socialID = @ID";
                    break;
            }

            using (SqlDataReader reader = cnn.ExecuteReader(sql,
                    new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@ID", id)
                    }))
            {
                if (reader.Read())
                {
                    HttpContext.Current.Response.Clear();
                    byte[] b = (byte[])reader[0];
                    imageDB = Convert.ToString(reader[0]);
                    HttpContext.Current.Response.ContentType = "image/jpg";// +reader["Photo"].ToString();
                    HttpContext.Current.Response.OutputStream.Write(b, 0, b.Length);
                    HttpContext.Current.Response.End();
                }
            }
        }





        public void ProcessRequest(HttpContext context)
        {
            socialID = !String.IsNullOrEmpty(context.Request.QueryString["ID"]) ? context.Request.QueryString["ID"] : "";
            type = !String.IsNullOrEmpty(context.Request.QueryString["type"]) ? context.Request.QueryString["type"] : "";
            img(socialID, Convert.ToInt16(type));

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