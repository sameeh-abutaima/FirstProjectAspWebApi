using System;

namespace FirstprojectAspWebApi.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImageUrl { get; set; }
        public string ImageString { get; set; }
        public string Email { get; set; }
    }
}
