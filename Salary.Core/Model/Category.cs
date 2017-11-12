using System.ComponentModel.DataAnnotations.Schema;

namespace Salary.Core.Model
{
    [Table("Category")]
    public class Category
    {
        /// <summary>
        /// Unique number of the category
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Salary Rate for this category
        /// </summary>
        public double SalaryRate { get; set; }
        /// <summary>
        /// Unique number of the currency
        /// </summary>
        public int CurrencyId { get; set; }
        /// <summary>
        /// Currency for a given salary
        /// </summary>
        public virtual Currency Currency { get; set; }
        /// <summary>
        /// Percentage of annual surcharge
        /// </summary>
        public double MinPercent { get; set; }
        /// <summary>
        /// Maximal percentage of surcharge
        /// </summary>
        public double MaxPercent { get; set; }
        /// <summary>
        /// This flag indicate the possibility for an employee of this category to have subordinates
        /// </summary>
        public bool HasEmployees { get; set; }
        /// <summary>
        /// Percentage of boss surcharge
        /// </summary>
        public double Surcharge { get; set; }

        /// <summary>
        /// This method copies an object
        /// </summary>
        /// <returns>Return copy of this Category</returns>
        public Category Copy()
        {
            Category tmp = new Category()
            {
                Id = this.Id,
                Name = this.Name,
                SalaryRate = this.SalaryRate,
                CurrencyId = this.CurrencyId,
                Currency = this.Currency.Copy(),
                MinPercent = this.MinPercent,
                MaxPercent = this.MaxPercent,
                HasEmployees = this.HasEmployees,
                Surcharge = this.Surcharge
            };
            return tmp;
        }
    }
}
