using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.UnitTests.Helpers
{
    /// <summary>
    /// https://isaacmartinezblog.wordpress.com/2017/11/29/unit-testing-controllers-with-authorize-attribute-in-asp-net-core/
    /// </summary>
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public ClaimsPrincipal User => _context.HttpContext?.User;
    }
}
