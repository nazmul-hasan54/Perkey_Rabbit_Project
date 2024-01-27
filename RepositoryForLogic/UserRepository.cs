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
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeDbContext _context;
        public UserRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> All()
        {
            var query = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection()) 
            {
                var result = await connection.QueryAsync<Users>(query);
                return result.ToList();
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Users?> GetById(int id)
        {
            var query = "SELECT * FROM Users WHERE UsersId=@Id";
            using (var connection = _context.CreateConnection()) 
            {
                var result = await connection.QueryAsync<Users>(query, new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<Users?> GetUserByEmail(string email)
        {
            var query = "SELECT * FROM Users WHERE Email=@Email";
            using (var connection = _context.CreateConnection()) 
            {
                var result = await connection.QueryAsync<Users>(query, new { email });
                return result.FirstOrDefault();
            }
        }

        public async Task<bool> Update(int id, Users entity)
        {
            var query = "UPDATE Users SET UsersName=@UsersName, Email=@Email, Password=@Password, Otp=@Otp, Role=@Role WHERE UsersId=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("UsersName", entity.UsersName, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("Password", entity.Password, DbType.String);
            parameters.Add("Otp", entity.Otp, DbType.Int32);
            parameters.Add("Role", entity.Role, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return true;
            }
        }

        public async Task<bool> Add(Users entity)
        {
            var query = "INSERT INTO Users (UsersName, Email, Password, Otp, Role) VALUES (@UsersName, @Email, @Password, @Otp, @Role)";
            var parameters = new DynamicParameters();
            parameters.Add("UsersName", entity.UsersName, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("Password", entity.Password, DbType.String);
            parameters.Add("Otp", entity.Otp, DbType.Int32);
            parameters.Add("Role", entity.Role, DbType.String);
            using (var connection = _context.CreateConnection()) 
            {
                await connection.ExecuteAsync(query, parameters);
                return true;
            }
        }
    }
}
