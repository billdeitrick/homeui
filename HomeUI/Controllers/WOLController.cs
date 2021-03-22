using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeUI.Services;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using HomeUI.DTOs;
using Vereyon.Web;

namespace HomeUI.Controllers
{
    public class WOLController : Controller
    {

        private readonly ILogger<WOLController> _logger;
        private readonly IWOLServiceAdapter _wOLService;
        private readonly WOLHostSettingsModel _hostSettings;
        private readonly IMapper _mapper;
        private readonly IFlashMessage _flashMessage;

        public WOLController(ILogger<WOLController> logger, IWOLServiceAdapter wOLService, WOLHostSettingsModel hostSettings, IMapper mapper, IFlashMessage flashMessage)
        {
            _logger = logger;
            _wOLService = wOLService;
            _hostSettings = hostSettings;
            _mapper = mapper;
            _flashMessage = flashMessage;
        }

        // GET: WOLController
        public ActionResult Index()
        {


            var hostSettingsDTOs = new List<WOLHostSettingsDTO>();

            for(int i = 0; i < _hostSettings.WOLHosts.Count; i++)
            {
                WOLHostSettingModel settings = _hostSettings.WOLHosts[i];
                var dto = new WOLHostSettingsDTO();
                _mapper.Map(settings, dto);
                dto.id = i;
                hostSettingsDTOs.Add(dto);
            }

            var viewModel = new WOLIndexViewModel
            {
                wolHosts = hostSettingsDTOs
            };
 

            return View(viewModel);
        }

        // POST: WOLController/Wake
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Wake(WOLIndexViewModel model)
        {

            var hostSettings = new Dictionary<int, WOLHostSettingModel>();

            for (int i = 0; i < _hostSettings.WOLHosts.Count; i++)
            {
                hostSettings.Add(i, _hostSettings.WOLHosts[i]);
            }

            var wakeHost = hostSettings[model.selectedHostId];

            _wOLService.Wake(wakeHost.MacAddress, wakeHost.BroadcastIP);

            _logger.Log(LogLevel.Information, $"Sent packets to wake {wakeHost.FriendlyName}");

            _flashMessage.Confirmation($"🚀 Wakeup call sent for {wakeHost.FriendlyName}");

            return RedirectToAction(nameof(Index));
        }

    }
}
