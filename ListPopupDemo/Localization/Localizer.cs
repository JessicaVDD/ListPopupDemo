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
            return string.Format("[{0}]", code);
        }
    }
}
