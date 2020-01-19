using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mouser.Domain.Entities
{
    public class Good
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string DataSheetUrl { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string ImagePath { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        [Required]
        [JsonIgnore]
        public Manufacturer Manufacturer { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int Min { get; set; }
        public int Mult { get; set; }
        [JsonIgnore]
        public string MouserPartNumber { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<PriceBreak> PriceBreaks { get; set; }
        public ICollection<AlternatePackaging> AlternatePackagings { get; set; }
        [JsonIgnore]
        public string ProductDetailUrl { get; set; }
        public bool Reeling { get; set; }
        public string ROHSStatus { get; set; }
        [JsonIgnore]
        public string SuggestedReplacement { get; set; }
        [JsonIgnore]
        public int MultiSimBlue { get; set; }
        public ICollection<ProductCompliance> ProductCompliances { get; set; }
    }

    [Table("Goods")]
    public class OldGood
    {
        [Key]
        public int Id { get; set; }
        public string DataSheetUrl { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int Min { get; set; }
        public int Mult { get; set; }
        public string MouserPartNumber { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<PriceBreak> PriceBreaks { get; set; }
        public ICollection<AlternatePackaging> AlternatePackagings { get; set; }
        public string ProductDetailUrl { get; set; }
        public bool Reeling { get; set; }
        public string ROHSStatus { get; set; }
        public ICollection<ProductCompliance> ProductCompliances { get; set; }
    }

    public class ProductAttribute
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public Good Good { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }

    public class PriceBreak
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public Good Good { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
    }

    public class AlternatePackaging
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public Good Good { get; set; }
        public string APMfrPN { get; set; }
    }

    public class ProductCompliance
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public Good Good { get; set; }
        public string ComplianceName { get; set; }
        public string ComplianceValue { get; set; }
    }
}
