using ShackProfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Data
{
    public interface IAuthorizeRepository
    {
        Task<bool> ValidateShackname(ProfileToModify profile);
    }
}
