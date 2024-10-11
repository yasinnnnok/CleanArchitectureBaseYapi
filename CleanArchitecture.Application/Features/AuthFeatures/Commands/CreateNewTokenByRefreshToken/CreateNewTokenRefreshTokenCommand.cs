using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;

public sealed record CreateNewTokenRefreshTokenCommand(
    string UserId,
    string RefreshToken) : IRequest<LoginCommandResponse>;