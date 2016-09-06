using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMSWeb.Models;
using CMSWeb.Models.Consumable;
using CMSWeb.Models.Data;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System.Data.SqlClient;


namespace CMSWeb.Models.Handler
{
    public class URL
    {

        #region globals var
        private static string result = "";
        private static string path = Path.TemplatePath();
        private static string siteID;
        public static string urlDirs, siteName, complement, sql, name, standar;
        public static string optionUnsecure = "true";
        #endregion

        #region Template

        /// <summary>
        /// Return Link By Current Site 
        /// </summary>
        /// <returns>URL By Current Site</returns>
        public static string GetLink()
        {
            string ReturnServer = HttpContext.Current.Request.ApplicationPath + (HttpContext.Current.Request.ApplicationPath.Length > 1 ? "/" : "") + "?s=" + GetVarURL();
            if ((HttpContext.Current.Session != null && HttpContext.Current.Session["RetUrl"] != null) && (HttpContext.Current.Session["RetUrl"].ToString().Length > 0)) ReturnServer = HttpContext.Current.Session["RetUrl"].ToString();
            return ReturnServer;
        }

        /// <summary>
        /// Return Unsecure Production Link By Current Site
        /// </summary>
        /// <returns>Unsecure Production Link By Current Site</returns>
        public static string UnsecureClientPost()
        {
            return MasterGlobal.GeneralParameter("UnsecureClientPost");
        }

        /// <summary>
        /// Return Secure Production Link By Current Site
        /// </summary>
        /// <returns>Secure Production Link By Current Site</returns>
        public static string SecureClientPost()
        {
            return MasterGlobal.GeneralParameter("SecureClientPost");
        }

        /// <summary>
        /// Return Value By Specific Type Get Variable
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Value By Specific Type Get Variable</returns>
        public static string GetVarURL(string v = "s") {
            string svar = !String.IsNullOrEmpty(HttpContext.Current.Request[v]) ? HttpContext.Current.Request[v] : String.Empty;
            return svar;
        }

        /// <summary> 
        /// Return Domin Name From Admin CRM
        /// </summary>
        /// <returns>Domin Name</returns>
        public static string getHostAdmin()
        {
            return getDNSURL("crm");
        }

        /// <summary>
        /// Return Domin Name From Cliente Site
        /// </summary>
        /// <returns>Domin Name</returns>
        public static string getHostClient()
        {
            return getDNSURL("client");
        }

        // getDNSURL
        private static string getDNSURL(string crm)
        {
            string result = String.Empty;
            Uri url = HttpContext.Current.Request.Url;
            string ReturnServer = url.Host.ToString() + (!String.IsNullOrEmpty(url.Port.ToString()) ? ":" + url.Port.ToString() : "");

            // QA Enveroment
            string QAServerAdmin = "http://admin.encompasscrm.com";
            string QAServerClient = "http://cs-qa.encompasscrm.com";
            // Relauch Enveroment
            string RelauchServerAdmin = "http://eq1vwbcrm04.dco.elmae";
            string RelauchServerClient = "http://eq1vwbcrm03.dco.elmae";
            // Dev Enveroment
            string DevServerAdmin = "http://10.112.113.201:9090";
            string DevServerClient = "http://10.112.113.202";
            // New Dev Envermoment
            string NewDevServerAdmin = "http://10.112.113.201:9696";
            string NewDevServerClient = "http://10.112.113.201:9797";
            // Local Everoment
            string LocalServerAdmin = "http://localhost:4247";
            string LocalServerClient = "http://localhost:14064";
            // Production Enveroment
            string ProdServerAdmin = MasterGlobal.GeneralParameter("CRMAdminURL"); //"admin.encompasscrm.com";
            string ProdServerClient = MasterGlobal.GeneralParameter("CRMClientSiteURL");//"secure.encompasscrm.com";

            switch (ReturnServer)
            {
                // QA Enveroment
                case "admin.encompasscrm.com":
                    result = crm == "crm" ? QAServerAdmin : QAServerClient;
                    break;
                case "cs-qa.encompasscrm.com":
                    result = crm == "crm" ? QAServerAdmin : QAServerClient;
                    break;
                // Relauch Enveroment
                case "eq1vwbcrm04.dco.elmae:80":
                    result = crm == "crm" ? RelauchServerAdmin : RelauchServerClient;
                    break;
                case "eq1vwbcrm03.dco.elmae:80":
                    result = crm == "crm" ? RelauchServerAdmin : RelauchServerClient;
                    break;
                // Dev Enveroment
                case "10.112.113.201:9090":
                    result = crm == "crm" ? DevServerAdmin : DevServerClient;
                    break;
                case "10.112.113.202":
                    result = crm == "crm" ? DevServerAdmin : DevServerClient;
                    break;
                // New Dev Envermoment
                case "10.112.113.201:9696":
                    result = crm == "crm" ? NewDevServerAdmin : NewDevServerClient;
                    break;
                case "10.112.113.201:9797":
                    result = crm == "crm" ? NewDevServerAdmin : NewDevServerClient;
                    break;
                // Local Everoment
                case "localhost:4247":
                    result = crm == "crm" ? LocalServerAdmin : LocalServerClient;
                    break;
                case "localhost:14064":
                    result = crm == "crm" ? LocalServerAdmin : LocalServerClient;
                    break;
                // Production Enveroment
                default:
                    result = crm == "crm" ? RelauchServerAdmin : RelauchServerClient;
                    break;
            }



            return result;
        }

        #endregion

        #region Free Url
        /// <summary>
        /// Return Free Redirection
        /// </summary>
        /// <param name="valor">String</param>
        public static void RedirectionByURL(string valor)
        {
            HttpContext.Current.Response.Redirect(valor);
        }
        #endregion

        #region Pull 
        /// <summary>
        /// Redirect from the parameter sent
        /// </summary>
        public static void pull()
        {
            pulls("", "");
        }

        /// <summary>
        /// Redirect from the parameter sent
        /// </summary>
        /// <param name="parameters">
        /// Parameter complement (Example. &otherVar=Value) 
        /// Parameter Option (varp 'add get var p' or true)
        /// </param>
        public static void pull(params string[] parameters)
        {
            string complement = parameters.Length > 0 ? parameters[0] : "";
            string options = parameters.Length > 1 ? parameters[1] : "";
            pulls("", "", complement, options);
        }

        /// <summary>
        /// Redirect from the parameter sent
        /// </summary>
        /// <param name="type">Parameter Type (id or name of site)</param>
        /// <param name="value">Parameter Value (value ID or Name of site)</param>
        public static void pull(string type, string value)
        {
            pulls(type, value);
        }

        /// <summary>
        /// Redirect from the parameter sent
        /// </summary>
        /// <param name="type">Parameter Type (id or name of site)</param>
        /// <param name="value">Parameter Value (value ID or Name of site)</param>
        /// <param name="complement">Parameter complement (Example. &otherVar=Value)</param>
        public static void pull(string type, string value, string complement)
        {
            pulls(type, value, complement);
        }

        /// <summary>
        /// Redirect from the parameter sent
        /// </summary>
        /// <param name="type">Parameter Type (id or name of site)</param>
        /// <param name="value">Parameter Value (value ID or Name of site)</param>
        /// <param name="complement">Parameter complement (Example. &otherVar=Value)</param>
        /// <param name="option">Parameter Option (varp 'add get var p' or true)</param>
        public static void pull(string type, string value, string complement, string option)
        {
            pulls(type, value, complement, option);
        }

        /// <summary>
        /// Redirect from the parameter sent
        /// </summary>
        public static void pulls(params string[] parameters)
        {
            #region parameter
            string[] varSplit;
            string type = parameters.Length > 0 ? parameters[0] : "";
            bool value = parameters.Length > 1 ? true : false;
            string com = parameters.Length > 2 ? parameters[2] : "";
            string option = parameters.Length > 3 ? parameters[3] : "";
            #endregion

            #region validation for kwen the parameter
            if (string.IsNullOrEmpty(type))
                type = "";

            if (string.IsNullOrEmpty(com))
                com = "";
            #endregion

            #region option separation
            varSplit = option.Split(',');
            if (varSplit.Length > 1)
            {
                if (varSplit[0] == "varp")
                    com = "&p=" + HttpContext.Current.Request.QueryString["p"] + com;

                if (varSplit[0] == "true")
                    optionUnsecure = "";

                if (varSplit[1] == "true")
                    optionUnsecure = "";
            }
            else
            {
                for (int i = 0; i < varSplit.Length; i++)
                {
                    if (varSplit[0] == "varp")
                    {
                        com = "&p=" + HttpContext.Current.Request.QueryString["p"] + com;
                    }
                    if (varSplit[i] == "true")
                    {
                        optionUnsecure = "";
                    }

                }

            }
            #endregion

            name = Select.Companies.corporateSiteName(type, value);
            pullAll(name, com, optionUnsecure);

        }

        /// <summary>
        /// Return Corporate Site Name By Any Type Of Search 
        /// </summary>
        private static void pullAll(params string[] parameters)
        {

            string value = parameters.Length > 0 ? parameters[0] : "";
            string com = parameters.Length > 1 ? parameters[1] : "";
            string option = parameters.Length > 2 ? parameters[2] : "";

            if (option != "") { standar = SecureClientPost(); } else { standar = UnsecureClientPost(); }
            if (!string.IsNullOrEmpty(com))
            {
                complement = com;
            }
            else
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["p"]))
                {
                    complement = "&p=" + HttpContext.Current.Request.QueryString["p"];
                }
            }
            urlDirs = string.Format("{0}/?s={1}{2}", standar, value, complement);
            HttpContext.Current.Response.Redirect(urlDirs);
        }

        #endregion

    }
}