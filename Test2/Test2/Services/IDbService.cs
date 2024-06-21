using Test2.Models;
using Test2.Models.DTOs;

namespace Test2.Services;

public interface  IDbService
{
    Task<bool> DoesCharacterExists(int characterId);
    public Task<ICollection<Character>> GetCharactersData(int id);
    Task<bool> DoesItemExists(int itemId);
    Task<bool> CheckWeight(List<AddItemDTO> itemDtos,int characterId);

    public Task AddItems(List<AddItemDTO> itemDtos,int characterId);
}