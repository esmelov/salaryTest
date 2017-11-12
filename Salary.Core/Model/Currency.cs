using System.ComponentModel.DataAnnotations.Schema;

namespace Salary.Core.Model
{
    [Table("Currency")]
    public class Currency
    {
        /// <summary>
        /// Unique number of the currency
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Unique code of the currency 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Name of the currency
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This method copies an object
        /// </summary>
        /// <returns>Return copy of this Currency</returns>
        public Currency Copy()
        {
            Currency tmp = new Currency()
            {
                Id = this.Id,
                Code = this.Code,
                Name = this.Name
            };
            return tmp;
        }
    }
}
