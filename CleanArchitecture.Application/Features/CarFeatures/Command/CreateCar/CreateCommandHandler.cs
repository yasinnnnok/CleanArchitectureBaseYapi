using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Command.CreateCar;

public sealed class CreateCommandHandler : IRequestHandler<CreateCarCommand, MessageResponse>
{
    private readonly ICarService _carService;

    public CreateCommandHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<MessageResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        await _carService.CreateAsync(request, cancellationToken);
        return new("Araç başarılı bir şekilde üretildi.");
    }
}
