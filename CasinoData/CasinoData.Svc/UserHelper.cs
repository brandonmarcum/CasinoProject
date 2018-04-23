using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CasinoData.Db
{
    public class UserHelper
    {
        public UserHelper()
        {
			
        }

		public static async Task<Users> GetUserAsync()
		{
			var client = new HttpClient();

			var result = await client.GetAsync("http://localhost:5000/api/user/userpost");

			if(result.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<Users>(await result.Content.ReadAsStringAsync());
			}
			else
			{
				return null;
			}
		}
        public static async Task<Users> RegisterUserAsync()
		{
			var client = new HttpClient();

			var result = await client.GetAsync("http://localhost:5000/api/user/userregister");

			if(result.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<Users>(await result.Content.ReadAsStringAsync());
			}
			else
			{
				return null;
			}
		}
    }
}
