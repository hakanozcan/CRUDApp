using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApp.API.Data;
using CRUDApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly CrudAppDbContext _crudAppDbContext;

        
        public EmployeesController(CrudAppDbContext crudAppDbContext)
        {
            _crudAppDbContext = crudAppDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _crudAppDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _crudAppDbContext.Employees.AddAsync(employeeRequest);
            await _crudAppDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }
    }
}
