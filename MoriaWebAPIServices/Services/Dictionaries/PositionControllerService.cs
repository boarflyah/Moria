using Microsoft.EntityFrameworkCore;
using MoriaBaseModels.Models;
using MoriaBaseServices;
using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class PositionControllerService : IPositionControllerService
{
    private readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public PositionControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
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
        var searchPosition = await _context.Positions.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        if (searchPosition == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        return _creator.GetPosition(searchPosition);
    }

    public async Task<List<PositionDo>> GetAllPositions()
    {
        return await _context.Positions
            .Select(position => _creator.GetPosition(position))
            .ToListAsync();
    }

    public async Task<PositionDo?> EditPosition(PositionDo position)
    {
        var searchPosition = await _context.Positions.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == position.Id);
        if (searchPosition == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdatePosition(searchPosition, position);
        await _context.SaveChangesAsync();
        return _creator.GetPosition(searchPosition);
    }

    public async Task<bool> DeletePosition(int id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null) return false;

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<LookupModel>> GetLookupObjects(int latestId, int pageSize)
    {
        return await _context.Positions
            .OrderBy(x => x.Id)
            //.SkipWhile(x => x.Id < latestId)
            .Take(pageSize)
            .Select(position => position.GetLookupObject())
            .ToListAsync();
    }

}
