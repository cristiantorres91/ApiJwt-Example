using Microsoft.AspNetCore.Authorization;

namespace ApiJwt.Helpers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
         public AuthorizeRolesAttribute(params int[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}