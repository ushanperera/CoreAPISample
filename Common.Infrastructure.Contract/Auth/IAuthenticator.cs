using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Contract.Auth
{
    public interface IAuthenticator
    {
        Task<string> AuthenticateAsync(string database, string userName, string password);
    }
}
