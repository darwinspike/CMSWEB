using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMSWeb.Models.Data
{
    public class Insert
    {

        private static Connections cnn = new Connections();
        private static string sql = "";
        private static int result = 0;

        #region Tools

        public class ContentBuil
        {
            public static int CustomContent(ContentBuilders.ContentEditor contentJson, string SiteID, string keywords)
            { 
                sql = "";
                sql = "INSERT INTO tblCustomContent (ID, SiteID, ContentID, Title, Body, Active) VALUES(@ID, @SiteID, @ContentID, @Title, @Body, @Active)";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] {
                                   new System.Data.SqlClient.SqlParameter("@ID", contentJson.id),
                                   new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                   new System.Data.SqlClient.SqlParameter("@ContentID", contentJson.NumberContent),
                                   new System.Data.SqlClient.SqlParameter("@Title", contentJson.title),
                                   new System.Data.SqlClient.SqlParameter("@Body", contentJson.body),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1)
                                });

                sql = "";
                sql = "INSERT INTO tblCustomContentSEO (ID, CustomContentID,Title,[Description],Keyword) VALUES(@ID, @CustomContentID,@Title,@Description,@Keyword)";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", contentJson.seo.id),
                                   new System.Data.SqlClient.SqlParameter("@CustomContentID", contentJson.seo.customContentID),
                                   new System.Data.SqlClient.SqlParameter("@Title", contentJson.seo.title),
                                   new System.Data.SqlClient.SqlParameter("@Description", contentJson.seo.description),
                                   new System.Data.SqlClient.SqlParameter("@Keyword", keywords)
                                })) { };




                return result;
            }
        }

        public class MenuBuil
        {
             
            public static int Menu(string catalogID, string id)
            {
                
                sql = "";
                sql = "INSERT INTO tblMenu (ID,CatalogToolID,MenuBuilderID, Active) VALUES (@NewGuidID,@CatalogID,@MenuID,@Active)";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@NewGuidID", Guid.NewGuid()),
                                   new System.Data.SqlClient.SqlParameter("@CatalogID", catalogID),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@MenuID", id)
                                });

                return result;
            }

            public static int MenuBuilder(MenuBuilders.MenuBuil item)
            {

                sql = "";
                sql = "INSERT INTO tblMenuBuilder (ID,MenuType,MenuName,Published,Active) Values(@ID,@MenuType,@MenuName,@Published,@Active)";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@MenuName", item.name),
                                   new System.Data.SqlClient.SqlParameter("@MenuType", item.type),
                                   new System.Data.SqlClient.SqlParameter("@Published", item.published),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@ID", item.id)
                                });

                return result;            
            }

            public static int MenuBuilderPropertie(MenuBuilders.MenuProperties item2, string id)
            {


                                        //Insert items from menu builder
                                        sql = "";
                                        sql = @"INSERT INTO tblMenuProperties (ID, MenuBuilderID, ParentID, PropertieName, OriginalTitle, URL, [Type], Value, [Index],TargetBlank,ShowCorporate, ShowBranch, ShowLO, Active) values(@ID,@MenuBuilderID,@ParentID,@Name,@OriginalTitle,@URL,@Type,@Value,@Index,@TargetBlank,@ShowCorporate,@ShowBranch,@ShowLo,@Active)";
                                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                                           new System.Data.SqlClient.SqlParameter[] { 
                                           new System.Data.SqlClient.SqlParameter("@ID", item2.id),
                                           new System.Data.SqlClient.SqlParameter("@Index", item2.index),
                                           new System.Data.SqlClient.SqlParameter("@MenuBuilderID", id),
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



                return result;
            }
        }

        public class ThemeBuil
        {

            public static int Theme(string catalogID, string NGID)
            {
                sql = "";
                sql = "INSERT INTO tblTheme (ID,CatalogToolID, ThemeBuilderID, Active) VALUES (@ID, @CatalogToolID,@ThemeBuilderID,@Active)";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", Guid.NewGuid()),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@CatalogToolID", catalogID),
                                   new System.Data.SqlClient.SqlParameter("@ThemeBuilderID", NGID)
                                });
                return result;
            
            }

            public static int ThemeBuilder(ThemeBuilders.ThemeBuilder item, string NGID)
            {

                sql = "";
                sql = @"INSERT INTO tblThemeBuilder (ID, BaseColor, FileType, StyleFile, StyleSheet, Advanced, Active) Values(@ID, @BaseColor, @FileType, @StyleFile,  @StyleSheet, @Advanced, @Active) ";


                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", NGID),
                                   new System.Data.SqlClient.SqlParameter("@Advanced", item.advanced),
                                   new System.Data.SqlClient.SqlParameter("@BaseColor", item.baseColor),
                                   new System.Data.SqlClient.SqlParameter("@StyleSheet", item.stylesheet),
                                   new System.Data.SqlClient.SqlParameter("@ModifyDate", DateTime.Now),
                                   new System.Data.SqlClient.SqlParameter("@StyleFile", item.stylefile),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1),
                                   new System.Data.SqlClient.SqlParameter("@FileType", item.filetype)
                                });
                return result;            
            }

            public static int ThemeBuilderProperties(ThemeBuilders.ThemeProperties item2, string id, string guidTem)
            {
                sql = "";
                sql = "INSERT INTO tblThemeProperties (ID, ThemeBuilderID, Name, [Key], Value, Active) VALUES (@ID, @ThemeBuilderID, @Name, @Key, @Value, @Active)";
                result = cnn.ExecuteNonQuery(sql,
                new System.Data.SqlClient.SqlParameter[] { 
                                            new System.Data.SqlClient.SqlParameter("@ID", guidTem),
                                            new System.Data.SqlClient.SqlParameter("@ThemeBuilderID", id),
                                            new System.Data.SqlClient.SqlParameter("@Name", item2.name),
                                            new System.Data.SqlClient.SqlParameter("@Key", item2.key),
                                            new System.Data.SqlClient.SqlParameter("@Active", 1),
                                            new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                            });
                return result;
            }
        }

        #endregion



    }
}