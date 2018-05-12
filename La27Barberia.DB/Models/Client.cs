using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace La27Barberia.DB.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Identification { get; set; }
        public DateTime Birthday { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public string Email { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
