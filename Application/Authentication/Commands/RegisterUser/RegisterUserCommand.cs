using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(RegisterUserDto Record) : IRequest;
}
