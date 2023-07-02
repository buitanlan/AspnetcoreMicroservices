using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Product.API.Entities;
public class CatalogProduct: EntityAuditBase<int>
{
    [Required]
    [MaxLength(150)]
    public string No { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [MaxLength(250)]
    public string? Summary { get; set; }
    public string? Description { get; set; }
    
    [Column(TypeName = "decimal(12,2)")]
    public decimal Price { get; set; } 
}
