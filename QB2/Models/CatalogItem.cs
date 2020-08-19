using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("CatalogItem")]
    public class CatalogItem
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Catalog Item Id")]
        public Int32 CatalogItemId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [StringLength(100)]
        [Display(Name = "Sku")]
        public String Sku { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Item Id")]
        public String QbItemId { get; set; }


    }
}
 
