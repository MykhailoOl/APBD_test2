namespace Test2.Models.DTOs;

public class GetCharacterDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int CurrWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<GetBackpackItemsInfoDTO> BackpackItems { get; set; } = null!;
    public ICollection<GetCharacterTitleDTO> CharacterTitles { get; set; } = null!;
}

public class GetBackpackItemsInfoDTO
{
    public string ItemName { get; set; } = null!;
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class GetCharacterTitleDTO
{
    public string Title { get; set; } = null!;
    public DateTime AcquiredAt { get; set; }
 
}