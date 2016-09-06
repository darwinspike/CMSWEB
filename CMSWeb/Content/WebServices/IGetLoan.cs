using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CMSWeb.Models.Consumable;

namespace CMSWeb.Content.WebServices
{ 
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetLoan" in both code and config file together.
    [ServiceContract]
    public interface IGetLoan
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ViewAllConsultant/{SiteID}/", ResponseFormat = WebMessageFormat.Json)]
        List<Consultants.Consultant> ViewAllLoanOfficer(string SiteID);
      
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ViewConsultant/{ConsultantID}", ResponseFormat = WebMessageFormat.Json)]
        Consultants.Consultant ViewLoanOfficer(string ConsultantID); 

        [OperationContract] 
        [WebInvoke(Method = "GET", UriTemplate = "ViewAllConsultantByBranch/{BranchID}/", ResponseFormat = WebMessageFormat.Json)]
        List<Consultants.Consultant> ViewAllLoanOfficerByBranch(string BranchID);
        
    }
}
