using BlogsAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

[ApiController]
[Authorize (Roles = "Admin")]
public class AdminController(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) : ControllerBase
{
    [HttpPut]
    [Route ("")]
    [ProducesResponseType (StatusCodes.Status204NoContent)]
    [ProducesResponseType (StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GiveRoles(string id, string role)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
            return BadRequest( new { msg = "User not found" } );
        if (!await roleManager.RoleExistsAsync(role))
            return BadRequest( new { msg = "Role not found" } );
        await userManager.AddToRoleAsync(user, role);
        await context.SaveChangesAsync();
        await userManager.UpdateSecurityStampAsync(user);
        return NoContent();
    }
}