using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSWeb.Models.Consumable
{
    public class Users
    {

        #region Properties

        #region Borrower Information

        // Borrower Information
        public string BorrowerFirstName { get; set; } 
        public string BorrowerLastName { get; set; } 
        public string BorrowerEmail { get; set; } 
        public string BorrowerPassword { get; set; } 
        public string BorrowerAddress { get; set; } 
        public string BorrowerCity { get; set; } 
        public string BorrowerState { get; set; }
        public string BorrowerZip { get; set; }
        public string BorrowerWorkPhone { get; set; }
        public string BorrowerPhone { get; set; }
        public string BorrowerCellPhone { get; set; }
        public string BorrowerBestTimeToCall { get; set; }
        public string BorrowerNickName { get; set; }
        public string BorrowerDoNotMail { get; set; }
        public string BorrowerDoNotEMail { get; set; }
        public string BorrowerDoNotCall { get; set; }
        public string BorrowerParticipationSold { get; set; }
        public string BorrowerEthnicity { get; set; }
        public string BorrowerRace { get; set; }
        public string BorrowerSex { get; set; }
        public string BorrowerMartialStatus { get; set; }
        public string BorrowerOwnership { get; set; }
        // End
        // Borrower Employer Information
        public string BorrowerEmployerName { get; set; }
        public string BorrowerEmployerAddress { get; set; }
        public string BorrowerEmployerCity { get; set; }
        public string BorrowerEmployerState { get; set; }
        public string BorrowerEmployerZip { get; set; }
        public string BorrowerEmployerPhone { get; set; }
        public string BorrowerEmployerTitle { get; set; }
        public bool BorrowerSelfEmployed { get; set; }
        public int BorrowerYearsWithEmloyer { get; set; }
        public int BorrowerMonthsWithEmloyer { get; set; }
        public string BorrowerBaseEmploymentIncome { get; set; }
        // End
        // Borrower Employer Previous Information
        public string BorrowerPrvEmployerName { get; set; }
        public string BorrowerPrvEmployerAddress { get; set; }
        public string BorrowerPrvEmployerCity { get; set; }
        public string BorrowerPrvEmployerState { get; set; }
        public string BorrowerPrvEmployerZip { get; set; }
        public string BorrowerPrvEmployerPhone { get; set; }
        public string BorrowerPrvEmployerTitle { get; set; }
        public bool BorrowerPrvSelfEmployed { get; set; }
        public int BorrowerPrvYearsWithEmloyer { get; set; }
        public int BorrowerPrvMonthsWithEmloyer { get; set; }
        public string BorrowerPrBaseEmploymentIncome { get; set; }
        // End
        // Borrower Mailing  Information
        public string BorrowerMailingAddress { get; set; }
        public string BorrowerMailingCity { get; set; }
        public string BorrowerMailingState { get; set; }
        public string BorrowerMailingZip { get; set; }
        // End
        // Borrower Previous Information
        public string BorrowerPrevAddress { get; set; }
        public string BorrowerPrevCity { get; set; }
        public string BorrowerPrevState { get; set; }
        public string BorrowerPrevZip { get; set; }
        public string BorrowerPrevAddrRent { get; set; }
        public string BorrowerPrevAddrOwn { get; set; }
        public string BorrowerPrevAddrNYrs { get; set; }
        public string BorrowerPrevAddrNMs { get; set; }

        public string BorrowerOvertime { get; set; }
        public string BorrowerBonuses { get; set; }
        public string BorrowerCommissions { get; set; }
        public string BorrowerDividends { get; set; }
        public string BorrowerNetRentalIncome { get; set; }
        public string BorrowerSocNum { get; set; }
        public string BorrowerDOB { get; set; }
        public string BorrowerCreditScore { get; set; }
        public string BorrowerTUCreditScore { get; set; }
        public string BorrowerEQCreditScore { get; set; }
        public string BorrowerTitleInsuranceCompany { get; set; }

        // Borrower Assets Information
        public string BorrowerAssetsAmount { get; set; }
        public string BorrowerAssetsType { get; set; }
        public string BorrowerAssetsSeasoning { get; set; }
        public string BorrowerAssetNameOfCurBank { get; set; }
        public string BorrowerAssetCheckingBalance { get; set; }
        public string BorrowerAssetSavingsBalance { get; set; }
        public string BorrowerAssetNameOfFirm { get; set; }
        public string BorrowerAssetBalanceOfOtherInvestments { get; set; }
        public string BorrowerAssetYearsOfSchool { get; set; }
        public string BorrowerAssetCountFromDB { get; set; }

        public string BorrowerAssetsAccT1 { get; set; }
        public string BorrowerAssetsInsT1 { get; set; }
        public string BorrowerAssetsAccN1 { get; set; }
        public int BorrowerAssetsTotal1 { get; set; }
        public string BorrowerAssets1Street { get; set; }
        public string BorrowerAssets1City { get; set; }
        public string BorrowerAssets1State { get; set; }
        public string BorrowerAssets1Zip { get; set; }
        public int BorrowerAssets1NumShares { get; set; }
        public int BorrowerAssets1FaceValue { get; set; }
        public int BorrowerAssets1MakeModel { get; set; }
        public int BorrowerAssets1Year { get; set; }
        public string BorrowerAssets1HeldBy { get; set; }
        public string BorrowerAssets1OtherDesc { get; set; }
        public string BorrowerAssetsAccT2 { get; set; }
        public string BorrowerAssetsInsT2 { get; set; }
        public string BorrowerAssetsAccN2 { get; set; }
        public int BorrowerAssetsTotal2 { get; set; }
        public string BorrowerAssets2Street { get; set; }
        public string BorrowerAssets2City { get; set; }
        public string BorrowerAssets2State { get; set; }
        public string BorrowerAssets2Zip { get; set; }
        public int BorrowerAssets2NumShares { get; set; }
        public int BorrowerAssets2FaceValue { get; set; }
        public int BorrowerAssets2MakeModel { get; set; }
        public int BorrowerAssets2Year { get; set; }
        public string BorrowerAssets2HeldBy { get; set; }
        public string BorrowerAssets2OtherDesc { get; set; }
        public string BorrowerAssetsAccT3 { get; set; }
        public string BorrowerAssetsInsT3 { get; set; }
        public string BorrowerAssetsAccN3 { get; set; }
        public int BorrowerAssetsTotal3 { get; set; }
        public string BorrowerAssets3Street { get; set; }
        public string BorrowerAssets3City { get; set; }
        public string BorrowerAssets3State { get; set; }
        public string BorrowerAssets3Zip { get; set; }
        public int BorrowerAssets3NumShares { get; set; }
        public int BorrowerAssets3FaceValue { get; set; }
        public int BorrowerAssets3MakeModel { get; set; }
        public int BorrowerAssets3Year { get; set; }
        public string BorrowerAssets3HeldBy { get; set; }
        public string BorrowerAssets3OtherDesc { get; set; }
        public string BorrowerAssetsAccT4 { get; set; }
        public string BorrowerAssetsInsT4 { get; set; }
        public string BorrowerAssetsAccN4 { get; set; }
        public int BorrowerAssetsTotal4 { get; set; }
        public string BorrowerAssets4Street { get; set; }
        public string BorrowerAssets4City { get; set; }
        public string BorrowerAssets4State { get; set; }
        public string BorrowerAssets4Zip { get; set; }
        public int BorrowerAssets4NumShares { get; set; }
        public int BorrowerAssets4FaceValue { get; set; }
        public int BorrowerAssets4MakeModel { get; set; }
        public int BorrowerAssets4Year { get; set; }
        public string BorrowerAssets4HeldBy { get; set; }
        public string BorrowerAssets4OtherDesc { get; set; }

        public string BorrowerAssetsOtherDescr1 { get; set; }
        public string BorrowerAssetsOtherVal1 { get; set; }
        public string BorrowerAssetsOtherDescr2 { get; set; }
        public string BorrowerAssetsOtherVal2 { get; set; }
        public string BorrowerAssetsOtherDescr3 { get; set; }
        public string BorrowerAssetsOtherVal3 { get; set; }
        public string BorrowerAssetsOtherDescr4 { get; set; }
        public string BorrowerAssetsOtherVal4 { get; set; }

        //public string AssetType1 { get; set; }
        //public string AssetInstitution1 { get; set; }
        //public string AssetAccount1 { get; set; }
        //public string AssetBalance1 { get; set; }
        //public string AssetType2 { get; set; }
        //public string AssetInstitution2 { get; set; }
        //public string AssetAccount2 { get; set; }
        //public string AssetBalance2 { get; set; }
        //public string AssetType3 { get; set; }
        //public string AssetInstitution3 { get; set; }
        //public string AssetAccount3 { get; set; }
        //public string AssetBalance3 { get; set; }
        //public string AssetType4 { get; set; }
        //public string AssetInstitution4 { get; set; }
        //public string AssetAccount4 { get; set; }
        //public string AssetBalance4 { get; set; }
        // End 

        public string BorrowerFullName { get; set; }
        public string BorrowerMiddleName { get; set; }
        public string BorrowerSpouseFullName { get; set; }
        public string BorrowerBankruptcy { get; set; }
        public string BorrowerGrossMonthlyIncome { get; set; }
        public string BorrowerGrossIncome { get; set; }

        public int BorrowerYearsInBusiness { get; set; }
        public string BorrowerBusinessGeneralLocation { get; set; }
        public string BorrowerTypeOfBusiness { get; set; }
        public string BorrowerAvailableFunds { get; set; }
        public string BorrowerIndustryOfInterest { get; set; }
        public string BorrowerGeographicAreas { get; set; }
        public int BorrowerDependentsNum { get; set; }
        public int BorrowerDependentsAges { get; set; }
        public string BorrowerIncome { get; set; }
        public string BorrowerMembershipID { get; set; }
        public string BorrowerStreetAddress2 { get; set; }
        public string BorrowerBestWayToContact { get; set; }
        public string BorrowerBestTimeToContact { get; set; }

        public string BorrowerRent { get; set; }
        public string BorrowerOwn { get; set; }
        public int BorrowerTimeAtResidenceYears { get; set; }
        public int BorrowerTimeAtResidenceMonths { get; set; }

        // Borrower Cash Information
        public string BorrowerCashDepositDescr1 { get; set; }
        public string BorrowerCashDepositVal1 { get; set; }
        public string BorrowerCashDepositDescr2 { get; set; }
        public string BorrowerCashDepositVal2 { get; set; }
        // End
        //Borrower Stock Infomation
        public string BorrowerStocksBondsCompNameAccount1 { get; set; }
        public string BorrowerStocksBondsVal1 { get; set; }
        public string BorrowerStocksBondsCompNameAccount2 { get; set; }
        public string BorrowerStocksBondsVal2 { get; set; }
        public string BorrowerStocksBondsCompNameAccount3 { get; set; }
        public string BorrowerStocksBondsVal3 { get; set; }
        //End 

        public string BorrowerLInsuranceFaceAmount { get; set; }
        public string BorrowerLInsuranceMarketValue { get; set; }
        public string BorrowerVestedInterestInRF { get; set; }
        public string BorrowerNetWorthOfBusinessOwned { get; set; }
        public string BorrowerAutoMakeAndYear1 { get; set; }
        public string BorrowerAutoMakeAndYear2 { get; set; }
        public string BorrowerAutoMakeAndYear3 { get; set; }
        public string BorrowerAutoVal1 { get; set; }
        public string BorrowerAutoVal2 { get; set; }
        public string BorrowerAutoVal3 { get; set; }

        // Borrower Lia Information
        public string BorrowerLiaCompanyName1 { get; set; }
        public string BorrowerLiaType1 { get; set; }
        public string BorrowerLiaBalance1 { get; set; }
        public string BorrowerLiaPayment1 { get; set; }
        public string BorrowerLiaMosLeft1 { get; set; }
        public string BorrowerLiaPaidOff1 { get; set; }
        public string BorrowerLiaCompanyName2 { get; set; }
        public string BorrowerLiaType2 { get; set; }
        public string BorrowerLiaBalance2 { get; set; }
        public string BorrowerLiaPayment2 { get; set; }
        public string BorrowerLiaMosLeft2 { get; set; }
        public string BorrowerLiaPaidOff2 { get; set; }
        public string BorrowerLiaCompanyName3 { get; set; }
        public string BorrowerLiaType3 { get; set; }
        public string BorrowerLiaBalance3 { get; set; }
        public string BorrowerLiaPayment3 { get; set; }
        public string BorrowerLiaMosLeft3 { get; set; }
        public string BorrowerLiaPaidOff3 { get; set; }
        public string BorrowerLiaCompanyName4 { get; set; }
        public string BorrowerLiaType4 { get; set; }
        public string BorrowerLiaBalance4 { get; set; }
        public string BorrowerLiaPayment4 { get; set; }
        public string BorrowerLiaMosLeft4 { get; set; }
        public string BorrowerLiaPaidOff4 { get; set; }
        public string BorrowerLiaCompanyName5 { get; set; }
        public string BorrowerLiaType5 { get; set; }
        public string BorrowerLiaBalance5 { get; set; }
        public string BorrowerLiaPayment5 { get; set; }
        public string BorrowerLiaMosLeft5 { get; set; }
        public string BorrowerLiaPaidOff5 { get; set; }
        public string BorrowerLiaCompanyName6 { get; set; }
        public string BorrowerLiaType6 { get; set; }
        public string BorrowerLiaBalance6 { get; set; }
        public string BorrowerLiaPayment6 { get; set; }
        public string BorrowerLiaMosLeft6 { get; set; }
        public string BorrowerLiaPaidOff6 { get; set; }
        public string BorrowerLiaCompanyName7 { get; set; }
        public string BorrowerLiaType7 { get; set; }
        public string BorrowerLiaBalance7 { get; set; }
        public string BorrowerLiaPayment7 { get; set; }
        public string BorrowerLiaMosLeft7 { get; set; }
        public string BorrowerLiaPaidOff7 { get; set; }
        // End
        // Borrower Monthly Housing Expenses Information
        public string BorrowerMHERent { get; set; }
        public string BorrowerMHE1stMrtgP { get; set; }
        public string BorrowerMHEOthrMrtgP { get; set; }
        public string BorrowerMHEOthrMrtgPprop { get; set; }
        public string BorrowerMHEHazIns { get; set; }
        public string BorrowerMHEHazInsProp { get; set; }
        public string BorrowerMHERETaxes { get; set; }
        public string BorrowerMHERETaxesProp { get; set; }
        public string BorrowerMHEMtgIns { get; set; }
        public string BorrowerMHEMtgInsProp { get; set; }
        public string BorrowerMHEHOADues { get; set; }
        public string BorrowerMHEHOADuesProp { get; set; }
        public string BorrowerMHEOther { get; set; }
        public string BorrowerCompany { get; set; }



        #endregion
       
        #region CoBorrower Information
        public string CoBorrowerFirstName { get; set; }
        public string CoBorrowerLastName { get; set; }
        public string CoBorrowerEmail { get; set; }
        public string CoBorrowerAddress { get; set; }
        public string CoBorrowerCity { get; set; }
        public string CoBorrowerState { get; set; }
        public string CoBorrowerZip { get; set; }
        public string CoBorrowerWorkPhone { get; set; }
        public string CoBorrowerHomePhone { get; set; }
        public string CoBorrowerCellPhone { get; set; }
        public string CoBorrowerFax { get; set; }
        public string CoBorrowerBestTimeToCall { get; set; }
        public string CoBorrowerEthnicity { get; set; }
        public string CoBorrowerRace { get; set; }
        public string CoBorrowerSex { get; set; }
        public string CoBorrowerMartialStatus { get; set; }
        public string CoBorrowerOwnership { get; set; }
        public string CoBorrowerEmployerName { get; set; }
        public string CoBorrowerEmployerAddress { get; set; }
        public string CoBorrowerEmployerCity { get; set; }
        public string CoBorrowerEmployerState { get; set; }
        public string CoBorrowerEmployerZip { get; set; }
        public string CoBorrowerEmployerPhone { get; set; }
        public string CoBorrowerEmployerTitle { get; set; }
        public string CoBorrowerSelfEmployed { get; set; }
        public string CoBorrowerMonthsWithEmloyer { get; set; }
        public string CoBorrowerBaseEmploymentInCoBorrowerme { get; set; }
        // Co Borrower Mailing Inforamtion
        public string CoBorrowerMailingAddress { get; set; }
        public string CoBorrowerMailingCity { get; set; }
        public string CoBorrowerMailingState { get; set; }
        public string CoBorrowerMailingZip { get; set; }
        // End
        public string CoBorrowerOvertime { get; set; }
        public string CoBorrowerBonuses { get; set; }
        public string CoBorrowerCommissions { get; set; }
        public string CoBorrowerDividends { get; set; }
        public string CoBorrowerNetRentalIncome { get; set; }
        public string CoBorrowerBorrowerGrossMonthlyIncome { get; set; }
        public string CoBorrowerSocNum { get; set; }
        public string CoBorrowerDOB { get; set; }
        public string CoBorrowerCreditScore { get; set; }
        public string CoBorrowerTUCreditScore { get; set; }
        public string CoBorrowerEQCreditScore { get; set; }
        public string CoBorrowerBorrowerTitle { get; set; }
        public string CoBorrowerPreviousEmployment { get; set; }

        public string CoBorrowerEmployerCSZ { get; set; }
        public string CoBorrowerYearsWithEmloyer { get; set; }
        // Co Borrower Assets Information
        public string CoBorrowerAssetsAmount { get; set; }
        public string CoBorrowerAssetsType { get; set; }
        public string CoBorrowerAssetsSeasoning { get; set; }
        public string CoBorrowerAssetNameOfCurBank { get; set; }
        public string CoBorrowerAssetCheckingBalance { get; set; }
        public string CoBorrowerAssetSavingsBalance { get; set; }
        public string CoBorrowerAssetNameOfFirm { get; set; }
        public string CoBorrowerAssetBalanceOfOtherInvestments { get; set; }
        public string CoBorrowerAssetYearsOfSchool { get; set; }
        //End
        public string CoBorrowerFullName { get; set; }
        public string CoBorrowerMiddleName { get; set; }
        public string CoBorrowerDependentsNum { get; set; }
        public string CoBorrowerDependentsAges { get; set; }
        public string CoBorrowerBankruptcy { get; set; }
        public string CoBorrowerIncome { get; set; }

        public string CoBorrowerRent { get; set; }
        public string CoBorrowerOwn { get; set; }
        public string CoBorrowerTimeAtResidenceYears { get; set; }
        public string CoBorrowerTimeAtResidenceMonths { get; set; }
        public string CoBorrowerPrevAddrRent { get; set; }
        public string CoBorrowerPrevAddrOwn { get; set; }
        public string CoBorrowerPrevAddrNYrs { get; set; }
        public string CoBorrowerPrevAddrNMs { get; set; }
        public string CoBorrowerPrevAddrNYrs { get; set; }
        public string CoBorrowerYearsInBusiness { get; set; }


        public string CoBorrowerBuyerID { get; set; }
        public string CoBorrowerBoAssetYearsOfSchool { get; set; } 
        public string CoBorrowerPager { get; set; }
        public string CoBorrowerTimeAtResidenceYears { get; set; }
        public string CoBorrowerTimeAtResidenceMonths { get; set; }
        public string CoBorrowerTypeOfBusiness { get; set; }
        public string CoBorrowerApply { get; set; }

        #endregion

        #region Loan Information

        public string LoanNumber { get; set; }
        public string loanTypeNotPurpose { get; set; }
        public string LoanPurposeRefiType { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanProgram { get; set; }
        public string LoanPurposeOther { get; set; }
        public string LoanType { get; set; }
        public string LoanTypeOther { get; set; }
        public string LoanMonthlyPayments { get; set; }
        public string LoanAmountDesired { get; set; } // DesiredLoanAmount
        public string LoanDownPayment { get; set; }
        public string LoanOwnerOccupied { get; set; }
        public string LoanOwnerOccupiedFlag { get; set; }
        public string LoanCurrentLoanToValue { get; set; }
        public string LoanMortgageDate { get; set; }
        public string LoanMargin { get; set; }
        public string LoanIndex { get; set; }
        
        public string LoanMortgageAmount { get; set; }
        public string LoanFirstMortgageLoanRateType { get; set; }
        public string LoanCurrentHomeValue { get; set; }
        public string LoanFirstMortgageInterestRate { get; set; }
        public string LoanFirstMortgageInterestRateInitialChangeDate { get; set; }
        public string LoanFirstMortgageInterestRateNextChangeDate { get; set; }
        public string LoanFirstMortgageLTV { get; set; }
        public string LoanFirstMortgageLenderName { get; set; }
        public string LoanFirstMortgagePrePaymentPenaltyExpirationDate { get; set; }

        public string LoanJunior1MortgageAmount { get; set; }
        public string LoanJunior1MortgageAssignedDate { get; set; }
        public string LoanLengthOfLoanInYears { get; set; }
        public string LoanOriginalNoteRate { get; set; }
        public string LoanFirstPaymentDate { get; set; }
        public string LoanDateClosed { get; set; }

        public string LoanFICO { get; set; }
        public string LoanSalesPrice { get; set; }
        public string LoanAppraisedValue { get; set; }
        public string LoanIncludeRemodelingCost { get; set; }
        public string LoanCurrentMonthlyPayments { get; set; }
        public string LoanCurrentEscrow { get; set; } 
        public string LoanApprovalDate { get; set; }
        public string LoanMortgageBalancePrincipal { get; set; }
        public string LoanMortgageBalanceFirst { get; set; }
        public string LoanMortgageBalanceSecond { get; set; }
        public string LoanInterestRateSecond { get; set; }
        public string LoanRefinancePurpose { get; set; }
        public string LoanTimeframe { get; set; }
        public string LoanIntRate { get; set; }
        public string LoanNoteDueDate { get; set; }
        public string LoanLTV { get; set; }
        public string LoanDesiredRate { get; set; }
        public string LoanCDFIRevenues { get; set; }
        public string LoanMICertNumber { get; set; }
        public string LoanMIType { get; set; }
        public string LoanMIAmount { get; set; }
        public string LoanMIPremiumType { get; set; }
        public string LoanOccupancy { get; set; }
        public string LoanOriginalToValue { get; set; }
        public string LoanGSEType { get; set; }
        public string LoanPrePenalty { get; set; }
        public string LoanAdditionalCash { get; set; }
        public string LoanNumberOfMortgageLates { get; set; }
        public string LoanVAStatus { get; set; }
        public string LoanProgramDesiredFirst { get; set; }
        public string LoanRateDesiredFirst { get; set; }
        public string LoanPointDesiredFirst { get; set; }
        public string LoanProgramDesiredSecond { get; set; }
        public string LoanRateDesiredSecond { get; set; }
        public string LoanPointDesiredSecond { get; set; }
        public string LoanProgramDesiredThird { get; set; }
        public string LoanRateDesiredThird { get; set; }
        public string LoanPointDesiredThird { get; set; }
        public string LoanLoanDTI { get; set; }
        public string LoanDestinatiryEscrow { get; set; }
        public string LoanIsYourMortgageCurrent { get; set; }
        public string LoanHaveASecondMortgage { get; set; }
        public string LoanTerm { get; set; }
        public string LoanDueIn { get; set; }

        public string LoanAmorTypeFixedRate { get; set; }
        public string LoanAmorTypeGPMRate { get; set; }
        public string LoanAmorTypeGPMRatePercent { get; set; }
        public string LoanAmorTypeGPMRateYears { get; set; }
        public string LoanAmorTypeARM { get; set; }
        public string LoanAmorTypeARMDescr { get; set; }
        public string LoanAmorTypeOther { get; set; }
        public string LoanAmorTypeOtherDescr { get; set; }

        public string LoanQualRate { get; set; }
        public string LoanUnDiscountedRate { get; set; }
        public string LoanInterestOnlyNumOfMonths { get; set; }
        public string LoanQualifyUsingPandI { get; set; }

        public string LoanPurposeType { get; set; }

        #endregion
        
        #region Property Information
        public string PropertyAdress { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyCounty { get; set; }

        public string PropertyWillBeIsPrimary { get; set; }
        public string PropertyWillBeIsSecondary { get; set; }
        public string PropertyWillGrossRnt { get; set; }
        public string PropertyWillBeIsInvestment { get; set; }
        public string PropertyWillBeOccupRate { get; set; }
        public string PropertyType { get; set; }
        public string PropertyRealStatus { get; set; }
        public string PropertyTaxAmount { get; set; }
        public string PropertyYearBuilt { get; set; }
        public string PropertyLastRenovated { get; set; }
        public string PropertyBuildingSizeSqFt { get; set; }
        public string PropertyLandSizeAcres { get; set; }
        public string PropertyEstimatedCurrentMarketValueofCollateralProperty { get; set; }
        public string PropertyDatePropertyPurchased { get; set; }
        public string PropertyPurchasePrice { get; set; }
        public string PropertyHowPropOwned { get; set; }
        public string PropertyBuildingStructure { get; set; }
        public int PropertyNumOfUnits { get; set; }
        public string PropertyGrossAnnualRents { get; set; }
        public string PropertyCurrentVacancy { get; set; }
        public string PropertyAverageHistoricalVacancy { get; set; }
        public string PropertyNetRentableArea { get; set; }
        public string PropertyPROJECTEDGrossRent { get; set; }
        public string PropertyExpensesExcludingOperatingIncome { get; set; }
        public string PropertyPREVIOUSYEARGrossRent { get; set; }
        public string PropertyExpensesIncludingOperatingIncome { get; set; }

        public string PropertyLienPositionIsFirst { get; set; }
        public string PropertyLienPositionIsSecond { get; set; }
        public string PropertyLienPosition { get; set; }
        public string PropertyAdditionalLiensOnProperty { get; set; }
        public string PropertyHomeOwnersInsurance { get; set; }
        public string PropertyMortgageLienAmounts { get; set; }
        public string PropertyRealEstateMortgagePayments  { get; set; }

        public string PropertyZip { get; set; }
        public string PropertyWillBe { get; set; }
        public string PropertyHomeValuePrice { get; set; }
        public string PropertyStyle { get; set; }
        public string PropertyArea { get; set; }
        public int PropertyUnitNumber { get; set; }
        public string PropertyCarrierRoute { get; set; }


        #endregion

        #region Declaration
        //Borrower
        public string DeclarationBoA { get; set; }
        public string DeclarationBoB { get; set; }
        public string DeclarationBoC { get; set; }
        public string DeclarationBoD { get; set; }
        public string DeclarationBoE { get; set; }
        public string DeclarationBoF { get; set; }
        public string DeclarationBoG { get; set; }
        public string DeclarationHBo { get; set; }
        public string DeclarationBoI { get; set; }
        public string DeclarationBoJ { get; set; }
        public string DeclarationBoK { get; set; }
        public string DeclarationBoL { get; set; }
        public string DeclarationBoM { get; set; }
        public string DeclarationBo1 { get; set; }
        public string DeclarationBo2 { get; set; }
        //Co-Borrewer
        public string DeclarationCoBoA { get; set; }
        public string DeclarationCoBoB { get; set; }
        public string DeclarationCoBoC { get; set; }
        public string DeclarationCoBoD { get; set; }
        public string DeclarationCoBoE { get; set; }
        public string DeclarationCoBoF { get; set; }
        public string DeclarationCoBoG { get; set; }
        public string DeclarationCoBoH { get; set; }
        public string DeclarationCoBoI { get; set; }
        public string DeclarationCoBoJ { get; set; }
        public string DeclarationCoBoK { get; set; }
        public string DeclarationCoBoL { get; set; }
        public string DeclarationCoBoM { get; set; }
        public string DeclarationCoBo1 { get; set; }
        public string DeclarationCoBo2 { get; set; }
        #endregion

        #region REAL

        public string RealAddress1 { get; set; }
        public string RealPropertyCity1 { get; set; }
        public string RealPropertyState1 { get; set; }
        public string RealPropertyZip1 { get; set; }
        public string RealStatus1 { get; set; }
        public string RealType1 { get; set; }
        public string RealMarkeTValue1 { get; set; }
        public string RealMortgage1 { get; set; }
        public string RealMortpay1 { get; set; }
        public string RealMONTHPAY1 { get; set; }
        public string RealAddress2 { get; set; }
        public string RealPropertyCity2 { get; set; }
        public string RealPropertyState2 { get; set; }
        public string RealPropertyZip2 { get; set; }
        public string RealStatus2 { get; set; }
        public string RealType2 { get; set; }
        public string RealMarkeTValue2 { get; set; }
        public string RealMortgage2 { get; set; }
        public string RealMortpay2 { get; set; }
        public string RealMONTHPAY2 { get; set; }
        public string RealAddress3 { get; set; }
        public string RealPropertyCity3 { get; set; }
        public string RealPropertyState3 { get; set; }
        public string RealPropertyZip3 { get; set; }
        public string RealStatus3 { get; set; }
        public string RealType3 { get; set; }
        public string RealMarkeTValue3 { get; set; }
        public string RealMortgage3 { get; set; }
        public string RealMortpay3 { get; set; }
        public string RealMONTHPAY3 { get; set; }


        #endregion

        #region Step 1

        public Guid SiteID { get; set; }
        public string LeadSource { get; set; }
        public string AgentID { get; set; }

        #endregion

        #region Step 2

        public string SalesDetailsFoundAHome { get; set; }
        public string PurchaseHomePrice { get; set; }

        #endregion

        #region Step 3

        public int AssetYearsOfSchool { get; set; }
        public string TimeAtResidenceYears { get; set; }
        public string TimeAtResidenceMonths { get; set; }

       public string PreferredLanguage { get; set; }

        #endregion

        #region Step 11
        //Authorization
        public string CreditCheckAuthorization { get; set; }


        #endregion

        #region Other Information
        public string notes { get; set; }
        public string emailStatus { get; set; }
        public DateTime REGISTERED_DATE { get; set; }

        public int LastChangeDate { get; set; }
        public string BorrowerPager { get; set; }


        public string PreviousEmployment { get; set; }
        public int TotalIncome { get; set; }

        public string BuyerID { get; set; }
        public string MailCareOfName { get; set; }
        public string MailStreetAddress{ get; set; }
        public string MailCity { get; set; }
        public string MailState { get; set; }
        public string MailZip { get; set; }
        public string UnitDesignator { get; set; }
        public string MailUnitNumber { get; set; }
        public string Status { get; set; }
        public string NextStep { get; set; }
        public string NextStepDate { get; set; }
        public string EstimatedRevenue { get; set; }
        public string Probability { get; set; }
        public string EstimatedClosingDate { get; set; }

        public string Lender { get; set; }
        public string OfferCode { get; set; }
        public string TransactionType { get; set; }
        public string Relationshiptotheproperty { get; set; }
        public string HomeType { get; set; }
        public string Currentinterest { get; set; }
        public string Currentinterest2nd { get; set; }
        public string Balance { get; set; }
        public string Creditrating { get; set; }
        public string BorrowerOccupationalStatus { get; set; }
        public string TrackingID { get; set; }
        public string ListingDate { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }

        public string Garage { get; set; }
        public string YearBuilt { get; set; }
        public string Acreage { get; set; }
        public string Notes { get; set; }
        public string RID { get; set; }
        public string FIPS { get; set; }
        public string PIN { get; set; }
        public string LandUse { get; set; }
        public string Area { get; set; }
        public string BuildingArea { get; set; }
        public string Pool { get; set; }
        public string SubdivisionName { get; set; }
        public string TotalAssessedValue { get; set; }
        public string AssessmentYear { get; set; }
        public string TotalAssessorMarketValue { get; set; }
        public string AssessorMarketValueYear { get; set; }
        public string SalesPrice { get; set; }
        public string RecordingDate { get; set; }
        public string RecorderBookNumber { get; set; }
        public string RecorderPageNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentTypeCode { get; set; }
        public string RecordedTrustDeed { get; set; }
        public string LenderName { get; set; }
        public string LenderTypeCode { get; set; }
        public string TypeFinancing { get; set; }
        public string InitialInterestRate { get; set; }
        public string TrustDeedDueDate { get; set; }

        public string AdjustableRateRider { get; set; }
        public string AdjustableRateIndex { get; set; }
        public string ChangeIndex { get; set; }
        public string RateChangeFrequency { get; set; }
        public string InterestRateNotGreaterThan { get; set; }
        public string InterestRateNotLessThan { get; set; }
        public string MaximumInterestRate { get; set; }
        public string InterestOnlyPeriod { get; set; }
        public string FixedStepRateRider { get; set; }
        public string FirstChangeYear { get; set; }
        public string FirstChangeMonthDate { get; set; }
        public string PrepaymentRider { get; set; }
        public string PrepaymentPenaltyRiderMonths { get; set; }
        public string Source { get; set; }
        public string PID { get; set; }
        public string ImagePath { get; set; }
        public string TD2LOANAMOUNT { get; set; }
        public string TD2LoanType { get; set; }
        public string TD2LenderName { get; set; }
        public string TD2LenderTypeCode { get; set; }
        public string TD2TypeFinancing { get; set; }
        public string TD2Rate { get; set; }
        public string TD2DueDate { get; set; }
        public string TD2RECORDINGDATE { get; set; }
        public string Association { get; set; }
        public string MTANumber { get; set; }
        public string ClosingDate { get; set; }
        public string CreditReferenceNumber { get; set; }
        public string StatDate { get; set; }
        public string DotMortgageDate { get; set; }
        public string BalloonDueDate { get; set; }
        public string InterestOnly { get; set; }
        public string Amortization { get; set; }
        public string FileOrignator { get; set; }
        public string PrepayPenalty { get; set; }
        public string AppraisedValue { get; set; }
        public string ProductType { get; set; }
        public string AppraisalCompany { get; set; }



        public string SalesDetailsClass { get; set; }

       
        #endregion

        #endregion

        public Users() { 
        
        
        }

    }
}