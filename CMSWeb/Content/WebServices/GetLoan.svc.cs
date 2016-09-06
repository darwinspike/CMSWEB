using CMSWeb.Models.Consumable;
using CMSWeb.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetLoan" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetLoan.svc or GetLoan.svc.cs at the Solution Explorer and start debugging.
    public class GetLoan : IGetLoan
    {

        public List<Consultants.Consultant> ViewAllLoanOfficer(string SiteID)
        {
            List<Consultants.Consultant> cs = Select.LoanOfficer.consultantDataAll(SiteID);
            return cs;
        }

        public Consultants.Consultant ViewLoanOfficer(string ConsultantID)
        {
            Consultants.Consultant cs = Select.LoanOfficer.consultantData(ConsultantID, 3);
            return cs;
        }

        public List<Consultants.Consultant> ViewAllLoanOfficerByBranch(string BranchID)
        {
            List<Consultants.Consultant> cs = Select.LoanOfficer.consultantDataByBranch(BranchID);
            return cs;
        }
    }
}
