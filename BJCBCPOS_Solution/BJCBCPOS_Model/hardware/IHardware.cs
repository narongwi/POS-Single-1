using System;
using System.Collections;
using System.Data;

namespace BJCBCPOS_Model
{
    interface IHardware
    {
        bool isDrawerOpened { get; }
        bool checkCashDrawer();
        bool openDrawer();
        void addDrawerListeners(Hardware.DrawerListener listener);
        void clearDrawerListeners();
        bool checkPrinter();
        bool printTermal(ArrayList data);
        bool printTermal(DataTable data);
        bool printFullTax(string refNo, string fftiNo, string offtiNo);
        bool printCN(string refCN);
    }
}
