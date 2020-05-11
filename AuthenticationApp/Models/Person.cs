using AuthenticationApp.Roles;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationApp.Models
{
    public class Person
    {
        [Key]
        public Guid id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public PersonRole Role { get; set; }
    }
}