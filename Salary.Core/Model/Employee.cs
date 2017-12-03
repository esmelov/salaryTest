using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salary.Core.Model
{
    [Table("Employee")]
    public class Employee
    {
        /// <summary>
        /// Unique number of the Employee
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// First Name of the Employee
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name of the Employee
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Date of Birth of the Employee
        /// </summary>
        public int BirthDate { get; set; }
        /// <summary>
        /// Address of the Employee
        /// </summary>
        [ForeignKey("Id")]
        public virtual Address Address { get; set; }
        /// <summary>
        /// Contacts of the Employee
        /// </summary>
        [ForeignKey("Id")]
        public virtual Contact Contact { get; set; }
        /// <summary>
        /// Unique number of the Employee's Category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Category of the Employee
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// Salaries of the Employee
        /// </summary>
        [ForeignKey("EmployeeId")]
        public virtual ICollection<Salary> Salary { get; set; }
        /// <summary>
        /// Unique number of the Boss of the Employee
        /// </summary>
        public int? BossId { get; set; }
        /// <summary>
        /// Boss of the Employee
        /// </summary>
        public virtual Employee Boss { get; set; }
        /// <summary>
        /// Subordinates of the Employee
        /// </summary>
        [ForeignKey("BossId")]
        public virtual ICollection<Employee> Subordinates { get; set; }
        /// <summary>
        /// Date of hiring
        /// </summary>
        public int BeginDate { get; set; }
        /// <summary>
        /// Date of dismissal
        /// </summary>
        public int? DismissalDate { get; set; }

        /// <summary>
        /// This method copies an object
        /// </summary>
        /// <returns>Return copy of this Employee</returns>
        public Employee Copy()
        {
            Employee tmp = new Employee()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                BirthDate = this.BirthDate,
                Address = this.Address?.Copy(),
                Contact = this.Contact?.Copy(),
                CategoryId = this.CategoryId,
                BossId = this.BossId,
                Subordinates = this.Subordinates,
                BeginDate = this.BeginDate,
                DismissalDate = this.DismissalDate
            };
            return tmp;
        }
    }
}
