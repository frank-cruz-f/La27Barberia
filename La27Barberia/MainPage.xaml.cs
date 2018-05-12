using La27Barberia.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace La27Barberia
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public QueueDisplay tvDisplay;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {

            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home")
                {
                    NavView.SelectedItem = item;
                    NavView_Navigate((NavigationViewItem)item);
                    break;
                }
            }

            ContentFrame.Navigated += On_Navigated;

            // add keyboard accelerators for backwards navigation
            KeyboardAccelerator GoBack = new KeyboardAccelerator();
            GoBack.Key = VirtualKey.GoBack;
            GoBack.Invoked += BackInvoked;
            KeyboardAccelerator AltLeft = new KeyboardAccelerator();
            AltLeft.Key = VirtualKey.Left;
            AltLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(GoBack);
            this.KeyboardAccelerators.Add(AltLeft);
            // ALT routes here
            AltLeft.Modifiers = VirtualKeyModifiers.Menu;
            NavView.IsPaneOpen = false;
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // find NavigationViewItem with Content that equals InvokedItem
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem);
        }

        private async void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "Home":
                    ContentFrame.Navigate(typeof(Home));
                    break;
                case "BarberSelection":
                    ContentFrame.Navigate(typeof(BarberSelection));
                    break;
                case "CreateBarbers":
                    ContentFrame.Navigate(typeof(CreateBarbers));
                    break;
                case "Clients":
                    ContentFrame.Navigate(typeof(Clients));
                    break;
                case "QueueDisplay":
                    CoreApplicationView newCoreView = CoreApplication.CreateNewView();

                    ApplicationView newAppView = null;
                    int mainViewId = ApplicationView.GetApplicationViewIdForWindow(
                      CoreApplication.MainView.CoreWindow);

                    await newCoreView.Dispatcher.RunAsync(
                      CoreDispatcherPriority.Normal,
                      () =>
                      {
                          newAppView = ApplicationView.GetForCurrentView();
                          tvDisplay = new QueueDisplay();
                          Window.Current.Content = tvDisplay;
                          Window.Current.Activate();
                      });
                    await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                      newAppView.Id,
                      ViewSizePreference.Custom,
                      mainViewId,
                      ViewSizePreference.Custom);
                    break;
            }
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            bool navigated = false;

            // don't go back if the nav pane is overlayed
            if (NavView.IsPaneOpen && (NavView.DisplayMode == NavigationViewDisplayMode.Compact || NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
            {
                return false;
            }
            else
            {
                if (ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                    navigated = true;
                }
            }
            return navigated;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            Dictionary<Type, string> lookup = new Dictionary<Type, string>()
                {
                    {typeof(Home), "Home"},
                    {typeof(BarberSelection), "BarberSelection"},
                    {typeof(CreateBarbers), "CreateBarbers"},
                    {typeof(Clients), "Clients"},
                    {typeof(QueueDisplay), "QueueDisplay"}
                };

            String stringTag = lookup[ContentFrame.SourcePageType];
            if(e.Content.GetType() == typeof(CreateBarbers))
            {
                var content = (CreateBarbers)e.Content;
                content.TVDisplay = tvDisplay;
            }

            // set the new SelectedItem  
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.Equals(stringTag))
                {
                    item.IsSelected = true;
                    break;
                }
            }
            NavView.IsPaneOpen = false;
        }
    }
}
