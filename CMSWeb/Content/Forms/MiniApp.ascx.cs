using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSWeb.Content.Forms
{
    public partial class MiniApp : 
        //System.Web.UI.UserControl
        ViewUserControl
    {
        public string Unsecure;
        public string Secure;
        public string path;
        public string link;
        public string SendEmailAdress;
        public string SendEmailName;

        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Unsecure = CMSWeb.Models.Consumable.MasterGlobal.GeneralParameter("UnsecureClientPost");
            Secure = CMSWeb.Models.Consumable.MasterGlobal.GeneralParameter("SecureClientPost");
            path = CMSWeb.Models.Handler.Path.TemplatePath();
            link = CMSWeb.Models.Handler.URL.GetLink();
            SendEmailAdress = CMSWeb.Models.Consumable.MasterGlobal.SendEmailAddress;
            SendEmailName = CMSWeb.Models.Consumable.MasterGlobal.SendEmailName;

        }

        //Function To Add Lead
        private bool AddLead()
        {
            return true;
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
            //u.LeadSource = AdminClients.DataAccess.LeadSource.Website_MiniApp;
            //u["Loan Type"] = this.loanPurpose.SelectedValue;
            //string s = this.howMuch.SelectedValue;
            //decimal amt;
            //if (decimal.TryParse(s, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowThousands, null, out amt))
            //{
            //    u.LoanAmount = amt;
            //}
            //u["CreditScore"] = this.creditProfile.SelectedValue;
            //u.txtFirstName = this.firstName.Text.Trim();
            //u.txtLastName = this.lastName.Text.Trim();
            //u.txtEmail = this.email.Text.Trim();
            //u.txtPhone = this.phone.Text.Trim();
            //u.BorrowerZip = this.zip.Text.Trim();
            //u["Notes"] = "How did you hear about us? " + this.ddlHowdidyouhearaboutus.SelectedValue;
            //u["AGENT_ID"] = c_id;
            //#endregion

            ////Save changes into the datase
            //return u.ApplyChanges(true, out err, out isDupLeadSource);

        }

        //Submit Button
        protected void Submit_Click1(object sender, EventArgs e)
        {
            if (this.AddLead())
            { 
                #region Set Values Borrower Autoresponder
                CMSWeb.Models.Tools.Autoresponders.BorrowerCo dataForm = new Models.Tools.Autoresponders.BorrowerCo();
                dataForm.BorrowerFirstName = this.firstName.Value.Trim();
                dataForm.BorrowerLastName = this.lastName.Value.Trim();
                dataForm.BorrowerEmail = this.email.Value.Trim();
                dataForm.BorrowerHomePhone = this.phone.Value.Trim();
                dataForm.BorrowerZip = this.zip.Value.Trim();
                #endregion

                //Send autoresponder
                //CMSWeb.Models.Tools.Autoresponders.General(dataForm, "mini");
                CMSWeb.Models.Tools.Autoresponders.send_AutoResponderLead(dataForm, "mini");
            }
            this.formData.Visible = false;
            this.thankYou.Visible = true;
        }    
    
    
    }
}