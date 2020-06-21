using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MachineManagement.Portal.UI.Startup))]

namespace MachineManagement.Portal.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapper.Mapper.Initialize(c =>
            {
                c.AddProfile<UiModelProfile>();
            });
        }
    }
}