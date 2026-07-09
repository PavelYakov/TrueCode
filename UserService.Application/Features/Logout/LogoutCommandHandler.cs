using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        public Task<bool> Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
