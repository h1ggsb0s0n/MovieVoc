using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.UnitTests.Helpers
{
    /// <summary>
    /// Verwendet um den User zu mocken.
    /// </summary>
    public interface IUserResolverService
    {
        ClaimsPrincipal User { get; }
    }
}
