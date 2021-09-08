using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.CQRS.Handlers.Authentication
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Response<Tuple<AuthenticationResponse, RefreshToken>>>
    {
        private readonly IAdminService _adminService;

        public RefreshTokenHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<Response<Tuple<AuthenticationResponse, RefreshToken>>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var data = await _adminService.RefreshAdminToken(request.Token);

            return new Response<Tuple<AuthenticationResponse, RefreshToken>>(data);
        }
    }
}