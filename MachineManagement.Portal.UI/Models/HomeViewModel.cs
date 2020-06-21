using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineManagement.Portal.UI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<VirtualMachine> VirtualMachines { get; set; }
    }
}