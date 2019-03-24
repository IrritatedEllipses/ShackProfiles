using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Models
{
    public class ShackProfile
    {
        public int Id { get; set; }

        [Required]
        public string Shackname { get; set; }

        public bool Verified { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TimeZone { get; set; }
        public string SteamName { get; set; }
        public string SteamUrl { get; set; }
        public string DiscordId { get; set; }
        public string PSNName { get; set; }
        public string XboxGamertag { get; set; }
        public string NintendoId { get; set; }
        public string OriginId { get; set; }
        public string BattlenetId { get; set; }
        public string UplayId { get; set; }
        public string EpicGamesId { get; set; }
    }
}
