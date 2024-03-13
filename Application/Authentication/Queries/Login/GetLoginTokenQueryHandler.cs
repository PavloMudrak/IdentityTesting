using Identity.Application.Authentication.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Authentication.Queries.Login
{
    public class GetLoginTokenQueryHandler : IRequestHandler<GetLoginTokenQuery, JwtModelDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public GetLoginTokenQueryHandler(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<JwtModelDto> Handle(GetLoginTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Record.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Record.Password))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var token = GetToken(claims);
                
                var result =  new JwtModelDto()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpirationDate = token.ValidTo
                };
                return result;
            }
            throw new UnauthorizedAccessException();
        }

        private JwtSecurityToken GetToken(List<Claim> authCkaims)
        {
            var secret = _configuration["JWT:Secret"];
            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authCkaims,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
