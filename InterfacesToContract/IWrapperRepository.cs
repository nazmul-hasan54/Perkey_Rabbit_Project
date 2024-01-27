﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesToContract
{
    public interface IWrapperRepository
    {
        IUserRepository Users { get; }
        IDepartmentRepository Departments { get; }
        ICertificateRepository Certificates { get; }
        IEmployeeRepository Employees { get; }
    }
}
