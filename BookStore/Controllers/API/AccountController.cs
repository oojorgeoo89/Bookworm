using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers.API
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<BookwormUser> _userManager;
        private readonly SignInManager<BookwormUser> _signinManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<BookwormUser> userManager
                                 , SignInManager<BookwormUser> signInManager
                                 , IConfiguration config)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> register([FromBody] RegisterViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new BookwormUser { 
                UserName = model.Username, 
                Email = model.Email,
                Address = model.Address 
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            return Ok();
        }

        [HttpPost("getToken")]
        public async Task<IActionResult> getToken([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            BookwormUser user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var token = generateJWT(user);

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return new ObjectResult(results);
        }

        private JwtSecurityToken generateJWT(BookwormUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(Policies.USER, Policies.USER));
            if (user.IsAdmin) claims.Add(new Claim(Policies.ADMIN, Policies.ADMIN));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(24),
                signingCredentials: creds  
            );

            return token;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
            
            BookwormUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }

            return Ok();
        }

    }
}
