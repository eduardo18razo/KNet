using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiiniHelp
{
    public delegate void DelegateCerrarModal();
    public interface IControllerModal
    {
        event DelegateCerrarModal OnCerraModal;
    }
}