using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entites;

namespace UserService.Application.Interfaces.Services
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
