using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Requests;

public sealed class UpdateProductDescriptionRequest
{
    [Required]
    public string Description { get; set; } = null!;
}