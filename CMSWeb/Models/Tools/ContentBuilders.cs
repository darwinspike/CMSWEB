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
using System.Collections;
using System.Text.RegularExpressions;


using System.Runtime.Serialization;

namespace CMSWeb.Models.Tools
{
    [DataContract]    
    public class ContentBuilders
    {

        #region Class

        [DataContract]
        public class ContentEditor
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string body { get; set; }
            [DataMember]
            public string NumberContent { get; set; }
            [DataMember]
            public ContentEditorSeo seo { get; set; }
            [DataMember]
            public string created_at { get; set; }
            [DataMember]
            public string updated_at { get; set; }
            [DataMember]
            public string deleted_at { get; set; }
            [DataMember]
            public string SiteID { get; set; }
            [DataMember]
            public string Active { get; set; }
            [DataMember]
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

        [DataContract]
        public class ContentEditorSeo
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string customContentID { get; set; }
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public ArrayList keywords { get; set; }

            public ContentEditorSeo()
            {
                this.id = "";
                this.customContentID = "";
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

        #region Contents

        /// <summary>
        /// Return Content Body with Multi Tags By Specific Site ID and Content Number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="SiteID"></param>
        /// <returns>Content Body with Multi Tags By Specific Site ID and Content Number</returns>
        public static string contentContent(int number, string SiteID)
        {
            string result = "";
            result = contentBlock(number, SiteID);
            result = MultiTags.GeneralClearTags(result);
            return result;
        }

        /// <summary>
        /// Return Content Body By Specific Site ID and Content Number
        /// </summary>
        /// <param name="number">Content Number</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Content Body By Specific Site ID and Content Number</returns>
        public static string contentBlock(int number, string SiteID)
        {
            #region Basic variables
            string result = "";
            string pull = "";
            result = MultiTags.SystemTag(Body(number, SiteID));
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
                if (Convert.ToInt32(numbers[i]) != number)
                {
                    if (type[i] == "Title")
                    {
                        result = result.Replace(pulls[i], removeHash(Title(Convert.ToInt32(numbers[i]))));
                    }
                    if (type[i] == "Content")
                    {
                        result = result.Replace(pulls[i], contentBlock(Convert.ToInt32(numbers[i]), SiteID));
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
         
        #endregion

        #region Content Editor Value(Title and Body)

        #region Body
        /// <summary>
        /// Return Content Body By Specific Content Number
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <returns>Content Body By Specific Content Number</returns>
        public static string Body(int numberContent)
        {
            if (numberContent > 0)
                return Contents(numberContent, MasterGlobal.SiteID()).body;
            else
                return "We can't find the information you are try to get";
        }

        /// <summary>
        /// Return Content Body By Specific Content Number and Site ID
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Content Body By Specific Content Number and Site ID</returns>
        public static string Body(int numberContent, string siteID)
        {
            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);

            if (String.IsNullOrEmpty(erro))
            {
                if (numberContent > 0)
                    return Contents(numberContent, ids).body;
                else
                    return "We can't find the information you are try to get";
            }
            else
            {
                return erro;
            }

        }

        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <returns>The Content (Title Or Content) With Tags By Current Site</returns>
        public static string BodyWithTags(int numberContent)
        {
            return contentContent(numberContent, MasterGlobal.SiteID());
        }

        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID 
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="dataType">Data Type ("Title" or "Content")</param>
        /// <param name="siteID">String Site ID</param>
        /// <returns>The Content (Title Or Content) With Tags By Specific Site ID</returns>
        public static string BodyWithTags(int numberContent, string siteID)
        {

            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);
            if (String.IsNullOrEmpty(erro))
            {
                return contentContent(numberContent, ids);
            }
            else
            {
                return erro;
            }
        }

        #endregion

        #region Title

        /// <summary>
        /// Return Content Title
        /// </summary>
        /// <param name="numberContent">Number Content</param>
        /// <param name="siteID">Site ID</param>
        /// <returns>Title</returns>
        public static string Title(int numberContent)
        {
            if (numberContent > 0)
                return Contents(numberContent, MasterGlobal.SiteID()).title;
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
                if (numberContent > 0)
                    return Contents(numberContent, ids).title;
                else
                    return "We can't find the information you are try to get";
            }
            else
            {
                return erro;
            }
        }

        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <returns>The Content (Title Or Content) With Tags By Current Site</returns>
        public static string TitleWithTags(int numberContent)
        {
            return removeHash(Title(numberContent));
        }

        /// <summary>
        /// Return The Content (Title Or Content) With Tags By Current Site Or Specific Site ID 
        /// </summary>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="dataType">Data Type ("Title" or "Content")</param>
        /// <param name="siteID">String Site ID</param>
        /// <returns>The Content (Title Or Content) With Tags By Specific Site ID</returns>
        public static string TitleWithTags(int numberContent, string siteID)
        {

            string ids = MasterGlobal.ValidGuidID(siteID);
            string erro = MasterGlobal.ValidGuidIDError(siteID);
            if (String.IsNullOrEmpty(erro))
            {
                return removeHash(Title(numberContent, siteID));
            }
            else
            {
                return erro;
            }
        }
        #endregion

        private static ContentEditor Contents(int numberContents, string siteID)
        {
            int numberContent = numberContents > 0 ? numberContents : 0;
            string SITE_ID = String.IsNullOrEmpty(siteID) ? siteID : MasterGlobal.SiteID();

            ContentEditor ce = Select.ContentBuil.Contents(siteID,numberContent.ToString());

            return ce;
        }

        #endregion

        #region Content Editor Tools

        /// <summary>
        /// Return Count To Custom Page By Specific Site ID
        /// </summary>
        /// <param name="SiteID">Site ID</param>
        /// <returns>Count To Custom Page By Specific Site ID</returns>
        public static int CountCustomPageBySiteID(string SiteID)
        {
            int res = 0;
            try
            {
                res = Convert.ToInt16(Select.ContentBuil.CountCustomPageBySiteID(SiteID));
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Return True Or False If Exist Any Content By Specific Content ID and Site ID
        /// </summary>
        /// <param name="id">Content ID</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>True Or False If Exist Any Content By Specific Content ID and Site ID</returns>
        public static bool IfExitContent(string id, string SiteID)
        {
            try
            {
                string dat = Select.ContentBuil.IfExitContent(id,SiteID).ToString();
                if (Convert.ToInt32(dat) > 0)
                    return true;
                else
                    return false;

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Return SEO ID By Specific Site ID and Content ID
        /// </summary>
        /// <param name="id">Content ID</param>
        /// <param name="SiteID">Site ID</param>
        /// <returns>SEO ID By Specific Site ID and Content ID</returns>
        public static string SeoIDByContentID(string id, string SiteID)
        {
            string res = "";
            try
            {
                res = Select.ContentBuil.SeoIDByContentID(id, SiteID).ToString();
            }
            catch
            {
                throw;
            }
            return res;
        }

        #endregion


    }
}