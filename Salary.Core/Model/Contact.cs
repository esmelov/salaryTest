using System.ComponentModel.DataAnnotations.Schema;

namespace Salary.Core.Model
{
    [Table("Contact")]
    public class Contact
    {
        /// <summary>
        /// Unique number of Employee
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Phone number of the Employee
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email adress of the Employee
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Experation date of the Employee's contact
        /// </summary>
        public int? ExperationDate { get; set; }

        /// <summary>
        /// This method copies an object
        /// </summary>
        /// <returns>Return copy of this Contact</returns>
        public Contact Copy()
        {
            Contact tmp = new Contact()
            {
                Id = this.Id,
                PhoneNumber = this.PhoneNumber,
                EmailAddress = this.EmailAddress,
                ExperationDate = this.ExperationDate
            };
            return tmp;
        }
    }
}
