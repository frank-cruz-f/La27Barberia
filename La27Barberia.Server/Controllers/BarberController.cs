using La27Barberia.Core.DTO;
using La27Barberia.DB.DA;
using System;
using System.Web.Http;

namespace La27Barberia.Server.Controllers
{
    public class BarberController : ApiController
    {
        private BarberDA barberDA;

        [HttpPost]
        public IHttpActionResult CreateBarber([FromBody]BarberDTO newBarber)
        {
            barberDA = new BarberDA();
            barberDA.CreateBarber(newBarber);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateBarber([FromBody]BarberDTO barber)
        {
            barberDA = new BarberDA();
            barberDA.UpdateBarber(barber);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetActiveBarberList()
        {
            try
            {
                barberDA = new BarberDA();
                return Ok(barberDA.GetActiveBarbers());
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult GetBarbers()
        {
            try
            {
                barberDA = new BarberDA();
                return Ok(barberDA.GetBarbers());
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult GetBarbersForType([FromUri]int barberType)
        {
            barberDA = new BarberDA();
            return Ok(barberDA.GetBarbersForType(barberType));
        }
        

        [HttpDelete]
        public IHttpActionResult DeleteBarber([FromUri]int id)
        {
            barberDA = new BarberDA();
            barberDA.DeleteBarber(id);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetNextTicket([FromUri]int barberId, [FromUri]int currentTicketId)
        {
            barberDA = new BarberDA();
            var ticketResult = barberDA.GetNextTicket(barberId, currentTicketId);
            return Ok(ticketResult);
        }
    }
}
