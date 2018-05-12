using AutoMapper;
using La27Barberia.Core.DTO;
using La27Barberia.DB.Models;
using System.Collections.Generic;

namespace La27Barberia.DB
{
    public static class AutomapperConfig
    {
        public static void MapperConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Barber, BarberDTO>().ReverseMap();
                cfg.CreateMap<Ticket, TicketDTO>().ReverseMap();
                cfg.CreateMap<Client, ClientDTO>().ReverseMap();
            });
        }
    }
}
