using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralWorkPermit.Models;

namespace GeneralWorkPermit.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(User user);
        Task<string> CreateToken();
    }
}
