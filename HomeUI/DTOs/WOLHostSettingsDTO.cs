using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.DTOs
{
    public class WOLHostSettingsDTO
    {

        public int id { get; set; }
        public string FriendlyName { get; set; }
        public string MacAddress { get; set; }
        public string BroadcastIP { get; set; }
        public string Description { get; set; }

    }
}
