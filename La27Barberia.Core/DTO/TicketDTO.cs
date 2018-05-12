using System;

namespace La27Barberia.Core.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ClientId { get; set; }
        public int BarberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int EstimatedMinutes { get; set; }
        public bool IsActive { get; set; }
        public bool HasStarted { get; set; }
    }
}
