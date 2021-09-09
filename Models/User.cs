using System.Text.Json.Serialization;

namespace ApiJwt.Models
{
    public class User
    {
        [JsonIgnoreAttribute]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [JsonIgnoreAttribute]
        public UserRol IdRol { get; set; }
    }
}