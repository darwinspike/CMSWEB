using CMSWeb.Models.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace CMSWeb.Content.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Captcha" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Captcha.svc or Captcha.svc.cs at the Solution Explorer and start debugging.
    public class Captcha : ICaptcha
    {

        public void ViewCaptcha(string key)
        {
            CaptchaImageSessn2 ci = new CaptchaImageSessn2(key);

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/jpeg";
            HttpContext.Current.Response.BufferOutput = true;
            // Write the image to the response stream in JPEG format.
            ci.Image.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            // Dispose of the CAPTCHA image object.
            ci.Dispose();


        
        }
    }
}
