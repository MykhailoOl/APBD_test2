using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Models;
using Test2.Models.DTOs;

namespace Test2.Services;

public class DbService : IDbService
{
    private readonly ApplicationContext _context;
    public DbService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesCharacterExists(int characterId)
    {
        return await _context.Characters.AnyAsync(c => c.Id == characterId);
    }
    public async Task<ICollection<Character>> GetCharactersData(int id)
    {
        return await _context.Characters
            .Include(e => e.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(e => e.CharacterTitles)
            .ThenInclude(c => c.Title)
            .Where(e => e.Id == id)
            .ToListAsync();
    }

    public async Task<bool> DoesItemExists(int itemId)
    {
        return await _context.Items.AnyAsync(i => i.Id == itemId);
    }

    public async Task<bool> CheckWeight(List<AddItemDTO> itemDtos, int characterId)
    {
        var weight = 0;
        foreach (var item in itemDtos)
        {
            Item newItem = await _context.Items.FirstOrDefaultAsync(e => e.Id == item.ItemId);
            weight += newItem.Weight;
        }

        Character character = await _context.Characters.FirstOrDefaultAsync(e => e.Id == characterId);
        if (character.CurrWeight + weight > character.MaxWeight)
        {
            return false;
        }

        return true;
    }

    public async Task AddItems(List<AddItemDTO> itemDtos, int characterId)
    {
        Character character = await _context.Characters.FirstOrDefaultAsync(e => e.Id == characterId);
        foreach (var item in itemDtos)
        {
            foreach (var backpack in character.Backpacks)
            {
                if (item.ItemId == backpack.ItemId)
                {
                    backpack.Amount += item.Amount;
                    _context.Backpacks.Update(backpack);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Backpacks.Add(new Backpack()
                    {
                        CharacterId = characterId,
                        ItemId = item.ItemId,
                        Amount = item.Amount
                    });
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}