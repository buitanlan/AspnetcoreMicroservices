using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class CreateOrUpdateProductDto
{
    [Required]
    [MaxLength(250, ErrorMessage = "Maximum length for Product Name is 250 characters.")]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
