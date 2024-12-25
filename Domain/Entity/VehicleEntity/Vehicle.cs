

namespace Domain.Entity.VehicleEntity
{
    public class Vehicle : BaseClass
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public VehileOwner? VehicleOwner { get; set; }
        public int VehicleOwnerId { get; set; }
        public VehileBrand? VehicleBrand { get; set; }
        public int VehicleBrandId { get; set; }
    }
}
