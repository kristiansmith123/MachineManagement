using MachineManagement.Portal.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineManagement.Portal.Domain.Interfaces
{
    public interface IVirtualMachineService
    {
        void Add(VirtualMachine virtualMachine);
        void Edit(VirtualMachine virtualMachine);
        void Delete(VirtualMachine virtualMachine);
        Task<IEnumerable<VirtualMachine>> GetAllAsync();
        Task<VirtualMachine> GetByIdAsync(int id);
        Task<bool> IsOnlineAsync(string ipAddress);
        Task<bool> ControlPowerRemoteAsync(string machineName, string machineGroup, string action);
        bool ControlPowerLocal(string machineName, string command);
    }
}
