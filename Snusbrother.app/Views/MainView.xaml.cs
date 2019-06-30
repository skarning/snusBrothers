using Snusbrother.app.Views;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Snusbrothers.app.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class MainView : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();
            ContentView.Navigate(typeof(HomeView));
            NavView.Header = "Home";
        }

        /// <summary>
        /// Navigates to view when the view selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NavigationViewSelectionChangedEventArgs"/> instance containing the event data.</param>
        public void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentView.Navigate(typeof(SettingsView));
                NavView.Header = "Settings";
            }

            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                if (item.Name.ToString() == "MyStashNavItem")
                {
                    ContentView.Navigate(typeof(MyStashView));
                    NavView.Header = "My Stash";
                }


                if (item.Name.ToString() == "HomeNavItem")
                {
                    ContentView.Navigate(typeof(HomeView));
                    NavView.Header = "Home";
                }
            }
        }
    }
}
