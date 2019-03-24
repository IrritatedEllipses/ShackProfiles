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
using ShackProfiles.Models.Dtos;

namespace ShackProfiles.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProfileRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShackProfile> AddProfile(ProfileToModify profileToCreate)
        {
            if (await ValidateShackname(profileToCreate))
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
            if (await ValidateShackname(profile))
            {
                var profileToDelete = await _context.ShackProfiles.FirstOrDefaultAsync(x => x.Shackname == profile.Shackname.ToUpper());

                _context.ShackProfiles.Remove(profileToDelete);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<bool> ProfileExists(string shacker)
        {
            shacker = shacker.ToUpper();
            var shackerExists = await _context.ShackProfiles.FirstOrDefaultAsync(x => x.Shackname == shacker);

            if (shackerExists != null)
                return true;
            return false;
        }

        public Task<ShackProfile> UpdateProfile(ProfileToModify profile)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateShackname(ProfileToModify profile)
        {
            var client = new HttpClient();
            var verifyUri = new Uri("https://winchatty.com/v2/verifyCredentials/");

            var credsToVerify = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("username", profile.Shackname),
                new KeyValuePair<string, string>("password", profile.Password),
            });

            var response = await client.PostAsync(verifyUri, credsToVerify);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                WinChattyVerify result = JsonConvert.DeserializeObject<WinChattyVerify>(res);

                return result.IsValid;
            }
            return false;
        }

        public async Task<ShackProfile> ViewProfile(ProfileToView shackname)
        {
            var shacker = shackname.Shackname.ToUpper();
            if (await ProfileExists(shacker))
            {
                var profileToView = await _context.ShackProfiles.FirstOrDefaultAsync(x => x.Shackname == shacker);
                return profileToView;
            }
            return null;
        }

        public async Task<ShackProfile[]> ViewProfiles()
        {
            var profiles = _context.ShackProfiles.AsQueryable();
            return await profiles.ToArrayAsync();
        }
    }
}
