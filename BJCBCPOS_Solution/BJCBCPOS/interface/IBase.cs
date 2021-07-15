using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJCBCPOS
{
    interface IBase
    {
        void DropDownClick(UCDropDownList ucDDL, string str);
        void CurrencyClick(string str);
        void ShowDropdownList(UCDropDownList ucDDL, bool isLeftSide = true);
        void SearchFormTextBoxIcon(UCTextBoxWithIcon ucTBWI);
    }
}
