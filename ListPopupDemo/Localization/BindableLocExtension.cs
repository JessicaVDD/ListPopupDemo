using System;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Markup;

namespace ListPopupDemo
{
    /// <summary>
    /// Creates a MarkupExtension that can use a binding to get the translation code
    /// This binding cannot have a converter, because it will be overwritten by the LocalizationConverter
    /// </summary>
    public class BindableLocExtension : LocExtensionBase
    {
        [ConstructorArgument("binding")]
        public Binding Binding { get; set; }

        #region Constructor

        public BindableLocExtension() { }

        public BindableLocExtension(Binding binding)
        {
            this.Binding = binding;
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ReferenceEquals(this.Binding, null)) return null;

            try
            {
                var context = DetermineContext(serviceProvider);

                if (String.IsNullOrEmpty(context))
                    Debug.WriteLine(String.Format("LocExtension: Could not find valid translation context for binding with path [{0}]. Please specify context explicitly", this.Binding.Path));

                IValueConverter converter = new LocalizationConverter(context, Localizer);
                if (!ReferenceEquals(this.Binding.Converter, null))
                {
                    var convGroup = new ConverterGroup();
                    convGroup.Converters.Add(this.Binding.Converter);
                    convGroup.Converters.Add(converter);
                    converter = convGroup;
                }
                this.Binding.Converter = converter;
                return this.Binding.ProvideValue(serviceProvider);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LocExtension: An exception occured while resolving translation for path [{0}]\r\n{1}\r\n{2}", this.Binding.Path, ex.Message, ex.StackTrace);
                return String.Format("[{0}]", this.Binding.Path);
            }
        }
    }
}
