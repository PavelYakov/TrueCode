using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces.Services;

namespace UserService.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userId = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(
                        ClaimTypes.NameIdentifier);

                return int.Parse(userId!);
            }
        }
    }
}
