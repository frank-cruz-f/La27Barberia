using La27Barberia.Core.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace La27Barberia.DB.Models
{
    public class Barber
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhotoRoute { get; set; }
        public bool IsActive { get; set; }
        public BarberType BarberType { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
    }
}
