using La27Barberia.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel;
namespace La27Barberia.Core.DTO
{
    public class BarberDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int waitEstimatedMinutes;
        private List<TicketDTO> tickets;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhotoRoute { get; set; }
        public bool IsActive { get; set; }
        public bool HasStarted { get; set; }
        public int CurrentTicketId { get; set; }
        public int WaitEstimatedMinutes
        {
            get
            {
                return waitEstimatedMinutes;
            }
            set
            {
                waitEstimatedMinutes = value;
                OnPropertyChanged("WaitEstimatedMinutes");
            }
        }
        public BarberType BarberType { get; set; }

        public List<TicketDTO> Tickets {
            get {
                return tickets;
            }
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
