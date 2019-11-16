using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalSolution.Web.Data;
using GlobalSolution.Web.Data.Entities;

namespace GlobalSolution.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public JobTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<JobType> GetJobTypes()
        {
            return _context.JobTypes.OrderBy(jt => jt.Name);
        }

   
    }
}