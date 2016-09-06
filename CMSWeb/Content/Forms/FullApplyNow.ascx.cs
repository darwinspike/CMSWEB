using CMSWeb.Models.Consumable;
using CMSWeb.Models.Generic;
using CMSWeb.Models.Handler;
using CMSWeb.Models.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSWeb.Content.Forms
{
    public partial class FullApplyNow : 
        //System.Web.UI.UserControl
        ViewUserControl
    {
        //Class To Fill And Set All Dropdowns
        protected class fullDropDowns
        {
            //Variable To Set Values
            private List<KeyValuePair<string, string>> data;

            //Constructor
            public fullDropDowns()
            {
            }

            //Fill The Constructor
            public fullDropDowns(string type)
            {
                this.data = new List<KeyValuePair<string, string>>();
                switch (type)
                {
                    case "LoanPurpose":
                        #region
                        this.data.Add(new KeyValuePair<string, string>("Purchase", "Purchase"));
                        this.data.Add(new KeyValuePair<string, string>("Refinance", "Refinance"));
                        this.data.Add(new KeyValuePair<string, string>("Construction", "Construction"));
                        this.data.Add(new KeyValuePair<string, string>("Construction - Perm", "Construction - Perm"));
                        this.data.Add(new KeyValuePair<string, string>("Other", "Other"));
                        break;
                        #endregion
                    case "Liabilities":
                        #region
                        this.data.Add(new KeyValuePair<string, string>("Select an Option", ""));
                        this.data.Add(new KeyValuePair<string, string>("Child Care", "Child Care"));
                        this.data.Add(new KeyValuePair<string, string>("Child Support", "Child Support"));
                        this.data.Add(new KeyValuePair<string, string>("Collections Judgments And Liens", "Collections Judgments And Liens"));
                        this.data.Add(new KeyValuePair<string, string>("HELOC", "HELOC"));
                        this.data.Add(new KeyValuePair<string, string>("Installment", "Installment"));
                        this.data.Add(new KeyValuePair<string, string>("Lease Payments", "Lease Payments"));
                        this.data.Add(new KeyValuePair<string, string>("Mortgage", "Mortgage"));
                        this.data.Add(new KeyValuePair<string, string>("Open 30 Days Charge Account", "Open 30 Days Charge Account"));
                        this.data.Add(new KeyValuePair<string, string>("Other Liability", "Other Liability"));
                        this.data.Add(new KeyValuePair<string, string>("Revolving", "Revolving"));
                        this.data.Add(new KeyValuePair<string, string>("Separate Maintenance Expense", "Separate Maintenance Expense"));
                        this.data.Add(new KeyValuePair<string, string>("Other Expense", "Other Expense"));
                        this.data.Add(new KeyValuePair<string, string>("Taxes", "Taxes"));
                        break;
                        #endregion
                    case "RealStatus":
                        #region
                        this.data.Add(new KeyValuePair<string, string>("Please Select", ""));
                        this.data.Add(new KeyValuePair<string, string>("Already Sold, Sold", "Already Sold, Sold"));
                        this.data.Add(new KeyValuePair<string, string>("Pending Sale", "Pending Sale"));
                        this.data.Add(new KeyValuePair<string, string>("Rental Being Held for Income, Rental", "Rental Being Held for Income, Rental"));
                        this.data.Add(new KeyValuePair<string, string>("Will Remain or Become Primary", "Will Remain or Become Primary"));
                        break;
                        #endregion
                    case "PropertyType":
                        #region
                        this.data.Add(new KeyValuePair<string, string>("Select an Option", ""));
                        this.data.Add(new KeyValuePair<string, string>("Single Family", "Single Family"));
                        this.data.Add(new KeyValuePair<string, string>("Co-Operative", "Co-Operative"));
                        this.data.Add(new KeyValuePair<string, string>("Commercial - Non-Residential", "Commercial - Non-Residential"));
                        this.data.Add(new KeyValuePair<string, string>("Condominium", "Condominium"));
                        this.data.Add(new KeyValuePair<string, string>("Farm", "Farm"));
                        this.data.Add(new KeyValuePair<string, string>("Home - Business Combined", "Home - Business Combined"));
                        this.data.Add(new KeyValuePair<string, string>("Land", "Land"));
                        this.data.Add(new KeyValuePair<string, string>("Manufactured/Mobile Home", "Manufactured/Mobile Home"));
                        this.data.Add(new KeyValuePair<string, string>("Mixed Use - Residential", "Mixed Use - Residential"));
                        this.data.Add(new KeyValuePair<string, string>("Multifamily (More than 4 units", "Multifamily (More than 4 units)"));
                        this.data.Add(new KeyValuePair<string, string>("Townhouse", "Townhouse"));
                        this.data.Add(new KeyValuePair<string, string>("Two-to-Four-Unit Property", "Two-to-Four-Unit Property"));
                        break;
                        #endregion
                    case "State":
                        #region
                        this.data.Add(new KeyValuePair<string, string>("Please select", ""));
                        this.data.Add(new KeyValuePair<string, string>("Alabama", "AL"));
                        this.data.Add(new KeyValuePair<string, string>("Alaska", "AK"));
                        this.data.Add(new KeyValuePair<string, string>("American Samoa", "AS"));
                        this.data.Add(new KeyValuePair<string, string>("Arizona", "AZ"));
                        this.data.Add(new KeyValuePair<string, string>("Arkansas", "AR"));
                        this.data.Add(new KeyValuePair<string, string>("California", "CA"));
                        this.data.Add(new KeyValuePair<string, string>("Colorado", "CO"));
                        this.data.Add(new KeyValuePair<string, string>("Connecticut", "CT"));
                        this.data.Add(new KeyValuePair<string, string>("Delaware", "DE"));
                        this.data.Add(new KeyValuePair<string, string>("District of Columbia", "DC"));
                        this.data.Add(new KeyValuePair<string, string>("Dominican Republic", "DO"));
                        this.data.Add(new KeyValuePair<string, string>("Federated states of Micronesia", "FM"));
                        this.data.Add(new KeyValuePair<string, string>("Florida", "FL"));
                        this.data.Add(new KeyValuePair<string, string>("Georgia", "GA"));
                        this.data.Add(new KeyValuePair<string, string>("Guam", "GU"));
                        this.data.Add(new KeyValuePair<string, string>("Hawaii", "HI"));
                        this.data.Add(new KeyValuePair<string, string>("Idaho", "ID"));
                        this.data.Add(new KeyValuePair<string, string>("Illinois", "IL"));
                        this.data.Add(new KeyValuePair<string, string>("Indiana", "IN"));
                        this.data.Add(new KeyValuePair<string, string>("Iowa", "IA"));
                        this.data.Add(new KeyValuePair<string, string>("Kansas", "KS"));
                        this.data.Add(new KeyValuePair<string, string>("Kentucky", "KY"));
                        this.data.Add(new KeyValuePair<string, string>("Louisiana", "LA"));
                        this.data.Add(new KeyValuePair<string, string>("Maine", "ME"));
                        this.data.Add(new KeyValuePair<string, string>("Marshall Islands", "MH"));
                        this.data.Add(new KeyValuePair<string, string>("Maryland", "MD"));
                        this.data.Add(new KeyValuePair<string, string>("Massachusetts", "MA"));
                        this.data.Add(new KeyValuePair<string, string>("Michigan", "MI"));
                        this.data.Add(new KeyValuePair<string, string>("Minnesota", "MN"));
                        this.data.Add(new KeyValuePair<string, string>("Mississippi", "MS"));
                        this.data.Add(new KeyValuePair<string, string>("Missouri", "MO"));
                        this.data.Add(new KeyValuePair<string, string>("Montana", "MT"));
                        this.data.Add(new KeyValuePair<string, string>("Nebraska", "NE"));
                        this.data.Add(new KeyValuePair<string, string>("Nevada", "NV"));
                        this.data.Add(new KeyValuePair<string, string>("New Hampshire", "NH"));
                        this.data.Add(new KeyValuePair<string, string>("New Jersey", "NJ"));
                        this.data.Add(new KeyValuePair<string, string>("New Mexico", "NM"));
                        this.data.Add(new KeyValuePair<string, string>("New York", "NY"));
                        this.data.Add(new KeyValuePair<string, string>("North Carolina", "NC"));
                        this.data.Add(new KeyValuePair<string, string>("North Dakota", "ND"));
                        this.data.Add(new KeyValuePair<string, string>("Northern Mariana Islands", "MP"));
                        this.data.Add(new KeyValuePair<string, string>("Ohio", "OH"));
                        this.data.Add(new KeyValuePair<string, string>("Oklahoma", "OK"));
                        this.data.Add(new KeyValuePair<string, string>("Oregon", "OR"));
                        this.data.Add(new KeyValuePair<string, string>("Palau", "PW"));
                        this.data.Add(new KeyValuePair<string, string>("Pennsylvania", "PA"));
                        this.data.Add(new KeyValuePair<string, string>("Puerto Rico", "PR"));
                        this.data.Add(new KeyValuePair<string, string>("Rhode Island", "RI"));
                        this.data.Add(new KeyValuePair<string, string>("South Carolina", "SC"));
                        this.data.Add(new KeyValuePair<string, string>("South Dakota", "SD"));
                        this.data.Add(new KeyValuePair<string, string>("Tennessee", "TN"));
                        this.data.Add(new KeyValuePair<string, string>("Texas", "TX"));
                        this.data.Add(new KeyValuePair<string, string>("Utah", "UT"));
                        this.data.Add(new KeyValuePair<string, string>("Vermont", "VT"));
                        this.data.Add(new KeyValuePair<string, string>("Virgin Islands", "VI"));
                        this.data.Add(new KeyValuePair<string, string>("Virginia", "VA"));
                        this.data.Add(new KeyValuePair<string, string>("Washington", "WA"));
                        this.data.Add(new KeyValuePair<string, string>("West Virginia", "WV"));
                        this.data.Add(new KeyValuePair<string, string>("Wisconsin", "WI"));
                        this.data.Add(new KeyValuePair<string, string>("Wyoming", "WY"));
                        break;
                        #endregion
                    case "RealPropertyType":
                        #region
                        this.data.Add(new KeyValuePair<string, string>("Select an Option", ""));
                        this.data.Add(new KeyValuePair<string, string>("Single Family", "Single Family"));
                        this.data.Add(new KeyValuePair<string, string>("Co-Operative", "Co-Operative"));
                        this.data.Add(new KeyValuePair<string, string>("Commercial - Non-Residential", "Commercial - Non-Residential"));
                        this.data.Add(new KeyValuePair<string, string>("Condominium", "Condominium"));
                        this.data.Add(new KeyValuePair<string, string>("Farm", "Farm"));
                        this.data.Add(new KeyValuePair<string, string>("Home - Business Combined", "Home - Business Combined"));
                        this.data.Add(new KeyValuePair<string, string>("Land", "Land"));
                        this.data.Add(new KeyValuePair<string, string>("Manufactured/Mobile Home", "Manufactured/Mobile Home"));
                        this.data.Add(new KeyValuePair<string, string>("Mixed Use - Residential", "Mixed Use - Residential"));
                        this.data.Add(new KeyValuePair<string, string>("Multifamily (More than 4 units", "Multifamily (More than 4 units)"));
                        this.data.Add(new KeyValuePair<string, string>("Townhouse", "Townhouse"));
                        this.data.Add(new KeyValuePair<string, string>("Two-to-Four-Unit Property", "Two-to-Four-Unit Property"));
                        break;
                        #endregion
                }
            }

            //Set Values To The Dropdown
            public void setData(DropDownList dd)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    dd.Items.Add(new ListItem(this.data[i].Key, this.data[i].Value));
                }
            }

            //Clear Previous Data
            public void clearData()
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    data.Remove(new KeyValuePair<string, string>(data[i].Key, data[i].Value));
                }
            }
        }

        //Guid U ID
        Guid uid()
        {
            if (u.Value != string.Empty)
            {
                return new Guid(u.Value);
            }
            else
            {
                return Guid.Empty;
            }
        }

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
                fullDropDowns fDD = new fullDropDowns();

                #region Set Loan Purpose Data Dropdown
                fDD = new fullDropDowns("LoanPurpose");
                fDD.setData(edtAPP_LOAN_PURPOSE);
                fDD.clearData();
                #endregion

                #region Set State Data Dropdown
                fDD = new fullDropDowns("State");
                fDD.setData(edtAPP_PROPERTY_STATE);
                fDD.setData(edtAPP_CB_EMP_STATE);
                fDD.setData(edtAPP_PB_CURR_STATE);
                fDD.setData(edtAPP_CB_CURR_STATE);
                fDD.setData(edtAPP_PB_EMP_STATE);
                fDD.setData(edtAPP_CB_EMP_STATE);
                fDD.setData(edtAPP_REAL_PROPERTY_STATE);
                fDD.setData(edtAPP_REAL_PROPERTY_STATE2);
                fDD.setData(edtAPP_REAL_PROPERTY_STATE3);
                fDD.clearData();
                #endregion

                #region Set Liabilities Data Dropdown
                fDD = new fullDropDowns("Liabilities");
                fDD.setData(LiaType1);
                fDD.setData(LiaType2);
                fDD.setData(LiaType3);
                fDD.setData(LiaType4);
                fDD.setData(LiaType5);
                fDD.setData(LiaType6);
                fDD.setData(LiaType7);
                fDD.clearData();
                #endregion

                #region Set Real Status Data Dropdown
                fDD = new fullDropDowns("RealStatus");
                fDD.setData(edtAPP_REAL_STATUS);
                fDD.setData(edtAPP_REAL_STATUS2);
                fDD.setData(edtAPP_REAL_STATUS3);
                fDD.clearData();
                #endregion

                #region Set Real Property Type Data Dropdown
                fDD = new fullDropDowns("RealPropertyType");
                fDD.setData(edtAPP_REAL_PROPERTY_TYPE);
                fDD.setData(edtAPP_REAL_PROPERTY_TYPE2);
                fDD.setData(edtAPP_REAL_PROPERTY_TYPE3);
                fDD.clearData();
                #endregion

                for (int i = 1; i < 5; i++)
                {
                    edtAPP_UNITS_NUM.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
        }

        //Function To Set Dropdow Values
        private void SetDropDownValue(DropDownList dd, string value)
        {
            ListItem li = dd.Items.FindByValue(value);
            if (null == li)
            {
                li = new ListItem(value);
                dd.Items.Add(li);
            }
            dd.SelectedIndex = -1;
            li.Selected = true;
        }

        //Function To Get Sales Price Or Home Value
        private void DB2SalesPriceOrHomevalue(
            //AdminClients.BusinessLogicLayer.User u
            )
        {
            //if (this.edtAPP_LOAN_PURPOSE.SelectedValue == "Refinance")
            //{
            //    this.edtHOME_VALUE.Value = u["Property Value/Price"].ToString();
            //}
            //else
            //{
            //    this.edtHOME_VALUE.Value = u["Purchase Price"].ToString();
            //}
        }

        //Function To Set Sales Price Or Home Value
        private void SalesPriceOrHomevalue2DB(
            //AdminClients.BusinessLogicLayer.User u
            )
        {
            if (this.edtAPP_LOAN_PURPOSE.SelectedValue == "Refinance")
            {
               // u["Property Value/Price"] = this.edtHOME_VALUE.Value.Trim();
            }
            else
            {
              //  u["Purchase Price"] = this.edtHOME_VALUE.Value.Trim();
            }
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
                }
            }
            else
            {
                result = Consultants.Data().ID;
            }
            return result;
        }

        //Function To Set Values From A Previous Registration
        private bool SetFields(Guid userid)
        {
            //#region Basic Variables
            //string s;
            //bool b;
            //#endregion

            //#region Set User Class
            //AdminClients.BusinessLogicLayer.User u = new AdminClients.DataAccess.SQLDataAccess().GetUser(userid);
            //#endregion

            ////Check if the user is null if it's return
            //if (u == null)
            //{
            //    return false;
            //}

            //#region Set Steps
            //if (!string.IsNullOrEmpty(u["Notes"].ToString()))
            //{
            //    MultiView1.ActiveViewIndex = Convert.ToInt32(u["Notes"].ToString());
            //}
            //#endregion

            //#region Step 2
            //s = u["Loan Type"].ToString();
            //edtAPP_LOAN_PURPOSE.SelectedValue = u["Loan Type"].ToString();
            //if (s == "Other")
            //{
            //    this.LoanPurposeOther.Value = u["LoanPurposeOther"].ToString();
            //}
            //this.edtAPP_REF_LOAN_PURPOSE.SelectedValue = u["LoanPurposeRefiType"].ToString();
            //DB2SalesPriceOrHomevalue(u);
            //edtLOAN_AMOUNT.Value = u.LoanAmount.ToString();
            //s = u["SalesDetailsFoundAHome"].ToString();
            //if (s == "not found")
            //{
            //    edtPROPERTY_NOT_FOUND.Checked = true;
            //}
            //edtAPP_PROPERTY_ADDRESS.Value = u.txtPropertyAdress;
            //edtAPP_PROPERTY_CURR_CITY.Value = u.txtCity;
            //edtAPP_PROPERTY_STATE.SelectedValue = u.txtState;
            //edtAPP_PROPERTY_ZIP.Value = u.intZip;
            //edtAPP_PROPERTY_COUNTY.Value = u["Property County"].ToString();
            //edtAPP_UNITS_NUM.SelectedValue = u["Num Of Units"].ToString();
            //s = u["pWillBeIsPrimary"].ToString();
            //if (bool.TryParse(s, out b))
            //{
            //    this.propertyWillBeIsPrimary.Checked = b;
            //}
            //s = u["pWillBeIsSecondary"].ToString();
            //if (bool.TryParse(s, out b))
            //{
            //    this.propertyWillBeIsSecondary.Checked = b;
            //}
            //s = u["pWillBeIsInvestment"].ToString();
            //if (bool.TryParse(s, out b))
            //{
            //    this.propertyWillBeIsInvestment.Checked = b;
            //}
            //s = u.LoanType;
            //DropDownLoanType.SelectedValue = u.LoanType;
            //if (s == "Other")
            //{
            //    LoanTypeOther.Value = u["LoanTypeOther"].ToString();
            //}
            //#endregion

            //#region Step 3
            ////Borrower
            //edtAPP_PB_FIRST_NAME.Value = u.txtFirstName;
            //edtAPP_PB_LAST_NAME.Value = u.txtLastName;
            //edtAPP_PB_MIDDLE_INITIALS.Value = u.txtMiddleName;
            ////this.edtAPP_PB_SOC_NO.Value = u["SocNum"].ToString();
            //edtAPP_PB_MARITAL.SelectedValue = u["MartialStatus"].ToString();
            //edtAPP_PB_DOB.Value = u["DOB"].ToString();
            //edtAPP_PB_YEARS_IN_SCOOL.Value = u["AssetYearsOfSchool"].ToString();
            //edtAPP_PB_DEPENDANTS_NO.Value = u["DependentsNum"].ToString();
            //edtAPP_PB_DEPENDANTS_AGE.Value = u["DependentsAges"].ToString();
            //txtPhone.Value = u.txtPhone;
            //txtCellPhone.Value = u.txtCellPhone;
            //WorkPhone.Value = u.WorkPhone;
            //edtAPP_PB_CURR_ADDRESS.Value = u.BorrowerAddress;
            //edtAPP_PB_CURR_CITY.Value = u.BorrowerCity;
            //edtAPP_PB_CURR_STATE.SelectedValue = u.BorrowerState;
            //edtAPP_PB_CURR_ZIP.Value = u.BorrowerZip;
            //this.edtAPP_PB_HOW_LONG_YEARS.Value = u["TimeAtResidenceYears"].ToString();
            //this.edtAPP_PB_HOW_LONG_MONTHS.Value = u["TimeAtResidenceMonths"].ToString();
            //this.edtAPP_PB_OWNERSHIP.SelectedValue = u["Ownership"].ToString();

            ////Co-Borrower
            //edtCoBorrowerEmail.Value = u["CoEmail"].ToString();
            //edtAPP_CB_FIRST_NAME.Value = u.CoFirstName;
            //edtAPP_CB_LAST_NAME.Value = u.CoLastName;
            //edtAPP_CB_MIDDLE_INITIALS.Value = u["CoBorrowerMiddleName"].ToString();
            ////this.edtAPP_CB_SOC_NO.Value = u["CoSocNum"].ToString();
            //this.edtAPP_CB_MARITAL.SelectedValue = u["CoMartialStatus"].ToString();
            //this.edtAPP_CB_DOB.Value = u["CoDOB"].ToString();
            //this.edtAPP_CB_YEARS_IN_SCOOL.Value = u["CoBoAssetYearsOfSchool"].ToString();
            //this.edtAPP_CB_DEPENDANTS_NO.Value = u["CoBoDependentsNum"].ToString();
            //this.edtAPP_CB_DEPENDANTS_AGE.Value = u["CoBoDependentsAges"].ToString();
            //this.CoWorkPhone.Value = u.CoWorkPhone;
            //this.CoPhone.Value = u.CoPhone;
            //this.CoCellPhone.Value = u.CoCellPhone;
            //this.edtAPP_CB_CURR_ADDRESS.Value = u.CoAddress;
            //this.edtAPP_CB_CURR_CITY.Value = u.CoCity;
            //this.edtAPP_CB_CURR_STATE.SelectedValue = u.CoState;
            //this.edtAPP_CB_CURR_ZIP.Value = u.CoZip;
            //this.edtAPP_CB_HOW_LONG_YEARS.Value = u["CoTimeAtResidenceYears"].ToString();
            //this.edtAPP_CB_HOW_LONG_MONTHS.Value = u["CoTimeAtResidenceMonths"].ToString();
            //this.edtAPP_CB_OWNERSHIP.SelectedValue = u["CoOwnership"].ToString();

            ////Hidden but needed
            //this.DropDownPreferredLanguage.Value = u["Preferred Language"].ToString();
            //#endregion

            //#region Step 4
            ////Borrower
            //edtAPP_PB_EMP_NAME.Value = u.txtCompany;
            //edtAPP_PB_EMP_ADDRESS.Value = u["EmployerAddress"].ToString();
            //edtAPP_PB_EMP_CITY.Value = u["EmployerCity"].ToString();
            //edtAPP_PB_EMP_STATE.SelectedValue = u["EmployerState"].ToString();
            //edtAPP_PB_EMP_ZIP.Value = u["EmployerZip"].ToString();
            //edtAPP_PB_EMP_PHONE.Value = u["EmployerPhone"].ToString();
            //edtAPP_PB_EMP_TITLE.Value = u["EmployerTitle"].ToString();
            //if (bool.TryParse(u["SelfEmployed"].ToString(), out b))
            //{
            //    edtAPP_PB_EMP_SELF.Checked = b;
            //}
            //edtAPP_PB_EMP_YEARS.Value = u["YearsWithEmloyer"].ToString();
            //edtAPP_PB_EMP_MONTHS.Value = u["MonthsWithEmloyer"].ToString();

            ////CO-Borrower
            //edtAPP_CB_EMP_NAME.Value = u["CoEmployerName"].ToString();
            //edtAPP_CB_EMP_ADDRESS.Value = u["CoEmployerAddress"].ToString();
            //edtAPP_CB_EMP_CITY.Value = u["CoEmployerCity"].ToString();
            //edtAPP_CB_EMP_STATE.SelectedValue = u["CoEmployerState"].ToString();
            //edtAPP_CB_EMP_ZIP.Value = u["CoEmployerZip"].ToString();
            //edtAPP_CB_EMP_PHONE.Value = u["CoEmployerPhone"].ToString();
            //edtAPP_CB_EMP_TITLE.Value = u["CoEmployerTitle"].ToString();
            //if (bool.TryParse(u["CoSelfEmployed"].ToString(), out b))
            //{
            //    edtAPP_CB_EMP_SELF.Checked = b;
            //}
            //edtAPP_CB_EMP_YEARS.Value = u["CoYearsWithEmloyer"].ToString();
            //edtAPP_CB_EMP_MONTHS.Value = u["CoMonthsWithEmloyer"].ToString();
            //#endregion

            //#region Step 5
            ////Borrower
            //edtAPP_INC_BASE.Value = u["BaseEmploymentIncome"].ToString();
            //edtAPP_INC_OVERTIME.Value = u["Overtime"].ToString();
            //edtAPP_INC_BONUSES.Value = u["Bonuses"].ToString();
            //edtAPP_INC_COMMISSIONS.Value = u["Commissions"].ToString();
            //edtAPP_INC_DIVIDENTS.Value = u["Dividends"].ToString();
            //edtAPP_INC_RENTAL_INC.Value = u["NetRentalIncome"].ToString();

            ////CO-Borrower
            //edtAPP_INC_BASE_CB.Value = u["CoBaseEmploymentIncome"].ToString();
            //edtAPP_INC_OVERTIME_CB.Value = u["CoOvertime"].ToString();
            //edtAPP_INC_BONUSES_CB.Value = u["CoBonuses"].ToString();
            //edtAPP_INC_COMMISSIONS_CB.Value = u["CoCommissions"].ToString();
            //edtAPP_INC_DIVIDENTS_CB.Value = u["CoDividends"].ToString();
            //edtAPP_INC_RENTAL_INC_CB.Value = u["CoNetRentalIncome"].ToString();
            //#endregion

            //#region Step 6
            //CashDepositDescr1.Value = u["CashDepositDescr1"].ToString();
            //CashDepositVal1.Value = u["CashDepositVal1"].ToString();
            //CashDepositDescr2.Value = u["CashDepositDescr2"].ToString();
            //CashDepositVal2.Value = u["CashDepositVal2"].ToString();
            //StocksBondsCompNameAccount1.Value = u["StocksBondsCompNameAccount1"].ToString();
            //StocksBondsVal1.Value = u["StocksBondsVal1"].ToString();
            //StocksBondsCompNameAccount2.Value = u["StocksBondsCompNameAccount2"].ToString();
            //StocksBondsVal2.Value = u["StocksBondsVal2"].ToString();
            //StocksBondsCompNameAccount3.Value = u["StocksBondsCompNameAccount3"].ToString();
            //StocksBondsVal3.Value = u["StocksBondsVal3"].ToString();
            //LInsuranceFaceAmount.Value = u["LInsuranceFaceAmount"].ToString();
            //LInsuranceMarketValue.Value = u["LInsuranceMarketValue"].ToString();
            //VestedInterestInRF.Value = u["VestedInterestInRF"].ToString();
            //NetWorthOfBusinessOwned.Value = u["NetWorthOfBusinessOwned"].ToString();
            //AutoMakeAndYear1.Value = u["AutoMakeAndYear1"].ToString();
            //AutoMakeAndYear2.Value = u["AutoMakeAndYear2"].ToString();
            //AutoMakeAndYear3.Value = u["AutoMakeAndYear3"].ToString();
            //AutoVal1.Value = u["AutoVal1"].ToString();
            //AutoVal2.Value = u["AutoVal2"].ToString();
            //AutoVal3.Value = u["AutoVal3"].ToString();
            //AssetsOtherDescr1.Value = u["AssetsOtherDescr1"].ToString();
            //AssetsOtherVal1.Value = u["AssetsOtherVal1"].ToString();
            //AssetsOtherDescr2.Value = u["AssetsOtherDescr2"].ToString();
            //AssetsOtherVal2.Value = u["AssetsOtherVal2"].ToString();
            //AssetsOtherDescr3.Value = u["AssetsOtherDescr3"].ToString();
            //AssetsOtherVal3.Value = u["AssetsOtherVal3"].ToString();
            //AssetsOtherDescr4.Value = u["AssetsOtherDescr4"].ToString();
            //AssetsOtherVal4.Value = u["AssetsOtherVal4"].ToString();
            //AssetType1.Value = u["AssetType1"].ToString();
            //AssetInstitution1.Value = u["AssetInstitution1"].ToString();
            //AssetAccount1.Value = u["AssetAccount1"].ToString();
            //AssetBalance1.Value = u["AssetBalance1"].ToString();
            //AssetType2.Value = u["AssetType2"].ToString();
            //AssetInstitution2.Value = u["AssetInstitution2"].ToString();
            //AssetAccount2.Value = u["AssetAccount2"].ToString();
            //AssetBalance2.Value = u["AssetBalance2"].ToString();
            //AssetType3.Value = u["AssetType3"].ToString();
            //AssetInstitution3.Value = u["AssetInstitution3"].ToString();
            //AssetAccount3.Value = u["AssetAccount3"].ToString();
            //AssetBalance3.Value = u["AssetBalance3"].ToString();
            //AssetType4.Value = u["AssetType4"].ToString();
            //AssetInstitution4.Value = u["AssetInstitution4"].ToString();
            //AssetAccount4.Value = u["AssetAccount4"].ToString();
            //AssetBalance4.Value = u["AssetBalance4"].ToString();
            //#endregion

            //#region Step 7
            //LiaCompanyName1.Value = u["LiaCompanyName1"].ToString();
            //SetDropDownValue(LiaType1, u["LiaType1"].ToString());
            //LiaBalance1.Value = u["LiaBalance1"].ToString();
            //LiaPayment1.Value = u["LiaPayment1"].ToString();
            //LiaMosLeft1.Value = u["LiaMosLeft1"].ToString();
            //if (bool.TryParse(u["LiaPaidOff1"].ToString(), out b))
            //{
            //    LiaPaidOff1.Checked = b;
            //}
            //LiaCompanyName2.Value = u["LiaCompanyName2"].ToString();
            //SetDropDownValue(LiaType2, u["LiaType2"].ToString());
            //LiaBalance2.Value = u["LiaBalance2"].ToString();
            //LiaPayment2.Value = u["LiaPayment2"].ToString();
            //LiaMosLeft2.Value = u["LiaMosLeft2"].ToString();
            //if (bool.TryParse(u["LiaPaidOff2"].ToString(), out b))
            //{
            //    LiaPaidOff2.Checked = b;
            //}
            //LiaCompanyName3.Value = u["LiaCompanyName3"].ToString();
            //SetDropDownValue(LiaType3, u["LiaType3"].ToString());
            //LiaBalance3.Value = u["LiaBalance3"].ToString();
            //LiaPayment3.Value = u["LiaPayment3"].ToString();
            //LiaMosLeft3.Value = u["LiaMosLeft3"].ToString();
            //if (bool.TryParse(u["LiaPaidOff3"].ToString(), out b))
            //{
            //    LiaPaidOff3.Checked = b;
            //}
            //LiaCompanyName4.Value = u["LiaCompanyName4"].ToString();
            //SetDropDownValue(LiaType4, u["LiaType4"].ToString());
            //LiaBalance4.Value = u["LiaBalance4"].ToString();
            //LiaPayment4.Value = u["LiaPayment4"].ToString();
            //LiaMosLeft4.Value = u["LiaMosLeft4"].ToString();
            //if (bool.TryParse(u["LiaPaidOff4"].ToString(), out b))
            //{
            //    LiaPaidOff4.Checked = b;
            //}
            //LiaCompanyName5.Value = u["LiaCompanyName5"].ToString();
            //SetDropDownValue(LiaType5, u["LiaType5"].ToString());
            //LiaBalance5.Value = u["LiaBalance5"].ToString();
            //LiaPayment5.Value = u["LiaPayment5"].ToString();
            //LiaMosLeft5.Value = u["LiaMosLeft5"].ToString();
            //if (bool.TryParse(u["LiaPaidOff5"].ToString(), out b))
            //{
            //    LiaPaidOff5.Checked = b;
            //}
            //LiaCompanyName6.Value = u["LiaCompanyName6"].ToString();
            //SetDropDownValue(LiaType6, u["LiaType6"].ToString());
            //LiaBalance6.Value = u["LiaBalance6"].ToString();
            //LiaPayment6.Value = u["LiaPayment6"].ToString();
            //LiaMosLeft6.Value = u["LiaMosLeft6"].ToString();
            //if (bool.TryParse(u["LiaPaidOff6"].ToString(), out b))
            //{
            //    LiaPaidOff6.Checked = b;
            //}
            //LiaCompanyName7.Value = u["LiaCompanyName7"].ToString();
            //SetDropDownValue(LiaType7, u["LiaType7"].ToString());
            //LiaBalance7.Value = u["LiaBalance7"].ToString();
            //LiaPayment7.Value = u["LiaPayment7"].ToString();
            //LiaMosLeft7.Value = u["LiaMosLeft7"].ToString();
            //if (bool.TryParse(u["LiaPaidOff7"].ToString(), out b))
            //{
            //    LiaPaidOff7.Checked = b;
            //}
            //#endregion

            //#region Step 8
            //MHERent.Value = u["MHERent"].ToString();
            //MHE1stMrtgP.Value = u["MHE1stMrtgP"].ToString();
            //MHEOthrMrtgP.Value = u["MHEOthrMrtgP"].ToString();
            //MHEHazIns.Value = u["MHEHazIns"].ToString();
            //MHERETaxes.Value = u["MHERETaxes"].ToString();
            //MHEMtgIns.Value = u["MHEMtgIns"].ToString();
            //MHEHOADues.Value = u["MHEHOADues"].ToString();
            //MHEOther.Value = u["MHEOther"].ToString();
            //#endregion

            //#region Step 9
            //edtAPP_REAL_ADDRESS.Value = u["REAL_ADDRESS1"].ToString();
            //edtAPP_REAL_PROPERTY_CITY.Value = u["REAL_PROPERTY_CITY1"].ToString();
            //edtAPP_REAL_PROPERTY_STATE.SelectedValue = u["REAL_PROPERTY_STATE1"].ToString();
            //edtAPP_REAL_PROPERTY_ZIP.Value = u["REAL_PROPERTY_ZIP1"].ToString();
            //edtAPP_REAL_STATUS.SelectedValue = u["REAL_STATUS1"].ToString();
            //edtAPP_REAL_PROPERTY_TYPE.SelectedValue = u["REAL_TYPE1"].ToString();
            //edtAPP_REAL_MARKET_VALUE.Value = u["REAL_MARKET_VALUE1"].ToString();
            //edtAPP_REAL_MORTGAGE.Value = u["REAL_MORTGAGE1"].ToString();
            //edtAPP_REAL_MORT_PAY.Value = u["REAL_MORT_PAY1"].ToString();
            //edtAPP_REAL_MONTH_PAY.Value = u["REAL_MONTH_PAY1"].ToString();
            //edtAPP_REAL_ADDRESS2.Value = u["REAL_ADDRESS2"].ToString();
            //edtAPP_REAL_PROPERTY_CITY2.Value = u["REAL_PROPERTY_CITY2"].ToString();
            //edtAPP_REAL_PROPERTY_STATE2.SelectedValue = u["REAL_PROPERTY_STATE2"].ToString();
            //edtAPP_REAL_PROPERTY_ZIP2.Value = u["REAL_PROPERTY_ZIP2"].ToString();
            //edtAPP_REAL_STATUS2.SelectedValue = u["REAL_STATUS2"].ToString();
            //edtAPP_REAL_PROPERTY_TYPE2.SelectedValue = u["REAL_TYPE2"].ToString();
            //edtAPP_REAL_MARKET_VALUE2.Value = u["REAL_MARKET_VALUE2"].ToString();
            //edtAPP_REAL_MORTGAGE2.Value = u["REAL_MORTGAGE2"].ToString();
            //edtAPP_REAL_MORT_PAY2.Value = u["REAL_MORT_PAY2"].ToString();
            //edtAPP_REAL_MONTH_PAY2.Value = u["REAL_MONTH_PAY2"].ToString();
            //edtAPP_REAL_ADDRESS3.Value = u["REAL_ADDRESS3"].ToString();
            //edtAPP_REAL_PROPERTY_CITY3.Value = u["REAL_PROPERTY_CITY3"].ToString();
            //edtAPP_REAL_PROPERTY_STATE3.SelectedValue = u["REAL_PROPERTY_STATE3"].ToString();
            //edtAPP_REAL_PROPERTY_ZIP3.Value = u["REAL_PROPERTY_ZIP3"].ToString();
            //edtAPP_REAL_STATUS3.SelectedValue = u["REAL_STATUS3"].ToString();
            //edtAPP_REAL_PROPERTY_TYPE3.SelectedValue = u["REAL_TYPE3"].ToString();
            //edtAPP_REAL_MARKET_VALUE3.Value = u["REAL_MARKET_VALUE3"].ToString();
            //edtAPP_REAL_MORTGAGE3.Value = u["REAL_MORTGAGE3"].ToString();
            //edtAPP_REAL_MORT_PAY3.Value = u["REAL_MORT_PAY3"].ToString();
            //edtAPP_REAL_MONTH_PAY3.Value = u["REAL_MONTH_PAY3"].ToString();
            //#endregion

            //#region Step 10
            ////Borrower
            //if (bool.TryParse(u["DeclarationA"].ToString(), out b))
            //{
            //    this.edtAPP_Q_A.SelectedValue = u["DeclarationA"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationB"].ToString(), out b))
            //{
            //    this.edtAPP_Q_B.SelectedValue = u["DeclarationB"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationC"].ToString(), out b))
            //{
            //    this.edtAPP_Q_C.SelectedValue = u["DeclarationC"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationD"].ToString(), out b))
            //{
            //    this.edtAPP_Q_D.SelectedValue = u["DeclarationD"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationE"].ToString(), out b))
            //{
            //    this.edtAPP_Q_E.SelectedValue = u["DeclarationE"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationF"].ToString(), out b))
            //{
            //    this.edtAPP_Q_F.SelectedValue = u["DeclarationF"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationG"].ToString(), out b))
            //{
            //    this.edtAPP_Q_G.SelectedValue = u["DeclarationG"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationH"].ToString(), out b))
            //{
            //    this.edtAPP_Q_H.SelectedValue = u["DeclarationH"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationI"].ToString(), out b))
            //{
            //    this.edtAPP_Q_I.SelectedValue = u["DeclarationI"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationJ"].ToString(), out b))
            //{
            //    this.edtAPP_Q_J.SelectedValue = u["DeclarationJ"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationK"].ToString(), out b))
            //{
            //    this.edtAPP_Q_K.SelectedValue = u["DeclarationK"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationL"].ToString(), out b))
            //{
            //    this.edtAPP_Q_L.SelectedValue = u["DeclarationL"].ToString();
            //}
            //if (bool.TryParse(u["DeclarationM"].ToString(), out b))
            //{
            //    this.edtAPP_Q_M.SelectedValue = u["DeclarationM"].ToString();
            //}
            //this.edtAPP_Q_1.SelectedValue = u["Declaration1"].ToString();
            //this.edtAPP_Q_2.SelectedValue = u["Declaration2"].ToString();

            ////CO-Borrower
            //if (bool.TryParse(u["CoDeclarationA"].ToString(), out b))
            //{
            //    this.edtCoDeclarationA.SelectedValue = u["CoDeclarationA"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationB"].ToString(), out b))
            //{
            //    this.edtCoDeclarationB.SelectedValue = u["CoDeclarationB"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationC"].ToString(), out b))
            //{
            //    this.edtCoDeclarationC.SelectedValue = u["CoDeclarationC"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationD"].ToString(), out b))
            //{
            //    this.edtCoDeclarationD.SelectedValue = u["CoDeclarationD"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationE"].ToString(), out b))
            //{
            //    this.edtCoDeclarationE.SelectedValue = u["CoDeclarationE"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationF"].ToString(), out b))
            //{
            //    this.edtCoDeclarationF.SelectedValue = u["CoDeclarationF"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationG"].ToString(), out b))
            //{
            //    this.edtCoDeclarationG.SelectedValue = u["CoDeclarationG"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationH"].ToString(), out b))
            //{
            //    this.edtCoDeclarationH.SelectedValue = u["CoDeclarationH"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationI"].ToString(), out b))
            //{
            //    this.edtCoDeclarationI.SelectedValue = u["CoDeclarationI"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationJ"].ToString(), out b))
            //{
            //    this.edtCoDeclarationJ.SelectedValue = u["CoDeclarationJ"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationK"].ToString(), out b))
            //{
            //    this.edtCoDeclarationK.SelectedValue = u["CoDeclarationK"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationL"].ToString(), out b))
            //{
            //    this.edtCoDeclarationL.SelectedValue = u["CoDeclarationL"].ToString();
            //}
            //if (bool.TryParse(u["CoDeclarationM"].ToString(), out b))
            //{
            //    this.edtCoDeclarationM.SelectedValue = u["CoDeclarationM"].ToString();
            //}
            //this.edtCoDeclaration1.SelectedValue = u["CoDeclaration1"].ToString();
            //this.edtCoDeclaration2.SelectedValue = u["CoDeclaration2"].ToString();
            //#endregion

            //#region Step 11
            ////Borrower
            //this.edtEthnicity.SelectedValue = u["Ethnicity"].ToString();
            //this.edtRace.SelectedValue = u["Race"].ToString();
            //this.edtSex.SelectedValue = u["Sex"].ToString();

            ////CO-Borrower
            //this.edtCoEthnicity.SelectedValue = u["CoEthnicity"].ToString();
            //this.edtCoRace.SelectedValue = u["CoRace"].ToString();
            //this.edtCoSex.SelectedValue = u["CoSex"].ToString();

            ////authorization
            //if (chkCreditCheckAuthorization != null && bool.TryParse(u["CreditCheckAuthorization"].ToString(), out b))
            //{
            //    chkCreditCheckAuthorization.Checked = b;
            //}
            //#endregion

            return true;
        }

        //Function To Save All Values
        protected bool SaveData(Guid userid)
        {
            //#region Basic Variables
            //string err;
            //bool isDupLeadSource;
            //string Consultant_ID = "";
            //Guid c_id = Guid.Empty;
            //Autoresponders.BorrowerCo dataForm = new Autoresponders.BorrowerCo();
            //#endregion

            //#region Initialize User Class
            //AdminClients.BusinessLogicLayer.User u;
            //if (userid.Equals(Guid.Empty))
            //{
            //    u = new AdminClients.BusinessLogicLayer.User();
            //}
            //else
            //{
            //    u = new AdminClients.DataAccess.SQLDataAccess().GetUser(userid);
            //}
            //#endregion

            ////Delete values with $ and ,
            //deleteExtraValues();

            //#region Set Consultant ID
            //Consultant_ID = getConsultant_ID();
            //if (Consultant_ID.Length > 0)
            //{
            //    c_id = new Guid(Consultant_ID);
            //}
            //#endregion

            //#region Step 1
            //u.SITE_ID = new Guid(MasterGlobal.SiteID());
            //u.LeadSource = LeadSource.Website_FullApp;
            //u.txtEmail = txtEmail.Value;
            //u.txtPwd = txtPwd.Value;
            //u["AGENT_ID"] = c_id;
            //#endregion

            //#region Step 2
            //u["Loan Type"] = this.edtAPP_LOAN_PURPOSE.SelectedValue;
            //u["LoanPurposeOther"] = this.LoanPurposeOther.Value.Trim();
            //u["LoanPurposeRefiType"] = this.edtAPP_REF_LOAN_PURPOSE.SelectedValue;
            //SalesPriceOrHomevalue2DB(u);
            //string s = this.edtLOAN_AMOUNT.Value.Trim();
            //decimal amt;
            //if (decimal.TryParse(s, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowThousands, null, out amt))
            //{
            //    u.LoanAmount = amt;
            //}
            //u["SalesDetailsFoundAHome"] = this.edtPROPERTY_NOT_FOUND.Checked ? "not found" : "";
            //u.txtPropertyAdress = this.edtAPP_PROPERTY_ADDRESS.Value.Trim();
            //u.txtCity = this.edtAPP_PROPERTY_CURR_CITY.Value.Trim();
            //u.txtState = this.edtAPP_PROPERTY_STATE.SelectedValue;
            //u.intZip = this.edtAPP_PROPERTY_ZIP.Value.Trim();
            //u["Property County"] = this.edtAPP_PROPERTY_COUNTY.Value.Trim();
            //u["Num Of Units"] = this.edtAPP_UNITS_NUM.Value.Trim();
            //u["pWillBeIsPrimary"] = this.propertyWillBeIsPrimary.Checked;
            //u["pWillBeIsSecondary"] = this.propertyWillBeIsSecondary.Checked;
            //u["pWillBeIsInvestment"] = this.propertyWillBeIsInvestment.Checked;
            //u.LoanType = this.DropDownLoanType.SelectedValue;
            //u["LoanTypeOther"] = this.LoanTypeOther.Value.Trim();
            //#endregion

            //#region Step 3
            ////Borrower
            //u.txtFirstName = this.edtAPP_PB_FIRST_NAME.Value.Trim();
            //u.txtLastName = this.edtAPP_PB_LAST_NAME.Value.Trim();
            //u.txtMiddleName = this.edtAPP_PB_MIDDLE_INITIALS.Value.Trim();
            ////u["SocNum"] = this.edtAPP_PB_SOC_NO.Value.Trim();
            //u["MartialStatus"] = this.edtAPP_PB_MARITAL.SelectedValue;
            //u["DOB"] = this.edtAPP_PB_DOB.Value.Trim();
            //u["AssetYearsOfSchool"] = this.edtAPP_PB_YEARS_IN_SCOOL.Value.Trim();
            //u["DependentsNum"] = this.edtAPP_PB_DEPENDANTS_NO.Value.Trim();
            //u["DependentsAges"] = this.edtAPP_PB_DEPENDANTS_AGE.Value.Trim();
            //u.WorkPhone = this.WorkPhone.Value.Trim();
            //u.txtPhone = this.txtPhone.Value.Trim();
            //u.txtCellPhone = this.txtCellPhone.Value.Trim();
            //u.BorrowerAddress = this.edtAPP_PB_CURR_ADDRESS.Value.Trim();
            //u.BorrowerCity = this.edtAPP_PB_CURR_CITY.Value.Trim();
            //u.BorrowerState = this.edtAPP_PB_CURR_STATE.SelectedValue;
            //u.BorrowerZip = this.edtAPP_PB_CURR_ZIP.Value.Trim();
            //u["TimeAtResidenceYears"] = this.edtAPP_PB_HOW_LONG_YEARS.Value.Trim();
            //u["TimeAtResidenceMonths"] = this.edtAPP_PB_HOW_LONG_MONTHS.Value.Trim();
            //u["Ownership"] = this.edtAPP_PB_OWNERSHIP.SelectedValue;

            ////CO-Borrower
            //u.CoFirstName = this.edtAPP_CB_FIRST_NAME.Value.Trim();
            //u.CoLastName = this.edtAPP_CB_LAST_NAME.Value.Trim();
            //u["CoBorrowerMiddleName"] = this.edtAPP_CB_MIDDLE_INITIALS.Value.Trim();
            ////u["CoSocNum"] = this.edtAPP_CB_SOC_NO.Value.Trim();
            //u["CoMartialStatus"] = this.edtAPP_CB_MARITAL.SelectedValue;
            //u["CoDOB"] = this.edtAPP_CB_DOB.Value.Trim();
            //u["CoBoAssetYearsOfSchool"] = this.edtAPP_CB_YEARS_IN_SCOOL.Value.Trim();
            //u["CoBoDependentsNum"] = this.edtAPP_CB_DEPENDANTS_NO.Value.Trim();
            //u["CoBoDependentsAges"] = this.edtAPP_CB_DEPENDANTS_AGE.Value.Trim();
            //u.CoWorkPhone = this.CoWorkPhone.Value.Trim();
            //u.CoPhone = this.CoPhone.Value.Trim();
            //u.CoCellPhone = this.CoCellPhone.Value.Trim();
            //u.CoAddress = this.edtAPP_CB_CURR_ADDRESS.Value.Trim();
            //u.CoCity = this.edtAPP_CB_CURR_CITY.Value.Trim();
            //u.CoState = this.edtAPP_CB_CURR_STATE.SelectedValue;
            //u["CoEmail"] = edtCoBorrowerEmail.Value;
            //u.CoZip = this.edtAPP_CB_CURR_ZIP.Value.Trim();
            //u["CoTimeAtResidenceYears"] = this.edtAPP_CB_HOW_LONG_YEARS.Value.Trim();
            //u["CoTimeAtResidenceMonths"] = this.edtAPP_CB_HOW_LONG_MONTHS.Value.Trim();
            //u["CoOwnership"] = this.edtAPP_CB_OWNERSHIP.SelectedValue;

            ////Hidden
            //u["Preferred Language"] = this.DropDownPreferredLanguage.SelectedValue;
            //#endregion

            //#region Step 4
            ////Borrower
            //u.txtCompany = this.edtAPP_PB_EMP_NAME.Value.Trim();
            //u["EmployerAddress"] = this.edtAPP_PB_EMP_ADDRESS.Value.Trim();
            //u["EmployerCity"] = this.edtAPP_PB_EMP_CITY.Value.Trim();
            //u["EmployerState"] = this.edtAPP_PB_EMP_STATE.SelectedValue;
            //u["EmployerZip"] = this.edtAPP_PB_EMP_ZIP.Value.Trim();
            //u["EmployerPhone"] = this.edtAPP_PB_EMP_PHONE.Value.Trim();
            //u["EmployerTitle"] = this.edtAPP_PB_EMP_TITLE.Value.Trim();
            //u["SelfEmployed"] = this.edtAPP_PB_EMP_SELF.Checked;
            //u["YearsWithEmloyer"] = this.edtAPP_PB_EMP_YEARS.Value.Trim();
            //u["MonthsWithEmloyer"] = this.edtAPP_PB_EMP_MONTHS.Value.Trim();

            ////CO-Borrower
            //u["CoEmployerName"] = this.edtAPP_CB_EMP_NAME.Value.Trim();
            //u["CoEmployerAddress"] = this.edtAPP_CB_EMP_ADDRESS.Value.Trim();
            //u["CoEmployerCity"] = this.edtAPP_CB_EMP_CITY.Value.Trim();
            //u["CoEmployerState"] = this.edtAPP_CB_EMP_STATE.SelectedValue;
            //u["CoEmployerZip"] = this.edtAPP_CB_EMP_ZIP.Value.Trim();
            //u["CoEmployerPhone"] = this.edtAPP_CB_EMP_PHONE.Value.Trim();
            //u["CoEmployerTitle"] = this.edtAPP_CB_EMP_TITLE.Value.Trim();
            //u["CoSelfEmployed"] = this.edtAPP_CB_EMP_SELF.Checked;
            //u["CoYearsWithEmloyer"] = this.edtAPP_CB_EMP_YEARS.Value.Trim();
            //u["CoMonthsWithEmloyer"] = this.edtAPP_CB_EMP_MONTHS.Value.Trim();
            //#endregion

            //#region Step 5
            ////Borrower
            //u["BaseEmploymentIncome"] = this.edtAPP_INC_BASE.Value.Trim();
            //u["Overtime"] = this.edtAPP_INC_OVERTIME.Value.Trim();
            //u["Bonuses"] = this.edtAPP_INC_BONUSES.Value.Trim();
            //u["Commissions"] = this.edtAPP_INC_COMMISSIONS.Value.Trim();
            //u["Dividends"] = this.edtAPP_INC_DIVIDENTS.Value.Trim();
            //u["NetRentalIncome"] = this.edtAPP_INC_RENTAL_INC.Value.Trim();

            ////CO-Borrower
            //u["CoBaseEmploymentIncome"] = this.edtAPP_INC_BASE_CB.Value.Trim();
            //u["CoOvertime"] = this.edtAPP_INC_OVERTIME_CB.Value.Trim();
            //u["CoBonuses"] = this.edtAPP_INC_BONUSES_CB.Value.Trim();
            //u["CoCommissions"] = this.edtAPP_INC_COMMISSIONS_CB.Value.Trim();
            //u["CoDividends"] = this.edtAPP_INC_DIVIDENTS_CB.Value.Trim();
            //u["CoNetRentalIncome"] = this.edtAPP_INC_RENTAL_INC_CB.Value.Trim();
            //#endregion

            //#region Step 6
            //u["CashDepositDescr1"] = CashDepositDescr1.Value.Trim();
            //u["CashDepositVal1"] = CashDepositVal1.Value.Trim();
            //u["CashDepositDescr2"] = CashDepositDescr2.Value.Trim();
            //u["CashDepositVal2"] = CashDepositVal2.Value.Trim();
            //u["StocksBondsCompNameAccount1"] = StocksBondsCompNameAccount1.Value.Trim();
            //u["StocksBondsVal1"] = StocksBondsVal1.Value.Trim();
            //u["StocksBondsCompNameAccount2"] = StocksBondsCompNameAccount2.Value.Trim();
            //u["StocksBondsVal2"] = StocksBondsVal2.Value.Trim();
            //u["StocksBondsCompNameAccount3"] = StocksBondsCompNameAccount3.Value.Trim();
            //u["StocksBondsVal3"] = StocksBondsVal3.Value.Trim();
            //u["LInsuranceFaceAmount"] = LInsuranceFaceAmount.Value.Trim();
            //u["LInsuranceMarketValue"] = LInsuranceMarketValue.Value.Trim();
            //u["VestedInterestInRF"] = VestedInterestInRF.Value.Trim();
            //u["NetWorthOfBusinessOwned"] = NetWorthOfBusinessOwned.Value.Trim();
            //u["AutoMakeAndYear1"] = AutoMakeAndYear1.Value.Trim();
            //u["AutoMakeAndYear2"] = AutoMakeAndYear2.Value.Trim();
            //u["AutoMakeAndYear3"] = AutoMakeAndYear3.Value.Trim();
            //u["AutoVal1"] = AutoVal1.Value.Trim();
            //u["AutoVal2"] = AutoVal2.Value.Trim();
            //u["AutoVal3"] = AutoVal3.Value.Trim();
            //u["AssetsOtherDescr1"] = AssetsOtherDescr1.Value.Trim();
            //u["AssetsOtherVal1"] = AssetsOtherVal1.Value.Trim();
            //u["AssetsOtherDescr2"] = AssetsOtherDescr2.Value.Trim();
            //u["AssetsOtherVal2"] = AssetsOtherVal2.Value.Trim();
            //u["AssetsOtherDescr3"] = AssetsOtherDescr3.Value.Trim();
            //u["AssetsOtherVal3"] = AssetsOtherVal3.Value.Trim();
            //u["AssetsOtherDescr4"] = AssetsOtherDescr4.Value.Trim();
            //u["AssetsOtherVal4"] = AssetsOtherVal4.Value.Trim();
            //u["AssetType1"] = AssetType1.Value.Trim();
            //u["AssetInstitution1"] = AssetInstitution1.Value.Trim();
            //u["AssetAccount1"] = AssetAccount1.Value.Trim();
            //u["AssetBalance1"] = AssetBalance1.Value.Trim();
            //u["AssetType2"] = AssetType2.Value.Trim();
            //u["AssetInstitution2"] = AssetInstitution2.Value.Trim();
            //u["AssetAccount2"] = AssetAccount2.Value.Trim();
            //u["AssetBalance2"] = AssetBalance2.Value.Trim();
            //u["AssetType3"] = AssetType3.Value.Trim();
            //u["AssetInstitution3"] = AssetInstitution3.Value.Trim();
            //u["AssetAccount3"] = AssetAccount3.Value.Trim();
            //u["AssetBalance3"] = AssetBalance3.Value.Trim();
            //u["AssetType4"] = AssetType4.Value.Trim();
            //u["AssetInstitution4"] = AssetInstitution4.Value.Trim();
            //u["AssetAccount4"] = AssetAccount4.Value.Trim();
            //u["AssetBalance4"] = AssetBalance4.Value.Trim();
            //#endregion

            //#region Step 7
            //u["MHERent"] = MHERent.Value.Trim();
            //u["MHE1stMrtgP"] = MHE1stMrtgP.Value.Trim();
            //u["MHEOthrMrtgP"] = MHEOthrMrtgP.Value.Trim();
            //u["MHEHazIns"] = MHEHazIns.Value.Trim();
            //u["MHERETaxes"] = MHERETaxes.Value.Trim();
            //u["MHEMtgIns"] = MHEMtgIns.Value.Trim();
            //u["MHEHOADues"] = MHEHOADues.Value.Trim();
            //u["MHEOther"] = MHEOther.Value.Trim();
            //#endregion

            //#region Step 8
            //u["LiaCompanyName1"] = LiaCompanyName1.Value.Trim();
            //u["LiaType1"] = LiaType1.SelectedValue;
            //u["LiaBalance1"] = LiaBalance1.Value.Trim();
            //u["LiaPayment1"] = LiaPayment1.Value.Trim();
            //u["LiaMosLeft1"] = LiaMosLeft1.Value.Trim();
            //u["LiaPaidOff1"] = LiaPaidOff1.Checked;
            //u["LiaCompanyName2"] = LiaCompanyName2.Value.Trim();
            //u["LiaType2"] = LiaType2.SelectedValue;
            //u["LiaBalance2"] = LiaBalance2.Value.Trim();
            //u["LiaPayment2"] = LiaPayment2.Value.Trim();
            //u["LiaMosLeft2"] = LiaMosLeft2.Value.Trim();
            //u["LiaPaidOff2"] = LiaPaidOff2.Checked;
            //u["LiaCompanyName3"] = LiaCompanyName3.Value.Trim();
            //u["LiaType3"] = LiaType3.SelectedValue;
            //u["LiaBalance3"] = LiaBalance3.Value.Trim();
            //u["LiaPayment3"] = LiaPayment3.Value.Trim();
            //u["LiaMosLeft3"] = LiaMosLeft3.Value.Trim();
            //u["LiaPaidOff3"] = LiaPaidOff3.Checked;
            //u["LiaCompanyName4"] = LiaCompanyName4.Value.Trim();
            //u["LiaType4"] = LiaType4.SelectedValue;
            //u["LiaBalance4"] = LiaBalance4.Value.Trim();
            //u["LiaPayment4"] = LiaPayment4.Value.Trim();
            //u["LiaMosLeft4"] = LiaMosLeft4.Value.Trim();
            //u["LiaPaidOff4"] = LiaPaidOff4.Checked;
            //u["LiaCompanyName5"] = LiaCompanyName5.Value.Trim();
            //u["LiaType5"] = LiaType5.SelectedValue;
            //u["LiaBalance5"] = LiaBalance5.Value.Trim();
            //u["LiaPayment5"] = LiaPayment5.Value.Trim();
            //u["LiaMosLeft5"] = LiaMosLeft5.Value.Trim();
            //u["LiaPaidOff5"] = LiaPaidOff5.Checked;
            //u["LiaCompanyName6"] = LiaCompanyName6.Value.Trim();
            //u["LiaType6"] = LiaType6.SelectedValue;
            //u["LiaBalance6"] = LiaBalance6.Value.Trim();
            //u["LiaPayment6"] = LiaPayment6.Value.Trim();
            //u["LiaMosLeft6"] = LiaMosLeft6.Value.Trim();
            //u["LiaPaidOff6"] = LiaPaidOff6.Checked;
            //u["LiaCompanyName7"] = LiaCompanyName7.Value.Trim();
            //u["LiaType7"] = LiaType7.SelectedValue;
            //u["LiaBalance7"] = LiaBalance7.Value.Trim();
            //u["LiaPayment7"] = LiaPayment7.Value.Trim();
            //u["LiaMosLeft7"] = LiaMosLeft7.Value.Trim();
            //u["LiaPaidOff7"] = LiaPaidOff7.Checked;
            //#endregion

            //#region Step 9
            ////Verify if any all the values from property 1 in Step 9 (Real Estate) is not empty
            //if (!String.IsNullOrEmpty(edtAPP_REAL_ADDRESS.Value) || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_CITY.Value) || edtAPP_REAL_PROPERTY_STATE.SelectedIndex > 0 || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_ZIP.Value) || edtAPP_REAL_STATUS.SelectedIndex > 0 || edtAPP_REAL_PROPERTY_TYPE.SelectedIndex > 0 || !String.IsNullOrEmpty(edtAPP_REAL_MORTGAGE.Value) || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_CITY.Value) || !String.IsNullOrEmpty(edtAPP_REAL_MORT_PAY.Value) || !String.IsNullOrEmpty(edtAPP_REAL_MONTH_PAY.Value))
            //{
            //    u["REAL_ADDRESS1"] = edtAPP_REAL_ADDRESS.Value.Trim();
            //    u["REAL_PROPERTY_CITY1"] = edtAPP_REAL_PROPERTY_CITY.Value.Trim();
            //    u["REAL_PROPERTY_STATE1"] = edtAPP_REAL_PROPERTY_STATE.SelectedValue;
            //    u["REAL_PROPERTY_ZIP1"] = edtAPP_REAL_PROPERTY_ZIP.Value.Trim();
            //    u["REAL_STATUS1"] = edtAPP_REAL_STATUS.SelectedValue;
            //    u["REAL_TYPE1"] = edtAPP_REAL_PROPERTY_TYPE.SelectedValue;
            //    u["REAL_MARKET_VALUE1"] = edtAPP_REAL_MARKET_VALUE.Value.Trim();
            //    u["REAL_MORTGAGE1"] = edtAPP_REAL_MORTGAGE.Value.Trim();
            //    u["REAL_MORT_PAY1"] = edtAPP_REAL_MORT_PAY.Value.Trim();
            //    u["REAL_MONTH_PAY1"] = edtAPP_REAL_MONTH_PAY.Value.Trim();
            //}

            ////Verify if any all the values from property 2 in Step 9 (Real Estate) is not empty
            //if (!String.IsNullOrEmpty(edtAPP_REAL_ADDRESS2.Value) || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_CITY2.Value) || edtAPP_REAL_PROPERTY_STATE2.SelectedIndex > 0 || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_ZIP2.Value) || edtAPP_REAL_STATUS2.SelectedIndex > 0 || edtAPP_REAL_PROPERTY_TYPE2.SelectedIndex > 0 || !String.IsNullOrEmpty(edtAPP_REAL_MORTGAGE2.Value) || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_CITY2.Value) || !String.IsNullOrEmpty(edtAPP_REAL_MORT_PAY2.Value) || !String.IsNullOrEmpty(edtAPP_REAL_MONTH_PAY2.Value))
            //{
            //    u["REAL_ADDRESS2"] = edtAPP_REAL_ADDRESS2.Value.Trim();
            //    u["REAL_PROPERTY_CITY2"] = edtAPP_REAL_PROPERTY_CITY2.Value.Trim();
            //    u["REAL_PROPERTY_STATE2"] = edtAPP_REAL_PROPERTY_STATE2.SelectedValue;
            //    u["REAL_PROPERTY_ZIP2"] = edtAPP_REAL_PROPERTY_ZIP2.Value.Trim();
            //    u["REAL_STATUS2"] = edtAPP_REAL_STATUS2.SelectedValue;
            //    u["REAL_TYPE2"] = edtAPP_REAL_PROPERTY_TYPE2.SelectedValue;
            //    u["REAL_MARKET_VALUE2"] = edtAPP_REAL_MARKET_VALUE2.Value.Trim();
            //    u["REAL_MORTGAGE2"] = edtAPP_REAL_MORTGAGE2.Value.Trim();
            //    u["REAL_MORT_PAY2"] = edtAPP_REAL_MORT_PAY2.Value.Trim();
            //    u["REAL_MONTH_PAY2"] = edtAPP_REAL_MONTH_PAY2.Value.Trim();
            //}

            ////Verify if any all the values from property 3 in Step 9 (Real Estate) is not empty
            //if (!String.IsNullOrEmpty(edtAPP_REAL_ADDRESS3.Value) || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_CITY3.Value) || edtAPP_REAL_PROPERTY_STATE.SelectedIndex > 0 || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_ZIP3.Value) || edtAPP_REAL_STATUS3.SelectedIndex > 0 || edtAPP_REAL_PROPERTY_TYPE3.SelectedIndex > 0 || !String.IsNullOrEmpty(edtAPP_REAL_MORTGAGE3.Value) || !String.IsNullOrEmpty(edtAPP_REAL_PROPERTY_CITY3.Value) || !String.IsNullOrEmpty(edtAPP_REAL_MORT_PAY3.Value) || !String.IsNullOrEmpty(edtAPP_REAL_MONTH_PAY3.Value))
            //{
            //    u["REAL_ADDRESS3"] = edtAPP_REAL_ADDRESS3.Value.Trim();
            //    u["REAL_PROPERTY_CITY3"] = edtAPP_REAL_PROPERTY_CITY3.Value.Trim();
            //    u["REAL_PROPERTY_STATE3"] = edtAPP_REAL_PROPERTY_STATE3.SelectedValue;
            //    u["REAL_PROPERTY_ZIP3"] = edtAPP_REAL_PROPERTY_ZIP3.Value.Trim();
            //    u["REAL_STATUS3"] = edtAPP_REAL_STATUS3.SelectedValue;
            //    u["REAL_TYPE3"] = edtAPP_REAL_PROPERTY_TYPE3.SelectedValue;
            //    u["REAL_MARKET_VALUE3"] = edtAPP_REAL_MARKET_VALUE3.Value.Trim();
            //    u["REAL_MORTGAGE3"] = edtAPP_REAL_MORTGAGE3.Value.Trim();
            //    u["REAL_MORT_PAY3"] = edtAPP_REAL_MORT_PAY3.Value.Trim();
            //    u["REAL_MONTH_PAY3"] = edtAPP_REAL_MONTH_PAY3.Value.Trim();
            //}
            //#endregion

            //#region step 10
            ////Borrower
            //u["DeclarationA"] = this.edtAPP_Q_A.Value.Trim();
            //u["DeclarationB"] = this.edtAPP_Q_B.Value.Trim();
            //u["DeclarationC"] = this.edtAPP_Q_C.Value.Trim();
            //u["DeclarationD"] = this.edtAPP_Q_D.Value.Trim();
            //u["DeclarationE"] = this.edtAPP_Q_E.Value.Trim();
            //u["DeclarationF"] = this.edtAPP_Q_F.Value.Trim();
            //u["DeclarationG"] = this.edtAPP_Q_G.Value.Trim();
            //u["DeclarationH"] = this.edtAPP_Q_H.Value.Trim();
            //u["DeclarationI"] = this.edtAPP_Q_I.Value.Trim();
            //u["DeclarationJ"] = this.edtAPP_Q_J.Value.Trim();
            //u["DeclarationK"] = this.edtAPP_Q_K.Value.Trim();
            //u["DeclarationL"] = this.edtAPP_Q_L.Value.Trim();
            //u["DeclarationM"] = this.edtAPP_Q_M.Value.Trim();
            //u["Declaration1"] = this.edtAPP_Q_1.SelectedValue;
            //u["Declaration2"] = this.edtAPP_Q_2.SelectedValue;

            ////Co-Borrewer
            //u["CoDeclarationA"] = this.edtCoDeclarationA.Value.Trim();
            //u["CoDeclarationB"] = this.edtCoDeclarationB.Value.Trim();
            //u["CoDeclarationC"] = this.edtCoDeclarationC.Value.Trim();
            //u["CoDeclarationD"] = this.edtCoDeclarationD.Value.Trim();
            //u["CoDeclarationE"] = this.edtCoDeclarationE.Value.Trim();
            //u["CoDeclarationF"] = this.edtCoDeclarationF.Value.Trim();
            //u["CoDeclarationG"] = this.edtCoDeclarationG.Value.Trim();
            //u["CoDeclarationH"] = this.edtCoDeclarationH.Value.Trim();
            //u["CoDeclarationI"] = this.edtCoDeclarationI.Value.Trim();
            //u["CoDeclarationJ"] = this.edtCoDeclarationJ.Value.Trim();
            //u["CoDeclarationK"] = this.edtCoDeclarationK.Value.Trim();
            //u["CoDeclarationL"] = this.edtCoDeclarationL.Value.Trim();
            //u["CoDeclarationM"] = this.edtCoDeclarationM.Value.Trim();
            //u["CoDeclaration1"] = this.edtCoDeclaration1.SelectedValue;
            //u["CoDeclaration2"] = this.edtCoDeclaration2.SelectedValue;
            //#endregion

            //#region step 11
            ////Borrower
            //u["Ethnicity"] = this.edtEthnicity.SelectedValue;
            //u["Race"] = this.edtRace.SelectedValue;
            //u["Sex"] = this.edtSex.SelectedValue;

            ////CO-Borrower
            //u["CoEthnicity"] = this.edtCoEthnicity.SelectedValue;
            //u["CoRace"] = this.edtCoRace.SelectedValue;
            //u["CoSex"] = this.edtCoSex.SelectedValue;

            ////Authorization
            //if (chkCreditCheckAuthorization != null)
            //{
            //    u["CreditCheckAuthorization"] = chkCreditCheckAuthorization.Checked;
            //}
            //#endregion

            //#region Current Step
            //int activeIndex;
            //activeIndex = MultiView1.ActiveViewIndex;
            //u["Notes"] = activeIndex;
            //#endregion

            //#region Set Values Autoresponder
            ////Borrower
            //dataForm.BorrowerFirstName = edtAPP_PB_FIRST_NAME.Value;
            //dataForm.BorrowerLastName = edtAPP_PB_LAST_NAME.Value;
            //dataForm.BorrowerAddress = edtAPP_PB_CURR_ADDRESS.Value;
            //dataForm.BorrowerCellPhone = txtCellPhone.Value;
            //dataForm.BorrowerCity = edtAPP_PB_CURR_CITY.Value;
            //dataForm.BorrowerDOB = edtAPP_PB_DOB.Value;
            //dataForm.BorrowerEmail = txtEmail.Value;
            //dataForm.BorrowerHomePhone = txtPhone.Value;
            //dataForm.BorrowerLoanAmount = edtLOAN_AMOUNT.Value;
            //dataForm.BorrowerState = edtAPP_PB_CURR_STATE.SelectedValue;
            //dataForm.BorrowerZip = edtAPP_PB_CURR_ZIP.Value;

            ////CO-Borrower
            //dataForm.CoBorrowerFirstName = edtAPP_CB_FIRST_NAME.Value;
            //dataForm.CoBorrowerLastName = edtAPP_CB_LAST_NAME.Value;
            //dataForm.CoBorrowerAddress = edtAPP_CB_CURR_ADDRESS.Value;
            //dataForm.CoBorrowerCellPhone = CoCellPhone.Value;
            //dataForm.CoBorrowerCity = edtAPP_CB_CURR_CITY.Value;
            //dataForm.CoBorrowerDOB = edtAPP_CB_DOB.Value;
            //dataForm.CoBorrowerEmail = edtCoBorrowerEmail.Value;
            //dataForm.CoBorrowerHomePhone = CoPhone.Value;
            //dataForm.CoBorrowerState = edtAPP_CB_CURR_STATE.SelectedValue;
            //dataForm.CoBorrowerZip = edtAPP_CB_CURR_ZIP.Value;

            //#endregion

            ////Send notification to LO
            ////NewClientSites.MasterGlobal.send_AutoResponderLO(dataForm, activeIndex);
            //Autoresponders.FullAplication(dataForm, activeIndex);

            ////Send notification to Lead
            ////NewClientSites.MasterGlobal.send_AutoResponderLead(dataForm, activeIndex, "full");

            ////Move to last step due to is a save and continue
            //MultiView1.ActiveViewIndex = 11;

            ////Thanks message
            //this.Message.Value = edtAPP_PB_FIRST_NAME.Value;
            ////this.ConsultantName.Value = this.drpMortgageSpecialist1.SelectedItem.Value.ToString();

            ////Save changes into the datase
            //return u.ApplyChanges(true, out err, out isDupLeadSource);
            return true;

        }

        //Function To delete Values Like $ or ,
        protected void deleteExtraValues()
        {
            //Create a list for all textbox
            List<TextBox> values = new List<TextBox>();

            //Add each textbox that could have a number
            //values.Add(AssetBalance1);
            //values.Add(AssetBalance2);
            //values.Add(AssetBalance3);
            //values.Add(AssetBalance4);
            //values.Add(edtHOME_VALUE);
            //values.Add(edtLOAN_AMOUNT);
            //values.Add(edtAPP_INC_BASE);
            //values.Add(edtAPP_INC_BASE_CB);
            //values.Add(edtAPP_INC_OVERTIME);
            //values.Add(edtAPP_INC_OVERTIME_CB);
            //values.Add(edtAPP_INC_BONUSES);
            //values.Add(edtAPP_INC_BONUSES_CB);
            //values.Add(edtAPP_INC_COMMISSIONS);
            //values.Add(edtAPP_INC_COMMISSIONS_CB);
            //values.Add(edtAPP_INC_DIVIDENTS);
            //values.Add(edtAPP_INC_DIVIDENTS_CB);
            //values.Add(edtAPP_INC_RENTAL_INC);
            //values.Add(edtAPP_INC_RENTAL_INC_CB);
            //values.Add(edtAPP_INC_OTHER_INC);
            //values.Add(edtAPP_INC_OTHER_INC_CB);
            //values.Add(CashDepositDescr1);
            //values.Add(CashDepositDescr2);
            //values.Add(CashDepositVal1);
            //values.Add(CashDepositVal2);
            //values.Add(VestedInterestInRF);
            //values.Add(NetWorthOfBusinessOwned);
            //values.Add(AutoVal1);
            //values.Add(AutoVal2);
            //values.Add(AutoVal3);
            //values.Add(StocksBondsVal1);
            //values.Add(StocksBondsVal2);
            //values.Add(StocksBondsVal3);
            //values.Add(AssetsOtherVal1);
            //values.Add(AssetsOtherVal2);
            //values.Add(AssetsOtherVal3);
            //values.Add(AssetsOtherVal4);
            //values.Add(LInsuranceFaceAmount);
            //values.Add(LInsuranceMarketValue);
            //values.Add(LiaBalance1);
            //values.Add(LiaBalance2);
            //values.Add(LiaBalance3);
            //values.Add(LiaBalance4);
            //values.Add(LiaBalance5);
            //values.Add(LiaBalance6);
            //values.Add(LiaBalance7);
            //values.Add(LiaPayment1);
            //values.Add(LiaPayment2);
            //values.Add(LiaPayment3);
            //values.Add(LiaPayment4);
            //values.Add(LiaPayment5);
            //values.Add(LiaPayment6);
            //values.Add(LiaPayment7);
            //values.Add(MHERent);
            //values.Add(MHE1stMrtgP);
            //values.Add(MHEOthrMrtgP);
            //values.Add(MHEHazIns);
            //values.Add(MHERETaxes);
            //values.Add(MHEMtgIns);
            //values.Add(MHEHOADues);
            //values.Add(MHEOther);
            //values.Add(edtAPP_REAL_MARKET_VALUE);
            //values.Add(edtAPP_REAL_MORTGAGE);
            //values.Add(edtAPP_REAL_MORT_PAY);
            //values.Add(edtAPP_REAL_MONTH_PAY);
            //values.Add(edtAPP_REAL_MARKET_VALUE2);
            //values.Add(edtAPP_REAL_MORTGAGE2);
            //values.Add(edtAPP_REAL_MORT_PAY2);
            //values.Add(edtAPP_REAL_MONTH_PAY2);
            //values.Add(edtAPP_REAL_MARKET_VALUE3);
            //values.Add(edtAPP_REAL_MORTGAGE3);
            //values.Add(edtAPP_REAL_MORT_PAY3);
            //values.Add(edtAPP_REAL_MONTH_PAY3);

            //After all textbox are added we check if it's empty if it's not we delete $ and ,
            for (int i = 0; i < values.Count; i++)
            {
                //if (values[i].Value != "")
                //{
                //    values[i].Value = values[i].Value.Replace("$", "");
                //    values[i].Value = values[i].Value.Replace(",", "");
                //}
            }
        }

        //Function To Copy The Address From Borrower To Co-Borrower
        protected void copyBorrowAddress(object sender, EventArgs e)
        {
            edtAPP_CB_CURR_ADDRESS.Value = edtAPP_PB_CURR_ADDRESS.Value;
            edtAPP_CB_CURR_STATE.SelectedIndex = edtAPP_PB_CURR_STATE.SelectedIndex;
            edtAPP_CB_CURR_CITY.Value = edtAPP_PB_CURR_CITY.Value;
            edtAPP_CB_CURR_ZIP.Value = edtAPP_PB_CURR_ZIP.Value;
        }

        //Function To Copy The Address From Step 2 To 3
        protected void copyPropAddress(object sender, EventArgs e)
        {
            edtAPP_PB_CURR_ADDRESS.Value = edtAPP_PROPERTY_ADDRESS.Value;
            edtAPP_PB_CURR_STATE.SelectedIndex = edtAPP_PROPERTY_STATE.SelectedIndex;
            edtAPP_PB_CURR_CITY.Value = edtAPP_PROPERTY_CURR_CITY.Value;
            edtAPP_PB_CURR_ZIP.Value = edtAPP_PROPERTY_ZIP.Value;
        }

        //Move Next Step
        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex++;
            if (edtAPP_PB_OWNERSHIP.SelectedIndex == 0)
            {
                edtAPP_REAL_ADDRESS.Value = edtAPP_PB_CURR_ADDRESS.Value;
                edtAPP_REAL_PROPERTY_STATE.SelectedIndex = edtAPP_PB_CURR_STATE.SelectedIndex;
                edtAPP_REAL_PROPERTY_CITY.Value = edtAPP_PB_CURR_CITY.Value;
                edtAPP_REAL_PROPERTY_ZIP.Value = edtAPP_PB_CURR_ZIP.Value;
            }
        }

        //Called By All Buttons Like Save And Continue, Submit or Update
        protected void btnSaveAndContinue_Click(object sender, EventArgs e)
        {
            SaveData(uid());
        }

        //Move Between Steps
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton value = sender as LinkButton;
            int val = int.Parse(value.Text) - 1;
            MultiView1.ActiveViewIndex = val;
            if (edtAPP_PB_OWNERSHIP.SelectedIndex == 0)
            {
                edtAPP_REAL_ADDRESS.Value = edtAPP_PB_CURR_ADDRESS.Value;
                edtAPP_REAL_PROPERTY_STATE.SelectedIndex = edtAPP_PB_CURR_STATE.SelectedIndex;
                edtAPP_REAL_PROPERTY_CITY.Value = edtAPP_PB_CURR_CITY.Value;
                edtAPP_REAL_PROPERTY_ZIP.Value = edtAPP_PB_CURR_ZIP.Value;
            }
        }

        //Verify If There Are More Than Value In The LO Dropdown
        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            if (drpMortgageSpecialist1.Items.Count > 0)
            {
                drpMortgageSpecialist1.Items.Insert(0, new ListItem("Please Select", ""));
                drpMortgageSpecialist1.Items.Insert(1, new ListItem("No, I am not", "noyet"));
            }
            else
            {
                pnlConsultant.Visible = false;
                hdfConsultant.Value = "false";
            }
        }

        //Evaluate If It's First Time Or A Previous Registration
        protected void Login_Click(object sender, EventArgs e)
        {
            //Get basic ids from Site ID and Lo ID
            Guid siteid = new Guid(MasterGlobal.SiteID());
            Guid agentid = new Guid(Consultants.Data().ID);

            //Variable to save errors
            string err;

            //Variable to set if the user is returning
            Guid userid = Guid.Empty;
            Connections t = new Connections();

            string s = string.Format("exec [dbo].[sp_USER_IDofAlead] '{0}', '{1}', '{2}' {3}",
                siteid, txtEmail.Value, txtPwd.Value, agentid.Equals(Guid.Empty) ? "" : string.Format(",'{0}'", agentid));
            ArrayList al = t.select(s, out err);

            if (err.Length > 0)
            {
                //NewClientSites.UIF.SendErrorNotification(s + "....." + err);
                return;
            }

            //If user exist add its ID
            if (al != null && al.Count > 0)
            {
                userid = new Guid(Connections.getHashtableField(al[0], "USER_ID"));
            }
            if (!userid.Equals(Guid.Empty))
            {
                //If user exist set its ID
                u.Value = userid.ToString();

                //After set the user, we get values pre filled
                SetFields(userid);

                //To hide submit and show update button
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
            }
            else
            {
                //If the user doesn't exist hide update button and to show submit
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

                //To move next step
                MultiView1.ActiveViewIndex++;
            }

            //Clean message error
            lblError.Text = "";
             
            //Set borrower email in step 2
            this.edtBorrowerEmail.Value = txtEmail.Value;
        }

        //Get Values For The LO Dropdown
        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            Guid g = new Guid( MasterGlobal.SiteID());
            if (g == null || g.Equals(Guid.Empty))
            {
                e.Cancel = true;
                return;
            }
            e.Command.Parameters[0].Value = g.ToString();
            e.Command.CommandTimeout = 300;
        }

        //Go To A Previous Step
        protected void btnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex--;
            if (edtAPP_PB_OWNERSHIP.SelectedIndex == 0)
            {
                edtAPP_REAL_ADDRESS.Value = edtAPP_PB_CURR_ADDRESS.Value;
                edtAPP_REAL_PROPERTY_STATE.SelectedIndex = edtAPP_PB_CURR_STATE.SelectedIndex;
                edtAPP_REAL_PROPERTY_CITY.Value = edtAPP_PB_CURR_CITY.Value;
                edtAPP_REAL_PROPERTY_ZIP.Value = edtAPP_PB_CURR_ZIP.Value;
            }
        }

        //Recover Password
        protected void Remember_Click(object sender, EventArgs e)
        {
            lblErrM.Text = "";
            lblErrM.Visible = false;

            Autoresponders.BorrowerCo dataForm = new Autoresponders.BorrowerCo();

            Connections conn = new Connections();

            try
            {
                using (SqlDataReader reader = conn.ExecuteReader("SELECT TOP 1 txtPwd, txtFirstName, txtLastName FROM tbluserdetails WHERE txtEmail = @EMAIL AND site_id = @SITE_ID ORDER BY lastchangedate DESC",
                        new System.Data.SqlClient.SqlParameter[] { 
                        new System.Data.SqlClient.SqlParameter("@SITE_ID", MasterGlobal.SiteID()),
                        new System.Data.SqlClient.SqlParameter("@EMAIL", txtEmail.Value)}))
                {
                    if (reader.Read())
                    {
                        string url = link + "&p=" + (String.IsNullOrEmpty(Request.QueryString["p"]) ? "applynow.ascx" : Request.QueryString["p"]) + (String.IsNullOrEmpty(Request.QueryString["type"]) ? "" : "&type=" + Request.QueryString["type"]);

                        dataForm.BorrowerFirstName = reader["txtFirstName"].ToString().Trim();
                        dataForm.BorrowerLastName = reader["txtLastName"].ToString().Trim();
                        dataForm.BorrowerEmail = txtEmail.Value;
                        dataForm.BorrowerPassword = reader["txtPwd"].ToString().Trim();
                        //Autoresponders.General(dataForm, "password");
                        Autoresponders.send_AutoResponderLead(dataForm, "password");

                        Response.Redirect(url);
                    }
                    else
                    {
                        this.lblErrM.Visible = true;
                        this.lblErrM.Text = "Sorry, your registration doesn't exit.";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}