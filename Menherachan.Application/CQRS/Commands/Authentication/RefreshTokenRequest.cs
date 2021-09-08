using System;
using MediatR;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.CQRS.Commands.Authentication
{
    public class RefreshTokenRequest : IRequest<Response<Tuple<AuthenticationResponse, RefreshToken>>>
    {
        public RefreshTokenRequest(string token)
        {
            Token = token;
        }
        public string Token { get; set; }
    }
}