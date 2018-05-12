using AutoMapper;
using System.Data.Entity.Infrastructure;
using La27Barberia.Core.DTO;
using La27Barberia.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace La27Barberia.DB.DA
{
    public class ClientDA : La27DataAccess
    {
        public ClientDA() : base()
        {

        }

        public void CreateClient(ClientDTO newClient)
        {
            try
            {
                var client = Mapper.Map<Client>(newClient);
                context.Clients.Add(client);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw (ex);
            }
            
        }

        public ClientDTO GetClientByIdentification(string identification)
        {
            try
            {
                return Mapper.Map<ClientDTO>(context.Clients.FirstOrDefault(c => c.Identification.Contains(identification)));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClientDTO> GetClients()
        {
            try
            {
                return Mapper.Map<List<ClientDTO>>(context.Clients);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
