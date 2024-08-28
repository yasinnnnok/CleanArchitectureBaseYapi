using CleanArchitecture.Application.Features.CarFeatures.Command.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest
{
    public class CarsControllerUnitTest
    {
        [Fact]
        public async void Create_ReturnOkResult_WhenRequestIsValid()
        {
            //Arrange-mediator-tan�mlamalar�n yap�ld��� par�ad�r-Mock sahte mediator kural�m.
            var mediatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new(
                "Toyoat","Corolla",5000);

            MessageResponse response = new("Ara� ba�ar�l� bir �ekilde kaydedildi!");
            CancellationToken cancellationToken = new();

            mediatorMock.Setup(m => m.Send(createCarCommand, cancellationToken)).ReturnsAsync(response);

            CarsController carsController = new(mediatorMock.Object);


            //Act-eylem par�as�d�r.
            var result = await carsController.Create(createCarCommand,cancellationToken);

            //Assert-yap�lacak kontrol par�as�d�r.
            var okResult= Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            Assert.Equal(response, returnValue);
            mediatorMock.Verify(m=>m.Send(createCarCommand,cancellationToken),Times.Once);



        }
    }
}