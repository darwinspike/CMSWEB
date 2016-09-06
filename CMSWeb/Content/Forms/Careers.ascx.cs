using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSWeb.Content.Forms
{
    public partial class Careers : 
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

        //Set Session Value
        void SetSessionValue(string key)
        {
            HttpContext.Current.Session[key] = GenerateRandomCode();
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

        //Clear Session Value
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

        //Is Valid Antirobot   
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

        //Get Sent Message
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

        //Display Sent Message If Need
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

        #region Other Function
        //On Init
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        // Initialize Component
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReSetCaptcha();
            }
            DisplayErrorIfNeed();
            DisplaySentMsgIfNeed();
        }

        //User ID
        public Guid User_ID
        {
            set { ViewState["User_ID"] = value; }
            get
            {
                if (ViewState["User_ID"] != null)
                {
                    return (Guid)(ViewState["User_ID"]);
                }
                return Guid.Empty;
            }
        }

        //Allow Attachments
        public bool AllowAttachments
        {
            set { ViewState["AllowAttachments"] = value; }
            get
            {
                if (ViewState["AllowAttachments"] != null)
                {
                    return (bool)(ViewState["AllowAttachments"]);
                }
                return false;
            }
        }

        //Function To Upload Files
        protected void uploadFiles()
        {
            Connections cnn = new Connections();
            int ATT_SIZE;
            HttpFileCollection Files = Request.Files;
            IList<string> detail = new List<string>();
            string ATT_FILE_NAME, ATT_MIME_TYPE, sql, dataText;
            string[] control = HttpContext.Current.Request.Form.AllKeys;
            string[] arr1 = Files.AllKeys;
            for (int g = 0; g < control.Length; g++)
            {
                dataText = control[g];
                if (dataText.ToLower().StartsWith("description"))
                {
                    detail.Add(HttpContext.Current.Request.Form[g].ToString());
                }
            }
            for (int i = 0; i < arr1.Length; i++)
            {
                ATT_FILE_NAME = System.IO.Path.GetFileName(Files[i].FileName).ToLower();
                ATT_MIME_TYPE = Files[i].ContentType;
                ATT_SIZE = Files[i].ContentLength;
                byte[] buffer = new byte[ATT_SIZE];
                //Modify without save as
                Files[i].SaveAs(System.IO.Path.GetFullPath(ATT_FILE_NAME));
                buffer = System.IO.File.ReadAllBytes(System.IO.Path.GetFullPath(ATT_FILE_NAME));
                //end modify
                sql = "";
                sql = @"INSERT INTO tblUserDocuments(USER_ID, Document_DESCRIPTION, Document,  Document_FILE_NAME, Document_MIME_TYPE, Document_SIZE) 
                    VALUES (@USER_ID, @Document_DESCRIPTION, @Document,  @Document_FILE_NAME, @Document_MIME_TYPE, @Document_SIZE)";
                using (SqlDataReader reader = cnn.ExecuteReader(sql,
                        new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@USER_ID", User_ID),
                        new System.Data.SqlClient.SqlParameter("@Document_DESCRIPTION", detail[i].ToString()),
                        new System.Data.SqlClient.SqlParameter("@Document", buffer),
                        new System.Data.SqlClient.SqlParameter("@Document_FILE_NAME", ATT_FILE_NAME),
                        new System.Data.SqlClient.SqlParameter("@Document_MIME_TYPE", ATT_MIME_TYPE),
                        new System.Data.SqlClient.SqlParameter("@Document_SIZE", ATT_SIZE)})) { }
            }
        }

        //Add Lead
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
            //u.LeadSource = AdminClients.DataAccess.LeadSource.Website_Careers;
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
            //string n = this.Notes.Text.Trim();
            //if (this.CareerPositions.SelectedValue.Length > 0)
            //{
            //    n = string.Format("CareerPosition: {1}; \n {0}", n, this.CareerPositions.SelectedValue);
            //}
            //if (this.YearsOfExperience1.SelectedValue.Length > 0)
            //{
            //    n = string.Format("Years of industry experience: {1}; \n {0}", n, this.YearsOfExperience1.SelectedValue);
            //}
            //if (this.Licensed1.SelectedValue.Length > 0)
            //{
            //    n = string.Format("Are you currently licensed: {1}; \n {0}", n, this.Licensed1.SelectedValue);
            //}
            //u["Notes"] = n;
            //u["AGENT_ID"] = c_id;
            //#endregion

            ////Save changes into the datase
            //return u.ApplyChanges(true, out err, out isDupLeadSource);
            return true;
        }

        //Submint Button
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!isValidAntiRobot())
            {
                ViewState["SendErr"] = "<br /><br />Anti-robot validation code is wrong! <br>Please enter valid code.";
                DisplayErrorIfNeed();
                return;
            }
            if (AddLead())
            {
                if (this.IsAttachments.Checked == true)
                {
                    uploadFiles();
                }
                #region Set Borrower Values
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
                //Autoresponders.General(dataForm, "career");
                Autoresponders.send_AutoResponderLead(dataForm, "career");

                ViewState["SendErr"] = null;
                ViewState["SentMsg"] = "Your information has been sent. Thank You.";

                formData.Visible = false;
                Panel2.Visible = true;
            }
        } 
    }
}