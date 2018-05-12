using System.Web.Http;
using La27Barberia.DB.DA;
using La27Barberia.Core.DTO;
using System;

namespace La27Barberia.Server.Controllers
{
    public class ClientController : ApiController
    {
        private ClientDA clientDA;

        [HttpPost]
        public IHttpActionResult CreateClient([FromBody]ClientDTO newClient)
        {
            try
            {
                clientDA = new ClientDA();
                clientDA.CreateClient(newClient);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateClient([FromBody]ClientDTO client)
        {
            try
            {
                clientDA = new ClientDA();
                clientDA.CreateClient(client);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        public IHttpActionResult GetClientByIdentification(string identification)
        {
            try
            {
                clientDA = new ClientDA();
                return Ok(clientDA.GetClientByIdentification(identification));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult GetClients()
        {
            try
            {
                clientDA = new ClientDA();

                return Ok(clientDA.GetClients());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
