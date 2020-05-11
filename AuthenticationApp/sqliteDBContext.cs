using AuthenticationApp.Models;
using AuthenticationApp.Roles;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp
{
    public class sqliteDBContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public sqliteDBContext(DbContextOptions<sqliteDBContext> dbContextOptions):base(dbContextOptions)
        {
            Database.EnsureCreated();
        }
        public sqliteDBContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
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
            modelBuilder.Entity<Person>().HasNoKey();
            modelBuilder.Entity<Person>().HasData(new Person[] { prsUsr, prsAdm, prsMod });
            base.OnModelCreating(modelBuilder);
        }
    }
}