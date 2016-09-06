using CMSWeb.Models.Consumable;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace CMSWeb.Content.Forms
{
    public partial class ContactUs :
        //System.Web.UI.UserControl
        ViewUserControl
    {
        #region Generic Variables
        const string PAGEKEY = "c-tUs";
        const string KEY = "KEY";
        #endregion

        #region AntiRobot Logic
        //Generate Key
        string GenKey(string ip, string pagefrefix)
        {
            return (string.Format("{0}_{1}", ip.Trim(), pagefrefix.Trim()));
        }

        //Set Key
        string SetKey(string ip, string pagefrefix)
        {
            string k = GenKey(ip, pagefrefix);
            ViewState[KEY] = k;
            return k;
        }

        //Get Key
        string GetKey()
        {
            if (ViewState[KEY] != null)
            {
                return ViewState[KEY].ToString();
            }
            else
            {
                return "";
            }
        }

        //Generate Random Code
        string GenerateRandomCode()
        {
            string s = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                s = String.Concat(s, r.Next(10).ToString());
            }
            return s;
        }

        //Get Session Value
        string GetSessionValue(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return HttpContext.Current.Session[key].ToString();
            }
            return string.Empty;
        }

        //Set Session Value
        void SetSessionValue(string key)
        {
            HttpContext.Current.Session[key] = GenerateRandomCode();
        }

        //Clear Seesion Value
        void ClearSessionValue(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        //Captcha IMG SRC
        string CaptchaimgSrc(string key)
        {
            return string.Format("/Content/WebServices/Captcha.ashx?key={0}", GetSessionValue(key));
        }

        //Reset Captcha
        void ReSetCaptcha()
        {
            string k = SetKey(Request.UserHostAddress, PAGEKEY);
            SetSessionValue(k);
            captchaimg1.Src = CaptchaimgSrc(k);
        }

        //Is Valid Anti Robot
        bool isValidAntiRobot()
        {
            string userentered = CodeNumberTextBox.Value.Trim();
            CodeNumberTextBox.Value = "";
            string value = GetSessionValue(GetKey());
            if (value.CompareTo(userentered) == 0)
            {
                ClearSessionValue(GetKey());
                return true;
            }
            else
            {
                ReSetCaptcha();
                return false;
            }
        }
        #endregion

        #region Message Logic
        //Is Error
        bool isError()
        {
            try
            {
                return string.IsNullOrEmpty(ViewState["SendErr"].ToString()) ? false : true;
            }
            catch
            {
                return false;
            }
        }

        //Get Error
        string getError()
        {
            try
            {
                return System.String.IsNullOrEmpty(ViewState["SendErr"].ToString()) ? "" : ViewState["SendErr"].ToString();
            }
            catch
            {
                return "";
            }
        }

        //Display Error If Need
        void DisplayErrorIfNeed()
        {
            if (isError())
            {
                this.errLabel.Visible = true;
                this.errLabel.Text = getError();
            }
            else
            {
                this.errLabel.Visible = false;
            }
        }

        //Get Mesage
        string getSentMsg()
        {
            try
            {
                return string.IsNullOrEmpty(ViewState["SentMsg"].ToString()) ? "" : ViewState["SentMsg"].ToString();
            }
            catch
            {
                return "";
            }
        }

        //Display Message If Need
        void DisplaySentMsgIfNeed()
        {
            string s = getSentMsg();
            if (s.Length > 0)
            {
                this.Message.Text = s;
            }
            else
            {
                this.Message.Text = "";
            }
        }
        #endregion

        public string Unsecure;
        public string Secure;
        public string path;
        public string link;
        public string SendEmailAdress;
        public string SendEmailName;
        public bool los;

        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Unsecure = MasterGlobal.GeneralParameter("UnsecureClientPost");
            Secure = MasterGlobal.GeneralParameter("SecureClientPost");
            path = Path.TemplatePath();
            link = URL.GetLink();
            SendEmailAdress = MasterGlobal.SendEmailAddress;
            SendEmailName = MasterGlobal.SendEmailName;
            los = Consultants.HasConsultant; 
            
            if (!IsPostBack)
            {
                ReSetCaptcha();
            }
            DisplayErrorIfNeed();
            DisplaySentMsgIfNeed();
        }

        //Function To Get The User ID
        public Guid User_ID
        {
            set
            { ViewState["User_ID"] = value; }
            get
            {
                if (ViewState["User_ID"] != null)
                {
                    return (Guid)(ViewState["User_ID"]);
                }
                return Guid.Empty;
            }
        }

        //Function To Add Lead
        private bool AddLead()
        {
            //#region Basic variables
            //string err;
            //bool isDupLeadSource;
            //string Consultant_ID = "";
            //Guid c_id = Guid.Empty;
            //#endregion

            //#region Initialize User Class
            //AdminClients.BusinessLogicLayer.User u = new AdminClients.BusinessLogicLayer.User();
            //#endregion

            //#region Get Consultant ID
            //if (NewClientSites.UIF.CurrentLO.HasLo)
            //{
            //    Consultant_ID = NewClientSites.UIF.GetConsultantParam("Consultant_ID").ToString().Trim();
            //}
            //if (Consultant_ID.Length > 0)
            //{
            //    c_id = new Guid(Consultant_ID);
            //}
            //#endregion

            //#region Step 1
            //u.SITE_ID = NewClientSites.UIF.Get_SITE_ID();
            //u.LeadSource = AdminClients.DataAccess.LeadSource.Website_ContactUs;
            //u.txtFirstName = this.FirstName.Text.Trim();
            //u.txtLastName = this.LastName.Text.Trim();
            //u.txtEmail = this.Email.Text.Trim();
            //u.txtPhone = this.Phone.Text.Trim();
            //u.Fax = this.Fax.Text.Trim();
            //u.BorrowerCity = this.City.Text.Trim();
            //u.BorrowerZip = this.Zip.Text.Trim();
            //u.BorrowerState = this.State.SelectedValue;
            //u.BorrowerAddress = this.Address1.Text.Trim();
            //u["BorrowerStreetAddress2"] = this.Address2.Text.Trim();
            //u["Notes"] = this.Notes.Text.Trim();
            //u["AGENT_ID"] = c_id;
            //#endregion

            ////Save changes into the datase
            //return u.ApplyChanges(true, out err, out isDupLeadSource);

            return true;
        }

        //Submit Click
        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!isValidAntiRobot())
            {
                ViewState["SendErr"] = "<br /><br />Anti-robot validation code is wrong! <br>Please enter valid code.";
                DisplayErrorIfNeed();
                return;
            }
            if (AddLead())
            {
                #region Set Values Borrower Autoresponder
                Autoresponders.BorrowerCo dataForm = new Autoresponders.BorrowerCo();
                dataForm.BorrowerFirstName = this.FirstName.Value.Trim();
                dataForm.BorrowerLastName = this.LastName.Value.Trim();
                dataForm.BorrowerAddress = this.Address1.Value.Trim();
                dataForm.BorrowerCity = this.City.Value.Trim();
                dataForm.BorrowerEmail = this.Email.Value.Trim();
                dataForm.BorrowerHomePhone = this.Phone.Value.Trim();
                dataForm.BorrowerState = this.State.Value.Trim();
                dataForm.BorrowerZip = this.Zip.Value.Trim();
                #endregion

                //Send autoresponder
                //Autoresponders.General(dataForm, "contact");
                Autoresponders.send_AutoResponderLead(dataForm, "contact");

                ViewState["SendErr"] = null;
                ViewState["SentMsg"] = "Your information has been sent. Thank You.";

                Panel1.Visible = false;
                Panel2.Visible = true;
            }
        }

    }
}