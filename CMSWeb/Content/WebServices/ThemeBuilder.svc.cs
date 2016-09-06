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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ThemeBuilder" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ThemeBuilder.svc or ThemeBuilder.svc.cs at the Solution Explorer and start debugging.
    public class ThemeBuilder : IThemeBuilder
    {

        private static Errors error = new Errors();

        public List<ThemeBuilders.ThemeBuilder> information(string SiteID)
        {
            List<ThemeBuilders.ThemeBuilder> ltb = new List<ThemeBuilders.ThemeBuilder>();
            int totalTheme = 0;
            string catalogID = ThemeBuilders.ThemeBuilderCatalogID(SiteID);
            if (!String.IsNullOrEmpty(catalogID))
            {
                totalTheme = ThemeBuilders.CountThemeBuilderallByCatalogID(catalogID);

                if (totalTheme > 0)
                {
                    ltb = Select.Theme.information(catalogID);
                }
            }


            return ltb;
        }

        public string update(List<ThemeBuilders.ThemeBuilder> content, string SiteID)
        {
            try
            {
                Guid guidTem;

                if (content.Count > 0)
                {
                    string catalogID = ThemeBuilders.ThemeBuilderCatalogID(SiteID);
                    if (!String.IsNullOrEmpty(catalogID))
                    {
                        List<string> themeBuilder = new List<string>();
                        List<string> themeProperties = new List<string>();
                        foreach (var item in content)
                        {
                            #region Insert or update Theme
                            Guid NGID = Guid.NewGuid();
                            if (!ThemeBuilders.CountThemeAllByThemeBuilderID(item.id))
                            {
                                Insert.ThemeBuil.Theme(catalogID, NGID.ToString());   
                            }


                            if (ThemeBuilders.CountThemeBuilderByID(item.id) > 0)
                            {
                                Update.ThemeBuil.ThemeBuilder(item);
                            }
                            else
                            {
                                Insert.ThemeBuil.ThemeBuilder(item, NGID.ToString());
                            }
                            themeBuilder.Add(item.id);



                            if (item.variables.Count > 0)
                            {
                                foreach (ThemeBuilders.ThemeProperties item2 in item.variables)
                                {
                                    int Properties = !String.IsNullOrEmpty(item2.id) ? ThemeBuilders.CountThemePropertyByID(item2.id) : 0;
                                    if (Properties > 0)
                                    {
                                        Update.ThemeBuil.ThemeProperties(item2);
                                        themeProperties.Add(item2.id);
                                    }
                                    else
                                    {
                                        guidTem = Guid.NewGuid();
                                        Insert.ThemeBuil.ThemeBuilderProperties(item2, item.id.ToString(), guidTem.ToString());
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
            string sql = "";
            #region Delete the items menu (Property)
            try
            {
                if (properties.Count > 0)
                {

                    sql = "";
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
                    Delete.ThemeBuil.ThemeProperties(catalogID, sql);
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
                    Delete.ThemeBuil.ThemeBuilder(catalogID, sql);

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

                    for (k = 0; theme.Count > k; k++)
                    {
                        if (k == (theme.Count - 1))
                            sql += "'" + theme[k] + "'";
                        else
                            sql += "'" + theme[k] + "',";
                    }
                    Delete.ThemeBuil.Theme(catalogID, sql);
                }
            }
            catch { }

            #endregion
        }



    }
}
