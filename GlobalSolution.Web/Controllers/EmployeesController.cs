using GlobalSolution.Web.Data;
using GlobalSolution.Web.Data.Entities;
using GlobalSolution.Web.Helpers;
using GlobalSolution.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Controllers
{

    [Authorize(Roles = "Manager")]
    public class EmployeesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public EmployeesController(
            DataContext dataContext,
            IUserHelper userHelper,
            IComboHelper comboHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)

        {
            _dataContext = dataContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
        }




        public IActionResult Index()
        {
            return View(_dataContext.Employees
                .Include(e => e.User)
                .Include(e => e.Vehicles)
                .Include(e => e.Orders));
        }





        ////CREAR NUEVO EMPLEADO
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user != null)
                {
                    var employee = new Employee
                    {
                        Orders = new List<Order>(),
                        Vehicles = new List<Vehicle>(),
                        User = user

                    };

                    _dataContext.Employees.Add(employee);
                    await _dataContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "User with this email already exist!!");
            }
            return View(model);
        }

        private async Task<User> CreateUserAsync(AddUserViewModel model)
        {
            var user = new User
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username

            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userHelper.GetUserByEmailAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, "Employee");
                return user;
            }
            return null;
        }


        ///DETALLES DE EMPLEADO
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _dataContext.Employees
                .Include(e => e.User)
                .Include(e => e.Vehicles)
                .ThenInclude(v => v.VehiclePhotos)
                .Include(e => e.Orders)
                .ThenInclude(o => o.Customer)
                .ThenInclude(c => c.User)

                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }









        public async Task<IActionResult> EditVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _dataContext.Vehicles

               .Include(v => v.Employee)
               .Include(v => v.VehicleType)
               .FirstOrDefaultAsync(v => v.Id == id);


            if (vehicle == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToVehicleViewModel(vehicle);

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditVehicle(VehicleViewModel model)
        {



            if (ModelState.IsValid)
            {
                var vehicle = await _converterHelper.ToVehicleAsync(model, false);
                _dataContext.Vehicles.Update(vehicle);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction($"Details/{model.EmployeeId}");
            }

            return View(model);
        }


        public async Task<IActionResult> DetailsVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _dataContext.Vehicles
                .Include(e => e.Employee)
                .ThenInclude(e => e.User)
                .Include(e => e.Orders)
                .ThenInclude(or => or.Customer)
                .ThenInclude(c => c.User)
                .Include(e => e.VehicleType)
                .Include(v => v.VehiclePhotos)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }



        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _dataContext.Vehicles.FindAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            var model = new VehiclePhotoViewModel
            {
                Id = vehicle.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(VehiclePhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }

                var vehiclePhoto = new VehiclePhoto
                {
                    ImageUrl = path,
                    Vehicle = await _dataContext.Vehicles.FindAsync(model.Id)
                };

                _dataContext.VehiclePhotos.Add(vehiclePhoto);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsVehicle)}/{model.Id}");
            }

            return View(model);
        }





        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _dataContext.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = employee.User.Address,
                Document = employee.User.Document,
                FirstName = employee.User.FirstName,
                Id = employee.Id,
                LastName = employee.User.LastName,
                PhoneNumber = employee.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _dataContext.Employees
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                employee.User.Document = model.Document;
                employee.User.FirstName = model.FirstName;
                employee.User.LastName = model.LastName;
                employee.User.Address = model.Address;
                employee.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(employee.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }








        ///AGREGAR VEHICULO
        public async Task<IActionResult> AddVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new VehicleViewModel
            {
                EmployeeId = employee.Id,
                VehicleTypes = _comboHelper.GetComboVehicleTypes()

            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel model)
        {


            if (ModelState.IsValid)
            {
                var vehicle = await _converterHelper.ToVehicleAsync(model, true);
                _dataContext.Vehicles.Add(vehicle);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction($"Details/{model.EmployeeId}");
            }

            return View(model);
        }



        //////////////////////////////////////////////////////////
        public async Task<IActionResult> EditOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dataContext.Orders
                .Include(v => v.Employee)
                .Include(v => v.Customer)
                .Include(v => v.Vehicle)
                .Include(v => v.JobType)
                .FirstOrDefaultAsync(v => v.Id == id.Value);
            if (order == null)
            {
                return NotFound();
            }


            return View(_converterHelper.ToOrderViewModel(order));
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = await _converterHelper.ToOrderAsync(model, false);
                _dataContext.Orders.Update(order);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(($"DetailsVehicle/{model.VehicleId}"));
            }
            return View(model);
        }
        ///////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> AddOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _dataContext.Vehicles
                .Include(v => v.Employee)
                .FirstOrDefaultAsync(v => v.Id == id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            var model = new OrderViewModel
            {
                EmployeeId = vehicle.Employee.Id,
                VehicleId = vehicle.Id,
                Customers = _comboHelper.GetComboCustomers(),
                JobTypes = _comboHelper.GetComboJobTypes(),

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = await _converterHelper.ToOrderAsync(model, true);
                _dataContext.Orders.Add(order);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(($"DetailsVehicle/{model.VehicleId}"));
            }
            model.Customers = _comboHelper.GetComboCustomers();
            model.Customers = _comboHelper.GetComboJobTypes();
            return View(model);
        }


        ///////////////////////////////////////////////////////////////////////



        public async Task<IActionResult> DetailsOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dataContext.Orders
                .Include(o => o.Employee)
                .ThenInclude(e => e.User)
                .Include(o => o.Customer )
                .ThenInclude(e => e.User)
                .Include(o => o.Vehicle)
                .ThenInclude(v => v.VehicleType)
                .FirstOrDefaultAsync(vp => vp.Id == id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }




        ////////////////////////////////////////////////////////////////////////
























    }
}






