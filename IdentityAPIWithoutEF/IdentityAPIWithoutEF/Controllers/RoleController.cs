using IdentityAPIWithoutEF.Library.Models;
using IdentityAPIWithoutEF.Library.Stores;
using IdentityAPIWithoutEF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleStore _roleStore;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly ILogger<RoleModel> _logger;

        public RoleController(IRoleStore roleStore,
                              RoleManager<RoleModel> roleManager,
                              ILogger<RoleModel> logger)
        {
            _roleStore = roleStore;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task CreateRoleAsync(RoleApiModel roleApiModel)
        {
            if (ModelState.IsValid)
            {
                var role = new RoleModel
                {
                    Name = roleApiModel.Name
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New Role created");
                }
                else
                {
                    _logger.LogInformation("Role not created.");
                }
            }
        }

        [HttpPost]
        [Authorize]
        public async Task DeleteAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Role Deleted Successfully.");
                }
                else
                {
                    _logger.LogInformation("Role Not Deleted.");
                }
            }
        }
    }
}
