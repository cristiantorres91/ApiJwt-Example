using ApiJwt.Models;

namespace ApiJwt.Services
{
    public interface IAuthServices
    {
         string Login(User user);
    }
}