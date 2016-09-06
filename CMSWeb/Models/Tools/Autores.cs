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
using System.Net.Mail;


namespace CMSWeb.Models.Tools
{
    public class Autoresponders
    {
        #region globals var
        private static Connections cnn = new Connections();
        private static string SendEmailAdress = MasterGlobal.SendEmailAddress;
        private static string SendEmailName = MasterGlobal.SendEmailName;
        private static string result = "";
        private static string siteID;
        public static string urlDirs, siteName, complement, sql, name, standar;
        public static string optionUnsecure = "true";
        #endregion

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

        #region AutoResponder Class

        #region Send AutoResponder By Full App

        public static void FullAplication(BorrowerCo data, int stepNumber)
        {
            send_AutoResponderLOs(data, stepNumber, String.Empty, String.Empty, String.Empty);
        }

        public static void FullAplication(BorrowerCo data, int stepNumber, string siteID)
        {
            send_AutoResponderLOs(data, stepNumber, String.Empty, MasterGlobal.ValidGuidID(siteID), String.Empty);
        }

        public static void FullAplicationByConsultantID(BorrowerCo data, int number, string option, string siteID, string CID)
        {
            send_AutoResponderLOs(data, number, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(CID));
        }

        public static void FullAplicationByBranchID(BorrowerCo data, int number, string option, string siteID, string CID)
        {
            send_AutoResponderLOs(data, number, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(CID));
        }

        public static void FullAplicationByConsultantBranchID(BorrowerCo data, int number, string option, string siteID, string CID)
        {
            send_AutoResponderLOs(data, number, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(CID));
        }

        private static void send_AutoResponderLOs(BorrowerCo data, int stepNumber, string SiteID, string CID, string BID)
        {
            #region Set initial variables
            string html_body = "";
            string subject = "";
            string recipient = "";
            string[] allRecipients;
            int number = stepNumber;
            string SITE_ID = !String.IsNullOrEmpty(SiteID) ? SiteID : MasterGlobal.SiteID();
            string c_id = !String.IsNullOrEmpty(CID) ? CID : (Consultants.HasConsultant ? Consultants.Data().ID : Guid.Empty.ToString());
            string b_id = !String.IsNullOrEmpty(BID) ? CID : (Branchs.HasBranch ? Branchs.Data().BranchID : Guid.Empty.ToString());
            string recipientFrom;
            string siteName = Companys.Data().CompanyName;
            string fullName = "";
            bool searchContent = false;
            MailMessage m = new MailMessage();
            #endregion

            #region Get Message

            if (number < 10)
            {
                //Get subject from autoresponder without tags
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_CONGRAT", SITE_ID, c_id, b_id).Replace("</a>", " ");

                //get body from autoresponder without tags
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_CONGRAT", SITE_ID, c_id, b_id);
            }
            else
            {
                //Get subject from autoresponder without tags
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_FS", SITE_ID, c_id, b_id).Replace("</a>", " ");

                //Get body from autoresponder without tags
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_FS", SITE_ID, c_id, b_id);
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
                }else if(!String.IsNullOrEmpty(b_id)){
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
            //AdminClients.Controls.SmtpAPIWrapper cl = AdminClients.EmailEvents.Controls.SmtpHosts.SmtpClientCreaCRM();
            //cl.Send(m);
            #endregion
        }
        #endregion

        #region Send AutoResponder Lead send_AutoResponderLead

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        public static void General(BorrowerCo data, string option)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.SiteID(), String.Empty, String.Empty);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        public static void General(BorrowerCo data, string option, string siteID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.ValidGuidID(siteID), String.Empty, String.Empty);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void GeneralByConsultantID(BorrowerCo data, string option, string cID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(cID), String.Empty);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void GeneralByConsultantID(BorrowerCo data, string option, string cID, string siteID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(cID), String.Empty);
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void GeneralByBranchID(BorrowerCo data, string option, string BID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.SiteID(), String.Empty, MasterGlobal.ValidGuidID(BID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void GeneralByBranchID(BorrowerCo data, string option, string BID, string siteID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.SiteID(), String.Empty, MasterGlobal.ValidGuidID(BID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void GeneralByConsultantBranchID(BorrowerCo data, string option, string cID, string BID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.SiteID(), MasterGlobal.ValidGuidID(cID), MasterGlobal.ValidGuidID(BID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="option">"Full" or "short" or "career" or "contact" or "Mini" or "Quick" or "Password"</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Consultant ID</param>
        public static void GeneralByConsultantBranchID(BorrowerCo data, string option, string cID, string BID, string siteID)
        {
            send_AutoResponderLeads(data, option, MasterGlobal.ValidGuidID(siteID), MasterGlobal.ValidGuidID(cID), MasterGlobal.ValidGuidID(BID));
        }

        /// <summary>
        /// Send Autoresponder To The Admin, Consultant or Branch With Content Editor Or Manager
        /// </summary>
        private static void send_AutoResponderLeads(BorrowerCo data, string option, string SiteID, string CID, string BID)
        {

            #region Set initial variables
            string html_body = "";
            string subject = "";
            string recipient = "";
            string fullName = "";
            string SITE_ID = !String.IsNullOrEmpty(SiteID) ? SiteID : MasterGlobal.SiteID();
            string c_id = !String.IsNullOrEmpty(CID) ? CID : (Consultants.HasConsultant ? Consultants.Data().ID : Guid.Empty.ToString());
            string b_id = !String.IsNullOrEmpty(BID) ? CID : (Branchs.HasBranch ? Branchs.Data().BranchID : Guid.Empty.ToString());
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
            //Get subject from autoresponder without tags

            if (searchFull)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_FC", SITE_ID, c_id, b_id).Replace("</a>","");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_FC", SITE_ID, c_id, b_id);
            }
            else if (searchShort)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_RT", SITE_ID, c_id, b_id).Replace("</a>", " ");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_RT", SITE_ID, c_id, b_id);
            }
            else if (searchCareer)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_LOST_PASSWD", SITE_ID, c_id, b_id).Replace("</a>", " ");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_LOST_PASSWD", SITE_ID, c_id, b_id);
            }
            else if (searchContactUs)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_LA", SITE_ID, c_id, b_id).Replace("</a>", " ");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_LA", SITE_ID, c_id, b_id);
            }
            else if (searchMini)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_MIN", SITE_ID, c_id, b_id).Replace("</a>", " ");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_MIN", SITE_ID, c_id, b_id);
            }
            else if (searchQuick)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_QAP", SITE_ID, c_id, b_id).Replace("</a>", " ");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_QAP", SITE_ID, c_id, b_id);
            }
            else if (searchPassword)
            {
                subject = getHTML_DATA(new BorrowerCo(), "EMAIL_SUBJECT_CAR", SITE_ID, c_id, b_id).Replace("</a>", " ");
                html_body = getHTML_DATA(new BorrowerCo(), "EMAIL_BODY_CAR", SITE_ID, c_id, b_id);
            }
            else
            {
                subject = getHTML_DATA(new BorrowerCo(), "subject", SITE_ID, c_id, b_id);
                html_body = getHTML_DATA(new BorrowerCo(), "html_body", SITE_ID, c_id, b_id);
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
                }else if(!String.IsNullOrEmpty(b_id)){
                    Branchs.Branch branch = Branchs.DataByID(b_id);
                    fullName = branch.Name;
                    recipient = branch.Email;
                    //Add the email to send
                    m.To.Add(new MailAddress(recipient, fullName));
                }
                else
                {
                    //This is to get the recipient dinamic: 
                    recipient = Companys.Data().CompanyEmailNotifications.ToString();//UIF.GetEmailTo();
                    if (recipient != "")
                    {
                        string[] allRecipients = recipient.Split(',');
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
            //AdminClients.Controls.SmtpAPIWrapper cl = AdminClients.EmailEvents.Controls.SmtpHosts.SmtpClientCreaCRM();
            //cl.Send(m);
            #endregion
        }
        #endregion

        #region send AutoResponder User

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        public static void User(BorrowerCo data, string type)
        {
            send_AutoResponderUsers(data, type, MasterGlobal.SiteID(), String.Empty, String.Empty);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        public static void User(BorrowerCo data, string type, string siteID)
        {
            send_AutoResponderUsers(data, type, siteID, String.Empty, String.Empty);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void UserByConsultantID(BorrowerCo data, string type, string cID)
        {
            send_AutoResponderUsers(data, type, MasterGlobal.SiteID(), cID, String.Empty);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void UserByConsultantID(BorrowerCo data, string type, string cID, string siteID)
        {
            send_AutoResponderUsers(data, type, siteID, cID, String.Empty);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void UserByBranchID(BorrowerCo data, string type, string BID)
        {
            send_AutoResponderUsers(data, type, MasterGlobal.SiteID(), String.Empty, BID);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void UserByBranchID(BorrowerCo data, string type, string BID, string siteID)
        {
            send_AutoResponderUsers(data, type, siteID, String.Empty, BID);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void UserByConsultantBranchID(BorrowerCo data, string type, string CID, string BID)
        {
            send_AutoResponderUsers(data, type, MasterGlobal.SiteID(), CID, BID);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        /// <param name="data">All Information Of Borrower</param>
        /// <param name="numberContent">Number Of Content</param>
        /// <param name="siteID">Site ID</param>
        /// <param name="cID">Loan Officer ID</param>
        public static void UserByConsultantBranchID(BorrowerCo data, string type, string CID, string BID, string siteID)
        {
            send_AutoResponderUsers(data, type, siteID, CID, BID);
        }

        /// <summary>
        /// Sent Autoresponder By User (Borrower)
        /// </summary>
        private static void send_AutoResponderUsers(BorrowerCo data, string type, string SiteID, string CID, string BID)
        {
            #region Set initial variables
            string html_body = "";
            string subject = "";
            string recipientFrom = "";
            string SITE_ID = !String.IsNullOrEmpty(SiteID) ? SiteID : MasterGlobal.SiteID();
            string c_id = !String.IsNullOrEmpty(CID) ? CID : (Consultants.HasConsultant ? Consultants.Data().ID : Guid.Empty.ToString());
            string b_id = !String.IsNullOrEmpty(BID) ? CID : (Branchs.HasBranch ? Branchs.Data().BranchID : Guid.Empty.ToString());
            string siteName = Companys.Data().CompanyName;
            string fullName = "";
            string recipient = "";
            MailMessage m = new MailMessage();
            #endregion

            // Ojito 
            #region Get Message
            subject = getHTML_DATA(new BorrowerCo(), "subject", SITE_ID, c_id, b_id);
            html_body = getHTML_DATA(new BorrowerCo(), "html_body", SITE_ID, c_id, b_id);
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
                }else if(!String.IsNullOrEmpty(b_id)){
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
            //AdminClients.Controls.SmtpAPIWrapper cl = AdminClients.EmailEvents.Controls.SmtpHosts.SmtpClientCreaCRM();
            //cl.Send(m);
            #endregion
        }
        #endregion

        #endregion


        #region getHTML_DATA With content number or Without content number
        //This function is auto responder (normal)
        private static string getHTML_DATA(BorrowerCo data, string type, string SiteID, string CID, string BID)
        {
            string body = "";

            try
            {
                using (SqlDataReader reader = cnn.ExecuteReader("SELECT CONTENT_VALUE FROM tblClientPagesContent WHERE SITE_ID=@SITE_ID AND CONTENT_TYPE_NAME=@type",
                            new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", SiteID),
                        new System.Data.SqlClient.SqlParameter("@type", type)}))
                {
                    if (reader.Read())
                    {
                        body = reader["CONTENT_VALUE"].ToString();
                        body = MultiTags.AutoresponderBorrowerByConsultantBranchID(body, CID, BID, data, SiteID);
                    }
                    return body;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


    }
}