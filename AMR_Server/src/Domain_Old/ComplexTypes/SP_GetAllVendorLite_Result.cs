using AMR_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMR_Server.Domain.ComplexTypes
{
    public class SP_GetAllVendorLite_Result
    {
        public short Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }

    }
}
