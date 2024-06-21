using System.Collections.ObjectModel;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Test2.Models;
using Test2.Models.DTOs;
using Test2.Services;

namespace Test2.Controllers;

[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharacterController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterData(int characterId)
    {
        if (!await _dbService.DoesCharacterExists(characterId))
            return NotFound($"Character with given ID - {characterId} doesn't exist");
        var characters = await _dbService.GetCharactersData(characterId);
    
        return Ok(characters.Select(e => new GetCharacterDTO()
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            CurrWeight = e.CurrWeight,
            MaxWeight = e.MaxWeight,
            BackpackItems = e.Backpacks.Select(b => new GetBackpackItemsInfoDTO()
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList(),
            CharacterTitles = e.CharacterTitles.Select(c=>new GetCharacterTitleDTO()
            {
                Title = c.Title.Name,
                AcquiredAt = c.AcquiredAt
            }).ToList()
        }));
    }

    [HttpGet("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemToCharacter(int characterId, List<AddItemDTO> addItemDto)
    {
        
        foreach (var item in addItemDto) {
            if (!await _dbService.DoesItemExists(item.ItemId))
                return NotFound($"Item with given ID - {item.ItemId} doesn't exist");
        }
        
        if (!await _dbService.DoesCharacterExists(characterId))
            return NotFound($"Character with given ID - {characterId} doesn't exist");
        
        if (!await _dbService.CheckWeight(addItemDto,characterId))
            return NotFound("Not enough space");
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddItems(addItemDto,characterId);
            scope.Complete();
        }

        List<ReturnItemDTO> list = new List<ReturnItemDTO>();
        foreach (var item in addItemDto)
        {
            list.Add(new ReturnItemDTO {Amount = item.Amount,ItemId = item.ItemId,CharacterId = characterId});
        }
        return Created("api/characters", list);
    }
}