using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMSWeb.Models.Data
{

    public class Update
    {
        private static Connections cnn = new Connections();
        private static string sql = "";
        private static int result = 0;
        #region Tools

        public class ContentBuil
        {
            public static int UpdateCustomContent(string SiteID, string id, string active)
            {
                try
                {
                    sql = @"UPDATE tblCustomContent set Active = @active, DeleteDate = @DeleteDate WHERE SiteID = @SITEID AND ContentID = @id";
                     result = cnn.ExecuteNonQuery(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now),
                                new System.Data.SqlClient.SqlParameter("@SITEID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@active", active),
                                new System.Data.SqlClient.SqlParameter("@id", id)
                               });

                    return result;
                
                }
                catch
                {
                    throw;
                }            
            }

            public static int CustomContent(ContentBuilders.ContentEditor contentJson, string SiteID, string keywords)
            {

                sql = "";
                sql = "UPDATE tblCustomContent SET Title = @Title, Body = @Body, UpdateDate = @UpdateDate WHERE SiteID = @SiteID AND ContentID = @ContentID";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                   new System.Data.SqlClient.SqlParameter("@ContentID", contentJson.id),
                                   new System.Data.SqlClient.SqlParameter("@Title", contentJson.title),
                                   new System.Data.SqlClient.SqlParameter("@Body", contentJson.body),
                                   new System.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now)
                                });
                sql = "";
                sql = "UPDATE tblCustomContentSEO SET Title = @Title, [Description] = @Description, Keyword = @Keyword WHERE ID = @ID";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", contentJson.seo.id),
                                   new System.Data.SqlClient.SqlParameter("@Title", contentJson.seo.title),
                                   new System.Data.SqlClient.SqlParameter("@Description", contentJson.seo.description),
                                   new System.Data.SqlClient.SqlParameter("@Keyword", keywords)
                                });



                return result;
            }
        }

        public class MenuBuil
        {

            public static int MenuBuilder(MenuBuilders.MenuBuil item)
            {
                sql = "";
                sql = "UPDATE tblMenuBuilder SET MenuName = @MenuName, MenuType = @MenuType, UpdateDate = @ModifyDate, Published = @Published WHERE ID = @ID";
                result = cnn.ExecuteNonQuery(sql,
                    new System.Data.SqlClient.SqlParameter[] { 
                    new System.Data.SqlClient.SqlParameter("@MenuName", item.name),
                    new System.Data.SqlClient.SqlParameter("@MenuType", item.type),
                    new System.Data.SqlClient.SqlParameter("@ModifyDate", DateTime.Now),
                    new System.Data.SqlClient.SqlParameter("@Published", item.published),
                    new System.Data.SqlClient.SqlParameter("@ID", item.id)
                });


                return result;
            }

            public static int MenuBuilderProperties(MenuBuilders.MenuProperties item2, string id)
            {
                sql = "";
                sql = "UPDATE tblMenuProperties SET [Index] = @Index, PropertieName = @Name, OriginalTitle = @OriginalTitle, ParentID = @ParentID, ShowBranch = @ShowBranch, ShowCorporate = @ShowCorporate, ShowLo = @ShowLo, TargetBlank = @TargetBlank, [Type] = @Type, URL = @URL, Value = @Value , UpdateDate = @UpdateDate WHERE ID = @ID";
                result = cnn.ExecuteNonQuery(sql,
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
                                           new System.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now),
                                           new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                        });
                return result;            
            }

        }

        public class ThemeBuil
        {

            public static int ThemeBuilder(ThemeBuilders.ThemeBuilder item)
            {

                sql = "";
                sql = "UPDATE tblThemeBuilder SET Advanced = @Advanced, BaseColor = @BaseColor, StyleSheet = @StyleSheet, UpdateDate = @ModifyDate, FileType = @FileType, StyleFile = @StyleFile WHERE ID = @ID";
                result = cnn.ExecuteNonQuery(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", item.id),
                                   new System.Data.SqlClient.SqlParameter("@Advanced", item.advanced),
                                   new System.Data.SqlClient.SqlParameter("@BaseColor", item.baseColor),
                                   new System.Data.SqlClient.SqlParameter("@StyleSheet", item.stylesheet),
                                   new System.Data.SqlClient.SqlParameter("@ModifyDate", DateTime.Now),
                                   new System.Data.SqlClient.SqlParameter("@StyleFile", item.stylefile),
                                   new System.Data.SqlClient.SqlParameter("@FileType", item.filetype)
                                });
                return result;            
            }

            public static int ThemeProperties(ThemeBuilders.ThemeProperties item2)
            {

                sql = "";
                sql = "UPDATE tblThemeProperties SET Name = @Name, [Key] = @Key, Value = @Value, UpdateDate = @UpdateDate WHERE ID = @ID;";
                result = cnn.ExecuteNonQuery(sql,
                new System.Data.SqlClient.SqlParameter[] { 
                                        new System.Data.SqlClient.SqlParameter("@ID", item2.id),
                                        new System.Data.SqlClient.SqlParameter("@Name", item2.name),
                                        new System.Data.SqlClient.SqlParameter("@Key", item2.key),
                                        new System.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now),
                                        new System.Data.SqlClient.SqlParameter("@Value", item2.value)
                                        });
                return result;
            
            }
        
        }


        #endregion


    }


}