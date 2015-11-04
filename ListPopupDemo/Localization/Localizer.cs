using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListPopupDemo
{
    public interface ILocalizer
    {
        string Translate(string code);
        string Translate(Enum value);
    }

    public class Localizer : ILocalizer
    {
        public string Translate(Enum value)
        {
            return string.Format("[{0}]", value);
        }

        public string Translate(string code)
        {
            if (code == "ListPopupDemo.MenuView.MenuTitle") return "I'm a translated Menu Title";
            if (code == "ListPopupDemo.MenuView.The last line") return "Bound last line: translated";
            if (code == "General.LastLine") return "Translated with adjusted context";
            return string.Format("[{0}]", code);
        }
    }
}
