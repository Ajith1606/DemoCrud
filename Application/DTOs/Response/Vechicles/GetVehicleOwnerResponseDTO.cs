using Application.DTOs.Request.Vechicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Vechicles
{
    public class GetVehicleOwnerResponseDTO : CreateVechicleOwnerRequestDTO
    {
        public int Id { get; set; }
        public virtual ICollection<GetVehicleResponseDTO>? Vehicles { get; set; }
    }
}
