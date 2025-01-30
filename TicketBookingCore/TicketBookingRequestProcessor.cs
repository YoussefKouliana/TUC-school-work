
namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {

        private readonly ITicketBookingRepository _iTicket;

      

        public TicketBookingRequestProcessor(ITicketBookingRepository ticketBookingRepository)
        {
            _iTicket = ticketBookingRepository;
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _iTicket.Save(new TicketBooking
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                
            }
            );


            //refractor för att returnera en ny TicketBookingResponse
            return new TicketBookingResponse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

        }
    }
}