
namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {

        private readonly ITicketBookingRepository _ticket;

        
        public TicketBookingRequestProcessor(ITicketBookingRepository ticketBookingRepository)
        {
            _ticket = ticketBookingRepository;
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _ticket.Save(Helper.Create<TicketBooking>(request));
            return Helper.Create<TicketBookingResponse>(request);

        }



        //refractor för att returnera en ny TicketBookingResponse
        //   return new TicketBookingResponse
        //   {
        //       FirstName = request.FirstName,
        //       LastName = request.LastName,
        //       Email = request.Email
        //   };

    }
    
}
