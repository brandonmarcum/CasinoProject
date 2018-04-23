using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CasinoService.Api.Models;
using Newtonsoft.Json;

namespace CasinoService.Api.Models
{
    public class UserHelper
    {
        public UserHelper()
        {
			
        }

		public static async Task<User> GetUserDataAsync(string username)
		{
			var client = new HttpClient();

			var result = await client.GetAsync("http://localhost:5002/api/user/datauserget/" + username);

			if(result.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync());
			}
			else
			{
				return null;
			}
		}
    }
}
