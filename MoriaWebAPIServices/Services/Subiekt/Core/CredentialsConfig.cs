using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaWebAPIServices.Services.Subiekt.Core;

public class CredentialsConfig
{
    public string DatabaseName { get; set; } = string.Empty;
    public string DatabaseUsername { get; set; } = string.Empty;
    public string DatabasePassword { get; set; } = string.Empty;
    public bool IsWindowsAuthenticated { get; set; } = false;
    public string ServerName { get; set; } = string.Empty;
    public string SubiektUsername { get; set; } = string.Empty;
    public string SubiektPassword { get; set; } = string.Empty;
}
