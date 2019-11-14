using GlobalSolution.Web.Data;
using GlobalSolution.Web.Data.Entities;
using GlobalSolution.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {

        private readonly DataContext _dataContext;
        private readonly IComboHelper _comboHelper;

        public ConverterHelper(
            DataContext dataContext,
            IComboHelper comboHelper)
        {
            _dataContext = dataContext;
            _comboHelper = comboHelper;
        }


        public async Task<Order> ToOrderAsync(OrderViewModel model, bool isNew)
        {
            return new Order
            {

                Employee = await _dataContext.Employees.FindAsync(model.EmployeeId),
                Customer = await _dataContext.Customers.FindAsync(model.CustomerId),
                JobType = await _dataContext.JobTypes.FindAsync(model.JobTypeId),
                Vehicle = await _dataContext.Vehicles.FindAsync(model.VehicleId),
                Garantia = model.Garantia,
                Trabajo = model.Trabajo,
                Price = model.Price,
                EntryDate1 = model.EntryDate1.ToUniversalTime(),
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
            };

        }











        public async Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool isNew)
        {
            return new Vehicle
            {
                Color = model.Color,
                Employee = await _dataContext.Employees.FindAsync(model.EmployeeId),
                Id = isNew ? 0 : model.Id,
                Modelo = model.Modelo,
                Orders = isNew ? new List<Order>() : model.Orders,
                Placa = model.Placa,
                VehiclePhotos = isNew ? new List<VehiclePhoto>() : model.VehiclePhotos,
                VehicleType = await _dataContext.VehicleTypes.FindAsync(model.VehicleTypeId),

            };
        }





        public VehicleViewModel ToVehicleViewModel(Vehicle vehicle)
        {
            return new VehicleViewModel
            {
                Color = vehicle.Color,
                Employee = vehicle.Employee,
                Id = vehicle.Id,
                Modelo = vehicle.Modelo,
                Orders = vehicle.Orders,
                Placa = vehicle.Placa,
                VehiclePhotos = vehicle.VehiclePhotos,
                VehicleType = vehicle.VehicleType,
                EmployeeId = vehicle.Employee.Id,
                VehicleTypeId = vehicle.VehicleType.Id,
                VehicleTypes = _comboHelper.GetComboVehicleTypes()

            };
        }
    }
}

