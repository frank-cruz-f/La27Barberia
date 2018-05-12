using System;
using System.Collections.Generic;

namespace La27Barberia.Core.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public List<TicketDTO> Tickets { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
