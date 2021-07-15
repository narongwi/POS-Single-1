using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJCBCPOS
{
    public interface IRedeem
    {
        event EventHandler EnterFromButton;
        string SEQText { get; set; }
        string QTYText { get; set; }
        string UsePointTxt { get; set; }
        string UseCashTxt { get; set; }
        string SumPointTxt { get; set; }
        string SumCashTxt { get; set; }
        string DiscountTxt { get; set; }
        string ItemNameTxt { get; set; }
        string LimitTxt { get; set; }
        string RedeemCode{ get; set; }
        string RuleID { get; set; }
        bool VisibleBtnPlusMinus { set; }
    }
}
