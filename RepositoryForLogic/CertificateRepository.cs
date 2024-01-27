using Dapper;
using Domain;
using Domain.Models;
using InterfacesToContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryForLogic
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly EmployeeDbContext _context;
        public CertificateRepository(EmployeeDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> Add(Certificate entity)
        {
            var query = "INSERT INTO Certificate (CertificateName,CertificateDate,EmployeeId) VALUES (@CertificateName,@CertificateDate,@EmployeeId)";
            var perameters = new DynamicParameters();
            perameters.Add("CertificateName", entity.CertificateName, DbType.String);
            perameters.Add("CertificateDate", entity.CertificateDate, DbType.Date);
            perameters.Add("EmployeeId", entity.EmployeeId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, perameters);
                return true;
            }
        }

        public async Task<IEnumerable<Certificate>> All()
        {
            var query = "SELECT * FROM Certificate";
            using (var connection = _context.CreateConnection())
            {
                var certificate = await connection.QueryAsync<Certificate>(query);
                return certificate.ToList();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Certificate WHERE CertificateId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var certificate = await connection.ExecuteAsync(query, new { id });
                return true;
            }
        }

        public async Task<Certificate?> GetById(int id)
        {
            var query = "SELECT * FROM Certificate WHERE CertificateId=@Id";
            using (var connection = _context.CreateConnection())
            {
                var certificate = await connection.QueryAsync<Certificate>(query, new { id });
                return certificate.FirstOrDefault();
            }
        }

        public async Task<bool> Update(int id, Certificate entity)
        {
            var query = "UPDATE Certificate SET CertificateName=@CertificateName, CertificateDate=@CertificateDate, EmployeeId=@EmployeeId WHERE CertificateId=@Id";
            var perameters = new DynamicParameters();
            perameters.Add("Id", id, DbType.Int32);
            perameters.Add("CertificateName", entity.CertificateName, DbType.String);
            perameters.Add("CertificateDate", entity.CertificateDate, DbType.Date);
            perameters.Add("EmployeeId", entity.EmployeeId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, perameters);
                return true;
            }
        }
    }
}
