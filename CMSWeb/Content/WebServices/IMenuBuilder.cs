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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenuBuilder" in both code and config file together.
    [ServiceContract]
    public interface IMenuBuilder
    {

        [OperationContract]
        [WebGet(UriTemplate = "AllMenu/{SiteID}", ResponseFormat = WebMessageFormat.Json)]
        List<MenuBuilders.MenuBuil> information(string SiteID);

        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            UriTemplate = "UpdateMenu/",  
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string upDateInformation(List<MenuBuilders.MenuBuil> content, string SiteID);    

    }
}
