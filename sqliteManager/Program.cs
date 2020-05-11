using AuthenticationApp;
using AuthenticationApp.Models;
using Microsoft.Extensions.Options;
using System;

namespace sqliteManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (sqliteDBContext sqlite = new sqliteDBContext())
                {
                    Person prsUsr = new Person()
                    {
                        Login = "user",
                        Role = AuthenticationApp.Roles.PersonRole.User,
                        Password = "12345"
                    };
                    Person prsAdm = new Person()
                    {
                        Login = "Administrator",
                        Role = AuthenticationApp.Roles.PersonRole.Admin,
                        Password = "admin"
                    };
                    Person prsMod = new Person()
                    {
                        Login = "Moderator",
                        Password = "moder",
                        Role = AuthenticationApp.Roles.PersonRole.Moderator
                    };
                    sqlite.Persons.Add(prsUsr);
                    sqlite.Persons.Add(prsAdm);
                    sqlite.Persons.Add(prsMod);
                    sqlite.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
