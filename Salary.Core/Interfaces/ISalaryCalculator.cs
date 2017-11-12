using Salary.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Salary.Core.Interfaces
{
    public interface ISalaryCalculator
    {
        event EventHandler<double> OneCalculate;
        event EventHandler<double> OneSumCount;
        int GetExperience(Employee currentEmployee, DateTime onDate);
        Model.Salary CalculateSalary(Employee currentEmployee, DateTime onDate);
        IEnumerable<Model.Salary> CalculateAllSalary(IEnumerable<Employee> employees, DateTime onDate);
        double CalculateSumSalary(IEnumerable<Employee> employees, DateTime onDate);
        Task<double> AsyncCalculateSumSalary(IEnumerable<Employee> employees, DateTime onDate);
        Task<IEnumerable<Model.Salary>> AsyncCalculateAllSalary(IEnumerable<Employee> employees, DateTime onDate);
    }
}
