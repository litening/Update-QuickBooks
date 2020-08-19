using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("OrderDetailOptions")]
    public class OrderDetailOptions
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Order Option Id")]
        public Int32 OrderOptionId { get; set; }

        [Required]
        [Display(Name = "Order Detail Id")]
        public Int32 OrderDetailId { get; set; }

        [Required]
        [Display(Name = "Item Option Id")]
        public Int32 ItemOptionId { get; set; }

        [Required]
        [Display(Name = "Price")]
        public Decimal Price { get; set; }

        // ComboBox
        public virtual ItemOption ItemOption { get; set; }

    }
}
 
