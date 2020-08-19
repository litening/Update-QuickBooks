using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("ItemOption")]
    public class ItemOption
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Item Option Id")]
        public Int32 ItemOptionId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Description")]
        public String Description { get; set; }


    }
}
 
