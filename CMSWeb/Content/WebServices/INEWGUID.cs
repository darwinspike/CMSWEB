using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INEWGUID" in both code and config file together.
    [ServiceContract]
    public interface INEWGUID
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Guid/", ResponseFormat = WebMessageFormat.Json)]
        guidID ViewNewGuid();
    }
}
