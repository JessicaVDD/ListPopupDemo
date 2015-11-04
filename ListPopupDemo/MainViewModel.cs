using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;

namespace ListPopupDemo
{
    public class MainViewModel : Screen
    {
        private List<Product> _Products;
        private MenuViewModel _MenuViewModel = new MenuViewModel();
        private bool _IsPopupVisible = false;

        public MainViewModel()
        {
            DisplayName = "Demo Application";
        }
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

        public MenuViewModel MenuView { get { return _MenuViewModel; } }

        public bool IsPopupVisible
        {
            get { return _IsPopupVisible; }
            set { _IsPopupVisible = value; NotifyOfPropertyChange(nameof(IsPopupVisible)); }
        }

        public void OpenMenu()
        {
            IsPopupVisible = !IsPopupVisible;
        }
    }
}
