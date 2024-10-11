using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenRefreshTokenCommandHandler : IRequestHandler<CreateNewTokenRefreshTokenCommand, LoginCommandResponse>
{
    private readonly IAuthService _authService;

    public CreateNewTokenRefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginCommandResponse> Handle(CreateNewTokenRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        LoginCommandResponse response = await _authService.CreateTokenByRefreshTokenAsync(request,cancellationToken);
        return response;
    }
}
