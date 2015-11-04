using System;
using System.Windows.Data;

namespace ListPopupDemo
{
    [ValueConversion(typeof(object), typeof(string))]
    public class LocalizationConverter : IValueConverter
    {
        public string Context { get; set; }

        public ILocalizer Localizer { get; set; }

        #region Constructor

        public LocalizationConverter()
        {
            this.Localizer = new Localizer();
        }

        public LocalizationConverter(string context, ILocalizer localizer)
        {
            this.Context = context;
            this.Localizer = localizer;
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (ReferenceEquals(this.Localizer, null)) return value;

            if (value is Enum) return this.Localizer.Translate((Enum)value);

            var code = value as string;
            if (String.IsNullOrWhiteSpace(code)) return String.Empty;
            if (String.IsNullOrWhiteSpace(this.Context)) return this.Localizer.Translate(code);
            return this.Localizer.Translate(String.Format("{0}.{1}", this.Context, code));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
