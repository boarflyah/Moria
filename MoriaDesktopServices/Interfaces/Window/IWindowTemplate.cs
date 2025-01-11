using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoriaDesktopServices.Interfaces.Window;

public interface IWindowTemplate
{
    public ICommand CloseCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand SaveAndCloseCommand { get; set; }
    public object CreatedObject { get; set; }
    private void CloseWindow()
    {
        //this.Close();
    }
}
