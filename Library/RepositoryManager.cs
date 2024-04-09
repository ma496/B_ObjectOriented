using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Library;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _dbContext;

    public RepositoryManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Initialize()
    {

    }

    public async Task Register(string itemName, string itemContent, int itemType)
    {
        if (_dbContext == null)
        {
            throw new InvalidOperationException("AppDbContext is not initialized.");
        }

        if (await _dbContext.Items.AnyAsync(x => x.Name == itemName))
        {
            throw new ArgumentException("Item already exists in the db.");
        }

        // Perform validation based on item type (e.g., JSON or XML validation)
        if (itemType == 1 && !StringHelper.IsJson(itemContent))
            throw new FormatException("Item content type must be json.");
        else if (itemType == 2 && !StringHelper.IsXml(itemContent))
            throw new FormatException("Item content type must be xml.");

        var item = new Item
        {
            Name = itemName,
            Content = itemContent,
            Type = itemType
        };
        await _dbContext.Items.AddAsync(item);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> Retrieve(string itemName)
    {
        if (_dbContext == null)
        {
            throw new InvalidOperationException("AppDbContext is not initialized.");
        }

        var item = await _dbContext.Items
            .Where(x => x.Name == itemName)
            .FirstOrDefaultAsync();

        if (item == null)
        {
            throw new KeyNotFoundException("Item does not exist in the db.");
        }

        return item.Content;
    }

    public async Task<int> GetType(string itemName)
    {
        if (_dbContext == null)
        {
            throw new InvalidOperationException("AppDbContext is not initialized.");
        }

        var item = await _dbContext.Items
            .Where(x => x.Name == itemName)
            .FirstOrDefaultAsync();

        if (item == null)
        {
            throw new KeyNotFoundException("Item does not exist in the db.");
        }

        return item.Type;
    }

    public async Task Deregister(string itemName)
    {
        if (_dbContext == null)
        {
            throw new InvalidOperationException("AppDbContext is not initialized.");
        }

        var item = await _dbContext.Items
            .Where(x => x.Name == itemName)
            .FirstOrDefaultAsync();

        if (item == null)
        {
            throw new KeyNotFoundException("Item does not exist in the db.");
        }

        _dbContext.Items.Remove(item);

        await _dbContext.SaveChangesAsync();
    }
}

