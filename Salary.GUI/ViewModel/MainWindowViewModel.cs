using Salary.Core.Interfaces;
using Salary.Core.Model;
using Salary.Core.Helper;
using Salary.GUI.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Salary.GUI.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Private Variables
        private ObservableCollection<Employee> _employees;
        private Employee _currentEmployee;
        private Employee _tempEmployee;
        private Core.Model.Salary _employeeSalary;
        private ISalaryCalculator _salaryCalculator;
        private IRepository _employeeRepository;
        private double _totalSalary;
        private double _calcSalaryPBValue;
        private string _currentPage;
        private bool _negativeExecuting = true;

        #region Commands 
        private RelayCommand _addEmployeeCmd;
        private RelayCommand _calculateTotalSumCmd;
        private RelayCommand _deleteEmployeeCmd;
        private RelayCommand _modifyEmployeeCmd;
        private RelayCommand _saveEmployeeCmd;
        private RelayCommand _backToInfoCmd;
        private RelayCommand _saveSalaryCmd;
        private RelayCommand _calculateSalary;
        private RelayCommand _calculateAllSalaryCmd;
        #endregion

        #endregion

        #region Public Variables

        #region Constructors

        public MainWindowViewModel(IRepository employeeRepository, ISalaryCalculator salaryCalculator)
        {
            _employeeRepository = employeeRepository;
            _employees = new ObservableCollection<Employee>(_employeeRepository.GetEmployees);
            _salaryCalculator = salaryCalculator;
            _salaryCalculator.OneCalculate += _salaryCalculator_OneCalculate;
            CurrentPage = "Pages/EmployeeInfoPage.xaml";
            CalcSalaryPBValue = 0;
        }

        #endregion

        #region Properties

        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        public List<Category> Categories
        {
            get
            {
                return _employeeRepository.GetCategories.ToList();
            }
        }

        public List<Employee> Bosses
        {
            get
            {
                List<Employee> emp = Employees.Where(w => w.DismissalDate == null && w.Category?.HasEmployees == true && w.Id != TempEmployee.Id).Select(s => s).ToList();
                if (emp.Count > 0)
                    emp.Add(null);
                return emp;
            }
        }


        public Employee CurrentEmployee
        {
            get
            {
                return _currentEmployee;
            }
            set
            {
                _currentEmployee = value;
                EmployeeSalary = null;
                OnPropertyChanged("Experience");
                OnPropertyChanged("SubordinateIsEnabled");
                OnPropertyChanged();
            }
        }

        public Employee TempEmployee
        {
            get
            {
                return _tempEmployee;
            }
            set
            {
                _tempEmployee = value;
                OnPropertyChanged();
            }
        }

        public Core.Model.Salary EmployeeSalary
        {
            get
            {
                return _employeeSalary;
            }
            private set
            {
                _employeeSalary = value;
                OnPropertyChanged();
            }
        }

        public int Experience
        {
            get
            {
                return _salaryCalculator.GetExperience(CurrentEmployee, DateTime.Now);
            }
        }

        public double CalcSalaryPBValue
        {
            get
            {
                return _calcSalaryPBValue;
            }
            private set
            {
                _calcSalaryPBValue = value;
                OnPropertyChanged();
            }
        }       

        public double TotalSalary
        {
            get
            {
                return _totalSalary;
            }
            private set
            {
                _totalSalary = value;
                OnPropertyChanged();
            }
        }

        public bool NegativeExecuting
        {
            get
            {
                return _negativeExecuting;
            }
            set
            {
                _negativeExecuting = value;
                OnPropertyChanged();
            }
        }

        public bool SubordinateIsEnabled
        {
            get
            {
                return CurrentEmployee?.Subordinates?.Count() > 0;
            }
        }

        #region Commands

        public RelayCommand AddEmployeeCmd
        {
            get
            {
                return _addEmployeeCmd ??
                    (_addEmployeeCmd = new RelayCommand(obj => _addEmployee((ObservableCollection<Employee>)obj), obj => _canAddEmployee(obj)));
            }
        }

        public RelayCommand DeleteEmployeeCmd
        {
            get
            {
                return _deleteEmployeeCmd ??
                    (_deleteEmployeeCmd = new RelayCommand(obj =>
                    {
                        _dismissEmployee(obj);
                        OnPropertyChanged("CurrentEmployee");
                        Employees = new ObservableCollection<Employee>(_employeeRepository.GetEmployees);
                    }, obj => _canDismissEmployee(obj)));
            }
        }

        public RelayCommand CalculateTotalSumCmd
        {
            get
            {
                return _calculateTotalSumCmd ??
                    (_calculateTotalSumCmd = new RelayCommand(async obj =>
                    {
                        var values = (object[])obj;
                        TotalSalary = await _salaryCalculator.AsyncCalculateSumSalary((IEnumerable<Employee>)values[1], (DateTime)values[0]);//await _calculateTotalSumAsync((IEnumerable<Employee>)values[1], (DateTime)values[0]);
                    }, obj =>  _canCalculateTotalSum(obj)));
            }
        }

        public RelayCommand ModifyEmployeeCmd
        {
            get
            {                
                return _modifyEmployeeCmd ??
                    (_modifyEmployeeCmd = new RelayCommand(async obj =>
                    {
                        NegativeExecuting = false;
                        var emp = (Employee)obj;
                        if (emp.Id == 0)
                            TempEmployee = new Employee()
                            {
                                BeginDate = emp.BeginDate,
                                BirthDate = emp.BirthDate,
                                Address = new Address(),
                                Contact = new Contact()
                            };
                        else
                        {
                            TempEmployee = emp.Copy();
                        }
                        await Task.Delay(500);
                        CurrentPage = "Pages/ModifyEmployeePage.xaml";
                    }, obj => _canModify(obj)));
            }
        }

        public RelayCommand SaveEmployeeCmd
        {
            get
            {
                return _saveEmployeeCmd ??
                    (_saveEmployeeCmd = new RelayCommand(obj =>
                    {
                        var t = (Employee)obj;
                        // Address
                        CurrentEmployee.Address.IsResident = t.Address.IsResident;
                        CurrentEmployee.Address.CountryName = t.Address.CountryName;
                        CurrentEmployee.Address.CityName = t.Address.CityName;
                        CurrentEmployee.Address.Home = t.Address.Home;
                        CurrentEmployee.Address.AdditionalInfo = t.Address.AdditionalInfo;
                        CurrentEmployee.Address.StreetName = t.Address.StreetName;
                        CurrentEmployee.Address.ExperationDate = t.Address.ExperationDate;

                        CurrentEmployee.BeginDate = t.BeginDate;
                        CurrentEmployee.BirthDate = t.BirthDate;
                        CurrentEmployee.BossId = t.BossId;
                        CurrentEmployee.CategoryId = t.CategoryId;

                        // Contact
                        CurrentEmployee.Contact.EmailAddress = t.Contact.EmailAddress;
                        CurrentEmployee.Contact.ExperationDate = t.Contact.ExperationDate;
                        CurrentEmployee.Contact.PhoneNumber = t.Contact.PhoneNumber;

                        CurrentEmployee.FirstName = t.FirstName;
                        CurrentEmployee.LastName = t.LastName;

                        CurrentEmployee = _employeeRepository.SaveEmployee(CurrentEmployee);
                        Employees = new ObservableCollection<Employee>(_employeeRepository.GetEmployees);
                        CurrentPage = "Pages/EmployeeInfoPage.xaml";
                        NegativeExecuting = true;
                    }, obj => _canSaveEmployee(obj)));
            }
        }

        public RelayCommand BackToInfoCmd
        {
            get
            {
                return _backToInfoCmd ??
                    (_backToInfoCmd = new RelayCommand(async obj =>
                    {
                        TempEmployee = null;
                        await Task.Delay(500);
                        CurrentPage = "Pages/EmployeeInfoPage.xaml";
                        NegativeExecuting = true;
                    }));
            }
        }

        public RelayCommand SaveSalaryCmd
        {
            get
            {
                return _saveSalaryCmd ??
                    (_saveSalaryCmd = new RelayCommand(obj =>
                    {
                        EmployeeSalary = _employeeRepository.SaveEmployeeSalary((Core.Model.Salary)obj);
                        Employees = new ObservableCollection<Employee>(_employeeRepository.GetEmployees);
                        OnPropertyChanged("CurrentEmployee");
                    }, obj => _canSaveSalary(obj)));
            }
        }

        public RelayCommand CalculateSalaryCmd
        {
            get
            {
                return _calculateSalary ??
                    (_calculateSalary = new RelayCommand(obj =>
                    {
                        var values = (object[])obj;
                        DateTime onDT = ((DateTime)values[0]).AddSeconds(-1);
                        Employee curEmp = (Employee)values[1];
                        if (curEmp.BeginDate < onDT.ToUnixTimeStamp())
                            EmployeeSalary = _salaryCalculator.CalculateSalary(curEmp, onDT);
                    }));
            }
        }

        public RelayCommand CalculateAllSalaryCmd
        {
            get
            {
                return _calculateAllSalaryCmd ??
                    (_calculateAllSalaryCmd = new RelayCommand(async obj =>
                    {
                        CalcSalaryPBValue = 0;
                        var values = (object[])obj;
                        DateTime onDT = ((DateTime)values[0]).AddSeconds(-1);
                        var tmpEmps = ((ObservableCollection<Employee>)values[1]).Where(w => w.DismissalDate == null && w.BeginDate.ToDateTime() < onDT && w.Id > 0);
                        IEnumerable<Core.Model.Salary> tmpSalaries = await _salaryCalculator.AsyncCalculateAllSalary(tmpEmps, onDT);
                        tmpSalaries = _employeeRepository.SaveEmployeeSalary(tmpSalaries.Where(w => w.Id == 0));
                        Employees = new ObservableCollection<Employee>(_employeeRepository.GetEmployees);
                        OnPropertyChanged("CurrentEmployee");
                    }, obj => _canCalculateTotalSum(obj)));
            }
        }

        #endregion

        public string CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        private void _salaryCalculator_OneCalculate(object sender, double e)
        {
            CalcSalaryPBValue += e;
        }

        private void _addEmployee(ObservableCollection<Employee> employees)
        {
            Employee newEmployee = new Employee
            {
                FirstName = "<FirstName>",
                LastName = "<LastName>",
                BeginDate = DateTime.Now.ToUnixTimeStamp(),
                BirthDate = DateTime.Now.ToUnixTimeStamp(),
                Address = new Address(),
                Contact = new Contact(),
                DismissalDate = null
            };
            employees.Add(newEmployee);
        }

        private bool _canAddEmployee(object employees)
        {
            if (employees is ObservableCollection<Employee> emps && NegativeExecuting)
            {
                return emps.Count > 0;
            }
            return false;
        }

        private void _dismissEmployee(object delCmpPrms)
        {
            var parameters = delCmpPrms as object[];
            Employee emp = parameters[0] as Employee;
            ObservableCollection<Employee> emps = parameters[1] as ObservableCollection<Employee>;
            if (emp.Id == 0)
                emps.Remove(emp);
            else
            {
                emp.DismissalDate = DateTime.Now.ToUnixTimeStamp();

                if (emp.Address != null)
                    emp.Address.ExperationDate = DateTime.Now.ToUnixTimeStamp();
                if (emp.Contact != null)
                    emp.Contact.ExperationDate = DateTime.Now.ToUnixTimeStamp();

                int i = _employeeRepository.Dismissal(emp);
            }
        }

        private bool _canDismissEmployee(object parameters)
        {
            if (parameters is object[] values && NegativeExecuting)
            {
                if (values[0] is Employee emp && values[1] is ObservableCollection<Employee> emps)
                    if (emp.DismissalDate == null)
                        return true;
            }
            return false;
        }

        private async Task<double> _calculateTotalSumAsync(IEnumerable<Employee> employees, DateTime dtStart)
        {

            double totalSum = await _salaryCalculator.AsyncCalculateSumSalary(employees, dtStart);
            return totalSum;
        }

        private async Task<IEnumerable<Core.Model.Salary>> _calculateAllSalary(IEnumerable<Employee> employees, DateTime onDate)
        {
            return await _salaryCalculator.AsyncCalculateAllSalary(employees, onDate);
        }

        private bool _canCalculateTotalSum(object parameters)
        {
            if (parameters is object[] values)
            {
                if (values[1] is IEnumerable<Employee> employees && values[0] is DateTime dtStart)
                {
                    return employees.Count() > 0 && dtStart > DateTime.MinValue;
                }
            }
            return false;
        }

        private bool _canModify(object parameters)
        {
            if (parameters is Employee emp && NegativeExecuting)
            {
                return true;
            }
            return false;
        }

        private bool _canSaveSalary(object employeeSalary)
        {
            if (employeeSalary is Core.Model.Salary empSalary)
            {
                if (empSalary.Id == 0)
                    return true;
            }
            return false;
        }

        private bool _canSaveEmployee(object parameters)
        {
            if (parameters is Employee emp)
            {
                if (!String.IsNullOrEmpty(emp.FirstName) &&
                    !String.IsNullOrEmpty(emp.LastName) &&
                    !String.IsNullOrEmpty(emp.Address?.CountryName) &&
                    !String.IsNullOrEmpty(emp.Contact?.PhoneNumber) &&
                    emp.CategoryId > 0)
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
