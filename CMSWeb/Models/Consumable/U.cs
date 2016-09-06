using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

using CMSWeb.Models.Generic;
using System.Text;
using CMSWeb.Models.Handler;
using System.Data;
using System.Reflection;
using System.Globalization;
using System.Xml.XPath;
using System.IO;
using System.Xml;

namespace CMSWeb.Models.Consumable
{
    public class Users
    {

        #region string properties
        private static string[][] allProperties = new string[][]
		{
			new string[]{"txtFirstName", "Borrower First Name", "APP_PB_FIRST_NAME", "both"},
			new string[]{"txtLastName", "Borrower Last Name", "APP_PB_LAST_NAME", "both"},
			new string[]{"txtMiddleName", "Borrower Middle Name", ""},
			new string[]{"txtEmail", "Borrower Email", "", "both"}, //APP_EMAIL
            new string[]{"emailStatus", "Email Status", "", "both"}, //CRM-2828
            new string[]{"REGISTERED_DATE", "Borrower REGISTERED_DATE", ""},    
            new string[]{"LastChangeDate", "Borrower Last Change Date", ""},                
			new string[]{"BorrowerAddress", "Borrower Address", "APP_PB_CURR_ADDRESS", "both"},
			new string[]{"BorrowerCity", "Borrower City", "APP_PB_CURR_CITY", "both"},
			new string[]{"BorrowerState", "Borrower State", "APP_PB_CURR_STATE", "both"},
			new string[]{"BorrowerZip", "Borrower Zip", "APP_PB_CURR_ZIP", "both"},
			new string[]{"txtPhone", "Borrower Home Phone", "APP_PB_PHONE1", "both"},
			new string[]{"WorkPhone", "Borrower Work Phone", "APP_PB_PHONE2", "both"},
			new string[]{"txtCellPhone", "Borrower Cell Phone", ""},
			new string[]{"CompName", "Company", ""},
			new string[]{"Fax", "Borrower Fax", ""},
			new string[]{"Pager", "Borrower Pager", ""},
			new string[]{"BestTimeToCall", "Borrower Best Time to Call", ""},
			new string[]{"SocNum", "Borrower Social Security#", "APP_PB_SOC_NO"},
			new string[]{"DOB", "Borrower Date of Birth", "APP_PB_DOB"},
			new string[]{"Race", "Borrower Race", ""},
			new string[]{"Sex", "Borrower Sex", ""},
			new string[]{"Ethnicity", "Borrower Ethnicity", ""},
			new string[]{"CreditScore", "Borrower Credit Score", ""},
		 	new string[]{"TUCreditScore", "Borrower TU Credit Score", ""},
			new string[]{"EQCreditScore", "Borrower EQ Credit Score", ""},
            new string[]{"BorrowerGrossMonthlyIncome", "Borrower Gross Monthly Income", ""},
            new string[]{"BorrowerGrossIncome", "Borrower Gross Income", ""},            
            
			new string[]{"PreviousEmployment", "Previous Employment", ""},
			new string[]{"AssetsAmount", "Assets Amount", ""},
			new string[]{"AssetsType", "Assets Type", ""},
			new string[]{"AssetsSeasoning", "Assets Seasoning", ""},

			new string[]{"MartialStatus", "Borrower Marital Status", "APP_PB_MARITAL"},
			new string[]{"Ownership", "Borrower Ownership", "APP_PB_OWNERSHIP"},
			new string[]{"EmployerName", "Borrower Employer Name", "APP_PB_EMP_NAME"},
			new string[]{"EmployerAddress", "Borrower Employer Address", "APP_PB_EMP_ADDRESS"},
			new string[]{"EmployerCity", "Borrower Employer City", "APP_PB_EMP_CITY"},
			new string[]{"EmployerState", "Borrower Employer State", "APP_PB_EMP_STATE"},
			new string[]{"EmployerZip", "Borrower Employer Zip", "APP_PB_EMP_ZIP"},
			new string[]{"EmployerCSZ", "Borrower Employer CSZ", ""},
			new string[]{"EmployerPhone", "Borrower Employer Phone", "APP_PB_EMP_PHONE"},
			new string[]{"EmployerTitle", "Borrower Employer Title", "APP_PB_EMP_TITLE"},
			new string[]{"MonthsWithEmloyer", "Borrower Months With Emloyer", "APP_PB_EMP_MONTHS"},
			new string[]{"BaseEmploymentIncome", "Borrower Base Employment Income", "APP_INC_BASE"},
			new string[]{"TotalIncome", "Borrower Total Income", ""},
			new string[]{"Overtime", "Borrower Overtime", "APP_INC_OVERTIME"},
			new string[]{"Bonuses", "Borrower Bonuses", "APP_INC_BONUSES"},
			new string[]{"Commissions", "Borrower Commissions", "APP_INC_COMMISSIONS"},
			new string[]{"Dividends", "Borrower Dividends", "APP_INC_DIVIDENTS"},
			new string[]{"NetRentalIncome", "Borrower Net Rental Income", "APP_INC_RENTAL_INC"},
			new string[]{"SelfEmployed", "Borrower Self Employed", "APP_PB_EMP_SELF"},
			new string[]{"TypeOfBusiness", "Borrower Type Of Business", ""},

			new string[]{"txtPropertyAdress", "Property Address", "APP_PROPERTY_ADDRESS"},
			new string[]{"txtCity", "Property City", "APP_PROPERTY_CITY"},
			new string[]{"txtState", "Property State", "APP_PROPERTY_STATE"},
			new string[]{"intZip", "Property Zip", "APP_PROPERTY_ZIP"},
			new string[]{"Property Value/Price", "Property Value/Price", "HOME_VALUE"},
			new string[]{"PropertyType", "Property Type", "APP_PROPERTY_TYPE"},
			new string[]{"PropertyWillBe", "Property Will Be", "APP_OCCUPANCY_TYPE"},
			new string[]{"County", "County", ""},
			new string[]{"BuyerID", "Buyer ID Code", ""},

			new string[]{"CoFirstName", "Co-Borrower First Name", "APP_CB_FIRST_NAME"},
			new string[]{"CoLastName", "Co-Borrower Last Name", "APP_CB_LAST_NAME"},
			new string[]{"CoEmail", "Co-Borrower Email", ""},
			new string[]{"CoAddress", "Co-Borrower Address", "APP_CB_CURR_ADDRESS"},
			new string[]{"CoCity", "Co-Borrower City", "APP_CB_CURR_CITY"},
			new string[]{"CoState", "Co-Borrower State", "APP_CB_CURR_STATE"},
			new string[]{"CoZip", "Co-Borrower Zip", "APP_CB_CURR_ZIP"},
			new string[]{"CoPhone", "Co-Borrower Home Phone", "APP_CB_PHONE1"},
			new string[]{"CoWorkPhone", "Co-Borrower Work Phone", "APP_CB_PHONE2"},
			new string[]{"CoCellPhone", "Co-Borrower Cell Phone", ""},
			new string[]{"CoFax", "Co-Borrower Fax", ""},
			new string[]{"CoPager", "Co-Borrower Pager", ""},
			new string[]{"CoBestTimeToCall", "Co-Borrower Best Time to Call", ""},
			new string[]{"CoSocNum", "Co-Borrower Social Security#", "APP_CB_SOC_NO"},
			new string[]{"CoDOB", "Co-Borrower Date of Birth", "APP_CB_DOB"},
			new string[]{"CoRace", "Co-Borrower Race", ""},
			new string[]{"CoSex", "Co-Borrower Sex", ""},
			new string[]{"CoEthnicity", "Co-Borrower Ethnicity", ""},
			new string[]{"CoCreditScore", "Co-Borrower Credit Score", ""},
			new string[]{"CoTUCreditScore", "Co-Borrower TU Credit Score", ""},
			new string[]{"CoEQCreditScore", "Co-Borrower EQ Credit Score", ""},
			new string[]{"CoPreviousEmployment", "Co-Borrower Previous Employment", ""},
			new string[]{"CoAssetsAmount", "Co-Borrower Assets Amount", ""},
			new string[]{"CoAssetsType", "Co-Borrower Assets Type", ""},
			//new string[]{"CoAssetsSeasoning", "Co-Borrower Assets Seasoning", ""},
			new string[]{"CoTypeOfBusiness", "Co-Borrower Type Of Business", ""},


			new string[]{"CoMartialStatus", "Co-Borrower Marital Status", "APP_CB_MARITAL"},
			new string[]{"CoOwnership", "Co-Borrower Ownership", "APP_CB_OWNERSHIP"},
			new string[]{"CoEmployerName", "Co-Borrower Employer Name", "APP_CB_EMP_NAME"},
			new string[]{"CoEmployerAddress", "Co-Borrower Employer Address", "APP_CB_EMP_ADDRESS"},
			new string[]{"CoEmployerCity", "Co-Borrower Employer City", "APP_CB_EMP_CITY"},
			new string[]{"CoEmployerState", "Co-Borrower Employer State", "APP_CB_EMP_STATE"},
			new string[]{"CoEmployerZip", "Co-Borrower Employer Zip", "APP_CB_EMP_ZIP"},
			new string[]{"CoEmployerCSZ", "Co-Borrower Employer CSZ", ""},
			new string[]{"CoEmployerPhone", "Co-Borrower Employer Phone", "APP_CB_EMP_PHONE"},
			new string[]{"CoEmployerTitle", "Co-Borrower Employer Title", "APP_CB_EMP_TITLE"},
			new string[]{"CoMonthsWithEmloyer", "Co-Borrower Months With Emloyer", "APP_CB_EMP_MONTHS"},
			new string[]{"CoBaseEmploymentIncome", "Co-Borrower Base Employment Income", "APP_INC_BASE_CB"},
			new string[]{"CoBoIncome", "Co-Borrower Total Income", ""},
			new string[]{"CoOvertime", "Co-Borrower Overtime", "APP_INC_OVERTIME_CB"},
			new string[]{"CoBonuses", "Co-Borrower Bonuses", "APP_INC_BONUSES_CB"},
			new string[]{"CoCommissions", "Co-Borrower Commissions", "APP_INC_COMMISSIONS_CB"},
			new string[]{"CoDividends", "Co-Borrower Dividends", "APP_INC_DIVIDENTS_CB"},
			new string[]{"CoNetRentalIncome", "Co-Borrower Net Rental Income", "APP_INC_RENTAL_INC_CB"},
            new string[]{"CoBorrowerGrossMonthlyIncome", "Co-Borrower Gross Monthly Income", ""},
            
			new string[]{"CoSelfEmployed", "Co-Borrower Self Employed", "APP_CB_EMP_SELF"},
			new string[]{"CoBuyerID", "Co-Borrower Buyer ID Code", ""},

			new string[]{"MailCareOfName", "Mail Care-Of Name", ""},
			new string[]{"MailStreetAddress", "Mail Street Address", ""},
			new string[]{"MailCity", "Mail City Name", ""},
			new string[]{"MailState", "Mail State", ""},
			new string[]{"MailZip", "Mail Zip Code", ""},
			new string[]{"UnitDesignator", "Mailing Address: Unit Designator", ""},
			new string[]{"MailUnitNumber", "Mail Unit Number", ""},

			new string[]{"Loan Type", "Loan purpose", "APP_LOAN_PURPOSE"},
            new string[]{"LoanPurposeOther", "Loan Purpose Other", ""},
            new string[]{"LoanPurposeRefiType", "Loan Purpose Refinance Other", ""},

            new string[]{"LoanType", "LoanType", ""},
            new string[]{"LoanTypeOther", "LoanTypeOther", ""},

			new string[]{"LoanAmount", "Loan Amount", "LOAN_AMOUNT"}, //
			new string[]{"LoanProgram", "Loan Program", ""},
			new string[]{"DownPayment", "Down Payment", ""},
			new string[]{"CoBorrowerApply", "Co-Borrower Apply", ""},
			new string[]{"LoanTerm", "Loan Term", ""},

			new string[]{"Status", "Status", ""},
			new string[]{"NextStep", "Next Step", ""},
			new string[]{"NextStepDate", "Next Step Date", ""},
			new string[]{"EstimatedRevenue", "Estimated Revenue", ""},
			new string[]{"Probability", "Probability", ""},
			new string[]{"EstimatedClosingDate", "Estimated Closing Date", ""},
			new string[]{"LeadSource", "Lead Source", ""},
			new string[]{"MortgageAmount", "Mortgage Amount", ""},
			new string[]{"MortgageDate", "Mortgage Date", ""},
			new string[]{"Lender", "Lender", ""},
			new string[]{"OfferCode", "Offer Code", ""},

			new string[]{"Transaction Type", "Transaction Type", ""},
			new string[]{"Relationship to the property", "Relationship to the property", "APP_PB_OWNERSHIP"},
			new string[]{"Home Type", "Home Type", ""},
			new string[]{"Current interest", "Current interest", ""},
			new string[]{"Current interest 2nd", "Current interest 2nd", ""},
			new string[]{"Balance", "Balance", ""},
			new string[]{"2nd balance", "2nd balance", ""},
			//new string[]{"Loan purpose", "Loan purpose", "APP_LOAN_PURPOSE"},
			new string[]{"Credit rating", "Credit rating", ""},
            new string[]{"BorrowerOccupationalStatus", "Borrower Occupational Status", ""},
            
			new string[]{"Tracking ID", "Tracking ID", ""},

			new string[]{"ListingDate", "Listing Date", ""},
			new string[]{"Bedrooms", "Bedrooms", ""},
			new string[]{"Bathrooms", "Bathrooms", ""},
			new string[]{"PropertyStyle", "Property Style", ""},
			new string[]{"PropertyArea", "Property Square Footage", ""},
			new string[]{"Garage", "Garage", ""},
			new string[]{"YearBuilt", "Year Built", "APP_YEAR_BUILT"},
			new string[]{"Acreage", "Acreage", ""},
			new string[]{"Notes", "Notes", ""},

			new string[]{"RID", "RID", ""},
			new string[]{"FIPS", "FIPS Code", ""},
			new string[]{"PIN", "PIN Number", ""},
			//new string[]{"PropertyUnitNumber", "Property Unit Number", ""},
			new string[]{"Num Of Units", "Property Unit Number", ""},

			new string[]{"PropertyCarrierRoute ", "Property Carrier Route ", ""},
			new string[]{"OwnerOccupiedFlag", "Owner-Occupied Flag", ""},
			new string[]{"LandUse", "Standardized Assessor Land-Use Code", ""},
			new string[]{"Area", "Lot-Size or Area", ""},
			new string[]{"BuildingArea", "Building Area", ""},
			new string[]{"Pool", "Pool", ""},
			new string[]{"SubdivisionName", "Subdivision Name", ""},
			new string[]{"TotalAssessedValue", "Total Assessed Value", ""},
			new string[]{"AssessmentYear", "Assessment Year", ""},
			new string[]{"TotalAssessorMarketValue", "Total Assessor Market Value", ""},
			new string[]{"AssessorMarketValueYear", "Assessor Market Value Year", ""},
			new string[]{"SalesPrice", "Sales Price", ""},
			new string[]{"RecordingDate", "Recording Date", ""},
			new string[]{"RecorderBookNumber", "Recorder’s Book Number", ""},
			new string[]{"RecorderPageNumber", "Recorder’s Page Number", ""},
			new string[]{"DocumentNumber", "Document Number", ""},
			new string[]{"DocumentTypeCode", "Document Type Code", ""},
			new string[]{"RecordedTrustDeed", "Document # or Book/Page of Concurrently Recorded Trust Deed", ""},
			new string[]{"LenderName", "Lender Name (Beneficiary)", ""},
			new string[]{"LenderTypeCode", "Lender Type Code", ""},
			new string[]{"TypeFinancing", "Type Financing", ""},
			new string[]{"InitialInterestRate ", "Initial Interest Rate ", ""},
			new string[]{"TrustDeedDueDate", "Trust Deed Due Date", ""},
			new string[]{"CurrentLoanToValue", "Combined LTV Ratio", ""},
			new string[]{"Margin", "Margin", ""},
			new string[]{"Index", "Index", ""},
			new string[]{"OriginalLoanToValue", "Original Loan-to-Value", ""},
			new string[]{"AdjustableRateRider", "Adjustable Rate Rider", ""},
			new string[]{"AdjustableRateIndex", "Adjustable Rate Index", ""},
			new string[]{"ChangeIndex ", "Change Index ", ""},
			new string[]{"RateChangeFrequency", "Rate Change Frequency", ""},
			new string[]{"InterestRateNotGreaterThan", "Interest Rate Not Greater Than", ""},
			new string[]{"InterestRateNotLessThan ", "Interest Rate Not Less Than ", ""},
			new string[]{"MaximumInterestRate ", "Maximum Interest Rate ", ""},
			new string[]{"InterestOnlyPeriod", "Interest Only Period", ""},
			new string[]{"FixedStepRateRider", "Fixed/Step (Conversion) Rate Rider", ""},
			new string[]{"FirstChangeYear", "First Change Date: Year", ""},
			new string[]{"First ChangeMonthDate", "First Change Date: Month & Date", ""},
			new string[]{"PrepaymentRider", "Prepayment Rider", ""},
			new string[]{"PrepaymentPenaltyRiderMonths", "Prepayment Penalty Rider: Term in Months", ""},
			new string[]{"Source", "Source", ""},
			new string[]{"PID", "PID", ""},//LoanAppraisedValue
			new string[]{"ImagePath", "Image Path", ""},
			new string[]{"TD2LOANAMOUNT", "TD2LOANAMOUNT", ""},
			new string[]{"TD2LoanType", "TD2LoanType", ""},
			new string[]{"TD2LenderName", "TD2LenderName", ""},
			new string[]{"TD2LenderTypeCode", "TD2LenderTypeCode", ""},
			new string[]{"TD2TypeFinancing", "TD2TypeFinancing", ""},
			new string[]{"TD2Rate", "TD2Rate", ""},
			new string[]{"TD2DueDate", "TD2DueDate", ""},
			new string[]{"TD2RECORDINGDATE", "TD2RECORDINGDATE", ""},

			new string[]{"Association", "Association", ""},
			new string[]{"MTANumber", "MTA Number", ""},

			new string[]{"ClosingDate", "Closing Date", ""},
			new string[]{"CreditReferenceNumber", "Credit Reference Number", ""},
			new string[]{"StatDate", "Stat Date", ""},
			new string[]{"IntRate", "Int Rate", ""},
			new string[]{"LienPosition", "Lien Position", ""},
			new string[]{"DotMortgageDate", "Dot/Mortgage Date", ""},
			new string[]{"NoteDueDate", "Note Due Date", ""},
			new string[]{"BalloonDueDate", "Balloon Due Date", ""},
			new string[]{"InterestOnly", "Interest Only?", ""},
			new string[]{"Amortization", "Amortization", ""},
			new string[]{"FileOrignator", "File Orignator", ""},
			//new string[]{"Occupancy", "Occupancy", ""}, occupancy = property will be
			new string[]{"PurchasePrice", "Purchase Price", ""},
			new string[]{"LoanAppraisedValue", "Appraised Value", ""},
            new string[]{"LoanAppraisedValue", "Loan Appraised Value", ""},
			new string[]{"PrepayPenalty", "Prepay Penalty [may/will not]", ""},
			new string[]{"PurchasePrice", "Purchase Price", ""},
			//new string[]{"LoanAppraisedValue", "Loan Appraised Value", ""},
			new string[]{"LenderName", "Lender's Name", ""},

			//new string[]{"AppraisedValue", "Appraised Value", ""},
			new string[]{"ProductType", "Product Type", ""},
			new string[]{"AppraisalCompany", "Appraisal Company", ""},
			new string[]{"TitleInsuranceCompany", "Title Insurance Company", ""},

			new string[]{"DeclarationA", "a. Are there any outstanding judgments against you?", "APP_Q_A"},
			new string[]{"DeclarationB", "b. Have you declared bankruptcy within the last 7 years?", "APP_Q_B"},
			new string[]{"DeclarationC", "c. Have you had property foreclosed upon in the last 7 years?", "APP_Q_C"},
			new string[]{"DeclarationD", "d. Are you a party to a lawsuit?", "APP_Q_D"},
			new string[]{"DeclarationE", "e. Have you directly or indirectly been obligated on any loan which resulted in foreclosure, transfer of title in lieu of foreclosure, or judgment?", "APP_Q_E"},
			new string[]{"DeclarationF", "f. Are you presently delinquent or in default on any Federal debt or any other loan, mortgage, financial obligation, bond or loan guarantee?", "APP_Q_F"},
			new string[]{"DeclarationG", "g. Are you obligated to pay alimony child support or separate maintenance?", "APP_Q_G"},
			new string[]{"DeclarationH", "h. Is any part of the down payment borrowed?", "APP_Q_H"},
			new string[]{"DeclarationI", "i. Are you the maker or endorser on a note?", "APP_Q_I"},
			new string[]{"DeclarationJ", "j. Are you a U.S. citizen?", "APP_Q_J"},
			new string[]{"DeclarationK", "k. Are you a permanent resident alien?", "APP_Q_K"},
			new string[]{"DeclarationL", "l. Do you intend to occupy the property as your primary residence?", "APP_Q_L"},
			new string[]{"DeclarationM", "m. Have you had an ownership interest in a property in the last 3 years?", "APP_Q_M"},
			new string[]{"Declaration1", "1. What type of property did you own?:", "APP_Q_1"},
			new string[]{"Declaration2", "2. How did you hold title to the home?", "APP_Q_2"},
			
			new string[]{"CoDeclarationA", "a. [Co-Borrower] Are there any outstanding judgments against you?", ""},
			new string[]{"CoDeclarationB", "b. [Co-Borrower] Have you declared bankruptcy within the last 7 years?", ""},
			new string[]{"CoDeclarationC", "c. [Co-Borrower] Have you had property foreclosed upon in the last 7 years?", ""},
			new string[]{"CoDeclarationD", "d. [Co-Borrower] Are you a party to a lawsuit?", ""},
			new string[]{"CoDeclarationE", "e. [Co-Borrower] Have you directly or indirectly been obligated on any loan which resulted in foreclosure, transfer of title in lieu of foreclosure, or judgment?", ""},
			new string[]{"CoDeclarationF", "f. [Co-Borrower] Are you presently delinquent or in default on any Federal debt or any other loan, mortgage, financial obligation, bond or loan guarantee?", ""},
			new string[]{"CoDeclarationG", "g. [Co-Borrower] Are you obligated to pay alimony child support or separate maintenance?", ""},
			new string[]{"CoDeclarationH", "h. [Co-Borrower] Is any part of the down payment borrowed?", ""},
			new string[]{"CoDeclarationI", "i. [Co-Borrower] Are you the maker or endorser on a note?", ""},
			new string[]{"CoDeclarationJ", "j. [Co-Borrower] Are you a U.S. citizen?", ""},
			new string[]{"CoDeclarationK", "k. [Co-Borrower] Are you a permanent resident alien?", ""},
			new string[]{"CoDeclarationL", "l. [Co-Borrower] Do you intend to occupy the property as your primary residence?", ""},
			new string[]{"CoDeclarationM", "m. [Co-Borrower] Have you had an ownership interest in a property in the last 3 years?", ""},
			new string[]{"CoDeclaration1", "1. [Co-Borrower] What type of property did you own?:", ""},
			new string[]{"CoDeclaration2", "2. [Co-Borrower] How did you hold title to the home?", ""},
            
            //Feb 15, 2008, Loan Info Section:
            new string[]{"OwnerOccupied", "Owner Occupied", ""},
            new string[]{"FirstMortgageLoanRateType", "First Mortgage Loan Rate Type", ""},
            new string[]{"FirstMortgageInterestRateInitialChangeDate", "First Mortgage Interest Rate Initial Change Date", ""},
            new string[]{"FirstMortgageInterestRateNextChangeDate", "First Mortgage Interest Rate Next Change Date", ""},
            new string[]{"FirstMortgageLTV", "First Mortgage LTV", ""},
            new string[]{"FirstMortgageLenderName", "First Mortgage Lender Name", ""},
            new string[]{"FirstMortgagePrePaymentPenaltyExpirationDate", "First Mortgage Pre-Payment Penalty Expiration Date", ""},
            new string[]{"Junior1MortgageAmount", "Junior 1 Mortgage Amount", ""},
            new string[]{"Junior1MortgageAssignedDate", "Junior 1 Mortgage Assigned Date", ""},
            //Feb 15, 2008,  Borrower Section:
            //txtMiddleName new string[]{"BorrowerMiddleName", "Borrower Middle Name", ""},
            new string[]{"BorrowerFullName", "Borrower Full Name", ""},            
            //Feb 15, 2008,  Co-Borrower Section:
            new string[]{"CoBorrowerMiddleName", "Co-Borrower Middle Name", ""},
            new string[]{"CoBorrowerFullName", "Co-Borrower Full Name", ""},            
            //Feb 15, 2008,  Property Information Section:
            new string[]{"PropertyTaxAmount", "Property Tax Amount", ""},
            new string[]{"PropertyYearBuilt", "Property Year Built", ""},
            //Oct 06, 2008,  Borrower Information:
            new string[]{"SpouseFullName", "Spouse Full Name", ""},
            
            new string[]{"MortgageBalancePrincipal", "Mortgage Balance Principal", ""},
            new string[]{"MortgageBalanceFirst", "Mortgage Balance (1st)", ""},
            new string[]{"MortgageBalanceSecond", "Mortgage Balance (2nd)", ""},              

            new string[]{"InterestRateSecond", "Interest Rate (2nd)", ""},
            new string[]{"RefinancePurpose", "Refinance Purpose", ""},
            new string[]{"LoanTimeframe", "Loan Timeframe", ""},
            new string[]{"Bankruptcy", "Bankruptcy", ""},
            //new string[]{"DatePropertyPurchased", "Date Property Purchased", ""},
            new string[]{"Date Property Purchased", "Date Property Purchased", ""},
            //new string[]{"Date Property Purchased", "Date Property Purchased", ""},

            new string[]{"LoanAdditionalCash", "Loan Additional Cash", ""},
            new string[]{"LoanVAStatus", "Loan VA Status", ""},
            new string[]{"LoanNumberOfMortgageLates", "Loan Number Of Mortgage Lates", ""},

            new string[]{"SalesDetailsClass", "Sales Details Class", ""},
            new string[]{"SalesDetailsGrade", "Sales Details Grade", ""},

            new string[]{"LoanAmountDesired", "Loan Amount Desired", ""},
            
            new string[]{"LoanProgramDesiredFirst", "Loan Amount Desired First", ""},//related to LoanRateDesiredFirst
            new string[]{"LoanRateDesiredFirst", "Loan Rate Desired First", ""},
            new string[]{"LoanPointDesiredFirst", "Loan Point Desired First", ""},
            
            new string[]{"LoanProgramDesiredSecond", "Loan Amount Desired Second", ""},//related to LoanRateDesiredSecond
            new string[]{"LoanRateDesiredSecond", "Loan Rate Desired Second", ""},
            new string[]{"LoanPointDesiredSecond", "Loan Point Desired Second", ""},

            new string[]{"LoanProgramDesiredThird", "Loan Amount Desired Third", ""},//related to LoanRateDesiredThird
            new string[]{"LoanRateDesiredThird", "Loan Rate Desired Third", ""},
            new string[]{"LoanPointDesiredThird", "Loan Point Desired Third", ""},
            //next 6 are new for FerarriBiz1:
            new string[]{"BorrowerYearsInBusiness", "Borrower Years In Business", ""},
            new string[]{"BorrowerBusinessGeneralLocation", "Borrower Business General Location", ""},
            new string[]{"BorrowerTypeOfBusiness", "Borrower Type Of Business", ""},
            new string[]{"BorrowerAvailableFunds", "Borrower Available Funds", ""},
            new string[]{"BorrowerIndustryOfInterest", "Borrower Industry Of Interest", ""},
            new string[]{"BorrowerGeographicAreas", "Borrower Geographic Areas", ""},
            //next 6 are from LeadPoint:
            new string[]{"SalesDetailsFoundAHome", "Sales Details: Found a home?", ""},
            new string[]{"SalesDetailsWhenToBuyHome", "Sales Details: When to buy home", ""},
            new string[]{"SalesDetailsSingedAPurchaseContract", "Sales Details: Singed a Purchase Contract", ""},
            new string[]{"SalesDetailsIsFirstTimeHomeBuyer", "Sales Details: Is First Time Home Buyer", ""},
            new string[]{"SalesDetailsHaveRealEstateAgent", "Sales Details: Has Real Estate Agent", ""},
            new string[]{"SalesDetailsRealEstateOffice", "Sales Details: Real Estate Office", ""},
            //maverickfunding March 30, 2010:
            new string[]{"Borrower SUFFIX", "Borrower SUFFIX", ""},
            new string[]{"Borrower ZIP4", "Borrower ZIP4", ""},
            new string[]{"DPC", "Other: DPC", ""},
            new string[]{"CRRT", "Other: CRRT", ""},
            new string[]{"Other: COUNTY", "Other: COUNTY", ""},
            new string[]{"FICO", "Other: FICO", ""},
            new string[]{"Other: Credit Cards", "Other: Credit Cards", ""},
            new string[]{"Credit Payments", "Other: Credit Payments", ""},
            new string[]{"Mortgage Payment", "Other: Mortgage Payment", ""},
            new string[]{"HELOC", "Other: HELOC", ""},
            new string[]{"HELOC Payment", "Other: HELOC Payment", ""},
            new string[]{"Total Debt Balance", "Other: Total Debt Balance", ""},
            new string[]{"Total Debt Payment", "Other: Total Debt Payment", ""},
            new string[]{"New Refi Pmt", "Other: New Refi Pmt", ""},
            new string[]{"Total Savings", "Other: Total Savings", ""},
            new string[]{"Savings 1 year", "Other: Savings 1 year", ""},
            new string[]{"Savings 10 Years", "Other: Savings 10 Years", ""},
            new string[]{"Approval Code", "Other: Approval Code", ""},

            new string[]{"cust phone", "Other: cust phone", ""},
            new string[]{"job id", "Other: job id", ""},
            new string[]{"perm id", "Other: perm id", ""},
            new string[]{"level1", "Other: level1", ""},
            new string[]{"ph", "Other: ph", ""},
            new string[]{"num crc", "Other: num crc", ""},
            new string[]{"num mtg", "Other: num mtg", ""},
            new string[]{"total mtg", "Other: total mtg", ""},
            new string[]{"mtg int", "Other: mtg int", ""},
            new string[]{"credint", "Other: credint", ""},
            new string[]{"unit", "Other: unit", ""},
            new string[]{"ps_trynum", "Other: ps_trynum", ""},            
            new string[]{"LoanOriginalNoteRate", "LoanOriginalNoteRate", ""},
            new string[]{"LoanFirstPaymentDate", "LoanFirstPaymentDate", ""},
            new string[]{"LoanDateClosed", "LoanDateClosed", ""},
            new string[]{"LoanFICO", "LoanFICO", ""},
            new string[]{"LoanSalesPrice", "LoanSalesPrice", ""},

            new string[]{"LoanNumber", "LoanNumber", ""},
            new string[]{"LoanLastName", "LoanLastName", ""},
            new string[]{"BorrowerStreetAddress2", "BorrowerStreetAddress2", ""},
            new string[]{"LoanDTI", "LoanDTI", ""},

            new string[]{"BorrowerTitle", "Borrower Title", ""},
            new string[]{"CoTitle", "CoBorrower Title", ""},
            new string[]{"CompanyMonthlyVolume", "Company Monthly Volume", ""},
            new string[]{"CompanyNotes", "Company Notes", ""},

				new string[]{"PipelineSellingCompany", "Pipeline Selling Company", ""},
				new string[]{"PipelineBorrowerName", "Pipeline Borrower Name", ""},
				new string[]{"PipelineInterestRate", "Pipeline Interest Rate", ""},
				new string[]{"PipelineLoanAmount", "Pipeline Loan Amount", ""},
				new string[]{"PipelineTypeOfLoan", "Pipeline Type Of Loan", ""},
				new string[]{"PipelineCloseDate", "Pipeline Close Date", ""},
				new string[]{"PipelineBuyback", "Pipeline Buyback", ""},
				new string[]{"PipelinePricingWeGave", "Pipeline Pricing We Gave", ""},
				new string[]{"PipelineDateOfSubmission", "Pipeline Date Of Submission", ""},
				new string[]{"PipelineStatus", "Pipeline Status", ""},
				new string[]{"PipelineNotes", "Pipeline Notes", ""},

				// Maverick Branching documents
				new string[]{"MaverickApproveBranchApplication", "Branch Application", ""},
				new string[]{"MaverickApproveLeaseAgreement", "Executed Office Lease Agreement", ""},
				new string[]{"MaverickApproveBills", "Office Utility Bills", ""},
				new string[]{"MaverickApproveOfficeEquipmentLeases", "Office Equipment Leases", ""},
				new string[]{"MaverickApproveOther", "Other", ""},
				new string[]{"MaverickApproveBranchManagerAgreement", "Branch Manager Agreement", ""},
				new string[]{"MaverickApproveAdditionalStaffApplication", "Additional Staff Application", ""},

            new string[]{"AuthorizationToPullCredit", "Authorization to pull credit", ""},
            new string[]{"PlaceOfEmployment", "Place of Employment", ""},
            new string[]{"MonthlyGrossIncome", "Monthly Gross Income", ""},
            new string[]{"CreditCheckAuthorization", "Credit Check Authorization", ""},

            new string[]{"LeadType", "Contact Type", ""},
            new string[]{"LTV", "LTV", ""},
            new string[]{"REF ID", "Referral Source", ""},
            new string[]{"Nickname", "Nickname", ""},

            new string[]{"MICertNumber", "MI Cert Number", ""},
            new string[]{"MIType", "MI Type", ""},
            new string[]{"MIAmount", "MI Amount", ""},
            new string[]{"MIRate", "MI Rate", ""},
            new string[]{"MIPremium Type", "MI Premium Type", ""},
            new string[]{"Occupancy", "Occupancy", ""},
            new string[]{"OriginalLoanAmount", "Original Amount", ""},
            new string[]{"GSEType", "GSE Type", ""},
            new string[]{"BorrowerMailingAddress", "Borrower Mailing Address", ""},
            new string[]{"BorrowerMailingCity", "Borrower Mailing City", ""},
            new string[]{"BorrowerMailingState", "Borrower Mailing State", ""},
            new string[]{"BorrowerMailingZip", "Borrower Mailing Zip", ""},

            new string[]{"FieldNo", "Field No", ""},
				new string[]{"CurrentMonthlyPayments", "Current Monthly Payments", ""},
				new string[]{"Escrow", "Escrow", ""},
	
				new string[]{"CreaditRate", "Creadit Rate", ""},
				new string[]{"DoNotCall", "Do Not Call", ""},
				new string[]{"DoNotMail", "Do Not Mail", ""},
				new string[]{"DoNotEMail", "Do Not E-Mail", ""},
				new string[]{"IncludeRemodelingCost", "Include Remodeling Cost?", ""},

				new string[]{"BorrowerPreviousAddr", "Borrower Previous Addr", ""},
				new string[]{"BorrowerCurrentaddress", "Borrower Current address", ""},
				new string[]{"AppraisalReceived", "Appraisal Received", ""},
				new string[]{"ApprovalDate", "Approval Date", ""},
				new string[]{"LockExpDate", "Lock Exp Date", ""},
				new string[]{"FirstPaymentDate", "First Payment Date", ""},
				new string[]{"CancellationDate", "Cancellation Date", ""},
				new string[]{"TotalProposedMP", "Total Proposed MP", ""},
				new string[]{"DocType", "Doc Type", ""},
				new string[]{"ARMMargin", "ARM Margin", ""},
				new string[]{"ARMIndex", "ARM Index", ""},
				new string[]{"TransDetailsSubFinSecondMtgLoanAmt", "Trans Details Sub Fin Second Mtg Loan Amt", ""},
				new string[]{"RealtorType", "Realtor Type", ""},
				new string[]{"RealtorFirstName", "Realtor First Name", ""},
				new string[]{"RealtorLastName", "Realtor Last Name", ""},
				new string[]{"RealtorPhone", "Realtor Phone ", ""},
				new string[]{"RealtorCellPhone", "Realtor Cell Phone", ""},
				new string[]{"EscrowCompany", "Escrow Company", ""},
				new string[]{"EscrowAddress", "Escrow Address", ""},
				new string[]{"EscrowCity", "Escrow City", ""},
				new string[]{"EscrowState", "Escrow State", ""},
				new string[]{"EscrowZip", "Escrow Zip", ""},
				new string[]{"EscrowPhone", "Escrow Phone", ""},
				new string[]{"EscrowFax", "Escrow Fax", ""},
				new string[]{"Escrow#", "Escrow #", ""},

				new string[]{"DispositionStatus", "Disposition - Status", ""},
				new string[]{"Disposition", "Disposition", ""},
				new string[]{"DispositionSubDisposition", "Disposition - Subdisposition", ""},
				new string[]{"DestinationState", "Destination State", ""},
				new string[]{"RelocationConsultant", "Relocation Consultant", ""},
				new string[]{"ReferralSource_ClientCode", "Referral Source - Client Code", ""},
				new string[]{"ReferralSource_ClientName", "Referral Source - Client Name", ""},

            	new string[]{"Grade", "Grade", ""},
            	//new string[]{"howHeard", "HowYouHeardAboutUs", ""},
            	new string[]{"pWillBeOccupRate", "pWillBeOccupRate", ""},
            	new string[]{"pWillBeGrossRnt", "pWillBeGrossRnt", ""},
            	new string[]{"AmorTypeARMDescr", "AmorTypeARMDescr", ""},
            	//new string[]{"AmorTypeOtherDescr", "AmorTypeOtherDescr", ""},
            	
                new string[]{"AssetsOtherDescr1", "AssetsOtherDescr1", ""},
                new string[]{"AssetsOtherDescr2", "AssetsOtherDescr2", ""},
                new string[]{"AssetsOtherDescr3", "AssetsOtherDescr3", ""},
                new string[]{"AssetsOtherVal4", "AssetsOtherVal4", ""},
                new string[]{"coSelfEmployed", "coSelfEmployed", ""},

                new string[]{"CDFIRevenues", "CDFI Revenues", ""},
                //new string[]{"MIPremium Type", "MIPremiumType", ""},
                //new string[]{"Occupancy", "Occupancy", ""} ,
                new string[]{"OriginalAmount", "OriginalAmount", ""},
                new string[]{"CDFI Fees", "CDFIFees", ""},
                new string[]{"EscrowYN", "EscrowYN", ""},
               // new string[]{"EscrowYN", "EscrowYN", ""},

                new string[]{"AssetNameOfCurBank", "AssetNameOfCurBank", ""},
                new string[]{"AssetCheckingBalance", "AssetCheckingBalance", ""},
                new string[]{"AssetSavingsBalance", "AssetSavingsBalance", ""},
                new string[]{"AssetNameOfFirm", "AssetNameOfFirm", ""},
                new string[]{"AssetBalanceOfOtherInvestments", "AssetBalanceOfOtherInvestments", ""},
                
            new string[]{"PrevAddrRent", "PrevAddrRent", ""},
            new string[]{"PrevAddrNYrs", "PrevAddrNYrs", ""},
            new string[]{"PrevAddrNMs", "PrevAddrNMs", ""},

            new string[]{"MembershipID", "MembershipID", ""},
            new string[]{"CoAssetsSeasoning", "CoBorrowerAssetsSeasoning", ""},

            new string[]{"CoBoAssetNameOfCurBank", "CoBoAssetNameOfCurBank", ""},
            new string[]{"CoBoAssetCheckingBalance", "CoBoAssetCheckingBalance", ""},
            new string[]{"CoBoAssetSavingsBalance", "CoBoAssetSavingsBalance", ""},
            new string[]{"CoBoAssetNameOfFirm", "CoBoAssetNameOfFirm", ""},
            new string[]{"CoBoAssetBalanceOfOtherInvestments", "CoBoAssetBalanceOfOtherInvestments", ""},
            new string[]{"CoBoAssetYearsOfSchool", "CoBoAssetYearsOfSchool", ""},
            new string[]{"CoRent", "CoRent", ""},
            new string[]{"CoPrevAddrRent", "CoPrevAddrRent", ""},
            new string[]{"CoBorrowerYearsInBusiness", "CoBorrowerYearsInBusiness", ""},

            new string[]{"ExpensesIncludingOperatingIncome", "ExpensesIncludingOperatingIncome", ""},
            new string[]{"HomeOwnersInsurance", "HomeOwnersInsurance", ""},
            new string[]{"Mortgage_LienAmounts", "Mortgage_LienAmounts", ""},
            new string[]{"RealEstateMortgagePayments", "RealEstateMortgagePayments", ""},
            new string[]{"AssetYearsOfSchool", "AssetYearsOfSchool", ""},
            new string[]{"Incom", "Income", ""},
            new string[]{"Membership #", "MembershipID", ""},
            new string[]{"coYearsWithEmloyer", "coYearsWithEmloyer", ""},
            new string[]{"CoBoDependentsNum", "CoBoDependentsNum", ""},
            new string[]{"CoOwn", "CoOwn", ""},
            new string[]{"AmorTypeGPMRate", "AmorTypeGPMRate", ""},
            new string[]{"AmorTypeFixedRate", "AmorTypeFixedRate", ""},
            new string[]{"AmorTypeARM", "AmorTypeARM", ""},
            new string[]{"AmorTypeGPMRatePercent", "AmorTypeGPMRatePercent", ""},
            new string[]{"AmorTypeOtherDescr", "AmorTypeOtherDescr", ""},

            new string[]{"Expenses (including interest & depreciation) Operating Income", "ExpensesIncludingOperatingIncome", ""},
            new string[]{"How did you hear about us", "howHeard", ""},
            new string[]{"pWillBeIsSecondary", "pWillBeIsSecondary", ""},

            new string[]{"EPF", "EPF", ""},
            new string[]{"ExistingBanker", "Existing Banker", ""},
            new string[]{"NetWorth", "Net Worth", ""},
            new string[]{"ExistingLineProvider", "Existing Line Provider", ""},
            new string[]{"ParticipationSold", "Participation Sold", ""},
            //new string[]{"propertyWillBe", "propertyWillBe", ""},

            new string[]{"pWillBeIsSecondary", "propertyWillBeIsSecondary", ""},
            new string[]{"pWillBeIsPrimary", "propertyWillBeIsPrimary", ""},
            new string[]{"pWillBeIsInvestment", "propertyWillBeIsInvestment", ""},

             new string[]{"PrevEmployerAddress", "PrevEmployerAddress", ""},
             new string[]{"PrevEmployerCity", "PrevEmployerCity", ""},
             new string[]{"PrevEmployerState", "PrevEmployerState", ""},
             new string[]{"PrevEmployerZip", "PrevEmployerZip", ""},
             new string[]{"PrevEmployerPhone", "PrevEmployerPhone", ""},
             new string[]{"PrevEmployerTitle", "PrevEmployerTitle", ""},
             new string[]{"MonthsWithPrevEmloyer", "MonthsWithPrevEmloyer", ""},
             new string[]{"YearsWithPrevEmloyer", "YearsWithPrevEmloyer", ""},
             new string[]{"BasePrevEmploymentIncome", "BasePrevEmploymentIncome", ""},
             new string[]{"PrevSelfEmployed", "PrevSelfEmployed", ""},

             new string[]{"PrevEmployerName", "PrevEmployerName", ""},
             new string[]{"BorrowerPrevAddress", "BorrowerPrevAddress", ""},
             new string[]{"BorrowerPrevCity", "BorrowerPrevCity", ""},
             new string[]{"BorrowerPrevState", "BorrowerPrevState", ""},
             new string[]{"BorrowerPrevZip", "BorrowerPrevZip", ""},
             new string[]{"PropertyEstimatedValue", "PropertyEstimatedValue", ""},
             
             new string[]{"propertyWillGrossRnt", "propertyWillGrossRnt", ""},
             new string[]{"propertyWillBeOccupRate", "propertyWillBeOccupRate", ""}


			//Add new field for tblUserAdditionalParams / tblUserAdditionalDetails here:
            //new string[]{"[tblUserAdditionalParams].[PARAM_NAME]", "human name of par-r here", ""

		};
        #endregion string properties

        public static string[][] AllProperties
        {
            get { return allProperties; }
        }
        public static IList<string[]> AllPropertiesSorted()
        {
            //allProperties.
            //ArrayList<string[]> al = new ArrayList<string[]>(    BusinessLogicLayer.User.AllProperties);
            IList<string[]> allFields = new List<string[]>(Users.AllProperties);
            return allFields;
        }

        private static IDictionary<string, string[]> loanAppMappings = new SortedList<string, string[]>();
        private static IDictionary<string, string[]> properties = new SortedList<string, string[]>();

        public static string GetPropertyCaption(string propertyName)
        {
            if (loanAppMappings.ContainsKey(propertyName)) return loanAppMappings[propertyName][1];
            else if (properties.ContainsKey(propertyName)) return properties[propertyName][1];
            else return propertyName;
        }

        static Users()
        {
            foreach (string[] data in allProperties)
            {
                if (data.Length > 2 && !string.IsNullOrEmpty(data[2]) && !loanAppMappings.ContainsKey(data[2]))
                    loanAppMappings.Add(data[2], data);

                if (!string.IsNullOrEmpty(data[0]) && !properties.ContainsKey(data[0]))
                    properties.Add(data[0], data);
            }
        }

        private static string[] userCustomProperties = new string[]
		{
			"Transaction Type", "Timeframe for transacting", "Property Value/Price",
			"Relationship to the property", "How did you hear about us",
			"Receive a free home valuation report", "Notify me of low interest rates",
			"Credit Rating", "MartialStatus", "Ownership", "EmployerName", "EmployerAddress", "EmployerCity",
			"EmployerState", "EmployerZip", "EmployerPhone", "EmployerTitle", "MonthsWithEmloyer",
		};

        /// <summary>
        /// add the site to chain of SITE_ID's COPY2 & MOVE2 actions of NEW LEAD RULES
        /// </summary>
        /// <param name="site_id"></param>
        private void NewLeadRules_SiteId_List_Add(Guid site_id)
        {
            this.NewLeadRules_SiteId_List.Add(site_id);
        }
        /// <summary>
        /// is site_id already exists in the list of COPY2 & MOVE2 actions of NEW LEAD RULES
        /// </summary>
        /// <param name="site_id"></param>
        /// <returns></returns>
        private bool NewLeadRules_SiteId_List_Exists(Guid site_id)
        {
            return this.NewLeadRules_SiteId_List.Contains(site_id);
        }

        private bool NewLeadRules_SiteId_List_IsCycle(Guid site_id)
        {
            bool b = this.NewLeadRules_SiteId_List.Contains(site_id);
            if (b == false)
                return false;

            NewLeadRules_SiteId_List_IsCycle_SendNotificationMessage(site_id);
            return true;
        }
        /// <summary>
        /// Email about the cycle
        /// </summary>
        private void NewLeadRules_SiteId_List_IsCycle_SendNotificationMessage(Guid site_id)
        {
            try
            {
                System.Net.Mail.SmtpClient client = SMTP.Smtp_ClientCreaCRM();
                //client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.Subject = "New-Lead-Rules: recursive move-copy action is detected";

                StringBuilder sb = new StringBuilder("New-Lead-Rules: recursive move-copy action is detected!\n");
                sb.AppendFormat("Site {0} is already exists in the chain of move or copy actions\n\n", site_id);
                foreach (object o in this.NewLeadRules_SiteId_List)
                    sb.AppendFormat("{0}\n", o.ToString());
                sb.AppendFormat("\nUSER_ID: {0}\nUSER_ID_FROM: {1}\nSITE_ID_FROM: {2}\n", this.USER_ID, this.USER_ID_FROM, this.SITE_ID_FROM);
                msg.Body = sb.ToString();

                msg.Priority = System.Net.Mail.MailPriority.High;
                msg.From = new System.Net.Mail.MailAddress("info@encompasscrm.com");
                msg.To.Add("yaroslav@arginteractive.com");
                msg.To.Add("jaret@arginteractive.com");

                //client.Send(msg);
                SmtpAPIWrapper wrapper = SMTP.SmtpClientCreaCRM();
                wrapper.Send(msg);
            }
            catch { }
        }



        /// <summary>
        /// chain of SITE_ID's COPY2 & MOVE2 actions of NEW LEAD RULES
        /// </summary>
        private ArrayList NewLeadRules_SiteId_List = new ArrayList();

        private Guid _USER_ID;
        private int _IS_MOVED;
        private string _txtEmail;
        private string _txtFirstName;
        private string _txtLastName;
        private string _txtPhone;
        private string _txtCellPhone;
        private string _txtState;
        private string _txtCity;
        private string _intZip;
        private string _txtPropertyAdress;
        private string _MORTGAGE_SPECIALIST;
        private DateTime _REGISTERED_DATE;
        private DateTime _AssignedDate = DateTime.MinValue;

        private int _LOGINS_COUNT;
        private Guid _SITE_ID; private string _SITE_NAME = null;
        private Guid _AGENT_ID;
        private string _txtMiddleName;

        private SQLDataAccess da = new SQLDataAccess();

        private Guid baseBorrowerId = Guid.Empty;
        private Users baseBorrower = null;

        private Search filledFullAppForm = null;

        private int _emailStatus ; // { get; set; }

        private string changerName = string.Empty;
        public string ChangerName
        {
            get { return changerName; }
            set { changerName = value; }
        }

        private double intRate = 0;
        public virtual double IntRate
        {
            get { return intRate; }
            set { this.RegisterFieldModification("IntRate", value, false); intRate = value; }
        }

        private DateTime? noteDueDate = null;
        public virtual DateTime? NoteDueDate
        {
            get { return noteDueDate; }
            set { this.RegisterFieldModification("NoteDueDate", value); noteDueDate = value; }
        }

        public int _intUserType = 2;
        public virtual int intUserType
        {
            get { return _intUserType; }
            set { this.RegisterFieldModification("intUserType", value); _intUserType = value; }
        }

        public int _IS_ACTIVE = 1;
        public virtual int IS_ACTIVE
        {
            get { return _IS_ACTIVE; }
            set { this.RegisterFieldModification("IS_ACTIVE", value); _IS_ACTIVE = value; }
        }


        private string _txtPwd = string.Empty;
        public virtual string txtPwd
        {
            get { return _txtPwd; }
            set { this.RegisterFieldModification("txtPwd", value); _txtPwd = value; }
        }


        private string nextStep;
        public virtual string NextStep
        {
            get { return GetValue<string>(nextStep); }
            set { this.RegisterFieldModification("NextStep", value); nextStep = value; }
        }

        private string status;
        public virtual string Status
        {
            get { return GetValue<string>(status); }
            set { this.RegisterFieldModification("Status", value); status = value; }
        }

        private DateTime? newActionDate;
        public virtual DateTime? NewActionDate
        {
            get { return newActionDate; }
            set { this.RegisterFieldModification("NewActionDate", value); newActionDate = value; }
        }

        private string loanType;
        public virtual string LoanType
        {
            get { return GetValue<string>(loanType); }
            set { this.RegisterFieldModification("LoanType", value); loanType = value; }
        }

        private string loanTypeOther;
        public virtual string LoanTypeOther
        {
            get { return GetValue<string>(loanTypeOther); }
            set { this.RegisterFieldModification("LoanTypeOther", value); loanTypeOther = value; }
        }

        private decimal loanAmount;
        public virtual decimal LoanAmount
        {
            get { return GetValue<decimal>(loanAmount); }
            set { this.RegisterFieldModification("LoanAmount", value); loanAmount = value; }
        }

        public string rememberLeadSource = "";
        private string leadSource;
        public virtual string LeadSource
        {
            get { return leadSource; }
            set { this.RegisterFieldModification("LeadSource", value); leadSource = value; }
        }

        private string formName = string.Empty;
        public virtual string FormName
        {
            get { return formName; }
            set { formName = value; }
        }

        //private string notes = string.Empty;
        //public virtual string Notes
        //{
        //   get { return notes; }
        //   set { this.RegisterFieldModification("Notes", value); notes = value; }
        //}

        private decimal revenue = (decimal)0;
        public virtual decimal Revenue
        {
            get { return revenue; }
            set { this.RegisterFieldModification("Revenue", value); revenue = value; }
        }

        private int probability = 0;
        public virtual int Probability
        {
            get { return probability; }
            set { this.RegisterFieldModification("Probability", value); probability = value; }
        }

        private DateTime? closingDate = null;
        public virtual DateTime? ClosingDate
        {
            get { return closingDate; }
            set { this.RegisterFieldModification("ClosingDate", value); closingDate = value; }
        }

        private DateTime? rateLockExpiration = null;
        public virtual DateTime? RateLockExpiration
        {
            get { return rateLockExpiration; }
            set { this.RegisterFieldModification("RateLockExpiration", value); rateLockExpiration = value; }
        }

        private DateTime? submissionDate = null;
        public virtual DateTime? SubmissionDate
        {
            get { return submissionDate; }
            set { this.RegisterFieldModification("SubmissionDate", value); submissionDate = value; }
        }

        private string workPhone = string.Empty;
        public virtual string WorkPhone
        {
            get { return GetValue<string>(workPhone); }
            set { this.RegisterFieldModification("WorkPhone", value); workPhone = value; }
        }

        private string fax = string.Empty;
        public virtual string Fax
        {
            get { return fax; }
            set { this.RegisterFieldModification("Fax", value); fax = value; }
        }

        private string bestTimeToCall = string.Empty;
        public virtual string BestTimeToCall
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower.bestTimeToCall : this.bestTimeToCall); }
            set { this.RegisterFieldModification("BestTimeToCall", value); bestTimeToCall = value; if (this.baseBorrower != null) this.baseBorrower.BestTimeToCall = value; }
        }

        private string borrowerAddress = string.Empty;
        public virtual string BorrowerAddress
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower.borrowerAddress : borrowerAddress); }
            set { this.RegisterFieldModification("BorrowerAddress", value); borrowerAddress = value; if (this.baseBorrower != null) this.baseBorrower.BorrowerAddress = value; }
        }

        private string borrowerCity = string.Empty;
        public virtual string BorrowerCity
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower.borrowerCity : borrowerCity); }
            set { this.RegisterFieldModification("BorrowerCity", value); borrowerCity = value; if (this.baseBorrower != null) this.baseBorrower.BorrowerCity = value; }
        }

        private string borrowerState = string.Empty;
        public virtual string BorrowerState
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower.borrowerState : borrowerState); }
            set { this.RegisterFieldModification("BorrowerState", value); borrowerState = value; if (this.baseBorrower != null) this.baseBorrower.BorrowerState = value; }
        }

        private string borrowerZip = string.Empty;
        public virtual string BorrowerZip
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower.borrowerZip : borrowerZip); }
            set { this.RegisterFieldModification("BorrowerZip", value); borrowerZip = value; if (this.baseBorrower != null) this.baseBorrower.BorrowerZip = value; }
        }

        private string coFirstName = string.Empty;
        public virtual string CoFirstName
        {
            get { return GetValue<string>(coFirstName); }
            set { this.RegisterFieldModification("CoFirstName", value); coFirstName = value; }
        }

        private string coLastName = string.Empty;
        public virtual string CoLastName
        {
            get { return GetValue<string>(coLastName); }
            set { this.RegisterFieldModification("CoLastName", value); coLastName = value; }
        }

        private string coEmail = string.Empty;
        public virtual string CoEmail
        {
            get { return GetValue<string>(coEmail); }
            set { this.RegisterFieldModification("CoEmail", value); coEmail = value; }
        }

        private string coPhone = string.Empty;
        public virtual string CoPhone
        {
            get { return GetValue<string>(coPhone); }
            set { this.RegisterFieldModification("CoPhone", value); coPhone = value; }
        }

        private string coWorkPhone = string.Empty;
        public virtual string CoWorkPhone
        {
            get { return GetValue<string>(coWorkPhone); }
            set { this.RegisterFieldModification("CoWorkPhone", value); coWorkPhone = value; }
        }

        private string coCellPhone = string.Empty;
        public virtual string CoCellPhone
        {
            get { return GetValue<string>(coCellPhone); }
            set { this.RegisterFieldModification("CoCellPhone", value); coCellPhone = value; }
        }

        private string coFax = string.Empty;
        public virtual string CoFax
        {
            get { return GetValue<string>(coFax); }
            set { this.RegisterFieldModification("CoFax", value); coFax = value; }
        }

        private string coBestTimeToCall = string.Empty;
        public virtual string CoBestTimeToCall
        {
            get { return GetValue<string>(coBestTimeToCall); }
            set { this.RegisterFieldModification("CoBestTimeToCall", value); coBestTimeToCall = value; }
        }

        private string coAddress = string.Empty;
        public virtual string CoAddress
        {
            get { return GetValue<string>(coAddress); }
            set { this.RegisterFieldModification("CoAddress", value); coAddress = value; }
        }

        private string coCity = string.Empty;
        public virtual string CoCity
        {
            get { return GetValue<string>(coCity); }
            set { this.RegisterFieldModification("CoCity", value); coCity = value; }
        }

        private string coState = string.Empty;
        public virtual string CoState
        {
            get { return GetValue<string>(coState); }
            set { this.RegisterFieldModification("CoState", value); coState = value; }
        }

        private string coZip = string.Empty;
        public virtual string CoZip
        {
            get { return GetValue<string>(coZip); }
            set { this.RegisterFieldModification("CoZip", value); coZip = value; }
        }



        private string loanProgram = string.Empty;
        public virtual string LoanProgram
        {
            get { return GetValue<string>(loanProgram); }
            set { this.RegisterFieldModification("LoanProgram", value); loanProgram = value; }
        }

        private string propertyType = string.Empty;
        public virtual string PropertyType
        {
            get { return GetValue<string>(propertyType); }
            set { this.RegisterFieldModification("PropertyType", value); propertyType = value; }
        }

        private string leadType = string.Empty;
        public virtual string LeadType
        {
            get { return GetValue<string>(leadType); }
            set { this.RegisterFieldModification("LeadType", value); leadType = value; }
        }

        private decimal downPayment = 0;
        public virtual decimal DownPayment
        {
            get { return GetValue<decimal>(downPayment); }
            set { this.RegisterFieldModification("DownPayment", value); downPayment = value; }
        }

        private string propertyWillBe = string.Empty;
        public virtual string PropertyWillBe
        {
            get { return GetValue<string>(propertyWillBe); }
            set { this.RegisterFieldModification("PropertyWillBe", value); propertyWillBe = value; }
        }

        private bool coBorrowerApply = false;
        public virtual bool CoBorrowerApply
        {
            get { return GetValue<bool>(coBorrowerApply); }
            set { this.RegisterFieldModification("CoBorrowerApply", value); coBorrowerApply = value; }
        }

        //Porperties
        public virtual bool IS_MOVED
        {
            get { return _IS_MOVED > 0; }
            set
            {
                this.RegisterFieldModification("IS_MOVED", value);
                _IS_MOVED = value ? 1 : 0;
            }
        }
        public Guid USER_ID
        {
            get { return _USER_ID; }
            set
            {
                _USER_ID = value;
            }
        }
        public virtual int emailStatus
        {
            get { return GetValue<int>(_emailStatus); }
            set
            {                
                _emailStatus = value; 
            }
        }
        public virtual string txtFirstName
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower._txtFirstName : _txtFirstName); }
            set
            {
                this.RegisterFieldModification("txtFirstName", value);
                _txtFirstName = value; if (this.baseBorrower != null) this.baseBorrower.txtFirstName = value;
            }
        }

        public virtual string txtLastName
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower._txtLastName : _txtLastName); }
            set
            {
                this.RegisterFieldModification("txtLastName", value);
                _txtLastName = value; if (this.baseBorrower != null) this.baseBorrower.txtLastName = value;
            }
        }
        public virtual string txtEmail
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower._txtEmail : _txtEmail); }
            set
            {
                this.RegisterFieldModification("txtEmail", value);
                _txtEmail = value; if (this.baseBorrower != null) this.baseBorrower.txtEmail = value;
            }
        }

        public virtual DateTime REGISTERED_DATE
        {
            get { return _REGISTERED_DATE; }
            set { this.RegisterFieldModification("REGISTERED_DATE", value); _REGISTERED_DATE = value; }
        }
        public bool LastChangeDateIsUpdate = true;//from ImportLeads.aspx
        public bool ApplyNewLeadRules = true;//from ImportLeads.aspx
        public bool SendNotificationMssg = false;//from ImportLeads.aspx

        DateTime _LastChangeDate = DateTime.MinValue;
        public virtual DateTime LastChangeDate
        {
            get { return _LastChangeDate; }
            set { _LastChangeDate = value; }
        }

        public virtual DateTime AssignedDate
        {
            get { return _AssignedDate; }
            set { this.RegisterFieldModification("AssignedDate", value); _AssignedDate = value; }
        }

        public virtual string txtPhone
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower._txtPhone : _txtPhone); }
            set
            {
                this.RegisterFieldModification("txtPhone", value);
                _txtPhone = value; if (this.baseBorrower != null) this.baseBorrower.txtPhone = value;
            }
        }
        public virtual string txtCellPhone
        {
            get { return GetValue<string>(this.baseBorrower != null ? this.baseBorrower._txtCellPhone : _txtCellPhone); }
            set
            {
                this.RegisterFieldModification("txtCellPhone", value);
                _txtCellPhone = value; if (this.baseBorrower != null) this.baseBorrower.txtCellPhone = value;
            }
        }
        public virtual string txtState
        {
            get { return GetValue<string>(_txtState); }
            set
            {
                this.RegisterFieldModification("txtState", value);
                _txtState = value;
            }
        }

        public virtual string txtMiddleName
        {
            get
            {
                return GetValue<string>(_txtMiddleName);
                //return mtxtMiddleName;	
            }
            set
            {
                this.RegisterFieldModification("txtMiddleName", value);
                _txtMiddleName = value;
            }
        }

        public virtual string txtCity
        {
            get { return GetValue<string>(_txtCity); }
            set
            {
                this.RegisterFieldModification("txtCity", value);
                _txtCity = value;
            }
        }
        public virtual string intZip
        {
            get { return GetValue<string>(_intZip); }
            set
            {
                this.RegisterFieldModification("intZip", value);
                _intZip = value;
            }
        }
        public virtual string txtPropertyAdress
        {
            get { return GetValue<string>(_txtPropertyAdress); }
            set
            {
                this.RegisterFieldModification("txtPropertyAdress", value);
                _txtPropertyAdress = value;
            }
        }
        //Jan 05, 2010 (for Maverick): 
        string _txtCompany = string.Empty;
        public string txtCompany
        {
            get { return _txtCompany; }
            set { this.RegisterFieldModification("txtCompany", value); _txtCompany = value; }
        }

        public string txtMortgageSpecialist
        {
            get { return _MORTGAGE_SPECIALIST; }
        }
        public virtual int LOGINS_COUNT
        {
            get { return _LOGINS_COUNT; }
            set
            {
                _LOGINS_COUNT = value;
            }
        }

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
        }


        public string LendingTreeID = "";//LoanApplicationID
        /// <summary>
        /// Creates empty object
        /// </summary>

        public Users()
        {
            this.isNew = true;
            _USER_ID = Guid.NewGuid();
            _txtEmail = "";
            _txtFirstName = "";
            _txtLastName = "";
            _txtPhone = "";
            _txtCellPhone = "";
            _txtState = "";
            _txtCity = "";
            _intZip = "";
            _txtPropertyAdress = "";
            _REGISTERED_DATE = _LastChangeDate = DateTime.Now;

            _LOGINS_COUNT = 0;
            intUserType = 2;
            IS_MOVED = false;
            _MORTGAGE_SPECIALIST = String.Empty;
            _txtCompany = String.Empty;

            _SITE_ID = Guid.Empty; SITE_NAME = ""; _SITE_ID_FROM = Guid.Empty;
            _AGENT_ID = Guid.Empty;

            this.newActionDate = null;
            this.status = LeadStatus.New;
            this.nextStep = LeadNextStep.None;

            this.leadSource = CMSWeb.Models.Generic.LeadSource.Other;
            this.loanAmount = 0;
            this.loanType = LeadLoanType.Other;

            if (System.Web.HttpContext.Current != null)
            {
                this["Site URL"] = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                try
                {
                    this["URL"] = System.Web.HttpContext.Current.Request.ServerVariables["URL"];
                }
                catch { }
            }
        }

        public Users(Search fullAppForm) : this()
        {
            this.filledFullAppForm = fullAppForm;
            if (fullAppForm != null)
            {
                this.LeadSource = CMSWeb.Models.Generic.LeadSource.Website_FullApp;
                this.FormName = "FullApp";
                this.txtEmail = fullAppForm.Params["APP_EMAIL"].StringValue = fullAppForm.txtEmail;
                this.txtPwd = (fullAppForm.txtPwd != null ? fullAppForm.txtPwd : string.Empty);
            }

            if (this.LastChangeDate.Equals(new DateTime(1900, 1, 1))) this.LastChangeDate = this.REGISTERED_DATE;
        }

                public Users(IDataReader dataReader)
        {
            this.isNew = false;
            foreach (DataRow row in dataReader.GetSchemaTable().Rows)
            {
                string propName = (string)row["ColumnName"];

                if (dataReader[propName] != DBNull.Value)
                {
                    //txtMiddleName
                    FieldInfo field = this.GetType().GetField(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);

                    if (null == field)
                        field = this.GetType().GetField("_" + propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);

                    if (field != null) field.SetValue(this, dataReader[propName]);
                }
            }

            if (!this.baseBorrowerId.Equals(Guid.Empty) && !this.baseBorrowerId.Equals(this.USER_ID)) this.baseBorrower = da.GetUser(this.baseBorrowerId);
            if (this.IsFullApp) this.filledFullAppForm = da.GetUserFilledAppForm(this._USER_ID);

            if (this.LastChangeDate.Equals(new DateTime(1900, 1, 1))) this.LastChangeDate = this.REGISTERED_DATE;
        }

        /// <summary>
        /// Creates user from e-mail
        /// </summary>
        /// <param name="_txtEmail"></param>
        public Users(Guid __USER_ID,
            string __txtEmail,
            string __txtFirstName,
            string __txtLastName,
            string __txtPhone,
            string __txtState,
            string __txtCity,
            string __txtZip,
            DateTime __REGISTERED_DATE,
            int __LOGINS_COUNT,
            int __intUserType,
            bool __IS_MOVED,
            string __txtMortgageSpecialist,
            Guid siteId,
            Guid agentId,
            DateTime? newActionDate,
            string status,
            string nextStep,
            string leadSource,
            string loanType,
            string loanTypeOther,
            decimal loanAmount,
            string notes,
            decimal revenue,
            int probability,
            DateTime? closingDate,
            DateTime? rateLockExpiration,
            DateTime? submissionDate
            )
            : this()
        {
            this.isNew = false;
            _USER_ID = __USER_ID;
            _txtEmail = __txtEmail;
            _txtFirstName = __txtFirstName;
            _txtLastName = __txtLastName;
            _txtPhone = __txtPhone;
            _txtState = __txtState;
            _txtCity = __txtCity;
            _intZip = __txtZip;
            _REGISTERED_DATE = _LastChangeDate = __REGISTERED_DATE;
            _LOGINS_COUNT = __LOGINS_COUNT;
            intUserType = __intUserType;
            IS_MOVED = __IS_MOVED;
            _MORTGAGE_SPECIALIST = __txtMortgageSpecialist;

            _SITE_ID = siteId; _SITE_ID_FROM = Guid.Empty;
            _AGENT_ID = agentId;

            this.newActionDate = newActionDate;
            this.status = status;
            this.nextStep = nextStep;

            this.leadSource = leadSource;
            this.loanAmount = loanAmount;
            this.loanType = loanType;
            this.loanTypeOther = loanTypeOther;
            //this.notes = (notes != null ? notes : string.Empty);

            this.revenue = revenue;
            this.probability = probability;
            this.closingDate = closingDate;
            this.rateLockExpiration = rateLockExpiration;
            this.submissionDate = submissionDate;

            if (this.IsFullApp) this.filledFullAppForm = da.GetUserFilledAppForm(this._USER_ID);
        }

        private bool IsFullApp
        {
            get
            {
                return (this.leadSource == "Full App"
                    || this.leadSource == CMSWeb.Models.Generic.LeadSource.Website_FullApp
                    || this.rememberLeadSource == CMSWeb.Models.Generic.LeadSource.Website_FullApp
                    || this.rememberLeadSource == "Full App"
              );
            }
        }

        public System.Guid SITE_ID
        {
            get
            {
                /*Begin Template Expansion{D5B2A73C-2A02-4E54-BB8D-ACA26D64EEC7}*/
                return _SITE_ID;
                /*End Template Expansion{D5B2A73C-2A02-4E54-BB8D-ACA26D64EEC7}*/
            }
            set
            {
                /*Begin Template Expansion{14949471-0B07-4DD4-BD7F-57E67BCE809D}*/
                _SITE_ID = value;
                /*End Template Expansion{14949471-0B07-4DD4-BD7F-57E67BCE809D}*/
            }
        }

        private Guid _SITE_ID_FROM = Guid.Empty;
        public System.Guid SITE_ID_FROM
        {
            get { return _SITE_ID_FROM; }
            set { _SITE_ID_FROM = value; }
        }
        private Guid _USER_ID_FROM = Guid.Empty;
        public System.Guid USER_ID_FROM
        {
            get { return _USER_ID_FROM; }
            set { _USER_ID_FROM = value; }
        }

        public string SITE_NAME
        {
            get { return _SITE_NAME; }
            set { _SITE_NAME = value; }
        }

        private Consultants agent = null;
        public Consultants Agent { get { return this.agent; } }

        public virtual string MortgageSpecialist
        {
            get
            {
                if (this.agent == null || this.agent.CONSULTANT_ID != this._AGENT_ID) this.agent = da.GetConsultant(this._AGENT_ID);
                return (this.agent != null ? this.agent.CONSULTANT_FULLNAME : string.Empty);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.agent = da.GetConsultantByFullName(this.SITE_ID, value);
                    this.AGENT_ID = (this.agent != null ? this.agent.CONSULTANT_ID : Guid.Empty);
                }
            }
        }

        public virtual Guid AGENT_ID
        {
            get { return _AGENT_ID; }
            set
            {
                this.RegisterFieldModification("AGENT_ID", value);
                _AGENT_ID = value;
                this.agent = da.GetConsultant(value);
            }
        }

        
        private IDictionary<string, FieldMofification> fieldModifications = new SortedList<string, FieldMofification>();

        private void RegisterFieldModification(object newValue)
        {
            string propertyName = Regex.Replace(new StackFrame(1).GetMethod().Name, "^set_", "");
            //System.Web.HttpContext.Current.Response.Write(propertyName + "<br>");
            this.RegisterFieldModification(propertyName, newValue, false);
        }

        private void RegisterFieldModification(string propertyName, object newValue)
        {
            this.RegisterFieldModification(propertyName, newValue, false);
        }

        private void RegisterFieldModification(string propertyName, object newValue, bool isAdditional)
        {
            if (!fieldModifications.ContainsKey(propertyName))
            {
                PropertyInfo prop = isAdditional
                    ? this.GetType().GetProperty("Item", BindingFlags.Instance | BindingFlags.Public,
                        null, typeof(object), new Type[] { typeof(string) }, null)
                    : this.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

                //System.Web.HttpContext.Current.Response.Write(string.Format("{0} [{1}]<br>", propertyName, prop != null));

                if (prop != null)
                {
                    object oldValue = null;

                    if (!isNew)
                    {
                        if (isAdditional)
                            oldValue = prop.GetValue(this, new object[] { propertyName });
                        else
                            oldValue = prop.GetValue(this, null);
                    }
                    else oldValue = null;

                    this.fieldModifications.Add(propertyName,
                        new FieldMofification(propertyName, oldValue, newValue, isAdditional));
                }
            }
            else fieldModifications[propertyName].NewValue = newValue.ToString();

            if (fieldModifications.ContainsKey(propertyName) && !fieldModifications[propertyName].IsChanged)
                fieldModifications.Remove(propertyName);
        }
        
        private T GetValue<T>(T value)
        {
            string propertyName = Regex.Replace(new StackFrame(1).GetMethod().Name, "^get_", "");
            return this.GetValue(propertyName, value);
        }

        private T GetValue<T>(string propertyName, T value)
        {
            if (this.filledFullAppForm != null)
            {
                object newVal = null;

                if (properties.ContainsKey(propertyName) && properties[propertyName].Length > 2
                            && !string.IsNullOrEmpty(properties[propertyName][2])
                            && this.filledFullAppForm.Params.ParamExists(properties[propertyName][2]))
                    newVal = this.filledFullAppForm.Params[properties[propertyName][2]].Value;
                else if (this.filledFullAppForm.Params.ParamExists(propertyName))
                    newVal = this.filledFullAppForm.Params[propertyName].Value;

                if (newVal != null)
                {
                    if (typeof(T).Equals(typeof(object))) return (T)newVal;

                    MethodInfo convertTo = typeof(Convert).GetMethod("To" + typeof(T).Name,
                        BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase, null,
                        new Type[] { newVal.GetType() }, null);

                    try
                    {
                        if (convertTo != null) return (T)convertTo.Invoke(null, new object[] { newVal });
                    }
                    catch
                    {
                    }
                }
            }

            return value;
        }

        
        public virtual object this[string propertyName]
        {
            get { return this[propertyName, propertyName]; }
            set { this[propertyName, propertyName] = value; }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public virtual object this[string propertyName, string altName]
        {
            get
            {
                if (string.IsNullOrEmpty(altName)) altName = propertyName;
                if (loanAppMappings.ContainsKey(propertyName)) propertyName = loanAppMappings[propertyName][0];
                else propertyName = altName;

                if (this.IsUserProperty(propertyName) && this.baseBorrower != null)
                {
                    return this.baseBorrower[propertyName];
                }
                else
                {
                    PropertyInfo prop = this.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                    if (prop != null) return prop.GetValue(this, null);
                    else
                    {
                        SQLDataAccess da;
                        if (!string.IsNullOrEmpty(this.ConnectionString))
                            da = new SQLDataAccess(this.ConnectionString);
                        else
                            da = new SQLDataAccess();

                        string value = v(da.GetUserDetailsParam(this.USER_ID, propertyName));
                        object newVal = GetValue<object>(propertyName, value);
                        return ((newVal != null) ? newVal : string.Empty);
                    }
                }
            }

            set
            {
                if (string.IsNullOrEmpty(altName)) altName = propertyName;
                if (loanAppMappings.ContainsKey(propertyName)) propertyName = loanAppMappings[propertyName][0];
                else propertyName = altName;

                PropertyInfo prop = this.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

                if (prop != null)
                {
                    if (!prop.PropertyType.IsAssignableFrom(value.GetType()))
                    {
                        if (value.GetType().Equals(typeof(string)))
                        {
                            object newValue = null;
                            string strValue = v(((string)value).Trim());

                            //if (is_attack(strValue))
                            //  return;

                            switch (prop.PropertyType.Name)
                            {
                                case "Boolean":
                                    newValue = (strValue.ToLower() == "true" || strValue.ToLower() == "yes" || strValue.ToLower() == "1");
                                    break;

                                case "Decimal":
                                    decimal decVal;
                                    if (decimal.TryParse(strValue.Trim().Split(' ', '-', '\t')[0], NumberStyles.Currency, null, out decVal))
                                        newValue = decVal;

                                    break;

                                case "Byte":
                                case "Int16":
                                case "Int32":
                                case "Single":
                                case "Double":
                                case "DateTime":
                                    MethodInfo parseMethod = prop.PropertyType.GetMethod("Parse", new Type[] { typeof(string) });

                                    if (parseMethod != null)
                                    {
                                        try
                                        {
                                            newValue = parseMethod.Invoke(null, new object[] { strValue });
                                        }
                                        catch (Exception)
                                        {
                                            try
                                            {
                                                if (prop.PropertyType.Name == "DateTime") newValue = DateTime.ParseExact(strValue, "yyyyMMdd", null);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                    }
                                    break;

                                default:
                                    if (prop.PropertyType == typeof(DateTime?))
                                    {
                                        DateTime dt;
                                        Match m = Regex.Match(strValue, @"(\d)/0(\d\d)/(\d)");
                                        if (m.Success) strValue = string.Format("{0}/{1}/200{2}", m.Groups[1], m.Groups[2], m.Groups[3]);

                                        if (DateTime.TryParse(strValue, out dt)) newValue = new DateTime?(dt);
                                        else if (DateTime.TryParseExact(strValue, "yyyyMMdd", null, DateTimeStyles.AllowWhiteSpaces, out dt))
                                            newValue = new DateTime?(dt);
                                        else newValue = null;
                                    }
                                    else newValue = value.ToString();
                                    break;
                            }

                            if (newValue != null) prop.SetValue(this, newValue, null);
                        }
                    }
                    else prop.SetValue(this, value, null);
                }
                else
                {
                    this.RegisterFieldModification(propertyName, value, true);

                    if (this.IsUserProperty(propertyName) && this.baseBorrower != null)
                    {
                        this.baseBorrower[propertyName] = value;
                    }
                }
            }
        }

        public bool HasParam(string paramName)
        {
            if (this.GetType().GetProperty(paramName, BindingFlags.Instance | BindingFlags.Public) != null) return true;

            return properties.ContainsKey(paramName);
        }

                public static bool is_exist(Guid USER_ID)
        {
            try
            {
                string dummy;
                int i = (int)(new Connections().ExecuteScalar("select count(*) from [dbo].[tblUserDetails]  WITH(NOLOCK) where USER_ID=@USER_ID ",
                    new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID) }, out dummy));
                return i == 1;
            }
            catch
            {
                return false;
            }
        }

        public static string v(string inp)
        {
            //return inp;
            if (string.IsNullOrEmpty(inp))
                return inp;

            string outp = inp;
            //<script src=http://a0v.org/x.js></script>            
            Regex regex_script = new Regex("<script.*</script>|<script.*/>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex_script.Matches(inp);
            foreach (Match match in matches)
            {
                string script = match.ToString();
                //to do: add log
                outp = outp.Replace(match.ToString(), "");
            }
            return outp;
        }
        public bool is_attack(string inp)
        {
            Regex regex_script = new Regex("<script.*</script>|<script.*/>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex_script.Matches(inp);
            return matches.Count > 0;
        }

        private bool IsUserProperty(string property)
        {
            return (Array.IndexOf<string>(userCustomProperties, property) >= 0);
        }

        public static string getFromNVC(System.Collections.Specialized.NameValueCollection nvc, string key)
        {
            try
            {
                return v(nvc.GetValues(key)[0].Trim());
            }
            catch
            {
                return "";
            }
        }


    }


    public class ToStringComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((string[])x)[1].CompareTo(((string[])y)[1].ToString());
        }

        public static IList<string[]> SortIList(IList<string[]> list)
        {
            ArrayList.Adapter((IList)list).Sort(new ToStringComparer());

            return list;
        }

        public static IList<string[]> UserAllFieldsSortedByFirst()
        {
            IList<string[]> allFields = new List<string[]>(Users.AllProperties);
            ArrayList.Adapter((IList)allFields).Sort(new ToStringComparer());

            return allFields; // SortIList(allFields);
        }

    }

    #region lead

    public static class LeadStatus
	{
		public static readonly string New = "New";
		public static readonly string Submitted = "Submitted";
		public static readonly string Active = "Active";
		public static readonly string Application = "Application";
		public static readonly string FoundProgram = "Found Program/Approval";
		//public static readonly string FoundProgramExt = "Found Program/Approval Extension To Sell Per KO's";
		public static readonly string Sold = "Sold Loan/Commitment";
		public static readonly string DisclosuresOutAppointment = "Disclosures Out - Appointment";
		public static readonly string DisclosuresOutFedex = "Disclosures Out - Fedex";
		public static readonly string AppraisalOrdered = "Appraisal Ordered";
		public static readonly string Approved = "Approved";
		public static readonly string Funded = "Funded";
		public static readonly string Returned = "Returned";
		public static readonly string Inactive = "Inactive";
		public static readonly string Bad = "Bad";
		public static readonly string Rescinded = "Rescinded/Cancelled/Denied From Processing";
		public static readonly string RequestForCredit = "Request For Credit (DNMF)";
		public static readonly string RequestToCancelLead = "Request To Cancel Lead";
		public static readonly string RequestToCancelApplication = "Request To Cancel Application";
		//public static readonly string Email = "Email";
		//public static readonly string Meeting = "Meeting";
		//public static readonly string FollowUp = "Follow-up";
		//public static readonly string Call = "Call";

		//public static readonly string Process = "Process";
		public static readonly string Closed = "Closed";
		//public static readonly string Lost = "Lost";

		public static IList<string> ListAll()
		{
			List<string> list = new List<string>();

			ClientSite site = (ClientSite)HttpContext.Current.Session["Site"];
			PageContent pc = new PageContent();
			string statuses = (site != null ? pc.GetParameter("LEAD_STATUSES", site.SITE_ID) : string.Empty);
			//string addStatuses = (site != null ? pc.GetParameter("LEAD_STATUSES_ADD", site.SITE_ID) : string.Empty);
            //LEAD_STATUSES_ADD is not used yet. Removing it. ys. 25 Oct 2007

			if (string.IsNullOrEmpty(statuses))
			{
				foreach (FieldInfo fi in typeof(LeadStatus).GetFields(BindingFlags.Public | BindingFlags.Static))
				{
					list.Add((string)fi.GetValue(null));
				}
			}
			else
			{
				list.AddRange(statuses.Split(new string[] { ";", ",", "|" }, StringSplitOptions.RemoveEmptyEntries));
			}

			/*if (!string.IsNullOrEmpty(addStatuses))
				list.AddRange(addStatuses.Split(new string[] { ";", ",", "|" }, StringSplitOptions.RemoveEmptyEntries));
            */
			return list;
		}
	}

    public static class LeadNextStep
    {
        /*
		public static readonly string None = "None";
		public static readonly string ContactLead = "Contact Lead";
		public static readonly string CompleteApplication = "Complete Application";
		public static readonly string Email = "Email";
		public static readonly string Meeting = "Meeting";
		public static readonly string FollowUp = "Follow-up";
		public static readonly string Call = "Call";
         */

        public const string None = "0";
        public const string ContactLead = "1";
        public const string CompleteApplication = "2";
        public const string Email = "3";
        public const string Meeting = "4";
        public const string FollowUp = "5";
        public const string Call = "6";
        public const string Marketing = "8";

        /*
        public static IList<string> ListAll() //0
        {
            List<string> list = new List<string>();
            foreach (FieldInfo fi in typeof(LeadNextStep).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                list.Add((string)fi.GetValue(null));
            }

            return list;
        }*/
        public static Dictionary<string, string> ListAll()
        {
            Dictionary<string, string> l = new Dictionary<string, string>();//<key, value>
            l.Add("None", LeadNextStep.None);
            l.Add("Contact Lead", LeadNextStep.ContactLead);
            l.Add("Complete Application", LeadNextStep.CompleteApplication);
            l.Add("Email", LeadNextStep.Email);
            l.Add("Meeting", LeadNextStep.Meeting);
            l.Add("Follow-up", LeadNextStep.FollowUp);
            l.Add("Call", LeadNextStep.Call);
            return l;
        }
        public static Dictionary<string, string> ListAll(Guid SITE_ID)
        {
            if (SITE_ID.Equals(new Guid("0C2E7361-753B-46DC-B6B9-3C17519528DD")))
            {
                //0C2E7361-753B-46DC-B6B9-3C17519528DD	snmcusa	g7qwq	snational
                Dictionary<string, string> l = new Dictionary<string, string>();//<key, value>
                l.Add("", LeadNextStep.None);
                l.Add("Contact", "1");
                l.Add("Take Application", "2");
                l.Add("Didn’t qualify – credit", "3");
                l.Add("Didn’t qualify- Income", "4");
                l.Add("Not interested", "5");
                l.Add("Close loan", "6");
                l.Add("Post close marketing", "7");

                return l;
            }
            else if (SQLDataAccess.IsDitech(SITE_ID))
            {
                Dictionary<string, string> l = new Dictionary<string, string>();
                l.Add("", "");
                l.Add("None", LeadNextStep.None);
                l.Add("Call", LeadNextStep.Call);
                l.Add("Complete Application", LeadNextStep.CompleteApplication);
                l.Add("Email", LeadNextStep.Email);
                l.Add("Marketing", LeadNextStep.Marketing);

                return l;
            }
            else return ListAll();
        }
        public static string Name4Label(string s)
        {
            switch (s)
            {
                case LeadNextStep.None: return "None";
                case LeadNextStep.ContactLead: return "Contact Lead";
                case LeadNextStep.CompleteApplication: return "Complete Application";
                case LeadNextStep.Email: return "Email";
                case LeadNextStep.Meeting: return "Meeting";
                case LeadNextStep.FollowUp: return "Follow-up";
                case LeadNextStep.Call: return "Call";
                case LeadNextStep.Marketing: return "Marketing";
                default: return s;
            }
        }
    }

    public static class LeadLoanType
    {
        // a lot was commented according to the CRM-1176    1

        public static readonly string Other = "Other";
        public static readonly string Purchase = "Purchase";
        public static readonly string Refinance = "Refinance";
        public static readonly string HomeEquity = "Home Equity";
        public static readonly string Construction = "Construction";
        public static readonly string Commercial = "Commercial";
        public static readonly string ReverseMortgage = "Reverse Mortgage";

        public static readonly string LTV = "LTV";
        public static readonly string ProposedTerm = "Proposed Term";
        public static readonly string ProposedRate = "Proposed Rate";

        // 2014-05-09
        public static readonly string WarehouseLOC = "Warehouse LOC";

        //these 4 for RMS, Sep 07, 2010:
        public static readonly string Conventional = "Conventional";
        public static readonly string FHA = "FHA";
        public static readonly string VA = "VA";
        public static readonly string USDA_RHS = "USDA-RHS";
        //for CDFI, Aug 12, 2008:
        public static readonly string TraditionalCommercialMultiFamily = "Traditional Commercial -Multi-Family";
        public static readonly string TraditionalCommercialMixedUse = "Traditional Commercial - Mixed Use";
        public static readonly string TraditionalCommercialMobileHomePark = "Traditional Commercial - Mobile Home Park";
        public static readonly string TraditionalCommercialRetail = "Traditional Commercial - Retail";
        public static readonly string TraditionalCommercialOffice = "Traditional Commercial - Office";
        public static readonly string TraditionalCommercialIndustrial = "Traditional Commercial - Industrial";
        public static readonly string TraditionalCommercialStorage = "Traditional Commercial - Storage";
        public static readonly string TraditionalCommercialOther = "Traditional Commercial – Other";
        public static readonly string NonTraditionalCommercialOwnerOccupied = "Non-Traditional Commercial - Owner Occupied";
        public static readonly string NonTraditionalCommercialGasStation = "Non-Traditional Commercial - Gas Station";
        public static readonly string NonTraditionalCommercialAutomotiveCarDealerships = "Non-Traditional Commercial - Automotive, Car Dealerships";
        public static readonly string NonTraditionalCommercialDryCleaners = "Non-Traditional Commercial - Dry Cleaners";
        public static readonly string NonTraditionalCommercialHotelMotel = "Non-Traditional Commercial - Hotel / Motel";
        public static readonly string NonTraditionalCommercialRestaurant = "Non-Traditional Commercial - Restaurant";
        public static readonly string NonTraditionalCommercialOther = "Non-Traditional Commercial - Other";
        public static readonly string HardMoneyCommercial = "Hard Money Commercial";
        public static readonly string ConstructionLoans = "Construction Loans";
        public static readonly string BridgeMezzanineFinancing = "Bridge / Mezzanine Financing";

        public static readonly string Land = "Land";
        public static readonly string HELOC = "HELOC";


        public static IList<string> ListAll()
        {
            List<string> list = new List<string>();
            foreach (FieldInfo fi in typeof(LeadLoanType).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                list.Add((string)fi.GetValue(null));
            }

            return list;
        }

        public static IList<string> List(Guid siteId)
        {
            if (SQLDataAccess.IsStearns(siteId))
            {
                List<string> list = new List<string>();
                list.Add(Purchase);
                list.Add(Refinance);
                list.Add(Conventional);
                list.Add(FHA);
                list.Add(VA);
                list.Add("USDA");
                list.Add(ReverseMortgage);
                return list;
            }
            else if (SQLDataAccess.IsNattyMac(siteId))
            {
                List<string> list = new List<string>();
                list.Add("Tier 1");
                list.Add("Tier 2");
                list.Add("Tier 3");
                list.Add("Tier 4");
                list.Add("Unknown");
                return list;
            }
            //else return ListAll();
            else
            {
                List<string> list = new List<string>();
                list.Add("");
                list.Add("Conventional");
                list.Add("FHA");
                list.Add("VA");
                list.Add("USDA-RHS");
                list.Add("Other");
                return list;
            }
        }
    }

    #endregion

    #region Search


    [Serializable]
    public class Search : IComparable
    {
        public readonly IList<KeyValuePair<string, string>> CustomProperties = new List<KeyValuePair<string, string>>();


        /// <summary>
        /// SALE transaction types
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public static string sqlSaleTrans = "('DEED', 'SALE', 'EXECUTION', 'SHERIFFS DEED', 'MASTER DEED', 'EASEMENT DEED', 'TREASURERS DEED')";
        /// <summary>
        /// Ratio between area value in Sq Ft and Acres
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public static string SqFtAcre = "43560";

        private Guid _SEARCH_LOG_ID;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid SEARCH_LOG_ID
        {
            get { return _SEARCH_LOG_ID; }
            set { _SEARCH_LOG_ID = value; }
        }

        private string _LIST_NO;
        public string LIST_NO
        {
            get { return _LIST_NO; }
            set { _LIST_NO = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int SOURCE_ID;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid FILE_ID;

        private int _HAVE_NO_IMAGES;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int HAVE_NO_IMAGES
        {
            get { return _HAVE_NO_IMAGES; }
            set { _HAVE_NO_IMAGES = value; }
        }



        private Guid _PROD_CAT_ID;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid PROD_CAT_ID
        {
            get { return _PROD_CAT_ID; }
            set { _PROD_CAT_ID = value; }
        }


        private Guid _USER_ID;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid USER_ID
        {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }

        private Guid _CREATOR_ID = Guid.Empty;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid CREATOR_ID
        {
            get { return _CREATOR_ID; }
            set { _CREATOR_ID = value; }
        }

        private string _txtEmail;
        /// <summary>
        /// For log-in
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string txtEmail
        {
            get { return _txtEmail; }
            set { _txtEmail = value; }
        }

        private string _txtPwd;
        /// <summary>
        /// For log-in
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string txtPwd
        {
            get { return _txtPwd; }
            set { _txtPwd = value; }
        }

        private DateTime _DATE_DUMP;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public DateTime DATE_DUMP
        {
            get { return _DATE_DUMP; }
            set { _DATE_DUMP = value; }
        }



        private Guid _SITE_ID;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid SITE_ID
        {
            get { return _SITE_ID; }
            set { _SITE_ID = value; }
        }

        private int _RECORDS_COUNT = 0;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int RECORDS_COUNT
        {
            get { return _RECORDS_COUNT; }
            set { _RECORDS_COUNT = value; }
        }

        private int _EXECUTION_TIME = 0;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int EXECUTION_TIME
        {
            get { return _EXECUTION_TIME; }
            set { _EXECUTION_TIME = value; }
        }

        private string _ERROR_DESCRIPTION = "";
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string ERROR_DESCRIPTION
        {
            get { return _ERROR_DESCRIPTION; }
            set { _ERROR_DESCRIPTION = value; }
        }



        private string _SEARCH_TYPE_NAME;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string SEARCH_TYPE_NAME
        {
            get
            {
                return _SEARCH_TYPE_NAME;
            }

            set
            {
                _SEARCH_TYPE_NAME = value;
            }
        }

        private string _SEARCH_DESCRIPTION;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string SEARCH_DESCRIPTION
        {
            get { return _SEARCH_DESCRIPTION; }
            set { _SEARCH_DESCRIPTION = value; }
        }


        private string _AGENT_NAME;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string AGENT_NAME
        {
            get { return _AGENT_NAME; }
            set { _AGENT_NAME = value; }
        }

        private Guid _AGENT_ID = Guid.Empty;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid AGENT_ID
        {
            get { return _AGENT_ID; }
            set { _AGENT_ID = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string OFFICE_NAME;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string OFFICE_ADDRESS;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public double LAT;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public double LONG;



        /// <summary>
        /// Use this param name to sort
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string sortParam = "";

        public Search()
        {
            Params = new SearchParamCollection();
            _SEARCH_LOG_ID = Guid.NewGuid();
        }

        public Search(string __SEARCH_TYPE_NAME)
        {
            Params = new SearchParamCollection();
            SEARCH_TYPE_NAME = __SEARCH_TYPE_NAME;
            _SEARCH_LOG_ID = Guid.NewGuid();
        }

        public SearchParamCollection Params;

        public PropertyImageCollection Images;
        /// <summary>
        /// set field Images (PropertyImageCollection) on the base of field Params.
        /// </summary>
        public void AddImages()
        {
            Images = new PropertyImageCollection();

            foreach (SearchParam _Param in Params)
            {
                if (_Param.SEARCH_PARAM_NAME == "IMAGES")
                {//_Param.ImageName
                    //_Param.ImageNo                    
                    Images.Add(new PropertyImage(null, _Param.ValueImageContentType, _Param.ImageName, _Param.ImageNo));
                    Images[Images.Count - 1].IMAGE_ID = _Param.IMAGE_ID;
                }

            }
        }

        public string PrepareAddress(string sAddress)
        {
            string[] aToRemove = { ".", " STREET", " ST", " CIRCLE", " CIR", " DRIVE", " DR", " AVENUE", " AVE", " ROAD", " RD" };
            string sValue = sAddress.ToUpper();

            for (int i = 0; i <= aToRemove.Length - 1; i++)
            {
                sValue = sValue.Replace(aToRemove[i], "");
            }

            sValue.Trim();
            return sValue;
        }

        protected string WrapSqlForPaging(string inputSQL, string SortParam, int pageSize, int pageCurrent, int rows_count)
        {
            return WrapSqlForPaging(inputSQL, SortParam, "DESC", pageSize, pageCurrent, rows_count);
        }
        protected string WrapSqlForPaging(string inputSQL, string SortParam, string sort_order, int pageSize, int pageCurrent, int rows_count)
        {

            string _ID = "PRODUCT_ID";
            string _table_alias = "t";

            if (SEARCH_TYPE_NAME == "FOR_SALE_BASIC" ||
                SEARCH_TYPE_NAME == "FOR_SALE_ADVANCED" ||
                SEARCH_TYPE_NAME == "FORECLOSURE_SEARCH" ||
                SEARCH_TYPE_NAME == "VIRTUALMAP_SEARCH")
            {
                _ID = "LIST_NO";
                _table_alias = "fs";
            }
            string rString;

            int last_ps = 0;

            if (pageSize * (pageCurrent) > rows_count)
                last_ps = pageSize - (pageSize * (pageCurrent) - rows_count);
            else
                last_ps = pageSize;

            //if (last_ps == 0)
            //  last_ps = pageSize;

            rString = "SELECT TOP 100 PERCENT * FROM (" +
                "SELECT TOP " + last_ps.ToString() + " * FROM " +
                "(SELECT TOP 100 PERCENT * ";
            if (SEARCH_TYPE_NAME != "FOR_SALE_BASIC" &&
                SEARCH_TYPE_NAME != "FOR_SALE_ADVANCED" &&
                SEARCH_TYPE_NAME != "FORECLOSURE_SEARCH" &&
                SEARCH_TYPE_NAME != "VIRTUALMAP_SEARCH")
                rString += ", propertyinfo.GetDeedOwner(" + _ID + ") as FULL_NAME, propertyinfo.f_GetPropertyAddress(" + _ID + ") AS PROPERTY_ADDRESS, propertyinfo.f_GetPropertyState(" + _ID + ") AS PROPERTY_STATE ";

            rString += " from (SELECT distinct TOP " + (pageSize * (pageCurrent)).ToString() + " with ties";

            rString += inputSQL;

            //ys			rString += " ORDER BY "+_table_alias+"."+SortParam+" DESC, "+_table_alias+"."+_ID+" DESC) as INT1 ) as int1_1 ORDER BY  "+SortParam+" ASC, "+_ID+" ASC) AS INT2 "+
            //				" ORDER BY "+SortParam+" DESC, "+_ID+" DESC";
            //            rString  += getOrderBy( _table_alias, SortParam, _ID );
            rString += getOrderBy(_table_alias, SortParam, _ID, sort_order);

            return rString;
        }
        //ys It's old version (for history):
        protected string getOrderBy(string _table_alias, string SortParam, string _ID)
        {
            return " ORDER BY " + _table_alias + "."
                + SortParam + " DESC, " + _table_alias + "."
                + _ID + " DESC) as INT1 ) as int1_1 ORDER BY  "
                + SortParam + " ASC, " + _ID + " ASC) AS INT2 " +
                " ORDER BY " + SortParam + " DESC, " + _ID + " DESC";
        }

        protected string getOrderBy(string _table_alias, string SortParam, string _ID, string desc_asc)
        {
            return " ORDER BY " + _table_alias + "."
                + SortParam + " " + desc_asc + ", "
                + _table_alias + "."
                + _ID + " DESC) as INT1 ) as int1_1 ORDER BY  "
                + SortParam + " " + invert_desc_asc(desc_asc) + ", "
                + _ID + " ASC) AS INT2 " +
                " ORDER BY "
                + SortParam + " " + invert_desc_asc(invert_desc_asc(desc_asc)) + ", "
                + _ID + " DESC";
        }
        public string invert_desc_asc(string desc_asc)
        {
            if (desc_asc.Equals("DESC", StringComparison.OrdinalIgnoreCase))
                return "ASC";
            if (desc_asc.Equals("ASC", StringComparison.OrdinalIgnoreCase))
                return "DESC";
            return "DESC";
        }



        /// <summary>
        /// Get text for WHERE statement for given param
        /// </summary>
        /// <param name="_CONDITION_STRING"></param>
        /// <param name="_SEARCH_PARAM_NAME"></param>
        /// <returns></returns>
        private string GetParamCondition(string _CONDITION_STRING, string _SEARCH_PARAM_NAME)
        {

            if (Params.ParamExists(_SEARCH_PARAM_NAME) && Params[_SEARCH_PARAM_NAME].StringValue.Length > 0)
                return string.Format(_CONDITION_STRING, Params[_SEARCH_PARAM_NAME].StringValue);
            else
                return "";
        }


        private string GetForSaleBasicSQL(
            )
        {
            string sSQL = " fs.[LAT], fs.[LONG], [ITEM_ID], [DATE_DUMP], [PROD_CAT_ID], [TYPE_NAME], [LIST_NO], [PROPERTY_CITY], [PROPERTY_STATE], [AGENT_FIRST_NAME], [AGENT_LAST_NAME], [LIST_PRICE], [NO_BEDROOMS], ISNULL([NO_TOTAL_BATHS],CAST([NO_FULL_BATHS] AS NUMERIC(8,2))) as [NO_FULL_BATHS], [PROP_TYPE], [AREA], [NO_ROOMS], [ACRE], [REMARKS], [HAVE_NO_IMAGES], [SQUARE_FEET], [STATUS], [LIST_DATE], [STATUS_DATE], [SOURCE_ID], [FILE_ID], [YEAR_BUILT], [NEIGHBORHOOD], [NEIGHBORHOOD_CLEAR], [SALE_PRICE], [STREET_NAME], [STREET_NO], [ZIP_CODE], [ASSESSMENTS], [OFFICE_NAME], [OFFICE_ADDRESS], dbo.GetForsaleImageID(LIST_NO) as IMAGE_ID, ISNULL (fs.AGENT_FIRST_NAME,'') + ' '+ISNULL (fs.AGENT_LAST_NAME,'') as LIST_AGENT_NAME, dbo.GetForsaleImageCount(LIST_NO) as IMAGE_COUNT FROM tblItems fs  with(nolock) " +
                " ";
            string sWhere = "";

            if (Params.ParamExists("LIST_NO") && Params["LIST_NO"].StringValue.Length > 0)
            {
                sWhere = "WHERE " + GetParamCondition(" fs.LIST_NO = '{0}' ", "LIST_NO");
                return sSQL + sWhere;
            }



            if ((
                Params.ParamExists("TRANSACTION_TYPE") &&
                Params["TRANSACTION_TYPE"].StringValue.Length > 0) ||
                (Params.ParamExists("LIST_NO") && Params["LIST_NO"].StringValue.Length > 0))
                sWhere = " where fs.STATUS IN ('ACT', 'NEW', 'A', 'PCG', 'AA', 'AB', 'AC', 'AH', 'AN', 'Active', 'Auction', 'REO') ";
            else
                sWhere = " where fs.STATUS IN ('ACT', 'NEW', 'A', 'PCG', 'AA', 'AB', 'AC', 'AH', 'AN', 'Active', 'ALLDEL') ";

            if (Params.ParamExists("PROPERTY_CITY") && Params["PROPERTY_CITY"].StringValue.Length > 0)
            {

                ///Process boston neighborhoods
                ///To test:
                ///     Simple:
                ///     http://localhost:14062/?p=SC_FOR_SALE_SEARCH_RESULTS&s=AAA&SLID=3b913f2b-7f93-41e6-a964-119ada6e2d96
                ///     Advanced:
                ///     http://localhost:14062/?p=SC_FOR_SALE_SEARCH_RESULTS&s=AAA&SLID=d3ce9c24-51bb-45b7-89dd-e7aff2da628c
                string propertyCity = Params["PROPERTY_CITY"].StringValue;
                string neighborhoods = "";
                propertyCity = propertyCity.Replace("'", "");

                string[] cityList = propertyCity.Split(',');
                int i = 0;
                while (i <= cityList.Length - 1)
                {
                    if (cityList[i].IndexOf("--") > -1)
                    {
                        if (neighborhoods.Length > 0)
                            neighborhoods += ", ";

                        neighborhoods += "'" + cityList[i].Replace("--", "").Trim() + "'";
                    }
                    i++;
                }

                if (neighborhoods.Length > 0)
                {
                    sWhere += " AND fs.NEIGHBORHOOD_CLEAR IN (" + neighborhoods + ")";
                    sWhere += " AND fs.PROPERTY_CITY IN ( 'Boston' )";
                }
                else
                {
                    string str = "";
                    if (Params["PROPERTY_CITY"].StringValue.Substring(0, 1) != "'")
                        str = "'";

                    sWhere += " AND fs.PROPERTY_CITY IN (" + str + Params["PROPERTY_CITY"].StringValue + str + ")";
                }
            }

            if (Params.ParamExists("NEIGHBORHOODS") && Params["NEIGHBORHOODS"].StringValue.Length > 0)
            {
                string str = "";
                if (Params["NEIGHBORHOODS"].StringValue.Substring(0, 1) != "'")
                    str = "'";

                sWhere += " AND fs.NEIGHBORHOOD_CLEAR IN (" + str + Params["NEIGHBORHOODS"].StringValue + str + ")";
            }

            if (Params.ParamExists("PROPERTY_ZIP") && Params["PROPERTY_ZIP"].StringValue.Length > 0)
                sWhere += " AND fs.ZIP_CODE IN (" + Params["PROPERTY_ZIP"].StringValue + ")";


            if (Params.ParamExists("SALE_PRICE_MIN") && Params["SALE_PRICE_MIN"].StringValue.Length > 0)
                sWhere += " AND fs.LIST_PRICE >= " + Params["SALE_PRICE_MIN"].StringValue;

            if (Params.ParamExists("SALE_PRICE_MAX") && Params["SALE_PRICE_MAX"].StringValue.Length > 0)
                sWhere += " AND fs.LIST_PRICE <= " + Params["SALE_PRICE_MAX"].StringValue;

            if (Params.ParamExists("BED_MIN") && Params["BED_MIN"].StringValue.Length > 0)
                sWhere += " AND fs.NO_BEDROOMS >= " + Params["BED_MIN"].StringValue;

            if (Params.ParamExists("PROPERTY_TYPE") && Params["PROPERTY_TYPE"].StringValue.Length > 0)
                sWhere += " AND fs.PROP_TYPE = '" + Params["PROPERTY_TYPE"].StringValue + "' ";

            if (Params.ParamExists("MAX_AGE") && Params["MAX_AGE"].StringValue.Length > 0)
                sWhere += " AND fs.YEAR_BUILT >= " + (DateTime.Now.Year - int.Parse(Params["MAX_AGE"].StringValue)).ToString();

            if (Params.ParamExists("MIN_GROSS_LIVING_AREA") && Params["MIN_GROSS_LIVING_AREA"].StringValue.Length > 0)
                sWhere += " AND fs.SQUARE_FEET >= " + Params["MIN_GROSS_LIVING_AREA"].StringValue;

            if (Params.ParamExists("MIN_TOTAL_ROOMS") && Params["MIN_TOTAL_ROOMS"].StringValue.Length > 0)
                sWhere += " AND fs.NO_ROOMS >= " + Params["MIN_TOTAL_ROOMS"].StringValue;

            if (Params.ParamExists("MIN_LOT_SIZE") && Params["MIN_LOT_SIZE"].StringValue.Length > 0)
                sWhere += " AND fs.ACRE >= " + Params["MIN_LOT_SIZE"].StringValue;

            sWhere = sWhere + GetParamCondition(" and fs.PROPERTY_STATE like '{0}' ", "PROPERTY_STATE");
            sWhere = sWhere + GetParamCondition(" and fs.NO_FULL_BATHS >= {0} ", "BATH_MIN");

            if (Params.ParamExists("TRANSACTION_TYPE") && Params["TRANSACTION_TYPE"].StringValue.Length > 0)
            {
                if (Params["TRANSACTION_TYPE"].StringValue == "ALL")
                {
                }

                if (Params["TRANSACTION_TYPE"].StringValue == "MLS")
                {
                    sWhere += " AND source_id in ( 1,2,3,4,6, 9) ";

                }

                if (Params["TRANSACTION_TYPE"].StringValue == "7,8")
                {
                    sWhere += " AND source_id in ( 7,8) ";

                }
                if (Params["TRANSACTION_TYPE"].StringValue == "7")
                {
                    sWhere += " AND source_id in ( 7) ";

                }

                if (Params["TRANSACTION_TYPE"].StringValue == "8")
                {
                    sWhere += " AND source_id in ( 8) ";

                }

            }

            if (hideCape)
                sWhere += " and SOURCE_ID <> 2 ";

            sSQL += sWhere;

            return sSQL;
        }

        private string GetMapSearchSQL()
        {
            string sSQL = " [ITEM_ID], [DATE_DUMP], [PROD_CAT_ID], [TYPE_NAME], [LIST_NO], [PROPERTY_CITY], [PROPERTY_STATE], [AGENT_FIRST_NAME], [AGENT_LAST_NAME], [LIST_PRICE], [NO_BEDROOMS], ISNULL([NO_TOTAL_BATHS],CAST([NO_FULL_BATHS] AS NUMERIC(8,2))) as [NO_FULL_BATHS], [PROP_TYPE], [AREA], [NO_ROOMS], [ACRE], [REMARKS], [HAVE_NO_IMAGES], [SQUARE_FEET], [STATUS], [LIST_DATE], [STATUS_DATE], [SOURCE_ID], [FILE_ID], [YEAR_BUILT], [NEIGHBORHOOD], [NEIGHBORHOOD_CLEAR], [SALE_PRICE], [STREET_NAME], [STREET_NO], [ZIP_CODE], [ASSESSMENTS], [OFFICE_NAME], [OFFICE_ADDRESS], LAT, LONG, dbo.GetForsaleImageID(LIST_NO) as IMAGE_ID, fs.AGENT_FIRST_NAME + ' '+fs.AGENT_LAST_NAME as LIST_AGENT_NAME, dbo.GetForsaleImageCount(LIST_NO) as IMAGE_COUNT FROM tblItems fs  with(nolock) " +
              " ";


            string sWhere = " where fs.STATUS IN ('ACT', 'NEW', 'EXT', 'A', 'PCG', 'AA', 'AB', 'AC', 'AH', 'AN', 'Active') ";







            //
            //
            //chkWaterfront
            //chkViews
            //chkNewConstruction
            //chkSingleStoryHomes
            //chkPremierPropertiesSM

            string propertyZip = "";
            string propertyCity = "";
            string propertyState = "";

            //Splitting city state zip.
            if (Params.ParamExists("txtCityStateZip") && Params["txtCityStateZip"].StringValue.Length > 0)
            {
                string txtCityStateZip = Params["txtCityStateZip"].StringValue;

                //check if this is ZIP
                Regex r = new Regex(@"^\d{5}(-\d{4})?$");

                Match m = r.Match(txtCityStateZip);
                if (m.Success)
                {
                    propertyZip = txtCityStateZip;
                }
                else //try to find city and state
                {
                    /// looking for state
                    /// Will find coma and take all treat text after coma as state
                    /// 

                    if (txtCityStateZip.IndexOf(",") > 0)
                    {
                        propertyState = txtCityStateZip.Split(',')[1].Trim();
                        propertyCity = txtCityStateZip.Split(',')[0].Trim();

                    }
                    else
                    {
                        propertyState = "MA";
                        propertyCity = txtCityStateZip;
                    }


                }


            }

            if (propertyZip != "")
                sWhere += " AND fs.ZIP_CODE IN ('" + propertyZip + "')";

            if (propertyState != "")
                sWhere += " and fs.PROPERTY_STATE = '" + propertyState + "'";

            if (propertyCity != "")
                sWhere += " AND fs.PROPERTY_CITY = '" + propertyCity + "'";

            if (Params.ParamExists("drpMinPrice") && Params["drpMinPrice"].StringValue.Length > 0)
                sWhere += " AND fs.LIST_PRICE >= " + Params["drpMinPrice"].StringValue;

            if (Params.ParamExists("drpMaxPrice") && Params["drpMaxPrice"].StringValue.Length > 0)
                sWhere += " AND fs.LIST_PRICE <= " + Params["drpMaxPrice"].StringValue;

            if (Params.ParamExists("drpBedrooms") && Params["drpBedrooms"].StringValue.Length > 0)
                sWhere += " AND fs.NO_BEDROOMS >= " + Params["drpBedrooms"].StringValue;

            if (Params.ParamExists("MAX_AGE") && Params["MAX_AGE"].StringValue.Length > 0)
                sWhere += " AND fs.YEAR_BUILT >= " + (DateTime.Now.Year - int.Parse(Params["MAX_AGE"].StringValue)).ToString();

            sWhere = sWhere + GetParamCondition(" and fs.PROP_TYPE like '{0}' ", "rblPropertyType");

            sWhere = sWhere + GetParamCondition(" and fs.NO_FULL_BATHS >= {0} ", "drpBathrooms");

            sWhere = sWhere + GetParamCondition(" and fs.YEAR_BUILT >= {0} ", "txtYearBuiltFrom");
            sWhere = sWhere + GetParamCondition(" and fs.YEAR_BUILT <= {0} ", "txtYearBuiltTo");
            sWhere = sWhere + GetParamCondition(" and fs.ACRE >= {0} ", "drpLotSize");
            if (Params.ParamExists("txtNewInLast") && Params["txtNewInLast"].StringValue.Length > 0)
            {
                try
                {
                    //Chech if we have int value 
                    int i = int.Parse(Params["txtNewInLast"].StringValue);

                    sWhere = sWhere + GetParamCondition(" and fs.DATE_DUMP >= DATEADD(day,-{0},DATE_DUMP) ", "txtNewInLast");
                }
                catch { }
            }
            sWhere = sWhere + GetParamCondition(" and fs.SQUARE_FEET >= {0} ", "txtSquareFeetFrom");
            sWhere = sWhere + GetParamCondition(" and fs.SQUARE_FEET <= {0} ", "txtSquareFeetTo");

            sSQL += sWhere;

            return sSQL;
        }
        private string GetPropertySearchSQL()
        {
            //string strSQL = " t.PRODUCT_ID, t.TRANS_AMOUNT, t.TRANS_DATE as SALE_DATE, t.TRANS_AMOUNT as SALE_PRICE, t.LIVING_AREA from  vwTRANS_P t WITH (NOEXPAND) ";
            string strSQL = " t.PRODUCT_ID, t.TRANS_AMOUNT, t.TRANS_DATE as SALE_DATE, t.TRANS_AMOUNT as SALE_PRICE, t.LIVING_AREA from  vwTRANS_P t";
            string strWhere = " where t.TRANS_TYPE in " + sqlSaleTrans;

            if (Params.ParamExists("PROPERTY_ADDRESS") && Params["PROPERTY_ADDRESS"].StringValue.Length > 0)
            {
                strWhere += string.Format(" and t.PROPERTY_ADDRESS like '{0}%' ", PrepareAddress(Params["PROPERTY_ADDRESS"].StringValue));
                //strWhere += string.Format("  or t.PROPERTY_ST_NAME like '{0}%') ", PrepareAddress(Params["PROPERTY_ADDRESS"].StringValue));

            }


            strWhere = strWhere + GetParamCondition(" and t.PROPERTY_CITY like '{0}' ", "PROPERTY_CITY");
            strWhere = strWhere + GetParamCondition(" and t.PROPERTY_STATE like '{0}' ", "PROPERTY_STATE");

            strWhere = strWhere + GetParamCondition(" and t.TRANS_AMOUNT > {0} ", "SALE_PRICE_MIN");
            strWhere = strWhere + GetParamCondition(" and t.TRANS_AMOUNT < {0} ", "SALE_PRICE_MAX");

            strWhere = strWhere + GetParamCondition(" and t.YR_BUILT > {0} ", "YEAR_BUILT_MIN");
            strWhere = strWhere + GetParamCondition(" and t.YR_BUILT < {0} ", "YEAR_BUILT_MAX");

            strWhere = strWhere + GetParamCondition(" and t.LIVING_AREA > {0} ", "LIVING_AREA_MIN");
            strWhere = strWhere + GetParamCondition(" and t.LIVING_AREA < {0} ", "LIVING_AREA_MAX");

            strWhere = strWhere + GetParamCondition(" and t.BEDROOMS > {0} ", "BED_MIN");
            strWhere = strWhere + GetParamCondition(" and t.BEDROOMS < {0} ", "BED_MAX");

            strWhere = strWhere + GetParamCondition(" and t.BEDROOMS > {0} ", "BED_MIN");
            strWhere = strWhere + GetParamCondition(" and t.BEDROOMS < {0} ", "BED_MAX");

            strWhere = strWhere + GetParamCondition(" and t.LOTSIZE_SF > {0}*" + SqFtAcre + " ", "LOTSIZE_MIN");
            strWhere = strWhere + GetParamCondition(" and t.LOTSIZE_SF < {0}*" + SqFtAcre + " ", "LOTSIZE_MAX");

            strWhere = strWhere + GetParamCondition(" and t.BASEMENT like '{0}' ", "BASEMENT");

            strWhere = strWhere + GetParamCondition(" and t.FULL_BATHS > {0} ", "BATH_MIN");
            strWhere = strWhere + GetParamCondition(" and t.FULL_BATHS < {0} ", "BATH_MAX");

            if (Params.ParamExists("ASSESSMENT_MIN") || Params.ParamExists("ASSESSMENT_MAX"))
            {
                //strSQL += " inner join  vwTRANS_P t2 WITH (NOEXPAND) ON t.PRODUCT_ID = t2.PRODUCT_ID ";
                strSQL += " inner join  vwTRANS_P t2 ON t.PRODUCT_ID = t2.PRODUCT_ID ";
                strWhere = strWhere + GetParamCondition(" and t2.TRANS_AMOUNT > {0} ", "ASSESSMENT_MIN");
                strWhere = strWhere + GetParamCondition(" and t2.TRANS_AMOUNT < {0} ", "ASSESSMENT_MAX");
            }


            strWhere = strWhere + GetParamCondition(" and t.STRUCT_CLASS in ({0}) ", "STRUCT_CLASS");


            strSQL += strWhere;

            return strSQL;

        }


        private string GetForeclosureSearchSQL()
        {
            string sSQL = " [ITEM_ID], [DATE_DUMP], [PROD_CAT_ID], [TYPE_NAME], [LIST_NO], [PROPERTY_CITY], [PROPERTY_STATE], [AGENT_FIRST_NAME], [AGENT_LAST_NAME], [LIST_PRICE], [NO_BEDROOMS], ISNULL([NO_TOTAL_BATHS],CAST([NO_FULL_BATHS] AS NUMERIC(8,2))) as [NO_FULL_BATHS], [PROP_TYPE], [AREA], [NO_ROOMS], [ACRE], [REMARKS], [HAVE_NO_IMAGES], [SQUARE_FEET], [STATUS], [LIST_DATE], [STATUS_DATE], [SOURCE_ID], [FILE_ID], [YEAR_BUILT], [NEIGHBORHOOD], [NEIGHBORHOOD_CLEAR], [SALE_PRICE], [STREET_NAME], [STREET_NO], [ZIP_CODE], [ASSESSMENTS], [OFFICE_NAME], [OFFICE_ADDRESS], dbo.GetForsaleImageID(LIST_NO) as IMAGE_ID, fs.AGENT_FIRST_NAME + ' '+fs.AGENT_LAST_NAME as LIST_AGENT_NAME, dbo.GetForsaleImageCount(LIST_NO) as IMAGE_COUNT, LAT, LONG FROM tblItems fs with(nolock) " +
                " ";


            string sWhere = "";

            if (Params.ParamExists("LIST_NO") && Params["LIST_NO"].StringValue.Length > 0)
            {
                sWhere = "WHERE " + GetParamCondition(" fs.LIST_NO = '{0}' ", "LIST_NO");
                return sSQL + sWhere;
            }


            if (Params.ParamExists("FORECLOSURE_TYPE"))
            {
                if (Params["FORECLOSURE_TYPE"].StringValue == "REO")
                    sWhere += " where source_id = 8 ";
                else
                    sWhere += " where source_id = 7 ";

            }
            else
                sWhere += " where source_id in ( 7, 8) ";

            sWhere += " AND STATUS <> 'DEL' ";

            if (Params.ParamExists("PROPERTY_CITY") && Params["PROPERTY_CITY"].StringValue.Length > 0)
            {


                sWhere += " AND fs.PROPERTY_CITY IN ('" + Params["PROPERTY_CITY"].StringValue.Replace("\'", "\'\'") + "')";
            }


            if (Params.ParamExists("PROPERTY_ZIP") && Params["PROPERTY_ZIP"].StringValue.Length > 0)
                sWhere += " AND fs.ZIP_CODE IN ('" + Params["PROPERTY_ZIP"].StringValue.Replace("'", "''") + "')";


            if (Params.ParamExists("SALE_PRICE_MIN") && Params["SALE_PRICE_MIN"].StringValue.Length > 0)
                sWhere += " AND fs.LIST_PRICE >= " + Params["SALE_PRICE_MIN"].StringValue;

            if (Params.ParamExists("SALE_PRICE_MAX") && Params["SALE_PRICE_MAX"].StringValue.Length > 0)
                sWhere += " AND fs.LIST_PRICE <= " + Params["SALE_PRICE_MAX"].StringValue;

            if (Params.ParamExists("BED_MIN") && Params["BED_MIN"].StringValue.Length > 0)
                sWhere += " AND fs.NO_BEDROOMS >= " + Params["BED_MIN"].StringValue;

            if (Params.ParamExists("PROPERTY_TYPE") && Params["PROPERTY_TYPE"].StringValue.Length > 0)
                sWhere += " AND fs.PROP_TYPE = '" + Params["PROPERTY_TYPE"].StringValue + "' ";

            if (Params.ParamExists("MAX_AGE") && Params["MAX_AGE"].StringValue.Length > 0)
                sWhere += " AND fs.YEAR_BUILT >= " + (DateTime.Now.Year - int.Parse(Params["MAX_AGE"].StringValue)).ToString();

            if (Params.ParamExists("MIN_GROSS_LIVING_AREA") && Params["MIN_GROSS_LIVING_AREA"].StringValue.Length > 0)
                sWhere += " AND fs.SQUARE_FEET >= " + Params["MIN_GROSS_LIVING_AREA"].StringValue;

            if (Params.ParamExists("MIN_TOTAL_ROOMS") && Params["MIN_TOTAL_ROOMS"].StringValue.Length > 0)
                sWhere += " AND fs.NO_ROOMS >= " + Params["MIN_TOTAL_ROOMS"].StringValue;

            if (Params.ParamExists("MIN_LOT_SIZE") && Params["MIN_LOT_SIZE"].StringValue.Length > 0)
                sWhere += " AND fs.ACRE >= " + Params["MIN_LOT_SIZE"].StringValue;

            if (Params.ParamExists("IS_FORFEITED") && Params["IS_FORFEITED"].ValueBool)
                sWhere += " AND fs.IS_FORFEITED = 1 ";

            sWhere = sWhere + GetParamCondition(" and fs.PROPERTY_STATE = '{0}' ", "PROPERTY_STATE");
            sWhere = sWhere + GetParamCondition(" and fs.NO_FULL_BATHS >= {0} ", "BATH_MIN");

            if (Params.ParamExists("txtNewInLast") && Params["txtNewInLast"].StringValue.Length > 0)
            {
                try
                {
                    //Chech if we have int value 
                    int i = int.Parse(Params["txtNewInLast"].StringValue);

                    sWhere = sWhere + string.Format(" and fs.DATE_DUMP >= '{0}' ",
                        DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));

                    //sWhere = sWhere + GetParamCondition(" and fs.DATE_DUMP >= DATEADD(day,-{0},DATE_DUMP) ", "txtNewInLast");
                }
                catch { }
            }

            sSQL += sWhere;
            //System.Web.HttpContext.Current.Response.Write("<!-- " + Params.ParamExists("IS_FORFEITED").ToString() + " :: " + Params["IS_FORFEITED"].ValueBool.ToString() + " -->");
            //System.Web.HttpContext.Current.Response.Write("<!-- " + sSQL + " -->");
            return sSQL;

        }

        private string GetNameSearchSQL()
        {
            //			strSQL = " t.PRODUCT_ID, t.TRANS_AMOUNT from  tblP p  inner join  tblTrans t ON P.PRODUCT_ID = t.PRODUCT_ID ";
            //			strWhere = " where t.TRANS_TYPE in " + Tools.StaticFunctions.sqlSaleTrans;
            //			strWhere += " and t.TRANS_ORDER = 0 ";
            //
            //			if(this.Session["Search_Company"].ToString().Length > 0 ||
            //				this.Session["Search_FName"].ToString().Length > 0 || 
            //				this.Session["Search_LName"].ToString().Length > 0) 
            //			{
            //
            //				strSQL += " inner join  tblTrans t1 ON P.PRODUCT_ID = t1.PRODUCT_ID INNER JOIN "+
            //					"tblTrans_tblOwners ON t1.TRANS_ID = tblTrans_tblOwners.TRANS_ID INNER JOIN "+
            //					" tblOwners o ON tblTrans_tblOwners.OWNER_ID = o.OWNER_ID ";
            //
            //				strWhere += " and t1.TRANS_ORDER = 0 and t1.TRANS_TYPE <> 'MORTGAGE' and t1.TRANS_TYPE <> 'ASSESSMENT' and tblTrans_tblOwners.TRANS_OWNER_TYPE = 'BUYER' ";
            //
            //				if(this.Session["Search_Company"].ToString().Length > 0) 
            //				{
            //					strWhere = strWhere+" and o.FULL_NAME like @FULL_NAME";
            //
            //					tmpSqlParameter = new SqlParameter("@FULL_NAME", System.Data.SqlDbType.VarChar, 255, "FULL_NAME");
            //					tmpSqlParameter.Value = this.Session["Search_Company"].ToString()+"%";
            //					objCommand.Parameters.Add(tmpSqlParameter);
            //				}
            //
            //				if(this.Session["Search_FName"].ToString().Length > 0) 
            //				{
            //					strWhere = strWhere+" and o.FIRST_NAME like @FIRST_NAME ";
            //
            //					tmpSqlParameter = new SqlParameter("@FIRST_NAME", System.Data.SqlDbType.VarChar, 255, "FIRST_NAME");
            //					tmpSqlParameter.Value = this.Session["Search_FName"].ToString()+"%";
            //					objCommand.Parameters.Add(tmpSqlParameter);
            //				}
            //				if(this.Session["Search_LName"].ToString().Length > 0) 
            //				{
            //					strWhere = strWhere+" and o.LAST_NAME like @LAST_NAME ";
            //
            //					tmpSqlParameter = new SqlParameter("@LAST_NAME", System.Data.SqlDbType.VarChar, 255, "LAST_NAME");
            //					tmpSqlParameter.Value = this.Session["Search_LName"].ToString()+"%";
            //					objCommand.Parameters.Add(tmpSqlParameter);
            //				}
            //			}
            //
            //			if(this.Session["Search_City"].ToString().Length > 0) 
            //			{
            //				strWhere = strWhere+" and PROPERTY_CITY like @PROPERTY_CITY ";
            //
            //				tmpSqlParameter = new SqlParameter("@PROPERTY_CITY", System.Data.SqlDbType.VarChar, 255, "PROPERTY_CITY");
            //				tmpSqlParameter.Value = this.Session["Search_City"].ToString()+"%";
            //				objCommand.Parameters.Add(tmpSqlParameter);
            //			}
            //			if(this.Session["Search_State"].ToString().Length > 0) 
            //			{
            //				strWhere = strWhere+" and PROPERTY_STATE like @PROPERTY_STATE ";
            //
            //				tmpSqlParameter = new SqlParameter("@PROPERTY_STATE", System.Data.SqlDbType.VarChar, 255, "PROPERTY_STATE");
            //				tmpSqlParameter.Value = this.Session["Search_State"].ToString()+"%";
            //				objCommand.Parameters.Add(tmpSqlParameter);
            //			}
            //
            //			if(this.Session["Search_SalePriceFrom"].ToString().Length > 0) 
            //			{
            //				strWhere = strWhere+" and t.TRANS_AMOUNT >= @TRANS_AMOUNT1 ";
            //
            //				tmpSqlParameter = new SqlParameter("@TRANS_AMOUNT1", System.Data.SqlDbType.Int, 4, "TRANS_AMOUNT");
            //				tmpSqlParameter.Value = int.Parse(this.Session["Search_SalePriceFrom"].ToString());
            //				objCommand.Parameters.Add(tmpSqlParameter);
            //			}
            //			if(this.Session["Search_SalePriceTo"].ToString().Length > 0 ) 
            //			{
            //				strWhere = strWhere+" and t.TRANS_AMOUNT <= @TRANS_AMOUNT2";
            //
            //				tmpSqlParameter = new SqlParameter("@TRANS_AMOUNT2", System.Data.SqlDbType.Int, 4, "TRANS_AMOUNT");
            //				tmpSqlParameter.Value = int.Parse(this.Session["Search_SalePriceTo"].ToString());
            //				objCommand.Parameters.Add(tmpSqlParameter);
            //			}
            //			if ((this.Session["Search_PropType"].ToString().Length > 0)&&(this.Session["Search_PropType"].ToString() != "All"))
            //			{
            //				strWhere = strWhere+" and intPTYPE_ID like @intPTYPE_ID ";
            //
            //				tmpSqlParameter = new SqlParameter("@intPTYPE_ID", System.Data.SqlDbType.VarChar, 255, "intPTYPE_ID");
            //				tmpSqlParameter.Value = this.Session["Search_PropType"].ToString();
            //				objCommand.Parameters.Add(tmpSqlParameter);
            //			}
            //			if(this.Session["Search_SaleDate"].ToString().Length > 0) 
            //			{
            //				strWhere = strWhere+" and TRANS_DATE <= @TRANS_DATE ";
            //
            //				tmpSqlParameter = new SqlParameter("@TRANS_DATE", System.Data.SqlDbType.DateTime, 4, "TRANS_DATE");
            //				tmpSqlParameter.Value = this.Session["Search_SaleDate"].ToString();
            //				objCommand.Parameters.Add(tmpSqlParameter);
            //			}
            //
            //			strSQL += strWhere;
            return "";
        }

        private string GetDefaultSearchSQL()
        {
            //string strSQL = " t.PRODUCT_ID, t.TRANS_AMOUNT  from vwTRANS_P t WITH (NOEXPAND)   ";
            string strSQL = " t.PRODUCT_ID, t.TRANS_AMOUNT  from vwTRANS_P t  ";
            string strWhere = " where t.TRANS_TYPE in " + sqlSaleTrans;

            if (Params.ParamExists("PROPERTY_ADDRESS") && Params["PROPERTY_ADDRESS"].StringValue.Length > 0)
            {
                strWhere += string.Format(" and (t.PROPERTY_ADDRESS like '{0}%' ", PrepareAddress(Params["PROPERTY_ADDRESS"].StringValue));
                strWhere += string.Format("  or t.PROPERTY_ST_NAME like '{0}%') ", PrepareAddress(Params["PROPERTY_ADDRESS"].StringValue));

            }

            strWhere = strWhere + GetParamCondition(" and t.PROPERTY_CITY like '{0}' ", "PROPERTY_CITY");
            strWhere = strWhere + GetParamCondition(" and t.PROPERTY_STATE like '{0}' ", "PROPERTY_STATE");

            if (Params.ParamExists("FULL_NAME"))
            {
                strSQL += " inner join tblTrans_tblOwners with(nolock) ON t.TRANS_ID = tblTrans_tblOwners.TRANS_ID INNER JOIN " +
                    " tblOwners o with(nolock) ON tblTrans_tblOwners.OWNER_ID = o.OWNER_ID ";

                strWhere += " and t.TRANS_TYPE <> 'MORTGAGE' and t.TRANS_TYPE <> 'ASSESSMENT' and tblTrans_tblOwners.TRANS_OWNER_TYPE = 'BUYER' ";
                strWhere += GetParamCondition(" and o.FULL_NAME like '{0}%' ", "FULL_NAME");
            }



            strSQL += strWhere;

            return strSQL;
        }
        public string GetBasicSQLText(string dtLastProcessed)
        {
            string s = GetBasicSQLText();
            s += " AND ";
            if (SEARCH_TYPE_NAME == "FOR_SALE_BASIC" || SEARCH_TYPE_NAME == "FOR_SALE_ADVANCED" || SEARCH_TYPE_NAME == "FORECLOSURE_SEARCH")
                s += "fs.DATE_DUMP";
            else
                s += "t.DATA_DUMP";

            s += " >= '" + dtLastProcessed + "'";

            return s;



        }

        public bool hideCape = false;

        public string GetBasicSQLText()
        {

            switch (SEARCH_TYPE_NAME)
            {
                case "FOR_SALE_BASIC":
                    return GetForSaleBasicSQL();
                case "FOR_SALE_ADVANCED":
                    return GetForSaleBasicSQL();
                case "PROPERTY":
                    return GetPropertySearchSQL();
                case "DEFAULT":
                    return GetDefaultSearchSQL();
                case "FORECLOSURE_SEARCH":
                    return GetForeclosureSearchSQL();
                case "VIRTUALMAP_SEARCH":
                    return GetMapSearchSQL();
                default:
                    return "";
            }
        }

        public string GetBasicSQLText(double topLAT, double topLONG, double botLAT, double botLONG)
        {
            string geoString = " AND LAT <= " + topLAT.ToString() + " AND LONG >= " + topLONG.ToString() + " AND LAT >= " + botLAT.ToString() + " AND LONG <= " + botLONG.ToString();

            switch (SEARCH_TYPE_NAME)
            {
                case "FOR_SALE_BASIC":
                    return GetForSaleBasicSQL() + geoString;
                case "FOR_SALE_ADVANCED":
                    return GetForSaleBasicSQL() + geoString;
                case "PROPERTY":
                    return GetPropertySearchSQL();
                case "DEFAULT":
                    return GetDefaultSearchSQL();
                case "FORECLOSURE_SEARCH":
                    return GetForeclosureSearchSQL() + geoString;
                case "VIRTUALMAP_SEARCH":
                    return GetMapSearchSQL() + geoString;
                default:
                    return "";
            }
        }

        /// <summary>
        /// Count SQL for Notification processing
        /// </summary>
        /// <param name="dtLastProcessed"></param>
        /// <returns></returns>
        public string GetCountSQLText(string dtLastProcessed)
        {
            return "select count(*) as FieldCount from ( SELECT TOP 500 " + GetBasicSQLText(dtLastProcessed) + ") tbl1";
        }

        public string GetCountSQLText()
        {
            return "select count(*) as FieldCount from ( SELECT TOP 500 " + GetBasicSQLText() + ") tbl1";
        }

        public string GetCountSQLText(double topLAT, double topLONG, double botLAT, double botLONG)
        {
            return "select count(*) as FieldCount from ( SELECT TOP 500 " + GetBasicSQLText(topLAT, topLONG, botLAT, botLONG) + ") tbl1";
        }

        /// <summary>
        /// Search SQL for Notification processing
        /// </summary>
        /// <param name="_SortParam"></param>
        /// <param name="_PageSize"></param>
        /// <param name="_PageCurrent"></param>
        /// <param name="_Rows_Count"></param>
        /// <param name="dtLastProcessed"></param>
        /// <returns></returns>
        public string GetSearchSQLText(string _SortParam, int _PageSize, int _PageCurrent, int _Rows_Count, string dtLastProcessed)
        {
            return WrapSqlForPaging(GetBasicSQLText(dtLastProcessed), _SortParam, _PageSize, _PageCurrent, _Rows_Count);
        }

        public string GetSearchSQLText(string _SortParam, string sort_order, int _PageSize, int _PageCurrent, int _Rows_Count, string dtLastProcessed)
        {
            return WrapSqlForPaging(GetBasicSQLText(dtLastProcessed), _SortParam, sort_order, _PageSize, _PageCurrent, _Rows_Count);
        }
        /// <summary>
        /// Search SQL
        /// </summary>
        /// <param name="_SortParam"></param>
        /// <param name="_PageSize"></param>
        /// <param name="_PageCurrent"></param>
        /// <param name="_Rows_Count"></param>
        /// <returns></returns>
        public string GetSearchSQLText(string _SortParam, int _PageSize, int _PageCurrent, int _Rows_Count)
        {
            return WrapSqlForPaging(GetBasicSQLText(), _SortParam, _PageSize, _PageCurrent, _Rows_Count);
        }

        public string GetSearchSQLText(string _SortParam, string sort_order, int _PageSize, int _PageCurrent, int _Rows_Count)
        {
            return WrapSqlForPaging(GetBasicSQLText(), _SortParam, sort_order, _PageSize, _PageCurrent, _Rows_Count);
        }


        public string GetSearchSQLText(string _SortParam, int _PageSize, int _PageCurrent, int _Rows_Count,
            double topLAT, double topLONG, double botLAT, double botLONG)
        {
            return WrapSqlForPaging(GetBasicSQLText(topLAT, topLONG, botLAT, botLONG), _SortParam, _PageSize, _PageCurrent, _Rows_Count);
        }

        public string GetSearchSQLText(string _SortParam, string sort_order, int _PageSize, int _PageCurrent, int _Rows_Count,
            double topLAT, double topLONG, double botLAT, double botLONG)
        {
            return WrapSqlForPaging(GetBasicSQLText(topLAT, topLONG, botLAT, botLONG), _SortParam, sort_order, _PageSize, _PageCurrent, _Rows_Count);
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string FilledFormText
        {
            get
            {
                string s = "<table>";
                string _group = "";
                for (int i = 0; i <= Params.Count - 1; i++)
                {
                    if (_group != Params[i].GROUP_DESCR)
                    {
                        s += "<tr><td colspan=2 bgcolor=LightCyan><b>" + Params[i].GROUP_DESCR + "</b></td></tr>";
                        _group = Params[i].GROUP_DESCR;
                    }
                    s += "<tr><td align=right><b>" + Params[i].SEARCH_PARAM_DESCR + ": </b></td>";
                    s += "<td>" + Params[i].StringValue + "</td></tr>";

                }
                s += "</table>";

                return s;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string SEARCH_TEXT
        {
            get
            {
                string s = "<table>";
                for (int i = 0; i <= Params.Count - 1; i++)
                {
                    s += "<tr><td align=right><b>" + Params[i].SEARCH_PARAM_DESCR + ": </b></td>";
                    s += "<td>" + Params[i].StringValue + "</td></tr>";

                }
                s += "</table>";

                return s;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Search emptyElement;


        private void FillParamFromEmptyForm(SearchParam sp)
        {
            if (emptyElement.Params.ParamExists(sp.SEARCH_PARAM_NAME))
            {
                sp.ParamType = emptyElement.Params[sp.SEARCH_PARAM_NAME].ParamType;
                sp.ParamFormat = emptyElement.Params[sp.SEARCH_PARAM_NAME].ParamFormat;
                sp.SEARCH_PARAM_DESCR = emptyElement.Params[sp.SEARCH_PARAM_NAME].SEARCH_PARAM_DESCR;
                sp.GROUP_NAME = emptyElement.Params[sp.SEARCH_PARAM_NAME].GROUP_NAME;
                sp.GROUP_DESCR = emptyElement.Params[sp.SEARCH_PARAM_NAME].GROUP_DESCR;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string Template = "{%PRODUCT_IMAGE%} <br>\n	Checkbox <a href='some_link_to_product'> {%edtPRODUCT_NAME%} </a><br>\n  {%edtPRODUCT_DESCR%} <br>\n	  {%lblWIDTH%} : {%edtWIDTH%} <br> \n{%lblHEIGHT%} : {%edtHEIGHT%} <br>";

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string TemplateDetailsHeader = "<table>		<tr>			<td>		<img src='GetParamImage.aspx?IMAGE_ID={%edtAL_IMAGE%}'>			 </td>			 <td>		<table class=\"formtext\">";
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string TemplateDetailsFooter = "</table>			</td>		</tr>		</table>";
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string TemplateDetailsItem = "<tr><td valign=top>{%lbl%}</td><td>{%edt%}</tr>";
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string TemplateDetailsGroup = "";

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string OUT_GROUP = "";

        /// <summary>
        /// Returns formated filled form using TemplateDetailsHeader, TemplateDetailsFooter, TemplateDetailsItem properties.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string TextByTemplate
        {
            get
            {
                return "";                
            }
        }

        /// <summary>
        /// Returns formated filled form.
        /// All parameters are replaced one by one. 
        /// Example: lblPARAM_NAME reaplaced with SEARCH_PARAM_NAME's value. edtPARAM_NAME replaced with param's StringValue
        /// 
        /// Also replaced parameters:
        ///		SEARCH_LOG_ID
        ///		DATE_DUMP
        ///	
        ///	For these parameters 'edt' and 'lbl' not applyed.
        /// </summary>
        /// <param name="_Template">Template</param>
        /// <returns>Formated text</returns>
        public string GetTextByTemplate(string _Template)
        {
            int i = 0;
            string paramValue = "";
            while (i <= Params.Count - 1)
            {
                _Template = _Template.Replace("{%lbl" + Params[i].SEARCH_PARAM_NAME + "%}", Params[i].SEARCH_PARAM_DESCR);

                //If we have asme parametr more than once -- we should add all this parameters devided by <br>
                if (i < Params.Count - 1 && Params[i].SEARCH_PARAM_NAME == Params[i + 1].SEARCH_PARAM_NAME)
                    paramValue += Params[i].StringValue + "<hr>";
                else
                {
                    //If we already have same parameter then we need to replace previous name
                    if (paramValue != "")
                    {
                        paramValue += Params[i].StringValue + "<br>";
                        _Template = _Template.Replace("{%edt" + Params[i].SEARCH_PARAM_NAME + "%}", paramValue);
                        paramValue = "";
                    }
                    else
                        _Template = _Template.Replace("{%edt" + Params[i].SEARCH_PARAM_NAME + "%}", Params[i].StringValue);

                }

                i++;
            }
            _Template = _Template.Replace("{%SEARCH_LOG_ID%}", SEARCH_LOG_ID.ToString());
            _Template = _Template.Replace("{%DATE_DUMP%}", DATE_DUMP.ToString());


            ///Remove all unfilled parameters in template

            while (_Template.IndexOf("{%") > -1 && _Template.IndexOf("%}") > -1)
            {
                int a = _Template.IndexOf("{%");
                int b = _Template.IndexOf("%}") + 2;
                _Template = _Template.Remove(a, b - a);
            }

            return _Template;
        }




        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            try
            {
                Search s = (Search)obj;
                return String.Compare(this.Params[sortParam].StringValue, s.Params[sortParam].StringValue);
            }
            catch { }
            return 1;
        }

        #endregion

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string XMLData
        {
            get
            {
                MemoryStream sb = new MemoryStream();
                XmlWriterSettings set = new XmlWriterSettings();
                set.Encoding = Encoding.ASCII;
                //set.Indent = true;
                //set.IndentChars = ("     ");
                XmlWriter wr = XmlWriter.Create(sb, set);
                //wr.Set
                wr.WriteStartElement("item");
                string res = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?> \n<item>";

                for (int i = 0; i <= Params.Count - 1; i++)
                {
                    if (Params[i].SEARCH_PARAM_NAME != "IMAGES")
                    {
                        bool noFormat = Params[i].NoFormat;

                        Params[i].NoFormat = true;

                        Params[i].NoFormat = true;
                        res += "<param>\n";
                        wr.WriteStartElement("param");
                        res += "\t<paramName>" + Params[i].SEARCH_PARAM_NAME + "</paramName>\n";

                        wr.WriteStartElement("paramName");
                        wr.WriteElementString("paramName", Params[i].SEARCH_PARAM_NAME);
                        wr.WriteEndElement();

                        res += "\t<paramValue>\"" + Params[i].StringValue + "\"</paramValue>\n";
                        wr.WriteStartElement("paramValue");

                        ///Put assignment to try block because of Unicode characters which can't handle for now
                        try
                        {



                            wr.WriteElementString("paramValue", Encoding.ASCII.GetString(Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(System.Web.HttpUtility.UrlEncode(Params[i].StringValue)))));
                        }
                        catch
                        {
                            //wr.WriteElementString("paramValue", "");
                        }
                        wr.WriteEndElement();

                        res += "</param>\n";
                        wr.WriteEndElement();

                        Params[i].NoFormat = noFormat;
                    }
                }
                res += "</item>";
                wr.WriteEndElement();
                wr.Flush();
                byte[] b = new byte[sb.Length];
                sb.Position = 0;
                sb.Read(b, 0, (int)sb.Length);

                return Encoding.ASCII.GetString(b);
            }
            set
            {
                MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));

                XPathDocument doc = new XPathDocument(ms);
                XPathNavigator nav = doc.CreateNavigator();

                // Compile a standard XPath expression
                XPathExpression expr;
                expr = nav.Compile("/item/param");
                XPathNodeIterator iterator = nav.Select(expr);

                while (iterator.MoveNext())
                {
                    SearchParam sp = new SearchParam();
                    XPathNavigator nav2 = iterator.Current.Clone();

                    nav2.MoveToFirstChild();
                    sp.SEARCH_PARAM_NAME = nav2.Value;
                    FillParamFromEmptyForm(sp);
                    nav2.MoveToNext();
                    sp.StringValue = System.Web.HttpUtility.UrlDecode(nav2.Value);

                    if (sp.SEARCH_PARAM_NAME != "IMAGES")
                        Params.Add(sp);
                }


            }
        }

    }

    [Serializable]
    public class SearchParamCollection : CollectionBase
    {
        public SearchParam this[int index]
        {
            get { return ((SearchParam)List[index]); }
            set { List[index] = value; }
        }

        public bool ParamExists(string _ParamName)
        {
            for (int i = 0; i <= Count - 1; i++)
                if (this[i].SEARCH_PARAM_NAME == _ParamName)
                {
                    return true;
                }

            return false;
        }


        public SearchParam this[string index]
        {
            get
            {
                SearchParam _sp = null;

                for (int i = 0; i <= Count - 1; i++)
                    if (this[i].SEARCH_PARAM_NAME == index)
                        _sp = this[i];

                return _sp;
            }
        }

        public string GetParamStringValue(string _ParamName)
        {
            string r = "";
            if (ParamExists(_ParamName))
            {
                r = this[_ParamName].StringValue;
            }
            return r;
        }

        public int Add(SearchParam value)
        {
            //Prevent adding empty params
            if (value.StringValue != "")
                return (List.Add(value));
            else
                return -1;
        }

        /// <summary>
        /// Parameter added even if it is empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AddForce(SearchParam value)
        {
            return (List.Add(value));
        }


        /// <summary>
        /// Sort all params by goups.
        /// Top group -- first group
        /// </summary>
        public void SortByGroup()
        {
            for (int i = 0; i < this.Count - 1; i++)
            {
                int k = i + 1;

                for (int j = i + 1; j <= this.Count - 1; j++)
                {
                    if (this[i].GROUP_NAME == this[j].GROUP_NAME)
                    {
                        //swap
                        if (j != k)
                        {
                            SearchParam _sp = this[j];
                            this.RemoveAt(j);
                            this.Insert(k, _sp);
                        }
                        k++;
                    }
                }
            }
        }

        public int IndexOf(SearchParam value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, SearchParam value)
        {
            List.Insert(index, value);
        }

        public void Remove(SearchParam value)
        {
            List.Remove(value);
        }

        public bool Contains(SearchParam value)
        {
            // If value is not of type User, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParam"))
                throw new ArgumentException("value must be of type SearchParam.", "value");
        }

        protected override void OnRemove(int index, Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParam"))
                throw new ArgumentException("value must be of type SearchParam.", "value");
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (newValue.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParam"))
                throw new ArgumentException("newValue must be of type SearchParam.", "newValue");
        }

        protected override void OnValidate(Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParam"))
                throw new ArgumentException("value must be of type SearchParam.");
        }

    }

    [Serializable]
    public enum SearchParamTypes
    {
        ParamTypeInt,
        ParamTypeString,
        ParamTypeFloat,
        ParamTypeBool,
        ParamTypeDate,
        ParamTypeImage
    }

    /// <summary>
    /// Param for searches.
    /// </summary>
    [Serializable]
    public class SearchParam
    {
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public SearchParamFormat ParamFormat;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public SearchParamValueCollection DefaultValues;

        public SearchParamTypes ParamType;
        public bool NoFormat = false;
        private int _IS_PUBLIC;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int IS_PUBLIC
        {
            get { return _IS_PUBLIC; }
            set { _IS_PUBLIC = value; }
        }

        private string _SEARCH_PARAM_NAME;
        public string SEARCH_PARAM_NAME
        {
            get { return _SEARCH_PARAM_NAME; }
            set { _SEARCH_PARAM_NAME = value; }
        }


        private string _SEARCH_PARAM_DESCR;
        public string SEARCH_PARAM_DESCR
        {
            get { return _SEARCH_PARAM_DESCR; }
            set { _SEARCH_PARAM_DESCR = value; }
        }


        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int ValueInt;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public double ValueFloat;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string ValueString = "";
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public DateTime ValueDate;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool ValueBool;
        public byte[] ValueImage;
        public string ValueImageContentType;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int ImageNo = 0;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string ImageName;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid IMAGE_ID;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Guid SEARCH_PARAM_VALUE_ID;

        private string _GROUP_DESCR;
        public string GROUP_DESCR
        {
            get { return _GROUP_DESCR; }
            set { _GROUP_DESCR = value; }
        }

        private string _GROUP_NAME;
        public string GROUP_NAME
        {
            get { return _GROUP_NAME; }
            set { _GROUP_NAME = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool _MANDATORY;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool MANDATORY
        {
            get { return _MANDATORY; }
            set { _MANDATORY = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool _SHOW_IN_ADMIN_TABLE;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool SHOW_IN_ADMIN_TABLE
        {
            get { return _SHOW_IN_ADMIN_TABLE; }
            set { _SHOW_IN_ADMIN_TABLE = value; }
        }

        public string SEARCH_PARAM_TYPE
        {
            get
            {
                switch (ParamType)
                {
                    case SearchParamTypes.ParamTypeString:
                        return "STRING";
                    case SearchParamTypes.ParamTypeInt:
                        return "INT";
                    case SearchParamTypes.ParamTypeFloat:
                        return "FLOAT";
                    case SearchParamTypes.ParamTypeBool:
                        return "BOOLEAN";
                    case SearchParamTypes.ParamTypeDate:
                        return "DATE";
                    case SearchParamTypes.ParamTypeImage:
                        return "IMAGE";
                    default:
                        return "";
                }
            }
            set
            {
                if (value.ToUpper() == "STRING")
                    ParamType = SearchParamTypes.ParamTypeString;
                else
                    if (value.ToUpper() == "FLOAT")
                        ParamType = SearchParamTypes.ParamTypeFloat;
                    else
                        if (value.ToUpper() == "INT")
                            ParamType = SearchParamTypes.ParamTypeInt;
                        else
                            if (value.ToUpper() == "BOOLEAN")
                                ParamType = SearchParamTypes.ParamTypeBool;
                            else
                                if (value.ToUpper() == "DATE")
                                    ParamType = SearchParamTypes.ParamTypeDate;
                                else
                                    if (value.ToUpper() == "IMAGE")
                                        ParamType = SearchParamTypes.ParamTypeImage;
                                    else
                                        throw new Exception("SearchParam TYPE assign error. Unknown type");
            }
        }

        public object Value
        {
            get
            {
                switch (ParamType)
                {
                    case SearchParamTypes.ParamTypeString:
                        return this.ValueString;
                    case SearchParamTypes.ParamTypeInt:
                        return this.ValueInt;
                    case SearchParamTypes.ParamTypeFloat:
                        return this.ValueFloat;
                    case SearchParamTypes.ParamTypeBool:
                        return this.ValueBool;
                    case SearchParamTypes.ParamTypeDate:
                        return this.ValueDate;
                    case SearchParamTypes.ParamTypeImage:
                        return this.ValueImage;
                    default:
                        return StringValue;
                }
            }

            set
            {
                StringValue = value != null ? value.ToString() : string.Empty;
            }
        }

        public string StringValue
        {
            get
            {
                string s = "";
                switch (ParamType)
                {
                    case SearchParamTypes.ParamTypeString:
                        s = ValueString;
                        break;
                    case SearchParamTypes.ParamTypeInt:
                        if (ValueInt > 0)
                            s = ValueInt.ToString();
                        break;
                    case SearchParamTypes.ParamTypeFloat:
                        if (ValueFloat > 0)
                        {
                            if (ParamFormat != null && !NoFormat)
                            {
                                if (ParamFormat.FORMAT_STRING != null && ParamFormat.FORMAT_STRING != "")
                                    s = ValueFloat.ToString(ParamFormat.FORMAT_STRING);
                                else
                                    s = ValueFloat.ToString();
                            }
                            else
                                s = ValueFloat.ToString();
                        }
                        break;
                    case SearchParamTypes.ParamTypeDate:
                        if (ValueDate > DateTime.MinValue)
                        {
                            if (ParamFormat != null && !NoFormat)
                            {
                                if (ParamFormat.FORMAT_STRING != null && ParamFormat.FORMAT_STRING != "")
                                    s = ValueDate.ToString(ParamFormat.FORMAT_STRING);
                                else
                                    s = ValueDate.ToString();

                            }
                            else
                                s = ValueDate.ToString();
                        }
                        else
                            s = "";
                        break;
                    case SearchParamTypes.ParamTypeImage:
                        if (IMAGE_ID != Guid.Empty)
                            s = IMAGE_ID.ToString();
                        else
                            s = SEARCH_PARAM_VALUE_ID.ToString();
                        break;
                    case SearchParamTypes.ParamTypeBool:
                        s = ValueBool.ToString();
                        break;

                    default:
                        s = "";
                        break;

                }

                if (ParamFormat != null && s != "" && !NoFormat)
                {
                    if (ParamFormat.OUT_TEMPLATE != null && ParamFormat.OUT_TEMPLATE != "")
                        s = String.Format(ParamFormat.OUT_TEMPLATE, s);
                }
                return s;
            }
            set
            {
                switch (ParamType)
                {
                    case SearchParamTypes.ParamTypeString:
                        ValueString = value;
                        break;
                    case SearchParamTypes.ParamTypeInt:
                        try
                        {
                            ValueInt = int.Parse(value);
                        }
                        catch { }
                        break;
                    case SearchParamTypes.ParamTypeFloat:
                        try
                        {
                            ValueFloat = double.Parse(value);
                        }
                        catch { }
                        break;
                    case SearchParamTypes.ParamTypeDate:
                        try
                        {
                            ValueDate = DateTime.Parse(value);
                        }
                        catch { ValueDate = DateTime.MinValue; }
                        break;
                    case SearchParamTypes.ParamTypeBool:
                        try
                        {
                            ValueBool = bool.Parse(value);
                        }
                        catch { }
                        break;
                }
            }
        }



        private void Init()
        {
            DefaultValues = new SearchParamValueCollection();
            ParamFormat = new SearchParamFormat();
            ValueDate = DateTime.MinValue;
        }
        public SearchParam()
        {
            Init();
        }
        public SearchParam(string _SEARCH_PARAM_NAME, SearchParamTypes _ParamType)
        {
            Init();
            SEARCH_PARAM_NAME = _SEARCH_PARAM_NAME;
            ParamType = _ParamType;
        }

        public SearchParam(string _SEARCH_PARAM_NAME, SearchParamTypes _ParamType, int _ParamValue)
        {
            Init();
            SEARCH_PARAM_NAME = _SEARCH_PARAM_NAME;
            ParamType = _ParamType;
            ValueInt = _ParamValue;
        }

        public SearchParam(string _SEARCH_PARAM_NAME, SearchParamTypes _ParamType, string _ParamValue)
        {
            Init();
            SEARCH_PARAM_NAME = _SEARCH_PARAM_NAME;
            ParamType = _ParamType;
            ValueString = _ParamValue;
        }
        
        public SearchParam(string _SEARCH_PARAM_NAME, SearchParamTypes _ParamType, double _ParamValue)
        {
            Init();
            SEARCH_PARAM_NAME = _SEARCH_PARAM_NAME;
            ParamType = _ParamType;
            ValueFloat = _ParamValue;
        }

        public SearchParam(string _SEARCH_PARAM_NAME, SearchParamTypes _ParamType, bool _ParamValue)
        {
            Init();
            SEARCH_PARAM_NAME = _SEARCH_PARAM_NAME;
            ParamType = _ParamType;
            ValueBool = _ParamValue;
        }

        public SearchParam(string _SEARCH_PARAM_NAME, SearchParamTypes _ParamType, DateTime _ParamValue)
        {
            Init();
            SEARCH_PARAM_NAME = _SEARCH_PARAM_NAME;
            ParamType = _ParamType;
            ValueDate = _ParamValue;
        }

    }

    /// <summary>
    /// Summary description for SearchParamFormat.
    /// </summary>
    [Serializable]
    public class SearchParamFormat
    {
        public string FORMAT_NAME;
        public int PARAM_LENGTH;
        public string FORMAT_STRING;
        public string VALIDATION_EXPR;
        public string OUT_TEMPLATE;

        public SearchParamFormat()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool IsValueValid(string _value)
        {

            return false;
        }
    }

    /// <summary>
    /// Colection for SearchParamValues.
    /// </summary>
    [Serializable]
    public class SearchParamValueCollection : CollectionBase
    {

        public SearchParamValue this[int index]
        {
            get { return ((SearchParamValue)List[index]); }
            set { List[index] = value; }
        }

        public int Add(SearchParamValue value)
        {
            return (List.Add(value));
        }

        public int IndexOf(SearchParamValue value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, SearchParamValue value)
        {
            List.Insert(index, value);
        }

        public void Remove(SearchParamValue value)
        {
            List.Remove(value);
        }

        public bool Contains(SearchParamValue value)
        {
            // If value is not of type SearchParamValue, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParamValue"))
                throw new ArgumentException("value must be of type SearchParamValue.", "value");
        }

        protected override void OnRemove(int index, Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParamValue"))
                throw new ArgumentException("value must be of type SearchParamValue.", "value");
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (newValue.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParamValue"))
                throw new ArgumentException("newValue must be of type SearchParamValue.", "newValue");
        }

        protected override void OnValidate(Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.SearchProcessor.SearchParamValue"))
                throw new ArgumentException("value must be of type SearchParamValue.");
        }

        /// <summary>
        /// For default Param values. For comboboxes
        /// </summary>
        [Serializable]
        public class SearchParamValue
        {
            public int ValueInt;
            public float ValueFloat;
            public string ValueString = "";

            public string VALUE_CAPTION;

            public bool IS_SELECTED = false;

            public SearchParamValue()
            {
            }
            public SearchParamValue(string _ValueString, string _VALUE_CAPTION, bool _IS_SELECTED)
            {
                ValueString = _ValueString;
                VALUE_CAPTION = _VALUE_CAPTION;
                IS_SELECTED = _IS_SELECTED;
            }
        }


    }

    /// <summary>
    /// Colection for PropertyImages.
    /// </summary>
    [Serializable]
    public class PropertyImageCollection : CollectionBase
    {

        public PropertyImage this[int index]
        {
            get { return ((PropertyImage)List[index]); }
            set { List[index] = value; }
        }

        public int Add(PropertyImage value)
        {
            return (List.Add(value));
        }

        public int IndexOf(PropertyImage value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, PropertyImage value)
        {
            List.Insert(index, value);
        }

        public void Remove(PropertyImage value)
        {
            List.Remove(value);
        }

        public bool Contains(PropertyImage value)
        {
            // If value is not of type PropertyImage, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.BusinessLogicLayer.PropertyImage"))
                throw new ArgumentException("value must be of type PropertyImage.", "value");
        }

        protected override void OnRemove(int index, Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.BusinessLogicLayer.PropertyImage"))
                throw new ArgumentException("value must be of type PropertyImage.", "value");
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (newValue.GetType() != Type.GetType("AdminClients.BusinessLogicLayer.PropertyImage"))
                throw new ArgumentException("newValue must be of type PropertyImage.", "newValue");
        }

        protected override void OnValidate(Object value)
        {
            if (value.GetType() != Type.GetType("AdminClients.BusinessLogicLayer.PropertyImage"))
                throw new ArgumentException("value must be of type PropertyImage.");
        }

    }

    /// <summary>
    ///Property Image class.
    /// </summary>
    [Serializable]
    public class PropertyImage
    {
        public byte[] Image;
        public string ContentType;
        public string ImageDescription;
        private Guid _IMAGE_ID;
        public Guid IMAGE_ID
        {
            get { return _IMAGE_ID; }
            set { _IMAGE_ID = value; }
        }

        public int IMAGE_NO;

        /// <summary>
        /// Creates empty image
        /// </summary>
        public PropertyImage()
        {
            ImageDescription = " ";
        }

        /// <summary>
        /// Creates and fill image
        /// </summary>
        /// <param name="_Image"></param>
        /// <param name="_ContentType"></param>
        /// <param name="_ImageDecription"></param>
        public PropertyImage(byte[] _Image, string _ContentType, string _ImageDecription, int _IMAGE_NO)
        {
            Image = _Image;
            ContentType = _ContentType;
            ImageDescription = _ImageDecription;
            IMAGE_NO = _IMAGE_NO;
        }

        /// <summary>
        /// Load picture from BD
        /// </summary>
        /// <param name="_IMAGE_ID"></param>
        //		public PropertyImage(string _IMAGE_ID)
        //		{
        //			SqlConnection conn = new SqlConnection(Web.StaticFunctions.GetSetting("SqlConnectionString","MISSING CONNECTION STRING"));
        //			SqlDataReader rdr=null ;
        //			SqlCommand cmd=null ;
        //
        //			IMAGE_ID = _IMAGE_ID;
        //
        //			try
        //			{
        //				//initiallise so that later we can test if length > 1
        //				Image = new byte[' '];
        //
        //				conn.Open();
        //				cmd = new SqlCommand();
        //				cmd.Connection = conn;
        //
        //				cmd.CommandText ="select ImgBin, ImgContentType from tblPImages where IMAGE_ID = '" + _IMAGE_ID+"'";
        //				rdr = cmd.ExecuteReader();
        //				if(rdr.HasRows)
        //				{
        //					rdr.Read();
        //					//put the image in the byteArray
        //					Image = (byte[])rdr["ImgBin"];
        //					ContentType = rdr.GetString(1);
        //				}
        //			}
        //			finally
        //			{
        //				try
        //				{
        //					rdr.Close();
        //					conn.Close();
        //				}
        //					//ignore any error while closing.
        //				catch{}
        //			}
        //		}

        /// <summary>
        /// Save picture to DB
        /// </summary>
        /// <param name="FORSALE_ID">Fosr Sale property ID</param>
        public void SaveToDB(string _FORSALE_ID)
        {
            //			SqlConnection nwindConn = new SqlConnection(Web.StaticFunctions.GetSetting("SqlConnectionString","MISSING CONNECTION STRING"));
            //
            //			SqlCommand salesCMD = new SqlCommand("propertyinfo.p_Add_ForSaleImage", nwindConn);
            //			salesCMD.CommandType = CommandType.StoredProcedure;  
            //
            //
            //			SqlParameter myParm = salesCMD.Parameters.Add("@FORSALE_ID", SqlDbType.VarChar, 50);
            //			myParm.Value = _FORSALE_ID;
            //
            //			//				myParm = salesCMD.Parameters.Add("@IMAGE_ID", SqlDbType.VarChar, 50);
            //			//				myParm.Value = null;
            //
            //			myParm = salesCMD.Parameters.Add("@ImgBin", SqlDbType.Image,Image.Length);
            //			myParm.Value = Image;
            //
            //			myParm = salesCMD.Parameters.Add("@ImgThumb", Image.Length);
            //			myParm.Value = Image;
            //
            //			myParm = salesCMD.Parameters.Add("@ImgContentType", SqlDbType.VarChar, 50);
            //			myParm.Value = ContentType;
            //
            //			myParm = salesCMD.Parameters.Add("@ImgDescr", SqlDbType.VarChar, 5000);
            //			myParm.Value = ImageDescription;
            //
            //			nwindConn.Open();
            //
            //			salesCMD.ExecuteReader();
            //
            //			nwindConn.Close();
        }
    }




    #endregion

    [Serializable]
    public class ClientSite
    {
        public ClientSite()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Get SITE from DB
        /// </summary>
        /// <param name="_SITE_ID"></param>
        public ClientSite(Guid _SITE_ID)
        {

        }

        public ClientSite(Guid _SITE_ID, string _SITE_NAME, string _FULL_SITE_NAME)
        {
            SITE_ID = _SITE_ID;
            SITE_NAME = _SITE_NAME;
            FULL_SITE_NAME = _FULL_SITE_NAME;
        }


        public Guid SITE_ID;
        public Guid ADMIN_ID;
        public string ADMIN_EMAIL;
        public string FULL_SITE_NAME;
        public string SITE_NAME;
        public int SITE_ROLE = 0;

        /// <summary>
        /// Current admin's rights to this Site
        /// 0 - Denied
        /// 1 - Read 
        /// 2 - Write
        /// </summary>
        public int Right;

        public string notification_email;

        public override bool Equals(object obj)
        {
            return (obj is ClientSite ? this.SITE_ID.Equals(((ClientSite)obj).SITE_ID) : false);
        }
    }

    private class FieldMofification
    {
        public object OldValue;
        public object NewValue;
        public readonly string Name;
        public readonly bool IsAdditional;

        public bool IsChanged
        {
            get
            {
                return (this.OldValue != null ? !this.OldValue.Equals(this.NewValue) : this.NewValue != null && this.NewValue.ToString().Trim().Length > 0);
            }
        }

        public FieldMofification(string name, object oldValue, object newValue, bool isAdditional)
        {
            this.Name = name;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.IsAdditional = isAdditional;
        }
    }


}