using Salary.Core.Interfaces;
using Salary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salary.Core.Context
{
    public class Repository : IRepository, IDisposable
    {
        ~Repository()
        {
            Dispose(false);
        }

        private EmployeeDbSet _dbContext;
        public Repository()
        {
            _dbContext = new EmployeeDbSet();
        }
        public IEnumerable<Employee> GetEmployees
        {
            get
            {
                return _dbContext.Employee;
            }
        }
        public IEnumerable<Employee> GetBosses
        {
            get
            {
                return GetEmployees.Where(w => w.DismissalDate == null && w.Category.HasEmployees == true);
            }
        }

        public IEnumerable<Category> GetCategories
        {
            get
            {
                return _dbContext.Category;
            }
        }

        public IEnumerable<Contact> GetContacts
        {
            get
            {
                return _dbContext.Contact;
            }
        }

        public IEnumerable<Address> GetAddresses
        {
            get
            {
                return _dbContext.Address;
            }
        }

        public IEnumerable<Currency> GetCurrencies
        {
            get
            {
                return _dbContext.Currency;
            }

        }

        public IEnumerable<Model.Salary> GetSalary
        {
            get
            {
                return _dbContext.Salary;
            }
        }

        public int Dismissal(Employee employee)
        {
            var tmpEmployee = _dbContext.Employee.FirstOrDefault(f => f.Id == employee.Id);
            tmpEmployee = employee;
            return _dbContext.SaveChanges();
        }

        public Employee SaveEmployee(Employee employeeForSave)
        {
            Employee tmpEmployee;
            if (employeeForSave.Id == 0)
            {
                tmpEmployee = _dbContext.Employee.Add(employeeForSave);
            }
            else
            {
                tmpEmployee = _dbContext.Employee.FirstOrDefault(f => f.Id == employeeForSave.Id);
                tmpEmployee = employeeForSave;
            }
            _dbContext.SaveChanges();
            return tmpEmployee;
        }

        public Model.Salary SaveEmployeeSalary(Model.Salary salaryForSave)
        {
            salaryForSave = _dbContext.Salary.Add(salaryForSave);
            _dbContext.SaveChanges();
            return salaryForSave;
        }

        public IEnumerable<Model.Salary> SaveEmployeeSalary(IEnumerable<Model.Salary> salariesForSave)
        {
            salariesForSave = _dbContext.Salary.AddRange(salariesForSave);
            _dbContext.SaveChanges();
            return salariesForSave;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
            }
        }
    }
}
