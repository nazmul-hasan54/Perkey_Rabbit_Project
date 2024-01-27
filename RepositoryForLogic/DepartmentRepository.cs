using Dapper;
using Domain;
using Domain.Models;
using InterfacesToContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryForLogic
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDbContext _context;
        public DepartmentRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Department entity)
        {
            var query = "INSERT INTO Department (DepartmentName) VALUES (@DepartmentName)";
            var perameters = new DynamicParameters();
            perameters.Add("DepartmentName", entity.DepartmentName, DbType.String);
            using (var connection = _context.CreateConnection()) 
            {
                await connection.ExecuteAsync(query, perameters);
                return true;
            }
        }

        public async Task<IEnumerable<Department>> All()
        {
            var query = "SELECT * FROM Department";
            using (var connection = _context.CreateConnection()) 
            {
                var department = await connection.QueryAsync<Department>(query);
                return department.ToList();
            }
        }

        public async Task<Department?> GetById(int id)
        {
            var query = "SELECT * FROM Department WHERE DepartmentId=@Id";
            using (var connection = _context.CreateConnection()) 
            {
                var department = await connection.QueryAsync<Department>(query, new { id});
                return department.FirstOrDefault();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Department WHERE DepartmentId = @Id";
            using (var connection = _context.CreateConnection()) 
            {
                var department = await connection.ExecuteAsync(query, new { id});
                return true;
            }
        }

        public async Task<bool> Update(int id,Department entity)
        {
            var query = "UPDATE Department SET DepartmentName = @DepartmentName WHERE DepartmentId = @Id";
            var perameters = new DynamicParameters();
            perameters.Add("Id", id, DbType.Int32);
            perameters.Add("DepartmentName", entity.DepartmentName, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,perameters);
                return true;
            }
        }
    }
}
