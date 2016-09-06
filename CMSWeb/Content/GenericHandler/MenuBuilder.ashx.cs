using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


using CMSWeb.Models.Consumable;
using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for MenuBuilder
    /// </summary>
    public class MenuBuilder : IHttpHandler
    {

        #region Global Var
        //Generic
        private static Connections cnn = new Connections();
        private static string sql = "";
        // count var
        private int i = 0;
        private int j = 0;
        private int totalMenuProperty = 0;
        private int totalMenuBuild = 0;
        // Use Class
        private static Errors error = new Errors();
        List<MenuBuilders.MenuBuil> menuBuil = new List<MenuBuilders.MenuBuil>();
        List<MenuBuilders.MenuProperties> MenuProperty = new List<MenuBuilders.MenuProperties>();

        #endregion

        #region Select Information

        /// <summary>
        /// Return Serialize Json With General Information By Especial ID.
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Serialize Json</returns>
        private string information(string SiteID)
        {
            try
            {
                string catalogID = MenuBuilders.CatalogID(SiteID);
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
                        return new JavaScriptSerializer().Serialize(menuBuil);
                    }
                    else
                    {
                        error.error = true;
                        error.error_message = "Information Not Found";
                        return new JavaScriptSerializer().Serialize(error);
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

        #endregion

        //Need Inprutment after apruval
        #region Update and delete Information

        /// <summary>
        /// Update And Delete Information To Menu Builder According Serialization json Received By Specific ID
        /// </summary>
        /// <param name="json">Serialization jSON</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Status</returns>
        private string upDateInformation(string json, string SiteID)
        {
            try
            {
                MenuBuilders.content content = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MenuBuilders.content>(json);
                if (content.Content.Count > 0)
                {
                    string catalogID = MenuBuilders.CatalogID(SiteID).ToUpper();
                    if (!String.IsNullOrEmpty(catalogID))
                    {
                        List<string> allMenu = new List<string>();
                        List<string> allItems = new List<string>();
                        foreach (var item in content.Content)
                        {
                            #region Insert or update Menu
                            //Insert Menu 
                            if (!MenuBuilders.CountMenuAllByMenuBuilderID(item.id))
                            {
                                sql = "";
                                sql = "INSERT INTO tblMenu (ID,CatalogToolID,MenuBuilderID, Active) VALUES (@NewGuidID,@CatalogID,@MenuID,@Active)";
                                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@NewGuidID", Guid.NewGuid()),
                                   new System.Data.SqlClient.SqlParameter("@CatalogID", catalogID),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@MenuID", item.id)
                                })) { };
                            }
                            #endregion

                            #region Update or update menu buider
                            //Update Menu Builder 
                            if (MenuBuilders.CountMenuBuilderByMenuBuilderID(item.id) > 0)
                            {
                                sql = "";
                                sql = "UPDATE tblMenuBuilder SET MenuName = @MenuName, MenuType = @MenuType, UpdateDate = @ModifyDate, Published = @Published WHERE ID = @ID";
                                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@MenuName", item.name),
                                   new System.Data.SqlClient.SqlParameter("@MenuType", item.type),
                                   new System.Data.SqlClient.SqlParameter("@ModifyDate", DateTime.Now),
                                   new System.Data.SqlClient.SqlParameter("@Published", item.published),
                                   new System.Data.SqlClient.SqlParameter("@ID", item.id)
                                })) { };
                            }
                            //Insert Menu Builder
                            else
                            {
                                sql = "";
                                sql = "INSERT INTO tblMenuBuilder (ID,MenuType,MenuName,Published,Active) Values(@ID,@MenuType,@MenuName,@Published,@Active)";
                                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@MenuName", item.name),
                                   new System.Data.SqlClient.SqlParameter("@MenuType", item.type),
                                   new System.Data.SqlClient.SqlParameter("@Published", item.published),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@ID", item.id)
                                })) { };
                            }
                            #endregion

                            allMenu.Add(item.id);
                            #region Update and insert items from menu builder
                            if (item.data.Count > 0)
                            {
                                foreach (MenuBuilders.MenuProperties item2 in item.data)
                                {
                                    if (MenuBuilders.CountMenuPropertyByMenuProperty(item2.id) > 0)
                                    {
                                        //Update items from MenuBuilder
                                        sql = "";
                                        sql = "UPDATE tblMenuProperties SET [Index] = @Index, PropertieName = @Name, OriginalTitle = @OriginalTitle, ParentID = @ParentID, ShowBranch = @ShowBranch, ShowCorporate = @ShowCorporate, ShowLo = @ShowLo, TargetBlank = @TargetBlank, [Type] = @Type, URL = @URL, Value = @Value , UpdateDate = @UpdateDate WHERE ID = @ID";
                                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                           new System.Data.SqlClient.SqlParameter[] { 
                                           new System.Data.SqlClient.SqlParameter("@ID", item2.id),
                                           new System.Data.SqlClient.SqlParameter("@Index", item2.index),
                                           new System.Data.SqlClient.SqlParameter("@MenuBuilderID", item.id),
                                           new System.Data.SqlClient.SqlParameter("@Name", item2.name),
                                           new System.Data.SqlClient.SqlParameter("@OriginalTitle", item2.original_title),
                                           new System.Data.SqlClient.SqlParameter("@ParentID", (!String.IsNullOrEmpty(item2.parent_id) ? item2.parent_id : Guid.Empty.ToString())),
                                           new System.Data.SqlClient.SqlParameter("@ShowBranch", item2.showbranch),
                                           new System.Data.SqlClient.SqlParameter("@ShowCorporate", item2.showcorporate),
                                           new System.Data.SqlClient.SqlParameter("@ShowLo", item2.showlosite),
                                           new System.Data.SqlClient.SqlParameter("@TargetBlank", item2.target),
                                           new System.Data.SqlClient.SqlParameter("@Type", item2.type),
                                           new System.Data.SqlClient.SqlParameter("@URL", item2.url),
                                           new System.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now),
                                           new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                        })) { };
                                    }
                                    else
                                    {
                                        //Insert items from menu builder
                                        sql = "";
                                        sql = @"INSERT INTO tblMenuProperties (ID, MenuBuilderID, ParentID, PropertieName, OriginalTitle, URL, [Type], Value, [Index],TargetBlank,ShowCorporate, ShowBranch, ShowLO, Active) values(@ID,@MenuBuilderID,@ParentID,@Name,@OriginalTitle,@URL,@Type,@Value,@Index,@TargetBlank,@ShowCorporate,@ShowBranch,@ShowLo,@Active)";
                                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                           new System.Data.SqlClient.SqlParameter[] { 
                                           new System.Data.SqlClient.SqlParameter("@ID", item2.id),
                                           new System.Data.SqlClient.SqlParameter("@Index", item2.index),
                                           new System.Data.SqlClient.SqlParameter("@MenuBuilderID", item.id),
                                           new System.Data.SqlClient.SqlParameter("@Name", item2.name),
                                           new System.Data.SqlClient.SqlParameter("@OriginalTitle", item2.original_title),
                                           new System.Data.SqlClient.SqlParameter("@ParentID", (!String.IsNullOrEmpty(item2.parent_id) ? item2.parent_id : Guid.Empty.ToString())),
                                           new System.Data.SqlClient.SqlParameter("@ShowBranch", item2.showbranch),
                                           new System.Data.SqlClient.SqlParameter("@ShowCorporate", item2.showcorporate),
                                           new System.Data.SqlClient.SqlParameter("@ShowLo", item2.showlosite),
                                           new System.Data.SqlClient.SqlParameter("@TargetBlank", item2.target),
                                           new System.Data.SqlClient.SqlParameter("@Type", item2.type),
                                           new System.Data.SqlClient.SqlParameter("@URL", item2.url),
                                           new System.Data.SqlClient.SqlParameter("@Active", 1),
                                           new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                        })) { };
                                    }

                                    allItems.Add(item2.id);
                                }
                            }
                            #endregion
                        }

                        deleteInformation(allMenu, allItems, catalogID);
                        error.error = false;
                        error.error_message = string.Empty;
                    }
                    else
                    {
                        error.error = true;
                        error.error_message = "Information Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                error.error = true;
                error.error_message = ex.Message;
            }

            return new JavaScriptSerializer().Serialize(error);
        }

        #endregion

        /// <summary>
        /// Delete Information To Menu Builder By Specific ID
        /// </summary>
        /// <param name="allMenu">List Of Menu</param>
        /// <param name="allItems">List Of Items Menu</param>
        /// <param name="catalogID">Catalog ID</param>
        private void deleteInformation(List<string> allMenu, List<string> allItems, string catalogID)
        {
            #region Delete the items menu (Property)
            try
            {
                if (allItems.Count > 0)
                {
                    sql = "";
                    sql = @"UPDATE tmp Set tmp.Active = '0', tmp.DeleteDate = @DeleteDate FROM tblMenuProperties AS tmp INNER JOIN tblMenuBuilder AS tmb ON tmb.ID = tmp.MenuBuilderID INNER JOIN tblMenu AS tm ON tm.MenuBuilderID = tmb.ID WHERE tmp.ID NOT IN (";
                    for (j = 0; allItems.Count > j; j++)
                    {
                        if (j == (allItems.Count - 1))
                            sql += "'" + allItems[j] + "'";
                        else
                            sql += "'" + allItems[j] + "',";
                    }
                    sql += ") AND tm.CatalogToolID =  @catalogID AND tmp.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       })) { };
                }
            }
            catch { }
            #endregion

            #region Delete the items menu
            try
            {
                if (allMenu.Count > 0)
                {
                    sql = "";
                    sql = @"UPDATE tmb SET tmb.Active = '0', tmb.DeleteDate = @DeleteDate FROM tblMenuBuilder AS tmb INNER JOIN tblMenu AS tm ON tm.MenuBuilderID = tmb.ID WHERE tmb.ID NOT IN (";
                    for (i = 0; allMenu.Count > i; i++)
                    {
                        if (i == (allMenu.Count - 1))
                            sql += "'" + allMenu[i] + "'";
                        else
                            sql += "'" + allMenu[i] + "',";
                    }
                    sql += ") AND tm.CatalogToolID = @catalogID AND tmb.Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       })) { };
                }
            }
            catch { }

            #endregion

            #region Delete the menu
            try
            {
                if (allMenu.Count > 0)
                {
                    sql = "";
                    sql = @"UPDATE tblMenu SET Active = '0', DeleteDate = @DeleteDate WHERE MenuBuilderID NOT IN (";
                    for (i = 0; allMenu.Count > i; i++)
                    {
                        if (i == (allMenu.Count - 1))
                            sql += "'" + allMenu[i] + "'";
                        else
                            sql += "'" + allMenu[i] + "',";
                    }
                    sql += ") and CatalogToolID = @catalogID AND Active = 1";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                           new System.Data.SqlClient.SqlParameter("@catalogID", catalogID),
                           new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now)
                       })) { };
                }
            }
            catch { }

            #endregion
        }



        public void ProcessRequest(HttpContext context)
        {
            string Method = (!string.IsNullOrEmpty(HttpContext.Current.Request.HttpMethod) ? HttpContext.Current.Request.HttpMethod : "GET");
            string SiteID = "";
            string values = "";
            if (Method == "POST")
            {
                SiteID = (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["SiteID"]) ? HttpContext.Current.Request.Form["SiteID"] : "");
                values = (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Content"]) ? HttpContext.Current.Request.Form["Content"] : " nada 1 ");
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(upDateInformation(values, SiteID));
            }
            else
            {
                SiteID = (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SiteID"]) ? HttpContext.Current.Request.QueryString["SiteID"] : String.Empty);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(information(SiteID));
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