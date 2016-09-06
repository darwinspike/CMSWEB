using CMSWeb.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "getImage" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select getImage.svc or getImage.svc.cs at the Solution Explorer and start debugging.
    public class getImage : IgetImage
    {

        public void Logo(string SiteID)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.Clear();
            byte[] b = (byte[]) Select.Generic.logos.Logo(SiteID,4);
            HttpContext.Current.Response.ContentType = "image/jpg";
            HttpContext.Current.Response.OutputStream.Write(b, 0, b.Length);
            HttpContext.Current.Response.End();
        }

        public void ConsultantPhoto(string SiteID)
        { 
            Photo(SiteID, 1);
        }

        public void BranchPhoto(string SiteID)
        {
            Photo(SiteID, 2);
        }


        private void Photo(string SiteID, int type)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.Clear();
            byte[] b = (byte[])Select.Generic.logos.Logo(SiteID, type);
            HttpContext.Current.Response.ContentType = "image/jpg";
            HttpContext.Current.Response.OutputStream.Write(b, 0, b.Length);
            HttpContext.Current.Response.End();

        }
    }
}
