using Salary.Core.Model;
using System.Data.Entity;

namespace Salary.Core.Context
{
    internal class EmployeeDbSet: DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Model.Salary> Salary { get; set; }
    }
}
