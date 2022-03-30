using System.ComponentModel.DataAnnotations;
namespace StarBoutique.WebApi.Models;

public class AllocationModel
{
    [Required]
    public string? GuestId { get; set; }
}