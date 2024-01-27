using Domain.Models;
using InterfacesToContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IWrapperRepository _wrapperRepo;
        public CertificatesController(IWrapperRepository wrapperRepo)
        {
            _wrapperRepo = wrapperRepo;
        }

        [HttpGet("get-all-certificate")]
        public async Task<IActionResult> GetAllCertificates()
        {
            var certificate = await _wrapperRepo.Certificates.All();
            return Ok(certificate);
        }

        [HttpGet("get-certificate-by-id")]
        public async Task<IActionResult> GetCertificateByid(int id)
        {
            var certificate = await _wrapperRepo.Certificates.GetById(id);
            return Ok(certificate);
        }

        [HttpPost("add-certificate")]
        public async Task<IActionResult> SaveCertificate(Certificate certificate)
        {
            try
            {
                var createCertificate = await _wrapperRepo.Certificates.Add(certificate);
                return Ok(createCertificate);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-certificate")]
        public async Task<IActionResult> UpdateCertificate(int id, Certificate certificate)
        {
            try
            {
                var certId = await _wrapperRepo.Certificates.GetById(id);
                if (certId == null)
                    return NotFound();
                await _wrapperRepo.Certificates.Update(id, certificate);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-certificate")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var certificate = await _wrapperRepo.Certificates.Delete(id);
            return Ok(certificate);
        }
    }
}
