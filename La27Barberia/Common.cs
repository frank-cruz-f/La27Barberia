using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace La27Barberia
{
    public static class Common
    {
        public const string Host = "http://45.224.202.143";
        public const string GetActiveBarbersURI = "/api/Barber/GetActiveBarberList";
        public const string GetBarbersForTypeURI = "/api/Barber/GetBarbersForType?barberType={0}";
        public const string GetBarbersURI = "/api/Barber/GetBarbers";
        public const string CreateBarbersURI = "/api/Barber/CreateBarber";
        public const string UpdateBarberURI = "/api/Barber/UpdateBarber";
        public const string DeleteBarberURI = "/api/Barber/DeleteBarber?id={0}";
        public const string GetNextTicketURI = "/api/Barber/GetNextTicket?barberId={0}&currentTicketId={1}";

        public const string CreateTicketURI = "/api/Ticket/CreateTicket";

        public const string CreateClientURI = "/api/Client/CreateClient";
        public const string UpdateClientURI = "/api/Client/UpdateClient";
        public const string GetClientsURI = "/api/Client/GetClients";

        public const string DefaultPhotoRouteMale = "/Images/male2.png";
        public const string DefaultPhotoRouteFemale = "/Images/Female2.png";
    }
}
