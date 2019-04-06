using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Helpers
{
    public class ShackProfileParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value ; }
        }

        public bool SteamName { get; set; }
        public bool DiscordId { get; set; }
        public bool PSN { get; set; }
        public bool XboxGamertag { get; set; }
        public bool NintendoId { get; set; }
        public bool OriginId { get; set; }
        public bool BattlenetId { get; set; }
        public bool UplayId { get; set; }
        public bool EpicGamesId { get; set; }
    }
}
