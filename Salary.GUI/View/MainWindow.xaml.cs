using Salary.Core.Context;
using Salary.Core.Interfaces;
using Salary.Core.Logic;
using Salary.GUI.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Salary.GUI.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _initializeDataContext();
        }

        private void _initializeDataContext()
        {
            IRepository employeeRepository = new Repository();
            ISalaryCalculator salaryCalculator = new SalaryCalculator();
            DataContext = new MainWindowViewModel(employeeRepository, salaryCalculator);
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content is Page CurPage)
            {
                CurPage.NavigationService.RemoveBackEntry();
                CurPage.DataContext = this.DataContext;
            }
        }
    }
}
