using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesToContract
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Users user);
    }
}
