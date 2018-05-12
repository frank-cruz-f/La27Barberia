using La27Barberia.Core.DTO;
using La27Barberia.Core.Enum;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace La27Barberia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BarberSelection : Page
    {
        public ObservableCollection<BarberDTO> Barbers { get; set; }

        RestClient<BarberDTO> barberRestClient;
        RestClient<TicketDTO> ticketRestClient;
        private int estimatedTime = 0;
        DispatcherTimer dispatcherTimer;

        public BarberSelection()
        {
            this.InitializeComponent();
            barberRestClient = new RestClient<BarberDTO>();
            ticketRestClient = new RestClient<TicketDTO>();
            Barbers = new ObservableCollection<BarberDTO>();
            UpdateBarberList();
            CreateTimer();
        }

        private void UpdateBarberList()
        {
            Task.Run(async () =>
            {
                await GetActiveBarbers();
            }).Wait();

        }

        private async Task GetActiveBarbers()
        {
            var activeBarbers = await barberRestClient.GetListAsync(Common.GetActiveBarbersURI);
            Barbers.Clear();
            if(activeBarbers != null)
            {
                foreach (var item in activeBarbers)
                {
                    Barbers.Add(item);
                }
            }
        }

        private async void Button_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Tag != null)
            {
                var barbersForType = await barberRestClient.GetListAsync(string.Format(Common.GetBarbersForTypeURI, (int)((BarberType)button.Tag)));
                Barbers.Clear();
                foreach (var item in barbersForType)
                {
                    Barbers.Add(item);
                }
            }
            else
            {
                var activeBarbers = await barberRestClient.GetListAsync(Common.GetActiveBarbersURI);
                Barbers.Clear();
                if(activeBarbers != null)
                {
                    foreach (var item in activeBarbers)
                    {
                        Barbers.Add(item);
                    }
                }
            }
        }

        private void BarberOptionRbtn_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            estimatedTime = int.Parse(((RadioButton)sender).Tag.ToString());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((ComboBoxItem)StylistCbx.SelectedItem);
            estimatedTime = int.Parse(selectedItem.Tag.ToString());
        }

        private async void StackPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

            var selectedBarber = (BarberDTO)((StackPanel)sender).Tag;
            if (selectedBarber.BarberType == BarberType.Barber)
            {
                estimatedTime = 30;
                BarberOptions.Visibility = Visibility.Visible;
                StylistOptions.Visibility = Visibility.Collapsed;
                ManicureOptions.Visibility = Visibility.Collapsed;
            }
            else if (selectedBarber.BarberType == BarberType.Stylist)
            {
                estimatedTime = 45;
                BarberOptions.Visibility = Visibility.Collapsed;
                StylistOptions.Visibility = Visibility.Visible;
                ManicureOptions.Visibility = Visibility.Collapsed;
            }
            else if (selectedBarber.BarberType == BarberType.ManicurePedicure)
            {
                estimatedTime = 45;
                BarberOptions.Visibility = Visibility.Collapsed;
                StylistOptions.Visibility = Visibility.Collapsed;
                ManicureOptions.Visibility = Visibility.Visible;
            }

            var ticketDialog = await CreateTicketDialog.ShowAsync();

            if (ticketDialog == ContentDialogResult.Primary)
            {
                var ticket = new TicketDTO();
                ticket.ClientId = 1;
                ticket.Code = nameTxb.Text;
                ticket.CreateTime = DateTime.Now;
                ticket.StartTime = ticket.CreateTime;
                ticket.EstimatedMinutes = estimatedTime;
                ticket.BarberId = selectedBarber.Id;
                ticket.IsActive = true;
                ticket.HasStarted = false;
                try
                {
                    await ticketRestClient.PostAsync(ticket, Common.CreateTicketURI);
                }
                catch (Exception ex)
                {
                    MessageDialog message = new MessageDialog(ex.Message);
                    await message.ShowAsync();
                }
                nameTxb.Text = string.Empty;
                await GetActiveBarbers();
            }
        }

        private void CreateTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            foreach (BarberDTO barber in BarbersListView.Items)
            {
                if (barber.WaitEstimatedMinutes >= 2)
                {
                    barber.WaitEstimatedMinutes--;
                }
            }
        }
    }
}
