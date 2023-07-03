using System.ComponentModel.DataAnnotations;
using Contracts.Domains;

namespace Customer.API.Entities;

public class Customer : EntityBase<int>
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(150)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
}
