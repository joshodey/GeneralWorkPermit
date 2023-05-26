using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResultCompiler.Entity.DTOModels;

namespace ResultCompiler.Core.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDto user);
        Task<string> CreateToken();
    }
}
