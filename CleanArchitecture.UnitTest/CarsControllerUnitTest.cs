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
            //Arrange-mediator-tanýmlamalarýn yapýldýðý parçadýr-Mock sahte mediator kuralým.
            var mediatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new(
                "Toyoat","Corolla",5000);

            MessageResponse response = new("Araç baþarýlý bir þekilde kaydedildi!");
            CancellationToken cancellationToken = new();

            mediatorMock.Setup(m => m.Send(createCarCommand, cancellationToken)).ReturnsAsync(response);

            CarsController carsController = new(mediatorMock.Object);


            //Act-eylem parçasýdýr.
            var result = await carsController.Create(createCarCommand,cancellationToken);

            //Assert-yapýlacak kontrol parçasýdýr.
            var okResult= Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            Assert.Equal(response, returnValue);
            mediatorMock.Verify(m=>m.Send(createCarCommand,cancellationToken),Times.Once);



        }
    }
}