using AutoMapper;
using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.Domain.Services;
using MachineManagement.Portal.UI.Models;
using Microsoft.Azure.Management.BatchAI.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Management;
using System.Web.Mvc;

namespace MachineManagement.Portal.UI.Controllers
{
    public class MachinesController : Controller
    {
        private readonly VirtualMachineService _virtualMachineService;
        private readonly IMapper _mapper;
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public MachinesController(VirtualMachineService virtualMachineService, IMapper mapper)
        {
            _virtualMachineService = virtualMachineService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = new HomeViewModel
            {
                VirtualMachines = await _virtualMachineService.GetAllAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            var model = _mapper.Map<VirtualMachine, MachineViewModel>(await _virtualMachineService.GetByIdAsync(id));

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            var model = new MachineViewModel();

            if (id.HasValue)
            {
                model = _mapper.Map<VirtualMachine, MachineViewModel>(await _virtualMachineService.GetByIdAsync(id.Value));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MachineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id == 0)
            {
                _virtualMachineService.Add(_mapper.Map<MachineViewModel, VirtualMachine>(model));
            }
            else
            {
                _virtualMachineService.Edit(_mapper.Map<MachineViewModel, VirtualMachine>(model));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _virtualMachineService.GetByIdAsync(id);
            _virtualMachineService.Delete(entity);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> IsOnline(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(await _virtualMachineService.IsOnlineAsync(ipAddress), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ConnectToProxy(string ip, string proxyIp)
        {
            var t = new test();
            t.RequestInfo("52.236.136.191", "madmin", "bi&&estofms175");
        }

        [HttpPost]
        public async Task<ActionResult> ControlPower(string localMachineName, string remoteMachineName, string remoteMachineGroup, string action)
        {
            if (string.IsNullOrEmpty(localMachineName))
            {
                return Json("name missing");
            }

            if (string.IsNullOrEmpty(remoteMachineName))
            {
                return Json("remote name missing");
            }

            if (string.IsNullOrEmpty(remoteMachineGroup))
            {
                return Json("group missing");
            }

            if (string.IsNullOrEmpty(action))
            {
                return Json("action missing");
            }

            if (action != "start" && action != "stop")
            {
                return Json("action wrong");
            }

            if (await _virtualMachineService.ControlPowerRemoteAsync(remoteMachineName, remoteMachineGroup, action))
            {
                if (_virtualMachineService.ControlPowerLocal(localMachineName, action))
                {
                    return Json("success");
                }
                else
                {
                    return Json("error local");
                }
            }
            else
            {
                return Json("error remote");
            }

        }

    }
}