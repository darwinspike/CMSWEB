using CMSWeb.Models.Consumable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
namespace CMSWeb.Content.Forms
{
    public partial class ShortApplyNow : System.Web.UI.UserControl
    {
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
            getList();
        }

        //LO List Data Bound
        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            if (drpMortgageSpecialist1.Items.Count > 0)
            {
                drpMortgageSpecialist1.Items.Insert(0, new ListItem("Select A Mortgage Advisor", ""));
                drpMortgageSpecialist1.Items.Insert(1, new ListItem("No, I am not", ""));
            }
            else
            {
                if (Branchs.HasBranch)
                {
                    drpMortgageSpecialist1.Items.Insert(0, new ListItem("Select A Mortgage Advisor", ""));
                    drpMortgageSpecialist1.Items.Insert(1, new ListItem("No, I am not", ""));
                }
                else
                {
                    pnlConsultant.Visible = false;
                    hdfConsultant.Value = "false";
                }
            }
        }

        //Function To Get LO Dropdown List
        public void getList()
        {
            string sql = "";
            if (!Consultants.HasConsultant)
            {
                sql = @"SELECT Consultant_ID, RTRIM(FullName) + ' (' + role + ')' AS FullName FROM tblConsultants with(nolock) 
                    WHERE Site_ID = @SITE_ID AND IS_ACTIVE = 1 ORDER BY FullName";
                SqlDataSource1.SelectCommand = sql;
            }
        }

        //Send Paramenters To the DropDown List
        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@SITE_ID"].Value = MasterGlobal.SiteID();
        }

        //Function To Get The Consultant ID
        private string getConsultant_ID()
        {
            string result = "";
            if (hdfConsultant.Value == "true")
            {
                if (drpMortgageSpecialist1.SelectedIndex > 1)
                {
                    result = drpMortgageSpecialist1.Value.ToString();
                    this.ConsultantName.Text = drpMortgageSpecialist1.Value.ToString();
                }
            }
            else
            {
                result = Consultants.Data().ID.ToString().Trim();
            }
            return result;
        }

        //Function To Get The Purchase Price
        private void SalesPriceOrHomevalue2DB(
            //AdminClients.BusinessLogicLayer.User u
            )
        {
            if (this.edtAPP_LOAN_PURPOSE.Value == "Refinance")
            {
                //          u["Property Value/Price"] = this.PropertyEstimatedValue.Text.Trim();
            }
            else
            {
                //        u["Purchase Price"] = this.PropertyEstimatedValue.Text.Trim();
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
            //Consultant_ID = getConsultant_ID();
            //if (Consultant_ID.Length > 0)
            //{
            //    c_id = new Guid(Consultant_ID);        
            //}
            //#endregion

            //#region Step 1
            //u.SITE_ID = NewClientSites.UIF.Get_SITE_ID();
            //u.LeadSource = LeadSource.Website_ShortApp;
            //u.txtFirstName = this.firstName.Text.Trim();
            //u.txtLastName = this.lastName.Text.Trim();
            //u.txtEmail = this.email.Text.Trim();
            //u.txtPhone = this.phone.Text.Trim();
            //#endregion

            //#region Step 2
            //u["Loan Type"] = this.edtAPP_LOAN_PURPOSE.SelectedValue;
            //u["LoanPurposeRefiType"] = this.edtAPP_REF_LOAN_PURPOSE.SelectedValue;
            //u["LoanPurposeOther"] = this.LoanPurposeOther.Text.Trim();
            //SalesPriceOrHomevalue2DB(u);
            //string txtMarkGroup = "";
            //if (excellent.Checked)
            //{
            //    txtMarkGroup = this.excellent.Text;
            //}
            //else if (good.Checked)
            //{
            //    txtMarkGroup = this.good.Text;
            //}
            //else if (fair.Checked)
            //{
            //    txtMarkGroup = this.fair.Text;
            //}
            //else if (NotSure.Checked)
            //{
            //    txtMarkGroup = this.NotSure.Text;
            //}
            //u["CreditScore"] = txtMarkGroup.Trim();
            //u["LoanProgram"] = this.ddlLoanProgram.SelectedValue;
            //string s = this.LoanAmount.Text.Trim();
            //decimal amt;
            //if (decimal.TryParse(s, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowThousands, null, out amt))
            //{
            //    u.LoanAmount = amt;
            //}
            //u["LoanDownPayment"] = this.LoanDownPayment.Text.Trim();
            //#endregion

            //#region Step 3
            //u.BorrowerAddress = this.StreetAddress.Text.Trim();
            //u.BorrowerCity = this.city.Text.Trim();
            //u.BorrowerState = this.ddlstate.SelectedValue;
            //u.BorrowerZip = this.zip.Text.Trim();
            //u["PropertyType"] = this.ddlPropertyType.SelectedValue;
            //u["PropertyWillBe"] = this.ddlPropertyWillBe.SelectedValue;
            //u["Notes"] = "How did you hear about us? " + this.ddlHowdidyouhearaboutus.SelectedValue;
            //u["AGENT_ID"] = c_id;
            //#endregion

            ////Save changes into the datase
            //return u.ApplyChanges(true, out err, out isDupLeadSource);
            return true;
        }

        //Submit Button
        protected void submit_Click(object sender, EventArgs e)
        {
            if (this.AddLead())
            {
                #region Set Values Borrower Autoresponder
                Autoresponders.BorrowerCo dataForm = new Autoresponders.BorrowerCo();
                dataForm.BorrowerFirstName = this.firstName.Value.Trim();
                dataForm.BorrowerLastName = this.lastName.Value.Trim();
                dataForm.BorrowerAddress = this.StreetAddress.Value.Trim();
                dataForm.BorrowerCity = this.city.Value.Trim();
                dataForm.BorrowerEmail = this.email.Value.Trim();
                dataForm.BorrowerHomePhone = this.phone.Value.Trim();
                dataForm.BorrowerLoanAmount = this.LoanAmount.Value.Trim();
                dataForm.BorrowerState = this.ddlstate.Value;
                dataForm.BorrowerZip = this.zip.Value.Trim();
                #endregion

                //Send autoresponder
                //Autoresponders.General(dataForm, "short");
                Autoresponders.send_AutoResponderLead(dataForm, "short");

                //Hide form section
                this.formData.Visible = false;

                //Show “Thank you” section
                //this.Message.Text = firstName.Text.Trim() + " " + lastName.Text.Trim();
                this.thankYou.Visible = true;
            }
        }
    }
}