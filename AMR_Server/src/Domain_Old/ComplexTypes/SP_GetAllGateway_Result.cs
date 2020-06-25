using AMR_Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMR_Server.Domain.ComplexTypes
{
    public class SP_GetAllGateway_Result
    {
        public decimal Gateway_Id { get; set; }
        public string Gateway_Code { get; set; }
        public string FTP_Host_Name { get; set; }
        public decimal? Coord_LAT { get; set; }
        public decimal? Coord_LON { get; set; }
        public bool? Is_Fixed { get; set; }
    }
}
