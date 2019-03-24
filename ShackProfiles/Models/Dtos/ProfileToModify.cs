using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Models
{
    public class ProfileToModify : ShackProfile
    {
        [Required]
        public string Password { get; set; }
    }
}
