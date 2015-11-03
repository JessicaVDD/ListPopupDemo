using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;

namespace ListPopupDemo
{
    public class MainViewModel : Screen
    {
        private List<Product> _Products;

        public List<Product> Products
        {
            get { return _Products; }
            set { _Products = value; NotifyOfPropertyChange(nameof(Products)); }
        }

        public string ButtonText { get { return "Snert"; } }

        protected override void OnActivate()
        {
            base.OnActivate();
            Products = new DataManager().Load().ToList();
        }
    }
}
