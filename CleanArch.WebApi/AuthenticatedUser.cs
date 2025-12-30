using CleanArch.ApplicationCore.Contracts;

namespace CleanArch.WebApi
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        public string UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
