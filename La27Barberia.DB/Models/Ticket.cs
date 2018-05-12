using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace La27Barberia.DB.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public int ClientId { get; set; }
        public int BarberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int EstimatedMinutes { get; set; }
        public bool IsActive { get; set; }
        public bool IsInQueue { get; set; }
        public bool HasStarted { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        [ForeignKey("BarberId")]
        public virtual Barber Barber { get; set; }
    }
}
