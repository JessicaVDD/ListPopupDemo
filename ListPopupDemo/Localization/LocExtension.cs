using System;
using System.Diagnostics;
using System.Windows.Markup;


namespace ListPopupDemo
{
    [MarkupExtensionReturnType(typeof(string))]
    public class LocExtension : LocExtensionBase
    {
        #region Properties

        [ConstructorArgument("code")]
        public string Code { get; set; }

        #endregion

        #region Constructor

        public LocExtension() { }

        public LocExtension(string code)
            : this()
        {
            this.Code = code;
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (Localizer == null) return String.Format("[{0}]", this.Code);

                var code = this.Code;
                var context = DetermineContext(serviceProvider);

                if (String.IsNullOrEmpty(context))
                    Debug.WriteLine(String.Format("LocExtension: Could not find valid translation context for code [{0}]. Please specify context explicitly", code));
                else
                    code = String.Format("{0}.{1}", context, code);

                return Localizer.Translate(code);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LocExtension: An exception occured while resolving translation for code [{0}]\r\n{1}\r\n{2}", this.Code, ex.Message, ex.StackTrace);
                return String.Format("[{0}]", this.Code);
            }
        }

    }
}
