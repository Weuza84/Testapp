using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Testapp.Models;
using Testapp.Services;
using Testapp.Views;
using Xamarin.Forms;

namespace Testapp.ViewModels
{
    public class ModuleViewModel : BaseViewModel
    {
        private Module _selectedModule;
        public ObservableCollection<Module> Modules { get; set; }
        public List<Module> modList { get; set; }
        public Command LoadModuleCommand { get; }
        public Command<Module> ModuleTapped { get; }
        IImageService imageService = DependencyService.Get<IImageService>();

        public ModuleViewModel()
        {
            Title = "Module";
            Modules = new ObservableCollection<Module>();
            modList = new List<Module>();
            LoadModuleCommand = new Command(async () => await ExecuteLoadModuleCommand());

            ModuleTapped = new Command<Module>(OnModuleSelected);
        }

        async Task ExecuteLoadModuleCommand()
        {
            try
            {
                Modules.Clear();
                modList = await App.Database.GetModulesAsync();

                if (modList.Count <= 0)
                {
                    modList = await App.DataManager.GetModulesAsync();
                    foreach (Module mod in modList)
                    {
                        var input = await App.Database.SaveModuleAsync(mod);

                       imageService.DownloadImage(Constants.url + "/storage/images/" + mod.Pic, "FF_Pics");
                    }
                }

                foreach (Module mod in modList)
                {
                    Modules.Add(mod);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            SelectedModule = null;

            await ExecuteLoadModuleCommand();
        }

        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                SetProperty(ref _selectedModule, value);
                OnModuleSelected(value);
            }
        }

        async void OnModuleSelected(Module module)
        {
            if (module == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(QuestionPage)}?{nameof(QuestionViewModel.QuestionId)}={module.Id}");
        }
    }
}