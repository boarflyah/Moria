using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MoriaDesktop.Services;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Views.Orders.Window;

public partial class SetProductionDateWindowView : System.Windows.Window, INotifyPropertyChanged
{
    #region Property

    public DateTime? TechnicalDrawingPlanned { get; set; }
    public DateTime? TechnicalDrawingStarted { get; set; }
    public DateTime? TechnicalDrawingCompleted { get; set; }
    public DateTime? CuttingPlanned { get; set; }
    public DateTime? CuttingStarted { get; set; }
    public DateTime? CuttingCompleted { get; set; }
    public DateTime? MetalCliningPlanned { get; set; }
    public DateTime? MetalCliningStarted { get; set; }
    public DateTime? MetalCliningCompleted { get; set; }
    public DateTime? PaintingPlanned { get; set; }
    public DateTime? PaintingStarted { get; set; }
    public DateTime? PaintingCompleted { get; set; }
    public DateTime? PlannedMachineAssembled { get; set; }
    public DateTime? MachineAssembledStarted { get; set; }
    public DateTime? MachineAssembled { get; set; }
    public DateTime? PlannedMachineAssembledAll { get; set; }
    public DateTime? MachineAssembledAllStarted { get; set; }
    public DateTime? MachineAssembledAllCompleted { get; set; }
    public DateTime? PlannedMachineWiredAndTested { get; set; }
    public DateTime? MachineWiredAndTestedStarted { get; set; }
    public DateTime? MachineWiredAndTested { get; set; }
    public DateTime? PlannedTransport { get; set; }
    public DateTime? MachineReleased { get; set; }
    public DateTime? TransportOrdered { get; set; }

    public DateTime? WeldingPlanned { get; set; }
    public DateTime? WeldingStarted { get; set; }
    public DateTime? WeldingCompleted { get; set; }
 
    public DateTime? DueDate { get; set; }

    public PermissionDo Permission_TechnicalDrawingCompleted { get; set; }
    public PermissionDo Permission_CuttingCompleted { get; set; }
    public PermissionDo Permission_WeldingCompleted { get; set; }
    public PermissionDo Permission_MetalCliningCompleted { get; set; }
    public PermissionDo Permission_PaintingCompleted { get; set; }
    public PermissionDo Permission_MachineAssembled { get; set; }
    public PermissionDo Permission_MachineWiredAndTested{ get; set; }   
    public PermissionDo Permission_MachineReleased{ get; set; }
    public PermissionDo Permission_TransportOrdered { get; set; }
    public PermissionDo Permission_PlannedMachineAssembled { get; set; }
    public PermissionDo Permission_PlannedMachineWiredAndTested { get; set; }
    public PermissionDo Permission_PlannedTransport { get; set; }
    public PermissionDo Permission_DueDate{ get; set; }

    
    #endregion

    public SetProductionDateWindowView(List<PermissionDo> permissions)
    {
        this.DataContext = this;
        InitializeComponent();
        InitializePermissionsFromList(permissions);
    }

    private void InitializePermissionsFromList(List<PermissionDo> permissions)
    {
        string modelName = nameof(OrderItemDetailView).Replace("DetailView", "");

        var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            try
            {
                if (prop.PropertyType == typeof(PermissionDo))
                {
                    string permissionPropertyName = $"{modelName}_{prop.Name.Substring(11)}";

                    var permission = permissions
                        .FirstOrDefault(x => x.PropertyName.Equals(permissionPropertyName));

                    if (permission != null)
                        prop.SetValue(this, permission);
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }

    private void Accept_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}
