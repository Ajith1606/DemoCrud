using Application.Contracts;
using Application.DTOs.Request.Vechicles;
using Application.DTOs.Response;
using Application.DTOs.Response.Vechicles;
using Domain.Entity.VehicleEntity;
using Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Report
{
    internal class VehicleRepo(AppDbContext context) : IVehicle
    {
        private async Task<Vehicle> FindVehicleByName(string name) =>
            await context.Vehicles.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

        private async Task<VehileBrand> FindVehicleBrandByName(string name) =>
            await context.VehicleBrands.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
       
        private async Task<VehileOwner> FindVehicleOwnerByName(string name) =>
            await context.VehileOwners.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
      
        private async Task<Vehicle> FindVehicleById(int id) =>
            await context.Vehicles.Include(b => b.VehicleBrand).Include(o => o.VehicleOwner)
            .FirstOrDefaultAsync(x => x.Id == id);
       
        private async Task<VehileBrand> FindVehicleBrandById(int id) =>
            await context.VehicleBrands.FirstOrDefaultAsync(x => x.Id == id);
      
        private async Task<VehileOwner> FindVehicleOwnerById(int id) =>
            await context.VehileOwners.FirstOrDefaultAsync(x => x.Id == id);
        
        private async Task SaveChangesAsync() => await context.SaveChangesAsync();
       
        private static GeneralResponse NullResponse(string message) => new(false, message);
       
        private static GeneralResponse AlreadyExistResponse(string message) => new(false, message);
       
        private static GeneralResponse OperationSuccessResponse(string message) => new(true, message);

        //Add
        public async Task<GeneralResponse> AddVehicle(CreateVehicleRequestDTO model)
        {
            if (await FindVehicleByName(model.Name) is not null) return AlreadyExistResponse("Vehicle already exist");
            context.Vehicles.Add(model.Adapt(new Vehicle()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle data saved");
        }

        public async Task<GeneralResponse> AddVehicleBrand(CreateVechicleBrandRequestDTO model)
        {
            if (await FindVehicleBrandByName(model.Name) is not null) return AlreadyExistResponse("Vehicle Brand already exist");
            context.VehicleBrands.Add(model.Adapt(new VehileBrand()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Brand data saved");
        }

        public async Task<GeneralResponse> AddVehicleOwner(CreateVechicleOwnerRequestDTO model)
        {
            if (await FindVehicleOwnerByName(model.Name) is not null) return AlreadyExistResponse("Vehicle Owner already exist");
            context.VehileOwners.Add(model.Adapt(new VehileOwner()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Owner data saved");
        }

        //Delete
        public async Task<GeneralResponse> DeleteVehicle(int id)
        {
            if (await FindVehicleById(id) is null) return NullResponse("Vehicle not found");
            context.Vehicles.Remove(await FindVehicleById(id));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle deleted");
        }

        public async Task<GeneralResponse> DeleteVehicleBrand(int id)
        {
            if (await FindVehicleBrandById(id) is null) return NullResponse("Vehicle Brand not found");
            context.VehicleBrands.Remove(await FindVehicleBrandById(id));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Brand deleted");
        }

        public async Task<GeneralResponse> DeleteVehicleOwner(int id)
        {
            if (await FindVehicleOwnerById(id) is null) return NullResponse("Vehicle Owner not found");
            context.VehileOwners.Remove(await FindVehicleOwnerById(id));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Owner deleted");
        }

        //Get // single  
        public async Task<GetVehicleResponseDTO> GetVehicle(int id) =>
            (await FindVehicleById(id)).Adapt(new GetVehicleResponseDTO());
        public async Task<GetVehicleBrandResponseDTO> GetVehicleBrand(int id) =>
            (await FindVehicleBrandById(id)).Adapt(new GetVehicleBrandResponseDTO());
        public async Task<GetVehicleOwnerResponseDTO> GetVehicleOwner(int id) =>
          (await FindVehicleOwnerById(id)).Adapt(new GetVehicleOwnerResponseDTO());
       
        //Get //List
        public async Task<IEnumerable<GetVehicleResponseDTO>> GetVehicles()
        {
            var data = (await context.Vehicles.Include(b => b.VehicleBrand).Include(o => o.VehicleOwner).ToListAsync());
            return data.Select(vehicle => new GetVehicleResponseDTO
            {
                Id = vehicle.Id,
                Name = vehicle.Name,
                Description = vehicle.Description,
                VehicleOwnerId = vehicle.VehicleOwnerId,
                VehicleBrandId = vehicle.VehicleBrandId,
                VehicleBrand = new GetVehicleBrandResponseDTO()
                {
                    Id = vehicle.VehicleBrand.Id,
                    Name = vehicle.VehicleBrand.Name,
                    Location = vehicle.VehicleBrand.Location
                },
                VehicleOwner = new GetVehicleOwnerResponseDTO
                {
                    Id = vehicle.VehicleOwner.Id,
                    Name = vehicle.VehicleOwner.Name,
                    Address = vehicle.VehicleOwner.Address
                }
            }).ToList();       
        }
        public async Task<IEnumerable<GetVehicleBrandResponseDTO>> GetVehicleBrands() =>
            (await context.VehicleBrands.ToListAsync()).Adapt<List<GetVehicleBrandResponseDTO>>();   
        public async Task<IEnumerable<GetVehicleOwnerResponseDTO>> GetVehicleOwners() =>
            (await context.VehileOwners.ToListAsync()).Adapt<List<GetVehicleOwnerResponseDTO>>();
       
        //Update
        public async Task<GeneralResponse> UpdateVehicle(UpdateVehicleRequestDTO model)
        {
            if (await FindVehicleById(model.Id) is null) return NullResponse("Vehicle not found");
            context.Entry(await FindVehicleById(model.Id)).State = EntityState.Detached;
            context.Vehicles.Update(model.Adapt(new Vehicle()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle data updated");
        }

        public async Task<GeneralResponse> UpdateVehicleBrand(UpdateVehicleBrandRequestDTO model)
        {
            if (await FindVehicleBrandById(model.Id) is null) return NullResponse("Vehicle Brand not found");
            context.Entry(await FindVehicleBrandById(model.Id)).State = EntityState.Detached;
            context.VehicleBrands.Update(model.Adapt(new VehileBrand()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Brand data updated");
        }

        public async Task<GeneralResponse> UpdateVehicleOwner(UpdateVehicleOwnerRequestDTO model)
        {
            if (await FindVehicleOwnerById(model.Id) is null) return NullResponse("Vehicle Owner not found");
            context.Entry(await FindVehicleOwnerById(model.Id)).State = EntityState.Detached;
            context.VehileOwners.Update(model.Adapt(new VehileOwner()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Owner data updated");
        }
    }
}
