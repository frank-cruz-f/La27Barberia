using AutoMapper;
using La27Barberia.Core.DTO;
using La27Barberia.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace La27Barberia.DB.DA
{
    public class TicketDA : La27DataAccess
    {
        public List<TicketDTO> getTicketsForBarber(int barberId)
        {
            context = new BarberContext();
            return Mapper.Map<List<TicketDTO>>(context.Tickets.Where(t => t.BarberId == barberId).ToList());
        }

        public void CreateTicket(TicketDTO newTicket)
        {
            try
            {
                context = new BarberContext();
                var ticket = Mapper.Map<Ticket>(newTicket);
                context.Tickets.Add(ticket);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
