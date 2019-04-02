using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ShackProfiles.Models;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShackProfiles.Helpers;

namespace ShackProfiles.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizeRepository _auth;

        public ProfileRepository(DataContext context, IMapper mapper, IAuthorizeRepository auth)
        {
            _context = context;
            _mapper = mapper;
            _auth = auth;
        }

        public async Task<ShackProfile> AddProfile(ProfileToModify profileToCreate)
        {
            if (await _auth.ValidateShackname(profileToCreate))
            {
                var shacker = _mapper.Map<ShackProfile>(profileToCreate);

                shacker.Verified = true;
                shacker.CreatedAt = DateTime.Now;
                shacker.Shackname = shacker.Shackname.ToUpper();

                await _context.ShackProfiles.AddAsync(shacker);
                await _context.SaveChangesAsync();

                return shacker;
            }
            return profileToCreate;
        }

        public async Task<bool> DeleteProfile(ProfileToModify profile)
        {
            if (await _auth.ValidateShackname(profile))
            {
                var profileToDelete = await _context.ShackProfiles
                    .FirstOrDefaultAsync(x => x.Shackname == profile.Shackname.ToUpper());

                _context.ShackProfiles.Remove(profileToDelete);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<bool> ProfileExists(string shacker)
        {
            shacker = shacker.ToUpper();
            var shackerExists = await _context.ShackProfiles
                .FirstOrDefaultAsync(x => x.Shackname == shacker);

            if (shackerExists != null)
                return true;
            return false;
        }

        public async Task<ShackProfile> UpdateProfile(ProfileToModify profile)
        {
            profile.Shackname = profile.Shackname.ToUpper();

            var existingProfile = await _context.ShackProfiles
                .FirstOrDefaultAsync(x => x.Shackname == profile.Shackname);

            _mapper.Map(profile, existingProfile);

            if (await _context.SaveChangesAsync() > 0)
                return profile;

            throw new Exception($"Updating {profile.Shackname} failed");
        }

        public async Task<ShackProfile> ViewProfile(string shackname)
        {
            var shacker = shackname.ToUpper();
            if (await ProfileExists(shacker))
            {
                var profileToView = await _context.ShackProfiles
                    .FirstOrDefaultAsync(x => x.Shackname == shacker);

                return profileToView;
            }
            return null;
        }

        public async Task<List<ShackProfile>> ViewProfiles()
        {
            List<ShackProfile> profiles = await _context.ShackProfiles.ToListAsync<ShackProfile>();

            return profiles;
        }

        public async Task<PagedList<ShackProfile>> ViewProfiles(ShackProfileParams profileParams)
        {
            var profiles = _context.ShackProfiles;

            return await PagedList<ShackProfile>.CreateAsync(profiles, profileParams.PageNumber, profileParams.PageSize);
            
        }
    }
}
