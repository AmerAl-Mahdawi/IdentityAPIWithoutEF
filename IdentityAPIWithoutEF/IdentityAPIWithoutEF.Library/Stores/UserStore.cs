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
    public class UserStore : IUserStore
    {
        private readonly ISqlDataAccess _sql;

        public UserStore(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<UserModel, dynamic>("dbo.spUsers_Insert", user);
            user.Id = output.ToList().FirstOrDefault().Id;

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _sql.QueryAsync<Object, dynamic>("dbo.spUsers_DeleteById", new { user.Id });

            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<UserModel, dynamic>("dbo.spUsers_GetById", new { Id = userId });

            return output.FirstOrDefault();
        }

        public async Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<UserModel, dynamic>("dbo.spUsers_GetByName", new { normalizedUserName });

            return output.FirstOrDefault();
        }

        public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;

            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _sql.QueryAsync<Object, UserModel>("dbo.spUsers_UpdateById", user);

            return IdentityResult.Success;
        }

        public async Task AddToRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _sql.QueryAsync<Object, dynamic>("dbo.spUserRoles_AddRoleToUser", new { RoleName = roleName, UserId = user.Id });
        }

        public async Task RemoveFromRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _sql.QueryAsync<Object, dynamic>("dbo.spUserRoles_RemoveRoleFromUser", new { RoleName = roleName, UserId = user.Id });
        }

        public async Task<IList<string>> GetRolesAsync(UserModel user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<string, dynamic>("dbo.[spRoles_GetByUserId]", new { UserId = user.Id });

            return output.ToList();
        }

        public async Task<bool> IsInRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<bool, dynamic>("dbo.spUserRoles_IsUserInRole", new { UserId = user.Id, RoleName = roleName });

            return output.FirstOrDefault();
        }

        public async Task<IList<UserModel>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<UserModel, dynamic>("dbo.spUserRoles_GetUsersInRole", new { roleName });

            return output.ToList();
        }

        public Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(UserModel user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(UserModel user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<UserModel> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var output = await _sql.QueryAsync<UserModel, dynamic>("dbo.spUsers_GetByEmail", new { normalizedEmail });

            return output.FirstOrDefault();
        }

        public Task<string> GetNormalizedEmailAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(UserModel user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(UserModel user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(UserModel user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }
    }
}
