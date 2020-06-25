using AMR_Server.Application.Common.Exceptions;
using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Meters.Commands
{
    public class DeleteMeterCommand : IRequest<bool>
    {
        public string MeterId { get; set; }
    }

    public class DeleteMeterCommandHandler : IRequestHandler<DeleteMeterCommand, bool>
    {
        private readonly IAmrDbContext _context;
        public DeleteMeterCommandHandler(IAmrDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteMeterCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Meter.FindAsync(request.MeterId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Meter), request.MeterId);
            }
            entity.DeleteStaus = true;
            _context.Meter.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
