using MachineManagement.Portal.Domain.Entities;
using MachineManagement.Portal.Domain.Services;
using MachineManagement.Portal.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MachineManagement.Portal.UI.Controllers
{
    public class MachinesController : Controller
    {
        private readonly VirtualMachineService _virtualMachineService;

        public MachinesController(VirtualMachineService virtualMachineService)
        {
            _virtualMachineService = virtualMachineService;
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
            var entity = await _virtualMachineService.GetByIdAsync(id);

            var model = new MachineViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Ip = entity.Ip,
                Notes = entity.Notes
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            var model = new MachineViewModel();

            if (id.HasValue)
            {
                var entity = await _virtualMachineService.GetByIdAsync(id.Value);

                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Ip = entity.Ip;
                model.Notes = entity.Notes;
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
                _virtualMachineService.Add(new VirtualMachine
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ip = model.Ip,
                    Notes = model.Notes
                });
            }
            else
            {
                _virtualMachineService.Edit(new VirtualMachine
                {
                    Id = model.Id,
                    Name = model.Name,
                    Ip = model.Ip,
                    Notes = model.Notes
                });
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

    }
}