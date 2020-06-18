using MachineManagement.Portal.Domain.Entities;
using System.Collections.Generic;

namespace MachineManagement.Portal.UI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<VirtualMachine> VirtualMachines { get; set; }
    }
}