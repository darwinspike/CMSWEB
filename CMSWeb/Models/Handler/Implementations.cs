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
    public class Implementations
    {


        #region Global Var
        private static string page = "";
        #endregion

        #region Class

        #region Implementation's
        public class Implementation
        {
            public string ID { get; set; }
            public string SiteID { get; set; }
            public string TemplateName { get; set; }
            public string TemplatePath { get; set; }
            public string TemplateType { get; set; }
            public int TemplateTypeOrder { get; set; }
            public bool Catalogs { get; set; }
            public string Active { get; set; }
            public string CreateDate { get; set; }
            public string UpdateDate { get; set; }
            public string DeleteDate { get; set; }

            public Implementation()
            {
                this.ID = "";
                this.SiteID = "";
                this.TemplateName = "";
                this.TemplatePath = "";
                this.TemplateType = "";
                this.TemplateTypeOrder = 0;
                this.Catalogs = false;
                this.Active = "";
                this.CreateDate = "";
                this.UpdateDate = "";
                this.DeleteDate = "";
            }
        }
        #endregion

        #region CustomPage
        public class CustomPage
        {
            public string CustomPageID { get; set; }
            public string ImplementationID { get; set; }
            public string PageName { get; set; }
            public string Path { get; set; }
            public string Value { get; set; }
            public string Active { get; set; }
            public string CreateDate { get; set; }
            public string UpdateDate { get; set; }
            public string DeleteDate { get; set; }

            public CustomPage()
            {
                this.CustomPageID = "";
                this.ImplementationID = "";
                this.PageName = "";
                this.Path = "";
                this.Value = "";
                this.Active = "";
                this.CreateDate = "";
                this.UpdateDate = "";
                this.DeleteDate = "";
            }
        }
        #endregion

        #endregion

        #region Implementations

        /// <summary>
        /// Return Implementation ID By Current Site 
        /// </summary>
        /// <returns>Implementation ID By Current Site</returns>
        public static string ID()
        {
            return Select.Implement.implemen(MasterGlobal.SiteID()).ID;
        }

        /// <summary>
        /// Return Implementation ID By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Implementation ID By Current Site </returns>
        public static string ID(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return Select.Implement.implemen(SiteID).ID;
            }
            else
            {
                return Guid.Empty.ToString();
            }

        }

        /// <summary>
        /// Return Template Name By Current Site
        /// </summary>
        /// <returns>Template Name By Current Site</returns>
        public static string TemplateName()
        {
            return Select.Implement.implemen(MasterGlobal.SiteID()).TemplateName;
        }

        /// <summary>
        /// Return Template Name By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Template Name By Specific Site ID</returns>
        public static string TemplateName(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return Select.Implement.implemen(SiteID).TemplateName;
            }
            else
            {
                return Guid.Empty.ToString();
            }
        }

        /// <summary>
        /// Return Template type Name By Current Site
        /// </summary>
        /// <returns>Template type By Current Site</returns>
        public static string TemplateType()
        {
            return Select.Implement.implemen(MasterGlobal.SiteID()).TemplateType;
        }

        /// <summary>
        /// Return Template type Name By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Template type By Specific Site ID</returns>
        public static string TemplateType(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return Select.Implement.implemen(SiteID).TemplateType;
            }
            else
            {
                return Guid.Empty.ToString();
            }
        }

        /// <summary>
        /// Return Implementation Order type By Current Site
        /// </summary>
        /// <returns>Implementation Order type By Current Site</returns>
        public static int TemplateTypeOrder()
        {
            return Select.Implement.implemen(MasterGlobal.SiteID()).TemplateTypeOrder;
        }

        /// <summary>
        /// Return Implementation Order type By Specific Site
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Implementation Order type By Specific Site</returns>
        public static int TemplateTypeOrder(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return Select.Implement.implemen(SiteID).TemplateTypeOrder;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Return If Exists An Implementation By Current Site
        /// </summary>
        /// <returns>If Exists An Implementation By Current Site</returns>
        public static bool IfExistsImplementation()
        {
            return !string.IsNullOrEmpty(Select.Implement.implemen(MasterGlobal.SiteID()).ID) ? true : false;
        }
    
        /// <summary>
        /// If Exists An Implementation By Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>If Exists An Implementation By Current Site</returns>
        public static bool IfExistsImplementation(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return !string.IsNullOrEmpty(Select.Implement.implemen(SiteID).ID) ? true : false;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Implemen Load Page

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPages"></param>
        /// <returns></returns>
        public static string LoadPage(string getPages)
        {
            return LP(getPages, false, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPages"></param>
        /// <param name="viewWhere"></param>
        /// <returns></returns>
        public static string LoadPage(string getPages, bool viewWhere)
        {
            return LP(getPages, viewWhere, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPages"></param>
        /// <returns></returns>
        public static string LoanPageInternal(string getPages)
        {
            return LP(getPages, true, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPages"></param>
        /// <param name="viewWhere"></param>
        /// <param name="internalPage"></param>
        /// <returns></returns>
        private static string LP(string getPages, bool viewWhere, bool internalPage)
        {
            string fileSten = viewWhere ? ".ascx" : "";
            switch (TemplateTypeOrder())
            {
                case 1:
                    page = LPMaster(getPages, fileSten, internalPage);
                    break;
                case 2:
                    page = LPCustom(getPages, fileSten);
                    break;
                case 3:
                    // Funcion o
                    break;
                default:
                    // Ereror
                    break;
            }

            return page;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPages"></param>
        /// <param name="fileSten"></param>
        /// <param name="internalP"></param>
        /// <returns></returns>
        private static string LPMaster(string getPages, string fileSten, bool internalP)
        {
            string errorPage = "./ErrorPage.ascx";
            string SiteID = MasterGlobal.SiteID();

            try
            {
                string pa = internalP ? "./" + getPages.ToLower() + fileSten : Path.TemplatePath() + getPages.ToLower() + fileSten;
                page = pa;
            }
            catch (Exception)
            {
                page = errorPage;
            }
            return page;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPages"></param>
        /// <param name="fileSten"></param>
        /// <returns></returns>
        private static string LPCustom(string getPages, string fileSten)
        {
            string errorPage = "./ErrorPage.ascx";
            string SiteID = MasterGlobal.SiteID();

            if (IfExistsPage(getPages, SiteID))
                page = "~/" + Path.CustomPagePath(getPages, SiteID);
            else
            {
                try
                {
                    page = Path.TemplatePath() + getPages.ToLower() + fileSten;
                }
                catch (Exception)
                {
                    page = errorPage;
                }
            }
            return page;
        }

        #endregion

        #region CustomPage

        /// <summary>
        /// Return If Exists An Custom Page By Current Site
        /// </summary>
        /// <returns>If Exists An Implementation By Current Site</returns>
        public static bool IfExistsCustomPage()
        {
            return !string.IsNullOrEmpty(Select.Implement.cusPage(MasterGlobal.SiteID(), "").CustomPageID) ? true : false;
        }   

        /// <summary>
        /// Return If Exists An Implementation By Specific Site
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Return If Exists An Implementation By Specific Site</returns>
        public static bool IfExistsCustomPage(string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return !string.IsNullOrEmpty(Select.Implement.cusPage(SiteID, "").CustomPageID) ? true : false;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Return If Exists An Implementation By Current Site
        /// </summary>
        /// <param name="PageName">Page Name</param>
        /// <returns></returns>
        public static bool IfExistsPage(string PageName)
        {
            return !string.IsNullOrEmpty(Select.Implement.cusPage(MasterGlobal.SiteID(), PageName).CustomPageID) ? true : false;
        }

        /// <summary>
        /// Return If Existes Page to Custom Page By Specific Site 
        /// </summary>
        /// <param name="PageName">Page Name</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>If Existes Page to Custom Page By Specific Site</returns>
        public static bool IfExistsPage(string PageName, string SiteID)
        {
            string erro = MasterGlobal.ValidGuidIDError(SiteID);
            if (String.IsNullOrEmpty(erro))
            {
                return !string.IsNullOrEmpty(Select.Implement.cusPage(SiteID, PageName).CustomPageID) ? true : false;
            }
            else
            {
                return false;
            }
        }


        #endregion


    }
}