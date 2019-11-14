using GlobalSolution.Web.Data.Entities;
using GlobalSolution.Web.Models;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool isNew);
        VehicleViewModel ToVehicleViewModel(Vehicle vehicle);

        Task<Order> ToOrderAsync(OrderViewModel model, bool isNew);
        OrderViewModel ToOrderViewModel(Order order);
    }
}
