using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BJCBCPOS
{
    public interface IDropdownListItem
    {
        event EventHandler UCItemDropDownListClick;

        string DisplayText { get; set; }
        string ValueText { get; set; }
        int DisplayTextLength { get; set; }
        int LineWidth { get; set; }
        bool LineVisible { get; set; }
        Point LineLocation { get; }
        Image picIcon { get; set; }
        Font LabelFont { get; set; }

    }
}
