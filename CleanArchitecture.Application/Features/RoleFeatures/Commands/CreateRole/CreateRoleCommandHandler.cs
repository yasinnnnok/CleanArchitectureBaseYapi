
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;


public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, MessageResponse>
{
    private readonly IRoleServices _roleService;

    public CreateRoleCommandHandler(IRoleServices roleService)
    {
        _roleService = roleService;
    }

    public async Task<MessageResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleService.CreateAsync(request);
        return new("Rol kaydı başarıyla tamamlandı!");
    }
}
