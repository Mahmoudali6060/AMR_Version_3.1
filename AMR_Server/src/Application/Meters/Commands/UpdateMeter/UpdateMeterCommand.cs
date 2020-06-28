using AMR_Server.Application.Common.Exceptions;
using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Meters.Commands.UpdateMeter
{
    public partial class UpdateMeterCommand : IRequest<string>
    {
        public string MeterId { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }
        public string SerialNo { get; set; }
    }

    public class UpdateMeterCommandHandler : IRequestHandler<UpdateMeterCommand,string>
    {
        private readonly IAmrDbContext _context;

        public UpdateMeterCommandHandler(IAmrDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateMeterCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Meter.FindAsync(request.MeterId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Meter), request.MeterId);
            }

            entity.VendorId = request.VendorId;
            entity.GroupId = request.GroupId;
            entity.SerialNo = request.SerialNo;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.MeterId;
        }
    }
}
