using CMSWeb.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IThemeBuilder" in both code and config file together.
    [ServiceContract]
    public interface IThemeBuilder
    {
        [OperationContract]
        [WebGet(UriTemplate = "AllTheme/{SiteID}", ResponseFormat = WebMessageFormat.Json)]
        List<ThemeBuilders.ThemeBuilder> information(string SiteID);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "ThemeUpdate/",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest
            )]
        string update(List<ThemeBuilders.ThemeBuilder> content, string SiteID);


    }
}
