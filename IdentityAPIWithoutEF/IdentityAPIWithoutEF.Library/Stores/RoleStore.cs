using IdentityAPIWithoutEF.Library.Internal;
using IdentityAPIWithoutEF.Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Library.Stores
{
    public class RoleStore : IRoleStore
    {
        private readonly ISqlDataAccess _sql;

        public RoleStore(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<RoleModel>> GetRolesByUserName(string userName)
        {
            var output = await _sql.QueryAsync<RoleModel, dynamic>("dbo.spRoles_GetByUserName", new { userName });

            return output;
        }

        public async Task<IdentityResult> CreateAsync(RoleModel role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<RoleModel, dynamic>("dbo.spRoles_Insert", role);

            role.Id = output.ToList().FirstOrDefault().Id;

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(RoleModel role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _sql.QueryAsync<Object, dynamic>("dbo.spRoles_DeleteById", new { Id = role.Id });

            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<RoleModel> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<RoleModel, dynamic>("dbo.spRoles_GetById", new { Id = roleId });

            return output.FirstOrDefault();
        }

        public async Task<RoleModel> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<RoleModel, dynamic>("dbo.spRoles_GetByName", new { NormalizedName = normalizedRoleName });

            return output.FirstOrDefault();
        }

        public Task<string> GetNormalizedRoleNameAsync(RoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(RoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(RoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(RoleModel role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetRoleNameAsync(RoleModel role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(RoleModel role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<RoleModel, dynamic>("dbo.spRoles_UpdateById", new { Id = role.Id });

            return IdentityResult.Success;
        }
    }
}
