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
using System.Runtime.Serialization;



namespace CMSWeb.Models.Tools
{
    public class MenuBuilders
    {

        #region Global Var
        private static string res = "";
        private static int result = 0;
        private static int j = 0;
        private static int menu = 1;
        #endregion

        #region menu class's
        // First Option 
        [DataContract]
        public class MenuBuil
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public string ModifyDate { get; set; }
            [DataMember]
            public bool published { get; set; }
            [DataMember]
            public List<MenuProperties> data { get; set; }
            [DataMember]
            public bool Active { get; set; }
            [DataMember]
            public string CreateData { get; set; }
            [DataMember]
            public string DeleteData { get; set; }

            public MenuBuil()
            {
                this.name = "";
                this.id = "";
                this.type = "";
                this.ModifyDate = "";
                this.published = false;
                this.Active = false;
                this.CreateData = "";
                this.DeleteData = "";
            }
        }

        // Items Option
        [DataContract]
        public class MenuProperties
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string url { get; set; }
            [DataMember]
            public bool target { get; set; }
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public bool showcorporate { get; set; }
            [DataMember]
            public bool showbranch { get; set; }
            [DataMember]
            public bool showlosite { get; set; }
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public string original_title { get; set; }
            [DataMember]
            public string value { get; set; }
            [DataMember]
            public string parent_id { get; set; }
            [DataMember]
            public string[] subtree { get; set; }
            [DataMember]
            public int index { get; set; }
            [DataMember]
            public string MenuBuilderID { get; set; }
            [DataMember]
            public bool Active { get; set; }
            [DataMember]
            public string CreateData { get; set; }
            [DataMember]
            public string UpdateData { get; set; }
            [DataMember]
            public string DeleteData { get; set; }


            public MenuProperties()
            {
                this.name = "";
                this.url = "";
                this.target = false;
                this.id = "";
                this.showcorporate = true;
                this.showbranch = true;
                this.showlosite = true;
                this.type = "";
                this.original_title = "";
                this.value = "0";
                this.parent_id = "";
                this.index = 0;
                this.MenuBuilderID = "";
                this.Active = false;
                this.CreateData = "";
                this.UpdateData = "";
                this.DeleteData = "";

            }


        }

        // Content from Json Menu Builder
        public class content
        {
            public List<MenuBuil> Content { get; set; }
        }

        #endregion

        #region Catalig Tools ID

        /// <summary>
        /// Return Catalog ID By Currente Site
        /// </summary>
        /// <returns></returns>
        public static string CatalogID()
        {
            return Select.Menu.catID(MasterGlobal.SiteID(), menu);
        }

        /// <summary>
        /// Return Catalog ID By Specific Site ID
        /// </summary>
        /// <param name="siteID">Site ID</param>
        /// <returns>Catalog ID By Specific Site ID</returns>
        public static string CatalogID(string siteID)
        {
            return Select.Menu.catID(siteID, menu);
        }

        #endregion

        #region information MenuBuilder and MenuProperty

        /// <summary>
        /// Return ID Menu By Specific Menu Builder ID
        /// </summary>
        /// <param name="MenuBuilderID">Menu Builder ID</param>
        /// <returns>ID Menu</returns>
        public static bool CountMenuAllByMenuBuilderID(string MenuBuilderID) // MenuID
        {
            bool resu = false;
            try
            {
                resu = (Convert.ToInt32(Select.Menu.CountMenuAllByMenuBuilderID(MenuBuilderID)) > 0 ? true : false);
            }
            catch (Exception)
            {
            }

            return resu;
        }

        /// <summary>
        /// Return The Total Of All Menu
        /// </summary>
        /// <returns>The Total Of All Menu</returns>
        public static int CountMenuBuilderAllByCatalogID(string catalogID) // MenuBuilderCountAll
        {
            try
            {
                result = Select.Menu.CountMenuBuilderAllByCatalogID(catalogID);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return If The Menu Exist  
        /// </summary>
        /// <param name="menuBuilder">MenuBuilder ID</param>
        /// <returns>True = 1 or False = 0</returns>
        public static int CountMenuBuilderByMenuBuilderID(string menuBuilder) // MenuBuilderCount
        {
            try
            {
                result = Select.Menu.CountMenuBuilderByMenuBuilderID(menuBuilder);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return If The Menu Exist 
        /// </summary>
        /// <param name="menuBuilder">MenuBuilder ID</param>
        /// <returns>True = 1 or False = 0</returns>
        public static int CountMenuBuilderByMenuType(string CatalogToolID, string type) // MenuBuilderCountType
        {
            try
            {
                result = Select.Menu.CountMenuBuilderByMenuType(CatalogToolID, type);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return If The Menu Exist 
        /// </summary>
        /// <param name="menuBuilder">MenuBuilder ID</param>
        /// <returns>True = 1 or False = 0</returns>
        public static int CountMenuBuilderByMenuTypeAndPublic(string type, string CatalogToolID) // MenuBuilderCountTypePublic
        {
            try
            {
                result = Select.Menu.CountMenuBuilderByMenuTypeAndPublic(type, CatalogToolID);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return Total Items Of a Menu By Specific Menu ID
        /// </summary>
        /// <param name="MenuBuilderID">Specific Menu ID</param>
        /// <returns>Total Items</returns>
        public static int CountMenuPropertyAllByMenuBuilderID(string MenuBuilderID) // MenuBuilderCountPropertyAll
        {
            try
            {
                result = Select.Menu.CountMenuPropertyAllByMenuBuilderID(MenuBuilderID);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Return IF The Property Of Specific Property Menu ID
        /// </summary>
        /// <param name="menuPropertyID">Specific Property Menu ID</param>
        /// <returns>True = 1 or False = 0</returns>
        public static int CountMenuPropertyByMenuProperty(string menuPropertyID) // MenuBuilderCountProperty
        {
            try
            {
                result = Select.Menu.CountMenuPropertyByMenuProperty(menuPropertyID);
            }
            catch (Exception)
            {
            }
            return result;
        }

        #endregion

        #region Menu Builder Show Site

        /// <summary>
        /// Return Header Menu Builder By Current Site
        /// </summary>
        /// <returns>Header Menu Builder By Current Site</returns>
        public static string menuBuilderSiteMap()
        {
            return menuBuilder("header", "", true);
        }

        /// <summary>
        /// Return Header Menu Builder By Current Site
        /// </summary>
        /// <returns>Header Menu Builder By Current Site</returns>
        public static string menuBuilderHeader()
        {
            return menuBuilder("header", "");
        }

        /// <summary>
        /// Return Header Menu Builder By Specific Site ID
        /// </summary>
        /// <returns>Header Menu Builder By Specific Site ID</returns>
        public static string menuBuilderHeader(string SiteID)
        {
            return menuBuilder("header", SiteID);
        }

        /// <summary>
        /// Return Footer Menu Builder By Current Site
        /// </summary>
        /// <returns>Footer Menu Builder By Current Site</returns>
        public static string menuBuilderFooter()
        {
            return menuBuilder("footer", "");
        }

        /// <summary>
        /// Return Footer Menu Builder By Specific Site ID
        /// </summary>
        /// <returns>Footer Menu Builder By Specific Site ID</returns>
        public static string menuBuilderFooter(string SiteID)
        {
            return menuBuilder("footer", SiteID);
        }

        /// <summary>
        /// Return SiderBar Menu Builder By Current Site
        /// </summary>
        /// <returns>SiderBar Menu Builder By Current Site</returns>
        public static string menuBuilderSidebar()
        {
            return menuBuilder("sidebar", "");
        }

        /// <summary>
        /// Return SiderBar Menu Builder By Specific Site ID
        /// </summary>
        /// <returns>SiderBar Menu Builder By Specific Site ID</returns>
        public static string menuBuilderSidebar(string SiteID)
        {
            return menuBuilder("sidebar", SiteID);
        }

        /// <summary>
        /// Return Other Menu Builder By Current Site
        /// </summary>
        /// <returns>Other Menu Builder By Current Site</returns>
        public static string menuBuilderOther()
        {
            return menuBuilder("other", "");
        }

        /// <summary>
        /// Return Other Menu Builder By Specific Site ID
        /// </summary>
        /// <returns>Other Menu Builder By Specific Site ID</returns>
        public static string menuBuilderOther(string SiteID)
        {
            return menuBuilder("other", SiteID);
        }

        // All Menu
        private static string menuBuilder(string type, string SiteID, bool siteMap = false)
        {
            #region Global Var
            string firstClassCSS = "";
            string mmargin1 = ""; string mmargin2 = ""; string mmargin3 = ""; string mmargin4 = ""; string mmargin5 = "";
            string pull = "";
            string menuGeneral = "";
            List<MenuProperties> itemFather = new List<MenuProperties>();
            List<MenuProperties> itemChild = new List<MenuProperties>();
            List<MenuProperties> itemAll = new List<MenuProperties>();
            string siteID = !String.IsNullOrEmpty(SiteID) ? SiteID : MasterGlobal.SiteID();
            List<MenuProperties> allMenu = MenuBuilderPropertyAll(type, siteID);


            if (siteMap)
            {
                firstClassCSS = "id='primaryNav' class='col3' ";
                mmargin1 = " class='mmargin' ";
                mmargin2 = " class='mmargin' ";
            }
            else
            {
                firstClassCSS = "class='nav navbar-nav navbar-right'";
                mmargin2 = " class='dropdown' ";
                mmargin3 = " class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false' ";
                mmargin4 = " class='dropdown-menu' role='menu' ";
                mmargin5 = " <span class='caret'></span> ";
            }
            #endregion

            #region General Menu
            menuGeneral = "<ul " + firstClassCSS + " >" + @"
                        <li id='home'><a href='{{link}}'>Home</a></li>
                        <li id='Li1' mmargin1><a href='{{link}}&amp;content=ApplyNow'>Apply Now</a></li>
                        <li id='Li2' mmargin1><a href='{{link}}&amp;pageid=2&amp;showsidebar=true'>Loan Programs</a></li>
                        <li id='Li3' mmargin2><a href='{{link}}&amp;pageid=10' mmargin3>Resources mmargin5</a>
                            <ul mmargin4>
                                <li><a href='{{link}}&amp;content=Calculators'>Mortgage Calculator</a></li>
                                <li><a href='{{link}}&amp;pageid=12'>Mortgage Checklist</a></li>
                                <li><a href='{{link}}&amp;pageid=14'>Purchase Tips</a></li>
                                <li><a href='{{link}}&amp;pageid=24&amp;showsidebar=true'>Mortgage Glossary</a></li>
                                <li><a href='{{link}}&amp;content=BranchDirectory'>Find an Office</a></li>
                                <li><a href='{{link}}&amp;content=LoanDirectory'>Find a Loan Professional</a></li>
                            </ul>
                        </li>
                        <li id='Li4' mmargin1><a href='{{link}}&amp;pageid=15'>About {%Us%}</a></li>
                        <li id='Li5' mmargin1><a href='{{link}}&amp;content=ContactUsNow'>Contact {%Us%}</a></li>
                        <li id='Li6' mmargin1><a href='{{link}}&amp;content=CareersNow'>Careers</a></li>
                    </ul>";
            #endregion

            #region Created menu or assing default menu
            if (allMenu.Count > 0)
            {

                #region Principal Option
                foreach (MenuProperties items in allMenu)
                {
                    if (items.parent_id.ToString().Trim() == Guid.Empty.ToString())
                    {
                        itemFather.Add(new MenuProperties()
                        {
                            id = items.id,
                            index = items.index,
                            MenuBuilderID = items.MenuBuilderID,
                            name = items.name,
                            original_title = items.original_title,
                            parent_id = items.parent_id,
                            showbranch = items.showbranch,
                            showcorporate = items.showcorporate,
                            showlosite = items.showlosite,
                            target = items.target,
                            type = items.type,
                            url = items.url,
                            value = items.value
                        });

                    }
                    else
                    {
                        itemChild.Add(new MenuProperties()
                        {
                            id = items.id,
                            index = items.index,
                            MenuBuilderID = items.MenuBuilderID,
                            name = items.name,
                            original_title = items.original_title,
                            parent_id = items.parent_id,
                            showbranch = items.showbranch,
                            showcorporate = items.showcorporate,
                            showlosite = items.showlosite,
                            target = items.target,
                            type = items.type,
                            url = items.url,
                            value = items.value
                        });
                    }
                }
                #endregion

                #region Principal and Items
                if (itemFather.Count > 0)
                {
                    int change = 1;
                    int iChange = 0;
                    for (int i = 0; itemFather.Count > i; i++)
                    {
                        if (itemChild.Count > 0)
                        {
                            if (valSite(itemFather[i].showcorporate, itemFather[i].showbranch, itemFather[i].showlosite))
                            {
                                itemAll.Add(new MenuProperties()
                                {
                                    id = itemFather[i].id,
                                    index = itemFather[i].index,
                                    MenuBuilderID = itemFather[i].MenuBuilderID,
                                    name = itemFather[i].name,
                                    original_title = itemFather[i].original_title,
                                    parent_id = itemFather[i].parent_id,
                                    showbranch = itemFather[i].showbranch,
                                    showcorporate = itemFather[i].showcorporate,
                                    showlosite = itemFather[i].showlosite,
                                    target = itemFather[i].target,
                                    type = itemFather[i].type,
                                    url = itemFather[i].url,
                                    value = itemFather[i].value,
                                    subtree = new string[1] { "1" }
                                });


                                for (j = 0; itemChild.Count > j; j++)
                                {
                                    if (itemFather[i].id == itemChild[j].parent_id)
                                    {
                                        itemAll.Add(new MenuProperties()
                                        {
                                            id = itemChild[j].id,
                                            index = itemChild[j].index,
                                            MenuBuilderID = itemChild[j].MenuBuilderID,
                                            name = itemChild[j].name,
                                            original_title = itemChild[j].original_title,
                                            parent_id = itemChild[j].parent_id,
                                            showbranch = itemChild[j].showbranch,
                                            showcorporate = itemChild[j].showcorporate,
                                            showlosite = itemChild[j].showlosite,
                                            target = itemChild[j].target,
                                            type = itemChild[j].type,
                                            url = itemChild[j].url,
                                            value = itemChild[j].value,
                                            subtree = new string[1] { "2" }
                                        });
                                        // itemChild.RemoveAt(j);
                                        change++;
                                        iChange++;
                                    }
                                }
                                if (change == 1)
                                    itemAll[iChange].subtree[0] = "0";
                                else
                                    change = 1;

                                iChange++;

                            }
                        }
                        else
                        {
                            itemAll.Add(new MenuProperties()
                            {
                                id = itemFather[i].id,
                                index = itemFather[i].index,
                                MenuBuilderID = itemFather[i].MenuBuilderID,
                                name = itemFather[i].name,
                                original_title = itemFather[i].original_title,
                                parent_id = itemFather[i].parent_id,
                                showbranch = itemFather[i].showbranch,
                                showcorporate = itemFather[i].showcorporate,
                                showlosite = itemFather[i].showlosite,
                                target = itemFather[i].target,
                                type = itemFather[i].type,
                                url = itemFather[i].url,
                                value = itemFather[i].value,
                                subtree = new string[1] { "0" }
                            });
                        }
                    }
                }
                #endregion

                #region Menu
                if (itemAll.Count > 0)
                {
                    int k = 1;
                    int s = 1;
                    int count = itemAll.Count;
                    int con = 0;
                    pull += "<ul " + firstClassCSS + ">";
                    string d = itemAll[0].ToString();

                    foreach (MenuProperties items in itemAll)
                    {
                        //                        if (s == 1)

                        if (valSite(items.showcorporate, items.showbranch, items.showlosite))
                        {
                            if (items.subtree[0] == "0")
                            {
                                if (k != 1)
                                {
                                    pull += "</ul></li>";
                                    k = 1;
                                }
                                pull += "<li " + (con == 0 ? " id='home' " : " mmargin1 ");
                                pull += " ><a href='" + items.url + "' " + (items.target ? " target='_blank' " : "") + "  >" + items.name + "</a></li>";
                                con++;
                            }
                            else if (items.subtree[0] == "1")
                            {
                                if (k != 1)
                                {
                                    pull += "</ul></li>";
                                    k = 1;
                                }
                                pull += "<li mmargin2 ><a href='" + items.url + "'  " + (items.target ? " target='_blank' " : "") + " mmargin3 >" + items.name + " mmargin5</a>";
                                if (s == count)
                                    pull += "</li>";
                                else
                                    pull += "<ul mmargin4>";
                            }
                            else if (items.subtree[0] == "2")
                            {
                                pull += "<li><a href='" + items.url + "' " + (items.target ? " target='_blank' " : "") + ">" + items.name + "</li>";
                                k++;
                                if (s == count)
                                    pull += "</ul></li>";
                            }
                        }
                        s++;
                    }
                    pull += "</ul>";
                }
                else
                {
                    pull = menuGeneral;
                }
                #endregion
            }
            else
            {
                pull = menuGeneral;
            }
            #endregion

            #region replace's
            pull = pull
                .Replace("{{link}}", URL.GetLink())
                .Replace("{%link%}", URL.GetLink())
                .Replace("{{path}}", Path.TemplatePath())
                .Replace("{%path%}", Path.TemplatePath())
                .Replace("{{secure}}", URL.SecureClientPost())
                .Replace("{%secure%}", URL.SecureClientPost())
                .Replace("{{unsecure}}", URL.UnsecureClientPost())
                .Replace("{%unsecure%}", URL.UnsecureClientPost())
                .Replace("{%Us%}", (Companys.HasCorporate ? "Us" : "Me"))
                .Replace("mmargin1", mmargin1)
                .Replace("mmargin2", mmargin2)
                .Replace("mmargin3", mmargin3)
                .Replace("mmargin4", mmargin4)
                .Replace("mmargin5", mmargin5)
                ;
            #endregion

            return pull;
        }

        /// <summary>
        /// Return Menu Properties from specific ID
        /// </summary>
        /// <param name="type">Type ID</param>
        /// <param name="siteID">SITE ID</param>
        /// <returns>Menu Properties</returns>
        public static List<MenuProperties> MenuBuilderPropertyAll(string type, string siteID) // MenuBuilderPropertyAll
        {
            List<MenuProperties> MenuProperty= new List<MenuProperties>();
            try
            {
                string catalogID = CatalogID(siteID);
                if (!String.IsNullOrEmpty(catalogID))
                    MenuProperty = Select.Menu.MenuBuilderPropertyAll(type, catalogID);
            }
            catch { }
            return MenuProperty;
        }

        // Valid Site
        private static bool valSite(bool corporative, bool Branch, bool LO)
        {
            bool val = false;
            if (Branchs.HasBranch && Branch)
                val = true;
            if (Consultants.HasConsultant && LO)
                val = true;
            if (Companys.HasCorporate && corporative)
                val = true;
            return val;
        }

        #endregion


    }
}