﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMSWeb.Models.Handler;


namespace CMSWeb.Template.Master.responsive_template04.Content
{
    public partial class Calculators : System.Web.UI.UserControl
    {

        #region Public Variables
        public string path = "Assets/";
        public string link = URL.GetLink();

        public string newpath = "/Calculators/";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}