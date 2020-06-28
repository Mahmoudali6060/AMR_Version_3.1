using AMR_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMR_Server.Domain.ComplexTypes
{
    public class SP_GetAllMeter_Result 
    {
        public string MeterId { get; set; }
        public string HCN { get; set; }
        public string BadgeNo { get; set; }
        public short? ModuleId { get; set; }
        public decimal? GroupId { get; set; }
        public short? VendorId { get; set; }
        public string SerialNo { get; set; }
        public short? ModelId { get; set; }

    }
}
