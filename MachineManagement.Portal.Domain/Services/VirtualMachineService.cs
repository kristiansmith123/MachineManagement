using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineManagement.Portal.Domain.Services
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private readonly IReadWriteRepository<VirtualMachine> _virtualMachineRepository;

        public VirtualMachineService(IReadWriteRepository<VirtualMachine> virtualMachineRepository)
        {
            _virtualMachineRepository = virtualMachineRepository;
        }
        public void Add(VirtualMachine virtualMachine)
        {
            _virtualMachineRepository.Add(virtualMachine);
        }

        public void Edit(VirtualMachine virtualMachine)
        {
            _virtualMachineRepository.Edit(virtualMachine);
        }

        public Task<IEnumerable<VirtualMachine>> GetAllAsync()
        {
            return _virtualMachineRepository.GetAllAsync();
        }

        public Task<VirtualMachine> GetByIdAsync(int id)
        {
            return _virtualMachineRepository.GetByIdAsync(id);
        }

        public void Delete(VirtualMachine virtualMachine)
        {
            _virtualMachineRepository.Delete(virtualMachine);
        }
    }
}
