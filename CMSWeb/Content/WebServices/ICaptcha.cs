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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICaptcha" in both code and config file together.
    [ServiceContract]
    public interface ICaptcha
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Captcha/{key}")]
        void ViewCaptcha(string key);

    }
}
