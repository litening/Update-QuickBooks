using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace QB2.Models
{
    [Table("OurCustomer")]
    public class OurCustomer
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "User Id")]
        public Int32 UserId { get; set; }

        [StringLength(500)]
        [Display(Name = "Qb Customer Id")]
        public String QbCustomerId { get; set; }

        [StringLength(500)]
        [Display(Name = "Quickbooks Access Token")]
        public String QuickbooksAccessToken { get; set; }

        [StringLength(500)]
        [Display(Name = "Quickbooks Secret Token")]
        public String QuickbooksSecretToken { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Sales Account")]
        public String QbSalesAccount { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Sales Tax")]
        public String QbSalesTax { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Sales Discounts")]
        public String QbSalesDiscounts { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Freight Income")]
        public String QbFreightIncome { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Cash")]
        public String QbCash { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Cost Of Goods")]
        public String QbCostOfGoods { get; set; }

        [StringLength(50)]
        [Display(Name = "Qb Undepositied Funds")]
        public String QbUndepositiedFunds { get; set; }

        [Display(Name = "Qb Sales Id")]
        public Int32? QbSalesId { get; set; }

        [Display(Name = "Qb Sales Tax Id")]
        public Int32? QbSalesTaxId { get; set; }

        [Display(Name = "Qb Discounts Id")]
        public Int32? QbDiscountsId { get; set; }

        [Display(Name = "Qb Freight Id")]
        public Int32? QbFreightId { get; set; }

        [Display(Name = "Qb Cash Id")]
        public Int32? QbCashId { get; set; }

        [Display(Name = "Qb Costof Goods Id")]
        public Int32? QbCostofGoodsId { get; set; }

        [Display(Name = "Qb Undeposited Funds Id")]
        public Int32? QbUndepositedFundsId { get; set; }


    }
}
 
