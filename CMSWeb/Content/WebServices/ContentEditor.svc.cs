using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CMSWeb.Models.Tools;
using CMSWeb.Models.Data;
using System.Collections;
using System.Web;


namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContentEditor" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ContentEditor.svc or ContentEditor.svc.cs at the Solution Explorer and start debugging.
    public class ContentEditor : IContentEditor
    {

        public List<ContentBuilders.ContentEditor> ContentAll(string SiteID)
        {
            List<ContentBuilders.ContentEditor> content = new List<ContentBuilders.ContentEditor>();
            try
            {
                content = Select.ContentBuil.ContentAll(SiteID);                
            }
            catch (Exception ex)
            {

            }
            return content;
        }

        public ContentBuilders.ContentEditor Content(string id, string SiteID)
        {
            ContentBuilders.ContentEditor content = new ContentBuilders.ContentEditor();
            try
            {
                content = Select.ContentBuil.ContentAll(SiteID)[0];
            }
            catch (Exception ex)
            {

            }
            return content;
        }

        public bool desactiveContent(string SiteID, string id, string active)
        {
            try
            {
                int contentCount = ContentBuilders.CountCustomPageBySiteID(SiteID);
                if (contentCount > 0)
                {
                    bool re = Convert.ToBoolean(Update.ContentBuil.UpdateCustomContent(SiteID, id, active));
                    return re;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string upDateContent(ContentBuilders.ContentEditor contentJson, string SiteID)
        {
            try
            {
                string ContentIDVal = String.IsNullOrEmpty(contentJson.id) ? String.Empty : contentJson.id;
                string keywords = "";

                if (!ContentBuilders.IfExitContent(contentJson.id, SiteID))
                {
                    #region Insert New Content
                    contentJson.id = Guid.NewGuid().ToString();
                    contentJson.NumberContent = Convert.ToString(ContentBuilders.CountCustomPageBySiteID(SiteID) + 1);
                    contentJson.seo.id = Guid.NewGuid().ToString();
                    contentJson.seo.customContentID = contentJson.id;
                    for (int x = 0; x < contentJson.seo.keywords.Count; x++)
                    {
                        if (x == contentJson.seo.keywords.Count)
                            keywords += contentJson.seo.keywords[x];
                        else
                            keywords += contentJson.seo.keywords[x] + ",";
                    }

                    Insert.ContentBuil.CustomContent(contentJson, SiteID, keywords);

                    #endregion
                }
                else
                {
                    #region Update Content
                    contentJson.seo.id = ContentBuilders.SeoIDByContentID(contentJson.id, SiteID);
                    for (int x = 0; x < contentJson.seo.keywords.Count; x++)
                    {
                        if (x == contentJson.seo.keywords.Count)
                            keywords += contentJson.seo.keywords[x];
                        else
                            keywords += contentJson.seo.keywords[x] + ",";
                    }

                    Update.ContentBuil.CustomContent(contentJson, SiteID, keywords);
                    #endregion
                }
                return "Exito";
            }
            catch (Exception e)
            {
                return "Error: " + e.Message.ToString();
            }
        }


    }
}
