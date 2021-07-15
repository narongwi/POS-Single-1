using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionID
    {
        public static FunctionID NoFunctionID { get { return new FunctionID("N/A"); } }
        public static FunctionID Login_DataConfigStore_Country_CountryCode { get { return new FunctionID("100-001-001-001-000", "CountryCode"); } }
        public static FunctionID Login_DataConfigStore_Country_CountryName { get { return new FunctionID("100-001-001-002-000", "CountryName"); } }
        public static FunctionID Login_DataConfigStore_Business_BusinessCode { get { return new FunctionID("100-001-002-001-000", "BusinessCode"); } }
        public static FunctionID Login_DataConfigStore_Business_BusinessName { get { return new FunctionID("100-001-002-002-000", "BusinessName"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StroeCode { get { return new FunctionID("100-001-003-001-000", "StoreCode"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StroeFullName { get { return new FunctionID("100-001-003-002-000", "StoreFullName"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StroeShortName { get { return new FunctionID("100-001-003-003-000", "StoreShortName"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreTaxID { get { return new FunctionID("100-001-003-004-000", "StoreTaxID"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreAddressLine1 { get { return new FunctionID("100-001-003-005-000", "StoreAddressLine1"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreAddressLine2 { get { return new FunctionID("100-001-003-006-000", "StoreAddressLine2"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreAddressLine3 { get { return new FunctionID("100-001-003-007-000", "StoreAddressLine3"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreTelphoneNo { get { return new FunctionID("100-001-003-008-000", "StoreTelphoneNo"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreFaxNo { get { return new FunctionID("100-001-003-009-000", "StoreFaxNo"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_StoreEmail { get { return new FunctionID("100-001-003-010-000", "StoreEmail"); } }
        public static FunctionID Login_DataConfigStore_StoreProfile_GrandOpenning { get { return new FunctionID("100-001-003-011-000", "StoreGrandOpen"); } }
        public static FunctionID Login_DataConfigStore_StoreOpen_Open24hoursStatus { get { return new FunctionID("100-001-004-001-000", "Store24HourStatus"); } }
        public static FunctionID Login_DataConfigStore_StoreOpen_OpenTime { get { return new FunctionID("100-001-004-002-000", "StoreOpenTime"); } }
        public static FunctionID Login_DataConfigStore_StoreOpen_CloseTime { get { return new FunctionID("100-001-004-003-000", "StoreCloseTime"); } }
        public static FunctionID Login_DataConfigStore_StoreOpen_CloseDay { get { return new FunctionID("100-001-004-004-000", "StoreCloseDay"); } }
        public static FunctionID Login_DataConfigStore_POSServer_MainServer_ServerName { get { return new FunctionID("100-001-005-001-001", "MainPosServerName"); } }
        public static FunctionID Login_DataConfigStore_POSServer_MainServer_IPAddress { get { return new FunctionID("100-001-005-001-002", "MainPosServerIP"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_POS_ServerMode { get { return new FunctionID("100-001-006-001-001", "DBNamePOS_Server"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_POS_StandAloneMode { get { return new FunctionID("100-001-006-001-002", "DBNamePOS_StandAlone"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_Merchandise_ServerMode { get { return new FunctionID("100-001-006-002-001", "DBNameMerchandise_Server"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_Merchandise_StandAloneMode { get { return new FunctionID("100-001-006-002-002", "DBNameMerchandise_StandAlone"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_Promotion_ServerMode { get { return new FunctionID("100-001-006-003-001", "DBNamePromotion_Server"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_Promotion_StandAloneMode { get { return new FunctionID("100-001-006-003-002", "DBNamePromotion_StandAlone"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_Loyalty_ServerMode { get { return new FunctionID("100-001-006-004-001", "DBNameLoyalty_Server"); } }
        public static FunctionID Login_DataConfigStore_POSDatabase_Loyalty_StandAloneMode { get { return new FunctionID("100-001-006-004-002", "DBNameLoyalty_StandAlone"); } }
        public static FunctionID Login_DataConfigStore_Timeout_Application_AllMenu { get { return new FunctionID("100-001-007-001-001", "TimeOutApp"); } }
        public static FunctionID Login_DataConfigStore_Timeout_Database_AllMenu { get { return new FunctionID("100-001-007-002-001", "TimeOutDB"); } }
        public static FunctionID Login_DataConfigStore_Timeout_Retrytimes_AllMenu { get { return new FunctionID("100-001-007-003-001", "RetryTimes"); } }
        public static FunctionID Login_DataConfigStore_Account_Storecode { get { return new FunctionID("100-001-008-001-000", "AccountStoreCode"); } }
        public static FunctionID Login_DataConfigStore_Account_StoreShortName { get { return new FunctionID("100-001-008-002-000", "AccountStoreShortName"); } }
        public static FunctionID Login_DataConfigStore_Account_CompanyCode { get { return new FunctionID("100-001-008-003-000", "AccountCompanyCode"); } }
        public static FunctionID Login_DataConfigStore_Account_BranchName { get { return new FunctionID("100-001-008-004-000", "AccountBranchName"); } }
        public static FunctionID Login_DataConfigStore_TimeOfcuttransaction { get { return new FunctionID("100-001-009-000-000", "StoreTransactionCutTime"); } }
        public static FunctionID Login_DataConfigStore_CurrencyDefault { get { return new FunctionID("100-001-010-000-000", "CurrencyDefault"); } }
        public static FunctionID Login_DataConfigStore_LanguageDefault { get { return new FunctionID("100-001-011-000-000", "LanguageDefault"); } }
        public static FunctionID Login_DataConfigStore_AmountType { get { return new FunctionID("100-001-012-000-000", "AmountType"); } }
        public static FunctionID Login_DataConfigStore_AmountDisplay { get { return new FunctionID("100-001-013-000-000", "AmountDisplay"); } }
        public static FunctionID Login_DataConfigStore_AmountPrint { get { return new FunctionID("100-001-014-000-000", "AmountPrint"); } }
        public static FunctionID Login_DataConfigStore_AmountRound_Price { get { return new FunctionID("100-001-015-001-000", "AmountRoundPrice"); } }
        public static FunctionID Login_DataConfigStore_AmountRound_Discount { get { return new FunctionID("100-001-015-002-000", "AmountRoundDiscount"); } }
        public static FunctionID Login_DataConfigStore_AmountRound_Payment { get { return new FunctionID("100-001-015-003-000", "AmountRoundPayment"); } }
        public static FunctionID Login_DataConfigStore_DiscountPerItem { get { return new FunctionID("100-001-016-000-000", "DiscountItemType"); } }
        public static FunctionID Login_DataConfigStore_CurrencyTypeofChangeAmount { get { return new FunctionID("100-001-017-000-000", "ChangeCurrencyType"); } }
        public static FunctionID Login_DataConfigStore_FrequenctCountQue { get { return new FunctionID("100-001-018-000-000", "FreqCountQue"); } }
        public static FunctionID Login_DataConfigStore_MessageCashier_Duration { get { return new FunctionID("100-001-019-001-000", "MsgCashierDuration"); } }
        public static FunctionID Login_DataConfigStore_MessageCashier_TextMessage { get { return new FunctionID("100-001-019-002-000", "MsgCashierText"); } }
        public static FunctionID Login_DataConfigStore_HeadOfficeServer_ServerName { get { return new FunctionID("100-001-020-001-000", "HOServerName"); } }
        public static FunctionID Login_DataConfigStore_HeadOfficeServer_ServerIP { get { return new FunctionID("100-001-020-002-000", "HOServerIP"); } }
        
        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountType { get { return new FunctionID("100-001-021-011-000", "CashInFloatAmtType"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountType { get { return new FunctionID("161-060-020-000-000", "CashOutFloatAmtType"); } }
        
        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMethod { get { return new FunctionID("100-001-021-012-000", "CashInFloatAmtMethod"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMethod { get { return new FunctionID("161-060-030-000-000", "CashOutFloatAmtMethod"); } }

        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountMethod { get { return new FunctionID("161-060-030-000-000", "CashOutSaleAmtMethod"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountDefault { get { return new FunctionID("100-001-021-013-000", "CashInFloatAmtDefault"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountDefault { get { return new FunctionID("161-060-040-000-000", "CashOutFloatAmtDefault"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountDefault_THB { get { return new FunctionID("100-001-021-013-001", "CashInFloatAmtDefault_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountDefault_THB { get { return new FunctionID("161-001-021-013-001", "CashOutFloatAmtDefault_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountDefault_USD { get { return new FunctionID("100-001-021-013-002", "CashInFloatAmtDefault_USD");} }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountDefault_USD { get { return new FunctionID("161-001-021-013-002", "CashOutFloatAmtDefault_USD");} }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountDefault_KHR { get { return new FunctionID("100-001-021-013-003", "CashInFloatAmtDefault_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountDefault_KHR { get { return new FunctionID("161-001-021-013-003", "CashOutFloatAmtDefault_KHR"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountDefault_LAK { get { return new FunctionID("100-001-021-013-004", "CashInFloatAmtDefault_LAK");} }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountDefault_LAK { get { return new FunctionID("161-001-021-013-004", "CashInFloatAmtDefault_LAK"); } }
            
        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMinimum { get { return new FunctionID("100-001-021-014-000", "CashFloatAmtMin"); } }      
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMinimum { get { return new FunctionID("161-001-021-014-000", "CashOutFloatAmtMin"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMinimum_THB { get { return new FunctionID("100-001-021-014-001", "CashFloatAmtMin_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMinimum_THB { get { return new FunctionID("161-001-021-014-001", "CashOutFloatAmtMin_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMinimum_USD { get { return new FunctionID("100-001-021-014-002", "CashFloatAmtMin_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMinimum_USD { get { return new FunctionID("161-001-021-014-002", "CashOutFloatAmtMin_USD"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMinimum_KHR { get { return new FunctionID("100-001-021-014-003", "CashFloatAmtMin_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMinimum_KHR { get { return new FunctionID("161-001-021-014-003", "CashOutFloatAmtMin_KHR"); } }


        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMinimum_LAK { get { return new FunctionID("100-001-021-014-004", "CashFloatAmtMin_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMinimum_LAK { get { return new FunctionID("161-001-021-014-004", "CashOutFloatAmtMin_LAK"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMaximum { get { return new FunctionID("100-001-021-015-000", "CashFloatAmtMax"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMaximum { get { return new FunctionID("161-001-021-015-000", "CashOutFloatAmtMax"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMaximum_THB { get { return new FunctionID("100-001-021-015-001", "CashFloatAmtMax_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMaximum_THB { get { return new FunctionID("100-001-021-015-001", "CashOutFloatAmtMax_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMaximum_USD { get { return new FunctionID("100-001-021-015-002", "CashFloatAmtMax_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMaximum_USD { get { return new FunctionID("161-001-021-015-002", "CashOutFloatAmtMax_USD"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMaximum_KHR { get { return new FunctionID("100-001-021-015-003", "CashFloatAmtMax_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMaximum_KHR { get { return new FunctionID("161-001-021-015-003", "CashOutFloatAmtMax_KHR"); } }

        public static FunctionID Login_DataConfigStore_CashIn_FloatAmountMaximum_LAK { get { return new FunctionID("100-001-021-015-004", "CashFloatAmtMax_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_FloatAmountMaximum_LAK { get { return new FunctionID("161-001-021-015-004", "CashOutFloatAmtMax_LAK"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountType { get { return new FunctionID("100-001-021-021-000", "CashInLoanAmtType"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountType { get { return new FunctionID("161-001-021-021-000", "CashOutLoanAmtType"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMethod { get { return new FunctionID("100-001-021-022-000", "CashInLoanAmtMethod"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMethod { get { return new FunctionID("161-001-021-022-000", "CashOutLoanAmtMethod"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountDefault { get { return new FunctionID("100-001-021-023-000", "CashInLoanAmtDefault"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountDefault { get { return new FunctionID("161-001-021-023-000", "CashOutLoanAmtDefault"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountDefault { get { return new FunctionID("161-003-021-023-000", "CashOutLoanAmtDefault"); } }


        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountDefault_THB { get { return new FunctionID("100-001-021-023-001", "CashInLoanAmtDefault_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountDefault_THB { get { return new FunctionID("161-001-021-023-001", "CashOutLoanAmtDefault_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountDefault_THB { get { return new FunctionID("161-003-021-023-001", "CashOutSaleAmtDefault_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountDefault_USD { get { return new FunctionID("100-001-021-023-002", "CashInLoanAmtDefault_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountDefault_USD { get { return new FunctionID("161-001-021-023-002", "CashOutLoanAmtDefault_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountDefault_USD { get { return new FunctionID("161-003-021-023-002", "CashOutSaleAmtDefault_USD"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountDefault_KHR { get { return new FunctionID("100-001-021-023-003", "CashInLoanAmtDefault_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountDefault_KHR { get { return new FunctionID("161-001-021-023-003", "CashOutLoanAmtDefault_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountDefault_KHR { get { return new FunctionID("161-003-021-023-003", "CashOutSaleAmtDefault_KHR"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountDefault_LAK { get { return new FunctionID("100-001-021-023-004", "CashInLoanAmtDefault_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountDefault_LAK { get { return new FunctionID("100-001-021-023-004", "CashOutLoanAmtDefault_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountDefault_LAK { get { return new FunctionID("100-001-021-023-004", "CashOutSaleAmtDefault_LAK"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMinimum { get { return new FunctionID("100-001-021-024-000", "CashInLoanAmtMin"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMinimum { get { return new FunctionID("161-001-021-024-000", "CashOutLoanAmtMin"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMinimum_THB { get { return new FunctionID("100-001-021-024-001", "CashInLoanAmtMin_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMinimum_THB { get { return new FunctionID("161-001-021-024-001", "CashOutLoanAmtMin_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMinimum_USD { get { return new FunctionID("100-001-021-024-002", "CashInLoanAmtMin_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMinimum_USD { get { return new FunctionID("161-001-021-024-002", "CashOutLoanAmtMin_USD"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMinimum_KHR { get { return new FunctionID("100-001-021-024-003", "CashInLoanAmtMin_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMinimum_KHR { get { return new FunctionID("161-001-021-024-003", "CashOutLoanAmtMin_KHR"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMinimum_LAK { get { return new FunctionID("100-001-021-024-004", "CashInLoanAmtMin_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMinimum_LAK { get { return new FunctionID("161-001-021-024-004", "CashOutLoanAmtMin_LAK"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMaximum { get { return new FunctionID("100-001-021-025-000", "CashInLoanAmtMax"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMaximum { get { return new FunctionID("100-001-021-025-000", "CashOutLoanAmtMax"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMaximum_THB { get { return new FunctionID("100-001-021-025-001", "CashInLoanAmtMax_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMaximum_THB { get { return new FunctionID("161-001-021-025-001", "CashOutLoanAmtMax_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMaximum_USD { get { return new FunctionID("100-001-021-025-002", "CashInLoanAmtMax_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMaximum_USD { get { return new FunctionID("161-001-021-025-002", "CashOutLoanAmtMax_USD"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMaximum_KHR { get { return new FunctionID("100-001-021-025-003", "CashInLoanAmtMax_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMaximum_KHR { get { return new FunctionID("161-001-021-025-003", "CashOutLoanAmtMax_KHR"); } }

        public static FunctionID Login_DataConfigStore_CashIn_LoanAmountMaximum_LAK { get { return new FunctionID("100-001-021-025-004", "CashInLoanAmtMax_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_LoanAmountMaximum_LAK { get { return new FunctionID("161-001-021-025-004", "CashOutLoanAmtMax_LAK"); } }

        public static FunctionID Login_DataConfigStore_CashIn_SaleAmountType { get { return new FunctionID("100-001-021-031-000", "CashInSaleAmtType"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountType { get { return new FunctionID("161-001-021-031-000", "CashOutSaleAmtType"); } }

        public static FunctionID Login_DataConfigStore_CashIn_SaleAmountMaximum { get { return new FunctionID("100-001-021-032-000", "CashInSaleAmtMax"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountMaximum { get { return new FunctionID("161-001-021-032-000", "CashInSaleAmtMax"); } }

        public static FunctionID Login_DataConfigStore_CashIn_SaleAmountMaximum_THB { get { return new FunctionID("100-001-021-032-001", "CashInSaleAmtMax_THB"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountMaximum_THB { get { return new FunctionID("161-001-021-032-001", "CashOutSaleAmtMax_THB"); } }

        public static FunctionID Login_DataConfigStore_CashIn_SaleAmountMaximum_USD { get { return new FunctionID("100-001-021-032-002", "CashInSaleAmtMax_USD"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountMaximum_USD { get { return new FunctionID("161-001-021-032-002", "CashOutSaleAmtMax_USD"); } }

        public static FunctionID Login_DataConfigStore_CashIn_SaleAmountMaximum_KHR { get { return new FunctionID("100-001-021-032-003", "CashInSaleAmtMax_KHR"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountMaximum_KHR { get { return new FunctionID("161-001-021-032-003", "CashOutSaleAmtMax_KHR"); } }

        public static FunctionID Login_DataConfigStore_CashIn_SaleAmountMaximum_LAK { get { return new FunctionID("100-001-021-032-004", "CashInSaleAmtMax_LAK"); } }
        public static FunctionID Login_DataConfigStore_CashOut_SaleAmountMaximum_LAK { get { return new FunctionID("161-001-021-032-004", "CashOutSaleAmtMax_LAK"); } }

        public static FunctionID Login_DataConfigStore_SaleMenu_DefualtCursorPositionatStepBeforeInputProductItem { get { return new FunctionID("100-001-022-001-000", "CursorPositionBFInputItem"); } }
        public static FunctionID Login_DataConfigStore_CustomerQue_InputType { get { return new FunctionID("100-001-023-001-000", "CustomerQueInputMethod"); } }
        public static FunctionID Login_DataConfigStore_CustomerQue_QueqeFormat { get { return new FunctionID("100-001-023-002-000", "CustomerQueNumberMethod"); } }
        public static FunctionID Login_DataConfigStore_InputServeTypeofReceipt_Input_Level { get { return new FunctionID("100-001-024-001-001", "ServeTypeInputLevel"); } }
        public static FunctionID Login_DataConfigStore_InputServeTypeofReceipt_Input_Step { get { return new FunctionID("100-001-024-001-002", "ServeTypeInputMethod"); } }
        public static FunctionID Login_DataConfigStore_InputServeTypeofReceipt_Input_ServerType { get { return new FunctionID("100-001-024-001-003", "ServerTypeData"); } }
        public static FunctionID Login_DataConfigStore_InputServeTypeofReceipt_DefaultServetype { get { return new FunctionID("100-001-024-002-000", "ServeTypeDefault"); } }
        public static FunctionID Login_DataConfigStore_Member_Status { get { return new FunctionID("100-001-025-001-000", "MemberEveryReceiptStatus"); } }
        public static FunctionID Login_DataConfigStore_Member_InputMethod { get { return new FunctionID("100-001-025-002-000", "MemberInputMethod"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_MemberID { get { return new FunctionID("100-001-025-003-001", "MemberCardNoLengt"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_PhoneNo { get { return new FunctionID("100-001-025-003-002", "MemberTelNoLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_IDCardNo { get { return new FunctionID("100-001-025-003-003", "MemberIdCardLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_TaxID { get { return new FunctionID("100-001-025-003-004", "MemberTaxIdLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_Name { get { return new FunctionID("100-001-025-003-005", "MemberFirstNameLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_Lastname { get { return new FunctionID("100-001-025-003-006", "MemberLastNameLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Seacrh_NameEng { get { return new FunctionID("100-001-025-003-007", "MemberFirstNameEngLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Search_LastnameEng { get { return new FunctionID("100-001-025-003-008", "MemberLastNameEngLength"); } }
        public static FunctionID Login_DataConfigStore_Member_Display { get { return new FunctionID("100-001-025-004-000", "MemberDisplayTemplateID"); } }
        public static FunctionID Login_DataConfigStore_Employee_InputMethod { get { return new FunctionID("100-001-026-001-000", "EmployeeInputMethod"); } }
        public static FunctionID Login_DataConfigStore_Employee_Search_EmployeeID { get { return new FunctionID("100-001-026-002-001", "EmployeeIDlength"); } }
        public static FunctionID Login_DataConfigStore_Employee_Display { get { return new FunctionID("100-001-026-003-000", "EmplyeeDisplayTempleteID"); } }
        public static FunctionID Login_DataConfigStore_Employee_Discount_DiscountRateDefault { get { return new FunctionID("100-001-026-004-001", "EmployeeDiscountRate"); } }
        public static FunctionID Login_DataConfigStore_Employee_Discount_LimitDiscountAmountPermonth { get { return new FunctionID("100-001-026-004-002", "EmployeeDiscountMax"); } }
        public static FunctionID Login_DataConfigStore_PCMan_Input_Level { get { return new FunctionID("100-001-027-001-001", "PCmanInputLevel"); } }
        public static FunctionID Login_DataConfigStore_PCMan_Input_Step { get { return new FunctionID("100-001-027-001-002", "PCmanInputMethod"); } }
        public static FunctionID Login_DataConfigStore_PCMan_Search_PCSaleMan { get { return new FunctionID("100-001-027-002-010", "PCmanIDlength"); } }
        public static FunctionID Login_DataConfigStore_PCMan_Display { get { return new FunctionID("100-001-027-003-000", "PCmanDisplayTempleteID"); } }
        public static FunctionID Login_DataConfigStore_ProductInput_InputType { get { return new FunctionID("100-001-027-001-000", "ProductItemInputType"); } }
        public static FunctionID Login_DataConfigStore_Deposit_ProductCode { get { return new FunctionID("100-001-028-001-000", "DepositProductCode"); } }
        public static FunctionID Login_DataConfigStore_Deposit_Amount { get { return new FunctionID("100-001-028-002-000", "DepositAmountMax"); } }
        public static FunctionID Login_DataConfigStore_Quantity_Format { get { return new FunctionID("100-001-029-001-000", "QuantItemDisplayTemplateID"); } }
        public static FunctionID Login_DataConfigStore_Quantity_Limit { get { return new FunctionID("100-001-029-002-000", "QuantItemMax"); } }
        public static FunctionID Login_DataConfigStore_AdjustPrice_ItemMax { get { return new FunctionID("100-001-030-001-000", "AdjustPriceItemMax"); } }
        public static FunctionID Login_DataConfigStore_AdjustPrice_ReceiptMax { get { return new FunctionID("100-001-030-002-000", "AdjustPriceReceiptMax"); } }
        public static FunctionID Login_DataConfigStore_Delete_Limit { get { return new FunctionID("100-001-031-001-000", "DeleteItemAmountMax"); } }
        public static FunctionID Login_DataConfigStore_DiscountManual_LimitItemDiscount { get { return new FunctionID("100-001-032-001-000", "DiscountItemMax"); } }
        public static FunctionID Login_DataConfigStore_DiscountManual_LimitReceiptDiscount { get { return new FunctionID("100-001-033-001-000", "DiscountReceiptLimit"); } }
        public static FunctionID Login_DataConfigStore_PrintTakeOut { get { return new FunctionID("100-001-034-000-000", "PrintTakeOutDocumentMethod"); } }
        public static FunctionID Login_DataConfigStore_CancelOrder_Limit { get { return new FunctionID("100-001-035-001-000", "CancelReceiptAmountMax"); } }
        public static FunctionID Login_DataConfigStore_HoldOrder_SaveOrderNoType { get { return new FunctionID("100-001-036-001-000", "HoldOrderType"); } }
        public static FunctionID Login_DataConfigStore_DiscountManaul_ReceiptLimitDiscount { get { return new FunctionID("100-001-037-001-000", "ReceiptDiscountAmountMax"); } }
        public static FunctionID Login_DataConfigStore_Payment_LimitType { get { return new FunctionID("100-001-038-001-000", "CountPaymentMax"); } }
        public static FunctionID Login_DataConfigStore_Payment_Input_InputMethod { get { return new FunctionID("100-001-038-002-001", "PaymentInputMethod"); } }
        public static FunctionID Login_DataConfigStore_Change_Display { get { return new FunctionID("100-001-039-001-000", "ChageAmtType"); } }
        public static FunctionID Login_DataConfigStore_Change_Input { get { return new FunctionID("100-001-039-002-000", "ChangeAmountMax"); } }
        public static FunctionID Login_DataConfigStore_Change_Adjust { get { return new FunctionID("100-001-039-003-000", "ChangeDiffAmountMax"); } }
        public static FunctionID Login_DataConfigStore_ReceiptRequire { get { return new FunctionID("100-001-040-000-000", "CustomerGetReceiptData"); } }
        public static FunctionID Login_DataConfigStore_ReceiptABB_PrintMethod { get { return new FunctionID("100-001-041-001-000", "PrintReceiptMethod"); } }
        public static FunctionID Login_DataConfigStore_ReceiptABB_Format { get { return new FunctionID("100-001-041-002-000", "PrintABBtemplateID"); } }
        public static FunctionID Login_DataConfigStore_FullTax_Format { get { return new FunctionID("100-001-042-001-000", "PrintFFTITemplateID"); } }
        public static FunctionID Login_DataConfigStore_PrintOrder_Coffee { get { return new FunctionID("100-001-043-001-000", "PrintCoffeeOrderTemplateID"); } }
        public static FunctionID Login_DataConfigStore_PrintOrder_Food { get { return new FunctionID("100-001-043-002-000", "PrintFoodOrderTemplateID"); } }
        public static FunctionID Login_DataConfigStore_OperateDate { get { return new FunctionID("100-001-044-000-000", "OperateDate"); } }
        public static FunctionID Login_DataConfigStore_Login_KeyboradDisplay { get { return new FunctionID("100-001-045-001-000", "LoginKeyboardDisplay"); } }
        public static FunctionID Login_DataConfigTill_TillNumber { get { return new FunctionID("100-002-001-000-000", "TillNo"); } }
        public static FunctionID Login_DataConfigTill_TillType { get { return new FunctionID("100-002-002-000-000", "TillType"); } }
        public static FunctionID Login_DataConfigTill_TillName { get { return new FunctionID("100-002-003-000-000", "TillName"); } }
        public static FunctionID Login_DataConfigTill_PermissionID { get { return new FunctionID("100-002-004-000-000", "TillPermissionID"); } }
        public static FunctionID Login_DataConfigTill_LastCashier_CashierID { get { return new FunctionID("100-002-005-001-000", "TillLastCashierID"); } }
        public static FunctionID Login_DataConfigTill_LastCashier_CashierName { get { return new FunctionID("100-002-005-002-000", "TillLastCashierName"); } }
        public static FunctionID Login_DataConfigTill_SaleModeType { get { return new FunctionID("100-002-006-000-000", "TillSaleMode"); } }
        public static FunctionID Login_DataConfigTill_CurrentServer_ServerName { get { return new FunctionID("100-002-007-001-000", "TillPosServerName"); } }
        public static FunctionID Login_DataConfigTill_CurrentServer_IPAddress { get { return new FunctionID("100-002-007-002-000", "TillPosServerIP"); } }
        public static FunctionID Login_DataConfigTill_CurrentServer_DatabaseName { get { return new FunctionID("100-002-007-003-000", "TillDBNamePOS"); } }
        public static FunctionID Login_DataConfigTill_ReceiptNumber_ABBNo { get { return new FunctionID("100-002-008-001-000", "TillABBno"); } }
        public static FunctionID Login_DataConfigTill_ReceiptNumber_CreditNoteNo { get { return new FunctionID("100-002-008-002-000", "TillCNno"); } }
        public static FunctionID Login_DataConfigTill_ReceiptNumber_FFTINo { get { return new FunctionID("100-002-008-003-000", "TillFFTIno"); } }
        public static FunctionID Login_DataConfigTill_POSClient_ComputerName { get { return new FunctionID("100-002-009-001-000", "TillClientName"); } }
        public static FunctionID Login_DataConfigTill_POSClient_IPAddress { get { return new FunctionID("100-002-009-002-000", "TillClientIP"); } }
        public static FunctionID Login_CheckVersionofApplication { get { return new FunctionID("100-010-000-000-000"); } }
        public static FunctionID Login_LoadConfigDatatoPOSClient { get { return new FunctionID("100-020-000-000-000"); } }
        public static FunctionID Login_LoadPaymentConfig { get { return new FunctionID("100-021-001-000-000"); } }
        public static FunctionID Login_LoadPaymentPolicy { get { return new FunctionID("100-021-002-000-000"); } }
        public static FunctionID Login_LoadAppAlertMessage { get { return new FunctionID("100-022-000-000-000"); } }
        public static FunctionID Login_LoadPosImage { get { return new FunctionID("100-025-000-000-000"); } }
        public static FunctionID Login_PopupLoginScreen { get { return new FunctionID("100-030-000-000-000"); } }
        public static FunctionID Login_InputUserIDPassword { get { return new FunctionID("100-040-000-000-000"); } }
        public static FunctionID Login_ValidUserIDPassword { get { return new FunctionID("100-050-000-000-000"); } }
        public static FunctionID Login_ValidUserIDPassword_WorkPasswordTime { get { return new FunctionID("100-050-001-000-000", "ValidPasswordMaxTime"); } }
        public static FunctionID Login_ValidUserIDPassword_PasswordExpire { get { return new FunctionID("100-050-002-000-000"); } }
        public static FunctionID Login_ValidUserIDPassword_WarningPasswordExpire { get { return new FunctionID("100-050-003-000-000"); } }
        public static FunctionID Login_ChangePassword_SaveTransactionChangePassword { get { return new FunctionID("100-055-001-000-000"); } }
        public static FunctionID Login_ChangePassword_CheckPasswordReuse { get { return new FunctionID("100-055-002-000-000", "ChangePasswordReuse"); } }
        public static FunctionID Login_ChangePassword_AutoLogout { get { return new FunctionID("100-055-003-000-000"); } }
        public static FunctionID Login_ChangePassword_SaveLogoutTransaction { get { return new FunctionID("100-055-004-000-000"); } }
        public static FunctionID Login_ChangePassword_SaveLogoutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("100-055-004-001-000"); } }
        public static FunctionID Login_ChangePassword_EndOfLogin { get { return new FunctionID("100-055-005-000-000"); } }
        public static FunctionID Login_LoadPolicyProfiletoPOSClient { get { return new FunctionID("100-060-000-000-000"); } }
        public static FunctionID Login_CheckProfile { get { return new FunctionID("100-070-000-000-000"); } }
        public static FunctionID Login_CheckTerminal { get { return new FunctionID("100-080-000-000-000"); } }
        public static FunctionID Login_CheckTerminal_CheckDupTillNo { get { return new FunctionID("100-080-010-000-000"); } }
        public static FunctionID Login_CheckTerminal_CheckLastUseratTill { get { return new FunctionID("100-080-020-000-000"); } }
        public static FunctionID Login_CheckTerminal_ValidRunningofReceiptAllDocumentType { get { return new FunctionID("100-080-030-000-000"); } }
        public static FunctionID Login_SaveLoginTransaction { get { return new FunctionID("100-090-000-000-000"); } }
        public static FunctionID Login_SaveLoginTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("100-090-001-000-000"); } }
        public static FunctionID Login_DisplayMainMenu { get { return new FunctionID("100-100-000-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_OpenDay { get { return new FunctionID("100-100-010-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_CashIn { get { return new FunctionID("100-100-020-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_Sale { get { return new FunctionID("100-100-030-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_Return_ByReceipt { get { return new FunctionID("100-100-040-001-000"); } }
        public static FunctionID Login_DisplayMainMenu_Return_ByItem { get { return new FunctionID("100-100-040-002-000"); } }
        public static FunctionID Login_DisplayMainMenu_Void { get { return new FunctionID("100-100-050-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_CashOut { get { return new FunctionID("100-100-060-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_EndOfShift { get { return new FunctionID("100-100-070-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_EndOfDay { get { return new FunctionID("100-100-080-000-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_ChangePassword { get { return new FunctionID("100-100-090-001-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_CheckProduct { get { return new FunctionID("100-100-090-002-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_FullTax { get { return new FunctionID("100-100-090-003-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_AdjustPrice { get { return new FunctionID("100-100-090-040-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_RerunBeginOfDay { get { return new FunctionID("100-100-090-050-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_MemberBigCard { get { return new FunctionID("100-100-090-060-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_RedeemPoint { get { return new FunctionID("100-100-090-070-000"); } }
        public static FunctionID Login_DisplayMainMenu_Tool_CancelCashOut { get { return new FunctionID("100-100-090-008-000"); } }
        public static FunctionID Login_DisplayMainMenu_Report_CheckDaySale { get { return new FunctionID("100-100-100-001-000"); } }
        public static FunctionID Login_DisplayMainMenu_Report_CheckDayReceipt { get { return new FunctionID("100-100-100-002-000"); } }
        public static FunctionID Login_CheckHardware_PrinterABB { get { return new FunctionID("100-110-001-000-000"); } }
        public static FunctionID Login_CheckHardware_PrinterFFTI { get { return new FunctionID("100-110-002-000-000"); } }
        public static FunctionID Login_CheckHardware_CustomerDisplay_Brand { get { return new FunctionID("100-110-003-001-000"); } }
        public static FunctionID Login_CheckHardware_CustomerDisplay_Language { get { return new FunctionID("100-110-003-002-000"); } }
        public static FunctionID Login_CheckHardware_Keyboard { get { return new FunctionID("100-110-004-000-000"); } }
        public static FunctionID Login_CheckHardware_Scanner_TableScanner { get { return new FunctionID("100-110-005-001-000"); } }
        public static FunctionID Login_CheckHardware_Scanner_handheldScanner { get { return new FunctionID("100-110-005-002-000"); } }
        public static FunctionID Login_CheckHardware_MagneticStripe { get { return new FunctionID("100-110-006-000-000"); } }
        public static FunctionID Login_CheckHardware_EDC_Type { get { return new FunctionID("100-110-007-001-000", "DeviceEDCtype"); } }
        public static FunctionID Login_CheckHardware_EDC_Port { get { return new FunctionID("100-110-007-002-000"); } }
        public static FunctionID Login_CheckHardware_Drawer { get { return new FunctionID("100-110-008-000-000"); } }
        public static FunctionID Login_CheckHardware_RabbitReader_Type { get { return new FunctionID("100-110-009-001-000"); } }
        public static FunctionID Login_CheckHardware_RabbitReader_Port { get { return new FunctionID("100-110-009-002-000"); } }
        public static FunctionID Login_CheckHardware_WEBCAM { get { return new FunctionID("100-110-010-000-000"); } }
        public static FunctionID Login_CashierMessage_Enabled { get { return new FunctionID("100-120-000-000-000"); } }
        public static FunctionID Login_CashierMessage_Status { get { return new FunctionID("100-120-001-001-000"); } }
        public static FunctionID Login_LoadLanguage { get { return new FunctionID("100-130-000-000-000"); } }
        public static FunctionID Login_CheckLastReceiptCredPay { get { return new FunctionID("100-096-000-000-000"); } }

        public static FunctionID OpenDay_SelectOpenDayMenu { get { return new FunctionID("101-010-000-000-000"); } }
        public static FunctionID OpenDay_GetRunningNo { get { return new FunctionID("101-011-000-000-000"); } }
        public static FunctionID OpenDay_CheckOpenDayofTillStatus { get { return new FunctionID("101-012-000-000-000"); } }
        public static FunctionID OpenDay_GetMessageCashier { get { return new FunctionID("101-013-000-000-000"); } }
        public static FunctionID OpenDay_PopupOpenDayProcessScreen { get { return new FunctionID("101-020-000-000-000"); } }
        public static FunctionID OpenDay_ConfirmOpenDayProcess { get { return new FunctionID("101-030-000-000-000"); } }
        public static FunctionID OpenDay_DownLoadDatafromHeadOfficetoClient { get { return new FunctionID("101-040-000-000-000"); } }
        public static FunctionID OpenDay_SaveOpenDayTransaction { get { return new FunctionID("101-050-000-000-000"); } }
        public static FunctionID OpenDay_SaveOpenDayTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("101-050-001-000-000"); } }
        public static FunctionID OpenDay_PrintOpenDayDocument { get { return new FunctionID("101-060-000-000-000"); } }
        public static FunctionID CashIn_SelectCashInMenu { get { return new FunctionID("102-010-000-000-000"); } }
        public static FunctionID CashIn_GetRunningNo { get { return new FunctionID("102-011-000-000-000"); } }
        public static FunctionID CashIn_CheckOpenDayofTillStatus { get { return new FunctionID("102-012-000-000-000"); } }
        public static FunctionID CashIn_GetMessageCashier { get { return new FunctionID("102-013-000-000-000"); } }
        public static FunctionID CashIn_CheckUserIDPasswordCurrentCashier { get { return new FunctionID("102-020-000-000-000"); } }
        public static FunctionID CashIn_DisplayCashInMenu { get { return new FunctionID("102-029-000-000-000"); } }
        public static FunctionID CashIn_NormalChange { get { return new FunctionID("102-030-000-000-000"); } }
        public static FunctionID CashIn_NormalChange_Type { get { return new FunctionID("102-030-001-000-000"); } }
        public static FunctionID CashIn_NormalChange_InputMethod { get { return new FunctionID("102-030-002-000-000"); } }
        public static FunctionID CashIn_NormalChange_DefalutAmount { get { return new FunctionID("102-030-003-000-000"); } }
        public static FunctionID CashIn_NormalChange_DefalutAmount_Editable { get { return new FunctionID("102-030-003-001-000"); } }
        public static FunctionID CashIn_NormalChange_CheckAmount_Minimum { get { return new FunctionID("102-030-004-001-000"); } }
        public static FunctionID CashIn_NormalChange_CheckAmount_Maximum { get { return new FunctionID("102-030-004-002-000"); } }
        public static FunctionID CashIn_NormalChange_PopupSummaryPage { get { return new FunctionID("102-030-005-000-000"); } }
        public static FunctionID CashIn_NormalChange_Submit { get { return new FunctionID("102-030-006-000-000"); } }
        public static FunctionID CashIn_NormalChange_ShowChangeDetail { get { return new FunctionID("102-030-007-000-000"); } }
        public static FunctionID CashIn_AdditionChange { get { return new FunctionID("102-040-000-000-000"); } }
        public static FunctionID CashIn_AdditionChange_Type { get { return new FunctionID("102-040-001-000-000"); } }
        public static FunctionID CashIn_AdditionChange_InputMethod { get { return new FunctionID("102-040-002-000-000"); } }
        public static FunctionID CashIn_AdditionChange_DefalutAmount { get { return new FunctionID("102-040-003-000-000"); } }
        public static FunctionID CashIn_AdditionChange_DefalutAmount_Editable { get { return new FunctionID("102-040-003-001-000"); } }
        public static FunctionID CashIn_AdditionChange_CheckAmount_Minimum { get { return new FunctionID("102-040-004-001-000"); } }
        public static FunctionID CashIn_AdditionChange_CheckAmount_Maximum { get { return new FunctionID("102-040-004-002-000"); } }
        public static FunctionID CashIn_AdditionChange_PopupSummaryPage { get { return new FunctionID("102-040-005-000-000"); } }
        public static FunctionID CashIn_AdditionChange_Submit { get { return new FunctionID("102-040-006-000-000"); } }
        public static FunctionID CashIn_AdditionChange_ShowChangeDetail { get { return new FunctionID("102-040-007-000-000"); } }
        public static FunctionID CashIn_ConfirmCashIn { get { return new FunctionID("102-100-000-000-000"); } }
        public static FunctionID CashIn_SaveOpenCashDrawer { get { return new FunctionID("102-110-000-000-000"); } }
        public static FunctionID CashIn_PushMoneyToCashDrawer { get { return new FunctionID("102-120-000-000-000"); } }
        public static FunctionID CashIn_SaveCloseCashDrawer { get { return new FunctionID("102-130-000-000-000"); } }
        public static FunctionID CashIn_SaveCashInTransaction { get { return new FunctionID("102-140-000-000-000"); } }
        public static FunctionID CashIn_SaveCashInTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("102-140-001-000-000"); } }
        public static FunctionID CashIn_PrintCashInDocument { get { return new FunctionID("102-150-000-000-000"); } }
        public static FunctionID Sale_SelectSaleMenu { get { return new FunctionID("121-010-000-000-000"); } }
        public static FunctionID Sale_GetRunningNo { get { return new FunctionID("121-011-000-000-000"); } }
        public static FunctionID Sale_CheckOpenDayofTillStatus { get { return new FunctionID("121-012-000-000-000"); } }
        public static FunctionID Sale_GetMessageCashier { get { return new FunctionID("121-012-000-000-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeCode { get { return new FunctionID("121-020-010-001-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_StroeName { get { return new FunctionID("121-020-010-002-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_TAXID { get { return new FunctionID("121-020-010-003-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_Till_TillNumber { get { return new FunctionID("121-020-010-004-001"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_Till_TillType { get { return new FunctionID("121-020-010-004-002"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_Till_TillName { get { return new FunctionID("121-020-010-004-003"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_PermissionID { get { return new FunctionID("121-020-010-005-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_ReceiptNumber_ABBNo { get { return new FunctionID("121-020-010-006-001"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_ReceiptNumber_CreditNoteNo { get { return new FunctionID("121-020-010-006-002"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_ReceiptNumber_FFTINo { get { return new FunctionID("121-020-010-006-003"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_CashierID { get { return new FunctionID("121-020-010-007-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_CashierName { get { return new FunctionID("121-020-010-008-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_SaleModeType { get { return new FunctionID("121-020-010-009-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_POSServer_ServerName { get { return new FunctionID("121-020-010-010-001"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_POSServer_IPAddress { get { return new FunctionID("121-020-010-010-002"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_DatabaseName_POS { get { return new FunctionID("121-020-010-011-001"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_QueqeNo { get { return new FunctionID("121-020-010-012-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_DayCountSaleCups { get { return new FunctionID("121-020-010-013-000"); } }
        public static FunctionID Sale_PopupSaleProcessScreen_ContentonPOSScreen_ServeType { get { return new FunctionID("121-020-010-014-000"); } }
        public static FunctionID Sale_BeforeInputProductItem_CheckSaleCashIn { get { return new FunctionID("121-030-010-000-000"); } }
        public static FunctionID Sale_BeforeInputProductItem_DefaultCursorPosition { get { return new FunctionID("121-030-050-000-000"); } }
        public static FunctionID Sale_InputTableNo { get { return new FunctionID("121-040-000-000-000"); } }
        public static FunctionID Sale_InputServeTypeofReceipt { get { return new FunctionID("121-050-000-000-000"); } }
        public static FunctionID Sale_Member { get { return new FunctionID("121-060-000-000-000"); } }
        public static FunctionID Sale_Member_Search { get { return new FunctionID("121-060-001-000-000"); } }
        public static FunctionID Sale_Member_Search_ID { get { return new FunctionID("121-060-001-001-000"); } }
        public static FunctionID Sale_Member_Search_PhoneNo { get { return new FunctionID("121-060-001-002-000"); } }
        public static FunctionID Sale_Member_Search_CitizenID { get { return new FunctionID("121-060-001-003-000"); } }
        public static FunctionID Sale_Member_Search_TaxID { get { return new FunctionID("121-060-001-004-000"); } }
        public static FunctionID Sale_Member_Search_Firstname { get { return new FunctionID("121-060-001-005-000"); } }
        public static FunctionID Sale_Member_Search_Lastname { get { return new FunctionID("121-060-001-006-000"); } }
        public static FunctionID Sale_Member_Search_EnglishFirstname { get { return new FunctionID("121-060-001-007-000"); } }
        public static FunctionID Sale_Member_Search_EnglishLastname { get { return new FunctionID("121-060-001-008-000"); } }
        public static FunctionID Sale_Member_Display { get { return new FunctionID("121-060-002-000-000"); } }
        //public static FunctionID Sale_Member_Display { get { return new FunctionID("121-060-001-000-000"); } }
        public static FunctionID Sale_Member_Search_Data { get { return new FunctionID("121-060-003-000-000"); } }
        public static FunctionID Sale_Employee { get { return new FunctionID("121-070-000-000-000"); } }
        public static FunctionID Sale_Employee_Search_EmployeeID { get { return new FunctionID("121-070-020-001-000"); } }
        public static FunctionID Sale_Employee_Display { get { return new FunctionID("121-070-030-000-000"); } }
        public static FunctionID Sale_PCMan { get { return new FunctionID("121-080-000-000-000"); } }
        public static FunctionID Sale_PCMan_Search_PCMan { get { return new FunctionID("121-080-020-010-001"); } }
        public static FunctionID Sale_PCMan_Display { get { return new FunctionID("121-080-030-000-000"); } }
        public static FunctionID Sale_InputSaleItem_CallOrder_Temporary { get { return new FunctionID("121-090-010-001-000"); } }
        public static FunctionID Sale_InputSaleItem_CallOrder_OrderShoppingOnline { get { return new FunctionID("121-090-010-002-000"); } }
        public static FunctionID Sale_InputSaleItem_CallOrder_FastScan { get { return new FunctionID("121-090-010-003-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_NormalSale { get { return new FunctionID("121-090-020-001-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_BillPayment { get { return new FunctionID("121-090-020-002-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_TopUpService { get { return new FunctionID("121-090-020-003-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_SimCard { get { return new FunctionID("121-090-020-004-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_Kerry { get { return new FunctionID("121-090-020-005-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_AirlineTicket { get { return new FunctionID("121-090-020-006-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_Deposit { get { return new FunctionID("121-090-020-007-000"); } }
        public static FunctionID Sale_InputSaleItem_InputProduct_ReservedBlock { get { return new FunctionID("121-090-020-008-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_FreshFood { get { return new FunctionID("121-090-030-001-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_ReduceToClearRTC { get { return new FunctionID("121-090-030-002-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_Drugs { get { return new FunctionID("121-090-030-003-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_Electronics { get { return new FunctionID("121-090-030-004-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_NotSale { get { return new FunctionID("121-090-030-005-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_DangerDrug { get { return new FunctionID("121-090-030-006-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_Online { get { return new FunctionID("121-090-030-007-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_SaleLimit { get { return new FunctionID("121-090-030-008-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_OfferInstallation { get { return new FunctionID("121-090-030-009-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_OfferInsurance { get { return new FunctionID("121-090-030-010-000"); } }
        public static FunctionID Sale_InputSaleItem_CheckProduct_Bar0Bath { get { return new FunctionID("121-090-030-011-000"); } }
        public static FunctionID Sale_InputSaleItem_DisplayProductDesc_DisplayPromotionECampaign { get { return new FunctionID("121-090-040-001-000"); } }
        public static FunctionID Sale_InputSaleItem_DisplayProductDesc_ShowSuggestMessage { get { return new FunctionID("121-090-040-002-000"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_ChangeItem { get { return new FunctionID("121-090-050-001-000"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_ChangeItem_StepChange { get { return new FunctionID("121-090-050-001-001"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_EditPrice { get { return new FunctionID("121-090-050-002-000"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_EditPrice_InputNewPrice { get { return new FunctionID("121-090-050-002-010"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_EditPrice_InputReason { get { return new FunctionID("121-090-050-002-040"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_DeleteItem { get { return new FunctionID("121-090-050-003-000"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_DeleteItem_OverLimit { get { return new FunctionID("121-090-050-003-020"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_DeleteItem_InputReason { get { return new FunctionID("121-090-050-003-030"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_DiscountByProductManual { get { return new FunctionID("121-090-050-004-000"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_AdditionalInput_SpecialProduct { get { return new FunctionID("121-090-050-005-001"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_AdditionalInput_Problem { get { return new FunctionID("121-090-050-005-002"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_AdditionalInput_TakeoutDocument { get { return new FunctionID("121-090-050-005-003"); } }
        public static FunctionID Sale_InputSaleItem_EditProduct_AdditionalInput_Condiment { get { return new FunctionID("121-090-050-005-004"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder { get { return new FunctionID("121-100-010-000-000"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder_CheckLimit { get { return new FunctionID("121-100-010-010-000"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder_OverLimit { get { return new FunctionID("121-100-010-020-000"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder_InputReason { get { return new FunctionID("121-100-010-030-000"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction { get { return new FunctionID("121-100-010-040-000"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder_SaveCancelTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("121-100-010-040-001"); } }
        public static FunctionID Sale_CancelWhileSale_CancelOrder_PrintCancelDocument { get { return new FunctionID("121-100-010-050-000"); } }
        public static FunctionID Sale_CancelWhileSale_HoldOrder { get { return new FunctionID("121-100-020-000-000"); } }
        public static FunctionID Sale_CancelWhileSale_HoldOrder_SaveOrderNoType { get { return new FunctionID("121-100-020-010-000"); } }
        public static FunctionID Sale_CancelWhileSale_HoldOrder_SaveHoldTransaction { get { return new FunctionID("121-100-020-020-000"); } }
        public static FunctionID Sale_CancelWhileSale_HoldOrder_SaveHoldTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("121-100-020-020-010"); } }
        public static FunctionID Sale_CancelWhileSale_HoldOrder_PrintDocument { get { return new FunctionID("121-100-020-050-000"); } }
        public static FunctionID Sale_CancelWhileSale_CheckHolderOrder { get { return new FunctionID("121-030-060-000-000"); } }
        public static FunctionID Sale_CancelWhileSale_LoadHolderOrder { get { return new FunctionID("121-030-060-010-000"); } }
        public static FunctionID Sale_DiscountByReceiptManaul { get { return new FunctionID("121-110-000-000-000"); } }
        public static FunctionID Sale_DiscountByReceiptManaul_ChooseDiscount { get { return new FunctionID("121-110-010-000-000"); } }
        public static FunctionID Sale_TotalAmtDiff { get { return new FunctionID("121-185-010-000-000"); } }
        public static FunctionID Sale_ProcessBeforePayment { get { return new FunctionID("121-130-010-000-000"); } }
        public static FunctionID Sale_ProcessBeforePayment_ShowPromotion { get { return new FunctionID("121-120-010-000-000"); } }
        public static FunctionID Sale_ProcessBeforePayment_NoBag { get { return new FunctionID("121-120-020-000-000"); } }
        public static FunctionID Sale_CalculateDiscount { get { return new FunctionID("121-130-000-000-000"); } }
        public static FunctionID Sale_Redeem_Main { get { return new FunctionID("121-131-000-000-000"); } }
        public static FunctionID Sale_Redeem_Product { get { return new FunctionID("121-131-010-000-000"); } }
        public static FunctionID Sale_SaleOrderType { get { return new FunctionID("121-120-030-000-000"); } }
        public static FunctionID Sale_VoidDepo { get { return new FunctionID("121-275-000-000-000"); } }

        
        //public static FunctionID Sale_Redeem_Product_DisplayCustomer { get { return new FunctionID("121-131-010-001-000"); } }
        public static FunctionID Sale_Redeem_Product_DisplayCashier { get { return new FunctionID("121-131-010-010-001"); } }
        public static FunctionID Sale_Redeem_Product_DisplayCustomer { get { return new FunctionID("121-131-010-010-002"); } }

        public static FunctionID Sale_Redeem_Product_DisplayCustomer_ButtonPlusMinus { get { return new FunctionID("121-131-010-020-001"); } }
        public static FunctionID Sale_Redeem_Product_DisplayCashier_ButtonPlusMinus { get { return new FunctionID("121-131-010-020-002"); } }

        public static FunctionID Sale_Redeem_Discount { get { return new FunctionID("121-131-020-000-000"); } }
        //public static FunctionID Sale_Redeem_Discount_DisplayCashier { get { return new FunctionID("121-131-020-001-000"); } }
        public static FunctionID Sale_Redeem_Discount_DisplayCustomer { get { return new FunctionID("121-131-020-010-002"); } }
        public static FunctionID Sale_Redeem_Discount_DisplayCashier { get { return new FunctionID("121-131-020-010-001"); } }
        public static FunctionID Sale_Redeem_Discount_DisplayCustomer_ButtonYesNo { get { return new FunctionID("121-131-020-020-001"); } }
        public static FunctionID Sale_Redeem_Discount_DisplayCashier_ButtonYesNo { get { return new FunctionID("121-131-020-020-002"); } }

        public static FunctionID Sale_Redeem_Cash { get { return new FunctionID("121-131-030-000-000"); } }
        //public static FunctionID Sale_Redeem_Cash_DisplayCustomer { get { return new FunctionID("121-131-030-001-000"); } }
        public static FunctionID Sale_Redeem_Cash_DisplayCustomer { get { return new FunctionID("121-131-030-010-002"); } }
        public static FunctionID Sale_Redeem_Cash_DisplayCashier { get { return new FunctionID("121-131-030-010-001"); } }
        public static FunctionID Sale_Redeem_Cash_DisplayCustomer_ButtonPlusMinus { get { return new FunctionID("121-131-030-020-001"); } }
        public static FunctionID Sale_Redeem_Cash_DisplayCashier_ButtonPlusMinus { get { return new FunctionID("121-131-030-020-002"); } }

        public static FunctionID Sale_Redeem_VerifyID { get { return new FunctionID("121-131-040-002-000"); } }
        public static FunctionID Sale_Redeem_KeyBoard { get { return new FunctionID("121-131-040-003-000"); } }

        public static FunctionID Sale_Redeem_Main2 { get { return new FunctionID("121-285-000-000-000"); } }

        public static FunctionID Sale_Redeem_Coupon { get { return new FunctionID("121-285-010-000-000"); } }
        //public static FunctionID Sale_Redeem_Coupon_DisplayCustomer { get { return new FunctionID("121-285-010-001-000"); } }
        public static FunctionID Sale_Redeem_Coupon_DisplayCustomer { get { return new FunctionID("121-285-101-010-002"); } }
        public static FunctionID Sale_Redeem_Coupon_DisplayCashier { get { return new FunctionID("121-285-101-010-001"); } }
        public static FunctionID Sale_Redeem_Coupon_DisplayCustomer_ButtonPlusMinus { get { return new FunctionID("121-285-010-010-001"); } }
        public static FunctionID Sale_Redeem_Coupon_DisplayCashier_ButtonPlusMinus { get { return new FunctionID("121-285-010-010-002"); } }

        //public static FunctionID Sale_Redeem_Coupon_VerifyID { get { return new FunctionID("121-285-020-001-000"); } }
        public static FunctionID Sale_Redeem_Coupon_VerifyID { get { return new FunctionID("121-285-040-000-000"); } }
        //public static FunctionID Sale_Redeem_Coupon_KeyBoard { get { return new FunctionID("121-285-020-002-000"); } }
        public static FunctionID Sale_Redeem_Coupon_KeyBoard { get { return new FunctionID("121-285-040-020-000"); } }
        public static FunctionID Sale_Print_ExportPermit { get { return new FunctionID("121-280-050-000-000"); } }

        public static FunctionID Sale_CalculateVAT { get { return new FunctionID("121-140-000-000-000"); } }
        public static FunctionID Sale_DisplayPaymentMenu { get { return new FunctionID("121-141-000-000-000"); } }
        public static FunctionID Sale_InputBigCGiftVoucher { get { return new FunctionID("121-150-000-000-000"); } }
        public static FunctionID Sale_InputBigCCoupon { get { return new FunctionID("121-160-000-000-000"); } }
        public static FunctionID Sale_CalculateNonCash { get { return new FunctionID("121-170-000-000-000"); } }
        public static FunctionID Sale_ShowPaymentAmount { get { return new FunctionID("121-185-000-000-000"); } }
        public static FunctionID Sale_InputPaymentType_ShowAllowType { get { return new FunctionID("121-190-001-000-000"); } }
        public static FunctionID Sale_InputPaymentType_ChooseType { get { return new FunctionID("121-190-002-000-000"); } }
        public static FunctionID Sale_AutoVoid_BscanC { get { return new FunctionID("121-190-002-060-060"); } }

        public static FunctionID Sale_DiscountNonCash { get { return new FunctionID("121-200-000-000-000"); } }
        public static FunctionID Sale_ShowNonCashAmount { get { return new FunctionID("121-210-000-000-000"); } }
        public static FunctionID Sale_ConfirmPaymentAmount { get { return new FunctionID("121-220-000-000-000"); } }
        public static FunctionID Sale_OpenDrawerAndRecordTime { get { return new FunctionID("121-230-000-000-000"); } }
        public static FunctionID Sale_Change_Display { get { return new FunctionID("121-240-010-000-000"); } }
        public static FunctionID Sale_Change_InputChange { get { return new FunctionID("121-240-020-000-000"); } }
        public static FunctionID Sale_Change_EditChange { get { return new FunctionID("121-240-021-000-000"); } }
        public static FunctionID Sale_PushCashInDrawer { get { return new FunctionID("121-250-000-000-000"); } }
        public static FunctionID Sale_SaveSaleTransaction { get { return new FunctionID("121-260-000-000-000"); } }
        public static FunctionID Sale_SaveSaleTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("121-260-001-000-000"); } }
        public static FunctionID Sale_ProcessAfterSaveSaleTransaction { get { return new FunctionID("121-270-000-000-000"); } }
        public static FunctionID Sale_Document_NoReceipt { get { return new FunctionID("121-280-010-000-000"); } }
        public static FunctionID Sale_Document_ReceiptABB { get { return new FunctionID("121-280-020-000-000"); } }
        public static FunctionID Sale_Document_FullTax { get { return new FunctionID("121-280-030-000-000"); } }
        public static FunctionID Sale_Document_Order_Coffee { get { return new FunctionID("121-280-040-001-000"); } }
        public static FunctionID Sale_Document_Order_Food { get { return new FunctionID("121-280-040-002-000"); } }
        public static FunctionID Sale_CloseDrawerAndRecordTime { get { return new FunctionID("121-290-000-000-000"); } }
        public static FunctionID Sale_RedeemBigCardPointforCoupon { get { return new FunctionID("121-300-000-000-000"); } }
        public static FunctionID Sale_StandByNextReceipt { get { return new FunctionID("121-310-000-000-000"); } }
        public static FunctionID Sale_CheckCashdrawer { get { return new FunctionID("121-285-000-000-000", "TimeOutDeviceCashDrawer"); } }
        public static FunctionID Sale_CheckEmployee { get { return new FunctionID("121-070-010-000-000"); } }
        public static FunctionID Sale_CheckPCMan { get { return new FunctionID("121-080-010-010-001"); } }

        public static FunctionID Sale_QRPayment_Limit_Amount { get { return new FunctionID("121-190-002-050-010", "QRPAYMENT_LIMIT_AMT"); } }

        public static FunctionID Sale_AutoVoid_API_POS_BSC_VOID { get { return new FunctionID("121-190-002-050-000"); } }
        //To Do change FunctionID
        public static FunctionID Sale_AuthQRPaymentOffline { get { return new FunctionID("121-190-002-050-010"); } }

        public static FunctionID Sale_AuthQRPaymentCSB { get { return new FunctionID("121-190-002-050-080"); } }
        public static FunctionID Sale_AuthQRPaymentCSB2 { get { return new FunctionID("121-190-002-050-085"); } }
        
        public static FunctionID Sale_CheckPrinterInvoiceType { get { return new FunctionID("121-030-045-000-000"); } }
        
        public static FunctionID Return_SelectReturnMenu { get { return new FunctionID("122-010-000-000-000"); } }
        public static FunctionID Return_SelectReturnTypeMenu_ByReceipt { get { return new FunctionID("122-010-010-000-000"); } }
        public static FunctionID Return_SelectReturnTypeMenu_ByProduct { get { return new FunctionID("122-010-020-000-000"); } }
        public static FunctionID Return_GetRunningNo { get { return new FunctionID("122-020-000-000-000"); } }
        public static FunctionID Return_CheckOpenDayofTillStatus { get { return new FunctionID("122-030-000-000-000"); } }
        public static FunctionID Return_GetMessageCashier { get { return new FunctionID("122-035-000-000-000"); } }
        public static FunctionID Return_InputCustomer_ByMember { get { return new FunctionID("122-040-010-000-000"); } }
        public static FunctionID Return_InputCustomer_ByMember_Search_MemberID { get { return new FunctionID("122-040-010-010-001"); } }
        public static FunctionID Return_InputCustomer_ByMember_Search_PhoneNo { get { return new FunctionID("122-040-010-010-002"); } }
        public static FunctionID Return_InputCustomer_ByMember_Search_CitizenID { get { return new FunctionID("122-040-010-010-003"); } }
        public static FunctionID Return_InputCustomer_ByMember_Search_TaxID { get { return new FunctionID("122-040-010-010-004"); } }
        public static FunctionID Return_InputCustomer_InputCustomer { get { return new FunctionID("122-040-020-000-000"); } }
        public static FunctionID Return_InputCustomer_Display { get { return new FunctionID("122-040-030-000-000"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_SearchByReceiptNo { get { return new FunctionID("122-050-010-010-000"); } }

        public static FunctionID Return_InputReturnItem_ByReceipt_SearchByReceiptNo_ReceiptNo { get { return new FunctionID("122-050-010-010-001"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_SearchByReceiptNo_ProductCode { get { return new FunctionID("122-050-010-010-002"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_SearchByReceiptNo_MemberID { get { return new FunctionID("122-050-010-010-003"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_SearchByReceiptNo_PhoneNO { get { return new FunctionID("122-050-010-010-004"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_SearchByReceiptNo_CitizenID { get { return new FunctionID("122-050-010-010-005"); } }

        public static FunctionID Return_InputReturnItem_ByReceipt_CheckReturnable { get { return new FunctionID("122-050-010-020-000"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_InputAmountReturn_ReturnAllItem { get { return new FunctionID("122-050-010-030-010"); } }
        public static FunctionID Return_InputReturnItem_ByReceipt_InputAmountReturn_ReturnSomeItem { get { return new FunctionID("122-050-010-030-020"); } }
        public static FunctionID Return_InputReturnItem_ByProduct_InputProduct_InputAmountReturn { get { return new FunctionID("122-050-020-010-010"); } }
        public static FunctionID Return_InputReturnItem_ByProduct_InputProduct_InputReturnProductCode { get { return new FunctionID("122-050-020-010-020"); } }
        public static FunctionID Return_InputReturnItem_ByProduct_InputProduct_CheckReturnable { get { return new FunctionID("122-050-020-010-030"); } }
        public static FunctionID Return_InputReturnItem_ByProduct_InputProduct_EditPrice { get { return new FunctionID("122-050-020-010-040"); } }
        public static FunctionID Return_InputReturnItem_ByProduct_DeleteProduct { get { return new FunctionID("122-050-020-020-000"); } }
        public static FunctionID Return_InputReturnReason { get { return new FunctionID("122-060-000-000-000"); } }
        public static FunctionID Return_SuggestReturnPaymentType { get { return new FunctionID("122-070-000-000-000"); } }
        public static FunctionID Return_DisplayReturnPaymentTypeAndAmount { get { return new FunctionID("122-080-000-000-000"); } }
        public static FunctionID Return_ConfirmReturn { get { return new FunctionID("122-090-000-000-000"); } }
        public static FunctionID Return_CheckAmountZeroPrice { get { return new FunctionID("122-095-000-000-000"); } }
        public static FunctionID Return_OpenDrawerAndRecordTime { get { return new FunctionID("122-100-000-000-000"); } }
        public static FunctionID Return_SaveReturnTransaction { get { return new FunctionID("122-110-000-000-000"); } }
        public static FunctionID Return_SaveReturnTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("122-110-001-000-000"); } }
        public static FunctionID Return_ProcessAfterReturnTransaction { get { return new FunctionID("122-120-000-000-000"); } }
        public static FunctionID Return_Document_CNCreditNote { get { return new FunctionID("122-130-010-000-000"); } }
        public static FunctionID Return_PrintThermal { get { return new FunctionID("122-130-010-001-000"); } }
        public static FunctionID Return_PrintFullTax { get { return new FunctionID("122-130-010-002-000"); } }

        public static FunctionID Return_Document_CopyOfCNCreditNote { get { return new FunctionID("122-130-020-000-000"); } }
        public static FunctionID Return_CloseDrawerAndRecordTime { get { return new FunctionID("122-140-000-000-000"); } }
        public static FunctionID Return_GotoMainMenu { get { return new FunctionID("122-150-000-000-000"); } }
        public static FunctionID Void_SelectVoidMenu { get { return new FunctionID("123-010-000-000-000"); } }
        public static FunctionID Void_GetRunningNo { get { return new FunctionID("123-020-000-000-000"); } }
        public static FunctionID Void_CheckOpenDayofTillStatus { get { return new FunctionID("123-030-000-000-000"); } }
        public static FunctionID Void_GetMessageCashier { get { return new FunctionID("123-035-000-000-000"); } }
        public static FunctionID Void_InputReceiptNo { get { return new FunctionID("123-040-000-000-000"); } }
        public static FunctionID Void_CheckReceipt_OnlySameTill { get { return new FunctionID("123-050-010-000-000"); } }
        public static FunctionID Void_CheckReceipt_OnlySameDate { get { return new FunctionID("123-050-020-000-000"); } }
        public static FunctionID Void_InputReason { get { return new FunctionID("123-060-000-000-000"); } }
        public static FunctionID Void_ConfirmVoid { get { return new FunctionID("123-070-000-000-000"); } }
        public static FunctionID Void_InputUserApproveVoid { get { return new FunctionID("123-080-000-000-000"); } }
        public static FunctionID Void_OpenDrawerAndRecordTime { get { return new FunctionID("123-090-000-000-000"); } }
        public static FunctionID Void_SaveVoidTransaction { get { return new FunctionID("123-100-000-000-000"); } }
        public static FunctionID Void_SaveVoidTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("123-100-001-000-000"); } }
        public static FunctionID Void_ProcessAfterVoidTransaction { get { return new FunctionID("123-110-000-000-000"); } }
        public static FunctionID Void_PrintVoidDocument { get { return new FunctionID("123-120-000-000-000"); } }
        public static FunctionID Void_CloseDrawerAndRecordTime { get { return new FunctionID("123-130-000-000-000"); } }
        public static FunctionID Void_GotoMainMenu { get { return new FunctionID("123-140-000-000-000"); } }
        public static FunctionID Void_SaveVoidDepositTransaction { get { return new FunctionID("131-020-070-000-000"); } }


        //Deposit
        public static FunctionID Deposit_Payment { get { return new FunctionID("131-010-000-000-000"); } }
        public static FunctionID Deposit_CheckAuthorize { get { return new FunctionID("131-010-010-000-000"); } }
        public static FunctionID Deposit_GetRunning { get { return new FunctionID("131-010-011-000-000"); } }
        public static FunctionID Deposit_CheckOpenDayofTillStatus { get { return new FunctionID("131-010-012-000-000"); } }
        public static FunctionID Deposit_GetMessageCashier { get { return new FunctionID("131-010-013-000-000"); } }
        public static FunctionID Deposit_GetDepositCustomerType { get { return new FunctionID("131-010-040-000-000"); } }

        public static FunctionID Deposit_ShowDetailCustomer { get { return new FunctionID("131-010-040-040-000"); } }
        public static FunctionID Deposit_RCV2FULLFORM_FFTI { get { return new FunctionID("131-010-130-020-000"); } }
        public static FunctionID Deposit_InputVoid { get { return new FunctionID("131-020-030-000-000"); } }
        

        public static FunctionID Deposit_SearchMember1 { get { return new FunctionID("131-010-040-010-000"); } }
        public static FunctionID Deposit_SearchMember1_Search_PhoneNo { get { return new FunctionID("131-010-040-010-001"); } }
        public static FunctionID Deposit_SearchMember1_Search_TaxID { get { return new FunctionID("131-010-040-010-001"); } }

        public static FunctionID Deposit_SearchMember2 { get { return new FunctionID("131-010-040-020-000"); } }
        public static FunctionID Deposit_SearchMember2_Search_MemberID { get { return new FunctionID("131-010-040-020-001"); } }
        public static FunctionID Deposit_SearchMember2_Search_PhoneNo { get { return new FunctionID("131-010-040-020-002"); } }
        public static FunctionID Deposit_SearchMember2_Search_CitizenID { get { return new FunctionID("131-010-040-020-003"); } }

        public static FunctionID Deposit_SearchMember3 { get { return new FunctionID("131-010-040-030-000"); } }
        public static FunctionID Deposit_SearchMember3_Search_MemberID { get { return new FunctionID("131-010-040-030-001"); } }
        public static FunctionID Deposit_SearchMember3_Search_PhoneNo { get { return new FunctionID("131-010-040-030-002"); } }
        public static FunctionID Deposit_SearchMember3_Search_TaxID { get { return new FunctionID("131-010-040-030-003"); } }
        public static FunctionID ValidFFTI { get { return new FunctionID("131-010-150-030-000"); } }
        //ReceivePayment
        public static FunctionID ReceivePOD_Payment { get { return new FunctionID("132-010-060-001-000"); } }
        public static FunctionID ReceivePOD_CheckAuthorize { get { return new FunctionID("132-010-010-000-000"); } }
        public static FunctionID ReceivePOD_CheckOpenDay { get { return new FunctionID("132-010-012-000-000"); } }
        public static FunctionID ReceivePOD_GetOrder { get { return new FunctionID("132-010-030-000-000"); } }
        //public static FunctionID ReceivePOD_ShowQR { get { return new FunctionID("132-010-060-002-000"); } }
        public static FunctionID ReceivePOD_ShowQR { get { return new FunctionID("070-010-000-000-000"); } }
        public static FunctionID ReceivePOD_SaveChange { get { return new FunctionID("132-010-120-000-000"); } }
        public static FunctionID ReceivePOD_PrintReceipt { get { return new FunctionID("132-010-130-020-000"); } }
        public static FunctionID ReceivePOD_SyncToDataTank { get { return new FunctionID("132-010-120-010-000"); } }
        public static FunctionID ReceivePOD_VoidReceipt { get { return new FunctionID("132-020-080-000-000"); } }
        public static FunctionID ReceivePOD_PrintVoidReceipt { get { return new FunctionID("132-020-100-000-000"); } }
        public static FunctionID ReceivePOD_GetRunning { get { return new FunctionID("132-010-011-000-000"); } }
        public static FunctionID ReceivePOD_GetRunningCallAPI { get { return new FunctionID("132-010-120-020-000"); } }
        public static FunctionID ReceivePOD_InputVoid { get { return new FunctionID("132-020-040-000-000"); } }
        public static FunctionID ReceivePOD_RollBack { get { return new FunctionID("132-010-120-020-000"); } }
        public static FunctionID ReceivePOD_CancelVoid { get { return new FunctionID("132-020-080-020-000"); } }

        public static FunctionID CreditSale_Payment { get { return new FunctionID("133-010-060-001-000"); } }
        public static FunctionID CreditSale_CheckAuthorize { get { return new FunctionID("133-010-010-000-000"); } }
        public static FunctionID CreditSale_CheckOpenDay { get { return new FunctionID("133-010-012-000-000"); } }
        public static FunctionID CreditSale_GetRunning { get { return new FunctionID("133-010-011-000-000"); } }
        public static FunctionID CreditSale_GetMessageCashier { get { return new FunctionID("133-010-013-000-000"); } }
        public static FunctionID CreditSale_Print { get { return new FunctionID("133-010-130-020-000"); } }
        public static FunctionID CreditSale_APIAR { get { return new FunctionID("133-010-120-020-000"); } }

        public static FunctionID CreditSale_GetRunningCallAPI { get { return new FunctionID("133-010-120-020-000"); } }
        public static FunctionID CreditSale_PrintVoidReceipt { get { return new FunctionID("133-020-100-000-000"); } }
        public static FunctionID CreditSale_CreditInputReason { get { return new FunctionID("133-020-040-000-000"); } }
        public static FunctionID CreditSale_CancelVoid { get { return new FunctionID("133-020-080-010-000"); } }

        public static FunctionID CashOut_SelectCashOutMenu { get { return new FunctionID("161-010-000-000-000"); } }
        public static FunctionID CashOut_GetRunningNo { get { return new FunctionID("161-011-000-000-000"); } }
        public static FunctionID CashOut_CheckOpenDayofTillStatus { get { return new FunctionID("161-012-000-000-000"); } }
        public static FunctionID CashOut_CheckUserIDPasswordCurrentCashier { get { return new FunctionID("161-020-000-000-000"); } }
        public static FunctionID CashOut_DisplayCashOutMenu { get { return new FunctionID("161-030-000-000-000"); } }
        public static FunctionID CashOut_OpenDrawerAndRecordTime { get { return new FunctionID("161-040-000-000-000"); } }
        public static FunctionID CashOut_PullCashFromDrawer { get { return new FunctionID("161-050-000-000-000"); } }
        public static FunctionID CashOut_NormalChange { get { return new FunctionID("161-060-000-000-000"); } }
        public static FunctionID CashOut_NormalChange_CheckData { get { return new FunctionID("161-060-010-000-000"); } }

        public static FunctionID CashOut_NormalChange_Type { get { return new FunctionID("161-060-020-000-000", "CashOutFloatAmtType"); } }
        public static FunctionID CashOut_NormalChange_InputMethod { get { return new FunctionID("161-060-030-000-000", "CashOutFloatAmtMethod"); } }
        public static FunctionID CashOut_NormalChange_DefaultAmount { get { return new FunctionID("161-060-040-000-000", "CashOutFloatAmtDefault"); } }
        public static FunctionID CashOut_NormalChange_DefaultAmount_Editable { get { return new FunctionID("161-060-040-001-000"); } }


        public static FunctionID CashOut_NormalChange_CheckAmount_Minimum { get { return new FunctionID("161-060-050-001-000"); } }
        public static FunctionID CashOut_NormalChange_CheckAmount_Maximum { get { return new FunctionID("161-060-050-002-000"); } }
        public static FunctionID CashOut_NormalChange_PopupSummaryPage { get { return new FunctionID("161-060-060-000-000"); } }
        public static FunctionID CashOut_NormalChange_Submit { get { return new FunctionID("161-060-070-000-000"); } }
        public static FunctionID CashOut_NormalChange_CloseDrawerAndRecordTime { get { return new FunctionID("161-060-080-000-000"); } }
        public static FunctionID CashOut_NormalChange_SaveCashOutTransaction { get { return new FunctionID("161-060-090-000-000"); } }
        public static FunctionID CashOut_NormalChange_SaveCashOutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("161-060-090-001-000"); } }
        public static FunctionID CashOut_NormalChange_PrintCashoutDocument { get { return new FunctionID("161-060-100-000-000"); } }
        public static FunctionID CashOut_AdditionalChange { get { return new FunctionID("161-070-000-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_CheckData { get { return new FunctionID("161-070-010-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_Type { get { return new FunctionID("161-070-020-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_InputMethod { get { return new FunctionID("161-070-030-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_DefalutAmount { get { return new FunctionID("161-070-040-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_DefalutAmount_Editable { get { return new FunctionID("161-070-040-001-000"); } }
        public static FunctionID CashOut_AdditionalChange_CheckAmount_Minimum { get { return new FunctionID("161-070-050-001-000"); } }
        public static FunctionID CashOut_AdditionalChange_CheckAmount_Maximum { get { return new FunctionID("161-070-050-002-000"); } }
        public static FunctionID CashOut_AdditionalChange_PopupSummary { get { return new FunctionID("161-070-060-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_Submit { get { return new FunctionID("161-070-070-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_CloseDrawerAndRecordTime { get { return new FunctionID("161-070-080-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_SaveCashOutTransaction { get { return new FunctionID("161-070-090-000-000"); } }
        public static FunctionID CashOut_AdditionalChange_SaveCashOutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("161-070-090-001-000"); } }
        public static FunctionID CashOut_AdditionalChange_PrintCashoutDocument { get { return new FunctionID("161-070-100-000-000"); } }
        public static FunctionID CashOut_Sale { get { return new FunctionID("161-080-000-000-000"); } }
        public static FunctionID CashOut_Sale_CheckData { get { return new FunctionID("161-080-010-000-000"); } }
        public static FunctionID CashOut_Sale_DisplayPaymentType { get { return new FunctionID("161-080-020-000-000"); } }
        public static FunctionID CashOut_Sale_InputCashMethod { get { return new FunctionID("161-080-030-000-000"); } }
        public static FunctionID CashOut_Sale_InputMoneyBag { get { return new FunctionID("161-080-040-000-000"); } }
        public static FunctionID CashOut_Sale_CheckInputMoneyBag { get { return new FunctionID("161-080-045-000-000"); } }
        public static FunctionID CashOut_Sale_InputCashout { get { return new FunctionID("161-080-050-000-000"); } }
        public static FunctionID CashOut_Sale_ConfirmSaleAmount { get { return new FunctionID("161-080-060-000-000"); } }
        public static FunctionID CashOut_Sale_DisplaySummary { get { return new FunctionID("161-080-070-000-000"); } }
        public static FunctionID CashOut_Sale_Submit { get { return new FunctionID("161-080-080-000-000"); } }
        public static FunctionID CashOut_Sale_CloseDrawerAndRecordTime { get { return new FunctionID("161-080-090-000-000"); } }
        public static FunctionID CashOut_Sale_SaveCashOutTransaction { get { return new FunctionID("161-080-100-000-000"); } }
        public static FunctionID CashOut_Sale_SaveCashOutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("161-080-100-001-000"); } }
        public static FunctionID CashOut_Sale_PrintCashoutDocument { get { return new FunctionID("161-080-110-000-000"); } }
        public static FunctionID CashOut_CashierMessage_Status { get { return new FunctionID("161-013-000-000-000"); } }
        public static FunctionID CancelCashOut { get { return new FunctionID("181-080-000-000-000"); } }
        public static FunctionID CancelCashOut_DisplayCashOut { get { return new FunctionID("181-080-010-000-000"); } }
        public static FunctionID CashOut_Sale_ConfirmCloseTill { get { return new FunctionID("161-080-120-000-000"); } }
        public static FunctionID EndOfShift_SelectEndOfShiftMenu { get { return new FunctionID("162-010-000-000-000"); } }
        public static FunctionID EndOfShift_GetRunningNo { get { return new FunctionID("162-011-000-000-000"); } }
        public static FunctionID EndOfShift_CheckOpenDayofTillStatus { get { return new FunctionID("162-012-000-000-000"); } }
        public static FunctionID EndOfShift_GetMessageCashier { get { return new FunctionID("162-013-000-000-000"); } }
        public static FunctionID EndOfShift_PopupEndOfShiftProcessScreen { get { return new FunctionID("162-020-000-000-000"); } }
        public static FunctionID EndOfShift_ConfirmEndOfShiftProcess { get { return new FunctionID("162-030-000-000-000"); } }
        public static FunctionID EndOfShift_ConfirmEndOfShiftProcess_UserAuthorize { get { return new FunctionID("162-030-010-000-000"); } }
        public static FunctionID EndOfShift_SaveEndOfShiftTransaction { get { return new FunctionID("162-040-000-000-000"); } }
        public static FunctionID EndOfShift_SaveEndOfShiftTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("162-040-001-000-000"); } }
        public static FunctionID EndOfShift_PrintEndOfShiftDocument { get { return new FunctionID("162-050-000-000-000"); } }
        public static FunctionID EndOfShift_SaveLogoutTransaction { get { return new FunctionID("162-080-000-000-000"); } }
        public static FunctionID EndOfShift_SaveLogoutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("162-080-001-000-000"); } }
        public static FunctionID EndOfShift_BacktoMainMenu { get { return new FunctionID("162-070-000-000-000"); } }
        public static FunctionID EndOfTill_SelectEndOfTillMenu { get { return new FunctionID("163-010-000-000-000"); } }
        public static FunctionID EndOfTill_GetRunningNo { get { return new FunctionID("163-011-000-000-000"); } }
        public static FunctionID EndOfTill_CheckOpenDayofTillStatus { get { return new FunctionID("163-012-000-000-000"); } }
        public static FunctionID EndOfTill_GetMessageCashier { get { return new FunctionID("163-013-000-000-000"); } }
        public static FunctionID EndOfTill_PopupEndOfTillProcessScreen { get { return new FunctionID("163-020-000-000-000"); } }
        public static FunctionID EndOfTill_ConfirmEndOfTillProcess { get { return new FunctionID("163-030-000-000-000"); } }
        public static FunctionID EndOfTill_ConfirmEndOfTillProcess_UserAuthorize { get { return new FunctionID("163-030-010-000-000"); } }
        public static FunctionID EndOfTill_SaveEndOfTillTransaction { get { return new FunctionID("163-040-000-000-000"); } }
        public static FunctionID EndOfTill_SaveEndOfTillTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("163-040-001-000-000"); } }
        public static FunctionID EndOfTill_PrintEndOfTillDocument { get { return new FunctionID("163-050-000-000-000"); } }
        public static FunctionID EndOfTill_AutoLogout { get { return new FunctionID("163-060-000-000-000"); } }
        public static FunctionID EndOfTill_SaveLogoutTransaction { get { return new FunctionID("163-070-000-000-000"); } }
        public static FunctionID EndOfTill_SaveLogoutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("163-070-001-000-000"); } }
        public static FunctionID EndOfTill_BacktoMainMenu { get { return new FunctionID("163-080-000-000-000"); } }
        public static FunctionID Tool_SelectToolMenu { get { return new FunctionID("181-000-000-000-000"); } }
        public static FunctionID Tool_ChangePassword { get { return new FunctionID("181-010-000-000-000"); } }
        public static FunctionID Tool_CheckProduct { get { return new FunctionID("181-020-000-000-000"); } }
        public static FunctionID Tool_ScanProduct { get { return new FunctionID("181-020-020-000-000"); } }
        public static FunctionID Tool_RerunBeginOfDay { get { return new FunctionID("181-030-000-000-000"); } }
        public static FunctionID Tool_FullTax { get { return new FunctionID("181-040-000-000-000"); } }
        public static FunctionID Tool_AdjustPrice { get { return new FunctionID("181-050-000-000-000"); } }
        public static FunctionID Tool_RegisterBigCard { get { return new FunctionID("181-060-000-000-000"); } }
        public static FunctionID Tool_RedeemPoint { get { return new FunctionID("181-070-000-000-000"); } }
        public static FunctionID Report_SelectReportMenu { get { return new FunctionID("182-010-000-000-000"); } }
        public static FunctionID Report_CheckOpenDayofTillStatus { get { return new FunctionID("182-012-000-000-000"); } }
        public static FunctionID Report_CheckCurrentDaySale { get { return new FunctionID("182-020-000-000-000"); } }
        public static FunctionID Report_CheckCurrentDaySale_ReportOtherTill { get { return new FunctionID("182-020-010-000-000"); } }
        public static FunctionID Report_CheckCurrentDaySale_ReportOtherTillList { get { return new FunctionID("182-020-010-010-000"); } }
        public static FunctionID Report_CheckCurrentDaySale_PrintReportDocument { get { return new FunctionID("182-020-020-000-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt{ get { return new FunctionID("182-030-000-000-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_SearchReceipt { get { return new FunctionID("182-030-020-000-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_ReportOtherTill { get { return new FunctionID("182-030-010-000-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_ReportOtherTillList { get { return new FunctionID("182-030-010-010-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByTillNo { get { return new FunctionID("182-030-020-010-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByReceiptNo { get { return new FunctionID("182-030-020-020-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByProductCode { get { return new FunctionID("182-030-020-030-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByProductName { get { return new FunctionID("182-030-020-040-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByMemberID { get { return new FunctionID("182-030-020-050-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByMemberName { get { return new FunctionID("182-030-020-060-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_Search_SearchByPaymentType { get { return new FunctionID("182-030-020-070-000"); } }
        public static FunctionID Report_CheckCurrentDayReceipt_PrintReceiptCopy { get { return new FunctionID("182-030-030-000-000"); } }
        public static FunctionID Logout_SaveLogoutTransaction { get { return new FunctionID("199-010-000-000-000"); } }
        public static FunctionID Logout_SaveLogoutTransaction_SynchSaleTransactiontoDataTank { get { return new FunctionID("199-010-001-000-000"); } }

        public static FunctionID Login_AutoVoid { get { return new FunctionID("100-095-000-000-000"); } }
        public static FunctionID Login_AutoVoid_SaveCancelTrans { get { return new FunctionID("100-095-010-000-000"); } }
        public static FunctionID Login_AutoVoid_PrintCancel { get { return new FunctionID("100-095-010-001-000"); } }
        public static FunctionID Login_AutoVoid_SyncCancelToDataTank { get { return new FunctionID("100-095-010-002-000"); } }
        public static FunctionID Login_AutoVoid_SaveVoidTrans { get { return new FunctionID("100-095-020-000-000"); } }
        public static FunctionID Login_AutoVoid_ConcludeVoid { get { return new FunctionID("100-095-020-001-000"); } }
        public static FunctionID Login_AutoVoid_PrintVoidReceipt { get { return new FunctionID("100-095-020-002-000"); } }
        public static FunctionID Login_AutoVoid_SyncVoidToDataTank { get { return new FunctionID("100-095-020-003-000"); } }

        public static FunctionID Sale_AutoVoid { get { return new FunctionID("121-305-000-000-000"); } }
        public static FunctionID Sale_SaveVoidTrans { get { return new FunctionID("121-300-010-000-000"); } }
        public static FunctionID Sale_ConcludeVoid { get { return new FunctionID("121-305-010-001-000"); } }
        public static FunctionID Sale_SyncVoidToDataTank { get { return new FunctionID("121-305-010-002-000"); } }
        public static FunctionID Sale_PrintVoidReceipt { get { return new FunctionID("121-305-010-003-000"); } }

        //public static FunctionID PaymentForHirePurchase { get { return new FunctionID("002-004-002-000-000"); } }
        public static FunctionID PaymentForHirePurchase { get { return new FunctionID("010-030-010-000-000"); } }
        //public static FunctionID PaymentCoHirePurchase { get { return new FunctionID("002-004-001-000-000"); } }
        public static FunctionID PaymentCoHirePurchase { get { return new FunctionID("010-030-020-000-000"); } }
        //public static FunctionID PaymentDelete { get { return new FunctionID("002-005-000-000-000"); } }
        public static FunctionID PaymentDelete { get { return new FunctionID("010-040-020-000-000"); } }

        public string value { get; set; }
        public string parameterCode { get; set; }

        public FunctionID(string val)
        {
            if (val.Length > 15)
            {
                this.value = val.Replace("-", "").Trim();
            }
            else
            {
                this.value = val;
            }
        }

        public FunctionID(string val, string parameterCode)
        {
            if (val.Length > 15)
            {
                this.value = val.Replace("-", "");
            }
            else
            {
                this.value = val;
            }
            this.parameterCode = parameterCode;
        }

        public string formatValue
        {
            get
            {
                if (!string.IsNullOrEmpty(this.value) && this.value.Length == 15)
                {
                    return this.value.Substring(0, 3) + "-" + this.value.Substring(3, 3) + "-" + this.value.Substring(6, 3) + "-" + this.value.Substring(9, 3) + "-" + this.value.Substring(12, 3);
                }
                return this.value;
            }
        }

        public static bool operator ==(FunctionID lhs, FunctionID rhs)
        {
            return lhs.value.Equals(rhs.value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator !=(FunctionID lhs, FunctionID rhs)
        {
            return !lhs.value.Equals(rhs.value, StringComparison.OrdinalIgnoreCase);
        }
    }

}
