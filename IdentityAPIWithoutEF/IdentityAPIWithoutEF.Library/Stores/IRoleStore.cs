using IdentityAPIWithoutEF.Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Library.Stores
{
    public interface IRoleStore : IRoleStore<RoleModel>
    {
        Task<IEnumerable<RoleModel>> GetRolesByUserName(string userName);
    }
}
