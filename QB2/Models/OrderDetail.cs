using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Order Detail Id")]
        public Int32 OrderDetailId { get; set; }

        [Required]
        [Display(Name = "Order Id")]
        public Int32 OrderId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public Decimal Quantity { get; set; }

        [Required]
        [Display(Name = "Catalog Item Id")]
        public Int32 CatalogItemId { get; set; }

        [Required]
        [Display(Name = "Price")]
        public Decimal Price { get; set; }

        [StringLength(250)]
        [Display(Name = "Special Instructions")]
        public String SpecialInstructions { get; set; }

        [Required]
        [Display(Name = "Discount Percent")]
        public Byte DiscountPercent { get; set; }

        // ComboBox
        public virtual OrderHeader OrderHeader { get; set; }
        public virtual CatalogItem CatalogItem { get; set; }

    }
}
 
