using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Testapp.Services;
using Testapp.Views;
using Testapp.Models;
using Testapp.Data;
using System.IO;

namespace Testapp
{
    public partial class App : Application
    {
        public static ApiManager DataManager { get; private set; }
        public static FFDatabase database;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            Device.SetFlags(new string[] { "RadioButton_Experimental" });

            DataManager = new ApiManager(new RestService());
            MainPage = new AppShell();
        }

        public static FFDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FFDatabase();
                }

                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
