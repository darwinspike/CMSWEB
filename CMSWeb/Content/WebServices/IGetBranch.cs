using CMSWeb.Models.Consumable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetBranch" in both code and config file together.
    [ServiceContract]
    public interface IGetBranch
    {
        [OperationContract]
        [WebGet(UriTemplate = "AllBranch/{SiteID}", ResponseFormat = WebMessageFormat.Json)]
        List<Branchs.Branch> allbranch(string SiteID);
    }
}
