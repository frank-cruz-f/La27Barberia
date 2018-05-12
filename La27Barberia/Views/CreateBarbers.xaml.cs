using La27Barberia.Core.DTO;
using La27Barberia.Core.Enum;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace La27Barberia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateBarbers : Page
    {
        public ObservableCollection<BarberDTO> Barbers;
        RestClient<BarberDTO> rest;
        RestClient<TicketDTO> restTicket;
        private int selectedBarberType = 0;
        private string photoRoute = "";
        private string genre = "male";

        public QueueDisplay TVDisplay { get; set; }

        public CreateBarbers()
        {
            this.InitializeComponent();
            rest = new RestClient<BarberDTO>();
            restTicket = new RestClient<TicketDTO>();
            Barbers = new ObservableCollection<BarberDTO>();
            GetBarbers();
        }
        

        private async Task GetBarbers()
        {
            var barbers = await rest.GetListAsync(Common.GetBarbersURI);
            Barbers.Clear();
            if (barbers != null)
            {
                foreach (var item in barbers)
                {
                    Barbers.Add(item);
                }
            }
            
        }

        private async void CreateBarberBtn_ClickAsync(object sender, TappedRoutedEventArgs e)
        {
            var result = await BarberCreateDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var newBarber = new BarberDTO();
                newBarber.Name = nameTxb.Text;
                newBarber.Code = codeTxb.Text;
                newBarber.PhotoRoute = photoRoute != string.Empty ? photoRoute : genre == "male" ? Common.DefaultPhotoRouteMale : Common.DefaultPhotoRouteFemale;
                newBarber.BarberType = (BarberType)selectedBarberType;
                await rest.PostAsync(newBarber, Common.CreateBarbersURI);
            }
            ClearForm();
            await GetBarbers();
        }

        private void BarberTypeRbtn_Checked(object sender, RoutedEventArgs e)
        {
            var button = (RadioButton)sender;
            selectedBarberType = int.Parse((string)button.Tag);
        }

        private async void PickPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new FileOpenPicker();
            fileDialog.FileTypeFilter.Add(".jpeg");
            fileDialog.FileTypeFilter.Add(".png");
            fileDialog.FileTypeFilter.Add(".jpg");
            fileDialog.ViewMode = PickerViewMode.Thumbnail;
            fileDialog.CommitButtonText = "Aceptar";
            fileDialog.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            var pickerResult = await fileDialog.PickSingleFileAsync();

            if (pickerResult != null)
            {
                var copyResult = await pickerResult.CopyAsync(ApplicationData.Current.LocalFolder, pickerResult.Name, NameCollisionOption.ReplaceExisting);
                PhotoImg.Source = new BitmapImage(new Uri(copyResult.Path));
                photoRoute = copyResult.Path;
            }
        }

        private void GenreRbtn_Checked(object sender, RoutedEventArgs e)
        {
            var button = (RadioButton)sender;
            genre = (string)button.Tag;
        }

        private void ClearForm()
        {
            nameTxb.Text = string.Empty;
            codeTxb.Text = string.Empty;
            selectedBarberType = 0;
            photoRoute = string.Empty;
            genre = "male";
        }

        private async void ActiveToggle_Toggled(object sender, RoutedEventArgs e)
        {
            var button = (ToggleSwitch)sender;
            var barber = Barbers.First(b => b.Id == int.Parse(button.Tag.ToString()));
            barber.IsActive = button.IsOn;
            await rest.PutAsync(barber, Common.UpdateBarberURI);
        }

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void editBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var barber = Barbers.FirstOrDefault(b => b.Id == int.Parse(((Button)sender).Tag.ToString()));

            nameTxb.Text = barber.Name;
            codeTxb.Text = barber.Code;
            photoRoute = barber.PhotoRoute;
            selectedBarberType = (int)barber.BarberType;
            PhotoImg.Source = new BitmapImage(new Uri(Path.GetFullPath(barber.PhotoRoute))); //;
            var result = await BarberCreateDialog.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                barber.Name = nameTxb.Text;
                barber.Code = codeTxb.Text;
                barber.PhotoRoute = photoRoute != string.Empty ? photoRoute : genre == "male" ? Common.DefaultPhotoRouteMale : Common.DefaultPhotoRouteFemale;
                barber.BarberType = (BarberType)selectedBarberType;
                await rest.PutAsync(barber, Common.UpdateBarberURI);
            }
            ClearForm();
            await GetBarbers();
        }


        private async void deleteBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var id = int.Parse(((Button)sender).Tag.ToString());
            await rest.DeleteAsync(string.Format(Common.DeleteBarberURI, id));
            await GetBarbers();
        }

        private async void NextTicket_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var barber = (BarberDTO)((Button)sender).Tag;
            var newTicket = await restTicket.GetAsync(string.Format(Common.GetNextTicketURI, barber.Id, barber.CurrentTicketId));
            if(TVDisplay != null)
            {
                if (newTicket != null)
                {
                    TVDisplay.ShowNewTicket(newTicket.Code, barber.Name);
                }
                else
                {
                    TVDisplay.ShowNewTicket(string.Empty, barber.Name);
                }
            }
            
            await GetBarbers();
        }
    }
}
