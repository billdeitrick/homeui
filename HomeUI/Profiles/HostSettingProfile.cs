using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeUI.DTOs;
using HomeUI.Models;
using AutoMapper;

namespace HomeUI.Profiles
{
    public class HostSettingProfile : Profile
    {
        public HostSettingProfile()
        {
            CreateMap<WOLHostSettingModel, WOLHostSettingsDTO>();
        }
    }
}
