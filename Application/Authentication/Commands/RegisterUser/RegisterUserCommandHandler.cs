using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration)
        {                                 
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userManager.FindByEmailAsync(request.Record.Email);
        }
    }
}
