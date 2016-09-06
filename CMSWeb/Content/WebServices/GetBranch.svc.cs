using CMSWeb.Models.Consumable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetBranch" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetBranch.svc or GetBranch.svc.cs at the Solution Explorer and start debugging.
    public class GetBranch : IGetBranch
    {

        public List<Branchs.Branch> allbranch(string SiteID)
        {
            List<Branchs.Branch> lbh = new List<Branchs.Branch>();

            return lbh;        
        }
    }
}
