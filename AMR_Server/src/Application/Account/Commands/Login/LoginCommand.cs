using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Account.Commands.Login
{
    public class GetUserPrivilegesCommand : IRequest<GetUserPrivilegesCommand>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }

    }

    public class LoginCommandHandler : IRequestHandler<GetUserPrivilegesCommand, GetUserPrivilegesCommand>
    {
        private readonly IAmrDbContext _context;
        private readonly IIdentityService _identityService;
        public LoginCommandHandler(IAmrDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<GetUserPrivilegesCommand> Handle(GetUserPrivilegesCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.AuthenticateAD(request.UserName, request.Password);
            request.Token = user.Token;
            request.UserId = user.UserId;
            return request;
        }
    }
}
