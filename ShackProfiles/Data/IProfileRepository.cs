using ShackProfiles.Helpers;
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
        Task<ShackProfile> ViewProfile(string shackname);
        Task<List<ShackProfile>> ViewProfiles();
        Task<PagedList<ShackProfile>> ViewProfiles(ShackProfileParams profileParams);
        Task<bool> DeleteProfile(ProfileToModify profile);
        Task<bool> ProfileExists(string shacker);
    }
}
