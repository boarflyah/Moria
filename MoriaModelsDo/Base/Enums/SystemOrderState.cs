using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaModelsDo.Base.Enums;

public enum SystemOrderState
{
    None = 0,
    TechnicalDrawingCompleted = 10,
    CuttingCompleted = 20,
    WeldingCompleted = 30,
    MetalCliningCompleted = 40,
    PaintingCompleted = 50,
    ElectricalDiagramCompleted = 60,
    ElectricaCabinetCompleted = 70,
    MachineAssembled = 80,
    MachineWiredAndTested = 90,
    TransportOrdered = 100,
    MachineReleased = 110
}
