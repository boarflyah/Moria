using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaWebAPIServices.Services.Subiekt.Interfaces;
public interface ICredentialsService
{
    string GetServerName();
    string GetDatabaseName();
    bool GetIsWindowsAuthenticated();
    string GetDatabaseUsername();
    string GetDatabasePassword();
    string GetMoriaUsername();
    string GetMoriaPassword();
}
