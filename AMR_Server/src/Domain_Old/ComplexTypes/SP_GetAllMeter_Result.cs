using AMR_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMR_Server.Domain.ComplexTypes
{
    public class SP_GetAllMeter_Result 
    {
        public string Meter_Id { get; set; }
        public string HCN { get; set; }
        public string Badge_No { get; set; }
        public short? Module_Id { get; set; }
    }
}
