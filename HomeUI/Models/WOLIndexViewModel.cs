using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HomeUI.DTOs;

namespace HomeUI.Models
{
    public class WOLIndexViewModel
    {
        public int selectedHostId { get; set; }
        public List<WOLHostSettingsDTO> wolHosts { get; set; }
    }
}
