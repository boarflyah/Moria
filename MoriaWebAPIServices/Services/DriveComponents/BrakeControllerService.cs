using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPIServices.Services.DriveComponents
{
    public class BrakeControllerService : IBrakeControllerService
    {
        readonly ApplicationDbContext _context;
        readonly ModelsCreator _creator;
        public BrakeControllerService(ApplicationDbContext context, ModelsCreator creator)
        {
            _context = context;
            _creator = creator;
        }
        public async Task<BrakeDo> CreateBrake(BrakeDo brakeDo)
        {
            var createdBrake = await _creator.CreateBrake(brakeDo);

            _context.Brakes.Add(createdBrake);
            await _context.SaveChangesAsync();
            brakeDo.Id = createdBrake.Id;

            return brakeDo;
        }

        public async Task<bool> DeleteBrake(int id)
        {
            var brake = await _context.Brakes.FindAsync(id);
            if (brake == null) return false;

            _context.Brakes.Remove(brake);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<BrakeDo> EditBrake(BrakeDo brakeDo)
        {
            if (brakeDo == null) throw new ArgumentNullException(nameof(brakeDo));

            var existing = await _context.Brakes.FindAsync(brakeDo.Id);


            if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

            await _creator.UpdateBrake(existing, brakeDo);

            await _context.SaveChangesAsync();

            return _creator.GetBrakeDo(existing);
        }

        public async Task<IEnumerable<BrakeDo>> GetAllBrakes()
        {
            List<BrakeDo> result = new();
            foreach (var brake in _context.Brakes)
            {
                result.Add(_creator.GetBrakeDo(brake));
            }

            return result;
        }

        public async Task<BrakeDo> GetBrakeById(int id)
        {
            var brake = await _context.Brakes.FindAsync(id);
            if (brake == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
            return _creator.GetBrakeDo(brake);
        }
    }
}
