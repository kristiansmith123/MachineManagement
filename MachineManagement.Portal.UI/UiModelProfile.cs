using AutoMapper;
using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.UI.Models;

namespace MachineManagement.Portal.UI
{
    public class UiModelProfile : Profile
    {
        public UiModelProfile()
        {
            CreateMap<VirtualMachine, MachineViewModel>();
        }
    }
}