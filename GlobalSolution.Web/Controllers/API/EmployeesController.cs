using GlobalSolution.Common.Models;
using GlobalSolution.Web.Data;
using GlobalSolution.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public EmployeesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


 


        [HttpPost]
        [Route("GetEmployeeByEmail")]
        public async Task<IActionResult> GetEmployeeByEmailAsync(EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employee = await _dataContext.Employees
                .Include(e => e.User)
                .Include(e => e.Vehicles)
                .ThenInclude(v => v.VehicleType)
                .Include(e => e.Vehicles)
                .ThenInclude(v => v.VehiclePhotos)
                .FirstOrDefaultAsync(e => e.User.Email.ToLower() == request.Email.ToLower());

            if (employee == null)
            {
                return NotFound();
            }

            var response = new EmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.User.FirstName,
                LastName = employee.User.LastName,
                Address = employee.User.Address,
                Document = employee.User.Document,
                PhoneNumber = employee.User.PhoneNumber,
                Email = employee.User.Email,
              
                Vehicles = employee.Vehicles?.Select(v => new VehicleResponse
                {
                    Placa = v.Placa,
                    Color = v.Color,
                    Id = v.Id,
                    Modelo = v.Modelo,        
                    Price = v.Price,
                    VehiclePhotos = v.VehiclePhotos?.Select(vp => new VehiclePhotoResponse
                    {
                        Id = vp.Id,
                        ImageUrl = vp.ImageFullPath
                    }).ToList(),
                    VehicleType = v.VehicleType.Name
                }).ToList()
            };

            return Ok(response);
        }        
    }
}
















