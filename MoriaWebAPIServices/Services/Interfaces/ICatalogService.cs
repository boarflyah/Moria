using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaWebAPIServices.Services.Interfaces;
public interface ICatalogService
{
    Task<bool> CreateCatalogs(string orderSymbol);
}
