using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJCBCPOS_Data;
using BJCBCPOS_Model;
using System.Data;
using MMFSAPI;
using System.Threading;
using BJCBCPOS_Model.api.Request;

namespace BJCBCPOS_Process
{
    /// <summary>
    /// SaleProcess
    /// contains all process about sale
    /// </summary>
    public class SaleProcess
    {
        private SqlCommand command;
        private DataTable _dtSaleMain;
        private DataTable _dtPayment;
        private API_PayInvoiceAR _ObjPayInvoiceAR;

        public delegate void AlertMessage(ResponseCode resCode, string resMsg, string resHelp = "");

        public SaleProcess()
        {
            _dtSaleMain = BaseProcess.dtSaleMain;
            _dtPayment = BaseProcess.dtPayment;
            _ObjPayInvoiceAR = BaseProcess.ObjPayInvoiceAR;
            command = BaseProcess.command;
        }

        public void newTransaction()
        {
            command.newTransaction();
        }

        public void commit()
        {
            command.commit();
        }

        public void rollback()
        {
            command.rollback();
        }

        public StoreResult posDisplayContent()
        {
            try
            {
                return command.posDisplayContent();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.posDisplayContent");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public ProcessResult checkOpenDaySuccess(FunctionID functionId)
        {
            try
            {
                Profile openDay = ProgramConfig.getProfile(FunctionID.Sale_CheckOpenDayofTillStatus);
                if (openDay.policy == PolicyStatus.Work)
                {
                    StoreResult checkOpenDay = command.checkOpenDay(functionId);
                    if (!checkOpenDay.response.next)
                    {
                        return new ProcessResult(checkOpenDay, true);
                    }
                    else
                    {
                        return new ProcessResult(checkOpenDay, false);
                    }
                }
                return new ProcessResult(ResponseCode.Success, "", "", false);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.checkOpenDaySuccess");
                throw;              
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message, "", true);
            }
        }

        public int selectMaxRecTempDlyptrans(string refNo)
        {
            try
            {
                 return command.selectMaxRecTempDlyptrans(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectMaxRecTempDlyptrans");
                throw;
            }
        }

        public int selectMaxRecTempDlyptransForTypeP_FormPMCODE(string pm_Code)
        {
            try
            {
                return command.selectMaxRecTempDlyptransForTypeP_FormPMCODE(pm_Code);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectMaxRecTempDlyptransForTypeP_CHGD");
                throw;
            }
        }

        public int selectMaxRecTempDlyptransForTypeP_FXCU_Diff()
        {
            try
            {
                return command.selectMaxRecTempDlyptransForTypeP_FXCU_Diif();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectMaxRecTempDlyptransForTypeP_FXCU_Diff");
                throw;
            }
        }

        public StoreResult getRunning(FunctionID functionID, RunningReceiptID receiptID)
        {
            try
            {
                StoreResult res = command.getRunning(functionID, receiptID);
                if (res.otherData != null && res.otherData.Rows.Count > 0)
                {
                    if (receiptID == RunningReceiptID.SaleRef)
                    {
                        ProgramConfig.saleRefNo = res.otherData.Rows[0]["ReferenceNo"].ToString();
                        ProgramConfig.saleRefNoIni = res.otherData.Rows[0]["ReferenceNoINI"].ToString(); 
                    }                
                }
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getRunning");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult posCheckCashInSaleAmt()
        {
            try
            {
                return command.posCheckCashInSaleAmt();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.posCheckCashInSaleAmt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        private void updateTempSaleItemLanguage()
        {
            int rec;
            string pcd, vty;
            double qnt, amt, fds, discamt, upc;

            if (BaseProcess.currentTempLanguage != ProgramConfig.language)
            {
                // case switch language
                foreach (DataRow row in _dtSaleMain.Rows)
                {
                    rec = int.Parse(row["REC"].ToString());
                    pcd = row["PCD"].ToString();
                    vty = row["VTY"].ToString();
                    qnt = double.Parse(row["QNT"].ToString());
                    amt = double.Parse(row["AMT"].ToString());
                    fds = double.Parse(row["FDS"].ToString());
                    discamt = double.Parse(row["DISCAMT"].ToString());
                    upc = double.Parse(row["UPC"].ToString());
                    if (rec > 2000)
                    {
                        row["DisplayRec"] = (rec - 2000);
                        row["DisplayPrice"] = fds;
                        row["DisplayAmt"] = fds * qnt;
                    }
                    else
                    {
                        row["DisplayRec"] = rec;
                        row["DisplayPrice"] = upc;
                        row["DisplayAmt"] = amt;
                    }

                    if (vty == "0" || vty == "1")
                    {
                        StoreResult result = command.getProductDesc(FunctionID.Sale_InputSaleItem_InputProduct_NormalSale, pcd);
                        DataTable data = (DataTable)result.otherData;
                        if (result.response == ResponseCode.Success)
                        {
                            if (data != null)
                            {
                                for (int j = 0; j < data.Rows.Count; j++)
                                {
                                    row["ProductName"] = data.Rows[j]["PR_NAME"].ToString();
                                    row["PromotionName"] = data.Rows[j]["Promotion"].ToString();
                                    row["PromotionPrice"] = double.Parse(data.Rows[j]["PricePromotion"].ToString());
                                    row["TotalPrice"] = amt - discamt;
                                }
                            }
                        }
                    }

                }
                BaseProcess.currentTempLanguage = ProgramConfig.language;
                _dtSaleMain.AcceptChanges();
            }
            else           
            {
                // check empty display data
                DataRow[] empty = _dtSaleMain.Select("DisplayRec = 0 or ProductName = ''");
                foreach (DataRow row in empty)
                {
                    rec = int.Parse(row["REC"].ToString());
                    pcd = row["PCD"].ToString();
                    vty = row["VTY"].ToString();
                    qnt = double.Parse(row["QNT"].ToString());
                    amt = double.Parse(row["AMT"].ToString());
                    fds = double.Parse(row["FDS"].ToString());
                    discamt = double.Parse(row["DISCAMT"].ToString());
                    upc = double.Parse(row["UPC"].ToString());
                    if (rec > 2000 && rec < 10000)
                    {
                        row["DisplayRec"] = (rec - 2000);
                        row["DisplayPrice"] = fds;
                        row["DisplayAmt"] = fds * qnt;
                    }
                    else
                    {
                        row["DisplayRec"] = rec;
                        row["DisplayPrice"] = upc;
                        row["DisplayAmt"] = amt;
                    }

                    if (vty == "0" || vty == "1")
                    {
                        //if ()
                        //{

                        //TO DO Test Case Special
                        if (row["PCD"].ToString().Length == 20)
                        {
                            pcd = row["PCD"].ToString().Substring(0, 19).Trim();

                            //switch (chr)
                            //{
                            //    case "A":
                            //        symbol = "+";
                            //        break;
                            //    case "K":
                            //        symbol = "-";
                            //        break;
                            //    case "L":
                            //        symbol = "/";
                            //        break;
                            //    default:
                            //        break;
                            //}
                            //ucitmSell.lbUC_ProductCode.Text = row["PCD"].ToString().Substring(0, 18).Trim();
                        }    


                        //}
                        StoreResult result = command.getProductDesc(FunctionID.Sale_InputSaleItem_InputProduct_NormalSale, pcd);
                        DataTable data = (DataTable)result.otherData;
                        if (result.response == ResponseCode.Success)
                        {
                            if (data != null)
                            {
                                for (int j = 0; j < data.Rows.Count; j++)
                                {
                                    row["ProductName"] = data.Rows[j]["PR_NAME"].ToString();
                                    row["PromotionName"] = data.Rows[j]["Promotion"].ToString();
                                    row["PromotionPrice"] = double.Parse(data.Rows[j]["PricePromotion"].ToString());
                                    row["TotalPrice"] = amt - discamt;
                                }
                            }
                        }
                    }
                }
                _dtSaleMain.AcceptChanges();
            }
        }

        public DataRow[] getTempSaleItem()
        {
            if (_dtSaleMain.Rows.Count == 0)
            {
                // get exists data in db
                DataTable dt = command.loadTempDlyptrans(ProgramConfig.saleRefNo);
                foreach (DataRow row in dt.Rows)
                {
                    insertDtSaleMain(row["REF"].ToString(), row["REC"].ToString(), row["STY"].ToString(), row["VTY"].ToString(),
                        row["PCD"].ToString(), row["QNT"].ToString(), row["AMT"].ToString(), row["FDS"].ToString(), row["USR"].ToString(),
                        row["EGP"].ToString(), row["STT"].ToString(), row["PDISC"].ToString(), row["DISCID"].ToString(),
                        row["UPC"].ToString(), row["DTY"].ToString(), row["DISCAMT"].ToString(), row["REASON_ID"].ToString(),
                        row["STV"].ToString());
                }
                _dtSaleMain.AcceptChanges();
            }
            
            updateTempSaleItemLanguage();

            //string saleRef = refLoadTemp == "" ? ProgramConfig.saleRefNo : refLoadTemp;
            //string whereCondition = refLoadTemp == "" ? " and ISNULL(STT, '') <> 'V' and REC < 2000 " : "";

            //return _dtSaleMain.Select("REF = '" + saleRef + "' and STCODE = '" + ProgramConfig.storeCode
            //    + "'  and VTY in ('0','1') and REC >= 0 " + whereCondition + "");

            return _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and STCODE = '" + ProgramConfig.storeCode
                + "' and ISNULL(STT, '') <> 'V' and VTY in ('0','1') and REC >= 0 and REC < 2000");
        }



        public ProcessResult scanSaleProduct(string scanProductCode, double inputQty, bool IsNoScanBarcode, int discountType, double discountValue, string couponNo, 
                        Func<Result_frmPopupInput> checkIME_Serial = null, 
                        Func<Profile, string, bool> CheckAuth = null, 
                        AlertMessage AlertMessage = null,
                        Action<ResponseCode, string> ShowAlertNoSale = null,
                        string priceInp = "")
        {
            try
            {
                updateTempSaleItemLanguage();

                StoreResult result = command.getProductDesc(FunctionID.Sale_InputSaleItem_InputProduct_NormalSale, scanProductCode);
                DataTable data = (DataTable)result.otherData;
                DataRow[] newData = null;
                if (result.response.next)
                {
                    if (data != null)
                    {
                        newData = new DataRow[data.Rows.Count];
                        int rec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            newData[i] = BaseProcess.dtSaleMain.NewRow();

                            string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";
                            string code = data.Rows[i]["PR_CODE"].ToString();
                            string name = data.Rows[i]["PR_NAME"].ToString();

                            string priceTmp = priceInp == "" ? data.Rows[i]["PR_PRICE"].ToString() : priceInp;
                            double dbPrice = double.Parse(priceTmp);
                            double price = dbPrice;
                            double amt = inputQty * dbPrice;
                            string vat = data.Rows[i]["PR_VAT"].ToString();
                            string vty = "";
                            double qty = inputQty;
                            if (vat == "V")
                            {
                                vty = "1";
                            }
                            else
                            {
                                vty = "0";
                            }
                            string promo = data.Rows[i]["Promotion"].ToString();
                            double dbPromoPrice = double.Parse(data.Rows[i]["PricePromotion"].ToString());
                            string promoPrice = dbPromoPrice.ToString();
                            string barcode = data.Rows[i]["BARCODE"].ToString();
                            string pdisc = data.Rows[i]["PDISC"].ToString();
                            string discid = data.Rows[i]["DISCID"].ToString();
                            string discamt = data.Rows[i]["DISCAMT"].ToString();
                            string dty = data.Rows[i]["PR_DTYPE"].ToString();
                            string stv = data.Rows[i]["PR_Type"].ToString();
                            string isFFNRTC = "N";
                            string product_type = data.Rows[i]["PRODUCT_TYPE"].ToString();
                            string barcodeExtend = data.Rows[i]["BARCODEEXTEND"].ToString();
                            string statusNoSale = data.Rows[i]["NOSALE_STT"].ToString();
                            string msgNoSale = data.Rows[i]["NOSALE_MSG"].ToString();

                            if (statusNoSale == "1")
                            {
                                //return new ProcessResult(ResponseCode.Error, msgNoSale);
                                ShowAlertNoSale(ResponseCode.Error, msgNoSale);
                                return new ProcessResult(ResponseCode.Ignore, "");
                            }
                            else if (statusNoSale == "2")
                            {
                                AlertMessage(ResponseCode.Information, msgNoSale);                               
                            }
                            else if (statusNoSale == "3")
                            {
                                Profile check = ProgramConfig.getProfile(FunctionID.Sale_CancelWhileSale_CancelOrder);
                                if (!CheckAuth(check, "ขายปกติ"))
                                {
                                    return new ProcessResult(ResponseCode.Ignore, "");
                                };
                            }
                            else if (statusNoSale != "0")
                            {
                                return new ProcessResult(ResponseCode.Error, msgNoSale);
                            }


                            if (data.Rows[i]["InputSerial"].ToString() == "Y")
                            {
                                if (checkIME_Serial != null)
                                {
                                    var res = checkIME_Serial();
                                    if (!res.IsSuccess)
                                    {
                                        return new ProcessResult(ResponseCode.Ignore, "");
                                    }
                                    else
                                    {
                                        if (!res.resultAction.response.next)
                                        {
                                            return new ProcessResult(res.resultAction.response, res.resultAction.responseMessage);
                                        }
                                        command.insertSCANPRODUCT_SERIAL(rec.ToString(), barcode, res.input1, res.input2);
                                    }
                                }
                            }



                            bool chkWeight = Convert.ToDouble(data.Rows[i]["WEIGHT"].ToString()) > 0;
                            if (data.Rows[i]["PRODUCT_TYPE"].ToString() == "FF")
                            {
                                isFFNRTC = chkWeight ? "Y" : "N";
                            }
                            else
                            {
                                isFFNRTC = data.Rows[i]["PRODUCT_TYPE"].ToString() == "RTC" ? "Y" : "N";
                            }

                            if (isFFNRTC == "Y")
                            {
                                qty = double.Parse(data.Rows[i]["WEIGHT"].ToString());
                                amt = double.Parse(data.Rows[i]["AMT"].ToString());
                                price = amt / qty;
                            }

                            if (ProgramConfig.flagDiscount == true && couponNo != "")
                            {
                                string discountId = "";
                                string discountCode = "";
                                string discountAmt = "";
                                string discountPer = "";
                                string checkDis = ProgramConfig.getPosConfig(FunctionID.Login_DataConfigStore_DiscountManual_LimitItemDiscount.parameterCode).ToString();

                                StoreResult resDiscount = getDiscount(couponNo, discountValue, price);
                                DataTable dt = resDiscount.otherData;

                                if (dt.Rows.Count != 0)
                                {
                                    discountId = dt.Rows[0]["DiscountID"].ToString();
                                    discountCode = dt.Rows[0]["DiscountCode"].ToString();
                                    discountAmt = dt.Rows[0]["DiscountAmount"].ToString();
                                    discountPer = dt.Rows[0]["DiscountPercent"].ToString();
                                }

                                if (discountType == 1)
                                {
                                    if (double.Parse(discountPer) > double.Parse(checkDis))
                                    {
                                        string responseMessage = ProgramConfig.message.get("frmSale", "Message Box").message;
                                        string helpMessage = ProgramConfig.message.get("frmSale", "Message Box").help;
                                        return new ProcessResult(ResponseCode.Error, responseMessage, string.Format(helpMessage, checkDis));

                                        //return new PageResult(false, ResponseCode.Error, "มูลค่าส่วนลดสูงกว่าส่วนลดที่กำหนด", "มูลค่าส่วนลดต้องไม่เกิน " + checkDis + "% ของยอดขายสินค้า");
                                    }
                                    else
                                    {
                                        //Save ScanProductItemDiscount
                                        discid = discountId;
                                        double a = (price / 100.00) * discountValue;
                                        pdisc = a.ToString();
                                        discamt = a.ToString();
                                    }
                                }
                                else
                                {
                                    if (double.Parse(discountAmt) > double.Parse(checkDis))
                                    {
                                        string responseMessage = ProgramConfig.message.get("frmSale", "Message Box").message;
                                        string helpMessage = ProgramConfig.message.get("frmSale", "Message Box").help;
                                        return new ProcessResult(ResponseCode.Error, responseMessage, string.Format(helpMessage, checkDis, ProgramConfig.currencyDefault));

                                        //return new PageResult(false, ResponseCode.Error, "มูลค่าส่วนลดสูงกว่าส่วนลดที่กำหนด", "มูลค่าส่วนลดต้องไม่เกิน " + checkDis + " " + ProgramConfig.currencyDefault + " ของยอดขายสินค้า");
                                    }
                                    else
                                    {
                                        //Save ScanProductItemDiscount
                                        discid = discountId;
                                        pdisc = discountAmt;
                                        discamt = discountAmt;
                                    }
                                }
                            }


                            if (stv == "F")
                            {
                                if (!command.saveBarcodeExtend(ProgramConfig.saleRefNo, rec.ToString(), barcodeExtend, product_type))
                                {
                                    _dtSaleMain.RejectChanges();
                                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveBarcodeExtendIncomplete").message;
                                    throw new Exception(responseMessage);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน BarcodeExtend");
                                }
                            }

                            if (stv == "Z")
                            {
                                //TO DO Change language
                                Profile check = ProgramConfig.getProfile(FunctionID.Sale_InputSaleItem_CheckProduct_Bar0Bath);
                                if (check.policy == PolicyStatus.Work)
                                {
                                    if (!CheckAuth(check, "ขายปกติ"))
                                    {
                                        return new ProcessResult(ResponseCode.Ignore, "");
                                    };
                                }
                                else
                                {
                                    return new ProcessResult(ResponseCode.Error, "ไม่มีนโยบายขายสินค้าราคา 0 บาท");
                                }

                            }

                            newData[i] = insertDtSaleMain(ProgramConfig.saleRefNo, rec.ToString(), sty, vty, barcode, qty.ToString(), amt.ToString(), "0.00", ProgramConfig.userId,
                                        "0", "", pdisc, discid, dbPrice.ToString(), dty, discamt, "0", stv, rec.ToString(), dbPrice.ToString(), amt.ToString(), (amt - double.Parse(discamt)).ToString(),
                                        data.Rows[i]["PR_NAME"].ToString(), data.Rows[i]["Promotion"].ToString(), data.Rows[i]["PricePromotion"].ToString(), isFFNRTC, product_type);
                           

                            if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, rec.ToString(), sty, vty, barcode, qty.ToString(), amt.ToString(), "0.00", ProgramConfig.userId,
                                    "0", "", pdisc, discid, dbPrice.ToString(), dty, discamt, "0", stv))
                            {
                                _dtSaleMain.RejectChanges();
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(responseMessage);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน TEMPDLYPTRANS");
                            }

                            if (IsNoScanBarcode)
                            {
                                updatePCDSymbolTempDlyptrans(barcode, rec.ToString(), "L");
                            }
                                                       
                            rec++;
                        }
                    }
                    _dtSaleMain.AcceptChanges();
                    return new ProcessResult(ResponseCode.Success, data: newData);
                }
                return new ProcessResult(result);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.scanSaleProduct");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public ProcessResult calculateDiscount()
        {
            command.newTransaction();
            try
            {
                int maxRecInt;
                string recNo = "";
                DataRow newRow;

                DataTable resSelectDelete = command.selectDataToDeleteByVTYTempDLY("D");
                if (resSelectDelete.Rows.Count != 0)
                {
                    foreach (DataRow row in resSelectDelete.Rows)
                    {
                        recNo = row["REC"].ToString();

                        if (!command.deleteTempDlyTransByRefRec(recNo))
                        {
                            string responseMessage = ProgramConfig.message.get("SaleProcess", "CanNotDeleteDiscount").message;
                            throw new Exception(responseMessage);
                            //throw new Exception("ไม่สามารถลบข้อมูลส่วนลดเดิม");
                        }
                        removeDtSaleMain(recNo);
                    }
                }
                //else
                //{
                //    removeDtSaleMainWithType("D");
                //}

                maxRecInt = selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                StoreResult result = command.getDiscountItem(ProgramConfig.saleRefNo);

                if (result.response.next)
                {
                    foreach (DataRow row in result.otherData.Rows)
                    {
                        string discountCode = row["Discount_Code"].ToString();
                        string discountDesc = row["Discount_Desc"].ToString();
                        double dbSaleAmt = double.Parse(row["Sale_Amt"].ToString());
                        double dbDiscount = double.Parse(row["Discount_Amt"].ToString());

                        DataTable dtDisId = command.selectDiscountCode(discountCode);
                        if (dtDisId.Rows.Count != 0)
                        {
                            foreach (DataRow item in dtDisId.Rows)
                            {
                                if (dbDiscount > 0)
                                {
                                    newRow = _dtSaleMain.NewRow();

                                    newRow["STCODE"] = ProgramConfig.storeCode;
                                    newRow["REF"] = ProgramConfig.saleRefNo;
                                    newRow["REC"] = maxRecInt;
                                    newRow["STY"] = "0";
                                    newRow["VTY"] = "D";
                                    newRow["PCD"] = discountCode;
                                    newRow["QNT"] = dbDiscount;
                                    newRow["AMT"] = dbDiscount;
                                    newRow["FDS"] = DBNull.Value;
                                    newRow["EGP"] = "0";
                                    newRow["STT"] = "";
                                    newRow["PDISC"] = "0";
                                    newRow["DISCID"] = item["DiscountId"];
                                    newRow["UPC"] = DBNull.Value;
                                    newRow["DTY"] = "D";
                                    newRow["DISCAMT"] = "0";
                                    newRow["REASON_ID"] = "0";
                                    newRow["STV"] = "0";

                                    newRow["DisplayRec"] = maxRecInt;
                                    newRow["DisplayAmt"] = dbSaleAmt;
                                    newRow["ProductName"] = discountDesc;



                                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), "0", "D", discountCode, dbSaleAmt.ToString(), dbDiscount.ToString(),
                                        "0", ProgramConfig.userId, "0", "", "0", item["DiscountId"].ToString(), "", "D", "0", "0", "0", maxRecInt.ToString(), "",
                                        dbSaleAmt.ToString(), "", discountDesc.ToString(), "", "");

                                    if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), "0", "D", discountCode, dbSaleAmt.ToString(), dbDiscount.ToString(), 
                                        "", ProgramConfig.userId, "0", "", "0", item["DiscountId"].ToString(), "", "D", "0", "0", "0"))
                                    {

                                        string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                        throw new Exception(responseMessage);
                                        //throw new Exception("ไม่สามารถบันทึกข้อมูลส่วนลดลง TEMPDLYPTRANS");
                                    }

                                    maxRecInt++;
                                }
                            }
                        }
                    }
                    _dtSaleMain.AcceptChanges();
                    command.commit();
                    return new ProcessResult(ResponseCode.Success, data: _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and VTY = 'D'"));
                }
                return new ProcessResult(result);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.calculateDiscount");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public ProcessResult beforePaymentProcess(string totalAmt)
        {
            try
            {
                string maxRec = "";
                int maxRecInt = 0;
                string pcd = "", amt = "", fds = "";
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                StoreResult res = getTotalAmtDiff(ProgramConfig.saleRefNo, totalAmt, "1", "");
                if (res != null && res.response.next && res.otherData != null)
                {
                    double saleAmt = 0f, saleAmt_round = 0f;
                    double.TryParse(res.otherData.Rows[0]["SaleAMT"].ToString(), out saleAmt);
                    double.TryParse(res.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round);

                    maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                    maxRec = maxRecInt.ToString();

                    if (saleAmt > saleAmt_round)
                    {
                        //บันทึก VTY = 'P',  PCD=Results.PaymentMainCode, AMT = Results CHGD, FDS = 0.00
                        pcd = res.otherData.Rows[0]["PaymentMainCode"].ToString();
                        amt = res.otherData.Rows[0]["CHGD"].ToString();
                        fds = "0";

                        insertDtSaleMain(ProgramConfig.saleRefNo, maxRec, sty, "P", pcd, "1", amt, "0", ProgramConfig.userId, "0", "", "0", "0", "", "1", "0", "0", "0");

                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec, sty, "P", pcd, "1", amt, "0", ProgramConfig.userId, "0", "", "0", "0", "", "1", "0", "0", "0"))
                        {
                            if (command.saveTempPay(ProgramConfig.saleRefNo, "P", pcd, amt, fds, "0.00", "0.00"))
                            {
                                _dtSaleMain.AcceptChanges();
                                return new ProcessResult(ResponseCode.Success, data: double.Parse(totalAmt) - double.Parse(amt));
                            }
                            else
                            {
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                string helpMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").help;
                                return new ProcessResult(ResponseCode.Error, responseMessage, helpMessage);
                                //return new PageResult(false, ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                            }
                        }
                        string message = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        string help = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").help;
                        return new ProcessResult(ResponseCode.Error, message, help);
                        //return new PageResult(false, ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }
                    else if (saleAmt < saleAmt_round)
                    {
                        //บันทึก VTY = 'P',  PCD=Results.PaymentMainCode, AMT = 0.00, FDS = Results CHGD
                        pcd = res.otherData.Rows[0]["PaymentMainCode"].ToString();
                        amt = "0";
                        fds = res.otherData.Rows[0]["CHGD"].ToString();

                        insertDtSaleMain(ProgramConfig.saleRefNo, maxRec, sty, "P", pcd, "1", amt, fds, ProgramConfig.userId, "0", "", "0", "0", "", "1", "0", "0", "0");

                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec, sty, "P", pcd, "1", amt, fds, ProgramConfig.userId, "0", "", "0", "0", "", "1", "0", "0", "0"))
                        {
                            if (command.saveTempPay(ProgramConfig.saleRefNo, "P", pcd, amt, fds, "0.00", "0.00"))
                            {
                                _dtSaleMain.AcceptChanges();
                                return new ProcessResult(ResponseCode.Success, data: double.Parse(totalAmt) + double.Parse(fds));
                            }
                            else
                            {
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                string helpMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").help;
                                return new ProcessResult(ResponseCode.Error, responseMessage, helpMessage);
                                //return new PageResult(false, ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                            }
                        }
                        string message = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        string help = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").help;
                        return new ProcessResult(ResponseCode.Error, message, help);
                        //return new PageResult(false, ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }
                    else
                    {
                        return new ProcessResult(ResponseCode.Success, data: double.Parse(totalAmt));
                    }
                }
                return new ProcessResult(ResponseCode.Success, "");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.beforePaymentProcess");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public ProcessResult beforePaymentProcessNew(string totalAmt1, string totolAmt2, DataTable dt)
        {           
            try
            {
                //if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                //{
                //    return new ProcessResult(ResponseCode.Success, "");
                //}

                double amtDiff = 0f;
                double fdsDiff = 0f;
                string maxRec = "";
                int maxRecInt = 0;
                string pcd = "", amt = "", fds = "";
                StoreResult res1 = getTotalAmtDiff(ProgramConfig.saleRefNo, totalAmt1, "1", "");
                StoreResult res2 = getTotalAmtDiff(ProgramConfig.saleRefNo, totolAmt2, "1", "");

                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                if (res1 != null && res1.response.next && res1.otherData != null && res2 != null && res2.response.next && res2.otherData != null)
                {
                    double saleAmt = 0f, saleAmt_round = 0f;
                    double.TryParse(res1.otherData.Rows[0]["SaleAMT"].ToString(), out saleAmt); //4200
                    double.TryParse(res1.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round); //4000

                    if (saleAmt > saleAmt_round)
                    {
                        amtDiff = saleAmt - saleAmt_round; //200
                    }
                    else if (saleAmt < saleAmt_round)
                    {
                        fdsDiff = saleAmt_round - saleAmt;
                    }

                    // ปัดครั้งที่ 2
                    //double.TryParse(res2.otherData.Rows[0]["SaleAMT"].ToString(), out saleAmt); //4400
                    //double.TryParse(res2.otherData.Rows[0]["SaleAMT_Rounding"].ToString(), out saleAmt_round); //4500

                    //if (saleAmt > saleAmt_round)
                    //{
                    //    amtDiff += saleAmt - saleAmt_round;
                    //}
                    //else if (saleAmt < saleAmt_round)
                    //{
                    //    fdsDiff += saleAmt_round - saleAmt; 
                    //}


                    //if (amtDiff > fdsDiff)
                    //{
                    //    amtDiff = amtDiff - fdsDiff;
                    //}
                    //else if (amtDiff < fdsDiff)
                    //{
                    //    fdsDiff = fdsDiff - amtDiff;
                    //}
                    //else
                    //{
                    //    return new ProcessResult(ResponseCode.Success);
                    //}
                }

                    double maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                    maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                    maxRec = maxRecInt.ToString();

                    //บันทึก VTY = 'P',  PCD=Results.PaymentMainCode, AMT = Results CHGD, FDS = 0.00
                    //pcd = res1.otherData.Rows[0]["PaymentMainCode"].ToString();
                    //amt = res.otherData.Rows[0]["CHGD"].ToString();

                    DataTable resCHGD = command.selectTempChange("CHGD");
                    if (resCHGD != null && resCHGD.Rows.Count > 0)
                    {
                        double chgd = Convert.ToDouble(resCHGD.Rows[0]["CHG"]);
                        if (chgd <= 0)
                        {
                            amtDiff += chgd * -1;
                        }
                        else
                        {
                            fdsDiff += chgd;
                        }
                    }

                    command.newTransaction();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["PM_CODE"].ToString() == "FXCU")
                            {
                                int maxRecCHGD = command.selectMaxRecTempDlyptransForTypeP_FormPMCODE(ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()));

                                if (maxRecCHGD > 0)
                                {
                                    if (command.updateTempPay(ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), dr["CHG"].ToString(), dr["EXCG_AMT"].ToString()))
                                    {
                                        if (!command.updateTempDlyptransFDS(maxRecCHGD.ToString(), dr["CHG"].ToString(), dr["EXCG_AMT"].ToString()))
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                            throw new Exception(messageTEMP);
                                        }
                                    }
                                    else
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                        throw new Exception(messageTEMP);
                                    }
                                }
                                else
                                {
                                    if (command.saveTempPay(ProgramConfig.saleRefNo, "P", ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), "0", dr["CHG"].ToString(), dr["EXCG_AMT"].ToString(), "0"))
                                    {
                                        insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), maxRecQntInt.ToString(), "0", dr["CHG"].ToString(), ProgramConfig.userId, "0"
                                        , "", dr["EXCG_AMT"].ToString(), "0", "0", "F", "0", "0", "0");
                                        if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), maxRecQntInt.ToString(), "0", dr["CHG"].ToString(), ProgramConfig.userId, "0"
                                        , "", dr["EXCG_AMT"].ToString(), "0", "0", "F", "0", "0", "0"))
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                            throw new Exception(messageTEMP);
                                        }
                                    }
                                    else
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                        throw new Exception(messageTEMPPAY);
                                    }
                                }
                                _dtSaleMain.AcceptChanges();
                                //command.commit();
                            }
                            else if (dr["PM_CODE"].ToString() == "CASH")
                            {
                                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                                {
                                    if (!command.updateCREDPAY_TRANS_PAY("CASH", dr["EXCG_AMT"].ToString()).response.next)
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TempCREDPAY_TRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTemp_podtrans_payIncomplete").message;
                                        throw new Exception(messageTEMP);
                                    }
                                }
                                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                                {
                                    if (!command.updateTemp_podtrans_pay("CASH", dr["EXCG_AMT"].ToString()).response.next)
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TEMP_PODTRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTemp_podtrans_payIncomplete").message;
                                        throw new Exception(messageTEMP);
                                    }
                                }
                                else
                                {
                                    int maxRecCHGD = command.selectMaxRecTempDlyptransForTypeP_FormPMCODE(ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()));
                                    if (maxRecCHGD > 0)
                                    {
                                        if (command.updateTempPay(ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), dr["CHG"].ToString(), dr["EXCG_AMT"].ToString()))
                                        {
                                            if (!command.updateTempDlyptransFDS(maxRecCHGD.ToString(), dr["CHG"].ToString(), dr["EXCG_AMT"].ToString()))
                                            {
                                                _dtSaleMain.RejectChanges();
                                                command.rollback();
                                                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                                throw new Exception(messageTEMP);
                                            }
                                        }
                                        else
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                            throw new Exception(messageTEMP);
                                        }

                                    }
                                    else
                                    {
                                        if (command.saveTempPay(ProgramConfig.saleRefNo, "P", ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), "0", dr["CHG"].ToString(), "0", "0"))
                                        {
                                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), maxRecQntInt.ToString(), "0", dr["CHG"].ToString(), ProgramConfig.userId, "0"
                                            , "", dr["EXCG_AMT"].ToString(), "0", "0", "F", "0", "0", "0");
                                            if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", ProgramConfig.payment.getPCD(dr["PM_CODE"].ToString(), dr["FXCU_CODE"].ToString()), maxRecQntInt.ToString(), "0", dr["CHG"].ToString(), ProgramConfig.userId, "0"
                                            , "", dr["EXCG_AMT"].ToString(), "0", "0", "F", "0", "0", "0"))
                                            {
                                                _dtSaleMain.RejectChanges();
                                                command.rollback();
                                                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                                throw new Exception(messageTEMP);
                                            }
                                        }
                                        else
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                            throw new Exception(messageTEMPPAY);
                                        }
                                    }
                                }
                                _dtSaleMain.AcceptChanges();
                            }

                        }



                        //double chgd = dt.AsEnumerable().Sum(s => Convert.ToDouble(s["EXCG_AMT"]));
                        //if (chgd <= 0)
                        //{
                        //    amtDiff += chgd * -1;
                        //}
                        //else
                        //{
                        //    fdsDiff += chgd;
                        //}
                    }


                    amt = amtDiff.ToString();
                    //fds = "0";
                    fds = fdsDiff.ToString();

                    if (amtDiff == 0 && fdsDiff == 0)
                    {
                        command.commit();
                        return new ProcessResult(ResponseCode.Success);
                    }

                    maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                    maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                    maxRec = maxRecInt.ToString();

                   
                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRec, sty, "P", "CHGD", maxRecQntInt.ToString(), amt, fds, ProgramConfig.userId, "0", "", "0", "0", "", "1", "0", "0", "0");

                    if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                    {
                        double maxSeq = command.selectMaxSeqTempCREDPAY_TRANS_PAY(ProgramConfig.creditSaleNo) + 1;
                        if (!command.saveTempCREDPAY_TRANS_PAY(maxSeq.ToString(), "CHGD", "", amt, fds))
                        {
                            _dtSaleMain.RejectChanges();
                            command.rollback();
                            string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TempCREDPAY_TRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(messageTEMP);
                        }
                        _dtSaleMain.AcceptChanges();
                        command.commit();
                        return new ProcessResult(ResponseCode.Success);
                    }               
                    else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                    {
                       int maxRecPOD = command.selectMaxRecTEMP_PODTRANS_PAY(ProgramConfig.podRefNo);
                       var res = savePaymentPOD("CHGD", "", amt, fds, "", "", "", "", "", "", "", "", "", maxRecPOD.ToString());
                       if (!res.response.next)
                       {
                            _dtSaleMain.RejectChanges();
                            command.rollback();
                            string message = "ไม่สามารถบันทึกข้อมูลลง TEMP_PODTRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            string help = "N/A";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").help;
                            return new ProcessResult(ResponseCode.Error, message, help);
                       }
                       _dtSaleMain.AcceptChanges();
                       command.commit();
                       return new ProcessResult(ResponseCode.Success);
                    }
                    else
                    {
                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec, sty, "P", "CHGD", maxRecQntInt.ToString(), amt, fds, ProgramConfig.userId, "0", "", "0", "0", "", "1", "0", "0", "0"))
                        {
                            if (command.saveTempPay(ProgramConfig.saleRefNo, "P", "CHGD", amt, fds, "0.00", "0.00"))
                            {
                                _dtSaleMain.AcceptChanges();
                                command.commit();
                                return new ProcessResult(ResponseCode.Success);
                            }
                            else
                            {
                                _dtSaleMain.RejectChanges();
                                command.rollback();
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                string helpMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").help;
                                return new ProcessResult(ResponseCode.Error, responseMessage, helpMessage);
                                //return new PageResult(false, ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                            }
                        }
                        else
                        {
                            _dtSaleMain.RejectChanges();
                            command.rollback();
                            string message = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            string help = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").help;
                            return new ProcessResult(ResponseCode.Error, message, help);
                        }
                    }

                
                return new ProcessResult(ResponseCode.Success, "");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.beforePaymentProcess");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult getCheckProduct(string productCode)
        {
            try
            {
                return command.getCheckProduct(FunctionID.Tool_ScanProduct, productCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getCheckProduct");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult searchMember(int searchType, string data, FunctionID function = null)
        {
            try
            {
                FunctionID fn = function ?? FunctionID.Sale_Member_Search_Data;
                if (fn == FunctionID.NoFunctionID)
                {
                    fn = FunctionID.Sale_Member_Search_Data;
                }

                if (ProgramConfig.memberFormat == MemberFormat.MegaMaket)
                {
                    return command.searchCustomer(fn, searchType, data);
                }
                else
                {
                    return command.searchMember(fn, searchType, data);
                }            
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.searchMember");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult searchMemberByType(int searchType, string data, SearchTypeCustomer type)
        {
            try
            {
                if (type == SearchTypeCustomer.SearchCustomer)
                {
                    return command.searchCustomer(FunctionID.Deposit_SearchMember3, searchType, data);
                }
                else if (type == SearchTypeCustomer.SearchMember)
                {
                    return command.searchMember(FunctionID.Deposit_SearchMember2, searchType, data);
                }
                else if (type == SearchTypeCustomer.SearchCustomerFullTax)
                {
                    return command.searchCustomerFullTax(FunctionID.Deposit_SearchMember1, searchType, data);
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, "Cannot Search Member");
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.searchMember");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getPromotionProduct(string productCode, string member)
        {
            try
            {
                return command.getPromotionProduct(FunctionID.Sale_InputSaleItem_DisplayProductDesc_ShowSuggestMessage, productCode, member);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getPromotionProduct");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getMemberProfile(string memberId)
        {
            try
            {
                if (ProgramConfig.memberFormat == MemberFormat.MegaMaket)
                {
                    var res = command.CheckCustomer(FunctionID.Sale_Member_Display, memberId);
                    if (res.response.next)
                    {
                        ProgramConfig.printInvoiceType = (PrintInvoiceType)Enum.Parse(typeof(PrintInvoiceType), res.otherData.Rows[0]["PrintInvoiceType"].ToString().ToUpper(), true);
                        ProgramConfig.memberProfileMMFormat.CreditCustomerNo = res.otherData.Rows[0]["CreditCustomerNo"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.CustomerCategory = res.otherData.Rows[0]["CustomerCategory"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.Customer_No = res.otherData.Rows[0]["Customer_No"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.Customer_IDCard = res.otherData.Rows[0]["IDCardNo"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.CustomerID = res.otherData.Rows[0]["CustID"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.Address = res.otherData.Rows[0]["InvoiceAddress"].ToString().ToUpper();
                        //res = command.selectCustomer_FFTI(ProgramConfig.memberProfileMMFormat.Customer_IDCard);
                        //if (res.response.next)
                        //{
                        //    if (res.otherData.Rows.Count > 0)
                        //    {
                        //        ProgramConfig.memberProfileMMFormat.CustomerIDFFTI = res.otherData.Rows[0]["CUSTOMERID"].ToString().ToUpper();
                        //    }                           
                        //}
                    }
                    return res;
                }
                else
                {
                    var res = command.getMemberProfile(FunctionID.Sale_Member_Display, memberId);
                    if (res.response.next)
                    {
                        ProgramConfig.printInvoiceType = (PrintInvoiceType)Enum.Parse(typeof(PrintInvoiceType), res.otherData.Rows[0]["PrintInvoiceType"].ToString().ToUpper(), true);
                    }
                    return res;
                }        
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getMemberProfile");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getMemberProfileByType(string memberId, SearchTypeCustomer searchType)
        {
            try
            {
                if (searchType == SearchTypeCustomer.SearchCustomer)
                {
                    var res = command.CheckCustomer(FunctionID.Sale_Member_Display, memberId);
                    if (res.response.next)
                    {
                        ProgramConfig.printInvoiceType = (PrintInvoiceType)Enum.Parse(typeof(PrintInvoiceType), res.otherData.Rows[0]["PrintInvoiceType"].ToString().ToUpper(), true);
                        ProgramConfig.memberProfileMMFormat.CreditCustomerNo = res.otherData.Rows[0]["CreditCustomerNo"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.CustomerCategory = res.otherData.Rows[0]["CustomerCategory"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.Customer_No = res.otherData.Rows[0]["Customer_No"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.Customer_IDCard = res.otherData.Rows[0]["IDCardNo"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.CustomerID = res.otherData.Rows[0]["CustID"].ToString().ToUpper();
                        ProgramConfig.memberProfileMMFormat.Address = res.otherData.Rows[0]["InvoiceAddress"].ToString().ToUpper();
                        //res = command.selectCustomer_FFTI(ProgramConfig.memberProfileMMFormat.Customer_IDCard);
                        //if (res.response.next)
                        //{
                        //    if (res.otherData.Rows.Count > 0)
                        //    {
                        //        ProgramConfig.memberProfileMMFormat.CustomerIDFFTI = res.otherData.Rows[0]["CUSTOMERID"].ToString().ToUpper();
                        //    }                           
                        //}
                    }
                    return res;
                }
                else if (searchType == SearchTypeCustomer.SearchMember)
                {
                    var res = command.getMemberProfile(FunctionID.Sale_Member_Display, memberId);
                    if (res.response.next)
                    {
                        ProgramConfig.printInvoiceType = (PrintInvoiceType)Enum.Parse(typeof(PrintInvoiceType), res.otherData.Rows[0]["PrintInvoiceType"].ToString().ToUpper(), true);
                    }
                    return res;
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, "Cannot Search Member");
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getMemberProfile");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult posGetDiscountItem(string refNo)
        {
            try
            {
                return command.getDiscountItem(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.posGetDiscountItem");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getTotalAmtDiff(string refNo, string saleAmt, string mode, string pmCode)
        {
            try
            {

                    return command.getTotalAmtDiff(refNo, saleAmt, mode, pmCode);
                //}
                //else
                //{
                //    return new StoreResult(ResponseCode.Success, "Success");
                //}
                
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getTotalAmtDiff");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        //getAmountExchangeRate ทำให้ error thread ทำให้ต้องใส่ InvokeRequired
        public StoreResult getAmountExchangeRate(string saleAmt, string mode, string pmCode, string pmMainCode)
        {
            try
            {
                return command.getAmountExchangeRate(saleAmt, mode, pmCode, pmMainCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getAmountExchangeRate");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog("Error Exception at SaleProcess.getAmountExchangeRate : " + ex.Message);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult generateMenu()
        {
            try
            {
                return command.generateMenu(FunctionID.Sale_SelectSaleMenu);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.generateMenu");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getDiscount(string discountCode, double discountValue, double saleAmount)
        {
            try
            {
                return command.getDiscount(discountCode, discountValue, saleAmount);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getDiscount");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveCancelSaleTransaction(FunctionID functionId, string reason)
        {
            try
            {
                command.newTransaction();
                int rec = 0, resRec; DataRow[] row1;
                double qnt = 0; double amt = 0;
                DataTable data = command.selectTempDlyptrans(ProgramConfig.saleRefNo);
                foreach (DataRow row in data.Rows)
                {
                    rec = (int)row["REC"];
                    if (!command.updateTempDlyptrans(ProgramConfig.saleRefNo, rec.ToString()))
                    {
                        command.rollback();
                        string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(responseMessage);
                        //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                    }

                    resRec = rec + 10000;
                    row1 = _dtSaleMain.Select("STCODE = '" + ProgramConfig.storeCode + "' and REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + rec + "'");
                    foreach (DataRow item in row1)
                    {
                        item["REC"] = resRec;
                        item["STT"] = "V";
                    }

                    if (rec < 2000 && (row["VTY"].ToString() == "0" || row["VTY"].ToString() == "1"))
                    {
                        qnt += Convert.ToDouble(row["QNT"] + "");
                        amt += Convert.ToDouble(row["AMT"] + "");
                    }
                }

                string newRec = (rec + 10001).ToString();

                insertDtSaleMain(ProgramConfig.saleRefNo, newRec, "0", "H", "Cancel By User", qnt.ToString(), amt.ToString(), "0", ProgramConfig.superUserId, "0"
                    , "V", "0", "0", "0", "0", "0", reason, "0");

                if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec, "0", "H", "Cancel By User", qnt.ToString(), amt.ToString(), "0", ProgramConfig.superUserId, "0"
                , "V", "0", "0", "0", "0", "0", reason, "0"))
                {
                    command.rollback();
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลใน TEMPDLYPTRANS");
                }
                _dtSaleMain.AcceptChanges();

                StoreResult res = command.saveCancelSaleTransaction(functionId);
                if (res.response.next)
                {
                    if (res.otherData != null)
                    {
                        ProgramConfig.cancelNo = res.otherData.Rows[0]["CancelNo"].ToString();
                    }

                    command.commit();
                    return res;
                }
                else
                {
                    command.rollback();
                    return res;
                }                
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveCancelSaleTransaction");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public ProcessResult saveDeleteItem(string productCode, string qty, string price)
        {
            command.newTransaction();
            try
            {
                int newRec = selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                DataTable selectDeleteItem = selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, productCode, qty, price);
                string stcode = selectDeleteItem.Rows[0]["STCODE"].ToString();
                string refNo = selectDeleteItem.Rows[0]["REF"].ToString();
                string rec = selectDeleteItem.Rows[0]["REC"].ToString();
                string sty = selectDeleteItem.Rows[0]["STY"].ToString();
                string vty = selectDeleteItem.Rows[0]["VTY"].ToString();
                string pcd = selectDeleteItem.Rows[0]["PCD"].ToString();
                string qnt = selectDeleteItem.Rows[0]["QNT"].ToString();
                string amt = selectDeleteItem.Rows[0]["AMT"].ToString();
                string fds = selectDeleteItem.Rows[0]["FDS"].ToString();
                string ttm = selectDeleteItem.Rows[0]["TTM"].ToString();
                string usr = selectDeleteItem.Rows[0]["USR"].ToString();
                string egp = selectDeleteItem.Rows[0]["EGP"].ToString();
                string stt = selectDeleteItem.Rows[0]["STT"].ToString();
                string stv = selectDeleteItem.Rows[0]["STV"].ToString();
                string reason = selectDeleteItem.Rows[0]["REASON_ID"].ToString();
                string pdisc = selectDeleteItem.Rows[0]["PDISC"].ToString();
                string discid = selectDeleteItem.Rows[0]["DISCID"].ToString();
                string discamt = selectDeleteItem.Rows[0]["DISCAMT"].ToString();
                string upc = selectDeleteItem.Rows[0]["UPC"].ToString();
                string dty = selectDeleteItem.Rows[0]["DTY"].ToString();

                //string userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                string userauth = "999999";

                DataRow newRow = insertDtSaleMain(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                   , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                   , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, reason, stv);

                if (!command.saveTempDlyptrans(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                   , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                   , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, reason, stv))
                {
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }

                userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                //TO DO Insert TempDLYPTRANS_AUTHORIZE
                if (!command.saveTempdly_Authorize(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                   , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                   , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, reason, stv))
                {
                    //TO DO Chnage language
                    string responseMessage = "ไม่สามารถบันทึกข้อมูลลง TEMPDLY_AUTHORIZE";
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }


                if (!command.updateDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, rec))
                {
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                }

                foreach (DataRow row in _dtSaleMain.Rows)
                {
                    if (row["STCODE"].ToString() == ProgramConfig.storeCode && row["REF"].ToString() == ProgramConfig.saleRefNo && row["REC"].ToString() == rec)
                    {
                        row["STT"] = "V";
                    }
                }

                _dtSaleMain.AcceptChanges();
                return new ProcessResult(ResponseCode.Success, data: newRow);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveDeleteItem");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new ProcessResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveDeleteWithReasonSingle(string reasonID, string productCode, string quant, string price)
        {
            command.newTransaction();
            try
            {

                int newRec = selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                //string aid = "000002";
                string aty = "D";
                StoreResult result = command.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ActionID);
                ProgramConfig.actionRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.actionRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();

                string aid = result.otherData.Rows[0]["ReferenceNo"].ToString();

                DataTable selectDeleteItem = selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, productCode, quant, price);
                if (selectDeleteItem != null && selectDeleteItem.Rows.Count > 0)
                {
                    string stcode = selectDeleteItem.Rows[0]["STCODE"].ToString();
                    string refNo = selectDeleteItem.Rows[0]["REF"].ToString();
                    string rec = selectDeleteItem.Rows[0]["REC"].ToString();
                    string sty = selectDeleteItem.Rows[0]["STY"].ToString();
                    string vty = selectDeleteItem.Rows[0]["VTY"].ToString();
                    string pcd = selectDeleteItem.Rows[0]["PCD"].ToString();
                    string qnt = selectDeleteItem.Rows[0]["QNT"].ToString();
                    string amt = selectDeleteItem.Rows[0]["AMT"].ToString();
                    string fds = selectDeleteItem.Rows[0]["FDS"].ToString();
                    string ttm = selectDeleteItem.Rows[0]["TTM"].ToString();
                    string usr = selectDeleteItem.Rows[0]["USR"].ToString();
                    string egp = selectDeleteItem.Rows[0]["EGP"].ToString();
                    string stt = selectDeleteItem.Rows[0]["STT"].ToString();
                    string stv = selectDeleteItem.Rows[0]["STV"].ToString();
                    string reason = selectDeleteItem.Rows[0]["REASON_ID"].ToString();
                    string pdisc = selectDeleteItem.Rows[0]["PDISC"].ToString();
                    string discid = selectDeleteItem.Rows[0]["DISCID"].ToString();
                    string discamt = selectDeleteItem.Rows[0]["DISCAMT"].ToString();
                    string upc = selectDeleteItem.Rows[0]["UPC"].ToString();
                    string dty = selectDeleteItem.Rows[0]["DTY"].ToString();

                    //string userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                    string userauth = "999999";

                    insertDtSaleMain(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                        , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                        , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv);

                    if (command.saveTempDlyptrans(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                        , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                        , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                    {

                        userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                        //TO DO Insert TempDLYPTRANS_AUTHORIZE
                        if (command.saveTempdly_Authorize(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                        , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                        , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                        {
                            foreach (DataRow row in _dtSaleMain.Rows)
                            {
                                if (row["REF"].ToString() == refNo && row["STCODE"].ToString() == ProgramConfig.storeCode && row["REC"].ToString() == rec)
                                {
                                    row["STT"] = "V";
                                }
                            }
                            if (command.updateDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, rec))
                            {
                                if (command.saveEditItemTrans(refNo, aid.ToString(), aty, rec, sty, vty, pcd, qnt, amt, fds, usr, "V", "", reasonID, upc))
                                {
                                    if (command.saveEditItemTrans(refNo, aid, aty, newRec.ToString(), sty, vty, pcd, (double.Parse(qnt) * -1).ToString()
                                                                                                    , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId, "V", rec, reasonID, upc))
                                    {
                                        _dtSaleMain.AcceptChanges();
                                        command.commit();
                                        return new StoreResult(ResponseCode.Success, "Success");
                                    }
                                }
                            }
                        }
                    }
                    command.rollback();
                    _dtSaleMain.RejectChanges();
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveDeleteProductIncomplete").message;
                    string helpMessage = ProgramConfig.message.get("SaleProcess", "SaveDeleteProductIncomplete").help;
                    return new StoreResult(ResponseCode.Error, responseMessage, helpMessage);
                    //return new PageResult(false, ResponseCode.Error, "ไม่สามารถบันทึกลบข้อมูลสินค้า");
                }
                string message = ProgramConfig.message.get("SaleProcess", "DeleteProductNotFound").message;
                string help = ProgramConfig.message.get("SaleProcess", "DeleteProductNotFound").help;
                return new StoreResult(ResponseCode.Error, message, help);
                //return new PageResult(false, ResponseCode.Information, "ไม่พบข้อมูลสินค้าที่ต้องการลบ");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveDeleteWithReasonSingle");
                throw;
            }
            catch (Exception ex)
            {
                command.rollback();
                _dtSaleMain.RejectChanges();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveDeleteWithReasonAll(string reasonID, string productCode, string quant, string deleteQuant, string price, string pDiscID)
        {
            command.newTransaction();
            try
            {
                int newRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                //string aid = "000002";
                string aty = "D";
                StoreResult result = command.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ActionID);
                ProgramConfig.actionRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.actionRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();

                string aid = result.otherData.Rows[0]["ReferenceNo"].ToString();
                string stcode, refNo, rec, sty, vty = "", pcd, qnt, amt, fds = "", ttm, usr, egp, stt, stv = "", reason, pdisc = "", discid = "", discamt = "", upc, dty = "";

                double resQty = double.Parse(quant) - double.Parse(deleteQuant);
                DataTable selectDeleteItem = command.selectAllDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, productCode, price, pDiscID);
                if (selectDeleteItem != null)
                {
                    for (int i = 0; i < selectDeleteItem.Rows.Count; i++)
                    {
                        stcode = selectDeleteItem.Rows[i]["STCODE"].ToString();
                        refNo = selectDeleteItem.Rows[i]["REF"].ToString();
                        rec = selectDeleteItem.Rows[i]["REC"].ToString();
                        sty = selectDeleteItem.Rows[i]["STY"].ToString();
                        vty = selectDeleteItem.Rows[i]["VTY"].ToString();
                        pcd = selectDeleteItem.Rows[i]["PCD"].ToString();
                        qnt = selectDeleteItem.Rows[i]["QNT"].ToString();
                        amt = selectDeleteItem.Rows[i]["AMT"].ToString();
                        fds = selectDeleteItem.Rows[i]["FDS"].ToString();
                        ttm = selectDeleteItem.Rows[i]["TTM"].ToString();
                        usr = selectDeleteItem.Rows[i]["USR"].ToString();
                        egp = selectDeleteItem.Rows[i]["EGP"].ToString();
                        stt = selectDeleteItem.Rows[i]["STT"].ToString();
                        stv = selectDeleteItem.Rows[i]["STV"].ToString();
                        reason = selectDeleteItem.Rows[i]["REASON_ID"].ToString();
                        pdisc = selectDeleteItem.Rows[i]["PDISC"].ToString();
                        discid = selectDeleteItem.Rows[i]["DISCID"].ToString();
                        discamt = selectDeleteItem.Rows[i]["DISCAMT"].ToString();
                        upc = selectDeleteItem.Rows[i]["UPC"].ToString();
                        dty = selectDeleteItem.Rows[i]["DTY"].ToString();

                        //string userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                        string userauth = "999999";

                        insertDtSaleMain(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                           , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                           , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv);

                        if (command.saveTempDlyptrans(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                           , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                           , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                        {

                            userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                            //TO DO Insert TempDLYPTRANS_AUTHORIZE
                            if (!command.saveTempdly_Authorize(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                               , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                               , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                            {
                                //TO DO Chnage language
                                string responseMessage = "ไม่สามารถบันทึกข้อมูลลง TEMPDLY_AUTHORIZE";
                                throw new Exception(responseMessage);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                            }



                            foreach (DataRow row in _dtSaleMain.Rows)
                            {
                                if (row["REF"].ToString() == refNo && row["STCODE"].ToString() == ProgramConfig.storeCode && row["REC"].ToString() == rec)
                                {
                                    row["STT"] = "V";
                                }
                            }
                            if (command.updateDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, rec))
                            {
                                if (command.saveEditItemTrans(refNo, aid.ToString(), aty, rec, sty, vty, pcd, qnt, amt, fds, usr, "V", "", reasonID, upc))
                                {
                                    if (command.saveEditItemTrans(refNo, aid, aty, newRec.ToString(), sty, vty, pcd, (double.Parse(qnt) * -1).ToString()
                                                                                                    , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId, "V", rec, reasonID, upc))
                                    {
                                        newRec++;
                                    }
                                    else
                                    {
                                        string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                        throw new Exception(responseMessage);
                                        //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                                    }
                                }
                                else
                                {
                                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                    throw new Exception(responseMessage);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                                }
                            }
                            else
                            {
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(responseMessage);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลใน TEMPDLYPTRANS");
                            }
                        }
                        else
                        {
                            string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(responseMessage);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลใน TEMPDLYPTRANS");
                        }
                    }

                    if (resQty != 0)
                    {
                        string newAmt = (resQty * double.Parse(price)).ToString("0.000");
                        //SaveTemp

                        insertDtSaleMain(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, resQty.ToString(), newAmt, fds, ProgramConfig.userId
                            , "0.00", "", pdisc, discid, price, dty, discamt, "", stv);
                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, resQty.ToString(), newAmt, fds, ProgramConfig.userId
                            , "0.00", "", pdisc, discid, price, dty, discamt, "", stv))
                        {
                            if (!command.saveEditItemTrans(ProgramConfig.saleRefNo, aid.ToString(), aty, newRec.ToString(), "0", vty, productCode, resQty.ToString(), newAmt, fds, ProgramConfig.userId, "", "", reasonID, price))
                            {
                                string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                throw new Exception(responseMessage);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                            }
                        }
                        else
                        {
                            string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                            throw new Exception(responseMessage);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                        }
                    }
                    _dtSaleMain.AcceptChanges();
                    command.commit();
                    return new StoreResult(ResponseCode.Success, "Success");
                }
                string message = ProgramConfig.message.get("SaleProcess", "DeleteProductNotFound").message;
                string help = ProgramConfig.message.get("SaleProcess", "DeleteProductNotFound").help;
                return new StoreResult(ResponseCode.Information, message, help);
                //return new PageResult(false, ResponseCode.Information, "ไม่พบข้อมูลสินค้าที่ต้องการลบ");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveDeleteWithReasonAll");
                throw;
            }
            catch (Exception ex)
            {
                command.rollback();
                _dtSaleMain.RejectChanges();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveEditItem(string productCode, string currentQty, string qty, string currentPrice, string price, string recEdit)
        {
            command.newTransaction();
            try
            {
                string reasonID = "";
                int currentRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo);
                int newRec = currentRec + 1;
                int editRec = Convert.ToInt32(recEdit) + 2000;
                double oldAmt = double.Parse(currentQty) * double.Parse(currentPrice);
                double newAmt = double.Parse(currentQty) * double.Parse(price);

                DataTable selectDeleteItem = selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, productCode, qty, price);
                string stcode = selectDeleteItem.Rows[0]["STCODE"].ToString();
                string refNo = selectDeleteItem.Rows[0]["REF"].ToString();
                string rec = selectDeleteItem.Rows[0]["REC"].ToString();
                string sty = selectDeleteItem.Rows[0]["STY"].ToString();
                string vty = selectDeleteItem.Rows[0]["VTY"].ToString();
                string pcd = selectDeleteItem.Rows[0]["PCD"].ToString();
                string qnt = selectDeleteItem.Rows[0]["QNT"].ToString();
                string amt = selectDeleteItem.Rows[0]["AMT"].ToString();
                string fds = selectDeleteItem.Rows[0]["FDS"].ToString();
                string ttm = selectDeleteItem.Rows[0]["TTM"].ToString();
                string usr = selectDeleteItem.Rows[0]["USR"].ToString();
                string egp = selectDeleteItem.Rows[0]["EGP"].ToString();
                string stt = selectDeleteItem.Rows[0]["STT"].ToString();
                string stv = selectDeleteItem.Rows[0]["STV"].ToString();
                string reason = selectDeleteItem.Rows[0]["REASON_ID"].ToString();
                string pdisc = selectDeleteItem.Rows[0]["PDISC"].ToString();
                string discid = selectDeleteItem.Rows[0]["DISCID"].ToString();
                string discamt = selectDeleteItem.Rows[0]["DISCAMT"].ToString();
                string upc = selectDeleteItem.Rows[0]["UPC"].ToString();
                string dty = selectDeleteItem.Rows[0]["DTY"].ToString();

                //string userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                string userauth = "999999";

                insertDtSaleMain(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                    , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                    , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv);

                if (!command.saveTempDlyptrans(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                    , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                    , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                {
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }

                userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                //TO DO Insert TempDLYPTRANS_AUTHORIZE
                if (!command.saveTempdly_Authorize(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                    , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                    , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                {
                    //TO DO Chnage language
                    string responseMessage = "ไม่สามารถบันทึกข้อมูลลง TEMPDLY_AUTHORIZE";
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }


                foreach (DataRow row in _dtSaleMain.Rows)
                {
                    if (row["STCODE"].ToString() == ProgramConfig.storeCode && row["REF"].ToString() == ProgramConfig.saleRefNo && row["REC"].ToString() == rec)
                    {
                        row["STT"] = "V";
                    }
                }
                if (!command.updateDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, rec))
                {
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                }

                insertDtSaleMain(ProgramConfig.saleRefNo, editRec.ToString(), "X", "X", pcd, qnt.ToString(), oldAmt.ToString(), newAmt.ToString(), ProgramConfig.userId
                        , rec, "", "0", discid, "0", "1", "0", reasonID, "");

                if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, editRec.ToString(), "X", "X", pcd, qnt.ToString(), oldAmt.ToString(), newAmt.ToString(), ProgramConfig.userId
                        , rec, "", "0", discid, "0", "1", "0", reasonID, ""))
                {
                    string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(responseMessage);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }

                _dtSaleMain.AcceptChanges();
                command.commit();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveEditItem");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveEditWithReasonSingle(string reasonID, string productCode, string editQuant, string currentPrice, string editPrice, string recEdit, string isFFNRTC, string Product_type, string totalPrice)
        {
            command.newTransaction();
            try
            {
                int newRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                string aty = "P";
                StoreResult result = command.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ActionID);
                ProgramConfig.actionRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.actionRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                string aid = result.otherData.Rows[0]["ReferenceNo"].ToString();

                string oldAmt = (double.Parse(editQuant) * double.Parse(currentPrice)).ToString(ProgramConfig.amountFormatString);
                string newAmt = (double.Parse(editQuant) * double.Parse(editPrice)).ToString(ProgramConfig.amountFormatString);
                DataTable selectDeleteItem = selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, productCode, editQuant, isFFNRTC == "Y" ? totalPrice : oldAmt);               
                string stcode = selectDeleteItem.Rows[0]["STCODE"].ToString();
                string refNo = selectDeleteItem.Rows[0]["REF"].ToString();
                string rec = selectDeleteItem.Rows[0]["REC"].ToString();
                string sty = selectDeleteItem.Rows[0]["STY"].ToString();
                string vty = selectDeleteItem.Rows[0]["VTY"].ToString();
                string pcd = selectDeleteItem.Rows[0]["PCD"].ToString();
                string qnt = selectDeleteItem.Rows[0]["QNT"].ToString();
                string amt = selectDeleteItem.Rows[0]["AMT"].ToString();
                string fds = selectDeleteItem.Rows[0]["FDS"].ToString();
                string ttm = selectDeleteItem.Rows[0]["TTM"].ToString();
                string usr = selectDeleteItem.Rows[0]["USR"].ToString();
                string egp = selectDeleteItem.Rows[0]["EGP"].ToString();
                string stt = selectDeleteItem.Rows[0]["STT"].ToString();
                string stv = selectDeleteItem.Rows[0]["STV"].ToString();
                string reason = selectDeleteItem.Rows[0]["REASON_ID"].ToString();
                string pdisc = selectDeleteItem.Rows[0]["PDISC"].ToString();
                string discid = selectDeleteItem.Rows[0]["DISCID"].ToString();
                string discamt = selectDeleteItem.Rows[0]["DISCAMT"].ToString();
                string upc = selectDeleteItem.Rows[0]["UPC"].ToString();
                string dty = selectDeleteItem.Rows[0]["DTY"].ToString();


                //string userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                string userauth = "999999";

                insertDtSaleMain(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                       , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                       , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv, "", "", "", "", "", "", "", isFFNRTC, Product_type);

                if (command.saveTempDlyptrans(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                       , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                       , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                {
                    userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                    //TO DO Insert TempDLYPTRANS_AUTHORIZE
                    if (!command.saveTempdly_Authorize(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                       , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                       , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                    {
                        //TO DO Chnage language
                        string responseMessage = "ไม่สามารถบันทึกข้อมูลลง TEMPDLY_AUTHORIZE";
                        throw new Exception(responseMessage);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }

                    foreach (DataRow row in _dtSaleMain.Rows)
                    {
                        if (row["STCODE"].ToString() == ProgramConfig.storeCode && row["REF"].ToString() == ProgramConfig.saleRefNo && row["REC"].ToString() == rec)
                        {
                            row["STT"] = "V";
                        }
                    }
                    if (command.updateDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, rec))
                    {
                        if (command.saveEditItemTrans(refNo, aid.ToString(), aty, rec, sty, vty, pcd, qnt, amt, fds, usr, stt, "", reasonID, upc))
                        {
                            if (command.saveEditItemTrans(refNo, aid, aty, newRec.ToString(), sty, vty, pcd, (double.Parse(qnt) * -1).ToString()
                                                                                            , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId, "V", rec, reasonID, upc))
                            {
                                newRec++;
                                insertDtSaleMain(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, editQuant, newAmt, "0", ProgramConfig.userId
                                    , "0.00", "", pdisc, discid, editPrice, dty, discamt, "", stv, "", "", "", "", "", "", "", isFFNRTC, Product_type);

                                if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, editQuant, newAmt, "0", ProgramConfig.userId
                                    , "0.00", "", pdisc, discid, editPrice, dty, discamt, "", stv))
                                {
                                    if (command.saveEditItemTrans(ProgramConfig.saleRefNo, aid.ToString(), aty, newRec.ToString(), "0", vty, productCode, editQuant, newAmt, fds, ProgramConfig.userId, "", "", reasonID, editPrice))
                                    {
                                        int editRec = (newRec + 2000);
                                        //int editRec = (Convert.ToInt32(recEdit) + 2000);
                                        insertDtSaleMain(ProgramConfig.saleRefNo, editRec.ToString(), "X", "X", productCode, editQuant, currentPrice, editPrice, ProgramConfig.userId
                                            , rec, "", "0.00", discid, "0.00", "1", "0.00", reasonID, stv, "", "", "", "", "", "", "", isFFNRTC, Product_type);
                                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, editRec.ToString(), "X", "X", productCode, editQuant, currentPrice, editPrice, ProgramConfig.userId
                                            , rec, "", "0.00", discid, "0.00", "1", "0.00", reasonID, stv))
                                        {
                                            if (command.saveEditItemTrans(ProgramConfig.saleRefNo, aid.ToString(), aty, editRec.ToString(), sty, vty, productCode, editQuant, currentPrice, editPrice, ProgramConfig.userId, "", "", reasonID, "0.00"))
                                            {
                                                _dtSaleMain.AcceptChanges();
                                                command.commit();
                                                return new StoreResult(ResponseCode.Success, "Success");
                                            }
                                            string messageETT = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                            throw new Exception(messageETT);
                                            //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                                        }
                                        string messageETEMP = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                                        throw new Exception(messageETEMP);
                                        //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                                    }
                                    string messageETT2 = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                    throw new Exception(messageETT2);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                                }
                                string messageETEMP2 = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(messageETEMP2);
                                //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                            }
                        }
                        string messageETT3 = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                        throw new Exception(messageETT3);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                    }
                }
                string messageETEMP3 = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                throw new Exception(messageETEMP3);
                //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveEditWithReasonSingle");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveEditWithReasonAll(string reasonID, string productCode, string currentQuant, string editQuant, string currentPrice, string editPrice, string pDiscID, string recEdit, string isFFNRTC, string Product_type, string totalPrice)
        {
            command.newTransaction();
            try
            {
                int newRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                string aty = "P";
                StoreResult result = command.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ActionID);
                ProgramConfig.actionRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                ProgramConfig.actionRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                string aid = result.otherData.Rows[0]["ReferenceNo"].ToString();
                string stcode, refNo = "", rec, sty = "", vty = "", pcd = "", qnt, amt, fds = "", ttm, usr, egp, stt, stv = "", reason, pdisc = "", discid = "", discamt = "", upc, dty = "";

                double resQty = double.Parse(currentQuant) - double.Parse(editQuant);
                string restAmt = (resQty * double.Parse(currentPrice)).ToString(ProgramConfig.amountFormatString);
                string oldAmt = (double.Parse(editQuant) * double.Parse(currentPrice)).ToString(ProgramConfig.amountFormatString);
                string newAmt = (double.Parse(editQuant) * double.Parse(editPrice)).ToString(ProgramConfig.amountFormatString);
                DataTable selectDeleteItem = command.selectAllDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, productCode, isFFNRTC == "Y" ? totalPrice : currentPrice, pDiscID);
                for (int i = 0; i < selectDeleteItem.Rows.Count; i++)
                {
                    stcode = selectDeleteItem.Rows[i]["STCODE"].ToString();
                    refNo = selectDeleteItem.Rows[i]["REF"].ToString();
                    rec = selectDeleteItem.Rows[i]["REC"].ToString();
                    recEdit = rec;
                    sty = selectDeleteItem.Rows[i]["STY"].ToString();
                    vty = selectDeleteItem.Rows[i]["VTY"].ToString();
                    pcd = selectDeleteItem.Rows[i]["PCD"].ToString();
                    qnt = selectDeleteItem.Rows[i]["QNT"].ToString();
                    amt = selectDeleteItem.Rows[i]["AMT"].ToString();
                    fds = selectDeleteItem.Rows[i]["FDS"].ToString();
                    ttm = selectDeleteItem.Rows[i]["TTM"].ToString();
                    usr = selectDeleteItem.Rows[i]["USR"].ToString();
                    egp = selectDeleteItem.Rows[i]["EGP"].ToString();
                    stt = selectDeleteItem.Rows[i]["STT"].ToString();
                    stv = selectDeleteItem.Rows[i]["STV"].ToString();
                    reason = selectDeleteItem.Rows[i]["REASON_ID"].ToString();
                    pdisc = selectDeleteItem.Rows[i]["PDISC"].ToString();
                    discid = selectDeleteItem.Rows[i]["DISCID"].ToString();
                    discamt = selectDeleteItem.Rows[i]["DISCAMT"].ToString();
                    upc = selectDeleteItem.Rows[i]["UPC"].ToString();
                    dty = selectDeleteItem.Rows[i]["DTY"].ToString();

                    //string userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                    string userauth = "999999";

                    insertDtSaleMain(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                       , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                       , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv, "", "", "", "", "", "", "", isFFNRTC, Product_type);

                    if (command.saveTempDlyptrans(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                       , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                       , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                    {
                        userauth = String.IsNullOrEmpty(ProgramConfig.superUserId.Trim()) || ProgramConfig.superUserId == "N/A" ? ProgramConfig.userId : ProgramConfig.superUserId;
                        //TO DO Insert TempDLYPTRANS_AUTHORIZE
                        if (!command.saveTempdly_Authorize(refNo, newRec.ToString(), "0", vty, pcd, (double.Parse(qnt) * -1).ToString()
                                       , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId
                                       , "0", "V", userauth, discid, (double.Parse(upc) * -1).ToString(), dty, discamt, rec, stv))
                        {
                            //TO DO Chnage language
                            string responseMessage = "ไม่สามารถบันทึกข้อมูลลง TEMPDLY_AUTHORIZE";
                            throw new Exception(responseMessage);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                        }

                        foreach (DataRow row in _dtSaleMain.Rows)
                        {
                            if (row["STCODE"].ToString() == ProgramConfig.storeCode && row["REF"].ToString() == ProgramConfig.saleRefNo && row["REC"].ToString() == rec)
                            {
                                row["STT"] = "V";
                            }
                        }
                        if (command.updateDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, rec))
                        {
                            if (command.saveEditItemTrans(refNo, aid.ToString(), aty, rec, sty, vty, pcd, qnt, amt, fds, usr, stt, "", reasonID, upc))
                            {
                                if (command.saveEditItemTrans(refNo, aid, aty, newRec.ToString(), sty, vty, pcd, (double.Parse(qnt) * -1).ToString()
                                                                                                , (double.Parse(amt) * -1).ToString(), (double.Parse(fds) * -1).ToString(), ProgramConfig.userId, "V", rec, reasonID, upc))
                                {
                                    newRec++;
                                }
                                else
                                {
                                    string messageETT = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                    throw new Exception(messageETT);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                                }
                            }
                            else
                            {
                                string messageETT = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                                throw new Exception(messageETT);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                            }
                        }
                        else
                        {
                            string messageETEMP = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(messageETEMP);
                            //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                        }
                    }
                    else
                    {
                        string messageETEMP = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageETEMP);
                        //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                    }
                }

                if (resQty != 0)
                {
                    //SaveTemp
                    insertDtSaleMain(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, resQty.ToString(), restAmt, "0", ProgramConfig.userId
                        , "0.00", " ", pdisc, discid, currentPrice, dty, discamt, "", stv, "", "", "", "", "", "", "", isFFNRTC, Product_type);
                    if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, resQty.ToString(), restAmt, "0", ProgramConfig.userId
                        , "0.00", " ", pdisc, discid, currentPrice, dty, discamt, "", stv))
                    {
                        if (command.saveEditItemTrans(refNo, aid.ToString(), aty, newRec.ToString(), "0", vty, pcd, resQty.ToString(), restAmt, fds, ProgramConfig.userId, "", "", reasonID, currentPrice))
                        {
                            newRec++;
                        }
                        else
                        {
                            string messageETT = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                            throw new Exception(messageETT);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                        }
                    }
                    else
                    {
                        string messageETEMP = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageETEMP);
                        //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                    }
                }

                insertDtSaleMain(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, editQuant, newAmt, "0", ProgramConfig.userId
                    , "0.00", " ", pdisc, discid, editPrice, dty, discamt, "", stv, "", "", "", "", "", "", "", isFFNRTC, Product_type);
                if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, newRec.ToString(), "0", vty, productCode, editQuant, newAmt, "0", ProgramConfig.userId
                    , "0.00", " ", pdisc, discid, editPrice, dty, discamt, "", stv))
                {
                    if (command.saveEditItemTrans(ProgramConfig.saleRefNo, aid.ToString(), aty, newRec.ToString(), "0", vty, productCode, editQuant, newAmt, fds, ProgramConfig.userId, "", "", reasonID, editPrice))
                    {
                        int editRec = (newRec + 2000);
                        //int editRec = (Convert.ToInt32(recEdit) + 2000);
                        insertDtSaleMain(ProgramConfig.saleRefNo, editRec.ToString(), "X", "X", productCode, editQuant, currentPrice, editPrice, ProgramConfig.userId
                            , "0.00", "", "0.00", discid, "0.00", "1", "0.00", reasonID, stv);
                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, editRec.ToString(), "X", "X", productCode, editQuant, currentPrice, editPrice, ProgramConfig.userId
                            , "0.00", "", "0.00", discid, "0.00", "1", "0.00", reasonID, stv))
                        {
                            if (command.saveEditItemTrans(ProgramConfig.saleRefNo, aid.ToString(), aty, editRec.ToString(), sty, vty, productCode, editQuant, currentPrice, editPrice, ProgramConfig.userId, "", "", reasonID, "0.00"))
                            {
                                _dtSaleMain.AcceptChanges();
                                command.commit();
                                return new StoreResult(ResponseCode.Success, "Success");
                            }
                            string messageETT = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                            throw new Exception(messageETT);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                        }
                        string messageETEMP = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageETEMP);
                        //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
                    }
                    string messageETT2 = ProgramConfig.message.get("SaleProcess", "SaveEditItemTransIncomplete").message;
                    throw new Exception(messageETT2);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลใน EditItemTrans");
                }
                string messageETEMP2 = ProgramConfig.message.get("SaleProcess", "SaveEditTEMPDLYPTRANSIncomplete").message;
                throw new Exception(messageETEMP2);
                //throw new Exception("ไม่สามารถบันทึกแก้ไขข้อมูลใน TEMPDLYPTRANS");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveEditWithReasonAll");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        //public StoreResult saveEditItemTrans(string refNo, string aid, string aty, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds
        //    , string usr, string stt, string referRec, string reason, string upc)
        //{
        //    try
        //    {
        //        return command.saveEditItemTrans(refNo, aid, aty, rec, sty, vty, pcd, qnt, amt, fds, usr, stt, referRec, reason, upc);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StoreResult(ResponseCode.Error, ex.Message, "", "");
        //    }
        //}

        public StoreResult searchItem(string productCode, SearchItemAction action)
        {
            try
            {
                return command.searchItem(productCode, action);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.searchItem");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getProductIcon()
        {
            try
            {
                return command.getProductIcon();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getProductIcon");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayDeleteReason()
        {
            try
            {
                return command.displayReason(FunctionID.Sale_CancelWhileSale_CancelOrder_InputReason);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.displayDeleteReason");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getDisplayDiscountManual()
        {
            try
            {
                return command.getDisplayDiscountManual();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getDisplayDiscountManual");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayEditItemReason()
        {
            try
            {
                return command.displayReason(FunctionID.Sale_InputSaleItem_EditProduct_EditPrice_InputReason);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.displayEditItemReason");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayDeleteItemReason()
        {
            try
            {
                return command.displayReason(FunctionID.Sale_InputSaleItem_EditProduct_DeleteItem_InputReason);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.displayDeleteItemReason");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataTable selectPaymentType(string paymentType)
        {
            try
            {
                return command.selectPaymentType(paymentType);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectPaymentType");
                throw;
            }
        }

        public DataTable selectTempDlyptrans(string refNo)
        {
            try
            {
                return command.selectTempDlyptrans(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectTempDlyptrans");
                throw;
            }
        }

        public DataTable selectTypeFromMaxRec(string rec)
        {
            try
            {
                return command.selectTypeFromMaxRec(rec);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectTypeFromMaxRec");
                throw;
            }
        }

        //public DataTable selectMaxRecTempDlyptransForTypeP(string refNo)
        //{
        //    return command.selectMaxRecTempDlyptransForTypeP(refNo);
        //}

        public DataTable loadTempDlyptrans(string refNo)
        {
            try
            {
                return command.loadTempDlyptrans(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.loadTempDlyptrans");
                throw;
            }
        }

        public DataTable loadTempDlyForPayment(string refNo)
        {
            try
            {
                return command.loadTempDlyForPayment(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.loadTempDlyForPayment");
                throw;
            }
        }

        public DataTable selectDeleteItemTempDlyptrans(string refNo, string code, string quant, string price)
        {
            try
            {
                //if (ProgramConfig.IsStandAloneMode)
                //{
                //   return  _dtSaleMain.Select(String.Format(@"REF = '{0}'
                //                            AND STCODE = '{1}'
                //                            AND PCD = '{2}'
                //                            AND QNT = '{3}'
                //                            AND AMT = '{4}'
                //                            AND STT <> 'V'
                //                            AND REC >= 0", ProgramConfig.saleRefNo, ProgramConfig.storeCode, code, quant, price)).CopyToDataTable();
                //}
                //else
                //{
                   return command.selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, code, quant, price);
                //}
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDeleteItemTempDlyptrans");
                throw;
            }
        }

        public DataTable selectAllDeleteItemTempDlyptrans(string refNo, string code, string price, string discID)
        {
            try
            {
                return command.selectAllDeleteItemTempDlyptrans(refNo, code, price, discID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectAllDeleteItemTempDlyptrans");
                throw;
            }
        }

        public StoreResult updatePrintTempDlyptrans()
        {
            try
            {
                return command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "1");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updatePrintTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateOpenCashDrawer(string vtime)
        {
            try
            {
                return command.updateOpenCashDrawer(vtime, ProgramConfig.saleRefNo, "0");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updateOpenCashDrawer");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataTable selectDISCSUMMARY(string refNo, List<string> pmType)
        {
            try
            {
                return command.selectDISCSUMMARY(refNo, pmType);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDISCSUMMARY");
                throw;
            }
        }

        public StoreResult saveSaleTransaction(string memberId)
        {
            try
            {
                return command.saveSaleTransaction(memberId);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveSaleTransaction");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveSaleTransactionUpdateABBNO(string mode)
        {
            try
            {
                return command.saveSaleTransactionUpdateABBNO(mode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveSaleTransactionUpdateABBNO");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult getConcludeSale(string abbNo)
        {
            try
            {
                return command.concludeSale(abbNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getConcludeSale");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult syncToDataTank(string eventName, FunctionID functionId, string referenceNo, string rec)
        {
            try
            {
                return command.syncToDataTank(eventName, functionId, referenceNo, rec, ProgramConfig.abbNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.syncToDataTank");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        //public StoreResult saveTempPay(string refNo, string type, string pay, string amt, string chg, string fxcuqnt, string pdisc)
        //{
        //    try
        //    {
        //        return command.saveTempPay(refNo, type, pay, amt, chg, fxcuqnt, pdisc);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StoreResult(ResponseCode.Error, ex.Message, "", "");
        //    }
        //}

        public StoreResult saveScanGFSL(string gfslNo, string gfslAmt)
        {
            try
            {
                return command.saveScanGFSL(gfslNo, gfslAmt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveScanGFSL");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        //public StoreResult saveEdcTran(string cardNo, string approveCode, string edcAmount)
        //{
        //    try
        //    {
        //        return command.saveEdcTran(cardNo, approveCode, edcAmount);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StoreResult(ResponseCode.Error, ex.Message, "", "");
        //    }
        //}

        public DataTable selectDataToDeleteCashTempDLY()
        {
            try
            {
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    return command.loadTempCREDPAY_TRANS_PAY(ProgramConfig.creditSaleNo, ProgramConfig.paymentType);
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    return command.loadTEMP_PODTRANS_PAY(ProgramConfig.podRefNo, ProgramConfig.paymentType);
                }
                else
                {
                    return command.selectDataToDeleteCashTempDLY();
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataToDeleteCashTempDLY");
                throw;
            }
        }

        public DataTable selectDataToDeleteTempDLY()
        {
            try
            {
                return command.selectDataToDeleteTempDLY();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataToDeleteTempDLY");
                throw;
            }
        }

        public DataTable selectDataToDeleteTempDLY(string rec)
        {
            try
            {
                return command.selectDataToDeleteTempDLY(rec);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataToDeleteTempDLY");
                throw;
            }
        }

        public DataTable selectDataToDeleteTempDLYByPCD(string pcd)
        {
            try
            {
                return command.selectDataToDeleteTempDLYByPCD(pcd);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataToDeleteTempDLYByPCD");
                throw;
            }
        }

        public DataTable selectDataToDeleteByVTYTempDLY(string vty)
        {
            try
            {
                return command.selectDataToDeleteByVTYTempDLY(vty);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataToDeleteByVTYTempDLY");
                throw;
            }
        }

        public DataTable selectDiscountCode(string discountCode)
        {
            try
            {
                return command.selectDiscountCode(discountCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDiscountCode");
                throw;
            }
        }

        public bool deleteDiscount(string recNo)
        {
            try
            {
                DataRow[] row1 = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + recNo + "'");
                foreach (var rows in row1)
                    rows.Delete();

                return command.deleteTempDlyTransByRefRec(recNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteDiscount");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return false;
            }
        }

        public StoreResult deletePaymentType(string recNo, LoadFromTable load = LoadFromTable.TEMPDLYPTRANS)
        {
            try
            {
                if (load == LoadFromTable.TEMP_PODTRANS_PAY)
                {
                    command.DeleteTEMP_PODTRANS_PAY(rec: recNo);
                }
                else if (load == LoadFromTable.TEMPCREDPAY_TRANS_PAY)
                {
                    command.DeleteTempCREDPAY_TRANS_PAY(seq: recNo);
                }
                else
                {
                    int recV = 0;
                    int recF = 0;

                    if (command.selectDataToDeleteByVTYTempDLY("V").Rows.Count != 0)
                    {
                        recV = Convert.ToInt32(command.selectDataToDeleteByVTYTempDLY("V").Rows[0]["REC"].ToString());
                    }
                    else
                    {
                        recV = 0;
                    }

                    if (command.selectDataToDeleteByVTYTempDLY("F").Rows.Count != 0)
                    {
                        recF = Convert.ToInt32(command.selectDataToDeleteByVTYTempDLY("F").Rows[0]["REC"].ToString());
                    }
                    else
                    {
                        recF = 0;
                    }

                    removeDtSaleMain(recV.ToString());
                    if (command.deleteTempDlyTransByRefRec(recV.ToString()))
                    {
                        removeDtSaleMain(recF.ToString());
                        command.deleteTempDlyTransByRefRec(recF.ToString());
                    }

                    DataRow[] row1 = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + recNo + "'");
                    foreach (var rows in row1)
                        removeDtSaleMain(recNo);

                    _dtSaleMain.AcceptChanges();
                    
                }
                return command.deletePaymentType(recNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deletePaymentType");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        //public StoreResult deletePaymentTypeForCoupon(string recNo, string paymentType)
        //{
        //    try
        //    {
        //        DataRow[] row1 = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + recNo + "'");
        //        foreach (var rows in row1)
        //            rows.Delete();

        //        return command.deletePaymentTypeForCoupon(recNo, paymentType);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StoreResult(ResponseCode.Error, ex.Message, "", "");
        //    }
        //}

        public ProcessResult deleteAllPayment(Func<Profile, string, bool> CheckAuth)
        {
            StoreResult resDelete = null;

            try
            {
                string recNo = "";
                string GFSLNO = "";
                DataTable resSelectDelete = command.selectDataToDeleteByVTYTempDLY("P");
                if (resSelectDelete.Rows.Count != 0)
                {
                    for (int i = 0; i < resSelectDelete.Rows.Count; i++)
                    {
                        recNo = resSelectDelete.Rows[i]["REC"].ToString();

                        DataRow[] row1 = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + recNo + "'");
                        removeDtSaleMain(recNo);
                        //foreach (var rows in row1)
                        //    rows.Delete();
                        _dtSaleMain.AcceptChanges();

                        resDelete = command.deleteAllPayment(recNo);
                    }
                }
                else
                {
                    resDelete = command.deleteAllPayment(recNo);
                }


                

                DataTable resSelectGFSL = command.selectDataSCANGFSL();
                if (resSelectGFSL.Rows.Count != 0)
                {
                    for (int i = 0; i < resSelectGFSL.Rows.Count; i++)
                    {
                        GFSLNO = resSelectGFSL.Rows[i]["GFSL_NO"].ToString();
                        resDelete = command.delGFSL(GFSLNO);
                    }
                }

                resDelete = command.delCouponAll();
                if (resDelete.response.next)
                {
                    resDelete = command.ClearPromotion();
                    if (resDelete.response.next)
                    {
                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                        {
                            resDelete = command.DeleteTEMP_PODTRANS_PAY();
                            if (!resDelete.response.next)
                            {
                                return new ProcessResult(resDelete, false);
                            }
                        }

                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                        {
                            resDelete = command.DeleteTempCREDPAY();
                            if (!resDelete.response.next)
                            {
                                return new ProcessResult(resDelete, false);
                            }
                        }

                        AutoVoidQR_BSC(ProgramConfig.saleRefNo, CheckAuth);
                        resDelete = command.delQRPayTrans();
                        if (resDelete.response.next)
                        {
                            return new ProcessResult(resDelete, true);
                        }
                    }
                }

                return new ProcessResult(resDelete, false);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteAllPayment");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public ProcessResult deleteAllPaymentTempDLYPTRANS()
        {
            StoreResult resDelete = null;

            try
            {
                string recNo = "";
                DataTable resSelectDelete = command.selectDataToDeleteByVTYTempDLY("P");
                if (resSelectDelete.Rows.Count != 0)
                {
                    for (int i = 0; i < resSelectDelete.Rows.Count; i++)
                    {
                        recNo = resSelectDelete.Rows[i]["REC"].ToString();

                        DataRow[] row1 = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + recNo + "'");
                        removeDtSaleMain(recNo);
                        //foreach (var rows in row1)
                        //    rows.Delete();
                        _dtSaleMain.AcceptChanges();

                        resDelete = command.deleteAllPayment(recNo);
                    }
                }
                        
                return new ProcessResult(resDelete, true);

            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteAllPayment");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public DataTable selectDataSCANGFSL()
        {
            try
            {
                return command.selectDataSCANGFSL();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataSCANGFSL");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return null;
            }
        }

        public StoreResult saveCloseDrawer(FunctionID functionId, string closeTime, string number)
        {
            try
            {
                return command.saveCloseDrawer(functionId, closeTime, number);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveCloseDrawer");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveToDataTank()
        {
            try
            {
                Profile check = ProgramConfig.getProfile(FunctionID.Sale_ProcessAfterSaveSaleTransaction);
                if (check.policy == PolicyStatus.Work) //2
                {
                    string vrec = "1";
                    StoreResult res = command.syncToDataTank("CloseSale", FunctionID.Sale_SaveSaleTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.saleRefNo, vrec, ProgramConfig.abbNo);
                    if (res.response.next)
                    {
                        return new StoreResult(ResponseCode.Success, "Success");
                    }
                    return new StoreResult(res.response, res.responseMessage, res.helpMessage);
                    //string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    //throw new Exception(messageTEMP);
                }
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveToDataTank");
                throw;
            }
            catch (Exception ex)
            {
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }


        public ProcessResult saleAutoVoid(string refNo, string abb, string abb_ini, string openTime, Func<Profile, string, bool> CheckAuth = null, Action AutoVoidEDC = null)
        {
            try
            {
                List<NotifyMessage> message = null;
                StoreResult res = null;
                Profile check = ProgramConfig.getProfile(FunctionID.Sale_ProcessAfterSaveSaleTransaction);
                if (check.policy == PolicyStatus.Work) //2
                {
                    string voidReceiptNo = "";
                    res = command.checkLastReceipt(refNo, abb, abb_ini, FunctionID.Sale_AutoVoid);
                    if (res.response == ResponseCode.Success)
                    {
                        return new ProcessResult(res, false);
                    }
                    else
                    {
                        if (res.response == ResponseCode.Information && res.otherData.Rows[0]["Action_Type"].ToString() != "N/A")
                        {
                            if (message == null) { message = new List<NotifyMessage>(); }
                            message.Add(new NotifyMessage(res.response, res.responseMessage, res.helpMessage));

                            ProgramConfig.abbNo = res.otherData.Rows[0]["ABB_NO_INI"].ToString();
                            ProgramConfig.running.updateValue();
                        }

                        //Auto Void QR
                        //select * from QRPAYTRANS where STORE_CODE='00800' and LOCKNO='010' and REF='010001031' and ACTION_TYPE='SA' and CHANNEL='BC' and STT='A'
                        AutoVoidQR_BSC(refNo, CheckAuth);

                        AutoVoidEDC();

                        if (res.otherData.Rows[0]["Action_Type"].ToString() == "V")
                        {
                            //ไม่ชัวร์ว่าต้องส่งค่าอะไรไปบ้าง
                            res = command.saveVoidTransaction(ProgramConfig.abbNo, openTime, "0", refNo, FunctionID.Sale_SaveVoidTrans);
                            if (!res.response.next)
                            {
                                return new ProcessResult(res, false);
                            }
                            else
                            {
                                if (res.response == ResponseCode.Information)
                                {
                                    if (message == null) { message = new List<NotifyMessage>(); }
                                    message.Add(new NotifyMessage(res.response, res.responseMessage, res.helpMessage));
                                }
                            }

                            voidReceiptNo = res.otherData.Rows[0]["VoidReceiptNo"].ToString();

                            check = ProgramConfig.getProfile(FunctionID.Sale_ConcludeVoid);
                            if (check.policy == PolicyStatus.Work) //2
                            {
                                //ไม่ชัวร์ว่าต้องส่งค่าอะไรไปบ้าง
                                res = command.concludeVoid(voidReceiptNo, ProgramConfig.saleRefNo, FunctionID.Sale_ConcludeVoid);
                                if (!res.response.next)
                                {
                                    return new ProcessResult(res, false);
                                }
                                else
                                {
                                    if (res.response == ResponseCode.Information)
                                    {
                                        if (message == null) { message = new List<NotifyMessage>(); }
                                        message.Add(new NotifyMessage(res.response, res.responseMessage, res.helpMessage));
                                    }
                                }
                            }

                            //ไม่ชัวร์ว่าต้องส่งค่าอะไรไปบ้าง
                            check = ProgramConfig.getProfile(FunctionID.Sale_SyncVoidToDataTank);
                            if (check.policy == PolicyStatus.Work) //2
                            {
                                res = command.syncToDataTank("Void", FunctionID.Sale_SyncVoidToDataTank, voidReceiptNo, "", "");
                                if (!res.response.next)
                                {
                                    return new ProcessResult(res, false);
                                }
                                else
                                {
                                    if (res.response == ResponseCode.Information)
                                    {
                                        if (message == null) { message = new List<NotifyMessage>(); }
                                        message.Add(new NotifyMessage(res.response, res.responseMessage, res.helpMessage));
                                    }
                                }
                            }

                            //ไม่ชัวร์ว่าต้องส่งค่าอะไรไปมั้ง
                            check = ProgramConfig.getProfile(FunctionID.Sale_PrintVoidReceipt);
                            if (check.policy == PolicyStatus.Work) //2
                            {
                                res = command.PrintVoidReceipt(voidReceiptNo, ProgramConfig.saleRefNo, FunctionID.Sale_PrintVoidReceipt);
                                if (!res.response.next)
                                {
                                    return new ProcessResult(res, false);
                                }
                                else
                                {
                                    if (res.response == ResponseCode.Information)
                                    {
                                        if (message == null) { message = new List<NotifyMessage>(); }
                                        message.Add(new NotifyMessage(res.response, res.responseMessage, res.helpMessage));
                                    }
                                }
                                DataTable dt = res.otherData;
                                Hardware.printTermal(dt);
                            }
                        }
                    }
                }
                return new ProcessResult(res, false, notify: message);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saleAutoVoid");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        private void AutoVoidQR_BSC(string refNo, Func<Profile, string, bool> CheckAuth)
        {
            var resQr = command.selectQRPAYTRANS(refNo);
            if (resQr.otherData.Rows.Count > 0)
            {
                DataRow dr = resQr.otherData.Rows[0];
                string org_tranID = dr["ORG_TRANID"].ToString();
                string pay_amount = dr["PAY_AMOUNT"].ToString();

                var dt = command.selectParameterForQR(refNo, "RQ", "BC", "VP");
                if (dt.Rows.Count > 0)
                {
                    string prn_TxnID = dt.Rows[0]["TranID"].ToString();
                    string seq = dt.Rows[0]["Seq"].ToString();
                    resQr = command.API_POS_BSC_VOID(ProgramConfig.saleRefNo, prn_TxnID, org_tranID, pay_amount);
                    if (resQr.response.next)
                    {
                        resQr = command.saveQRPayTransOnline(refNo, "VD", seq, prn_TxnID, dr["ACCOUNT_NAME"].ToString(), dr["QR_CODE"].ToString(), pay_amount
                             , "", "", "BC", dr["BANKCODE"].ToString(), dr["TOKEN_ID"].ToString(), dr["OTA"].ToString(), dr["BSD"].ToString(), dr["TEPA_CODE"].ToString(),
                             dr["Sending_Bank"].ToString(), dr["Receving_Bank"].ToString(), dr["REF2"].ToString(), dr["TRANSREF"].ToString(),
                             trn_status: dr["TXN_Status"].ToString(),
                             stt: "A",
                             onlineFlag: dr["ONLINE_FLAG"].ToString());
                    }
                    else if (resQr.response == ResponseCode.Warning)
                    {
                        var dt2 = command.selectParameterForQR(refNo, "RQ", "BC", "IV");
                        if (dt2.Rows.Count > 0)
                        {
                            string prn_TxnID2 = resQr.otherData.Rows[0]["TranID"].ToString();

                            resQr = command.API_POS_BSC_INQUIRY_VOID_STATUS(prn_TxnID2, org_tranID, pay_amount);
                            if (resQr.response.next)
                            {
                                resQr = command.saveQRPayTransOnline(refNo, "VD", seq, prn_TxnID2, dr["ACCOUNT_NAME"].ToString(), dr["QR_CODE"].ToString(), pay_amount
                                         , "", "", "BC", dr["BANKCODE"].ToString(), dr["TOKEN_ID"].ToString(), dr["OTA"].ToString(), dr["BSD"].ToString(), dr["TEPA_CODE"].ToString(),
                                         dr["Sending_Bank"].ToString(), dr["Receving_Bank"].ToString(), dr["REF2"].ToString(), dr["TRANSREF"].ToString(),
                                         trn_status: dr["TXN_Status"].ToString(),
                                         stt: "A",
                                         onlineFlag: dr["ONLINE_FLAG"].ToString(),
                                         tranID: prn_TxnID2);
                            }
                            else if (resQr.response == ResponseCode.Error)
                            {
                            RETRY:
                                var check = ProgramConfig.getProfile(FunctionID.Sale_AutoVoid_BscanC);
                                if (!CheckAuth(check, "Auto Void QR B Scan C"))
                                {
                                    goto RETRY;
                                };

                                resQr = command.saveQRPayTransOnline(refNo, "VD", seq, org_tranID, dr["ACCOUNT_NAME"].ToString(), dr["QR_CODE"].ToString(), pay_amount
                                             , "", "", "BC", dr["BANKCODE"].ToString(), dr["TOKEN_ID"].ToString(), dr["OTA"].ToString(), dr["BSD"].ToString(), dr["TEPA_CODE"].ToString(),
                                             dr["Sending_Bank"].ToString(), dr["Receving_Bank"].ToString(), dr["REF2"].ToString(), dr["TRANSREF"].ToString(),
                                             trn_status: dr["TXN_Status"].ToString(),
                                             stt: "A",
                                             onlineFlag: dr["ONLINE_FLAG"].ToString(),
                                             tranID: prn_TxnID2);

                                if (resQr.response.next)
                                {
                                    command.saveQRPayTransVoid(ProgramConfig.saleRefNo, org_tranID, pay_amount, "");
                                }
                            }
                        }
                    }
                    else if (resQr.response == ResponseCode.Error)
                    {
                    RETRY:
                        var check = ProgramConfig.getProfile(FunctionID.Sale_AutoVoid_BscanC);
                        if (!CheckAuth(check, "Auto Void QR B Scan C"))
                        {
                            goto RETRY;
                        };

                        resQr = command.saveQRPayTransOnline(ProgramConfig.voidRefNo, "VD", seq, org_tranID, dr["ACCOUNT_NAME"].ToString(), dr["QR_CODE"].ToString(), pay_amount
                                     , "", "", "BC", dr["BANKCODE"].ToString(), dr["TOKEN_ID"].ToString(), dr["OTA"].ToString(), dr["BSD"].ToString(), dr["TEPA_CODE"].ToString(),
                                     dr["Sending_Bank"].ToString(), dr["Receving_Bank"].ToString(), dr["REF2"].ToString(), dr["TRANSREF"].ToString(),
                                     trn_status: dr["TXN_Status"].ToString(),
                                     stt: "A",
                                     onlineFlag: dr["ONLINE_FLAG"].ToString(),
                                     tranID: prn_TxnID);

                        if (resQr.response.next)
                        {
                            command.saveQRPayTransVoid(ProgramConfig.saleRefNo, org_tranID, pay_amount, "");
                        }
                    }
                }


            }
        }

        //public PageResult saveCloseDrawer(FunctionID functionId, string closeTime, string number)
        //{
        //    try
        //    {
        //        StoreResult result = command.saveCloseDrawer(functionId, closeTime, number);
        //        if (result.response.next)
        //        {
        //            Profile check = ProgramConfig.getProfile(FunctionID.Sale_ProcessAfterSaveSaleTransaction);
        //            if (check.policy == PolicyStatus.Work) //2
        //            {
        //                string vrec = "1";
        //                StoreResult res = command.syncToDataTank("CloseSale", FunctionID.Sale_SaveSaleTransaction_SynchSaleTransactiontoDataTank, ProgramConfig.saleRefNo, vrec, ProgramConfig.abbNo);
        //                if (res.response.next)
        //                {
        //                    return new PageResult(false, ResponseCode.Success);
        //                }
        //                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
        //                throw new Exception(messageTEMP);
        //            }
        //            return new PageResult(false, ResponseCode.Success);
        //        }
        //        string messageUpdate = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
        //        throw new Exception(messageUpdate);
        //    }
        //    catch (ServerConnectionException ex)
        //    {
        //        AppLog.writeLog("connection to server lost at saveCloseDrawer");
        //        AppLog.writeLog(ex);
        //        return new PageResult(true, ResponseCode.Warning, "", "", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        command.rollback();
        //        AppLog.writeLog(ex);
        //        return new PageResult(false, ResponseCode.Error, ex.Message);
        //    }
        //}

        public StoreResult getPaymentCode(string paymentNumber)
        {
            try
            {
                return command.getPaymentCode(paymentNumber);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.getPaymentCode");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkGiftVoucher(string giftVoucherNo)
        {
            try
            {
                return command.checkGiftVoucher(giftVoucherNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.checkGiftVoucher");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult displayCoupon()
        {
            try
            {
                return command.displayCoupon();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.displayCoupon");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkCoupon(string couponNo, int qty, string memberId)
        {
            try
            {
                return command.checkCoupon(couponNo, qty, memberId);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.checkCoupon");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult delCoupon(string couponNo, int qty, int vRow)
        {
            try
            {
                return command.delCoupon(couponNo, qty, vRow);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.delCoupon");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult couponUse()
        {
            try
            {
                return command.couponUse();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.couponUse");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult delGFSL(string voucherNo)
        {
            try
            {
                return command.delGFSL(voucherNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.delGFSL");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult delQRPayTrans(string paymentCode)
        {
            try
            {
                return command.delQRPayTrans(paymentCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.delGFSL");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

 

        public StoreResult printCancel(FunctionID function)
        {
            try
            {
                return command.printCancel(function, ProgramConfig.saleRefNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.printCancel");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult printReceipt(string receiptNo)
        {
            try
            {
                return command.printReceipt(receiptNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.printReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult checkMinCashUnitAmount(string paymentCode, string payAmt, string currencyCode, string mode = "1")
        {
            try
            {
                return command.checkMinCashUnitAmount(paymentCode, payAmt, ProgramConfig.saleRefNo, currencyCode, mode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.checkMinCashUnitAmount");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        private DataRow insertDtSaleMain(string refNo, string rec, string sty, string vty, string pcd, string qnt, string amt, string fds,
            string usr, string egp, string stt, string pdisc, string discid, string upc, string dty, string discamt, string reason,
            string stv, string DisplayRec = "", string DisplayPrice = "", string DisplayAmt = "", string TotalPrice = "",
            string ProductName = "", string PromotionName = "", string PromotionPrice = "", string isFFNRTC = "", string product_type = "")
        {
            int tempInt;
            double tempdouble;

            DataRow row = _dtSaleMain.NewRow();

            row["STCODE"] = ProgramConfig.storeCode;
            row["REF"] = refNo;
            if (int.TryParse(rec, out tempInt))
            {
                row["REC"] = rec;                               //Int32
            }
            row["STY"] = sty;
            row["VTY"] = vty;
            row["PCD"] = pcd;
            if (double.TryParse(qnt, out tempdouble))
            {
                row["QNT"] = tempdouble;                               //double
            }
            if (double.TryParse(amt, out tempdouble))
            {
                row["AMT"] = tempdouble;                               //double
            }
            if (double.TryParse(fds, out tempdouble))
            {
                row["FDS"] = tempdouble;                  //double
            }
            row["TTM"] = DateTime.Now;                      //DateTime
            row["USR"] = usr;
            if (double.TryParse(egp, out tempdouble))
            {
                row["EGP"] = tempdouble;                           //double
            }
            row["STT"] = stt;
            if (double.TryParse(pdisc, out tempdouble))
            {
                row["PDISC"] = tempdouble;        //double
            }
            if (int.TryParse(discid, out tempInt))
            {
                row["DISCID"] = tempInt;               //Int32
            }
            if (double.TryParse(upc, out tempdouble))
            {
                row["UPC"] = tempdouble;                  //double
            }
            row["DTY"] = dty;

            if (double.TryParse(discamt, out tempdouble))
            {
                row["DISCAMT"] = tempdouble;
            }
            if (int.TryParse(reason, out tempInt))
            {
                row["REASON_ID"] = tempInt;     //Int32
            }
            row["STV"] = stv;

            if (int.TryParse(DisplayRec, out tempInt))
            {
                row["DisplayRec"] = tempInt;
            }
            if (double.TryParse(DisplayPrice, out tempdouble))
            {
                row["DisplayPrice"] = tempdouble;
            }
            if (double.TryParse(DisplayAmt, out tempdouble))
            {
                row["DisplayAmt"] = tempdouble;
            }
            if (double.TryParse(TotalPrice, out tempdouble))
            {
                row["TotalPrice"] = tempdouble;
            }

            row["ProductName"] = ProductName;
            row["PromotionName"] = PromotionName;

            if (double.TryParse(PromotionPrice, out tempdouble))
            {
                row["PromotionPrice"] = tempdouble;
            }

            row["IsFFNRTC"] = isFFNRTC;
            row["PRODUCT_TYPE"] = product_type;

            _dtSaleMain.Rows.Add(row);
            return row;
        }

        private void removeDtSaleMain(string rec)
        {
            DataRow[] row = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and REC = '" + rec + "'");
            foreach (DataRow item in row)
            {
                _dtSaleMain.Rows.Remove(item);
            }
        }

        private void removeDtSaleMainWithType(string type)
        {
            DataRow[] row = _dtSaleMain.Select("REF = '" + ProgramConfig.saleRefNo + "' and VTY = '" + type + "'");
            foreach (DataRow item in row)
            {
                _dtSaleMain.Rows.Remove(item);
            }
        }

        // payment
        public StoreResult savePaymentCashBalance(string amtPrice, string txtBalance, string pmCode, string total, string payAmt, string egp, string currency_code)
        {
            command.newTransaction();
            try
            {
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                    string chg = "";
                    if (Convert.ToDouble(total) > Convert.ToDouble(amtPrice))
                    {
                        chg = "";
                    }
                    else
                    {
                        chg = (Convert.ToDouble(amtPrice) - Convert.ToDouble(total)).ToString(ProgramConfig.amountFormatString);
                    }

                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    //Save TEMP_CREDPay_TRANS_PAY                   
                    double maxSeq = command.selectMaxSeqTempCREDPAY_TRANS_PAY(ProgramConfig.creditSaleNo) + 1;
                    if (!command.saveTempCREDPAY_TRANS_PAY(maxSeq.ToString(), pmCode, "", amtPrice, chg))
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TempCREDPAY_TRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                    }
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    int maxRecPOD = command.selectMaxRecTEMP_PODTRANS_PAY(ProgramConfig.podRefNo);
                    var res = savePaymentPOD(ProgramConfig.payment.getPCD(pmCode, currency_code), "", amtPrice, chg, "", "", "", "", "", "", "", "", "", maxRecPOD.ToString());
                    if (!res.response.next)
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TEMP_PODTRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                    }
                }
                else
                {
                    if (command.saveTempPay(ProgramConfig.saleRefNo, "P", ProgramConfig.payment.getPCD(pmCode, currency_code), amtPrice, "0", "0", "0"))
                    {
                        double maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                        int maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                        insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", ProgramConfig.payment.getPCD(pmCode, currency_code), maxRecQntInt.ToString(), amtPrice, "0", ProgramConfig.userId, pmCode == "FXCU" ? egp : "0"
                        , "", "0", "0", "0", "F", "0", "0", "0");
                        if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", ProgramConfig.payment.getPCD(pmCode, currency_code), maxRecQntInt.ToString(), amtPrice, "0", ProgramConfig.userId, pmCode == "FXCU" ? egp : "0"
                        , "", "0", "0", "0", "F", "0", "0", "0"))
                        {
                            _dtSaleMain.RejectChanges();
                            command.rollback();
                            string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(messageTEMP);
                            //_dtSaleMain.AcceptChanges();
                            //command.commit();
                            //return new StoreResult(ResponseCode.Success, "Success");
                        }

                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }
                    else
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                        throw new Exception(messageTEMPPAY);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                    }
                }

                _dtSaleMain.AcceptChanges();
                command.commit();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCashBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentRedeemBalance(string amtPrice, string txtBalance, string type)
        {
            command.newTransaction();
            try
            {
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                bool chk = true;
                if (type == "LTYP")
                {
                    chk = command.saveTempPay(ProgramConfig.saleRefNo, "P", type, amtPrice, txtBalance, "0", "0");
                }

                if (chk)
                {
                    double maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                    int maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", type, maxRecQntInt.ToString(), amtPrice, txtBalance, ProgramConfig.userId, "0"
                    , "", "0", "0", "0", "F", "0", "0", "0");
                    if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", type, maxRecQntInt.ToString(), amtPrice, txtBalance, ProgramConfig.userId, "0"
                    , "", "0", "0", "0", "F", "0", "0", "0"))
                    {
                        _dtSaleMain.AcceptChanges();
                        command.commit();
                        return new StoreResult(ResponseCode.Success, "Success");
                    }
                    string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    throw new Exception(messageTEMP);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }
                string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                throw new Exception(messageTEMPPAY);
                //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCashBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentCreditBalance(string creditCard, string approveCode, string dty, string amtPrice, string balance, bool isInsertEDCTrans, string paynum)
        {
            try
            {
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                //string chg = "";
                //if (Convert.ToDouble(total) > Convert.ToDouble(amtPrice))
                //{
                //    chg = "";
                //}
                //else
                //{
                //    chg = (Convert.ToDouble(amtPrice) - Convert.ToDouble(total)).ToString(ProgramConfig.amountFormatString);
                //}

                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    //Save TEMP_CREDPay_TRANS_PAY
                    double maxSeq = command.selectMaxSeqTempCREDPAY_TRANS_PAY(ProgramConfig.creditSaleNo) + 1;
                    string pmCode = creditCard.Substring(0, 4);
                    if (!command.saveTempCREDPAY_TRANS_PAY(maxSeq.ToString(), pmCode, paynum, amtPrice, balance))
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TempCREDPAY_TRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                    }
                    return new StoreResult(ResponseCode.Success);
                }
                else
                {
                    string vty = "P";
                    string egp = "0";
                    string stt = "";
                    string pdisc = "0";
                    string discid = "0";
                    string upc = "0";
                    string discamt = "0";
                    string reason = "0";
                    string stv = "0";

                    if (command.saveTempPay(ProgramConfig.saleRefNo, vty, creditCard, amtPrice, balance, "0", "0"))
                    {
                        if (dty == "O" || (isInsertEDCTrans && command.saveEdcTran(creditCard, approveCode, amtPrice)))
                        {
                            double maxRecQnt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                            int maxRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRec.ToString(), sty, vty, creditCard, maxRecQnt.ToString()
                                , amtPrice, balance, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                            if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec.ToString(), sty, vty, creditCard, maxRecQnt.ToString()
                                , amtPrice, balance, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                            {
                                _dtSaleMain.AcceptChanges();
                                command.commit();
                                return new StoreResult(ResponseCode.Success);
                            }
                            string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(messageTEMP);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                        }
                        string messageEDCT = ProgramConfig.message.get("SaleProcess", "SaveEDCTRANSIncomplete").message;
                        throw new Exception(messageEDCT);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง EDCTRANS");
                    }
                    string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                    throw new Exception(messageTEMPPAY);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCreditBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentVoucherBalance(string voucherNo, string paymentCode, string amtPrice, string balance, string upc, string total ,string payAmt)
        {
            try
            {
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";
                string vty = "P";
                string egp = "0";
                string stt = "";
                string pdisc = "0";
                string discid = "0";
                string dty = "";
                string discamt = "0";
                string reason = "0";
                string stv = "0";


                //if (Convert.ToDouble(payAmt) >= 0)
                //{
                //    string totalPayAmt = (Convert.ToDouble(payAmt) + Convert.ToDouble(amtPrice)).ToString();
                //    StoreResult res = GetChange(total, totalPayAmt);
                //    if (res.response.next)
                //    {
                //        if (res.response == ResponseCode.Ignore)
                //        {
                //            DataTable dtCHG = command.selectTempChange(paymentCode);
                //            if (dtCHG != null && dtCHG.Rows.Count > 0)
                //            {
                //                balance = dtCHG.Rows[0]["CHG"].ToString();
                //            }
                //        }
                //        //else
                //        //{
                //        //    dt = res.otherData;

                //        //    if (dt.AsEnumerable().Any(a => a["PM_CODE"].ToString().Trim() == paymentCode))
                //        //    {
                //        //        txtBalance = "0";
                //        //    }
                //        //}
                //    }
                //}




                //saveScan
                StoreResult resScan = command.saveScanGFSL(voucherNo, amtPrice);
                if (resScan.response.next)
                {
                    //saveTempPay
                    if (command.saveTempPay(ProgramConfig.saleRefNo, vty, paymentCode + voucherNo, amtPrice, balance, "0.00", "0.00"))
                    {
                        double maxRecQnt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                        int maxRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                        if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec.ToString(), sty, vty, paymentCode + voucherNo, maxRecQnt.ToString()
                            , amtPrice, balance, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                        {
                            _dtSaleMain.AcceptChanges();
                            command.commit();
                            return new StoreResult(ResponseCode.Success);
                        }
                        string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }
                    string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                    throw new Exception(messageTEMPPAY);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                }
                _dtSaleMain.RejectChanges();
                command.rollback();
                return resScan;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentVoucherBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentOffline(string amtPrice, string pcd, List<PaymentStepDet> lstPayDet = null, string refNoInp = "", string total = "", string depoRef = "")
        {
            command.newTransaction();
            try
            {
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                //ไว้กรณี จ่าย QR Payment offline ของลาว ต้อง sum amt ให้อยู่ใน record เดียว
                string amtDupPrice = SumDupPaymentAmount(pcd, amtPrice, ProgramConfig.paymentDupAmt);

                int maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                string chg = "";
                if (Convert.ToDouble(total) > Convert.ToDouble(amtPrice))
                {
                    chg = "";
                }
                else
                {
                    chg = (Convert.ToDouble(amtPrice) - Convert.ToDouble(total)).ToString(ProgramConfig.amountFormatString);
                }

                if (pcd.StartsWith("DEPO"))
                {
                    string refNo = pcd.Substring(4, pcd.Length - 4);
                    var res = command.saveTempdeposit_trans_history(refNo, amtDupPrice, depoRef);
                    if (!res.response.next)
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TEMPDEPOSIT_TRANS_HISTORY";
                        throw new Exception(messageTEMP);
                    }
                }

                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                {
                    //Save TEMP_CREDPay_TRANS_PAY
                    string payNum = lstPayDet.Select(s => s.MainRef).FirstOrDefault();
                    string pmCode = lstPayDet.Select(s => s.PMCode).FirstOrDefault();
                    maxRecInt = command.selectMaxSeqTempCREDPAY_TRANS_PAY(ProgramConfig.creditSaleNo) + 1;
                    if (!command.saveTempCREDPAY_TRANS_PAY(maxRecInt.ToString(), pmCode, payNum, amtPrice, chg))
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TempCREDPAY_TRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                    }
                }
                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                {
                    string pmCode = pcd.Substring(0, 4);
                    maxRecInt = command.selectMaxRecTEMP_PODTRANS_PAY(ProgramConfig.podRefNo);
                    var res = savePaymentPOD(pmCode, "", amtDupPrice, chg, "", "", "", "", "", "", "", "", "", maxRecInt.ToString());
                    if (!res.response.next)
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TEMP_PODTRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                    }
                }
                else
                {
                    if (command.saveTempPay(ProgramConfig.saleRefNo, "P", pcd, amtDupPrice, chg, "0", "0"))
                    {
                        double maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                        maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                        insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", pcd, maxRecQntInt.ToString(), amtDupPrice, chg, ProgramConfig.userId, "0"
                        , "", "0", "0", "0", "F", "0", "0", "0");
                        if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", pcd, maxRecQntInt.ToString(), amtDupPrice, chg, ProgramConfig.userId, "0"
                        , "", "0", "0", "0", "F", "0", "0", "0"))
                        {
                            _dtSaleMain.RejectChanges();
                            command.rollback();
                            string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(messageTEMP);
                        }
                        else
                        {
                            if (pcd.StartsWith("QR") && refNoInp != "")
                            {
                                if (!command.saveQRPayTransOffline(ProgramConfig.saleRefNo, "SA", Convert.ToInt32(maxRecQntInt).ToString(), refNoInp, amtDupPrice, "A", "M", "MN", pcd))
                                {
                                    _dtSaleMain.RejectChanges();
                                    command.rollback();
                                    string messageQRPayTrans = "Cannot Insert QRPayTrans";
                                    throw new Exception(messageQRPayTrans);
                                }
                            }
                        }

                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }
                    else
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                        throw new Exception(messageTEMPPAY);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                    }
                }

                if (lstPayDet != null && lstPayDet.Count > 0)
                {
                    foreach (var item in lstPayDet)
                    {
                        //command.saveTemppayDetail(item.PMCode, item.PaymentGroupID, item.Seq, item.StepID, item.DataType, item.DataValue);
                        var res = command.saveTemppayDetail(item.PMCode + item.MainRef, item.PaymentGroupID, item.Seq, item.StepID, item.DataType, item.DataValue, maxRecInt.ToString());
                        if (!res.response.next)
                        {
                            _dtSaleMain.RejectChanges();
                            command.rollback();
                            string messageTEMP = "ไม่สามารถบันทึกข้อมูลลง TEMPPAY_DETAIL"; //ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(messageTEMP);
                        }
                    }
                }

                _dtSaleMain.AcceptChanges();
                command.commit();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCashBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentQROnline(string amtPrice, string pcd, string flag)
        {
            command.newTransaction();
            try
            {
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";
                if (command.saveTempPay(ProgramConfig.saleRefNo, "P", pcd, amtPrice, "0", "0", "0"))
                {
                    double maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                    int maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", pcd, maxRecQntInt.ToString(), amtPrice, "0", ProgramConfig.userId, "0"
                    , "", "0", "0", "0", flag, "0", "0", "0");
                    if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, "P", pcd, maxRecQntInt.ToString(), amtPrice, "0", ProgramConfig.userId, "0"
                    , "", "0", "0", "0", flag, "0", "0", "0"))
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                    }
                }
                else
                {
                    _dtSaleMain.RejectChanges();
                    command.rollback();
                    string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                    throw new Exception(messageTEMPPAY);
                }

                _dtSaleMain.AcceptChanges();
                command.commit();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCashBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        private string SumDupPaymentAmount(string pcd, string amtPrice, string oldAmtPrince)
        {
            if (pcd.StartsWith("QR"))
            {
                //DataTable dt = selectDataToDeleteTempDLYByPCD(pcd);
                //double oldDouPrice = 0;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    oldDouPrice += Convert.ToDouble(dr["AMT"]);
                //}

                double amtDouPrice = 0;
                double oldDouPrice = 0;

                double.TryParse(amtPrice, out amtDouPrice);
                double.TryParse(oldAmtPrince, out oldDouPrice);

                amtPrice = (amtDouPrice + oldDouPrice).ToString();
            }

            ProgramConfig.paymentDupType = "";
            ProgramConfig.paymentDupAmt = "";

            return amtPrice;
        }

        public StoreResult saveDeleteCouponFromList(string couponNo, int qty, int vRow)
        {
            command.newTransaction();
            try
            {
                string sty, vty, qnt, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;
                int maxRec;

                //Delete P
                DataTable dt = command.selectDataToDeleteTempDLYByPCD("CPN1");
                if (dt.Rows.Count != 0)
                {
                    if (!command.deletePaymentTypeForCoupon(dt.Rows[0]["REC"].ToString(), "CPN1"))
                    {
                        string messageDCTEMPPAY = ProgramConfig.message.get("SaleProcess", "CanNotDeleteCouponTEMPPAY").message;
                        throw new Exception(messageDCTEMPPAY);
                        //throw new Exception("ไม่สามารถลบข้อมูลคูปองใน TEMPPAY");
                    }
                }

                //Delete CouponUse
                StoreResult resDelete = delCoupon(couponNo, qty, vRow);
                if (resDelete.response.next)
                {
                    string recNo = "";
                    DataTable resSelectDelete = command.selectDataToDeleteByVTYTempDLY("D");
                    if (resSelectDelete.Rows.Count != 0)
                    {
                        for (int i = 0; i < resSelectDelete.Rows.Count; i++)
                        {
                            recNo = resSelectDelete.Rows[i]["REC"].ToString();
                            if (!command.deleteTempDlyTransByRefRec(recNo))
                            {
                                string messageCNDD = ProgramConfig.message.get("SaleProcess", "CanNotDeleteDiscount").message;
                                throw new Exception(messageCNDD);
                                //throw new Exception("ไม่สามารถลบข้อมูลส่วนลดเดิม");
                            }
                        }
                    }

                    StoreResult resDiscount = command.getDiscountItem(ProgramConfig.saleRefNo);
                    if (resDiscount.response.next)
                    {
                        if (resDiscount.otherData != null && resDiscount.otherData.Rows != null && resDiscount.otherData.Rows.Count > 0)
                        {
                            for (int i = 0; i < resDiscount.otherData.Rows.Count; i++)
                            {
                                string discountCode = resDiscount.otherData.Rows[i]["Discount_Code"].ToString();
                                string discountDesc = resDiscount.otherData.Rows[i]["Discount_Desc"].ToString();
                                string saleAmt = resDiscount.otherData.Rows[i]["Sale_Amt"].ToString();
                                string disAmt = resDiscount.otherData.Rows[i]["Discount_Amt"].ToString();

                                if (double.Parse(disAmt) != 0)
                                {
                                    DataTable dtDisId = command.selectDiscountCode(discountCode);
                                    if (dtDisId.Rows.Count != 0)
                                    {
                                        for (int j = 0; j < dtDisId.Rows.Count; j++)
                                        {
                                            sty = "0";
                                            vty = "D";
                                            qnt = saleAmt;
                                            egp = "0";
                                            stt = "";
                                            pdisc = "0";
                                            //discid = "0";
                                            upc = "";
                                            dty = "D";
                                            discamt = "0";
                                            reason = "0";
                                            stv = "0";
                                            discid = dtDisId.Rows[j]["DiscountId"].ToString();

                                            maxRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;
                                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRec.ToString(), sty, vty, discountCode, qnt
                                            , disAmt, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                                            if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec.ToString(), sty, vty, discountCode, qnt
                                            , disAmt, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                                            {
                                                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                                throw new Exception(messageTEMP);
                                                // new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        _dtSaleMain.AcceptChanges();
                        command.commit();
                        return new StoreResult(ResponseCode.Success);
                    }
                    else
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        return resDiscount;
                    }
                }
                else
                {
                    _dtSaleMain.RejectChanges();
                    command.rollback();
                    return resDelete;
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveDeleteCouponFromList");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentCouponSelect()
        {
            command.newTransaction();
            try
            {
                string rec = "";
                string code = "";
                double maxRecQntInt;
                int maxRecInt;

                DataTable dt = command.selectDataToDeleteTempDLYByPCD("CPN1");
                if (dt.Rows.Count != 0)
                {
                    rec = dt.Rows[0]["REC"].ToString();
                    removeDtSaleMain(rec);

                    if (!command.deletePaymentTypeForCoupon(rec, "CPN1"))
                    {
                        string messageDCTEMPPAY = ProgramConfig.message.get("SaleProcess", "CanNotDeleteCouponTEMPPAY").message;
                        throw new Exception(messageDCTEMPPAY);
                        //throw new Exception("ไม่สามารถลบข้อมูลคูปองใน TEMPPAY");
                    }
                }

                //คำนวนส่วนลดคูปอง exec pos_CouponUse
                // มี Error อยู่
                //StoreResult res = command.couponUse(ProgramConfig.memberId);
                //if (res.response.next)
                //{
                    string sty, vty, qnt, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;

                    string recNo = "";
                    DataTable resSelectDelete = command.selectDataToDeleteByVTYTempDLY("D");
                    if (resSelectDelete.Rows.Count != 0)
                    {
                        for (int i = 0; i < resSelectDelete.Rows.Count; i++)
                        {
                            recNo = resSelectDelete.Rows[i]["REC"].ToString();
                            removeDtSaleMain(recNo);
                            if (!command.deleteTempDlyTransByRefRec(recNo))
                            {
                                string messageCNDD = ProgramConfig.message.get("SaleProcess", "CanNotDeleteDiscount").message;
                                throw new Exception(messageCNDD);
                                //throw new Exception("ไม่สามารถลบข้อมูลส่วนลดเดิม");
                            }
                        }
                    }

                    //StoreResult resDiscount = command.getDiscountItem(ProgramConfig.saleRefNo);
                    //if (resDiscount.response.next)
                    //{
                    //    if (resDiscount.otherData != null && resDiscount.otherData.Rows != null && resDiscount.otherData.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < resDiscount.otherData.Rows.Count; i++)
                    //        {
                    //            string discountCode = resDiscount.otherData.Rows[i]["Discount_Code"].ToString();
                    //            string discountDesc = resDiscount.otherData.Rows[i]["Discount_Desc"].ToString();
                    //            string saleAmt = resDiscount.otherData.Rows[i]["Sale_Amt"].ToString();
                    //            string disAmt = resDiscount.otherData.Rows[i]["Discount_Amt"].ToString();

                    //            if (double.Parse(disAmt) != 0)
                    //            {
                    //                DataTable dtDisId = command.selectDiscountCode(discountCode);
                    //                if (dtDisId.Rows.Count != 0)
                    //                {
                    //                    for (int j = 0; j < dtDisId.Rows.Count; j++)
                    //                    {
                    //                        sty = "0";
                    //                        vty = "D";
                    //                        qnt = saleAmt;
                    //                        egp = "0";
                    //                        stt = "";
                    //                        pdisc = "0";
                    //                        //discid = "0";
                    //                        upc = "";
                    //                        dty = "1";
                    //                        discamt = "0";
                    //                        reason = "0";
                    //                        stv = "0";
                    //                        discid = dtDisId.Rows[j]["DiscountId"].ToString();

                    //                        maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                    //                        insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                    //                        , disAmt, disAmt, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                    //                        if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                    //                        , disAmt, disAmt, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                    //                        {
                    //                            string messageDTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    //                            throw new Exception(messageDTEMP);
                    //                           //throw new Exception("ไม่สามารถบันทึกส่วนลดลง TEMPDLYPTRANS");
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    _dtSaleMain.RejectChanges();
                    //    command.rollback();
                    //    return new PageResult(false, resDiscount);
                    //}

                    StoreResult resDiscount = command.getDiscountItem(ProgramConfig.saleRefNo);
                    if (resDiscount.response.next)
                    {
                        if (resDiscount.otherData != null && resDiscount.otherData.Rows != null && resDiscount.otherData.Rows.Count > 0)
                        {
                            for (int i = 0; i < resDiscount.otherData.Rows.Count; i++)
                            {
                                string discountCode = resDiscount.otherData.Rows[i]["Discount_Code"].ToString();
                                string discountDesc = resDiscount.otherData.Rows[i]["Discount_Desc"].ToString();
                                string saleAmt = resDiscount.otherData.Rows[i]["Sale_Amt"].ToString();
                                string disAmt = resDiscount.otherData.Rows[i]["Discount_Amt"].ToString();

                                if (double.Parse(disAmt) != 0)
                                {
                                    DataTable dtDisId = command.selectDiscountCode(discountCode);
                                    if (dtDisId.Rows.Count != 0)
                                    {
                                        for (int j = 0; j < dtDisId.Rows.Count; j++)
                                        {
                                            DataTable dtSumDisc = command.selectDISCSUMMARYByDiscCode(ProgramConfig.saleRefNo, discountCode);
                                            sty = "0";
                                            vty = "D";
                                            qnt = saleAmt;
                                            egp = "0";
                                            stt = "";
                                            pdisc = "0";
                                            //discid = "0";
                                            upc = "";
                                            dty = "D";
                                            discamt = "0";
                                            reason = "0";
                                            stv = "0";
                                            discid = dtDisId.Rows[j]["DiscountId"].ToString();
                                            string fds = dtSumDisc.Rows[j]["PDISC"].ToString();

                                            maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                                            , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                                            if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                                            , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                                            {
                                                string messageDTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                                throw new Exception(messageDTEMP);
                                                //throw new Exception("ไม่สามารถบันทึกส่วนลดลง TEMPDLYPTRANS");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        return resDiscount;
                    }

                    DataTable dataDissummary = command.selectDISCSUMMARY(ProgramConfig.saleRefNo, new List<string>() { "P" });
                    if (dataDissummary != null && dataDissummary.Rows != null && dataDissummary.Rows.Count > 0)
                    {
                        sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";
                        vty = "P";
                        qnt = "1";
                        egp = "0";
                        stt = "";
                        pdisc = "0";
                        discid = "0";
                        upc = "";
                        dty = "F";
                        discamt = "0";
                        reason = "0";
                        stv = "0";

                        for (int p = 0; p < dataDissummary.Rows.Count; p++)
                        {
                            string dis = dataDissummary.Rows[p]["DISCAMT"].ToString();
                            code = dataDissummary.Rows[p]["DISCCODE"].ToString();

                            maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                            maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, code, maxRecQntInt.ToString()
                            , dis, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                            if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, code, maxRecQntInt.ToString()
                            , dis, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                            {
                                if (!command.saveTempPay(ProgramConfig.saleRefNo, "P", code, dis, "0.00", "0.00", "0.00"))
                                {
                                    string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                    throw new Exception(messageTEMPPAY);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                                }
                            }
                            else
                            {
                                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(messageTEMP);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                            }
                        }
                    }
                    _dtSaleMain.AcceptChanges();
                    command.commit();
                    return new StoreResult(ResponseCode.Success, "Success");
               // }
                //_dtSaleMain.RejectChanges();
                //command.rollback();
                //return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCouponSelect");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePaymentCoupon()
        {
            try
            {
                //Delete P
                DataTable dt = command.selectDataToDeleteTempDLYByPCD("CPN1");
                if (dt.Rows.Count != 0)
                {
                    string rec = dt.Rows[0]["REC"].ToString();
                    removeDtSaleMain(rec);
                    if (!command.deletePaymentTypeForCoupon(dt.Rows[0]["REC"].ToString(), "CPN1"))
                    {
                        string messageDCTEMPPAY = ProgramConfig.message.get("SaleProcess", "CanNotDeleteCouponTEMPPAY").message;
                        throw new Exception(messageDCTEMPPAY);
                        //throw new Exception("ไม่สามารถลบข้อมูลคูปองใน TEMPPAY");
                    }
                }

                string sty, vty, qnt, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;
                string code = "";
                double maxRecQntInt;
                int maxRecInt;

                //คำนวนส่วนลดคูปอง exec pos_CouponUse
                // มี Error อยู่
                //StoreResult res = command.couponUse(ProgramConfig.memberId);
                //if (res.response.next)
                //{
                    string recNo = "";
                    DataTable resSelectDelete = command.selectDataToDeleteByVTYTempDLY("D");
                    if (resSelectDelete.Rows.Count != 0)
                    {
                        for (int i = 0; i < resSelectDelete.Rows.Count; i++)
                        {
                            recNo = resSelectDelete.Rows[i]["REC"].ToString();
                            removeDtSaleMain(recNo);
                            if (!command.deleteTempDlyTransByRefRec(recNo))
                            {
                                string messageCNDD = ProgramConfig.message.get("SaleProcess", "CanNotDeleteDiscount").message;
                                throw new Exception(messageCNDD);
                                //throw new Exception("ไม่สามารถลบข้อมูลส่วนลดเดิม");
                            }
                        }
                    }

                    StoreResult resDiscount = command.getDiscountItem(ProgramConfig.saleRefNo);
                    if (resDiscount.response.next)
                    {
                        if (resDiscount.otherData != null && resDiscount.otherData.Rows != null && resDiscount.otherData.Rows.Count > 0)
                        {
                            for (int i = 0; i < resDiscount.otherData.Rows.Count; i++)
                            {
                                string discountCode = resDiscount.otherData.Rows[i]["Discount_Code"].ToString();
                                string discountDesc = resDiscount.otherData.Rows[i]["Discount_Desc"].ToString();
                                string saleAmt = resDiscount.otherData.Rows[i]["Sale_Amt"].ToString();
                                string disAmt = resDiscount.otherData.Rows[i]["Discount_Amt"].ToString();

                                if (double.Parse(disAmt) != 0)
                                {
                                    DataTable dtDisId = command.selectDiscountCode(discountCode);
                                    if (dtDisId.Rows.Count != 0)
                                    {
                                        for (int j = 0; j < dtDisId.Rows.Count; j++)
                                        {
                                            DataTable dtSumDisc = command.selectDISCSUMMARYByDiscCode(ProgramConfig.saleRefNo, discountCode);
                                            sty = "0";
                                            vty = "D";
                                            qnt = saleAmt;
                                            egp = "0";
                                            stt = "";
                                            pdisc = "0";
                                            //discid = "0";
                                            upc = "";
                                            dty = "D";
                                            discamt = "0";
                                            reason = "0";
                                            stv = "0";
                                            discid = dtDisId.Rows[j]["DiscountId"].ToString();
                                            string fds = dtSumDisc.Rows[j]["PDISC"].ToString();

                                            maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                                            , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                                            if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                                            , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                                            {
                                                string messageDTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                                throw new Exception(messageDTEMP);
                                                //throw new Exception("ไม่สามารถบันทึกส่วนลดลง TEMPDLYPTRANS");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        return resDiscount;
                    }

                    DataTable dataDissummary = command.selectDISCSUMMARY(ProgramConfig.saleRefNo, new List<string>() { "P" });
                    if (dataDissummary != null && dataDissummary.Rows != null && dataDissummary.Rows.Count > 0)
                    {
                        sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";
                        vty = "P";
                        qnt = "1";
                        egp = "0";
                        stt = "";
                        pdisc = "0";
                        discid = "0";
                        upc = "";
                        dty = "F";
                        discamt = "0";
                        reason = "0";
                        stv = "0";

                        for (int p = 0; p < dataDissummary.Rows.Count; p++)
                        {
                            code = dataDissummary.Rows[p]["DISCCODE"].ToString();
                            string dis = dataDissummary.Rows[p]["DISCAMT"].ToString();

                            maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                            maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, code, maxRecQntInt.ToString()
                            , dis, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                            if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, code, maxRecQntInt.ToString()
                            , dis, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                            {
                                if (!command.saveTempPay(ProgramConfig.saleRefNo, "P", code, dis, "0.00", "0.00", "0.00"))
                                {
                                    string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                                    throw new Exception(messageTEMPPAY);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                                }
                            }
                            else
                            {
                                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(messageTEMP);
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                            }
                        }
                    }
                    
                    _dtSaleMain.AcceptChanges();
                    command.commit();
                    return new StoreResult(ResponseCode.Success, "Success");
                //}
                //_dtSaleMain.RejectChanges();
                //command.rollback();
                //return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCoupon");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult saveConfirmPayment(string openTime, double posChg, double cashierChg, AlertMessage AlertMessage = null)
        {
            try
            {
                bool hasDepo = false;
                int maxRec;
                int recV;
                int recF;
                int recC;

                maxRec = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                if (command.selectDataToDeleteByVTYTempDLY("V").Rows.Count != 0)
                {
                    recV = Convert.ToInt32(command.selectDataToDeleteByVTYTempDLY("V").Rows[0]["REC"].ToString());
                }
                else
                {
                    recV = 0;
                }

                if (command.selectDataToDeleteByVTYTempDLY("F").Rows.Count != 0)
                {
                    recF = Convert.ToInt32(command.selectDataToDeleteByVTYTempDLY("F").Rows[0]["REC"].ToString());
                }
                else
                {
                    recF = 0;
                }

                if (command.selectDataToDeleteByVTYTempDLY("C").Rows.Count != 0)
                {
                    recC = Convert.ToInt32(command.selectDataToDeleteByVTYTempDLY("C").Rows[0]["REC"].ToString());
                }
                else
                {
                    recC = 0;
                }

                removeDtSaleMain(recV.ToString());
                if (command.deleteTempDlyTransByRefRec(recV.ToString()))
                {
                    removeDtSaleMain(recF.ToString());
                    if (command.deleteTempDlyTransByRefRec(recF.ToString()))
                    {
                        removeDtSaleMain(recC.ToString());
                        command.deleteTempDlyTransByRefRec(recC.ToString());
                    }
                }
                _dtSaleMain.AcceptChanges();

                var dtTempDepo = command.selectTempDlyptransForTypeP_FromPMCODE("DEPO");
                if (dtTempDepo.Rows.Count > 0)
                {
                    hasDepo = true;
                }

                List<DataTable> lstPrintDepo = new List<DataTable>();
                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";

                bool isCreditSaleOrPOD = ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale || ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD;
                string authDepo = "";
                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
	            {
                    if (ProgramConfig.superUserId != "")
                    {
                        authDepo = ProgramConfig.superUserId;
                    }
                    else
                    {
                        authDepo = ProgramConfig.userId;
                    }
                }

                command.newTransaction();

                insertDtSaleMain(ProgramConfig.saleRefNo, maxRec.ToString(), sty, "V", "Vat Value", "0", "0", ProgramConfig.vatRate, ProgramConfig.userId, ""
                , "", "", "0", "0", "1", "0", "0", "0");
                if (isCreditSaleOrPOD || command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec.ToString(), sty, "V", "Vat Value", "0", "0", ProgramConfig.vatRate, ProgramConfig.userId, ""
                , "", "", "0", "0", "1", "0", "0", "0"))
                {
                    maxRec++;
                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRec.ToString(), sty, "F", "Final Value", ProgramConfig.qntValue, ProgramConfig.amtValue
                        , ProgramConfig.disValue, ProgramConfig.userId, "", "", "", "", (ProgramConfig.IsStandAloneMode ? "1" : "") , "1", "", "", "");
                    if (isCreditSaleOrPOD || command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRec.ToString(), sty, "F", "Final Value", ProgramConfig.qntValue, ProgramConfig.amtValue
                        , ProgramConfig.disValue, ProgramConfig.userId, "", "", "999999", "", (ProgramConfig.IsStandAloneMode ? "1" : ""), "1", "", "", ""))
                    {
                        if (authDepo != "")
                        {
                            //TO DO Insert TempDLYPTRANS_AUTHORIZE
                            if (!command.saveTempdly_Authorize(ProgramConfig.saleRefNo, maxRec.ToString(), sty, "F", "Final Value", ProgramConfig.qntValue, ProgramConfig.amtValue
                        , ProgramConfig.disValue, ProgramConfig.userId, "", "", authDepo, "", (ProgramConfig.IsStandAloneMode ? "1" : ""), "1", "", "", ""))
                            {
                                _dtSaleMain.RejectChanges();
                                command.rollback();
                                //TO DO Chnage language
                                return new StoreResult(ResponseCode.Error, "ไม่สามารถบันทึกข้อมูลลง TEMPDLY_AUTHORIZE");
                                //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                            }
                        }

                        if (ProgramConfig.memberId != null && ProgramConfig.memberId != "" && ProgramConfig.memberId != "N/A")
                        {
                            bool isMMFormat = ProgramConfig.memberFormat == MemberFormat.MegaMaket;
                            string memberID = isMMFormat ? ProgramConfig.memberCardNo : ProgramConfig.memberId;
                            string pdisc = isMMFormat ? ProgramConfig.memberProfileMMFormat.CreditCustomerNo : ""; // CreditCustomerNo
                            string discID = isMMFormat ? ProgramConfig.memberProfileMMFormat.CustomerCategory : ""; // CustomerCategory
                            string discAmt = isMMFormat ? ProgramConfig.memberProfileMMFormat.Customer_No : "0"; // Customer_No

                            if (!isCreditSaleOrPOD)
                            {
                                string subMemberId = memberID.Substring(0, 2);
                                insertDtSaleMain(ProgramConfig.saleRefNo, "0", sty, "C", memberID, ProgramConfig.qntValue
                                    , ProgramConfig.amtValue, ProgramConfig.disValue, ProgramConfig.userId, subMemberId
                                    , "", pdisc, discID, "", "", discAmt, "0", "0");
                                if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, "0", sty, "C", memberID, ProgramConfig.qntValue
                                    , ProgramConfig.amtValue, ProgramConfig.disValue, ProgramConfig.userId, subMemberId
                                    , "", pdisc, discID, "", "", discAmt, "0", "0"))
                                {
                                    _dtSaleMain.RejectChanges();
                                    command.rollback();
                                    return new StoreResult(ResponseCode.Error, ProgramConfig.message.get("SaleProcess", "SaveMTEMPDLYPTRANSIncomplete").message);
                                    //throw new Exception("ไม่สามารถบันทึกข้อมูลสมาชิกใน TEMPDLYPTRANS");
                                }
                            }
                        }

                        StoreResult res = null;

                        if (ProgramConfig.hasDrawer)
                        {
                            res = command.updateOpenCashDrawer(openTime, ProgramConfig.saleRefNo, "0");
                        }

                        Profile check = ProgramConfig.getProfile(FunctionID.Sale_Change_EditChange); //#141
                        if (check.policy == PolicyStatus.Work)
                        {
                            //if (!(cashierChg == 0 && posChg >= 0))
                            //{
                            if(cashierChg > 0)
                            {
                                if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                                {
                                    res = command.PODSaveChangeTrans(posChg, cashierChg);
                                    if (!res.response.next)
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        return res;
                                    }
                                }
                                else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale) 
                                {
                                    res = command.CREDPaySaveChangeTrans(posChg, cashierChg);
                                    if (!res.response.next)
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        return res;
                                    }
                                }
                                else
                                {
                                    res = command.SaveChangeTrans(posChg, cashierChg);
                                    if (!res.response.next)
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        return res;
                                    }
                                }
                            }
                        }

                        DataTable dt1 = new DataTable();
                        DataTable dtCN = new DataTable();
                        string fftino = "";
                        string offtino = "";
                        string refNo = "";
                        string abbNo = "";
                        bool flagPrint = true;

                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                        {
                            res = command.saveCREDPaySaveSale();
                            if (!res.response.next)
                            {
                                _dtSaleMain.RejectChanges();
                                command.rollback();
                                return res;
                            }
                            command.commit();


                            DataTable dtCred = command.selectCREDPAY_TRANS(_ObjPayInvoiceAR.ReceiptNo);
                            DataTable dtCredDetial = command.selectCREDPAY_TRANS_DETAIL(_ObjPayInvoiceAR.ReceiptNo);
                            DataTable dtCredPay = command.selectCREDPAY_TRANS_PAY(_ObjPayInvoiceAR.ReceiptNo);


                            string rescode2 = "";
                            string resmsg_en2 = "";
                            string resmsg_th2 = "";
                            string paymentID2 = "";
                            string invoiceNo2 = "";
                            string transID2 = _ObjPayInvoiceAR.TransID;
                            double paymentAmount2 = Convert.ToDouble(dtCred.Rows[0]["AMOUNT"]);
                            string paydate2 = DateTime.Parse(dtCred.Rows[0]["TRANSACTION_DATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            string storeCode2 = dtCred.Rows[0]["STORE_CODE"].ToString();
                            string creditby2 = dtCred.Rows[0]["CASHIER_ID"].ToString();
                            string recepitNo2 = dtCred.Rows[0]["Ref_CredPay"].ToString();

                            RequestPayInvoiceAR dataLog = new RequestPayInvoiceAR();
                            dataLog.Trans_ID = transID2;
                            dataLog.Payment_Amount = paymentAmount2;
                            dataLog.Payment_Date = paydate2;
                            dataLog.Pay_Store_Code = storeCode2;
                            dataLog.Created_By = creditby2;
                            dataLog.Receipt_No = recepitNo2;

                            int cnt = 0;

                            dataLog.Invoice_list = new List<Invoice_List>();
                            clsMMFSAPI.invoice_list[] invList2 = new clsMMFSAPI.invoice_list[dtCredDetial.Rows.Count];
                            foreach (DataRow dr in dtCredDetial.Rows)
                            {
                                Invoice_List inv = new Invoice_List();
                                inv.Invoice_No = invList2[cnt].invoice_no = dr["CRED_INVOICE_NO"].ToString();
                                inv.Apply_Amount = invList2[cnt].invoice_amount = Convert.ToDouble(dr["CRED_AMOUNT"]);
                                cnt++;

                                dataLog.Invoice_list.Add(inv);
                            }

                            dataLog.Payment_list = new List<Payment_List>();
                            clsMMFSAPI.payment_list[] pmList2 = new clsMMFSAPI.payment_list[dtCredPay.Rows.Count];
                            cnt = 0;
                            foreach (DataRow dr in dtCredPay.Rows)
                            {
                                Payment_List paylst = new Payment_List();
                                paylst.SEQ = pmList2[cnt].seq_no = (int)dr["SEQ"];
                                paylst.Payment_Method = pmList2[cnt].payment_method = dr["PaymentMainCode"].ToString().Trim();
                                paylst.Apply_Amount = pmList2[cnt].apply_amount = Convert.ToDouble(dr["PAYMENT_AMOUNT"]) - Convert.ToDouble(dr["PAYMENT_CHANGE"]);
                                paylst.Payment_No = pmList2[cnt].payment_no = dr["PAYMENT_NUMBER"].ToString().Trim();
                                cnt++;

                                dataLog.Payment_list.Add(paylst);
                            }

                            clsMMFSAPI sv2 = new clsMMFSAPI();

                            int defaultDelay = 3;
                            int defaultRetry = 5;
                            res = command.selectAPICONF_RETRY("payInvoiceAR");
                            if (res.response.next)
                            {
                                defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                                defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
                            }


                            command.saveAPILOG(dataLog, ProgramConfig.creditSaleNo, "payInvoiceAR", "OUT");

                            int cnt3 = 0;
                        Retry:
                            sv2.payInvoiceAR(ref rescode2, ref  resmsg_en2, ref  resmsg_th2, ref  transID2, ref paymentAmount2, ref  paydate2, ref  storeCode2, ref  creditby2
                                , ref  recepitNo2, ref  pmList2, ref  invList2, ref  paymentID2);

                            ResponsePayInvoiceAR dataLogRes = new ResponsePayInvoiceAR();
                            dataLogRes.Result_Code = rescode2;
                            dataLogRes.Result_Msg_EN = resmsg_en2;
                            dataLogRes.Result_Msg_TH = resmsg_th2;
                            dataLogRes.Api_Type = "";
                            dataLogRes.Payment_ID = paymentID2;
                            dataLogRes.Trans_ID = transID2;
                            dataLogRes.Payment_Amount = paymentAmount2;
                            dataLogRes.Payment_Date = paydate2;
                            dataLogRes.Pay_Store_Code = storeCode2;
                            dataLogRes.Created_By = creditby2;
                            dataLogRes.Invoice_No = invoiceNo2;

                            command.saveAPILOG(dataLogRes, ProgramConfig.creditSaleNo, "payInvoiceAR", "IN");

                            if (rescode2 == "0000")
                            {
                                command.updateCreditPayTrans(_ObjPayInvoiceAR.ReceiptNo, false, transID: transID2, paymentID: paymentID2);
                                DataTable dt = command.CREDPayPrintReceipt().otherData;
                                Hardware.printTermal(dt);
                            }
                            else if (rescode2 == "9999" && cnt3 <= defaultRetry)
                            {
                                cnt3++;
                                Thread.Sleep(defaultDelay);
                                goto Retry;
                            }
                            else
                            {
                                AlertMessage(ResponseCode.Error, resmsg_th2);
                                res = command.clearUp(FunctionID.CreditSale_APIAR, "");
                                if (!res.response.next)
                                {
                                    AlertMessage(res.response, res.responseMessage);
                                }
                                return new StoreResult(ResponseCode.Ignore, res.responseMessage);
                            }


                            #region API P'Chai
                            //    res = command.saveCREDPaySaveSale();
                            //    if (!res.response.next)
                            //    {
                            //        _dtSaleMain.RejectChanges();
                            //        command.rollback();
                            //        return res;
                            //    }
                            //    command.commit();

                            //    string rescode = "";
                            //    string resmsg_en = "";
                            //    string resmsg_th = "";
                            //    string paymentID = "";
                            //    string invoiceNo = "";
                            //    string transID = _ObjPayInvoiceAR.TransID;
                            //    double paymentAmount = 0;
                            //    string paydate = "";
                            //    string storeCode = "";
                            //    string creditby = "";
                            //    string recepitNo = "";
                            //    clsMMFSAPI.payment_list[] lstPay = _ObjPayInvoiceAR.ListPaymnet;
                            //    clsMMFSAPI.invoice_list[] lstInvoice = _ObjPayInvoiceAR.ListInvoice;
                            //    CcosServices service = new CcosServices();

                            //    //clsMMFSAPI sv = new clsMMFSAPI();
                            //    //sv.payInvoiceAR(ref rescode, ref  resmsg_en, ref  resmsg_th, ref  transID, ref  paymentAmount, ref  paydate, ref  storeCode, ref  creditby, ref  recepitNo, ref  lstPay, ref  lstInvoice, ref  paymentID);

                            //    int defaultDelay = 3;
                            //    int defaultRetry = 5;
                            //    res = selectAPICONF_RETRY("payInvoiceAR");
                            //    if (res.response.next)
                            //    {
                            //        defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                            //        defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
                            //    }

                            //    int cnt = 0;

                            //Retry:
                            //    ResponsePayInvoiceAR resp = service.PayInvoiceAR(_ObjPayInvoiceAR.ReceiptNo, _ObjPayInvoiceAR.TransID);
                            //    if (resp.result_code == "0000")
                            //    {
                            //        command.updateCreditPayTrans(_ObjPayInvoiceAR.ReceiptNo, false, transID: resp.trans_id, paymentID: resp.payment_id);
                            //        DataTable dt = command.CREDPayPrintReceipt().otherData;
                            //        Hardware.printTermal(dt);
                            //    }
                            //    else if (resp.result_code == "9999" && cnt <= defaultRetry)
                            //    {
                            //        cnt++;
                            //        Thread.Sleep(defaultDelay);
                            //        goto Retry;
                            //    }
                            //    else
                            //    {
                            //        _dtSaleMain.RejectChanges();
                            //        command.rollback();
                            //        return new StoreResult(ResponseCode.Error, resp.result_msg_th);
                            //    }
                            #endregion
                        }
                        else if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.ReceivePOD)
                        {
                            #region POD
                            res = command.savePODSaveSale(ProgramConfig.podRefFFTI, ProgramConfig.totalValue, ProgramConfig.podQRCode, ProgramConfig.podRefID, openTime);
                            if (!res.response.next)
                            {
                                _dtSaleMain.RejectChanges();
                                command.rollback();
                                return res;
                            }

                            //TO DO Recheck ลูกค้าต้องการใบเสร็จหรือไม่ Policy #143

                            string flagCallAPI = res.otherData.Rows[0]["Flag_CallAPI"].ToString();
                            if (flagCallAPI == "Y")
                            {
                                command.commit();
                                res = CallAPIProcess();
                                if (!res.response.next)
                                {
                                    AlertMessage(res.response, res.responseMessage);
                                    res = command.clearUp(FunctionID.ReceivePOD_RollBack, "");
                                    if (!res.response.next)
                                    {
                                        AlertMessage(res.response, res.responseMessage);
                                    }

                                    return new StoreResult(ResponseCode.Ignore, res.responseMessage);
                                }
                            }

                            res = command.podPrintReceipt();
                            if (!res.response.next)
                            {
                                _dtSaleMain.RejectChanges();
                                command.rollback();
                                return res;
                            }

                            dt1 = res.otherData;

                            //#698
                            check = ProgramConfig.getProfile(FunctionID.ReceivePOD_PrintReceipt);
                            if (check.policy != PolicyStatus.Work)
                            {
                                flagPrint = false;
                            }

                            #endregion
                        }
                        else
                        {
                            #region Normal Sale
                            res = command.saveSaleTransaction(ProgramConfig.memberId);
                            if (res.response == ResponseCode.Success)
                            {

                                DataTable dt = res.otherData;
                                if (dt != null)
                                {
                                    abbNo = dt.Rows[0]["ABBNo"].ToString();
                                    if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                                    {
                                        ProgramConfig.tempFFTINo = abbNo;
                                    }
                                    else
                                    {
                                        ProgramConfig.abbNo = abbNo;
                                    }
                                    AppLog.writeLog("ABBNo from SaveSaleTrans " + abbNo);
                                }


                                res = command.saveSaleTransactionUpdateABBNO("SALE");
                                if (res.response.next)
                                {
                                    if (ProgramConfig.loadHoldOrderReceipt != null && ProgramConfig.loadHoldOrderReceipt != "")
                                    {
                                        res = command.updateTmpTrans();
                                        if (!res.response.next)
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            return res;
                                        }
                                    }

                                    //คำนวนโปรโมชั่น
                                    res = command.concludeSale(abbNo);
                                    if (!res.response.next)
                                    {
                                        _dtSaleMain.RejectChanges();
                                        command.rollback();
                                        return res;
                                    }

                                    //TO DO Recheck ลูกค้าต้องการใบเสร็จหรือไม่ Policy #143
                                    if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                                    {
                                        if (hasDepo)
                                        {
                                            foreach (DataRow dr in dtTempDepo.Rows)
                                            {
                                                dtCN = new DataTable();
                                                string refDepoNo = dr["PCD"].ToString().Substring(4, dr["PCD"].ToString().Length - 4);
                                                res = command.VoidDeposit(refDepoNo, abbNo, FunctionID.Sale_VoidDepo);
                                                if (!res.response.next)
                                                {
                                                    _dtSaleMain.RejectChanges();
                                                    command.rollback();
                                                    return res;
                                                }
                                                dtCN = res.otherData;
                                                ProgramConfig.cnNo = dtCN.Rows[0]["CNNUM_INI"].ToString();
                                                ProgramConfig.running.updateValue();
                                                lstPrintDepo.Add(dtCN);
                                            }

                                        }

                                        //Call RCV2FULLFROM
                                        res = command.rcv2fullform_ffti(abbNo);
                                        if (!res.response.next)
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            return res;
                                        }

                                        refNo = res.otherData.Rows[0]["REF"].ToString();
                                        fftino = res.otherData.Rows[0]["FFTI_NO"].ToString();
                                        offtino = res.otherData.Rows[0]["OFFTI_NO"].ToString();

                                        res = command.validateFFTI(fftino, abbNo);
                                        if (!res.response.next)
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            return res;
                                        }

                                        //FOR DEPOSIT 
                                        //Call pos_SaveCCODDeposit
                                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit)
                                        {
                                            res = command.saveDepositTrans(fftino, abbNo);
                                            if (!res.response.next)
                                            {
                                                _dtSaleMain.RejectChanges();
                                                command.rollback();
                                                return res;
                                            }
                                        }
                                        else
                                        {
                                            if (hasDepo)
                                            {
                                                res = command.SaveDepositPayTrans(fftino); //pos_SaveDepositPayTrans
                                                if (!res.response.next)
                                                {
                                                    _dtSaleMain.RejectChanges();
                                                    command.rollback();
                                                    return res;
                                                }
                                            }
                                        }

                                        command.commit();
                                        _dtSaleMain.AcceptChanges();
                                    }
                                    else
                                    {
                                        StoreResult result = command.printReceipt(ProgramConfig.saleRefNo);
                                        if (!result.response.next)
                                        {
                                            _dtSaleMain.RejectChanges();
                                            command.rollback();
                                            return result;
                                        }
                                        dt1 = result.otherData;
                                    }

                                    //if (!Hardware.printByType(dt1, abbNo, refNo, fftino, offtino))
                                    //{
                                    //    if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                                    //    {
                                    //        //TO DO Update PrintFlag ForFullTax
                                    //        var result = command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "0");
                                    //        return result;
                                    //    }
                                    //}

                                    //var result2 = command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "1");
                                    //if (!result2.response.next)
                                    //{
                                    //    if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                                    //    {
                                    //        result2 = command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "0");
                                    //    }
                                    //    else
                                    //    {
                                    //        _dtSaleMain.RejectChanges();
                                    //        command.rollback();
                                    //    }
                                    //    return result2;
                                    //}

                                    //if (ProgramConfig.printInvoiceType != PrintInvoiceType.FULLTAX)
                                    //{
                                    //    command.commit();
                                    //    BaseProcess.clearDataTable();
                                    //    _dtSaleMain.AcceptChanges();
                                    //}

                                    //return res;
                                }
                                else
                                {
                                    _dtSaleMain.RejectChanges();
                                    command.rollback();
                                    return res;
                                }


                            }
                            else
                            {
                                _dtSaleMain.RejectChanges();
                                command.rollback();
                                return res;
                            }

                            #endregion
                        }

                        #region Print

                        if (ProgramConfig.pageBackFromPayment == PageBackFormPayment.CreditSale)
                        {

                        }
                        else
                        {
                            if (ProgramConfig.memberFormat == MemberFormat.MegaMaket)
                            {
                                foreach (var dt in lstPrintDepo)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        Hardware.printCN(dt.Rows[0]["CNNo"].ToString());
                                    }
                                }
                            }

                            if (flagPrint)
                            {
                                if (!Hardware.printByType(dt1, abbNo, refNo, fftino, offtino))
                                {
                                    //if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                                    //{
                                    //TO DO Update PrintFlag ForFullTax
                                    var result = command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "0");
                                    return result;
                                    //}
                                }

                                var result2 = command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "1");
                                //if (!result2.response.next)
                                //{
                                //    if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                                //    {
                                //        result2 = command.updatePrintTempDlyptrans(ProgramConfig.abbNo, "0");
                                //    }
                                //}
                            }
                        }

                        #endregion

                        if (ProgramConfig.printInvoiceType != PrintInvoiceType.FULLTAX)
                        {
                            command.commit();                            
                            _dtSaleMain.AcceptChanges();
                        }


                        return res;


                    }
                }
                string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                throw new Exception(messageTEMP);
                //throw new Exception("ไม่สามารถบันทึกข้อมูลใน TEMPDLYPTRANS");
            }
            catch (NetworkConnectionException)
            {
                _dtSaleMain.RejectChanges();
                AppLog.writeLog("connection to server lost at SaleProcess.saveConfirmPayment");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                _dtSaleMain.RejectChanges();
                command.rollback();
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult PrintExport(DataTable dt)
        {
            try
            {
                //Profile chkProf = ProgramConfig.getProfile(FunctionID.Sale_Print_ExportPermit);
                //if (chkProf.policy == PolicyStatus.Work)
                //{
                    DataRow[] dr = dt.Select(String.Format(@"REF = '{0}' and STCODE = '{1}' and ISNULL(STT, '') <> 'V' and VTY in ('0','1') 
                             and REC >= 0 and REC < 2000 and PrintExport = 'Y'", ProgramConfig.saleRefNo, ProgramConfig.storeCode));

                    if (dr.Length > 0)
                    {
                        StoreResult res = command.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.ExportPermit);
                        if (res.response.next)
                        {
                            ProgramConfig.expermitRefNo  = res.otherData.Rows[0]["ReferenceNo"].ToString();
                            ProgramConfig.expermitRefNoIni = res.otherData.Rows[0]["ReferenceNoINI"].ToString();
                            string expNo = res.otherData.Rows[0]["ReferenceNo"].ToString();
                            foreach (DataRow item in dr)
                            {
                                command.insertEXPORTPERMIT(expNo, item["REC"].ToString(), Convert.ToInt32(expNo.Substring(expNo.Length - 3, 3).ToString()).ToString());
                            }

                            Profile profile = ProgramConfig.getProfile(FunctionID.Sale_Print_ExportPermit);
                            if (profile.policy == PolicyStatus.Work)
                            {
                                return command.PrintExportPermit();
                            }
                            else
                            {
                                return new StoreResult(ResponseCode.Success, "Success");
                                //return new StoreResult(ResponseCode.Error, "Policy Not Print");
                            }                     
                        }
                        else
                        {
                            return new StoreResult(ResponseCode.Error, "Cannot getRunning EXPORTPERMIT");
                        }
                    }
                    else
                    {
                        return new StoreResult(ResponseCode.Success, "Success");
                    }
                //}
                //else
                //{
                //    return new StoreResult(ResponseCode.Error, "Policy Not Print");
                //}
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.PrintExport");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public ProcessResult cashireMessageStatus()
        {
            try
            {
                StoreResult res = command.getMessageCashierStatus(FunctionID.Sale_GetMessageCashier);
                string sil = res.otherData.Rows[0]["silent"].ToString();
                if (sil.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return new ProcessResult(res, true);
                }
                else
                {
                    return new ProcessResult(res, false);
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.cashireMessageStatus");
                throw;
            }
            catch (Exception ex)
            {
                return new ProcessResult(AppLog.writeLog(ex));
            }
        }

        public StoreResult InsertTempDLYPTransLocalPOS()
        {
            try
            {
                foreach (DataRow row in _dtSaleMain.Rows)
                {
                    if ((row["VTY"] + "").Trim() != "C")
                    {
                        if (!command.saveTempDlyptrans(row["REF"] + "", row["REC"] + "", row["STY"] + "", row["VTY"] + "", row["PCD"] + "", row["QNT"] + "", row["AMT"] + "", row["FDS"] + "",
                            row["USR"] + "", row["EGP"] + "", row["STT"] + "", row["PDISC"] + "", row["DISCID"] + "", row["UPC"] + "", row["DTY"] + "", row["DISCAMT"] + "", row["REASON_ID"] + "", row["STV"] + ""))
                        {
                            _dtSaleMain.RejectChanges();
                            string responseMessage = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                            throw new Exception(responseMessage);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลลงใน TEMPDLYPTRANS");
                        }
                    }
                }

                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckRedeemPoint_Free_CPN(string saleAmt, string rdCode, string qty)
        {
            try
            {
                return command.CheckRedeemPoint_Free_CPN(saleAmt, rdCode, qty);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckRedeemPoint_Free_CPN");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Product(string cntRedeem, string rateUse, string pointUse, string ruleID, string rdCode)
        {
            try
            {
                return command.Update_PMS_REDEEM_POINT_Product(cntRedeem, rateUse, pointUse, ruleID, rdCode);
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckRedeemPointPercentDiscount(string saleAmt, string rdCode, string isRedeem)
        {
            try
            {
                return command.CheckRedeemPointPercentDiscount(saleAmt, rdCode, isRedeem);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckRedeemPointPercentDiscount");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult Delete_Temp_Redeem_Percent_Discount()
        {
            try
            {
                return command.Delete_Temp_Redeem_Percent_Discount();
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Percent_Discount()
        {
            try
            {
                return command.Update_PMS_REDEEM_POINT_Percent_Discount();
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckRedeemPoint(string saleAmt)
        {
            try
            {
                return command.CheckRedeemPoint(saleAmt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckRedeemPoint");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult Clear_PMS_REDEEM_POINT_Cash(string ruleID)
        {
            try
            {
                return command.Clear_PMS_REDEEM_POINT_Cash(ruleID);
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckCustIDCard(string idCard)
        {
            try
            {
                return command.CheckCustIDCard(idCard);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckCustIDCard");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult SaveRedeemIDCard()
        {
            try
            {
                return command.SaveRedeemIDCard();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.SaveRedeemIDCard");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Cash(string cntRedeem, string rateUse, string pointUse, string ruleID, string rdCode, string s_redeem)
        {
            try
            {
                return command.Update_PMS_REDEEM_POINT_Cash(cntRedeem, rateUse, pointUse, ruleID, rdCode, s_redeem);
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult RedeemPoint_sum()
        {
            try
            {
                return command.RedeemPoint_sum();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.RedeemPoint_sum");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckRedeemPoint_Coupon(string saleAmt)
        {
            try
            {
                StoreResult res = command.getRunning(FunctionID.Sale_GetRunningNo, RunningReceiptID.Redeem);
                if (res.otherData != null && res.otherData.Rows.Count > 0)
                {
                    ProgramConfig.redeemRefNo = res.otherData.Rows[0]["ReferenceNo"].ToString();
                    //ProgramConfig.redeemRefNoIni = res.otherData.Rows[0]["ReferenceNoINI"].ToString();
                    return command.CheckRedeemPoint_Coupon(saleAmt);
                }
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckRedeemPoint");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult Update_PMS_REDEEM_POINT_Coupon(string cntRedeem, string pointUse, string ruleID, string rdCode)
        {
            try
            {
                return command.Update_PMS_REDEEM_POINT_Coupon(cntRedeem, pointUse, ruleID, rdCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.Update_PMS_REDEEM_POINT_Coupon");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }


        public StoreResult RedeemPoint_Coupon_Sum()
        {
            try
            {
                return command.RedeemPoint_Coupon_Sum();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.RedeemPoint_sum");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult PrintRedeemPoint_Coupon()
        {
            try
            {
                return command.PrintRedeemPoint_Coupon();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.PrintRedeemPoint_Coupon");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult GetChange(string saleAmt, string payAmt)
        {
            try
            {
                return command.GetChange(saleAmt, payAmt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetChange");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult PaymentDiscount(string pmCode, string cardNo)
        {
            try
            {               
                string sty, vty, qnt, fds, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;

                //int recDel = command.selectMaxRecTempDlyptransForTypeD_FormPMCODE("PAYD");
                //DataTable paymentType = command.selectTypeFromMaxRec(recDel.ToString());

                //if (paymentType.Rows.Count > 0)
                //{
                //    ProgramConfig.paymentType = paymentType.Rows[0]["PCD"].ToString();
                //    ProgramConfig.paymentAmt = paymentType.Rows[0]["AMT"].ToString();
                //}
                //else
                //{
                //    ProgramConfig.paymentType = "";
                //    ProgramConfig.paymentAmt = "";
                //}

                //if (recDel > 0)
                //{
                //    removeDtSaleMain(recDel.ToString());
                //    StoreResult resCHGD = command.deletePaymentType(recDel.ToString());
                //    if (!resCHGD.response.next)
                //    {
                //        _dtSaleMain.RejectChanges();
                //        return new StoreResult(ResponseCode.Error, "Cannot Delete PCD table tempdlyptrans at SaleProcess.PaymentDiscount", "", "");
                //    }
                //    _dtSaleMain.AcceptChanges();
                //}

                //deletePaymentType


                StoreResult res = command.PaymentDiscount(pmCode, cardNo);
                if (res.response.next)
                {
                    if (res.otherData != null && res.otherData.Rows.Count > 0)
                    {
                        string disAmt = res.otherData.Rows[0]["DISC"].ToString();
                        if (Convert.ToDouble(disAmt) > 0)
                        {
                            string saleAmt = res.otherData.Rows[0]["SaleAmt"].ToString();
                            string discountCode = res.otherData.Rows[0]["DiscountCode"].ToString();
                            discid = res.otherData.Rows[0]["DiscountID"].ToString();
                            //DataTable dtSumDisc = command.selectDISCSUMMARYByDiscCode(ProgramConfig.saleRefNo, discountCode);

                            sty = "0";
                            vty = "D";
                            qnt = saleAmt;
                            fds = "0";
                            egp = "0";
                            stt = "";
                            pdisc = "0";
                            //discid = "0";
                            upc = "";
                            dty = "D";
                            discamt = "0";
                            reason = "0";
                            stv = "0";

                            int maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                            insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
            , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                            if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                            , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                            {
                                string messageDTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                                throw new Exception(messageDTEMP);
                                //throw new Exception("ไม่สามารถบันทึกส่วนลดลง TEMPDLYPTRANS");
                            }
                        }
                    }
                }

                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.PaymentDiscount");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveLTYD(DataTable dt)
        {
            try
            {
                string sty, vty, qnt, fds, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;

                string disAmt = dt.Rows[0]["LTYD"].ToString();
                if (Convert.ToDouble(disAmt) > 0)
                {        
                    string saleAmt = dt.Rows[0]["LTYD_SaleAmt"].ToString();
                    string discountCode = dt.Rows[0]["LTYDCODE"].ToString();
                    discid = dt.Rows[0]["LTYD_DISCID"].ToString();
                    //fds = dt.Rows[0]["LTYD_FDS"].ToString();
                    //DataTable dtSumDisc = command.selectDISCSUMMARYByDiscCode(ProgramConfig.saleRefNo, discountCode);

                    sty = "0";
                    vty = "D";
                    qnt = saleAmt;
                    fds = "0";
                    egp = "0";
                    stt = "";
                    pdisc = "0";
                    //discid = "0";
                    upc = "";
                    dty = "D";
                    discamt = "0";
                    reason = "0";
                    stv = "0";

                    int maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
    , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                    if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, discountCode, saleAmt
                    , disAmt, fds, ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                    {
                        _dtSaleMain.RejectChanges();
                        string messageDTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageDTEMP);
                        //throw new Exception("ไม่สามารถบันทึกส่วนลดลง TEMPDLYPTRANS");
                    }
                    _dtSaleMain.AcceptChanges();
                }

                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveLTYD");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updatePCDSymbolTempDlyptrans(string pcd, string rec, string symbol)
        {
            try
            {
                //Recheck qty
                //DataTable selectDeleteItem = selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, pcd, rec);
                //string recDB = selectDeleteItem.Rows[0]["rec"].ToString();
                //string pcdDB = selectDeleteItem.Rows[0]["rec"].ToString();

                DataRow dr = _dtSaleMain.AsEnumerable().Where(w => w["REF"].ToString() == ProgramConfig.saleRefNo && w["REC"].ToString() == rec && w["PCD"].ToString() == pcd).FirstOrDefault();
                if (dr != null)
                {
                    string pcdNew = pcd.PadRight(20, ' ').Substring(0, 19) + symbol;
                    StoreResult res = command.updatePCDTempDlyptrans(ProgramConfig.saleRefNo, pcd, rec, pcdNew);
                    if (res.response.next)
                    {                     
                        dr["PCD"] = pcdNew;
                        _dtSaleMain.AcceptChanges();
                    }
                    return res;
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, "Cannot update PCD table tempdlyptrans at SaleProcess.updatePCDSymbolTempDlyptrans", "", "");
                }
                
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.PaymentDiscount");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updatePrintExportTempDlyptrans(string pcd, string rec, string flag)
        {
            try
            {
                //Recheck qty
                //DataTable selectDeleteItem = selectDeleteItemTempDlyptrans(ProgramConfig.saleRefNo, pcd, rec);
                //string recDB = selectDeleteItem.Rows[0]["rec"].ToString();
                //string pcdDB = selectDeleteItem.Rows[0]["rec"].ToString();

                DataRow dr = _dtSaleMain.AsEnumerable().Where(w => w["REF"].ToString() == ProgramConfig.saleRefNo && w["REC"].ToString() == rec && w["PCD"].ToString() == pcd).FirstOrDefault();

                if (dr != null)
                {
                    dr["PrintExport"] = flag;
                    _dtSaleMain.AcceptChanges();
                    return new StoreResult(ResponseCode.Success, "Success");
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, "Cannot update 'Print Export' table tempdlyptrans at SaleProcess.updatePrintExportTempDlyptrans", "", "");
                }

            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updatePrintExportTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public string getDataDTSaleMain(string pcd, string rec, string col)
        {
            try
            {
                DataRow dr = _dtSaleMain.AsEnumerable().Where(w => w["REF"].ToString() == ProgramConfig.saleRefNo && w["REC"].ToString() == rec && w["PCD"].ToString() == pcd).FirstOrDefault();

                if (dr != null)
                {
                    return dr[col].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updatePrintExportTempDlyptrans");
                throw;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public StoreResult DiscplayDiscountReceipt()
        {
            try
            {
                return command.DisplayDiscountReceipt();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.DiscplayDiscountReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
            
        }

        public StoreResult savePaymentLast(string pmCode, string amt)
        {
            command.newTransaction();
            try
            {
                string rec = "";
                double maxRecQntInt;
                int maxRecInt;

                string sty, vty, qnt, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;

                sty = "0";
                vty = "P";
                qnt = "0";
                egp = "0";
                stt = "";
                pdisc = "0";
                discid = "0";
                upc = "";
                dty = "F";
                discamt = "0";
                reason = "0";
                stv = "0";

                //code = dataDissummary.Rows[p]["DISCCODE"].ToString();
                //dis = dataDissummary.Rows[p]["DISCAMT"].ToString();

                maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, pmCode, maxRecQntInt.ToString()
                , amt, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, pmCode, maxRecQntInt.ToString()
                , amt, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                {
                    if (!command.saveTempPay(ProgramConfig.saleRefNo, "P", pmCode, amt, "0.00", "0.00", "0.00"))
                    {
                        _dtSaleMain.RejectChanges();
                        command.rollback();
                        string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "savePaymentTicketCoupon").message;
                        throw new Exception(messageTEMPPAY);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                    }
                }
                else
                {
                    _dtSaleMain.RejectChanges();
                    command.rollback();
                    string messageTEMP = ProgramConfig.message.get("SaleProcess", "savePaymentTicketCoupon").message;
                    throw new Exception(messageTEMP);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                }

                _dtSaleMain.AcceptChanges();
                command.commit();
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentTicketCoupon");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataTable selectSCANTC()
        {
            try
            {
                return command.selectSCANTC();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectSCANTC");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new DataTable();
            }
        }


        //public string selectORG_TransFromQRPAYTRANS_Manual(string rec)
        //{
        //    try
        //    {
        //        DataTable dt = command.selectORG_TransFromQRPAYTRANS_Manual(rec);
        //        if (dt.Rows.Count > 0)
        //        {
        //            return dt.Rows[0][0].ToString();
        //        }
        //        return "";
        //    }
        //    catch (NetworkConnectionException)
        //    {
        //        AppLog.writeLog("connection to server lost at SaleProcess.selectQRPAYTRANS");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppLog.writeLog(ex);
        //        return "";
        //    }
        //}

        public StoreResult ClearPromotion()
        {
            try
            {
                return command.ClearPromotion();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.ClearPromotion");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult DeleteTempRedeemFreePointCash()
        {
            try
            {
                return command.DeleteTempRedeemFreePointCash();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.DeleteTempRedeemFreePointCash");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }


        public StoreResult UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL()
        {
            try
            {
                return command.UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.UPDATE_PMS_REDEEM_POINT_PRODUCT_CANCEL");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult SaveSmsDiscount()
        {
            command.newTransaction();
            string sty, vty, qnt, egp, stt, pdisc, upc, dty, discamt, reason, stv, discid;
            string code = "";
            double maxRecQntInt;
            int maxRecInt;

            DataTable dataDissummary = command.selectDISCSUMMARY(ProgramConfig.saleRefNo, new List<string>() { "P" });
            if (dataDissummary != null && dataDissummary.Rows != null && dataDissummary.Rows.Count > 0)
            {
                sty = "0";
                vty = "P";
                qnt = "1";
                egp = "0";
                stt = "";
                pdisc = "0";
                discid = "0";
                upc = "";
                dty = "F";
                discamt = "0";
                reason = "0";
                stv = "0";

                for (int p = 0; p < dataDissummary.Rows.Count; p++)
                {
                    string dis = dataDissummary.Rows[p]["DISCAMT"].ToString();
                    code = dataDissummary.Rows[p]["DISCCODE"].ToString();

                    maxRecQntInt = command.selectMaxRecTempDlyptransForTypeP(ProgramConfig.saleRefNo) + 1;
                    maxRecInt = command.selectMaxRecTempDlyptrans(ProgramConfig.saleRefNo) + 1;

                    insertDtSaleMain(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, code, maxRecQntInt.ToString()
                    , dis, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv);
                    if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, maxRecInt.ToString(), sty, vty, code, maxRecQntInt.ToString()
                    , dis, "", ProgramConfig.userId, egp, stt, pdisc, discid, upc, dty, discamt, reason, stv))
                    {
                        if (!command.saveTempPay(ProgramConfig.saleRefNo, "P", code, dis, "0.00", "0.00", "0.00"))
                        {
                            command.rollback();
                            _dtSaleMain.RejectChanges();
                            string messageTEMPPAY = ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                            throw new Exception(messageTEMPPAY);
                            //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPPAY");
                        }
                    }
                    else
                    {
                        command.rollback();
                        _dtSaleMain.RejectChanges();
                        string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageTEMP);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMPDLYPTRANS");
                    }
                }
            }
            _dtSaleMain.AcceptChanges();
            command.commit();
            return new StoreResult(ResponseCode.Success, "Success");
        }

        public double CalBalanceDiff(double balance, string pmCode, string pmSubCode)
        {
            double fxRate = command.GetFXCU_RATE(pmCode, pmSubCode);
            double min = command.GetMinCashUnit(pmSubCode);

            if (min == 0)
            {
                return balance;
            }
            else
            {
                if ((balance * fxRate) < min)
                {
                    return 0;
                }
                return balance;
            }
        }

        public DataTable selectTempChangeByRef()
        {
            try
            {
                return command.selectTempChangeByRef();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectTempChangeByRef");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new DataTable();
            }
        }

        public StoreResult CheckRunningNumber(string sRef, string vRef, string rRef, string cRef, string lRef,
                                              string eRef, string oRef, string pRef, string dRef, string hRef, string tRef)
        {
            try
            {
                StoreResult res = command.CheckRunningNumber(sRef, vRef, rRef, cRef, lRef, eRef, oRef, pRef, dRef, hRef, tRef);
                if (res.response.next && res.otherData != null && res.otherData.Rows.Count > 0)
                {
                    if (res.response == ResponseCode.Success)
                    {
                        DataTable dt = res.otherData;
                        ProgramConfig.saleRefNoIni = dt.Rows[0]["SaleInternalRef"].ToString();
                        ProgramConfig.voidRefNoIni = dt.Rows[0]["VoidInternalRef"].ToString();
                        ProgramConfig.returnRefNoIni = dt.Rows[0]["RetnInternalRef"].ToString();
                        ProgramConfig.cashInRefNoIni = dt.Rows[0]["CashinoutInternalRef"].ToString();
                        ProgramConfig.endOfShiftRefNoIni = dt.Rows[0]["LoginInternalRef"].ToString();
                        ProgramConfig.expermitRefNoIni = dt.Rows[0]["ExportPermitRef"].ToString();
                        ProgramConfig.openDayRefNoIni = dt.Rows[0]["OpendayInternalRef"].ToString();
                        ProgramConfig.posrepRefNoIni = dt.Rows[0]["PosRepRef"].ToString();
                        ProgramConfig.actionRefNoIni = dt.Rows[0]["DelEditItemInternalRef"].ToString();
                        ProgramConfig.holdOrderRefNoIni = dt.Rows[0]["HoldRef"].ToString();
                        ProgramConfig.tempFFTINo = dt.Rows[0]["TempFFTIRef"].ToString();

                        ProgramConfig.running.updateValue();

                        AppLog.writeLog(String.Format(@"[CheckRunningNumber] SaleInternalRef = {0}, VoidInternalRef = {1}, RetnInternalRef = {2}, 
                        CashinoutInternalRef = {3}, LoginInternalRef = {4}, ExportPermitRef = {5}, OpendayInternalRef = {6}, PosRepRef = {7}, actionRefNo = {8}"
                               , dt.Rows[0]["SaleInternalRef"].ToString()
                               , dt.Rows[0]["VoidInternalRef"].ToString()
                               , dt.Rows[0]["RetnInternalRef"].ToString()
                               , dt.Rows[0]["CashinoutInternalRef"].ToString()
                               , dt.Rows[0]["LoginInternalRef"].ToString()
                               , dt.Rows[0]["ExportPermitRef"].ToString()
                               , dt.Rows[0]["OpendayInternalRef"].ToString()
                               , dt.Rows[0]["PosRepRef"].ToString()
                               , dt.Rows[0]["DelEditItemInternalRef"].ToString()));
                    }
                }

                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckRunningNumber");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public DataTable GetPaymentMenuIcon()
        {
            return command.selectPaymentMenuIcon();
        }

        public DataTable GenerateTextBoxLabel()
        {
            return command.selectGenerateTextBoxLabel();
        }

        public DataTable selectCheckORG_TransFromQRPAYTRANS_Manual(string org_tranid, string paymentCode)
        {
            try
            {
                return command.selectCheckORG_TransFromQRPAYTRANS_Manual(org_tranid, paymentCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectCheckORG_TransFromQRPAYTRANS_Manual");
                throw;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

        public StoreResult CheckEmployee(string empID)
        {
            try
            {
                return command.CheckEmployee(empID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckEmployee");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckIME_Serial(string serial)
        {
            try
            {
                return command.CheckIME_Serial(serial);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckIME_Serial");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult CheckPCMan(string pcManID)
        {
            try
            {
                return command.CheckPCMan(pcManID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckPCMan");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult SavePCMan(string pcmanID)
        {
            command.newTransaction();
            try
            {
                removeDtSaleMain("1501");
                if (command.deleteTempDlyTransByRefRec("1501"))
                {
                    insertDtSaleMain(ProgramConfig.saleRefNo, "1501", "0", "E", pcmanID, "1", "0.00", "0.00", ProgramConfig.userId,
                                        "", "", "", "", "", "", "", "", "");

                    if (command.saveTempDlyptrans(ProgramConfig.saleRefNo, "1501", "0", "E", pcmanID, "1", "0.00", "0.00", ProgramConfig.userId,
                                            "", "", "", "", "", "", "", "", ""))
                    {
                        StoreResult res = command.SavePCMan(pcmanID);
                        if (res.response.next)
                        {
                            command.commit();
                            _dtSaleMain.AcceptChanges();
                        }
                        else
                        {
                            command.rollback();
                            _dtSaleMain.RejectChanges();
                        }
                        return res;
                    }
                }

                command.rollback();
                _dtSaleMain.RejectChanges();
                //Fix language
                return new StoreResult(ResponseCode.Error, "ไม่สามารถบันทึก PCMan");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.SavePCMan");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public DataTable selectSALEORDER_MENU()
        {
            try
            {
                return command.selectSALEORDER_MENU();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectSALEORDER_MENU");
                throw;
            }
        }

        public StoreResult GetSaleOrderType(string type, string menuID, string relateFlag, string level, string valueSaleOrder, string valueDelivery)
        {
            try
            {
                return command.GetSaleOrderType(type, menuID, relateFlag, level, valueSaleOrder, valueDelivery);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetSaleOrderType");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }


        public StoreResult insertSALEORDERTYPE_TRANS(int orderType, int deliveryType, int mediaType)
        {
            try
            {
                if (command.insertSALEORDERTYPE_TRANS(orderType, deliveryType, mediaType))
                {
                    return new StoreResult(ResponseCode.Success, "Success");
                }
                //Fix language
                return new StoreResult(ResponseCode.Error, "ไม่สามารถบันทึก SALEORDERTYPE_TRANS");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.GetSaleOrderType");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public DataTable selectSALEORDERTYPE_TRANS()
        {
            try
            {
                return command.selectSALEORDERTYPE_TRANS();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectDataToDeleteTempDLY");
                throw;
            }
        }

        public StoreResult deleteSALEORDERTYPE_TRANS()
        {
            try
            {
                return command.deleteSALEORDERTYPE_TRANS();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteSALEORDERTYPE_TRANS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult SaveHoldOrder()
        {
            try
            {
                StoreResult result = command.getRunning(FunctionID.Sale_CancelWhileSale_HoldOrder_SaveHoldTransaction, RunningReceiptID.HoldOrder);

                if (result.response.next)
                {
                    ProgramConfig.holdOrderRefNo = result.otherData.Rows[0]["ReferenceNo"].ToString();
                    ProgramConfig.holdOrderRefNoIni = result.otherData.Rows[0]["ReferenceNoINI"].ToString();
                }

                DataRow[] data = getTempSaleItem();
                if (command.insertTmpTrans(data.Length.ToString()))
                {                    
                    foreach (DataRow dr in data)
                    {
                        string rec = dr["REC"].ToString();
                        string pcd = dr["PCD"].ToString();
                        string qnt = dr["QNT"].ToString();
                        string amt = dr["AMT"].ToString();

                        string entryData = pcd;
                        var res = command.selectBarcodeExtend(rec);
                        if (res.response.next && res.otherData.Rows.Count > 0)
                        {
                            entryData = res.otherData.Rows[0]["PCD"].ToString();
                        }

                        string stt = dr["stt"].ToString();

                        command.insertTmpTransDetail(rec, pcd, qnt, amt, entryData, stt);
                    }

                    result = saveCancelSaleTransaction(FunctionID.Sale_CancelWhileSale_HoldOrder_SaveHoldTransaction, "0");
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, "Cannot Insert TmpTrans");
                }

                return result;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.SaveHoldOrder");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public DataRow[] GetDisplayOnhold(string refInp)
        {
            //try
            //{
                var res = command.selectTMPTRANS_DETAIL(refInp);
                if (res.response.next)
                {
                    foreach (DataRow dr in res.otherData.Rows)
                    {
                        string rec = dr["REC"].ToString();
                        string pcd = dr["PCD"].ToString();
                        string qnt = dr["QNT"].ToString();
                        string amt = dr["AMT"].ToString();
                        string stt = dr["stt"].ToString();

                        insertDtSaleMain(ProgramConfig.saleRefNo, rec, "", "0", pcd, qnt, amt, "", "", "", stt, "", "", (Convert.ToDouble(amt) / Convert.ToDouble(qnt)).ToString(), "", "", "", "");
                    }
                    _dtSaleMain.AcceptChanges();
                    updateTempSaleItemLanguage();
                }

                return _dtSaleMain.Select();
            //}
            //catch (NetworkConnectionException)
            //{
            //    AppLog.writeLog("connection to server lost at SaleProcess.deleteSALEORDERTYPE_TRANS");
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            //}
        }

        public StoreResult CheckOnhold()
        {
            try
            {
                return command.CheckOnhold();
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteSALEORDERTYPE_TRANS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult GetOnhold(string refInp)
        {
            try
            {
                return command.GetOnhold(refInp);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteSALEORDERTYPE_TRANS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult GetPaymentConfig(FunctionID function, string refNo)
        {
            try
            {
                return command.getPaymentConfig(function, refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteSALEORDERTYPE_TRANS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult SaveEDCTrans(string paymentNumber, string tid, string cardNo, string approveCode, string trace, string edcDate, double edcAmt, string cardIssue, string vatAmt)
        {
            try
            {
                DataTable dtCard = command.functionGetPaymentCode(paymentNumber);
                if (dtCard.Rows.Count > 0)
                {
                    //StoreResult result = command.getPaymentCode(paymentNumber);
                    //if (result.response.next)
                    //{
                    string creditCard = dtCard.Rows[0][0].ToString().Replace(" ", "");
                    string paymentCode = "";
                    if (creditCard.Length >= 4)
                    {
                        paymentCode = creditCard.Substring(0, 4);
                    }

                    StoreResult res = command.updateVat2EDCTrans(paymentCode, creditCard);
                    if (!res.response.next)
                    {
                        return new StoreResult(ResponseCode.Error, "Cannot updateVat2EDCTrans");
                    }

                    res = command.deleteEDCTrans(creditCard);
                    if (!res.response.next)
                    {
                        return new StoreResult(ResponseCode.Error, "Cannot deleteEDCTrans");
                    }

                    res = command.insertEDCTrans(tid, creditCard, approveCode, trace, edcDate, edcAmt, cardIssue, vatAmt);
                    if (!res.response.next)
                    {
                        return new StoreResult(ResponseCode.Error, "Cannot insertEDCTrans");
                    }
                    //}
                    return new StoreResult(ResponseCode.Success, "Success", data: dtCard);
                }
                else
                {
                    return new StoreResult(ResponseCode.Error, "No Data in dbo.getpaymentcode : " + paymentNumber + "");
                }
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.deleteSALEORDERTYPE_TRANS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult SpecialPay(string paymentCode, string cardNo, string amt, string memberCard)
        {
            try
            {
                return command.SpecialPay(paymentCode, cardNo, amt, memberCard);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.SpecialPay");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult findMember(int searchType, string data)
        {
            try
            {
                var res = searchMember(searchType, data);
                if (res.response.next)
                {
                    string memberID = data;//res.otherData.Rows[0]["MemberID"].ToString();
                    res = getMemberProfile(memberID);
                }
                return res;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.findMember");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult selectPAYMENT_PARAMETER(string pmCode)
        {
            try
            {
                DataTable dt = command.selectPAYMENT_PARAMETER(pmCode);
                if (dt.Rows.Count > 0)
                {
                    return new StoreResult(ResponseCode.Success, "Success", data: dt);
                }

                return new StoreResult(ResponseCode.Error, "Cannot selectPAYMENT_PARAMETER");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectPAYMENT_PARAMETER");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult selectParameterForQR(string type, string channel, string qrRunningType)
        {
            try
            {
                DataTable dt = command.selectParameterForQR(ProgramConfig.saleRefNo, type, channel, qrRunningType);
                if (dt.Rows.Count > 0)
	            {
		            return new StoreResult(ResponseCode.Success, "Success", data: dt);
                }
                return new StoreResult(ResponseCode.Error, "Cannot selectPAYMENT_PARAMETER"); 
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectParameterForQR");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }


        public StoreResult QRRequest(string tranID, string tranTime, string merchantID, string amt)
        {
            try
            {
                return command.QRRequest(tranID, tranTime, merchantID, amt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.QRRequest");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult saveQRPayTransOnline(string action_type, string seq, string org_tranID, string accountName, string qrCode, string resCode,
                                        string errorCode, string amount, string channal, string bankCode, string tokenID, string ota, 
                                        string bsd, string tepa_code, string sendBank, string receiveBank, string ref2, string transRef)
        {
            try
            {
                return command.saveQRPayTransOnline(ProgramConfig.saleRefNo,action_type, seq, org_tranID, accountName, qrCode, amount, resCode, errorCode, channal, bankCode, tokenID, ota
                                                , bsd, tepa_code, sendBank, receiveBank, ref2, transRef);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveQRPayTransOnline");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult QRInquirePayment(string tranID, string tranTime, string merchantID, string orgTran)
        {
            try
            {
                return command.QRInquirePayment(tranID, tranTime, merchantID, orgTran);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.QRInquirePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult selectAPICONF_RETRY(string apiName)
        {
            try
            {
                return command.selectAPICONF_RETRY(apiName);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectAPICONF_RETRY");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateQRPayTrans(string tranID, string resCode, string errCode, string resMsg, string onlineFlag, string auth, string authBy,
            string channel = "", string ref2 = "", string transRef = "", string stt = "",string whereCondition = "")
        {
            try
            {
                return command.updateQRPayTrans(tranID, resCode, errCode, resMsg, onlineFlag, auth, authBy, channel: channel, ref2: ref2, transRef: transRef, whereCondition: whereCondition, stt: stt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updateQRPayTrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updateQRPayTransBySet(string set, string whereCondition = "")
        {
            try
            {
                return command.updateQRPayTransBySet(set, whereCondition);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updateQRPayTrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }


        public StoreResult updateVoidQRPayTrans(string whereCondition = "")
        {
            try
            {
                return command.updateVoidQRPayTrans(whereCondition);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updateVoidQRPayTrans");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult API_POS_BSC_PAYMENT(string tranID, string qrCode, string amt, string seq)
        {
            try
            {
                DataTable dt = new DataTable();
                var res = command.API_POS_BSC_PAYMENT(tranID, qrCode, amt);
                if (res.response.next)
                {
                    dt = res.otherData;
                    DataRow dr = res.otherData.Rows[0];

                    res = command.saveQRPayTransOnline(ProgramConfig.saleRefNo, "SA", seq, dr["Prt_TxnUID"].ToString(), dr["Account_name"].ToString(),
                                                dr["QR_Code"].ToString(), amt, res.response.value, dr["ErrorCode"].ToString(), "BC"
                                                , dr["BankCode"].ToString()
                                                , dr["Token_ID"].ToString()
                                                , dr["OTA"].ToString()
                                                , dr["BSD"].ToString()
                                                , dr["TEPA_CODE"].ToString()
                                                , dr["Sending_Bank"].ToString()
                                                , "", "", "");
                    return new StoreResult(res.response, res.responseMessage, data: dt);
                }

                return new StoreResult(res.response, res.responseMessage);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.API_POS_BSC_PAYMENT");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckCallback(string tranID)
        {
            try
            {
                return command.CheckCallback(tranID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckCallback");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult API_POS_BSC_INQUIRY_PAYMENT_STATUS(string tranID, string orgTran, string qrCode, string bankCode, string tokenID, string ota)
        {
            try
            {
                return command.API_POS_BSC_INQUIRY_PAYMENT_STATUS(tranID, orgTran, qrCode, bankCode, tokenID, ota);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.API_POS_BSC_INQUIRY_PAYMENT_STATUS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult API_POS_BSC_VERIFY_SLIP(string tranID, string bankCode, string orgTran, string tranRef)
        {
            try
            {
                return command.API_POS_BSC_VERIFY_SLIP(tranID, bankCode, orgTran, tranRef);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.API_POS_BSC_VERIFY_SLIP");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult API_POS_BSC_VOID(string tranID, string orgTran, string amt)
        {
            try
            {
                return command.API_POS_BSC_VOID(ProgramConfig.saleRefNo, tranID, orgTran, amt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.API_POS_BSC_VOID");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult API_POS_BSC_INQUIRY_VOID_STATUS(string tranID, string orgTran, string amt)
        {
            try
            {
                return command.API_POS_BSC_INQUIRY_VOID_STATUS(tranID, orgTran, amt);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.API_POS_BSC_INQUIRY_VOID_STATUS");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult updatePOS_Running(string receiptID)
        {
            try
            {
                return command.updatePOS_Running(receiptID);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.updatePOS_Running");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult selectPOSADMIN_PAYMENT_STEP_DET(string whereCondition = "")
        {
            try
            {
                return command.selectPOSADMIN_PAYMENT_STEP_DET(whereCondition);
                //var cnt = res.otherData.AsEnumerable().Count(c => c["TextBoxType"].ToString() == PaymentStepDetail_TextBoxType.TextBox);
                //if (cnt > 2)
                //{
                //    return true;
                //}
                //return false;
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.selectPOSADMIN_PAYMENT_STEP_DET");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckCustomerModule(string memberCard)
        {
            try
            {
                return command.CheckCustomer(FunctionID.Sale_Member_Display, memberCard);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckCustomerModule");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckValueDate(string validateType, string dataEntry)
        {
            try
            {
                return command.CheckValueDate(validateType, dataEntry);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValueDate");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckValuePayment(string validateType, string dataEntry, string pmCode)
        {
            try
            {
                return command.CheckValuePayment(validateType, dataEntry, pmCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckProducRule(string projectid_productrule, string productcode, string barcodetype)
        {
            try
            {
                return command.CheckProducRule(projectid_productrule, productcode, barcodetype);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public void SaveMember()
        {
            try
            {

                string sty = ProgramConfig.pageBackFromPayment == PageBackFormPayment.Deposit ? "2" : "0";
                deletePaymentType("0");
                if (ProgramConfig.memberId != null && ProgramConfig.memberId != "" && ProgramConfig.memberId != "N/A")
                {
                    bool isMMFormat = ProgramConfig.memberFormat == MemberFormat.MegaMaket;
                    string memberID = isMMFormat ? ProgramConfig.memberCardNo : ProgramConfig.memberId;
                    string pdisc = isMMFormat ? ProgramConfig.memberProfileMMFormat.CreditCustomerNo : ""; // CreditCustomerNo
                    string discID = isMMFormat ? ProgramConfig.memberProfileMMFormat.CustomerCategory : ""; // CustomerCategory
                    string discAmt = isMMFormat ? ProgramConfig.memberProfileMMFormat.Customer_No : "0"; // Customer_No
                    string subMemberId = memberID.Substring(0, 2);

                    insertDtSaleMain(ProgramConfig.saleRefNo, "0", sty, "C", memberID, ProgramConfig.qntValue
                        , ProgramConfig.amtValue, ProgramConfig.disValue, ProgramConfig.userId, subMemberId
                        , "", pdisc, discID, "", "", discAmt, "0", "0");

                    if (!command.saveTempDlyptrans(ProgramConfig.saleRefNo, "0", sty, "C", memberID, ProgramConfig.qntValue
                        , ProgramConfig.amtValue, ProgramConfig.disValue, ProgramConfig.userId, subMemberId
                        , "", pdisc, discID, "", "", discAmt, "0", "0"))
                    {
                        string messageMTEMP = ProgramConfig.message.get("SaleProcess", "SaveMTEMPDLYPTRANSIncomplete").message;
                        throw new Exception(messageMTEMP);
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลสมาชิกใน TEMPDLYPTRANS");
                    }
                }
            }
            catch (NetworkConnectionException)
            {
                _dtSaleMain.RejectChanges();
                AppLog.writeLog("connection to server lost at SaleProcess.saveConfirmPayment");
                throw;
            }
            catch (Exception ex)
            {
                AppLog.writeLog(ex);
                _dtSaleMain.RejectChanges();
                command.rollback();
            }
        }

        public StoreResult SumSalesVat2EDC(string walfareID, string payamount, string pmCode, string cardNo, string menu)
        {
            try
            {
                return command.SumSalesVat2EDC(walfareID, payamount, pmCode, cardNo, menu);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult CheckDEPO(string validateType, string dataEntry, string pmCode)
        {
            try
            {
                return command.CheckDEPO(validateType, dataEntry, pmCode);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult savePaymentPOD(string pay, string payNum, string amt, string chg, string pdisc, string refID, string approveCode, string traceNo, 
                                          string terminalID, string merchantID, string edc_date, string invoiceNo, string QRCode, string rec)
        {
            try
            {
                string pmCode = "";
                if (pay.Length > 4)
                {
                    pay = pay.Substring(0, 4);
                }

                DataTable dt;

                if (payNum != "")
                {
                    dt = command.functionGetPaymentCode(payNum);
                    if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "XXXXXXXXXXXXXXXXXXXX")
                    {
                        pmCode = dt.Rows[0][0].ToString().Substring(0, 8);
                    }
                    else
                    {
                        pmCode = pay;
                    }                    
                }
                else
                {
                    pmCode = pay;
                }

                //string pmCode = res.otherData.Rows[0]["PAYMENTCODEDISPLAY"].ToString().Trim().Replace(" ", "");
                command.DeleteTEMP_PODTRANS_PAY(pay: pmCode, payNumber: payNum, approveCode: approveCode);
                var res = command.saveTEMP_PODTRANS_PAY(pmCode, payNum, amt, chg, pdisc, refID, approveCode, traceNo, terminalID, merchantID, edc_date, invoiceNo, QRCode, rec);
                if (!res.response.next)
                {
                    //Fix Language
                    string messageTEMPPAY = "ไม่สามารถบันทึกข้อมูลลง TEMP_PODTRANS_PAY";//ProgramConfig.message.get("SaleProcess", "SaveTEMPPAYIncomplete").message;
                    throw new Exception(messageTEMPPAY);
                    //throw new Exception("ไม่สามารถบันทึกข้อมูลลง TEMP_PODTRANS_PAY");
                }
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.savePaymentCashBalance");
                throw;
            }
            catch (Exception ex)
            {
                _dtSaleMain.RejectChanges();
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public StoreResult savePODToDataTank()
        {
            try
            {
                Profile check = ProgramConfig.getProfile(FunctionID.ReceivePOD_SyncToDataTank);
                if (check.policy == PolicyStatus.Work) //2
                {
                    string vrec = "1";
                    StoreResult res = command.syncToDataTank("POD", FunctionID.ReceivePOD_SyncToDataTank, ProgramConfig.podRefNo, vrec, ProgramConfig.abbNo);
                    if (res.response.next)
                    {
                        return new StoreResult(ResponseCode.Success, "Success");
                    }
                    return new StoreResult(res.response, res.responseMessage, res.helpMessage);
                    //string messageTEMP = ProgramConfig.message.get("SaleProcess", "SaveTEMPDLYPTRANSIncomplete").message;
                    //throw new Exception(messageTEMP);
                }
                return new StoreResult(ResponseCode.Success, "Success");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.saveToDataTank");
                throw;
            }
            catch (Exception ex)
            {
                command.rollback();
                AppLog.writeLog(ex);
                return new StoreResult(ResponseCode.Error, ex.Message);
            }
        }

        public DataTable loadTEMP_PODTRANS_PAY(string refNo)
        {
            try
            {
                return command.loadTEMP_PODTRANS_PAY(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.loadTEMP_PODTRANS_PAY");
                throw;
            }
        }

        public DataTable functionGetPaymentCode(string paymentNumber)
        {
            try
            {
                return command.functionGetPaymentCode(paymentNumber);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.loadTEMP_PODTRANS_PAY");
                throw;
            }
        }

        public void SaveCreditSalePayment(List<BJCBCPOS_Model.PaymentList> lst)
        {
            _ObjPayInvoiceAR.ListPaymnet = new clsMMFSAPI.payment_list[lst.Count];
            int cnt = 0;
            foreach (var item in lst)
            {
                _ObjPayInvoiceAR.ListPaymnet[cnt].payment_method = item.PaymentCode;
                _ObjPayInvoiceAR.ListPaymnet[cnt].apply_amount = Convert.ToDouble(item.Amount);
                _ObjPayInvoiceAR.ListPaymnet[cnt].payment_no = item.PaymentNo;
                cnt++;
            }
        }

        public StoreResult CallAPIProcess()
        {
            DataTable dt = command.selectPODTRANS(ProgramConfig.podRefNo);
            DataTable dt2 = command.selectPODTRANS_PAY();
            var res = command.getRunning(FunctionID.ReceivePOD_GetRunning, RunningReceiptID.PODAPI);

            RequestPayInvoicePOD dataLog = new RequestPayInvoicePOD();

            string rescode = "";
            string resmsg_en = "";
            string resmsg_th = "";
            string paymentID = "";
            string invoiceNo = "";
            string transID = "";
            double paymentAmount = 0;
            string paydate = "";
            string storeCode = "";
            string creditby = "";
            string recepitNo = "";
            string paymentNum = "";

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                dataLog.Invoice_No = invoiceNo = dr["REF_FULLTAX"].ToString();
                dataLog.Trans_ID = transID = res.otherData.Rows[0]["ReferenceNo"].ToString();
                dataLog.Payment_Amount = paymentAmount = Convert.ToDouble(dr["POD_AMT"].ToString());
                DateTime date = DateTime.Parse(dr["TTM"].ToString());
                dataLog.Payment_Date = paydate = date.ToString("yyyy-MM-dd HH:mm:ss");
                dataLog.Pay_Store_Code =  storeCode = dr["STCODE"].ToString();
                dataLog.Created_By = creditby = dr["CASHIER_ID"].ToString();
                dataLog.Receipt_No = recepitNo = dr["REF_POD"].ToString();

            }

            clsMMFSAPI.payment_list[] lst = new clsMMFSAPI.payment_list[dt2.Rows.Count];
            dataLog.Payment_list = new List<Payment_List>();

            if (dt2.Rows.Count > 0)
            {
                int cnt = 0;
                foreach (DataRow dr in dt2.Rows)
                {
                    var paylist = new Payment_List();

                    paylist.SEQ = lst[cnt].seq_no = (int)dr["REC"];
                    paylist.Payment_Method = lst[cnt].payment_method = dr["PAY"].ToString().Trim();
                    paylist.Apply_Amount = lst[cnt].apply_amount = Convert.ToDouble(dr["AMT"]) - Convert.ToDouble(dr["CHG"]);
                    paylist.Payment_No = lst[cnt].payment_no = dr["PAY_NUMBER"].ToString().Trim();
                    cnt++;

                    dataLog.Payment_list.Add(paylist);
                }
            }

            clsMMFSAPI sv = new clsMMFSAPI();

            int defaultDelay = 3;
            int defaultRetry = 5;
            res = selectAPICONF_RETRY("payInvoicePOD");
            if (res.response.next)
            {
                defaultDelay = Convert.ToInt32(res.otherData.Rows[0]["TIME_DELAY"]) * 1000;
                defaultRetry = Convert.ToInt32(res.otherData.Rows[0]["NUMBER_RETRY"]);
            }

            int cnt2 = 0;

            //Save API log
            command.saveAPILOG(dataLog, ProgramConfig.podRefNo, "payInvoicePOD", "OUT");
        Retry:
            sv.payInvoicePOD(ref rescode, ref resmsg_en, ref resmsg_th, ref paymentID, ref invoiceNo, ref transID, ref paymentAmount, ref paydate, ref storeCode, ref creditby, ref recepitNo, ref lst);

            ResponsePayInvoicePOD dataLogRes = new ResponsePayInvoicePOD();
            dataLogRes.Result_Code = rescode;
            dataLogRes.Result_Msg_EN = resmsg_en;
            dataLogRes.Result_Msg_TH = resmsg_th;
            dataLogRes.Api_Type = "";
            dataLogRes.Payment_ID = paymentID;
            dataLogRes.Invoice_No = invoiceNo;
            dataLogRes.Trans_ID = transID;
            dataLogRes.Payment_Amount = paymentAmount;
            dataLogRes.Payment_Date = paydate;
            dataLogRes.Pay_Store_Code = storeCode;
            dataLogRes.Created_By = creditby;
            dataLogRes.Receipt_No = recepitNo;

            command.saveAPILOG(dataLogRes, ProgramConfig.podRefNo, "payInvoicePOD", "IN");

            if (rescode == "0000")
            {
                command.updatePOD_TRANS(recepitNo, false, transID : transID, paymentID: paymentID);
                return new StoreResult(ResponseCode.Success, resmsg_th);
            }
            else if (rescode == "9999" && cnt2 < defaultRetry)
            {
                cnt2++;
                Thread.Sleep(defaultDelay);
                goto Retry;
            }
            else
            {
                return new StoreResult(ResponseCode.Error, resmsg_th);
            }
        }

        public DataTable loadTempCREDPAY_TRANS_PAY(string refNo)
        {
            try
            {
                return command.loadTempCREDPAY_TRANS_PAY(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.loadTEMP_PODTRANS_PAY");
                throw;
            }
        }

        public int selectMaxRecTEMP_PODTRANS_PAY(string refNo)
        {
            try
            {
                return command.selectMaxRecTEMP_PODTRANS_PAY(refNo);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.loadTEMP_PODTRANS_PAY");
                throw;
            }
        }

        public StoreResult SaveDrawerTrans(FunctionID function)
        {
            try
            {
                return command.saveDrawerTrans(ProgramConfig.saleRefNo, function);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult GetRefQRPayment_Offline(string pmCode, string qrRef)
        {
            try
            {
                return command.GetRefQRPayment_Offline(pmCode, qrRef);
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at SaleProcess.CheckValuePayment");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }

        public StoreResult selectDLYPTRANS(string refNo, string vty = "", string dty = "")
        {
            try
            {
                var res = command.selectTEMPDLYPTRANS_EDC(refNo, vty, dty);
                if (res.otherData.Rows.Count > 0)
                {
                    string cardNo = res.otherData.Rows[0]["PCD"].ToString();

                    res = command.selectEDCTrans(refNo, cardNo);
                    if (res.otherData.Rows.Count > 0)
                    {
                        return new StoreResult(ResponseCode.Success, data: res.otherData);
                    }
                }

                res = command.selectDLYPTRANS(refNo, vty, dty);
                if (res.otherData.Rows.Count > 0)
                {
                    string cardNo = res.otherData.Rows[0]["PCD"].ToString();

                    res = command.selectEDCTrans(refNo, cardNo);
                    if (res.otherData.Rows.Count > 0)
                    {
                        return new StoreResult(ResponseCode.Success, data: res.otherData);
                    }
                }
                return new StoreResult(ResponseCode.Error, "");
            }
            catch (NetworkConnectionException)
            {
                AppLog.writeLog("connection to server lost at VoidProcess.PrintVoidReceipt");
                throw;
            }
            catch (Exception ex)
            {
                return new StoreResult(ResponseCode.Error, ex.Message, "", "");
            }
        }
    }
}
