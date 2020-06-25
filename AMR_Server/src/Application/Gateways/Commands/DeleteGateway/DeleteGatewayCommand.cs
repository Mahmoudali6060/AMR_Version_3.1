using AMR_Server.Application.Common.Exceptions;
using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Gateways.Commands
{
    public class DeleteGatewayCommand : IRequest<bool>
    {
        public string GatewayId { get; set; }
    }

    public class DeleteGatewayCommandHandler : IRequestHandler<DeleteGatewayCommand, bool>
    {
        private readonly IAmrDbContext _context;
        public DeleteGatewayCommandHandler(IAmrDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteGatewayCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Gateway.FindAsync(request.GatewayId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Gateway), request.GatewayId);
            }
            entity.DeleteStatus = true;
            _context.Gateway.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
