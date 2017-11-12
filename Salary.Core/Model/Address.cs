using System.ComponentModel.DataAnnotations.Schema;

namespace Salary.Core.Model
{
    [Table("Address")]
    public class Address
    {
        /// <summary>
        /// Unique number of the Employee
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Flag which indicate what the Employee is resident of the country
        /// </summary>
        public bool IsResident { get; set; }
        /// <summary>
        /// Name of the country
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// Name of the city
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// Name of the street
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// Number or name of the home 
        /// </summary>
        public string Home { get; set; }
        /// <summary>
        /// Additional Info
        /// </summary>
        public string AdditionalInfo { get; set; }
        /// <summary>
        /// Experation date of the Employee's address
        /// </summary>
        public int? ExperationDate { get; set; }

        /// <summary>
        /// This method copies an object
        /// </summary>
        /// <returns>Return copy of this Address</returns>
        public Address Copy()
        {
            Address tmp = new Address()
            {
                Id = this.Id,
                IsResident = this.IsResident,
                CountryName = this.CountryName,
                CityName = this.CityName,
                StreetName = this.StreetName,
                Home = this.Home,
                AdditionalInfo = this.AdditionalInfo,
                ExperationDate = this.ExperationDate
            };
            return tmp;
        }
    }
}
