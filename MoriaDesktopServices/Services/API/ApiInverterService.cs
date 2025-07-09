using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Services.API
{
    public class ApiInverterService : IApiInverterService
    {
        readonly IApiService _apiService;

        public ApiInverterService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<InverterDo> CreateInverter(string username, InverterDo inverterDo)
        {
            return await _apiService.Post<InverterDo>(username, WebAPIEndpointsProvider.PostInverterPath, null, inverterDo);
        }

        public async Task<bool> DeleteInverter(string username, int id)
        {
            return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteInverterPath, null, id);
        }

        public async Task<InverterDo> GetInverter(string username, int id)
        {
            return await _apiService.Get<InverterDo>(username, WebAPIEndpointsProvider.GetInverterPath, null, null, parameters: id);
        }

        public async Task<IEnumerable<InverterDo>> GetInverters(string username)
        {
            var result = await _apiService.Get<IEnumerable<InverterDo>>(username, WebAPIEndpointsProvider.GetInvertersPath, null, null);
            if (result == null)
                return new List<InverterDo>();

            return result;
        }

        public async Task<InverterDo> UpdateInverter(string username, InverterDo inverterDo)
        {
            return await _apiService.Put<InverterDo>(username, WebAPIEndpointsProvider.PutInverterPath, null, inverterDo);
        }
    }
}
