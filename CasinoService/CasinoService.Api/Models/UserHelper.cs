using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

		public static async void RegisterUserDataAsync(User user)
		{
			var client = new HttpClient();

			var content = JsonConvert.SerializeObject(user);            
			var stringPost = new StringContent(content,Encoding.UTF8,"application/json");

			Console.WriteLine("Attempting to send user: " + user.Username);
			
			await client.PostAsync("http://localhost:5002/api/user/datauserregister", stringPost);
		}
    }
}
