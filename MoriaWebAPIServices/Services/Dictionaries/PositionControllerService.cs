using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class PositionControllerService
{
    private readonly ApplicationDbContext _context;

    public PositionControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PositionDo> CreatePosition(PositionDo position)
    {
        var createdPosition = new Position
        {
            Name = position.Name,
            Code = position.Code
        };

        _context.Positions.Add(createdPosition);
        await _context.SaveChangesAsync();

        position.Id = createdPosition.Id;
        return position;
    }

    public async Task<PositionDo?> GetPositionById(int id)
    {
        var searchPosition = await _context.Positions.FindAsync(id);
        if (searchPosition == null) return null;

        return new PositionDo
        {
            Id = searchPosition.Id,
            Name = searchPosition.Name,
            Code = searchPosition.Code
        };
    }

    public async Task<List<PositionDo>> GetAllPositions()
    {
        return await _context.Positions
            .Select(position => new PositionDo
            {
                Id = position.Id,
                Name = position.Name,
                Code = position.Code
            })
            .ToListAsync();
    }

    public async Task<PositionDo?> EditPosition(PositionDo position)
    {       
        var searchPosition = await _context.Positions.FindAsync(position.Id);
        if (searchPosition == null) return null;

        searchPosition.Name = position.Name;
        searchPosition.Code = position.Code;

        await _context.SaveChangesAsync();
        return position;
    }

    public async Task<bool> DeletePosition(int id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null) return false;

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return true;
    }
}
