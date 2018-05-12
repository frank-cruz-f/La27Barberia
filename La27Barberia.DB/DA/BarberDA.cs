using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using La27Barberia.Core.DTO;
using La27Barberia.Core.Enum;
using La27Barberia.DB.Models;

namespace La27Barberia.DB.DA
{
    public class BarberDA : La27DataAccess
    {

        public BarberDA(): base()
        {

        }

        public void CreateBarber(BarberDTO newBarber)
        {
            var barber = Mapper.Map<Barber>(newBarber);
            context.Barbers.Add(barber);
            context.SaveChanges();
        }

        public List<BarberDTO> GetActiveBarbers()
        {
            var barbersEntities = context.Barbers.Where(b => b.IsActive);
            var barbersList = barbersEntities.Include(b => b.Tickets).ToList();

            var barbersResult = Mapper.Map<List<BarberDTO>>(barbersList);
            foreach (var barber in barbersResult)
            {
                barber.WaitEstimatedMinutes = 0;
                barber.Tickets = barber.Tickets.Where(t => t.IsActive && t.CreateTime < DateTime.Today.AddDays(1)).OrderBy(t => t.CreateTime).ToList();
                foreach (var ticket in barber.Tickets)
                {
                    if (ticket.HasStarted)
                    {
                        TimeSpan difference = DateTime.Now - ticket.StartTime;
                        barber.WaitEstimatedMinutes += ticket.EstimatedMinutes - Convert.ToInt32(difference.TotalMinutes);
                    }
                    else
                    {
                        barber.WaitEstimatedMinutes += ticket.EstimatedMinutes;
                    }
                }
            }
            return barbersResult;
        }

        public void UpdateBarber(BarberDTO barber)
        {
            var entity = context.Barbers.Find(barber.Id);
            context.Entry(entity).CurrentValues.SetValues(barber);
            context.SaveChanges();
        }

        public List<BarberDTO> GetBarbers()
        {
            var barbers = context.Barbers.Include(b => b.Tickets).ToList();
            foreach (var barber in barbers)
            {
                barber.Tickets = barber.Tickets.Where(t => t.IsActive).ToList();
            }
            return Mapper.Map<List<BarberDTO>>(barbers);
        }

        public TicketDTO GetNextTicket(int barberId, int currentTicketId)
        {
            var barberEntity = context.Barbers.Include(t => t.Tickets).FirstOrDefault(b => b.Id == barberId);
            var currentTicket = barberEntity.Tickets.FirstOrDefault(t => t.IsActive && t.HasStarted);
            barberEntity.Tickets = barberEntity.Tickets.OrderBy(t => t.CreateTime).ToList();
            var firstTicket = barberEntity.Tickets?.FirstOrDefault(t => t.IsActive && !t.HasStarted);
            if (barberEntity.Tickets != null)
            {
                if (currentTicket != null && currentTicket.Id != 0)
                {
                    var activeTicket = context.Tickets.Find(currentTicket.Id);
                    activeTicket.IsActive = false;
                    activeTicket.HasStarted = false;
                }

                if (firstTicket != null)
                {
                    var newCurrent = context.Tickets.Find(firstTicket.Id);
                    firstTicket.IsActive = true;
                    firstTicket.StartTime = DateTime.Now;
                    firstTicket.HasStarted = true;
                }

                context.SaveChanges();
            }
            
            return Mapper.Map<TicketDTO>(firstTicket);

        }

        public void DeleteBarber(int id)
        {
            var barber = context.Barbers.Find(id);
            context.Barbers.Remove(barber);
            context.SaveChanges();
        }

        public List<BarberDTO> GetBarbersForType(int barberType)
        {
            var barbersEntities = context.Barbers.Where(b => b.IsActive == true && b.BarberType == (BarberType)barberType).Include(b => b.Tickets).ToList();
            var barbersResult = Mapper.Map<List<BarberDTO>>(barbersEntities);
            foreach (var barber in barbersResult)
            {
                foreach (var ticket in barber.Tickets)
                {
                    if (ticket.HasStarted)
                    {
                        TimeSpan difference = DateTime.Now - ticket.StartTime;
                        barber.WaitEstimatedMinutes += ticket.EstimatedMinutes - Convert.ToInt32(difference.TotalMinutes);
                    }
                    else
                    {
                        barber.WaitEstimatedMinutes += ticket.EstimatedMinutes;
                    }
                }
            }
            return barbersResult;
        }
    }
}
