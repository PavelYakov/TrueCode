using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Login
{
    public record LoginCommand(string Name, string Password) : IRequest<string>;
}
