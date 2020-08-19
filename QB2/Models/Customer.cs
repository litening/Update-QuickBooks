using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Customer Id")]
        public Int32 CustomerId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Name")]
        public String CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "Address1")]
        public String Address1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Address2")]
        public String Address2 { get; set; }

        [StringLength(50)]
        [Display(Name = "City")]
        public String City { get; set; }

        [StringLength(10)]
        [Display(Name = "State")]
        public String State { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip")]
        public String Zip { get; set; }

        [StringLength(10)]
        [Display(Name = "Country")]
        public String Country { get; set; }

        [StringLength(50)]
        [Display(Name = "Ship To Address1")]
        public String ShipToAddress1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Ship To Address2")]
        public String ShipToAddress2 { get; set; }

        [StringLength(50)]
        [Display(Name = "Ship To City")]
        public String ShipToCity { get; set; }

        [StringLength(10)]
        [Display(Name = "Ship To State")]
        public String ShipToState { get; set; }

        [StringLength(10)]
        [Display(Name = "Ship To Zip")]
        public String ShipToZip { get; set; }

        [StringLength(10)]
        [Display(Name = "Ship To Country")]
        public String ShipToCountry { get; set; }

        [StringLength(50)]
        [Display(Name = "Ship To Name")]
        public String ShipToName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone")]
        public String Phone { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "E Mail Addr")]
        public String eMailAddr { get; set; }

        [StringLength(500)]
        [Display(Name = "Qb Customer Id")]
        public String QbCustomerId { get; set; }


    }
}
 
