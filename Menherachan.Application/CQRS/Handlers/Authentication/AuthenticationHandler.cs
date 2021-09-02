using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.CQRS.Handlers.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, Response<Tuple<AuthenticationResponse, RefreshToken>>>
    {
        private readonly IAdminService _adminService;

        public AuthenticationHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<Response<Tuple<AuthenticationResponse, RefreshToken>>> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var data = await _adminService.AuthenticateAsync(request.Email, request.Password);
            return new Response<Tuple<AuthenticationResponse, RefreshToken>>(data);
        }
    }
}