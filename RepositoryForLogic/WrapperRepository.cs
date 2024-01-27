using Domain;
using InterfacesToContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryForLogic
{
    public class WrapperRepository : IWrapperRepository
    {
        private readonly EmployeeDbContext _context;

        public IDepartmentRepository Departments { get; }

        public IUserRepository Users { get; }

        public ICertificateRepository Certificates { get; }

        public IEmployeeRepository Employees { get; }

        public WrapperRepository(EmployeeDbContext context)
        {
            _context = context;
            Departments = new DepartmentRepository(_context);
            Users = new UserRepository(_context);
            Certificates = new CertificateRepository(_context);
            Employees = new EmployeeRepository(_context);
        }
    }
}
