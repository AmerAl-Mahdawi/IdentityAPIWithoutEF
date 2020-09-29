using IdentityAPIWithoutEF.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPIWithoutEF.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(UserController.ConfirmEmail),
                controller: "User",
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
