using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Account.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterCommand>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommand>
    {
        private readonly IAmrDbContext _context;
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IAmrDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<RegisterCommand> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.Register(request.UserName, request.Password);
            request.Token = user.Token;
            request.UserId = user.UserId;
            return request;
        }
    }
}
