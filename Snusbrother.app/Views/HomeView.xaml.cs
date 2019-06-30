using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Snusbrother.app.viewModels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Snusbrothers.app.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            this.InitializeComponent();
            this.DataContext = new HomeViewModel(CreateRequestFrame, NavigationMap);
        }
            /*// Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 59.288139, Longitude = 11.146045 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 12;
            MapControl1.LandmarksVisible = true;
            MapControl1.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.Aerial3DWithRoads;

            SetLocation();
        }

        private async void SetLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    Geopoint myLocation = pos.Coordinate.Point;

                    // Set the map location.
                    MapControl1.Center = myLocation;
                    MapControl1.ZoomLevel = 20;
                    MapControl1.LandmarksVisible = true;
                    PlaceInfoCreateOptions options = new PlaceInfoCreateOptions();


                    // Start at Microsoft in Redmond, Washington.

                    // End at the city of Seattle, Washington.
                    BasicGeoposition endLocation = new BasicGeoposition() { Latitude = 59.213236, Longitude = 10.930166 };

                    // Get the route between the points.
                    MapRouteFinderResult routeResult =
                          await MapRouteFinder.GetDrivingRouteAsync(
                          new Geopoint(myLocation.Position),
                          new Geopoint(endLocation),
                          MapRouteOptimization.Time,
                          MapRouteRestrictions.None);

                    if (routeResult.Status == MapRouteFinderStatus.Success)
                    {
                        // Use the route to initialize a MapRouteView.
                        MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                        viewOfRoute.RouteColor = Windows.UI.Colors.Blue;
                        viewOfRoute.OutlineColor = Windows.UI.Colors.Yellow;

                        // Add the new MapRouteView to the Routes collection
                        // of the MapControl.
                        MapControl1.Routes.Add(viewOfRoute);

                        // Fit the MapControl to the route.
                        await MapControl1.TrySetViewBoundsAsync(
                              routeResult.Route.BoundingBox,
                              null,
                              Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                    }
                        break;

                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    MessageDialog dialog = new MessageDialog("Access denied");
                    await dialog.ShowAsync();
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // Handle the case if  an unspecified error occurs.
                    MessageDialog di = new MessageDialog("Interrupted");
                    await di.ShowAsync();
                    break;
            }
        }*/
    }
}
