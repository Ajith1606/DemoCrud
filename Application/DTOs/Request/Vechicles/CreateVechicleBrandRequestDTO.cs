using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Vechicles
{
    public class CreateVechicleBrandRequestDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }
    }
}
