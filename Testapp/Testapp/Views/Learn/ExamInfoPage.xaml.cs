using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Testapp.ViewModels;

namespace Testapp.Views
{
    public partial class QuestionPage : ContentPage
    {
        public QuestionPage()
        {
            InitializeComponent();

            BindingContext = new QuestionViewModel();
        }
    }
}