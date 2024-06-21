using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Data;

public class ApplicationContext : DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Backpack> Backpacks{get;set;}
    public DbSet<Character> Characters{get;set;}
    public DbSet<Character_title> CharactersTitles{get;set;}
    public DbSet<Title> Titles{get;set;}
    public DbSet<Item> Items{get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new()
            {
                Id = 1, 
                Name = "Item", 
                Weight = 10
            },
        });
        
        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new()
            {
                Id = 1, 
                Name = "Title"
            },
        });
        
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new()
            {
                Id = 1, 
                FirstName = "FirstName", 
                LastName = "LastName",
                CurrWeight = 10,
                MaxWeight = 100
            },
        });
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new()
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 2
            },
        });
        
        modelBuilder.Entity<Character_title>().HasData(new List<Character_title>()
        {
            new()
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Now
            },
        });
    }
}