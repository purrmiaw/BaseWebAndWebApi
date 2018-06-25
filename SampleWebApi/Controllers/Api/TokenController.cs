using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi.Extensions;
using SampleWebApi.Models;

namespace SampleWebApi.Controllers.Api
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public TokenController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IPasswordHasher<User> passwordHasher
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] SharedRefreshTokenRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Success)
            {
                return BadRequest();
            }

            // get or generate refresh token
            if (user.RefreshToken == null || user.RefreshToken == "")
            {
                user.RefreshToken = Guid.NewGuid().ToString();

                // update user
                await _userManager.UpdateAsync(user);
            }

            var refreshToken = user.RefreshToken;
            var jwtToken = GenerateJwtToken(user);

            return Ok(new SharedRefreshTokenResponse()
            {
                RefreshToken = refreshToken,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Expiration = jwtToken.ValidTo,
                ApplicationUserId = user.Id
            });
        }

        [HttpPost("refreshtoken/invalidate")]
        public async Task<IActionResult> InvalidateRefreshToken([FromBody] SharedInvalidateRefreshTokenRequest model)
        {
            // validate
            if (model.RefreshToken == null || model.RefreshToken == "")
            {
                return BadRequest();
            }

            var user = await _userManager.Users.Where(x => x.RefreshToken == model.RefreshToken).FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest();
            }

            // invalidate refresh token and update user
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPost("authenticationtoken")]
        public async Task<IActionResult> AuthenticationToken([FromBody] SharedTokenAuthenticationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // validate
            if (model.RefreshToken == null || model.RefreshToken == "")
            {
                return BadRequest();
            }

            var user =
                await _userManager.Users.Where(x => x.RefreshToken == model.RefreshToken).FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest();
            }

            var token = GenerateJwtToken(user);

            return Ok(new SharedTokenAuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                ApplicationUserId = user.Id
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SharedAccountRegistrationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var displayName = model.Email.Split('@')[0];
            var user = new User { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        private JwtSecurityToken GenerateJwtToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddSeconds(Convert.ToDouble(_configuration["Jwt:Expire"]));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return jwtSecurityToken;
        }

    }
}