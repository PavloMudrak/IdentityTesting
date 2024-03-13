using Identity.Application.Authentication;
using Identity.Application.Authentication.Commands.RegisterUser;
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
        public async Task<IActionResult> Post([FromForm] RegisterUserDto request)
        {
            await this.sender.Send(new RegisterUserCommand(request));
            return this.Ok();
        }
    }
}
