using Identity.Application.Authentication.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Authentication.Queries.Login
{
    public class GetLoginTokenQueryHandler : IRequestHandler<GetLoginTokenQuery, string>
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
        public async Task<string> Handle(GetLoginTokenQuery request, CancellationToken cancellationToken)
        {
            return "";
        }
    }
}
