
using System;
using System.Net.Http;
using System.Windows.Input;
using Newtonsoft.Json;
using Snusbrother.app.Commands;
using Snusbrother.app.Tools;
using Snusbrother.Models.Models;
using Snusbrothers.app.views;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Snusbrother.app.viewModels
{
    class LogInViewModel: ViewModelBase
    {
        public ICommand LogInCommand { get; set; }
        
        public ICommand CreateProfileCommand { get; set; }

        private string userName;

        public string UserName {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password {
            get {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public LogInViewModel()
        {
            LogInCommand = new HandlingLogInCommand(LogIn);
            CreateProfileCommand = new HandlingLogInCommand(CreateProfile);

        }

        private string errorMessage;

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        private async void LogIn()
        {
            ErrorMessage = "Logging In";
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "All field needs to be filled";
                return;

            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51180/api/");
                var json = await client.GetStringAsync("profiles");
                Profile[] profiles = JsonConvert.DeserializeObject<Profile[]>(json);
                if(CheckUserNamePasswordCombo(profiles) == true)
                {
                    CreateFrame(typeof(MainView), ProfileHandler.CurrentProfile.Name + " is the ultimate snusbrother");
                }
           }
        }

        private bool CheckUserNamePasswordCombo(Profile[] profiles)
        {
            for(int i = 0; i < profiles.Length; i++)
            {
                bool checkUsername = false;
                bool checkPassword = false;
                if (profiles[i].Username == UserName)
                {
                    checkUsername = true;
                    if (profiles[i].Password == Password)
                    {
                        checkPassword = true;
                        ErrorMessage = "Login Succsessfull";
                        ProfileHandler.CurrentProfile = profiles[i];
                        return true;
                    }
                }
                if (checkUsername == true && checkPassword == false)
                {
                    ErrorMessage = "Incorrect password";
                    return false;
                }
            }
            ErrorMessage = "User does not exist";
            return false;
        }

        private void CreateProfile()
        {
            CreateFrame(typeof(CreateProfileView), "Create Profile");
        }

        private async void CreateFrame(Type view, string frameName)
        {
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            async () =>
                            {
                                var newWindow = Window.Current;
                                var newAppView = ApplicationView.GetForCurrentView();
                                newAppView.Title = frameName;

                                var frame = new Frame();
                                frame.Navigate(view, null);
                                newWindow.Content = frame;
                                newWindow.Activate();

                                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                                    newAppView.Id,
                                    ViewSizePreference.UseMinimum,
                                    currentAV.Id,
                                    ViewSizePreference.UseMinimum);
                            });
        }
    }
}
