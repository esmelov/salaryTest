using Salary.Core.Helper;
using Salary.Core.Interfaces;
using Salary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salary.Core.Logic
{
    public class SalaryCalculator: ISalaryCalculator
    {
        public event EventHandler<double> OneSumCount;
        public event EventHandler<double> OneCalculate;

        public Model.Salary CalculateSalary(Employee currentEmployee, DateTime onDate)
        {
            Model.Salary curSalary = null;
            foreach (Model.Salary s in currentEmployee.Salary)
            {
                if (s.DateFrom.ToDateTime() <= onDate && s.DateTo.ToDateTime() >= onDate)
                {
                    curSalary = s;
                    break;
                }
            }
            if (curSalary != null)
                return curSalary;

            double totalPercent = _getTotalPercent(currentEmployee, onDate);

            double salaryRate = currentEmployee.Category.SalaryRate * totalPercent;

            int dateFrom = _getFromDate(currentEmployee.Salary, onDate);
            dateFrom = dateFrom < currentEmployee.BeginDate ? currentEmployee.BeginDate : dateFrom;

            curSalary = new Model.Salary
            {
                EmployeeId = currentEmployee.Id,
                SalaryRate = salaryRate, 
                CurrencyId = currentEmployee.Category.CurrencyId,
                DateFrom = dateFrom,
                DateTo = onDate.ToUnixTimeStamp(),
                Currency = currentEmployee.Category.Currency
            };
            return curSalary;
        }

        private double _getTotalPercent(Employee currentEmployee, DateTime onDate)
        {
            double percent = GetExperience(currentEmployee, onDate) * currentEmployee.Category.MinPercent;
            double totalPercent = percent > currentEmployee.Category.MaxPercent ? currentEmployee.Category.MaxPercent : percent + 100;
            if (currentEmployee.Boss != null)
            {
                if (currentEmployee.Boss.Category.HasEmployees)
                    totalPercent += currentEmployee.Boss.Category.Surcharge;
            }
            return totalPercent / 100;
        }

        public int GetExperience (Employee currentEmployee, DateTime onDate)
        {
            if (currentEmployee != null && onDate > DateTime.MinValue)
                return onDate.Year - currentEmployee.BeginDate.ToDateTime().Year;
            return 0;
        }

        private int _getFromDate(IEnumerable<Model.Salary> employeeSalary, DateTime onDate)
        {
            int dateFrom = new DateTime(onDate.Year, onDate.Month, 1, 0, 0, 0).ToUnixTimeStamp();
            if (employeeSalary.Count() > 0)
            {
                dateFrom = employeeSalary.Where(w => w.DateTo.ToDateTime() < onDate).Max(m => m.DateTo) + 1;
                if (dateFrom == 1)
                    dateFrom = new DateTime(onDate.Year, onDate.Month, 1, 0, 0, 0).ToUnixTimeStamp();
            }
            return dateFrom;
        }

        public double CalculateSumSalary(IEnumerable<Employee> employees, DateTime afterDate)
        {
            double sumSalary = 0;
            double value = 100d / employees.Count();
            employees.ToList().ForEach(x =>
            {
                sumSalary += x.Salary.Where(w => w.DateFrom.ToDateTime() >= afterDate || (w.DateFrom.ToDateTime() <= afterDate && afterDate <= w.DateTo.ToDateTime())).Sum(y => y.SalaryRate);
                OneSumCount?.Invoke(this, value);
            });
            return sumSalary;
        }

        public async Task<double> AsyncCalculateSumSalary(IEnumerable<Employee> employees, DateTime afterDate)
        {
            return await Task.Run(() =>
             {
                 return CalculateSumSalary(employees, afterDate);
             });
        }

        public IEnumerable<Model.Salary> CalculateAllSalary(IEnumerable<Employee> employees, DateTime onDate)
        {
            List<Model.Salary> totalSalary = new List<Model.Salary>();
            double value = 100d / employees.Count();
            employees.ToList().ForEach(f =>
            {
                Model.Salary tmpSalary = CalculateSalary(f, onDate);
                totalSalary.Add(tmpSalary);
                OneCalculate?.Invoke(this, value);
            });
            return totalSalary;
        }

        public async Task<IEnumerable<Model.Salary>> AsyncCalculateAllSalary(IEnumerable<Employee> employees, DateTime onDate)
        {
            return await Task.Run(() =>
            {
                return CalculateAllSalary(employees, onDate);
            });
        }
    }
}