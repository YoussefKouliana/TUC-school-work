using System.ComponentModel.DataAnnotations;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessortests
    {

        private readonly TicketBookingRequestProcessor _processor;
        public TicketBookingRequestProcessortests()
        {
            _processor = new TicketBookingRequestProcessor();
        }
        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {
            //Arrange
            var processor = new TicketBookingRequestProcessor();


            var request = new TicketBookingRequest
            {
                FirstName = "Youssef",
                LastName = "Kouliana",
                Email = "yosep.koliana@gmail.com",
            };

            //Act
            TicketBookingResponse response = _processor.Book(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);

        }
        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            //Arrange
            var processor = new TicketBookingRequestProcessor();
            //Act
            var Exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));
            //Assert
            Assert.Equal("request", Exception.ParamName);
        }
    }
}