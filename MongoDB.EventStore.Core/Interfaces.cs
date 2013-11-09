using System.IO;
using System.Linq;
using System;
using System.Runtime.InteropServices;

namespace MongoDB.EventStore
{

    public interface IApplicationService
    {
        void Execute(ICommand cmd);
    }

    public interface IEvent { }

    public interface ICommand { }

    public interface IIdentity { }

   
}