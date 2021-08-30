using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.CQRS.Handlers.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, Response<AuthenticationResponse>>
    {
        private readonly IAdminService _adminService;

        public AuthenticationHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var data = await _adminService.AuthenticateAsync(request.Email, request.Password);
            return new Response<AuthenticationResponse>(data);
        }
    }
}