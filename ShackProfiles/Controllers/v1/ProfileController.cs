using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShackProfiles.Data;
using ShackProfiles.Helpers;
using ShackProfiles.Models;
using ShackProfiles.Models.Dtos;

namespace ShackProfiles.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _repo;

        public ProfileController(IProfileRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateProfile([FromBody]ProfileToModify profileToCreate)
        {
            if (profileToCreate == null)
            {
                throw new ArgumentNullException(nameof(profileToCreate));
            }

            var res = await _repo.AddProfile(profileToCreate);

            if (res.Verified)
            {
                return Ok(res);
            }

            return BadRequest("Could not create Profile, see TFO for details");
        }

        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile([FromBody]ProfileToModify profileToDelete)
        {
            if (profileToDelete == null)
            {
                throw new ArgumentNullException(nameof(profileToDelete));
            }

            var res = await _repo.DeleteProfile(profileToDelete);

            if (res)
                return Ok("Profile is deleted");
            return BadRequest("Could not delete Profile, see TFO for details");
        }

        [HttpGet("{shackname}")]
        public async Task<IActionResult> ViewProfile(string shackname)
        {
            var shackProfile = await _repo.ViewProfile(shackname);

            if (shackProfile != null)
                return Ok(shackProfile);
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfiles([FromQuery]ShackProfileParams profileParams)
        {
            var profiles = await _repo.ViewProfiles(profileParams);

            Response.AddPagination(profiles.CurrentPage, profiles.PageSize, 
                profiles.TotalCount, profiles.TotalPages);

            return Ok(profiles);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(ProfileToModify profile)
        {
            var result = await _repo.UpdateProfile(profile);

            return Ok(result);
        }
    }
}