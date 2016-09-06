using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMSWeb.Models.Data;



namespace CMSWeb.Models.Consumable
{
    public class MasterGlobal 
    {

        #region globals var
        public static string SendEmailAddress = "info@arginteractive.com";
        public static string SendEmailName = "ARG InterActive";
        private static string siteID;
        #endregion

        #region Site ID

        /// <summary>
        /// Return Site ID By Specific Environment
        /// </summary>
        /// <param name="corporate"></param>
        /// <returns>Site ID</returns>
        public static string SiteID(bool corporate = true)
        {
            if (!String.IsNullOrEmpty(HttpContext.Current.Request["s"]))
            {
                string s = HttpContext.Current.Request["s"];

                if (corporate)
                    siteID = Select.MastersGlobal.CorporativeSiteID(s);
                else
                    siteID = Select.MastersGlobal.SiteID(s);
            }
            else
            {
                siteID = Guid.Empty.ToString();
            }
            return siteID;
        }

        /// <summary>
        /// Return Site ID By Specific Site Name
        /// </summary>
        /// <param name="name">Site Name</param>
        /// <returns>Site ID By Specific Site Name</returns>
        public static string SiteIDBySiteName(string name)
        {
            return Select.MastersGlobal.SiteIDBySiteName(name);
        }

        /// <summary>
        /// Return If It's Valid Guid ID
        /// </summary>
        /// <param name="id">Guid ID</param>
        /// <returns>If It's Valid Guid</returns>
        public static string ValidGuidID(string id)
        {
            return ID(id)[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ValidGuidIDError(string id)
        {
            return ID(id)[1];
        }

        //returnVID
        private static string[] ID(string id)
        {
            string[] values = new string[2];
            string ids = "00000000-0000-0000-0000-000000000000";
            if (!String.IsNullOrEmpty(id))
            {
                if (id.Length == 36)
                {
                    values[0] = id;
                    values[1] = "";
                }
                else
                {
                    values[0] = ids;
                    values[1] = "No Valid ID";
                }
            }
            else
            {
                values[0] = ids;
                values[1] = "Empty ID";

            }
            return values;
        }

        #endregion

        /// <summary>
        /// Return General Paramenter By Specific Server Name
        /// </summary>
        /// <param name="serverName">Server Name</param>
        /// <returns>General Paramenter By Specific Server Name</returns>
        public static string GeneralParameter(string serverName)
        {
            string serverURL = Select.MastersGlobal.GeneralParameter(serverName);
            return serverURL;
        }


    }
}