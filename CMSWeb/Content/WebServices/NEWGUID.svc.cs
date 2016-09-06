using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NEWGUID" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select NEWGUID.svc or NEWGUID.svc.cs at the Solution Explorer and start debugging.
    public class NEWGUID : INEWGUID
    {

        public guidID ViewNewGuid()
        {
            return new guidID(); 
        }
    }

    [DataContract]
    public class guidID
    {
        [DataMember]
        public Guid NewGUID { get; set; }
        [DataMember]
        public string error { get; set; }

        public guidID()
        {
            this.NewGUID = Guid.NewGuid();
            this.error = "";
        }
    }

}
