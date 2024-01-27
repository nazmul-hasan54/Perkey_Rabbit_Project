using Domain.Models;
using InterfacesToContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryForLogic;

namespace DotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IWrapperRepository _wrapperRepo;
        public DepartmentController(IWrapperRepository wrapperRepo)
        {
            _wrapperRepo = wrapperRepo;
        }

        [HttpGet("get-all-department")]
        public async Task<IActionResult> GetAllDepartments() 
        {
            var department = await _wrapperRepo.Departments.All();
            return Ok(department);
        }

        [HttpGet("get-department-by-id")]
        public async Task<IActionResult> GetDepartmentByid(int id) 
        {
            var department = await _wrapperRepo.Departments.GetById(id);
            return Ok(department);
        }

        [HttpPost("add-department")]
        public async Task<IActionResult> SaveDepartment(Department department)
        {
            try
            {
                var createDepartment = await _wrapperRepo.Departments.Add(department);
                return Ok(createDepartment);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-department")]
        public async Task<IActionResult> UpdateDepartment(int id,Department department)
        {
            try
            {
                var deptId = await _wrapperRepo.Departments.GetById(id);
                if(deptId == null)
                    return NotFound();
                await _wrapperRepo.Departments.Update(id, department);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-department")]
        public async Task<IActionResult> DeleteDepartment(int id) 
        {
            var department = await _wrapperRepo.Departments.Delete(id);
            return Ok(department);
        }
    }
}
