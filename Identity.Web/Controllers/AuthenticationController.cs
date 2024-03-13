using Identity.Application.Authentication;
using Identity.Application.Authentication.Commands.RegisterUser;
using Identity.Application.Authentication.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender sender;

        public AuthenticationController(ISender sender)
        {
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUSer([FromForm] RegisterUserDto request)
        {
            await this.sender.Send(new RegisterUserCommand(request));
            return this.Ok();
        }

        [HttpGet("login")]
        [ProducesResponseType(typeof(LoginModelDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginModelDto request)
        {
            var token = await this.sender.Send(new GetLoginTokenQuery(request));
            return this.Ok(token);
        }
    }
}
