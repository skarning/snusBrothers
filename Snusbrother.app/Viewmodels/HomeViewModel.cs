using Snusbrother.app.Tools;
using Snusbrother.Models.Models;
using Snusbrothers.app;
using Snusbrothers.app.views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace Snusbrother.app.viewModels
{
    class HomeViewModel : ViewModelBase
    {
        public ICommand AcceptRequestCommand { get; set; }

        public ICommand DeclineRequestCommand { get; set; }

        public ICommand CreateRequestCommand { get; set; }

        public ICommand CancelRequestCommand { get; set; }

        private ObservableCollection<Request> requestList = new ObservableCollection<Request>();

        public ObservableCollection<Request> RequestList { get { return requestList; } }

        private Frame CreateRequestFrame;

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

        private MapControl NavigationMap;

        public HomeViewModel(Frame createRequestFrame, MapControl navigationMap)
        {
            CreateRequestFrame = createRequestFrame;
            NavigationMap = navigationMap;
            AcceptRequestCommand = new HandlingRequestCommand(AcceptRequest);
            DeclineRequestCommand = new HandlingRequestCommand(DeclineRequest);
            CreateRequestCommand = new CreateRequestCommand(CreateRequest);
            CancelRequestCommand = new HandlingRequestCommand(CancelRequest);
            InitializeRequestList();
        }

        private async void InitializeRequestList()
        {
            foreach(Request r in await DataTransferUtility.GetRequests())
            {
                RequestList.Add(r);
            }
        }

        private async void AcceptRequest(Object selectedItem)
        {
            ErrorMessage = "Loading Route";
            var request = (Request)selectedItem;
            if(request.ProfileId == ProfileHandler.CurrentProfile.ProfileId)
            {
                ErrorMessage = "Cannot Accept your own request";
                return;
            }
            if(await MapController.VerifyLocationAccess() == false)
            {
                ErrorMessage = "Cannot get your position";
                return;
            }
            StartNavigation(request);
        }

        private async void StartNavigation(Request request)
        {
            var myPosition = await MapController.GetMyPosition();
            BasicGeoposition requestPosition = new BasicGeoposition()
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
            Geopoint navigateTo = new Geopoint(requestPosition);
            MapRouteFinderResult route = await MapController.GetRouteBetweenTwoPoints(myPosition, navigateTo);
            SetMapView(route);
        }

        private async void SetMapView(MapRouteFinderResult route)
        {
            if (route.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(route.Route);
                viewOfRoute.RouteColor = Windows.UI.Colors.SkyBlue;
                viewOfRoute.OutlineColor = Windows.UI.Colors.Green;

                // Add the new MapRouteView to the Routes collection
                NavigationMap.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await NavigationMap.TrySetViewBoundsAsync(
                      route.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }

            else
            {
                ErrorMessage = "Could not find suitable route";
            }
        }

        private void DeclineRequest(Object selectedItem)
        {

        }

        private void CreateRequest()
        {
            if(CreateRequestFrame!=null)
                CreateRequestFrame.Navigate(typeof(CreateRequestView));
        }

        private void CancelRequest(Object selectedItem)
        {

        }
    }
}

