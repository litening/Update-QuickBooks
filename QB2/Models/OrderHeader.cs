using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("OrderHeader")]
    public class OrderHeader
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Order Id")]
        public Int32 OrderId { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public Int32 UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Customer Id")]
        public Int32? CustomerId { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public Decimal OrderTotal { get; set; }

        [Required]
        [Display(Name = "Sales Tax")]
        public Decimal SalesTax { get; set; }

        [Display(Name = "Sales Tax Code")]
        public Int32? SalesTaxCode { get; set; }

        [Required]
        [Display(Name = "Shipping Charge")]
        public Decimal ShippingCharge { get; set; }

        [Required]
        [Display(Name = "Qb Updated")]
        public Boolean QbUpdated { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Sales Tax Amt")]
        public Decimal? SalesTaxAmt { get; set; }

        [Display(Name = "Discount Amount")]
        public Decimal? DiscountAmount { get; set; }

        [Display(Name = "Status")]
        public Byte? Status { get; set; }

        [Display(Name = "B Test Order")]
        public Boolean? bTestOrder { get; set; }

        // ComboBox
        public virtual OurCustomer OurCustomer { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
 
