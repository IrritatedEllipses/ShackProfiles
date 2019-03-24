using ShackProfiles.Models;
using ShackProfiles.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Data
{
    public interface IProfileRepository
    {
        Task<ShackProfile> AddProfile(ProfileToModify profileToCreate);
        Task<ShackProfile> UpdateProfile(ProfileToModify profile);
        Task<ShackProfile> ViewProfile(ProfileToView shackname);
        Task<ShackProfile[]> ViewProfiles();
        Task<bool> DeleteProfile(ProfileToModify profile);
        Task<bool> ProfileExists(string shacker);
        Task<bool> ValidateShackname(ProfileToModify profile);
    }
}
