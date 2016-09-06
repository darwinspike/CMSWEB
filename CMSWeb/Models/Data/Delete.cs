using CMSWeb.Models.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSWeb.Models.Data
{
    public class Delete
    {

        private static Connections cnn = new Connections();
        private static string sql = "";
        private static int result = 0;


        #region Tools

        public class MenuBuil
        {

            public static int Menu(string allItems, string catalogID)
            {
                sql = "";
                sql = @"UPDATE tmp Set tmp.Active = '0', tmp.DeleteDate = @DeleteDate FROM tblMenuProperties AS tmp INNER JOIN tblMenuBuilder AS tmb ON tmb.ID = tmp.MenuBuilderID INNER JOIN tblMenu AS tm ON tm.MenuBuilderID = tmb.ID WHERE tmp.ID NOT IN (@allItems) AND tm.CatalogToolID =  @catalogID AND tmp.Active = 1";
                result = cnn.ExecuteNonQuery(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@allItems", allItems),
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       });
                return result;
            }

            public static int MenuBuilder(string allMenu, string catalogID)
            {
                int i;
                sql = "";
                sql = @"UPDATE tmb SET tmb.Active = '0', tmb.DeleteDate = @DeleteDate FROM tblMenuBuilder AS tmb INNER JOIN tblMenu AS tm ON tm.MenuBuilderID = tmb.ID WHERE tmb.ID NOT IN (@allMenu) AND tm.CatalogToolID = @catalogID AND tmb.Active = 1";
                result = cnn.ExecuteNonQuery(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@allMenu", allMenu),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       });

                return result;
            }

            public static int MenuBuilderProperties(string allMenu, string catalogID)
            {
                sql = "";
                sql = @"UPDATE tblMenu SET Active = '0', DeleteDate = @DeleteDate WHERE MenuBuilderID NOT IN (@allMenu) and CatalogToolID = @catalogID AND Active = 1";
                result = cnn.ExecuteNonQuery(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@allMenu", allMenu),
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       });

                return result;
            }

        }

        public class ThemeBuil
        {
             
            public static int ThemeProperties(string catalogID, string properties)
            { 
                    sql = @"UPDATE ttp SET Active = @Active, DeleteDate = @DeleteDate FROM tblThemeProperties AS ttp INNER JOIN tblThemeBuilder AS ttb ON ttp.ThemeBuilderID = ttb.ID INNER JOIN tblTheme AS tt ON tt.ThemeBuilderID = ttp.ID WHERE ttb.ID NOT IN (@properties) AND tt.CatalogToolID = @catalogID AND ttp.Active = 1";
                    result = cnn.ExecuteNonQuery(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@properties", properties),
                           new System.Data.SqlClient.SqlParameter("@Active", 0),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       });

                    return result;
            }

            public static int ThemeBuilder(string catalogID, string theme)
            {
                sql = @"UPDATE ttb SET ttb.Active = @Active, ttb.DeleteDate = @DeleteDate FROM tblThemeBuilder AS ttb INNER JOIN tblTheme AS tt ON tt.ThemeBuilderID = ttb.ID WHERE ttb.ID NOT IN (@theme) AND tt.CatalogToolID = @catalogID AND ttb.Active = 1 ";
                result = cnn.ExecuteNonQuery(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@theme", theme),
                           new System.Data.SqlClient.SqlParameter("@Active", 0),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       });
                return result;
            }

            public static int Theme(string catalogID, string theme)
            {
                sql = @"UPDATE tblTheme SET Active = @Active, DeleteDate = @DeleteDate WHERE ThemeBuilderID NOT IN (@theme) and CatalogToolID = @catalogID and Active = 1";
                result = cnn.ExecuteNonQuery(sql,
                       new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@theme", theme),
                           new System.Data.SqlClient.SqlParameter("@Active", 0),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       });
                return result;
            
            }

        }

        #endregion
    }
}