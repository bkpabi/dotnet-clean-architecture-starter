using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.Contracts;

public interface IAuthenticatedUser
{
    string UserId { get; set; }
}
