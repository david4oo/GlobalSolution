using GlobalSolution.Web.Data.Entities;
using GlobalSolution.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1128462788", "Walter", "Araujo", "walter-4029@hotmail.com", "3166699037", "Calle 69 #40-29", "Manager");
            var employee = await CheckUserAsync("1128462799", "Wally", "Muñoz", "waraujo095@gmail.com", "3168440133", "Av Poblado", "Employee");
            var customer1 = await CheckUserAsync("1128462777", "David", "Lopez", "david.4029@hotmail.com", "3122127180", "Calle 110a #30-24", "Customer");



            await CheckManagerAsync(manager);
            await CheckEmployeesAsync(employee);
            await CheckCustomersAsync(customer1);




            await CheckJobsTypesAsync();
            await CheckVehiclesTypesAsync();



            await CheckVehiclesAsync();
            await CheckOrderAsync();





            }




        ////////////////////////////////////////////////////////////////
        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Customer");
        }


        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }



        /////////////////////////////////////////////////////////////////////////

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckCustomersAsync(User user)
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { User = user });
                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckEmployeesAsync(User user)
        {
            if (!_context.Employees.Any())
            {
                _context.Employees.Add(new Employee { User = user });
                await _context.SaveChangesAsync();
            }
        }


        /////////////////////////////////////////////////////////////////////////







        private async Task CheckJobsTypesAsync()
        {
            if (!_context.JobTypes.Any())
            {
                _context.JobTypes.Add(new JobType { Name = "Mecanico" });
                _context.JobTypes.Add(new JobType { Name = "Electrico" });
                _context.JobTypes.Add(new JobType { Name = "Pintura" });
                _context.JobTypes.Add(new JobType { Name = "Limpieza" });
                _context.JobTypes.Add(new JobType { Name = "Latoneria" });
                await _context.SaveChangesAsync();

            }
        }
        

        private async Task CheckVehiclesTypesAsync()
        {
            if (!_context.VehicleTypes.Any())
            {
                _context.VehicleTypes.Add(new VehicleType { Name = "Automovil" });
                _context.VehicleTypes.Add(new VehicleType { Name = "Motocicleta" });
                _context.VehicleTypes.Add(new VehicleType { Name = "Camion" });
                _context.VehicleTypes.Add(new VehicleType { Name = "Bus" });
                _context.VehicleTypes.Add(new VehicleType { Name = "Camioneta" });
                await _context.SaveChangesAsync();

            }
        }




        ///////////////////////////////////////////////////////////////////////////////////////////////



        private async Task CheckVehiclesAsync()
        {
            var employee = _context.Employees.FirstOrDefault();
            var vehicleType = _context.VehicleTypes.FirstOrDefault();

            if (!_context.Vehicles.Any())
            {
                AddVehicle("Blanco", employee, "Chevrolet Spark", "SAT028", vehicleType);
                await _context.SaveChangesAsync();
            }
        }

        private void AddVehicle(
         string color,
         Employee employee,
         string modelo,
         string placa,
         VehicleType vehicleType)
        {
            _context.Vehicles.Add(new Vehicle
            {
                Color = color,
                Employee = employee,
                Modelo = modelo,
                Placa = placa,
                VehicleType = vehicleType,


            });
        }



        private async Task CheckOrderAsync()
        {
            var employee= _context.Employees.FirstOrDefault();
            var customer = _context.Customers.FirstOrDefault();
            var vehicle = _context.Vehicles.FirstOrDefault();
            var jobtype = _context.JobTypes.FirstOrDefault();
            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Order
                {
                    EntryDate1 = DateTime.Today,
                    Customer = customer,
                    Employee = employee,
                    Price = 800000M,
                    Vehicle = vehicle,
                    JobType  = jobtype,
                    Trabajo = "15 Dias",
                    Garantia = "5 meses",                
                    Description = "Fuselaje Mayor Dañado"
                });

                await _context.SaveChangesAsync();
            }
        }


        /*private async Task CheckOrderAsync()
        {
            var employee = _context.Employees.FirstOrDefault();
            var customer = _context.Customers.FirstOrDefault();
            var vehicle = _context.Vehicles.FirstOrDefault();
            var jobtype = _context.JobTypes.FirstOrDefault();


            if (!_context.Orders.Any())
            {

                AddOrder(employee, customer, jobtype, vehicle, "5 meses", "15 dias", 800000M,
                    "El vehiculo presenta un problema del arbol de leva y fuselaje del motor", DateTime.Today);
                await _context.SaveChangesAsync();
            }

        }

        private void AddOrder(
        Employee employee,
        Customer customer,
        JobType jobtype,
        Vehicle vehicle,
        string garantia,
        string trabajo,
        decimal price,
        string descripcion,
        DateTime entrydate1)
        {
            _context.Orders.Add(new Order
            {
                Employee = employee,
                Customer = customer,
                JobType = jobtype,
                Vehicle = vehicle,
                Garantia = garantia,
                Trabajo = trabajo,
                Price = price,
                Description = descripcion,
                EntryDate1 = entrydate1,

            });
        }*/

    }
}

