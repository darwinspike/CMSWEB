using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections;

using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System.Data.SqlClient;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for ContentEditor
    /// </summary>
    public class ContentEditor : IHttpHandler
    {

        #region Global Var
        //Generic
        private static Connections cnn = new Connections();
        private static string sql = "";
        private static string KeyWord = "";
        private static string ContentIDVal = "";
        private static string TitleTags = "";
        private static string BodyTags = "";


        // Use Class
        private static Errors error = new Errors();
        #endregion

        private string contentAll(string SiteID)
        {
            try
            {
                int contentCount = ContentBuilders.CountCustomPageBySiteID(SiteID);
                if (contentCount > 0)
                {
                    List<ContentBuilders.ContentEditor> content = new List<ContentBuilders.ContentEditor>();
                    string KeyWord = "";
                    ArrayList arrayKeyWord = new ArrayList();
                    sql = @"SELECT 
                            CC.ID AS CustomContentID, CC.ContentID AS ContentID, CC.Title AS TitleContent, CC.Body, CC.Active AS ActiveContent, 
                            CONVERT(varchar(10),CC.CreateDate,121) AS CreateDate, CONVERT(varchar(10),CC.UpdateDate,121) AS UpdateDate, CONVERT(varchar(10),CC.DeleteDate,121) AS DeleteDate, 
                            CCS.ID, CCS.Title AS TitleSEO, CCS.[Description] AS Des, CCS.Keyword
                            FROM tblCustomContent AS CC INNER JOIN tblCustomContentSEO AS CCS ON CC.ID = CCS.CustomContentID
                            WHERE CC.SiteID = @SITEID";
                    using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@SITEID", SiteID)                                
                               }))
                    {
                        while (reader.Read())
                        {
                            KeyWord = "";
                            KeyWord = reader["Keyword"].ToString();
                            if (!string.IsNullOrEmpty(KeyWord))
                            {
                                arrayKeyWord = new ArrayList();
                                string[] words = KeyWord.Split(',');
                                foreach (string word in words)
                                {
                                    arrayKeyWord.Add(word.Trim());
                                }
                            }

                            TitleTags = "";
                            TitleTags = MultiTags.multiTagsClear(reader["TitleContent"].ToString(), SiteID);
                            BodyTags = "";
                            BodyTags = MultiTags.multiTagsClear(reader["Body"].ToString(), SiteID);

                            content.Add(new ContentBuilders.ContentEditor()
                            {
                                title = TitleTags,
                                body = BodyTags,
                                id = reader["ContentID"].ToString(),
                                seo = (new ContentBuilders.ContentEditorSeo()
                                {
                                    title = reader["TitleSEO"].ToString(),
                                    description = reader["Des"].ToString(),
                                    keywords = arrayKeyWord
                                }),
                                Active = reader["ActiveContent"].ToString(),
                                created_at = reader["CreateDate"].ToString(),
                                updated_at = reader["UpdateDate"].ToString(),
                                deleted_at = reader["DeleteDate"].ToString()
                            });
                        }
                    }

                    return new JavaScriptSerializer().Serialize(content);
                }
                else
                {
                    error.error = true;
                    error.error_message = "Not";
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

        private string content(string id, string SiteID)
        {
            try
            {
                int contentCount = ContentBuilders.CountCustomPageBySiteID(SiteID);
                if (contentCount > 0)
                {
                    ContentBuilders.ContentEditor contents = new ContentBuilders.ContentEditor();
                    try
                    {
                        KeyWord = "";
                        ArrayList arrayKeyWord = new ArrayList();
                        string sql = @"SELECT 
                                        CC.ID AS CustomContentID, CC.ContentID AS ContentID, CC.Title AS TitleContent, CC.Body, CC.Active AS ActiveContent, 
                                        CONVERT(varchar(10),CC.CreateDate,121) AS CreateDate, CONVERT(varchar(10),CC.UpdateDate,121) AS UpdateDate, CONVERT(varchar(10),CC.DeleteDate,121) AS DeleteDate, 
                                        CCS.ID, CCS.Title AS TitleSEO, CCS.[Description] AS Des, CCS.Keyword
                                        FROM tblCustomContent AS CC INNER JOIN tblCustomContentSEO AS CCS ON CC.ID = CCS.CustomContentID
                                        WHERE CC.Active = 1 AND CC.SiteID = @SITEID AND CC.ContentID = @id";
                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@SITEID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@id", id)
                               }))
                        {
                            if (reader.Read())
                            {
                                KeyWord = reader["Keyword"].ToString();
                                if (!string.IsNullOrEmpty(KeyWord))
                                {
                                    string[] words = KeyWord.Split(',');
                                    foreach (string word in words)
                                    {
                                        arrayKeyWord.Add(word);
                                    }
                                }

                                contents.title = MultiTags.multiTagsClear(reader["TitleContent"].ToString(), SiteID);
                                contents.body = MultiTags.multiTagsClear(reader["Body"].ToString(), SiteID);
                                contents.id = reader["ContentID"].ToString();
                                contents.seo = (new ContentBuilders.ContentEditorSeo()
                                {
                                    title = reader["TitleSEO"].ToString(),
                                    description = reader["Des"].ToString(),
                                    keywords = arrayKeyWord
                                });
                                contents.Active = reader["ActiveContent"].ToString();
                                contents.created_at = reader["CreateDate"].ToString();
                                contents.updated_at = reader["UpdateDate"].ToString();
                                contents.deleted_at = reader["DeleteDate"].ToString();
                            }
                            else
                            {
                                contents.error = "Content Not Exists";
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    return new JavaScriptSerializer().Serialize(contents);
                }
                else
                {
                    error.error = true;
                    error.error_message = "Content Not";
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

        private string upDateContent(ContentBuilders.ContentEditor json, string SiteID)
        {
            KeyWord = "";
            if (json.seo.keywords.Count > 0)
            {
                for (int y = 0; json.seo.keywords.Count > y; y++)
                {
                    if ((json.seo.keywords.Count - 1) != y)
                        KeyWord += json.seo.keywords[y].ToString().Trim() + ",";
                    else
                        KeyWord += json.seo.keywords[y].ToString().Trim();
                }
            }
            else
                KeyWord = "";

            ContentIDVal = String.IsNullOrEmpty(json.id) ? String.Empty : json.id;
            if (!ContentBuilders.IfExitContent(json.id, SiteID))
            {
                #region Insert New Content
                Guid customID = Guid.NewGuid();
                int contentID = ContentBuilders.CountCustomPageBySiteID(SiteID) + 1;
                sql = "";
                sql = "INSERT INTO tblCustomContent (ID, SiteID, ContentID, Title, Body, Active) VALUES(@ID, @SiteID, @ContentID, @Title, @Body, @Active)";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                   new System.Data.SqlClient.SqlParameter[] {
                                   new System.Data.SqlClient.SqlParameter("@ID", customID),
                                   new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                   new System.Data.SqlClient.SqlParameter("@ContentID", contentID),
                                   new System.Data.SqlClient.SqlParameter("@Title", json.title),
                                   new System.Data.SqlClient.SqlParameter("@Body", json.body),
                                   new System.Data.SqlClient.SqlParameter("@Active", 1)
                                })) { };

                sql = "";
                sql = "INSERT INTO tblCustomContentSEO (ID, CustomContentID,Title,[Description],Keyword) VALUES(@ID, @CustomContentID,@Title,@Description,@Keyword)";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", Guid.NewGuid()),
                                   new System.Data.SqlClient.SqlParameter("@CustomContentID", customID),
                                   new System.Data.SqlClient.SqlParameter("@Title", json.seo.title),
                                   new System.Data.SqlClient.SqlParameter("@Description", json.seo.description),
                                   new System.Data.SqlClient.SqlParameter("@Keyword", KeyWord)
                                })) { };
                #endregion
            }
            else
            {
                #region Update Content
                string SeoID = ContentBuilders.SeoIDByContentID(json.id, SiteID);
                sql = "";
                sql = "UPDATE tblCustomContent SET Title = @Title, Body = @Body, UpdateDate = @UpdateDate WHERE SiteID = @SiteID AND ContentID = @ContentID";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                   new System.Data.SqlClient.SqlParameter("@ContentID", json.id),
                                   new System.Data.SqlClient.SqlParameter("@Title", json.title),
                                   new System.Data.SqlClient.SqlParameter("@Body", json.body),
                                   new System.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now)
                                })) { };
                sql = "";
                sql = "UPDATE tblCustomContentSEO SET Title = @Title, [Description] = @Description, Keyword = @Keyword WHERE ID = @ID";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                   new System.Data.SqlClient.SqlParameter[] { 
                                   new System.Data.SqlClient.SqlParameter("@ID", SeoID),
                                   new System.Data.SqlClient.SqlParameter("@Title", json.seo.title),
                                   new System.Data.SqlClient.SqlParameter("@Description", json.seo.description),
                                   new System.Data.SqlClient.SqlParameter("@Keyword", KeyWord)
                                })) { };

                #endregion
            }
            return new JavaScriptSerializer().Serialize(error);
        }

        private string desactiveContent(string id, string SiteID, string active)
        {
            try
            {
                int contentCount = ContentBuilders.CountCustomPageBySiteID(SiteID);
                if (contentCount > 0)
                {
                    ContentBuilders.ContentEditor contents = new ContentBuilders.ContentEditor();
                    try
                    {
                        string sql = @"UPDATE tblCustomContent set Active = @active, DeleteDate = @DeleteDate WHERE SiteID = @SITEID AND ContentID = @id";
                        using (SqlDataReader reader = cnn.ExecuteReader(sql,
                               new System.Data.SqlClient.SqlParameter[] { 
                                new System.Data.SqlClient.SqlParameter("@DeleteDate", DateTime.Now),
                                new System.Data.SqlClient.SqlParameter("@SITEID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@active", active),
                                new System.Data.SqlClient.SqlParameter("@id", id)
                               }))
                        { }
                    }
                    catch
                    {
                        throw;
                    }

                    return new JavaScriptSerializer().Serialize(error);
                }
                else
                {
                    error.error = true;
                    error.error_message = "Content Not";
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

        public void ProcessRequest(HttpContext context)
        {
            string Method = (!string.IsNullOrEmpty(context.Request.HttpMethod) ? context.Request.HttpMethod : "GET");
            string SiteID = "";
            string ContentID = "";

            if (Method == "POST")
            {
                SiteID = (!string.IsNullOrEmpty(context.Request.Form["SiteID"]) ? context.Request.Form["SiteID"] : "");
                context.Response.ContentType = "application/json";
                if (string.IsNullOrEmpty(context.Request.QueryString["Active"]))
                {
                    ContentBuilders.ContentEditor cn = new ContentBuilders.ContentEditor();
                    cn.title = (!string.IsNullOrEmpty(context.Request.Form["title"]) ? context.Request.Form["title"] : "");
                    cn.body = (!string.IsNullOrEmpty(context.Request.Form["body"]) ? context.Request.Form["body"] : "");
                    cn.id = (!string.IsNullOrEmpty(context.Request.Form["id"]) ? context.Request.Form["id"] : String.Empty);
                    cn.seo = new JavaScriptSerializer().Deserialize<ContentBuilders.ContentEditorSeo>(context.Request.Form["seo"].ToString());
                    context.Response.Write(upDateContent(cn, SiteID));
                }
                else
                {
                    ContentID = (!string.IsNullOrEmpty(context.Request.QueryString["id"]) ? context.Request.QueryString["id"] : String.Empty);
                    if (!string.IsNullOrEmpty(ContentID))
                        context.Response.Write(desactiveContent(context.Request.QueryString["id"], SiteID, context.Request.QueryString["Active"]));
                    else
                        context.Response.Write(new JavaScriptSerializer().Serialize(error));
                }
            }
            else
            {
                SiteID = (!string.IsNullOrEmpty(context.Request.QueryString["SiteID"]) ? context.Request.QueryString["SiteID"] : String.Empty);
                context.Response.ContentType = "application/json";
                ContentID = (!string.IsNullOrEmpty(context.Request.QueryString["id"]) ? context.Request.QueryString["id"] : String.Empty);
                if (string.IsNullOrEmpty(ContentID))
                    context.Response.Write(contentAll(SiteID));
                else
                    context.Response.Write(content(context.Request.QueryString["id"], SiteID));
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