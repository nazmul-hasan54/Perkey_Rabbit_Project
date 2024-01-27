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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Employee entity)
        {
            var query = "INSERT INTO Employee (EmployeeName,DepartmentId) VALUES (@EmployeeName,@DepartmentId)";
            var perameters = new DynamicParameters();
            perameters.Add("EmployeeName", entity.EmployeeName, DbType.String);
            perameters.Add("DepartmentId", entity.DepartmentId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, perameters);
                return true;
            }
        }

        public async Task<IEnumerable<Employee>> All()
        {
            var query = "SELECT * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QueryAsync<Employee>(query);
                return employee.ToList();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Employee WHERE EmployeeId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.ExecuteAsync(query, new { id });
                return true;
            }
        }

        public async Task<Employee?> GetById(int id)
        {
            var query = "SELECT * FROM Employee WHERE EmployeeId=@Id";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QueryAsync<Employee>(query, new { id });
                return employee.FirstOrDefault();
            }
        }

        public async Task<bool> Update(int id, Employee entity)
        {
            var query = "UPDATE Employee SET EmployeeName=@EmployeeName, DepartmentId=@DepartmentId WHERE EmployeeId = @Id";
            var perameters = new DynamicParameters();
            perameters.Add("Id", id, DbType.Int32);
            perameters.Add("EmployeeName", entity.EmployeeName, DbType.String);
            perameters.Add("DepartmentId", entity.DepartmentId, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, perameters);
                return true;
            }
        }
    }
}
