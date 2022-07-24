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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            var employee=
                await _crudAppDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee==null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute]Guid id,Employee updateEmployeeRequest)
        {
             var employee= await _crudAppDbContext.Employees.FindAsync(id);

            if (employee==null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _crudAppDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _crudAppDbContext.Employees.FindAsync(id);

            if (employee==null)
            {
                return NotFound();
            }

            _crudAppDbContext.Employees.Remove(employee);
            await _crudAppDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
