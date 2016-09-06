using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IgetImage" in both code and config file together.
    [ServiceContract]
    public interface IgetImage
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "CompanyLogo/{SiteID}/")]
        void Logo(string SiteID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ConsultantPhoto/{SiteID}/")]
        void ConsultantPhoto(string SiteID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "BranchPhoto/{SiteID}/")]
        void BranchPhoto(string SiteID);
    }
}
