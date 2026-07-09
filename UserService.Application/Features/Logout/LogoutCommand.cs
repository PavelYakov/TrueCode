using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Logout
{
    public class LogoutCommand : IRequest<bool>
    {
        // ничего не удаляем на сервере
    }
}
