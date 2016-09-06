using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using CMSWeb.Models.Data;

namespace CMSWeb.Models.Tools
{
    public class Autoresponders
    {

        #region globals var
        private static string SendEmailAdress = MasterGlobal.SendEmailAddress;
        private static string SendEmailName = MasterGlobal.SendEmailName;
        public static string urlDirs, siteName, complement, sql, name, standar;
        public static string optionUnsecure = "true";
        private static string fullName;
        private static string recipient;

        #endregion

        #region Suport tag and other stuff
        //Class For Create Autoresponders
        public class BorrowerCo
        {
            //Borrower
            public string BorrowerFirstName;
            public string BorrowerLastName;
            public string BorrowerAddress;
            public string BorrowerCity;
            public string BorrowerState;
            public string BorrowerZip;
            public string BorrowerHomePhone;
            public string BorrowerCellPhone;
            public string BorrowerEmail;
            public string BorrowerLoanAmount;
            public string BorrowerDOB;
            public string BorrowerPassword;

            //Co-Borrower
            public string CoBorrowerFirstName;
            public string CoBorrowerLastName;
            public string CoBorrowerAddress;
            public string CoBorrowerCity;
            public string CoBorrowerState;
            public string CoBorrowerZip;
            public string CoBorrowerHomePhone;
            public string CoBorrowerCellPhone;
            public string CoBorrowerEmail;
            public string CoBorrowerDOB;

            //Borrovwe And Co-Borrower List
            public BorrowerCo()
            {
                //Borrower
                BorrowerFirstName = "";
                BorrowerLastName = "";
                BorrowerAddress = "";
                BorrowerCity = "";
                BorrowerState = "";
                BorrowerZip = "";
                BorrowerHomePhone = "";
                BorrowerCellPhone = "";
                BorrowerEmail = "";
                BorrowerLoanAmount = "";
                BorrowerDOB = "";
                BorrowerPassword = "";

                //CO-Borrower
                CoBorrowerFirstName = "";
                CoBorrowerLastName = "";
                CoBorrowerAddress = "";
                CoBorrowerCity = "";
                CoBorrowerState = "";
                CoBorrowerZip = "";
                CoBorrowerHomePhone = "";
                CoBorrowerCellPhone = "";
                CoBorrowerEmail = "";
                CoBorrowerDOB = "";
            }
        }

        protected class getAutoResponder
        {
            #region Basic Variables

            private string C_ID;
            private string SITE_ID;
            private BorrowerCo dataBCo;

            #endregion

            #region Constructor to set initial values consultant
            public getAutoResponder(string cid, string site_id, BorrowerCo values)
            {
                //Set initial values
                this.dataBCo = new BorrowerCo();
                this.dataBCo = values;
                this.C_ID = cid;
                this.SITE_ID = site_id;
            }
            #endregion

            /******************************************************************************************
            * with this class we get in an easier way all autoresponders based and tags are translated
            * type means the value from AdminClients/Controls/Edit_EMAIL_MSL.ascx.cs
            * function Page_PreRender
            ******************************************************************************************/

            #region getHTML_DATA With content number or Without content number
            //This function is auto responder (normal)
            public string getHTML_DATA(string type)
            {
                string body = "";
                try
                {
                    body = MultiTags.AutoresponderBorrowerByConsultantID(Select.Autorespond.getHTML_DATA(this.SITE_ID, type) , this.C_ID, this.dataBCo, this.SITE_ID);
                }
                catch (Exception)
                {
                    throw;
                }
                return body;
            }

            //This function is auto responder (Content editor)
            public string getHTML_DATA(string type, int number)
            {
                string body = "";

                if (type == "html_body")
                    //                    body = multiTagAutoresponder(ContentData.Content(number.ToString()).Body, this.C_ID, this.dataBCo);
                    body = MultiTags.AutoresponderBorrowerByConsultantID(ContentBuilders.BodyWithTags(number), this.C_ID, this.dataBCo, this.SITE_ID);
                else if (type == "subject")
                    body = ContentBuilders.TitleWithTags(number);

                return body;
            }
            #endregion
        }
        #endregion

        #region Send AutoResponder LO (Only Full)

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Step Number</param>
        public static void send_AutoResponderLO(BorrowerCo data, int stepNumber)
        {
            send_AutoResponderLOs(data, stepNumber);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="number">If It's option "Full" Number = Step Number Or Option "Content" number = Content Number </param>
        /// <param name="option">"Full" or "Content"</param>
        public static void send_AutoResponderLO(BorrowerCo data, int number, string option)
        {
            send_AutoResponderLOs(data, number, option);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="number">If It's option "Full" Number = Step Number Or Option "Content" number = Content Number </param>
        /// <param name="option">"Full" or "Content"</param>
        /// <param name="siteID">Specific Site ID</param>
        public static void send_AutoResponderLO(BorrowerCo data, int number, string option, string siteID)
        {
            send_AutoResponderLOs(data, number, option, MasterGlobal.ValidGuidID(siteID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="number">If It's option "Full" Number = Step Number Or Option "Content" number = Content Number </param>
        /// <param name="option">"Full" or "Content"</param>
        /// <param name="siteID">Specific Site ID</param>
        /// <param name="CID">Specific Consultant ID</param>
        public static void send_AutoResponderLO(BorrowerCo data, int number, string option, string siteID, string CID)
        {
            send_AutoResponderLOs(data, number, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(CID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        private static void send_AutoResponderLOs(BorrowerCo data, int numberContent, params string[] parameters)
        {
            #region Set initial variables
            string html_body = "";
            string subject = "";
            string recipient = "";
            string[] allRecipients;
            int number = numberContent;
            string option = parameters.Length > 0 ? parameters[0] : "";
            string SITE_ID = parameters.Length > 1 ? parameters[1] : MasterGlobal.SiteID();
            string c_id = parameters.Length > 2 ? parameters[2] : (Consultants.HasConsultant ? Consultants.Data().ID : Guid.Empty.ToString());
            string b_id = parameters.Length > 3 ? parameters[3] : (Branchs.HasBranch ? Branchs.Data().BranchID : Guid.Empty.ToString());
            string recipientFrom;
            string siteName = Companys.Data().CompanyName;
            string fullName = "";
            bool searchContent = false;
            MailMessage m = new MailMessage();
            #endregion

            #region validate options

            if (option.ToLower() == "content")
                searchContent = true;

            #endregion

            #region Get Message
            //Get Values from the autoresponder
            getAutoResponder values = new getAutoResponder(c_id, SITE_ID, data);
            //Get subject from autoresponder without tags

            if (searchContent)
            {
                subject = values.getHTML_DATA("subject", number);
                html_body = values.getHTML_DATA("html_body", number);

            }
            else
            {
                if (number < 10)
                {
                    //Get subject from autoresponder without tags
                    subject = values.getHTML_DATA("EMAIL_SUBJECT_CONGRAT").Replace("</a>", " ");

                    //get body from autoresponder without tags
                    html_body = values.getHTML_DATA("EMAIL_BODY_CONGRAT");
                }
                else
                {
                    //Get subject from autoresponder without tags
                    subject = values.getHTML_DATA("EMAIL_SUBJECT_FS").Replace("</a>", " ");

                    //Get body from autoresponder without tags
                    html_body = values.getHTML_DATA("EMAIL_BODY_FS");
                }
            }
            #endregion

            #region Set Recipients
            try
            {

                //Verify if is not empty the LO
                if (!String.IsNullOrEmpty(c_id))
                {
                    Consultants.Consultant consultant = Consultants.DataByConsultantID(c_id);
                    fullName = consultant.FullName;
                    recipient = consultant.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else if (!String.IsNullOrEmpty(b_id))
                {
                    Branchs.Branch branch = Branchs.DataByID(b_id);
                    fullName = branch.Name;
                    recipient = branch.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else
                {
                    //This is to get the recipient dinamic: 
                    recipientFrom = Companys.Data().CompanyEmailNotifications.ToString();//UIF.GetEmailTo();
                    if (recipientFrom != "")
                    {
                        allRecipients = recipientFrom.Split(',');
                        for (int i = 0; i < allRecipients.Length; i++)
                        {
                            if (i == 0)
                                m.To.Add(new MailAddress(allRecipients[i], siteName));
                            else
                                m.To.Add(new MailAddress(allRecipients[i], siteName));
                        }
                    }
                    else
                    {
                        m.To.Add(new MailAddress(MasterGlobal.SendEmailAddress, siteName));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            #endregion


            #region Send Email

            m.From = new MailAddress(SendEmailAdress, SendEmailName);
            m.Subject = subject;
            m.Body = html_body;
            //Set place where is going to be send the email
            SmtpAPIWrapper cl = SMTP.SmtpClientCreaCRM();
            cl.Send(m);
            #endregion
        }
        #endregion

        #region Send AutoResponder Lead send_AutoResponderLead

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        public static void send_AutoResponderLead(BorrowerCo data, string option)
        {
            send_AutoResponderLeads(data, 1, option);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        public static void send_AutoResponderLead(BorrowerCo data, string option, string siteID)
        {
            send_AutoResponderLeads(data, 1, option, MasterGlobal.ValidGuidID(siteID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void send_AutoResponderLead(BorrowerCo data, string option, string siteID, string cID)
        {

            send_AutoResponderLeads(data, 1, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(cID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        public static void send_AutoResponderLead(BorrowerCo data, int numberContent)
        {
            send_AutoResponderLeads(data, numberContent);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="number">If It's option "Full" Number = Step Number Or Option "Content" number = Content Number </param>
        /// <param name="option">"Full" or "Content"</param>
        public static void send_AutoResponderLead(BorrowerCo data, int number, string option)
        {
            send_AutoResponderLeads(data, number, option);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="number">If It's option "Full" Number = Step Number Or Option "Content" number = Content Number </param>
        /// <param name="option">"Full" or "Content"</param>
        /// <param name="siteID">Site ID</param>
        public static void send_AutoResponderLead(BorrowerCo data, int number, string option, string siteID)
        {
            send_AutoResponderLeads(data, number, option, MasterGlobal.ValidGuidID(siteID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="number">If It's option "Full" Number = Step Number Or Option "Content" number = Content Number </param>
        /// <param name="option">"Full" or "Content"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void send_AutoResponderLead(BorrowerCo data, int number, string option, string siteID, string cID)
        {
            send_AutoResponderLeads(data, number, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(cID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        private static void send_AutoResponderLeads(BorrowerCo data, int numberContent, params string[] parameters)
        {

            #region Set initial variables
            string html_body = "";
            string subject = "";
            string recipientFrom = "";
            int number = numberContent;
            string option = parameters.Length > 0 ? parameters[0] : "";
            string SITE_ID = parameters.Length > 1 ? parameters[1] : MasterGlobal.SiteID();
            string c_id = parameters.Length > 2 ? parameters[2] : (Consultants.HasConsultant ? Consultants.Data().ID : Guid.Empty.ToString());
            string b_id = parameters.Length > 3 ? parameters[3] : (Branchs.HasBranch ? Branchs.Data().BranchID : Guid.Empty.ToString());
            string siteName = Companys.Data().CompanyName;
            bool searchFull = false;
            bool searchShort = false;
            bool searchCareer = false;
            bool searchContactUs = false;
            bool searchMini = false;
            bool searchQuick = false;
            bool searchPassword = false;
            MailMessage m = new MailMessage();
            #endregion

            #region option
            if (option.ToLower() == "full")
                searchFull = true;
            else if (option.ToLower() == "short")
                searchShort = true;
            else if (option.ToLower() == "career")
                searchCareer = true;
            else if (option.ToLower() == "contact")
                searchContactUs = true;
            else if (option.ToLower() == "mini")
                searchMini = true;
            else if (option.ToLower() == "quick")
                searchQuick = true;
            else if (option.ToLower() == "password")
                searchPassword = true;

            #endregion

            #region Get Message
            //Get Values from the autoresponder
            getAutoResponder values = new getAutoResponder(c_id, SITE_ID, data);
            //Get subject from autoresponder without tags

            if (searchFull)
            {
                if (number < 10)
                {
                    subject = values.getHTML_DATA("EMAIL_SUBJECT_FC").Replace("</a>", " ");
                    html_body = values.getHTML_DATA("EMAIL_BODY_FC");
                }
                else
                {
                    subject = values.getHTML_DATA("EMAIL_SUBJECT_PS").Replace("</a>", " ");
                    html_body = values.getHTML_DATA("EMAIL_BODY_PS");
                }
            }
            else if (searchShort)
            {
                subject = values.getHTML_DATA("EMAIL_SUBJECT_RT").Replace("</a>", " ");
                html_body = values.getHTML_DATA("EMAIL_BODY_RT");
            }
            else if (searchCareer)
            {
                subject = values.getHTML_DATA("EMAIL_SUBJECT_LOST_PASSWD").Replace("</a>", " ");
                html_body = values.getHTML_DATA("EMAIL_BODY_LOST_PASSWD");
            }
            else if (searchContactUs)
            {
                subject = values.getHTML_DATA("EMAIL_SUBJECT_LA").Replace("</a>", " ");
                html_body = values.getHTML_DATA("EMAIL_BODY_LA");
            }
            else if (searchMini)
            {
                subject = values.getHTML_DATA("EMAIL_SUBJECT_MIN").Replace("</a>", " ");
                html_body = values.getHTML_DATA("EMAIL_BODY_MIN");
            }
            else if (searchQuick)
            {
                subject = values.getHTML_DATA("EMAIL_SUBJECT_QAP").Replace("</a>", " ");
                html_body = values.getHTML_DATA("EMAIL_BODY_QAP");
            }
            else if (searchPassword)
            {
                subject = values.getHTML_DATA("EMAIL_SUBJECT_CAR").Replace("</a>", " ");
                html_body = values.getHTML_DATA("EMAIL_BODY_CAR");
            }
            else
            {
                subject = values.getHTML_DATA("subject", number);
                html_body = values.getHTML_DATA("html_body", number);
            }
            #endregion

            #region Set Recipients
            try
            {

                //Verify if is not empty the LO
                if (!String.IsNullOrEmpty(c_id))
                {
                    Consultants.Consultant consultant = Consultants.DataByConsultantID(c_id);
                    fullName = consultant.FullName;
                    recipient = consultant.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else if (!String.IsNullOrEmpty(b_id))
                {
                    Branchs.Branch branch = Branchs.DataByID(b_id);
                    fullName = branch.Name;
                    recipient = branch.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else
                {
                    //This is to get the recipient dinamic: 
                    recipientFrom = Companys.Data().CompanyEmailNotifications.ToString();//UIF.GetEmailTo();
                    if (recipientFrom != "")
                    {
                        string[] allRecipients = recipientFrom.Split(',');
                        for (int i = 0; i < allRecipients.Length; i++)
                        {
                            if (i == 0)
                                m.To.Add(new MailAddress(allRecipients[i], siteName));
                            else
                                m.To.Add(new MailAddress(allRecipients[i], siteName));
                        }
                    }
                    else
                    {
                        m.To.Add(new MailAddress(MasterGlobal.SendEmailAddress, siteName));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

            #region Send Email
            m.To.Add(new MailAddress(data.BorrowerEmail, data.BorrowerFirstName + "" + data.CoBorrowerLastName));
            m.Subject = subject;
            m.Body = html_body;
            //Set place where is going to be send the email
            SmtpAPIWrapper cl = SMTP.SmtpClientCreaCRM();
            cl.Send(m);
            #endregion
        }
        #endregion

        #region send AutoResponder User

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        public static void send_AutoResponderUser(BorrowerCo data, string numberContent)
        {
            send_AutoResponderUsers(data, numberContent);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        public static void send_AutoResponderUser(BorrowerCo data, string numberContent, string siteID)
        {
            send_AutoResponderUsers(data, numberContent, siteID);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void send_AutoResponderUser(BorrowerCo data, string numberContent, string siteID, string cID)
        {
            send_AutoResponderUsers(data, numberContent, siteID, cID);
        }


        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        private static void send_AutoResponderUsers(BorrowerCo data, params string[] parameters)
        {
            #region Set initial variables
            string html_body = "";
            string subject = "";
            string recipientFrom = "";
            int number = parameters.Length > 0 ? Convert.ToInt32(parameters[0]) : 1;
            string SITE_ID = parameters.Length > 1 ? parameters[1] : MasterGlobal.SiteID();
            string c_id = parameters.Length > 2 ? parameters[2] : (Consultants.HasConsultant ? Consultants.Data().ID : Guid.Empty.ToString());
            string b_id = parameters.Length > 3 ? parameters[3] : (Branchs.HasBranch ? Branchs.Data().BranchID : Guid.Empty.ToString());
            string siteName = Companys.Data().CompanyName;
            MailMessage m = new MailMessage();
            #endregion

            #region Get Message
            getAutoResponder values = new getAutoResponder(c_id, SITE_ID, data);
            subject = values.getHTML_DATA("subject", number);
            html_body = values.getHTML_DATA("html_body", number);
            #endregion

            #region Set Recipients
            try
            {

                //Verify if is not empty the LO
                if (!String.IsNullOrEmpty(c_id))
                {
                    Consultants.Consultant consultant = Consultants.DataByConsultantID(c_id);
                    fullName = consultant.FullName;
                    recipient = consultant.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else if (!String.IsNullOrEmpty(b_id))
                {
                    Branchs.Branch branch = Branchs.DataByID(b_id);
                    fullName = branch.Name;
                    recipient = branch.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else
                {
                    //This is to get the recipient dinamic: 
                    recipientFrom = Companys.Data().CompanyEmailNotifications.ToString();//UIF.GetEmailTo();
                    if (recipientFrom != "")
                    {
                        string[] allRecipients = recipientFrom.Split(',');
                        for (int i = 0; i < allRecipients.Length; i++)
                        {
                            if (i == 0)
                                m.To.Add(new MailAddress(allRecipients[i], siteName));
                            else
                                m.To.Add(new MailAddress(allRecipients[i], siteName));
                        }
                    }
                    else
                    {
                        m.To.Add(new MailAddress(MasterGlobal.SendEmailAddress, siteName));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

            #region Send Email
            m.To.Add(new MailAddress(data.BorrowerEmail, data.BorrowerFirstName + data.BorrowerLastName));
            m.Subject = subject;
            m.Body = html_body;
            //Set place where is going to be send the email
            SmtpAPIWrapper cl = SMTP.SmtpClientCreaCRM();
            cl.Send(m);
            #endregion
        }
        #endregion



    }
}