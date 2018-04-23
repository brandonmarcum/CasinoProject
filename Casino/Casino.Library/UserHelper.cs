using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Casino.Library.Models;
using Newtonsoft.Json;

namespace Casino.Library
{
    public class UserHelper
    {
        public UserHelper()
        {
			
        }

		public static async Task<User> GetUser(string username)
		{
			var client = new HttpClient();

			var result = await client.GetAsync("http://localhost:5000/api/user/userget/" + username);

			if(result.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync());
			}
			else
			{
				return null;
			}
		}
			public static void PostUserAsync(User user)
		{
			var client = new HttpClient();

			var content = JsonConvert.SerializeObject(user);            
			var stringPost = new StringContent(content,Encoding.UTF8,"application/json");

			Console.WriteLine("Attempting to send user: " + user.Username);
			
			client.PostAsync("http://localhost:5000/api/user/userpost", stringPost);

		}

		public static async void RegisterUserAsync(User user)
		{
			var client = new HttpClient();

			var content = JsonConvert.SerializeObject(user);            
			var stringPost = new StringContent(content,Encoding.UTF8,"application/json");

			Console.WriteLine("Attempting to send user: " + user.Username);
			
			await client.PostAsync("http://localhost:5000/api/user/userregister", stringPost);
		}
    }
}
