using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class CreateProductDto : CreateOrUpdateProductDto
{
    [Required]
    public string No { get; set; }
}
