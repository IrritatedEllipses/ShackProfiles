using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShackProfiles.Models;

namespace ShackProfiles.Data
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        public async Task<bool> ValidateShackname(ProfileToModify profile)
        {
            var client = new HttpClient();
            var verifyUri = new Uri("https://winchatty.com/v2/verifyCredentials/");

            var credsToVerify = new FormUrlEncodedContent(new[]
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
    }
}
