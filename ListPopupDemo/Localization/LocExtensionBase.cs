using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ListPopupDemo
{
    public abstract class LocExtensionBase : MarkupExtension
    {
        protected static ILocalizer Localizer = new Localizer();

        public string Context { get; set; }

        protected string DetermineContext(IServiceProvider serviceProvider)
        {
            if (!String.IsNullOrEmpty(this.Context)) return this.Context;

            var rootObjectProvider = serviceProvider.GetService(typeof(System.Xaml.IRootObjectProvider)) as System.Xaml.IRootObjectProvider;
            if (!ReferenceEquals(rootObjectProvider, null))
            {
                if (rootObjectProvider.RootObject is UserControl)
                    return rootObjectProvider.RootObject.GetType().FullName;
                
                var localizedDataTemplate = rootObjectProvider.RootObject as LocalizedDataTemplate;
                if (localizedDataTemplate != null)
                    return localizedDataTemplate.Context;

                // TODO: andere manier zoeken om dit op te lossen in geval van een ResourceDictionary - zie RADIUS-615
                var resourceDictionary = rootObjectProvider.RootObject as ResourceDictionary;
                if (resourceDictionary != null)
                    return resourceDictionary["TranslationContext"] as string;
            }
            else
            {
                IProvideValueTarget ipvt = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                var target = ipvt.TargetObject as DependencyObject;
                if (target == null) return null; //return this;
                var vw = GetView(target);
                if (!ReferenceEquals(vw, null)) return vw.GetType().FullName;
            }

            return null;
        }

        private static UserControl GetView(DependencyObject obj)
        {
            UserControl view = null;

            DependencyObject current = obj;
            while (view == null && current != null)
            {
                DependencyObject parent = null;
                if (current is ContextMenu)
                    parent = ((ContextMenu)current).PlacementTarget;
                else if (current is ToolTip)
                    parent = ((ToolTip)current).PlacementTarget;
                else if (current is Visual || current is System.Windows.Media.Media3D.Visual3D)
                    parent = VisualTreeHelper.GetParent(current);
                if (parent == null)
                    parent = LogicalTreeHelper.GetParent(current);

                var el = parent as UserControl;
                if (el != null && el.GetType().Name.EndsWith("View", StringComparison.CurrentCultureIgnoreCase)) view = el;
                current = parent;
            }

            return view;
        }
    }
}
