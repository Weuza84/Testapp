using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Testapp.Views;
using Testapp.ViewModels;
using Testapp.Models;

namespace Testapp.Views
{
    public partial class ModuleListView : ContentPage
    {
        ModuleViewModel _viewModel;

        public ModuleListView()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ModuleViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}