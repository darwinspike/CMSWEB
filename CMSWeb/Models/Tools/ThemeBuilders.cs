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
using System.ServiceModel;
using System.Runtime.Serialization;

namespace CMSWeb.Models.Tools
{
    public class ThemeBuilders
    {

        #region Global Var
        //Generic
        private static int result = 0;
        private static int theme = 2;
        #endregion

        #region themeBuilder Class's
        [DataContract]
        public class ThemeBuilder
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public bool advanced { get; set; }
            [DataMember]
            public string baseColor { get; set; }
            [DataMember]
            public List<ThemeProperties> variables { get; set; }
            [DataMember]
            public string stylesheet { get; set; }
            [DataMember]
            public string modifydate { get; set; }
            [DataMember]
            public string stylefile { get; set; }
            [DataMember]
            public string filetype { get; set; }
            [DataMember]
            public bool published { get; set; }
            [DataMember]
            public bool Active { get; set; }
            [DataMember]
            public string createData { get; set; }
            [DataMember]
            public string deleteData { get; set; }

            public ThemeBuilder()
            {
                this.id = "";
                this.advanced = false;
                this.baseColor = "";
                this.variables = new List<ThemeProperties>();
                this.stylesheet = "";
                this.modifydate = "";
                this.stylesheet = "";
                this.filetype = "";
                this.stylefile = "";
                this.published = false;
                this.Active = false;
                this.createData = "";
                this.deleteData = "";
            }
        }

        [DataContract]
        public class ThemeProperties
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string ThemeBuilderID { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string key { get; set; }
            [DataMember]
            public string value { get; set; }
            [DataMember]
            public string Active { get; set; }
            [DataMember]
            public string createDate { get; set; }
            [DataMember]
            public string updateDate { get; set; }
            [DataMember]
            public string deleteDate { get; set; }


            public ThemeProperties()
            {
                this.id = "";
                this.ThemeBuilderID = "";
                this.name = "";
                this.key = "";
                this.Active = "";
                this.createDate = "";
                this.updateDate = "";
                this.deleteDate = "";
                this.value = "";
            }
        }

        public class contentTheme
        {
            public List<ThemeBuilder> Content { get; set; }
        }

        #endregion

        #region Catalig Tools ID

        /// <summary>
        /// Return Theme Builder Catalog ID By Currente Site
        /// </summary>
        /// <returns>Theme Builder Catalog ID By Currente Site</returns>
        public static string ThemeBuilderCatalogID()
        {
            return Select.Menu.catID(MasterGlobal.SiteID(), theme);
        }

        /// <summary>
        /// Return Theme Builder Catalog ID By Specific Site ID
        /// </summary>
        /// <param name="siteID">Site ID</param>
        /// <returns>Theme Builder Catalog ID By Specific Site ID</returns>
        public static string ThemeBuilderCatalogID(string siteID)
        {
            return Select.Menu.catID(siteID, theme);
        }

        #endregion

        #region Theme

        /// <summary>
        /// Return Count Theme By Specific Theme ID
        /// </summary>
        /// <param name="ThemeID">Theme ID</param>
        /// <returns>Count Theme By Specific Theme ID</returns>
        public static bool CountThemeAllByThemeBuilderID(string ThemeID)
        {
            bool resu = false;
            try
            {
                resu = (Convert.ToInt32(Select.Theme.CountThemeAllByThemeBuilderID(ThemeID)) > 0 ? true : false);
            }
            catch (Exception)
            {
            }

            return resu;
        }

        /// <summary>
        /// Return Count Theme Builder By Specific Theme ID
        /// </summary>
        /// <param name="ThemeID">Theme ID</param>
        /// <returns>Count Theme Builder By Specific Theme ID</returns>
        public static int CountThemeBuilderByID(string ThemeID) // ThemeBuilderCountID
        {
            try
            {
                result = Convert.ToInt32(Select.Theme.CountThemeBuilderByID(ThemeID));
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return Count Theme Builder By Catalog ID
        /// </summary>
        /// <param name="catalogID">Catalog ID</param>
        /// <returns>Count Theme Builder By Catalog ID</returns>
        public static int CountThemeBuilderallByCatalogID(string catalogID) // ThemeBuilderCountCatalogID
        {
            try
            {
                result = Convert.ToInt32(Select.Theme.CountThemeBuilderallByCatalogID(catalogID));
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return Count Theme Properties By Specific Theme ID 
        /// </summary>
        /// <param name="ThemeID">Theme ID</param>
        /// <returns>Count Theme Properties By Specific Theme ID</returns>
        public static int CountThemePropertiesAll(string ThemeID) // ThemeBuilderCountAllProperties
        {
            try
            {
                result = Convert.ToInt32(Select.Theme.CountThemePropertiesAll(ThemeID));
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return Count Theme Properties By Spefic Theme Propertties ID
        /// </summary>
        /// <param name="ThemePropertyID"></param>
        /// <returns>Count Theme Properties By Spefic Theme Properties ID</returns>
        public static int CountThemePropertyByID(string ThemePropertyID) // ThemeBuilderCountPropertyID
        {
            try
            {
                result = Convert.ToInt32(Select.Theme.CountThemePropertyByID(ThemePropertyID));
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return Count Theeme Properues By Theme Builder ID 
        /// </summary>
        /// <param name="ThemeID">Theme ID</param>
        /// <returns>Count Theeme Properues By Theme Builder ID </returns>
        public static int CountThemePropertyAllByThemeBuilderID(string ThemeID) // ThemeBuilderCountAll
        {
            string ids = MasterGlobal.ValidGuidID(ThemeID);
            string erro = MasterGlobal.ValidGuidIDError(ThemeID);
            string err = String.IsNullOrEmpty(erro) ? "We can't find the information you are try to get" : erro;
            int total = 0;
            if (String.IsNullOrEmpty(erro))
            {
                try
                {
                    total = Convert.ToInt32(Select.Theme.CountThemePropertyAllByThemeBuilderID(ids));
                }
                catch
                {
                    throw;
                }

            }
            else
                total = 0;

            return total;
        }

        /// <summary>
        /// Return String Stylesheet By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>String Stylesheet By Specific Site ID</returns>
        public static string stylesheet(string SiteID) {
            return datas(SiteID).stylesheet;
        }

        private static ThemeBuilder datas(string SiteID)
        {
            ThemeBuilder dat = new ThemeBuilder();
            try
            {
                string catalogID = ThemeBuilderCatalogID(SiteID);
                dat = Select.Theme.datas(catalogID);
            }
            catch { }
            return dat;

        }

        /// <summary>
        /// Return True or False If Exists Theme Builder By Current Site
        /// </summary>
        /// <returns>True or False If Exists Theme Builder By Current Site</returns>
        public static bool ThemeBuiderIsSupported()
        {
            string sql = "";
            bool resu = false;
            string siteID = MasterGlobal.SiteID();
            string re = "";
            try
            {
                resu = Convert.ToBoolean(Select.Theme.ThemeBuiderIsSupported(siteID,theme));
            }
            catch (Exception)
            {
            }

            return resu;
        }

        #endregion

    }
}