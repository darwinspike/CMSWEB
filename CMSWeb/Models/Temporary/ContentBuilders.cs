using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CMSWeb.Models.Temporary
{
    public class ContentBuilders
    {


        #region globals var
        private static Connections cnn = new Connections();
        private static string Unsecure = "";
        private static string Secure = "";
        private static string SITE_ID;
        private static string path = "";
        private static string link = "";
        private static content ct;
        #endregion

        #region Classes

        /// <summary>
        /// The Class, It Driving Of The Tags In Content And Any String
        /// </summary>
        private class contentCRM
        {
            #region Basic variables
            private string site_id;
            #endregion

            /// <summary>
            /// Initialize All Var From Current Site
            /// </summary>
            /// <param name="siteid">Site ID</param>
            public contentCRM(string siteid)
            {
                this.site_id = siteid;
            }

            /// <summary>
            /// Return An String With All Tag
            /// </summary>
            /// <param name="result">String</param>
            /// <returns>String With All Tag</returns>
            public string getSystemTag(string result)
            {
                return MultiTags.Content(result);
            }

            /// <summary>
            /// Return An String With All Tag
            /// </summary>
            /// <param name="result">String</param>
            /// <param name="siteID">Site ID</param>
            /// <returns>String With All Tag</returns>
            public string getSystemTag(string result, string sid)
            {
                return MultiTags.Content(result, sid);
            }

            /// <summary>
            /// Return An Content With All Tag
            /// </summary>
            /// <param name="number">Number Of Content</param>
            /// <returns>Content With All Tag</returns>
            public string contentContent(string number)
            {
                string result = "";
                result = contentBlock(number);
                result = MultiTags.GeneralClearTags(result);
                return result;
            }

            public string contentBlock(string number)
            {
                #region Basic variables
                string result = "";
                string pull = "";
                result = getSystemTag(Content(number.ToString(), this.site_id).Body);
                string[] txtSplit = result.Split('|');
                string[] numbers = new string[txtSplit.Length];
                string[] pulls = new string[txtSplit.Length];
                string[] type = new string[txtSplit.Length];
                #endregion


                #region Content Information (Var)
                string arrayType = "";
                string values = "";
                int modal;
                int cont = 0;
                int contArray = 0;
                #endregion


                #region Diferent Regex (Var)
                string[] txtSplit2;
                string p1, p2, p3, p4, p5, p6, pe1a, pe1b, pe2a, pe2b, pe3a, pe3b, pe4a, pe4b, pe5a, pe5b, pe6a, pe6b;
                string ic0, ic1, ic2, ic3, ic4, ib0, ib1, ib2, ib3, ib4;
                string other, numberBlo;
                int endArr, totalSplitOther;
                Regex regex9, regex5;
                Match match9, match5;

                // Tags general
                p1 = @"\{\%ISCONSULTANT\%\}([^\]]+)\{\%ENDISCONSULTANT\%\}"; pe1a = @"\{\%ISCONSULTANT\%\}"; pe1b = @"\{\%ENDISCONSULTANT\%\}";
                p2 = @"\{\%NOTCONSULTANT\%\}([^\]]+)\{\%ENDNOTCONSULTANT\%\}"; pe2a = @"\{\%NOTCONSULTANT\%\}"; pe2b = @"\{\%ENDNOTCONSULTANT\%\}";
                p3 = @"\{\%ISBRANCH\%\}([^\]]+)\{\%ENDISBRANCH\%\}"; pe3a = @"\{\%ISBRANCH\%\}"; pe3b = @"\{\%ENDISBRANCH\%\}";
                p4 = @"\{\%NOTBRANCH\%\}([^\]]+)\{\%ENDNOTBRANCH\%\}"; pe4a = @"\{\%NOTBRANCH\%\}"; pe4b = @"\{\%ENDNOTBRANCH\%\}";
                p5 = @"\{\%ISCORPORATE\%\}([^\]]+)\{\%ENDISCORPORATE\%\}"; pe5a = @"\{\%ISCORPORATE\%\}"; pe5b = @"\{\%ENDISCORPORATE\%\}";
                p6 = @"\{\%NOTCORPORATE\%\}([^\]]+)\{\%ENDNOTCORPORATE\%\}"; pe6a = @"\{\%NOTCORPORATE\%\}"; pe6b = @"\{\%ENDNOTCORPORATE\%\}";
                // Consultan specific 
                ic0 = @"\{\%ISCONSULTANTS\[([^\]]+)\]\%\}([^\]]+)\{\%ENDISCONSULTANTS\%\}"; // Toma el bloque de codigo para el LO
                ic1 = @"\{\%ISCONSULTANTS\[([^\]]+)\]\%\}"; // Dentro del bloque toma los numeros de nmls
                ic2 = @"\{\%ISCONSULTANTS\[";
                ic3 = @"\]\%\}";
                ic4 = @"\{\%ENDISCONSULTANTS\%\}";
                // Branch Specific
                ib0 = @"\{\%ISBRANCHES\[([^\]]+)\]\%\}([^\]]+)\{\%ENDISBRANCHES\%\}"; // Toma el bloque de codigo para el LO
                ib1 = @"\{\%ISBRANCHES\[([^\]]+)\]\%\}"; // Dentro del bloque toma los numeros de nmls
                ib2 = @"\{\%ISBRANCHES\[";
                ib3 = @"\]\%\}";
                ib4 = @"\{\%ENDISBRANCHES\%\}";


                #endregion

                #region Search diferent content (title and content)
                for (int i = 0; i < txtSplit.Length; i++)
                {
                    modal = i % 2;
                    if (cont == 0 && i != (txtSplit.Length - 1))
                    {
                        pull += txtSplit[i].Substring(txtSplit[i].Length - 15) + "|";
                        if (txtSplit[i].Substring(txtSplit[i].Length - 13) == "CONTENT.TITLE")
                            type[contArray] = "Title";
                        else if (txtSplit[i].Substring(txtSplit[i].Length - 12) == "CONTENT.BODY")
                            type[contArray] = "Content";
                    }
                    if (modal == 1)
                    {
                        pull += txtSplit[i] + "|%}";
                        numbers[contArray] = txtSplit[i];
                    }
                    if (cont == 1)
                    {
                        pulls[contArray] = pull;
                        pull = "";
                        contArray++;
                        cont = 0;
                    }
                    else if (cont == 0)
                        cont++;
                }

                for (int i = 0; i < contArray; i++)
                {
                    if (numbers[i] != number)
                    {
                        if (type[i] == "Title")
                        {
                            result = result.Replace(pulls[i], removeHash(Content(numbers[i]).Title));
                        }
                        if (type[i] == "Content")
                        {
                            result = result.Replace(pulls[i], this.contentBlock(numbers[i]));
                        }
                    }
                }
                #endregion

                #region Special Consultan

                endArr = 0;
                regex9 = new Regex(ic0);
                match9 = regex9.Match(result);
                if (match9.Success)
                {
                    other = match9.Value; // Valor total de Bloque
                    regex5 = new Regex(ic1);
                    match5 = regex5.Match(other);
                    if (match5.Success)
                    {
                        numberBlo = match5.Value; // bloquesito de numero NMLS
                        // Estas dos lineas de codigo le quita lo demas para solo tener los numeros NMLS
                        numberBlo = Regex.Replace(numberBlo, ic2, String.Empty);
                        numberBlo = Regex.Replace(numberBlo, ic3, String.Empty);
                        // Estas dos lineas de codigo le quita lo demas para solo dejar el mero contenido
                        other = Regex.Replace(other, ic1, String.Empty);
                        other = Regex.Replace(other, ic4, String.Empty);

                        txtSplit2 = numberBlo.Split(',');
                        totalSplitOther = txtSplit2.Length;
                        for (int i = 0; i < txtSplit2.Length; i++)
                        {
                            if (Consultants.Data().NMLS == txtSplit2[i])
                                endArr = 1;
                        }
                        if (endArr > 0)
                            result = result.Replace(match9.Value, other);
                        else
                            result = result.Replace(match9.Value, "");
                    }

                }
                #endregion

                #region Special Branch
                endArr = 0;
                regex9 = new Regex(ib0);
                match9 = regex9.Match(result);
                if (match9.Success)
                {
                    other = match9.Value; // Valor total de Bloque
                    regex5 = new Regex(ib1);
                    match5 = regex5.Match(other);
                    if (match5.Success)
                    {
                        numberBlo = match5.Value; // bloquesito de numero NMLS
                        // Estas dos lineas de codigo le quita lo demas para solo tener los numeros NMLS
                        numberBlo = Regex.Replace(numberBlo, ib2, String.Empty);
                        numberBlo = Regex.Replace(numberBlo, ib3, String.Empty);
                        // Estas dos lineas de codigo le quita lo demas para solo dejar el mero contenido
                        other = Regex.Replace(other, ib1, String.Empty);
                        other = Regex.Replace(other, ib4, String.Empty);

                        txtSplit2 = numberBlo.Split(',');
                        totalSplitOther = txtSplit2.Length;
                        for (int i = 0; i < txtSplit2.Length; i++)
                        {
                            if (Branchs.Data().NMLS == txtSplit2[i])
                                endArr = 1;
                        }

                        if (endArr > 0)
                            result = result.Replace(match9.Value, other);
                        else
                            result = result.Replace(match9.Value, "");
                    }

                }


                #endregion

                #region Array List
                ArrayList arrayListExpre = new ArrayList();
                ArrayList arrayListExpre2 = new ArrayList();
                ArrayList arrayListExpre3 = new ArrayList();
                arrayListExpre.Add(p1); arrayListExpre.Add(p2); arrayListExpre.Add(p3); arrayListExpre.Add(p4); arrayListExpre.Add(p5); arrayListExpre.Add(p6);
                arrayListExpre3.Add(pe1a); arrayListExpre3.Add(pe2a); arrayListExpre3.Add(pe3a); arrayListExpre3.Add(pe4a); arrayListExpre3.Add(pe5a); arrayListExpre3.Add(pe6a);
                foreach (string arrayListExprefor in arrayListExpre)
                {
                    Regex regex = new Regex(arrayListExprefor);
                    Match match = regex.Match(result);
                    if (match.Success)
                    {
                        values = match.Value;
                        foreach (string arrayListExprefor3 in arrayListExpre3)
                        {
                            Regex regex2 = new Regex(arrayListExprefor3);
                            Match match2 = regex2.Match(values);
                            if (match2.Success)
                                arrayType = match2.Value.ToString();
                        }
                        if (arrayType == "{%ISCONSULTANT%}" && Consultants.HasConsultant)
                        {
                            result = Regex.Replace(result, pe1a, String.Empty);
                            result = Regex.Replace(result, pe1b, String.Empty);
                        }
                        else if (arrayType == "{%NOTCONSULTANT%}" && !Consultants.HasConsultant)
                        {
                            result = Regex.Replace(result, pe2a, String.Empty);
                            result = Regex.Replace(result, pe2b, String.Empty);
                        }
                        else if (arrayType == "{%ISBRANCH%}" && Branchs.HasBranch)
                        {
                            result = Regex.Replace(result, pe3a, String.Empty);
                            result = Regex.Replace(result, pe3b, String.Empty);
                        }
                        else if (arrayType == "{%NOTBRANCH%}" && !Branchs.HasBranch)
                        {
                            result = Regex.Replace(result, pe4a, String.Empty);
                            result = Regex.Replace(result, pe4b, String.Empty);
                        }
                        else if (arrayType == "{%ISCORPORATE%}" && Companys.HasCorporate)
                        {
                            result = Regex.Replace(result, pe5a, String.Empty);
                            result = Regex.Replace(result, pe5b, String.Empty);
                        }
                        else if (arrayType == "{%NOTCORPORATE%}" && !Companys.HasCorporate)
                        {
                            result = Regex.Replace(result, pe6a, String.Empty);
                            result = Regex.Replace(result, pe6b, String.Empty);
                        }
                        else
                            arrayListExpre2.Add(values);
                    }
                }
                foreach (string arrayListExprefor in arrayListExpre2)
                {
                    result = result.Replace(arrayListExprefor, "");
                }
                #endregion


                return result;
            }

        }

        public class content
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Error { get; set; }

            public content()
            {
                this.Title = "";
                this.Body = "";
                this.Error = "";
            }

        }

        public class ContentEditor
        {
            public string title { get; set; }
            public string body { get; set; }
            public string id { get; set; }
            public ContentEditorSeo seo { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string deleted_at { get; set; }
            public string SiteID { get; set; }
            public string Active { get; set; }
            public string error { get; set; }

            public ContentEditor()
            {
                this.title = "";
                this.body = "";
                this.id = "";
                this.seo = new ContentEditorSeo();
                this.created_at = "";
                this.updated_at = "";
                this.deleted_at = "";
                this.SiteID = "";
                this.Active = "";
                this.error = "";
            }
        }

        public class contentContentEditor
        {
            public List<ContentEditor> Content { get; set; }
        }

        public class ContentEditorSeo
        {
            public string title { get; set; }
            public string description { get; set; }
            public ArrayList keywords { get; set; }

            public ContentEditorSeo()
            {
                this.title = "";
                this.description = "";
                this.keywords = new ArrayList();
            }
        }

        #endregion

        #region removeHash
        /// <summary>
        /// Return String Without Any hash
        /// </summary>
        /// <param name="pageTitle">String</param>
        /// <returns>String Without Any hash</returns>
        public static string removeHash(string pageTitle)
        {
            if (pageTitle.Length > 1)
            {
                if (pageTitle.IndexOf("(") > -1)
                {
                    int titleStart = 0;
                    int titleStop = pageTitle.IndexOf("(");
                    pageTitle = pageTitle.Substring(titleStart, titleStop);
                }
                else
                {
                    if (pageTitle.IndexOf("#") > -1)
                    {
                        int titleStart = 0;
                        int titleStop = pageTitle.IndexOf("#");
                        pageTitle = pageTitle.Substring(titleStart, titleStop);
                    }
                }
            }
            return pageTitle;
        }
        #endregion

        #region Content Editor Value(Title and Body)

        /// <summary>
        /// Return Content Body
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <returns>Body</returns>
        public static string Body(int numberContent)
        {
            if (!String.IsNullOrEmpty(numberContent.ToString()))
                return Contents(numberContent.ToString(), MasterGlobal.SiteID(), "body").Body;
            else
                return "We can't find the information you are try to get";
        }

        /// <summary>
        /// Return Content Body
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Body</returns>
        public static string Body(int numberContent, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);

            if (String.IsNullOrEmpty(erro))
            {
                if (!String.IsNullOrEmpty(numberContent.ToString()))
                    return Contents(numberContent.ToString(), ids, "body").Body;
                else
                    return "We can't find the information you are try to get";
            }
            else
            {
                return erro;
            }

        }


        /// <summary>
        /// Return Content Body
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <returns>Body</returns>
        public static string BodyWithTags(int numberContent)
        {
            contentCRM crm = new contentCRM(MasterGlobal.SiteID());
            if (!String.IsNullOrEmpty(numberContent.ToString()))
                return crm.contentContent(numberContent.ToString());
            else
                return "We can't find the information you are try to get";
        }

        /// <summary>
        /// Return Content Body
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Body</returns>
        public static string BodyWithTags(int numberContent, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);

            if (String.IsNullOrEmpty(erro))
            {
                contentCRM crm = new contentCRM(ids);
                if (!String.IsNullOrEmpty(numberContent.ToString()))
                    return crm.contentContent(numberContent.ToString());
                else
                    return "We can't find the information you are try to get";
            }
            else
            {
                return erro;
            }

        }
        



        /// <summary>
        /// Return Content Title
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Title</returns>
        public static string Title(int numberContent)
        {
            if (!String.IsNullOrEmpty(numberContent.ToString()))
                return Contents(numberContent.ToString(), MasterGlobal.SiteID(), "title").Title;
            else
                return "We can't find the information you are try to get";

        }

        /// <summary>
        /// Return Content Title
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Title</returns>
        public static string Title(int numberContent, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);

            if (String.IsNullOrEmpty(erro))
            {
                if (!String.IsNullOrEmpty(numberContent.ToString()))
                    return Contents(numberContent.ToString(), ids, "title").Title;
                else
                    return "We can't find the information you are try to get";

            }
            else
            {
                return erro;
            }
        }



        /// <summary>
        /// Return Content Title
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Title</returns>
        public static string TitleWithTags(int numberContent)
        {
            if (!String.IsNullOrEmpty(numberContent.ToString()))
                return removeHash(Contents(numberContent.ToString(), MasterGlobal.SiteID(), "title").Title);
            else
                return "We can't find the information you are try to get";

        }

        /// <summary>
        /// Return Content Title
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Title</returns>
        public static string TitleWithTags(int numberContent, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);

            if (String.IsNullOrEmpty(erro))
            {
                if (!String.IsNullOrEmpty(numberContent.ToString()))
                    return removeHash(Contents(numberContent.ToString(), ids, "title").Title);
                else
                    return "We can't find the information you are try to get";

            }
            else
            {
                return erro;
            }
        }




        /// <summary>
        /// Return Title And Body 
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <returns>Title And Body</returns>
        public static content Content(string numberContent)
        {
            if (!String.IsNullOrEmpty(numberContent))
                return Contents(numberContent, MasterGlobal.SiteID(), "all");
            else
            {
                ct = new content();
                ct.Error = "We can't find the information you are try to get";
                return ct;
            }

        }

        /// <summary>
        /// Return Title And Body
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Title And Body</returns>
        public static content Content(string numberContent, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);

            if (String.IsNullOrEmpty(erro))
            {
                if (!String.IsNullOrEmpty(numberContent))
                    return Contents(numberContent, ids, "all");
                else
                {
                    ct = new content();
                    ct.Error = "We can't find the information you are try to get";
                    return ct;
                }
            }
            else
            {
                ct = new content();
                ct.Error = erro;
                return ct;
            }
        }

        /// <summary>
        /// Return Title And Body 
        /// </summary>
        private static content Contents(params string[] parameters)
        {
            string numberContentGet = parameters.Length > 0 ? parameters[0] : "1";
            string SITE_ID2 = parameters.Length > 1 ? parameters[1] : MasterGlobal.SiteID();
            string type = parameters.Length > 2 ? parameters[2] : "";
            string numberNameContent = "PAGE_TEXT_CONTENT_NAME" + numberContentGet;
            string numberContent = "PAGE_TEXT_CONTENT" + numberContentGet;
            string title = "";


            ct = new content();
            #region Title

            if (type == "title" || type == "all")
            {
                try
                {
                    using (SqlDataReader reader = cnn.ExecuteReader("SELECT CONTENT_VALUE FROM tblClientPagesContent WHERE Site_ID = @SITE_ID AND CONTENT_TYPE_NAME = @numberNameContent",
                                        new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@SITE_ID", SITE_ID2),
                            new System.Data.SqlClient.SqlParameter("@numberNameContent", numberNameContent)}))
                    {
                        if (reader.Read())
                        {
                            title = reader["CONTENT_VALUE"].ToString().Trim();
                        }
                    }
                    #region dinamic change Us Me
                    if (Companys.HasCorporate)
                    {
                        title = title.Replace("{%Us%}", "Us");
                    }
                    else
                    {
                        title = title.Replace("{%Us%}", "Me");
                    }
                    #endregion
                    ct.Title = title;
                }
                catch (Exception) { throw; }
            }
            else
            {
                ct.Title = "";
            }
            #endregion

            if (type == "body" || type == "all")
            {
                #region Body
                try
                {
                    using (SqlDataReader reader = cnn.ExecuteReader("SELECT CONTENT_VALUE FROM tblClientPagesContent WHERE Site_ID = @SITE_ID AND CONTENT_TYPE_NAME = @numberContent",
                                            new System.Data.SqlClient.SqlParameter[] { 
                            new System.Data.SqlClient.SqlParameter("@SITE_ID", SITE_ID2),
                            new System.Data.SqlClient.SqlParameter("@numberContent", numberContent)}))
                    {
                        if (reader.Read())
                        {
                            ct.Body = reader["CONTENT_VALUE"].ToString().Trim();
                        }
                    }
                }
                catch (Exception) { throw; }
                #endregion
            }
            else
            {
                ct.Body = "";
            }


            return ct;
        }

        #endregion

        #region getContent

        #region Content

        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <returns>The Content (Title Or Content) With Tags By Current Site</returns>
        public static string getContent(string numberContent)
        {
            return Contenter(numberContent);
        }

        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID 
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="dataType">Data Type ("Title" or "Content")</param>
        /// <returns>The Content (Title Or Content) With Tags By Current Site </returns>
        public static string getContent(string numberContent, string dataType)
        {
            return Contenter(numberContent, dataType);
        }


        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID 
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="dataType">Data Type ("Title" or "Content")</param>
        /// <param name="siteID">String Site ID</param>
        /// <returns>The Content (Title Or Content) With Tags By Specific Site ID</returns>
        public static string getContent(string numberContent, string dataType, string siteID)
        {

            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);
            if (String.IsNullOrEmpty(erro))
            {
                return Contenter(numberContent, dataType, ids);
            }
            else
            {
                return erro;
            }
        }

        #endregion

        /// <summary>
        /// Get Content
        /// </summary>
        private static string Contenter(params string[] parameters)
        {
            string numContent = parameters.Length > 0 ? parameters[0] : "";
            string tipe = parameters.Length > 1 ? parameters[1].ToLower() : "content";
            string returnSiteID = parameters.Length > 2 ? parameters[2] : MasterGlobal.SiteID();
            string resultado = "";

            if (!String.IsNullOrEmpty(numContent))
            {
                contentCRM values = new contentCRM(returnSiteID);
                if (tipe == "content")
                {
                    resultado = values.contentContent(numContent);
                }
                else if (tipe == "title")
                {
                    resultado = removeHash(Content(numContent.ToString(), returnSiteID).Title);
                }
            }
            else
            {
                resultado = "We can't find the information you are try to get";
            }


            return resultado;
        }
        #endregion

        #region getSystemTag

        #region
        /// <summary>
        /// Return An String With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="tags">String with tags</param>
        /// <returns>String With Tags</returns>
        public static string getSystemTag(string tags)
        {
            return SystemTag(tags);
        }

        /// <summary>
        /// Return An String With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="tags">String with tags</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>String With Tags</returns>
        public static string getSystemTag(string tags, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);
            if (String.IsNullOrEmpty(erro))
            {
                return SystemTag(tags, ids);
            }
            else
            {
                return erro;
            }
        }
        #endregion

        /// <summary>
        /// Get System Tags
        /// </summary>
        private static string SystemTag(params string[] parameters)
        {
            string tags = parameters.Length > 0 ? parameters[0] : "";
            string returnSiteID = parameters.Length > 1 ? parameters[1] : MasterGlobal.SiteID();

            if (!String.IsNullOrEmpty(tags))
            {
                contentCRM values = new contentCRM(returnSiteID);
                return values.getSystemTag(tags, returnSiteID);
            }
            else
            {
                return "We can't find the information you are try to get";
            }
        }

        #endregion

        #region Content Editor Tools

        public static int CountCustomPageBySiteID(string SiteID)
        {
            try
            {
                string sql = "SELECT COUNT(*) AS TOTAL FROM tblCustomContent WHERE SITEID = @SiteID";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID)
                           }))
                {
                    if (reader.Read())
                        return Convert.ToInt32(reader["TOTAL"].ToString());
                    else
                        return 0;
                }
            }
            catch
            {
                throw;
            }
        }

        public static bool IfExitContent(string id, string SiteID)
        {
            try
            {
                string sql = @"SELECT ISNULL(COUNT(*),0) AS TOTAL FROM tblCustomContent WHERE SiteID = @SiteID AND ContentID = @ContentID";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@ContentID", id)
                           }))
                {
                    if (reader.Read())
                    {
                        string dat = reader["TOTAL"].ToString();
                        if (Convert.ToInt32(dat) > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public static string SeoIDByContentID(string id, string SiteID)
        {
            try
            {
                string sql = @"SELECT CCS.ID AS CustomSeoID FROM tblCustomContentSEO AS CCS INNER JOIN tblCustomContent AS CC ON CCS.CustomContentID = CC.ID WHERE CC.SiteID = @SiteID AND CC.ContentID = @ContentID";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                           new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@SiteID", SiteID),
                                new System.Data.SqlClient.SqlParameter("@ContentID", id)
                           }))
                {
                    if (reader.Read())
                        return reader["CustomSeoID"].ToString();
                    else
                        return "";
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion








    }
}