using MicroserviceEnlistment.Dto;
using MicroserviceEnlistment.Helpers.Interfaces;
using MicroserviceEnlistment.Models;
using MicroserviceEnlistment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MicroserviceEnlistment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserContext _context;

        public AccountController(IUserService userService, UserContext context)
        {
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UserDto model)
        {
            User user = model;

            if (model.FirstName is null && model.LastName is null && model.Address is null)
            {
                return BadRequest("Something went wrong, please try again");
            }

            var result = await _userService.AddUserAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("Something went wrong, please try again");
            }
        }

        /// <summary>
        /// Login Companies Users
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _userService.LoginAsync(model);

            if (result.Succeeded)
            {
                var user = await _userService.GetUserByEmailAsync(model.UserName);


                return Ok(user);
            }
            else
            {
                return BadRequest("Wrong username or password.");
            }
        }

        /// <summary>
        /// LogOut de la Sección
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Logout")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok("See you soon");
        }

        /// <summary>
        /// Reiniciar contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Reset Password")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userService.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userService.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Password reset successful.");
                }
                return BadRequest("Error while resetting the password.");
            }

            return NotFound("User not found.");
        }

        /// <summary>
        /// Editar usuarios
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Edit User")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);

            if (model.FirstName.IsNullOrEmpty() ||
                model.LastName.IsNullOrEmpty() ||
                model.Address.IsNullOrEmpty() ||
                model.PhoneNumber.IsNullOrEmpty())
            {
                return BadRequest("Please complete the selected fields");
            }
            if (user == null)
            {
                return Ok(false);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            await _userService.UpdateUserAsync(user);
            return Ok(true);
        }

        /// <summary>
        /// Modificar contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Modify Password")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ModifyPassword(ChangePasswordViewModel model)
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity?.Name);
            if (user != null)
            {
                var result = await _userService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Please login again");
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
            }

            return BadRequest("User not found.");
        }

        /// <summary>
        /// olvido su contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Forgot Password")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ForgotPassword(RecoverPasswordViewModel model)
        {
            var user = await _userService.GetUserByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("The email doesn't correspont to a registered user.");
            }

            var myToken = await _userService.GeneratePasswordResetTokenAsync(user);

            return Ok(myToken);
        }

        /// <summary>
        /// Eliminar Usuario
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete User")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var userDelete = await _userService.GetUserByEmailAsync(email);

            if (userDelete == null)
            {
                return NotFound("user not found");
            }

            var response = await _userService.DeleteUserAsync(email);

            return Ok(response);
        }
    }
}