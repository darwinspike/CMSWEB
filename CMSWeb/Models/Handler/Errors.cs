using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSWeb.Models.Handler
{
    public class Errors
    {

        public bool error { get; set; }
        public string error_message { get; set; }

        public Errors()
        {
            this.error = false;
            this.error_message = "";
        }


    }
}