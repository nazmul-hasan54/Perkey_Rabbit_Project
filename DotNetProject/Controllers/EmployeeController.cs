using Domain.Models;
using InterfacesToContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IWrapperRepository _wrapperRepo;
        public EmployeeController(IWrapperRepository wrapperRepo)
        {
            _wrapperRepo = wrapperRepo;
        }

        [HttpGet("get-all-employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employee = await _wrapperRepo.Employees.All();
            return Ok(employee);
        }

        [HttpGet("get-employee-by-id")]
        public async Task<IActionResult> GetEmployeeByid(int id)
        {
            var employee = await _wrapperRepo.Employees.GetById(id);
            return Ok(employee);
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> SaveEmployee(Employee employee)
        {
            try
            {
                var createEmployee = await _wrapperRepo.Employees.Add(employee);
                return Ok(createEmployee);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                var empId = await _wrapperRepo.Employees.GetById(id);
                if (empId == null)
                    return NotFound();
                await _wrapperRepo.Employees.Update(id, employee);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-employee")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var employee = await _wrapperRepo.Employees.Delete(id);
            return Ok(employee);
        }
    }
}
