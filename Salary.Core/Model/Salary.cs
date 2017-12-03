using System.ComponentModel.DataAnnotations.Schema;

namespace Salary.Core.Model
{
    [Table("Salary")]
    public class Salary
    {
        /// <summary>
        /// Unique Salary number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique Employee number
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Start date of the period
        /// </summary>
        public int DateFrom { get; set; }

        /// <summary>
        /// End date of the period
        /// </summary>
        public int DateTo { get; set; }

        /// <summary>
        /// Employee salary rate
        /// </summary>
        public double SalaryRate { get; set; }

        /// <summary>
        /// Unique number of the currency
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Currency for this salary rate
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// This method copies an object
        /// </summary>
        /// <returns>Return copy of this salary</returns>
        
        public Salary Copy()
        {
            Salary tmp = new Salary()
            {
                Id = this.Id,
                EmployeeId = this.EmployeeId,
                DateFrom = this.DateFrom,
                DateTo = this.DateTo,
                SalaryRate = this.SalaryRate,
                CurrencyId = this.CurrencyId,
                Currency = this.Currency.Copy()
            };
            return tmp;
        }
    }
}
