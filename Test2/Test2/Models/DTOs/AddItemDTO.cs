using System.ComponentModel.DataAnnotations;

namespace Test2.Models.DTOs;

public class AddItemDTO
{
    [Required]
    public int ItemId { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Amount { get; set; }
}

public class ReturnItemDTO
{
    public int Amount { get; set; }
    public int ItemId { get; set; }
    public int CharacterId { get; set; }
}