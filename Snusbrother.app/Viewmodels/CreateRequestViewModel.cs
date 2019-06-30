using Snusbrother.app.Tools;
using Snusbrother.Models.Models;
using Snusbrothers.app;
using System;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace Snusbrother.app.viewModels
{
    class CreateRequestViewModel : ViewModelBase
    {
        private string requestedSnus;

        public string RequestedSnus
        {
            get
            {
                return requestedSnus;
            }

            set
            {
                requestedSnus = value;
                OnPropertyChanged();
            }
        }

        private string amount;

        public string Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
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

        public ICommand CreateRequestCommand { get; set; }

        public CreateRequestViewModel()
        {
            CreateRequestCommand = new CreateRequestCommand(CreateRequest);
        }

        private async void CreateRequest()
        {
            if (string.IsNullOrWhiteSpace(RequestedSnus) == true)
            {
                ErrorMessage = "Snus-Name needs to be filled";
                return;
            }
            if(await MapController.VerifyLocationAccess() == false)
            {
                ErrorMessage = "Could not access your position, please check settings";
                return;
            }
            InitializeRequest();
        }

        private async void InitializeRequest()
        {
            Geopoint myLocation = await MapController.GetMyPosition();
            var myLocationPositionDetails = myLocation.Position;
            var request = new Request
            {
                RequestedSnus = RequestedSnus,
                //Amount = Int16.Parse(Amount),
                ProfileId = ProfileHandler.CurrentProfile.ProfileId,
                Longitude = myLocationPositionDetails.Longitude,
                Latitude = myLocationPositionDetails.Longitude
            };
            if (await DataTransferUtility.PostRequest(request) == true)
                ErrorMessage = "Request Created";
            else
                ErrorMessage = "not success";
            Amount = "";
            RequestedSnus = "";
        }
    }
}
