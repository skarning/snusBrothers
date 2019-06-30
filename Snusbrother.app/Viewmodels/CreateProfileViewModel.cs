using Newtonsoft.Json;
using Snusbrother.app.Commands;
using Snusbrother.Models.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snusbrother.app.viewModels
{
    class CreateProfileViewModel : ViewModelBase
    {
        public ICommand CreateProfileCommand { get; set; }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string userName;

        public string UserName
        {
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

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string reEnteredPassword;

        public string ReEnteredPassword
        {
            get
            {
                return reEnteredPassword;
            }
            set
            {
                reEnteredPassword = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset birthday;

        public DateTimeOffset Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
                OnPropertyChanged();
            }
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

        public CreateProfileViewModel()
        {
            CreateProfileCommand = new CreateProfileCommand(CreateProfile);
        }

        private async void CreateProfile()
        {
            if (CheckProfile() == true)
            {
                var profile = new Profile
                {
                    Name = Name,
                    Username = UserName,
                    Password = Password,
                    Birthday = Birthday
                };

                string json = JsonConvert.SerializeObject(profile);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                bool response = await UploadProfileAsync(profile, content);
                if (response == true)
                {
                    ErrorMessage = "Profile Created";
                    Name = null;
                    Password = null;
                    ReEnteredPassword = null;
                    UserName = null;
                }
            }
        }

        private bool CheckProfile()
        {
            ErrorMessage = "";
            bool CreateProfileStatus = true;
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(UserName)|| string.IsNullOrWhiteSpace(Password) || 
                string.IsNullOrWhiteSpace(ReEnteredPassword) || Birthday == null)
            {
                CreateProfileStatus = false;
                ErrorMessage += "Not all fields are filled.\n";
            }
            if (Password != ReEnteredPassword)
            {
                CreateProfileStatus = false;
                ErrorMessage += "Passwords does not match\n";
            }
            if (CheckDate() == false)
            {
                CreateProfileStatus = false;
                ErrorMessage += "You need to be at least 18 years to use this application\n";
            }
            if (CreateProfileStatus == true)
            {
                if (Name == null)
                    errorMessage = "Helvete faan";
                return true;
            }
            return false;
        }

        private bool CheckDate()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (age > 18)
                return true;
            else
                return false;
        }

        private async Task<bool> UploadProfileAsync(Profile profile, StringContent content)
        {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(@"http://localhost:51180//api/");
                        HttpResponseMessage response = await client.PostAsync("profiles", content);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }
                    catch (HttpRequestException)
                    {
                        ErrorMessage = "Something went wring with creating profile";
                        return false;
                    }
                    catch (Exception)
                    {
                        ErrorMessage = "Something unexpected happened";
                        return false;
                    }
                }
            return true;
        }
    }
}


