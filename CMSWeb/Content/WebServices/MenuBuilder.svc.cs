using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MenuBuilder" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MenuBuilder.svc or MenuBuilder.svc.cs at the Solution Explorer and start debugging.
    public class MenuBuilder : IMenuBuilder
    {
        private static Errors error = new Errors();

        public List<MenuBuilders.MenuBuil> information(string SiteID)
        {
            List<MenuBuilders.MenuBuil> lmb = new List<MenuBuilders.MenuBuil>();

            string catalogID = MenuBuilders.CatalogID(SiteID);
            lmb = Select.Menu.information(catalogID);

            return lmb;
        }

        /// <summary>
        /// Update And Delete Information To Menu Builder According Serialization json Received By Specific ID
        /// </summary>
        /// <param name="json">Serialization jSON</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Status</returns>
        public string upDateInformation(List<MenuBuilders.MenuBuil> content, string SiteID)
        {
            try
            {
                if (content.Count > 0)
                {
                    string catalogID = MenuBuilders.CatalogID(SiteID).ToUpper();
                    if (!String.IsNullOrEmpty(catalogID))
                    {
                        List<string> allMenu = new List<string>();
                        List<string> allItems = new List<string>();
                        foreach (var item in content)
                        {
                            #region Insert or update Menu
                            //Insert Menu 
                            if (!MenuBuilders.CountMenuAllByMenuBuilderID(item.id))
                            {
                                Insert.MenuBuil.Menu(catalogID, item.id);
                            }
                            #endregion

                            #region Update or update menu buider
                            //Update Menu Builder 
                            if (MenuBuilders.CountMenuBuilderByMenuBuilderID(item.id) > 0)
                            {
                                Update.MenuBuil.MenuBuilder(item);
                            }
                            //Insert Menu Builder
                            else
                            {
                                Insert.MenuBuil.MenuBuilder(item);
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

                                        Update.MenuBuil.MenuBuilderProperties(item2, item.id);
                                    }
                                    else
                                    {
                                        Insert.MenuBuil.MenuBuilderPropertie(item2, item.id);
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

        /// <summary>
        /// Delete Information To Menu Builder By Specific ID
        /// </summary>
        /// <param name="allMenu">List Of Menu</param>
        /// <param name="allItems">List Of Items Menu</param>
        /// <param name="catalogID">Catalog ID</param>
        private void deleteInformation(List<string> allMenu, List<string> allItems, string catalogID)
        {
            string sql = "";
            int i = 0;
            int j = 0;
            #region Delete the items menu (Property)
            try
            {
                if (allItems.Count > 0)
                {
                    sql = "";
                    for (j = 0; allItems.Count > j; j++)
                    {
                        if (j == (allItems.Count - 1))
                            sql += "'" + allItems[j] + "'";
                        else
                            sql += "'" + allItems[j] + "',";
                    }

                    Delete.MenuBuil.Menu(sql, catalogID);
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
                    for (i = 0; allMenu.Count > i; i++)
                    {
                        if (i == (allMenu.Count - 1))
                            sql += "'" + allMenu[i] + "'";
                        else
                            sql += "'" + allMenu[i] + "',";
                    }

                    Delete.MenuBuil.MenuBuilder(sql, catalogID);
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
                    for (i = 0; allMenu.Count > i; i++)
                    {
                        if (i == (allMenu.Count - 1))
                            sql += "'" + allMenu[i] + "'";
                        else
                            sql += "'" + allMenu[i] + "',";
                    }

                    Delete.MenuBuil.MenuBuilderProperties(sql, catalogID);
                }
            }
            catch { }

            #endregion
        }


    }
}
