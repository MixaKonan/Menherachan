using System;
using MediatR;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.CQRS.Commands.Authentication
{
    public class AuthenticationRequest : IRequest<Response<Tuple<AuthenticationResponse, RefreshToken>>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}