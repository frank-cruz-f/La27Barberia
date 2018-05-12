using La27Barberia.Core.DTO;
using La27Barberia.DB.DA;
using System.Collections.Generic;
using System.Web.Http;

namespace La27Barberia.Server.Controllers
{
    public class TicketController : ApiController
    {
        private TicketDA ticketDA;

        public TicketController()
            : base()
        {
            ticketDA = new TicketDA();
        }

        // GET: api/Ticket/5
        [HttpGet]
        public IHttpActionResult GetTicketsForBarber(int id)
        {
            return Ok(ticketDA.getTicketsForBarber(id));
        }

        // POST: api/Ticket
        [HttpPost]
        public IHttpActionResult CreateTicket([FromBody]TicketDTO newTicket)
        {
            try
            {
                ticketDA.CreateTicket(newTicket);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return InternalServerError(ex);
            }
            
        }
    }
}
