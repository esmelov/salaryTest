using Salary.Core.Model;
using System.Collections.Generic;

namespace Salary.Core.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Employee> GetEmployees { get; }
        IEnumerable<Employee> GetBosses { get; }
        IEnumerable<Category> GetCategories { get; }
        IEnumerable<Contact> GetContacts { get; }
        IEnumerable<Address> GetAddresses { get; }
        IEnumerable<Currency> GetCurrencies { get; }
        IEnumerable<Model.Salary> GetSalary { get; }
        Model.Salary SaveEmployeeSalary(Model.Salary salaryForSave);
        IEnumerable<Model.Salary> SaveEmployeeSalary(IEnumerable<Model.Salary> salaryForSave);
        int Dismissal(Employee employee);
        Employee SaveEmployee(Employee employeeForSave);
    }
}
