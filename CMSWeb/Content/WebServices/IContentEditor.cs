using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using CMSWeb.Models.Tools;
using CMSWeb.Models.Handler;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IContentEditor" in both code and config file together.
    [ServiceContract]
    public interface IContentEditor
    {
        [OperationContract]
        [WebGet(UriTemplate = "ContentAll/{SiteID}", ResponseFormat = WebMessageFormat.Json)]
        List<ContentBuilders.ContentEditor> ContentAll(string SiteID);

        [OperationContract]
        [WebGet(UriTemplate = "Content/{SiteID}/{id}", ResponseFormat = WebMessageFormat.Json)]
        ContentBuilders.ContentEditor Content(string id, string SiteID);

        [OperationContract]
        [WebGet(UriTemplate = "ActiveOrDesactiveContent/{SiteID}/{id}/{active}", ResponseFormat = WebMessageFormat.Json)]
        bool desactiveContent(string SiteID, string id, string active);

        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "UpdateContent/", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string upDateContent(ContentBuilders.ContentEditor content, string SiteID);

    }
}
