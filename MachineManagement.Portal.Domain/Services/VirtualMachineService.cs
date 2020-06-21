using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.Domain.Interfaces;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MachineManagement.Portal.Domain.Services
{
    public class VirtualMachineService : IVirtualMachineService
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();
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

        public async Task<bool> IsOnlineAsync(string ipAddress)
        {
            var pingable = false;
            Ping pinger = new Ping();

            try
            {
                var reply = await pinger.SendPingAsync(ipAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public async Task<bool> StartAsync(string machineName, string machineGroup)
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromFile(ConfigurationManager.AppSettings["AzureAuth"]);

            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();
            try
            {
                _Logger.Info(string.Format("Starting VM {0} in group {0}", machineName, machineGroup));

                await azure.VirtualMachines.StartAsync(machineGroup, machineName);
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(e);
                return false;
            }
        }

        //USER MUST BE MEMBER OF HYPERV ADMINISTRATORS GROUP
        public bool StartLocalMachineAsync(string machineName)
        {
            try
            {
                var scope = new ManagementScope(@"root\virtualization\v2");
                var query = new ObjectQuery("SELECT * FROM Msvm_ComputerSystem");
                var vmSearcher = new ManagementObjectSearcher(scope, query);
                var vmCollection = vmSearcher.Get();

                foreach (ManagementObject vm in vmCollection)
                {
                    var vmName = vm["ElementName"].ToString().ToLower();

                    if (vmName.Contains(machineName.ToLower()))
                    {
                        var inParams = vm.GetMethodParameters("RequestStateChange");

                        inParams["RequestedState"] = 2; //Start
                        var result = vm.InvokeMethod("RequestStateChange", inParams, null);

                        //Success or already running
                        if ((UInt32)result["ReturnValue"] == 0 || (UInt32)result["ReturnValue"] == 32775)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

            return true;
        }

        public async Task<bool> StopAsync(string machineName, string machineGroup)
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromFile(ConfigurationManager.AppSettings["AzureAuth"]);

            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();
            try
            {
                _Logger.Info(string.Format("Stopping VM {0} in group {0}", machineName, machineGroup));

                await azure.VirtualMachines.PowerOffAsync(machineGroup, machineName);
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(e);
                return false;
            }
        }
    }
}
