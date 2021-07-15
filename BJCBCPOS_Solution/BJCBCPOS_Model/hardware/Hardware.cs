using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using BCOPOSTOSHIBA;
using BCOPOSHP;

namespace BJCBCPOS_Model
{
    public static class Hardware
    {
        public delegate void DrawerListener(string status);

        private static IHardware hardware = null;
        private const string key = "BIGC15dfnil23$498d#fdsfk25dcxi";
        private static PrinterBrand _printerBrand;
        private static bool printerConnected = false;

        public static bool isDrawerOpen
        {
            get { return hardware.isDrawerOpened; }
        }

        public static PrinterBrand printerBrand
        {
            get { return _printerBrand; }
            set
            {
                _printerBrand = value;
                if (_printerBrand == PrinterBrand.HP)
                {
                    hardware = new PrinterHP(key);
                }
                else if (_printerBrand == PrinterBrand.Toshiba)
                {
                    hardware = new PrinterToshiba(key);
                }
                else if (_printerBrand == PrinterBrand.Epson)
                {
                    hardware = new PrinterEpson(key);
                }
                else if (_printerBrand == PrinterBrand.Window)
                {
                    hardware = new PrinterWindows(key);
                }
            }
        }

        public static bool checkCashDrawer()
        {
            if (hardware != null)
            {
                return hardware.checkCashDrawer();
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }
        }

        public static bool openDrawer()
        {
            if (hardware != null)
            {
                return hardware.openDrawer();
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }

        }

        public static void addDrawerListeners(Hardware.DrawerListener listener)
        {
            if (hardware != null)
            {
                hardware.addDrawerListeners(listener);
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
            }
        }

        public static void clearDrawerListeners()
        {
            if (hardware != null)
            {
                hardware.clearDrawerListeners();
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
            }
        }

        public static bool checkPrinter()
        {
            if (hardware != null)
            {
                printerConnected = hardware.checkPrinter();
                return printerConnected;
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }
        }

        public static bool printTermal(ArrayList data)
        {
            if (hardware != null)
            {
                if (!printerConnected)
                {
                    hardware.checkPrinter();
                }
                return hardware.printTermal(data);
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }
        }

        public static bool printTermal(DataTable data)
        {
            if (hardware != null)
            {
                if (!printerConnected)
                {
                    hardware.checkPrinter();
                }
                return hardware.printTermal(data);        
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }
        }

        public static bool printByType(DataTable data, string abbNo, string refNo, string fftino, string offtino)
        {
            if (hardware != null)
            {
                if (!printerConnected)
                {
                    if (ProgramConfig.printInvoiceType == PrintInvoiceType.ABB)
                    {
                        hardware.checkPrinter();
                    }
                    else if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                    {
                  
                    }
                    else
                    {
                        hardware.checkPrinter();
                    }                    
                }

                if (ProgramConfig.printInvoiceType == PrintInvoiceType.ABB)
                {
                    return hardware.printTermal(data);
                }
                else if (ProgramConfig.printInvoiceType == PrintInvoiceType.FULLTAX)
                {
                    return hardware.printFullTax(refNo, fftino, offtino);
                }
                else
                {
                    return hardware.printTermal(data);
                }              
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }
        }

        public static bool printCN(string refCN)
        {
            if (hardware != null)
            {
                return hardware.printCN(refCN);
            }
            else
            {
                AppLog.writeLog(new Exception("no printer brand specified."));
                return false;
            }
        }

    }
}
