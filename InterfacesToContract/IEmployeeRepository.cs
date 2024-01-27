﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesToContract
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
    }
}
