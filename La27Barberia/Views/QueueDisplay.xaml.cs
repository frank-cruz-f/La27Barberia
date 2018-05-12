using La27Barberia.Core.DTO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace La27Barberia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QueueDisplay : Page
    {

        public ObservableCollection<BarberDTO> Barbers { get; set; }
        RestClient<BarberDTO> barberRestClient;
        RestClient<TicketDTO> ticketRestClient;
        DispatcherTimer dispatcherTimer;

        public QueueDisplay()
        {
            this.InitializeComponent();
            barberRestClient = new RestClient<BarberDTO>();
            ticketRestClient = new RestClient<TicketDTO>();
            Barbers = new ObservableCollection<BarberDTO>();
            UpdateBarberList();
            CreateWaitTimer();
        }

        public async void ShowNewTicket(string ticketCode, string barberName)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {
                if(ticketCode != string.Empty)
                {
                    NextClientMessage.Text = string.Format("Cliente {0} pase", ticketCode);
                    NextClientMessage3.Text = string.Format(barberName);
                    NextClientDialog.ShowAsync();
                }
                await GetActiveBarbers();

                ThreadPoolTimer hideContentTimer = ThreadPoolTimer.CreateTimer(
                     async (timer) =>
                     {
                         await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                         () =>

                         {
                             NextClientDialog.Hide();
                         });

                     }, TimeSpan.FromSeconds(10));
            });
        }

        public async void ReloadScreen()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await GetActiveBarbers();
            });
        }

        private void CreateWaitTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
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
            if(activeBarbers != null)
            {
                Barbers.Clear();
                foreach (var barber in activeBarbers)
                {
                    barber.Tickets = barber.Tickets.Where(t => !t.HasStarted).ToList();
                    Barbers.Add(barber);
                }
            }
            
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            foreach(BarberDTO barber in BarbersListView.Items)
            {
                if(barber.WaitEstimatedMinutes >= 2)
                {
                    barber.WaitEstimatedMinutes--;
                }
            }
        }
    }
}
