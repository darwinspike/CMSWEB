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
    public class Path
    {
        private static string res = "";

        //Tools Order
        private static int menu = 1;
        private static int theme = 2;

        #region template

        /// <summary>
        /// Return Path By Current Site
        /// </summary>
        /// <returns>Template Path By Current Site</returns>
        public static string GetPath()
        {
            return Select.Implement.implemen(MasterGlobal.SiteID()).TemplatePath;
        }

        /// <summary>
        /// Return Path By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Template Path By Specific Site ID</returns>
        public static string GetPath(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return Select.Implement.implemen(SiteID).TemplatePath;
            }
            else
            {
                return Guid.Empty.ToString();
            }
        }

        /// <summary>
        /// Return Asset Path By Current Site
        /// </summary>
        /// <returns>Asset Path By Current Site</returns>
        public static string GetPathAssets()
        {
            return "";
        }

        /// <summary>
        /// Retrn Template Path By Current Site
        /// </summary>
        /// <returns>Template Path By Specific Site ID</returns>
        public static string TemplatePath()
        {
            Implementations.Implementation data = Select.Implement.implemen(MasterGlobal.SiteID());
            return data.TemplatePath + data.TemplateName;
        }

        /// <summary>
        /// Return Template Path By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Template Path By Specific Site ID</returns>
        public static string TemplatePath(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                Implementations.Implementation data = Select.Implement.implemen(SiteID);
                return data.TemplatePath + data.TemplateName;
            }
            else
            {
                return String.Empty;
            }
        }


        #endregion

        #region CustomPage

        /// <summary>
        /// Return Path Custom Page By Specific Paga and Current Site
        /// </summary>
        /// <param name="PageName">Page Name</param>
        /// <returns>Path Custom Page By Specific Paga and Current Site</returns>
        public static string CustomPagePath(string PageName)
        {
            Implementations.CustomPage cp = Select.Implement.cusPage(MasterGlobal.SiteID(), PageName);
            res = cp.Path + cp.Value;
            return res;
        }

        /// <summary>
        /// Return Path Custom Page By Specific Paga and Site ID
        /// </summary>
        /// <param name="PageName">Page Name</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Path Custom Page By Specific Paga and Site ID</returns>
        public static string CustomPagePath(string PageName, string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                Implementations.CustomPage cp = Select.Implement.cusPage(MasterGlobal.SiteID(), PageName);
                res = cp.Path + cp.Value;
                return res;
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region Path Tools

        #region Menu

        /// <summary>
        /// Return Menu Builder Path By Current Site
        /// </summary>
        /// <returns>Menu Builder Path By Current Site</returns>
        public static string MenuBuilderPath()
        {
            return Select.Paths.toolsPath(menu)[0];
        }

        /// <summary>
        /// Return Menu Builder Folder Name By Current Site
        /// </summary>
        /// <returns>Menu Builder Folder Name By Current Site</returns>
        public static string MenuBuilderFolder()
        {
            return Select.Paths.toolsPath(menu)[1];
        }

        /// <summary>
        /// Return All Menu Builder Folder Path By Current Site
        /// </summary>
        /// <returns>All Menu Folder Path By Current Site</returns>
        public static string MenuBuilderPathAll()
        {
            return URL.getHostClient() + "/" + Select.Paths.toolsPath(menu)[0] + Select.Paths.toolsPath(menu)[1] + "/";
        }

        #endregion

        #region Theme

        /// <summary>
        /// Return Theme Builder Path By Currente Site
        /// </summary>
        /// <returns>Theme Builder Path By Currente Site</returns>
        public static string ThemeBuilderPath()
        {
            return Select.Paths.toolsPath(theme)[0];
        }

        /// <summary>
        /// Return Theme Builder Folder Name By Current Site
        /// </summary>
        /// <returns>Theme Builder Folder Name By Current Site</returns>
        public static string ThemeBuilderFolder()
        {
            return Select.Paths.toolsPath(theme)[1];
        }

        /// <summary>
        /// Return All Theme Builder Folder Path By Current Site
        /// </summary>
        /// <returns>All Theme Builder Folder Path By Current Site</returns>
        public static string ThemeBuilderPathAll()
        {
//            return URL.getHostClient() + "/" + toolsP(theme)[0] + toolsP(theme)[1] + "/";
            return Select.Paths.toolsPath(theme)[0] + Select.Paths.toolsPath(theme)[1] + "/";
        }

        #endregion

        #endregion



    }
}