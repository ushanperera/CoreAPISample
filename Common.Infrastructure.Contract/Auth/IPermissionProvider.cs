using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Contract.Auth
{
    public interface IPermissionProvider
    {
        bool HasPermission(string permission, bool isFlex);
    }
}
