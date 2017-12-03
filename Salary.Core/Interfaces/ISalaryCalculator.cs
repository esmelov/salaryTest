using Salary.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Salary.Core.Interfaces
{
    public interface ISalaryCalculator
    {
        /// <summary>
        /// An Event for indicate of the calculate salary process
        /// </summary>
        event EventHandler<double> OneCalculate;

        /// <summary>
        /// An event to indicate the process of calculating the salary
        /// </summary>
        event EventHandler<double> OneSumCount;

        /// <summary>
        /// Method for calculating Employee experience
        /// </summary>
        /// <param name="currentEmployee">The Employee for calculating</param>
        /// <param name="onDate">Date for which you need to calculate the experience</param>
        /// <returns>The Employee experience</returns>
        int GetExperience(Employee currentEmployee, DateTime onDate);

        /// <summary>
        /// Method for calculating the Employee's salary
        /// </summary>
        /// <param name="currentEmployee">Employee for calculating salary</param>
        /// <param name="onDate">Date for which you need to calculate the salary</param>
        /// <returns>The salary of the Employee</returns>
        Model.Salary CalculateSalary(Employee currentEmployee, DateTime onDate);

        /// <summary>
        /// Method for calculating employee salaries
        /// </summary>
        /// <param name="employees">List of the Employees for calculating</param>
        /// <param name="onDate">Date for which you need to calculate the salary</param>
        /// <returns>List of the employee salaries</returns>
        IEnumerable<Model.Salary> CalculateAllSalary(IEnumerable<Employee> employees, DateTime onDate);

        /// <summary>
        /// Async method for calculating employee salaries
        /// </summary>
        /// <param name="employees">List of the Employees for calculating</param>
        /// <param name="onDate">Date for which you need to calculate the salary</param>
        /// <returns>List of the employee salaries</returns>
        Task<IEnumerable<Model.Salary>> AsyncCalculateAllSalary(IEnumerable<Employee> employees, DateTime onDate);

        /// <summary>
        /// Method for calculating the total amount of the salaries
        /// </summary>
        /// <param name="employees">List of the Employees for calculating</param>
        /// <param name="onDate">The date on which the total amount of salaries will be calculated</param>
        /// <returns>Total amount of salaries</returns>
        double CalculateSumSalary(IEnumerable<Employee> employees, DateTime onDate);

        /// <summary>
        /// Async method for calculating the total amount of the salaries
        /// </summary>
        /// <param name="employees">List of the Employees for calculating</param>
        /// <param name="onDate">The date on which the total amount of salaries will be calculated</param>
        /// <returns>Total amount of salaries</returns>
        Task<double> AsyncCalculateSumSalary(IEnumerable<Employee> employees, DateTime onDate);
    }
}
