using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CMSWeb.Content.GenericHandler
{
    /// <summary>
    /// Summary description for NEWGUID
    /// </summary>
    public class NEWGUID : IHttpHandler
    {

        private class guidID
        {
            public Guid NewGUID { get; set; }
            public string error { get; set; }

            public guidID()
            {
                this.NewGUID = Guid.NewGuid();
                this.error = "";
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            try
            {
                context.Response.Write(new JavaScriptSerializer().Serialize(new guidID()));
            }
            catch (Exception)
            {
                context.Response.Write(new JavaScriptSerializer().Serialize(new CMSWeb.Models.Handler.Errors()));

            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}