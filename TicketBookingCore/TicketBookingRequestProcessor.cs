
namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        public TicketBookingRequestProcessor()
        {
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            //refractor för att returnera en ny ticketBookingResponse
            return new TicketBookingResponse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
        }
    }
}