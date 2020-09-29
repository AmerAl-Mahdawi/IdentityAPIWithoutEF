using IdentityAPIWithoutEF.Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Library.Stores
{
    public interface IUserStore : IUserStore<UserModel>,
                                IUserEmailStore<UserModel>,
                                IUserRoleStore<UserModel>,
                                IUserPasswordStore<UserModel>,
                                IUserPhoneNumberStore<UserModel>
    { }
}
