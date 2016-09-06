using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using CMSWeb.Models;
using CMSWeb.Models.Tools;
using System.Collections;
using System.Data.SqlClient;

using CMSWeb.Models.Consumable;
using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for ThemeBuilder
    /// </summary>
    public class ThemeBuilder : IHttpHandler
    {



        #region Global Var
        //Generic
        private static Connections cnn = new Connections();
        private static string sql = "";

        private static int totalTheme = 0;
        private static int totalThemeProperties = 0;

        // Use Class
        private static Errors error = new Errors();
        #endregion

        private string information(string SiteID)
        {
            try
            {
                string catalogID = ThemeBuilders.ThemeBuilderCatalogID(SiteID);
                if (!String.IsNullOrEmpty(catalogID))
                {
                    ThemeBuilders.ThemeBuilder Theme = new ThemeBuilders.ThemeBuilder();

                    totalTheme = ThemeBuilders.CountThemeBuilderallByCatalogID(catalogID);

                    if (totalTheme > 0)
                    {
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
                                    Theme.id = reader["ThemeBuiderID"].ToString().Trim();
                                    Theme.advanced = Convert.ToBoolean(reader["Advanced"].ToString().Trim());
                                    Theme.baseColor = reader["BaseColor"].ToString().Trim();
                                    Theme.stylesheet = reader["StyleSheet"].ToString().Trim();
                                    Theme.modifydate = reader["ModifyDate"].ToString().Trim();
                                    Theme.stylefile = reader["StyleFile"].ToString().Trim();
                                    Theme.filetype = reader["FileType"].ToString().Trim();
                                    Theme.Active = Convert.ToBoolean(reader["ActiveTheme"].ToString().Trim());
                                    Theme.createData = reader["CreateTheme"].ToString().Trim();
                                    Theme.deleteData = reader["DeleteThemme"].ToString().Trim();

                                    totalThemeProperties = ThemeBuilders.CountThemePropertiesAll(reader["ThemeBuiderID"].ToString().Trim());
                                }
                                if (totalThemeProperties > 0)
                                {
                                    Theme.variables.Add(new ThemeBuilders.ThemeProperties()
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
                                    BuilderID = reader["ThemeBuiderID"].ToString().Trim();

                            }
                        }
                        return new JavaScriptSerializer().Serialize(Theme);
                    }
                    else
                    {
                        return new JavaScriptSerializer().Serialize(Theme);
                    }
                }
                else
                {
                    error.error = true;
                    error.error_message = "Catalogo Not";
                    return new JavaScriptSerializer().Serialize(error);

                }
            }
            catch (Exception ex)
            {

                error.error = true;
                error.error_message = ex.Message;
                return new JavaScriptSerializer().Serialize(error);
            }

        }

        private static string upDateInformation(string json, string SiteID)
        {
            try
            {
                Guid guidTem;


                ThemeBuilders.contentTheme content = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ThemeBuilders.contentTheme>(json);

                if (content.Content.Count > 0)
                {
                    string catalogID = ThemeBuilders.ThemeBuilderCatalogID(SiteID);
                    if (!String.IsNullOrEmpty(catalogID))
                    {
                        List<string> themeBuilder = new List<string>();
                        List<string> themeProperties = new List<string>();
                        foreach (var item in content.Content)
                        {
                            #region Insert or update Theme
                            Guid NGID = Guid.NewGuid();
                            if (!ThemeBuilders.CountThemeAllByThemeBuilderID(item.id))
                            {
                                sql = "";
                                sql = "INSERT INTO tblTheme (ID,CatalogToolID, ThemeBuilderID, Active) VALUES (@ID, @CatalogToolID,@ThemeBuilderID,@Active)";
                                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", Guid.NewGuid()),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@CatalogToolID", catalogID),
                                   new System.Data.SqlClient.SqlParameter("@ThemeBuilderID", NGID)
                                })) { };
                            }


                            if (ThemeBuilders.CountThemeBuilderByID(item.id) > 0)
                            {
                                sql = "";
                                sql = "UPDATE tblThemeBuilder SET Advanced = @Advanced, BaseColor = @BaseColor, StyleSheet = @StyleSheet, UpdateDate = @ModifyDate, FileType = @FileType, StyleFile = @StyleFile WHERE ID = @ID";
                                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", item.id),
                                   new System.Data.SqlClient.SqlParameter("@Advanced", item.advanced),
                                   new System.Data.SqlClient.SqlParameter("@BaseColor", item.baseColor),
                                   new System.Data.SqlClient.SqlParameter("@StyleSheet", item.stylesheet),
                                   new System.Data.SqlClient.SqlParameter("@ModifyDate", DateTime.Now),
                                   new System.Data.SqlClient.SqlParameter("@StyleFile", item.stylefile),
                                   new System.Data.SqlClient.SqlParameter("@FileType", item.filetype)
                                })) { };

                            }
                            else
                            {
                                sql = "";
                                sql = @"INSERT INTO tblThemeBuilder (ID, BaseColor, FileType, StyleFile, StyleSheet, Advanced, Active) Values(@ID, @BaseColor, @FileType, @StyleFile,  @StyleSheet, @Advanced, @Active) ";


                                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", NGID),
                                   new System.Data.SqlClient.SqlParameter("@Advanced", item.advanced),
                                   new System.Data.SqlClient.SqlParameter("@BaseColor", item.baseColor),
                                   new System.Data.SqlClient.SqlParameter("@StyleSheet", item.stylesheet),
                                   new System.Data.SqlClient.SqlParameter("@ModifyDate", DateTime.Now),
                                   new System.Data.SqlClient.SqlParameter("@StyleFile", item.stylefile),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@FileType", item.filetype)
                                })) { };
                            }
                            themeBuilder.Add(item.id);



                            if (item.variables.Count > 0)
                            {
                                foreach (ThemeBuilders.ThemeProperties item2 in item.variables)
                                {
                                    int Properties = !String.IsNullOrEmpty(item2.id) ? ThemeBuilders.CountThemePropertyByID(item2.id) : 0;
                                    if (Properties > 0)
                                    {
                                        sql = "";
                                        sql = "UPDATE tblThemeProperties SET Name = @Name, [Key] = @Key, Value = @Value, UpdateDate = @UpdateDate WHERE ID = @ID;";
                                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                        new System.Data.SqlClient.SqlParameter[] { 
                                        new System.Data.SqlClient.SqlParameter("@ID", item2.id),
                                        new System.Data.SqlClient.SqlParameter("@Name", item2.name),
                                        new System.Data.SqlClient.SqlParameter("@Key", item2.key),
                                        new System.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now),
                                        new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                        })) { };
                                        themeProperties.Add(item2.id);
                                    }
                                    else
                                    {
                                        guidTem = Guid.NewGuid();
                                        sql = "";
                                        sql = "INSERT INTO tblThemeProperties (ID, ThemeBuilderID, Name, [Key], Value, Active) VALUES (@ID, @ThemeBuilderID, @Name, @Key, @Value, @Active)";
                                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                        new System.Data.SqlClient.SqlParameter[] { 
                                            new System.Data.SqlClient.SqlParameter("@ID", guidTem),
                                            new System.Data.SqlClient.SqlParameter("@ThemeBuilderID", item.id),
                                            new System.Data.SqlClient.SqlParameter("@Name", item2.name),
                                            new System.Data.SqlClient.SqlParameter("@Key", item2.key),
                                            new System.Data.SqlClient.SqlParameter("@Active", 1),
                                            new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                            })) { };
                                        themeProperties.Add(guidTem.ToString());
                                    }
                                }
                            }
                            #endregion
                        }

                        deleteInformation(themeBuilder, themeProperties, catalogID);

                        error.error = false;
                        error.error_message = "";
                        return new JavaScriptSerializer().Serialize(error);
                    }
                    else
                    {
                        error.error = true;
                        error.error_message = "Theme Not";
                        return new JavaScriptSerializer().Serialize(error);
                    }
                }

            }
            catch (Exception ex)
            {
                error.error = true;
                error.error_message = ex.Message;
                return new JavaScriptSerializer().Serialize(error);
            }

            return new JavaScriptSerializer().Serialize(error);
        }

        private static void deleteInformation(List<string> theme, List<string> properties, string catalogID)
        {
            int j = 0;
            int i = 0;
            int k = 0;
            #region Delete the items menu (Property)
            try
            {
                if (properties.Count > 0)
                {
                    sql = "";
                    sql = @"UPDATE ttp SET Active = @Active, DeleteDate = @DeleteDate FROM tblThemeProperties AS ttp INNER JOIN tblThemeBuilder AS ttb ON ttp.ThemeBuilderID = ttb.ID INNER JOIN tblTheme AS tt ON tt.ThemeBuilderID = ttp.ID WHERE ttb.ID NOT IN (";
                    for (j = 0; properties.Count > j; j++)
                    {
                        if (j == (properties.Count - 1))
                        {
                            sql += "'" + properties[j] + "'";
                        }
                        else
                        {
                            sql += "'" + properties[j] + "',";
                        }
                    }
                    sql += ") AND tt.CatalogToolID = @catalogID AND ttp.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@Active", 0),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       })) { };
                }
            }
            catch { }
            #endregion

            #region Delete the items menu
            try
            {
                if (theme.Count > 0)
                {
                    sql = "";
                    sql = @"UPDATE ttb SET ttb.Active = @Active, ttb.DeleteDate = @DeleteDate FROM tblThemeBuilder AS ttb INNER JOIN tblTheme AS tt ON tt.ThemeBuilderID = ttb.ID WHERE ttb.ID NOT IN (";
                    for (i = 0; theme.Count > i; i++)
                    {
                        if (i == (theme.Count - 1))
                        {
                            sql += "'" + theme[i] + "'";
                        }
                        else
                        {
                            sql += "'" + theme[i] + "',";
                        }
                    }
                    sql += ") AND tt.CatalogToolID = @catalogID AND ttb.Active = 1 ";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@Active", 0),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       })) { };
                }
            }
            catch { }

            #endregion

            #region Delete the menu
            try
            {
                if (theme.Count > 0)
                {
                    sql = "";
                    sql = @"UPDATE tblTheme SET Active = @Active, DeleteDate = @DeleteDate WHERE ThemeBuilderID NOT IN (";
                    for (k = 0; theme.Count > k; k++)
                    {
                        if (k == (theme.Count - 1))
                            sql += "'" + theme[k] + "'";
                        else
                            sql += "'" + theme[k] + "',";
                    }
                    sql += ") and CatalogToolID = @catalogID and Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@Active", 0),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       })) { };
                }
            }
            catch { }

            #endregion
        }



        public void ProcessRequest(HttpContext context)
        {
            string Method = (!string.IsNullOrEmpty(context.Request.HttpMethod) ? context.Request.HttpMethod : "GET");
            string SiteID = "";
            string values = "";

            if (Method == "POST")
            {
                SiteID = (!string.IsNullOrEmpty(context.Request.Form["SiteID"]) ? context.Request.Form["SiteID"] : "");
                values = (!string.IsNullOrEmpty(context.Request.Form["Content"]) ? context.Request.Form["Content"] : " nada 1 ");
                context.Response.ContentType = "application/json";
                context.Response.Write(upDateInformation(values, SiteID));
            }
            else
            {
                SiteID = (!string.IsNullOrEmpty(context.Request.QueryString["SiteID"]) ? context.Request.QueryString["SiteID"] : String.Empty);
                context.Response.ContentType = "application/json";
                context.Response.Write(information(SiteID));
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