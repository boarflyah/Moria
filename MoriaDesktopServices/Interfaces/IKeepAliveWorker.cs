using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Interfaces;

public interface IKeepAliveWorker : IDisposable
{
    void LockObject(LockHelper lockHelper);
    void RemoveLock(int objectID);

    void Start();
}
