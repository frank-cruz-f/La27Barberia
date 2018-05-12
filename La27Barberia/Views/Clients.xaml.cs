using La27Barberia.Core.DTO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace La27Barberia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Clients : Page
    {
        public ObservableCollection<ClientDTO> ClientList;
        RestClient<ClientDTO> rest;

        public Clients()
        {
            this.InitializeComponent();
            ClientList = new ObservableCollection<ClientDTO>();
            rest = new RestClient<ClientDTO>();
            GetClients();
        }

        private async void GetClients()
        {
            var clients = await rest.GetListAsync(Common.GetClientsURI);
            ClientList.Clear();
            if (clients != null)
            {
                foreach (var item in clients)
                {
                    ClientList.Add(item);
                }
            }
        }

        private async void CreateClientBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var createDialog = await CreateClientDialog.ShowAsync();
            if(createDialog == ContentDialogResult.Primary)
            {
                var newClient = new ClientDTO();
                newClient.Name = nameTxb.Text;
                newClient.LastName = lastNameTxb.Text;
                newClient.Identification = identificationTxb.Text;
                newClient.Birthday = birthDayDt.Date.DateTime;
                newClient.LastVisit = DateTime.Now;
                newClient.Email = mailTxb.Text;
                await rest.PostAsync(newClient, Common.CreateClientURI);
            }
            GetClients();
            ClearForm();
        }

        private async void editBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var client = ClientList.FirstOrDefault(b => b.Id == int.Parse(((Button)sender).Tag.ToString()));
            nameTxb.Text = client.Name;
            lastNameTxb.Text = client.LastName;
            identificationTxb.Text = client.Name;
            birthDayDt.Date = client.Birthday;
            mailTxb.Text = client.Email;
            var createDialog = await CreateClientDialog.ShowAsync();
            if (createDialog == ContentDialogResult.Primary)
            {
                client.Name = nameTxb.Text;
                client.LastName = lastNameTxb.Text;
                client.Identification = identificationTxb.Text;
                client.Birthday = birthDayDt.Date.DateTime;
                client.Email = mailTxb.Text;
                await rest.PutAsync(client, Common.UpdateClientURI);
            }
            ClearForm();
            GetClients();
        }

        private void deleteBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GetClients();
        }

        private void ClearForm()
        {
            nameTxb.Text = string.Empty;
            lastNameTxb.Text = string.Empty;
            identificationTxb.Text = string.Empty;
            mailTxb.Text = string.Empty;
            birthDayDt.Date = DateTime.Now;
        }
    }
}
