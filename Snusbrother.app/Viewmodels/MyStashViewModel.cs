using Snusbrother.app.Commands;
using Snusbrother.app.Tools;
using Snusbrother.Models.Models;
using Snusbrother.Models.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Snusbrother.app.viewModels
{
    class MyStashViewModel : ViewModelBase
    {
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


        private double price;

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        private Enums.Strength strength;

        public Enums.Strength Strength {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                OnPropertyChanged();
            }
        }

        public List<Enums.Strength> SnusStrengths { get; } = Enum.GetValues(typeof(Enums.Strength)).Cast<Enums.Strength>().ToList();

        private ObservableCollection<Snus> myStashList = new ObservableCollection<Snus>();

        public ObservableCollection<Snus> MyStashList {
            get
            {
                return myStashList;
            }
            set
            {
                value = myStashList;
                OnPropertyChanged();
            }
        }

        public ICommand AddSnusCommand { get; set; }

        public MyStashViewModel()
        {
           AddSnusCommand = new AddSnusCommand(AddSnusToStash);
            InitializeMyStashList();
        }

        private async void InitializeMyStashList()
        {
            Snus[] snus = await ProfileHandler.GetSnusForThisProfile();
            foreach(Snus s in snus)
            {
                MyStashList.Add(s);
            }
        }

        private async void AddSnusToStash()
        {
            ErrorMessage = "";
            if (Name != "")
            {
                var snus = new Snus
                {
                    Name = Name,
                    Price = Price,
                    SnusStrength = Strength,
                    ProfileId = ProfileHandler.CurrentProfile.ProfileId
                };
                if (await DataTransferUtility.PostSnus(snus) == true)
                    MyStashList.Add(snus);
                    ErrorMessage = "Snus added to stash";

            }
            else
            {
                ErrorMessage = "The Name field has to be filled";
            }
        }
    }
}