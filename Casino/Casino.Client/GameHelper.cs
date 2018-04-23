using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Casino.Client.Models;
using Casino.Library.Games;
using Casino.Library.Models;
using Newtonsoft.Json;

namespace Casino.Client
{
    public class GameHelper
    {
        public GameHelper()
        {
			
        }

		public static async Task<Blackjack> GetBlackjackAsync()
		{
			var client = new HttpClient();

			var result = await client.GetAsync("http://localhost:5000/api/blackjack/blackjackgameget");
			

			if(result.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<Blackjack>(await result.Content.ReadAsStringAsync());
			}
			else
			{
				return null;
			}
		}
		public static async void PostBlackJackAsync(Blackjack blackjack)
		{
			var client = new HttpClient();

			var content = JsonConvert.SerializeObject(blackjack);            
			var stringPost = new StringContent(content,Encoding.UTF8,"application/json");
			
			await client.PostAsync("http://localhost:5000/api/blackjack/blackjackgamepost", stringPost);
		}


			public static async Task<User> GetUserAsync()
			{
			var client = new HttpClient();

			var result = await client.GetAsync("http://localhost:5000/api/user/userget");
			

			if(result.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync());
			}
			else
			{
				return null;
			}
		}

		public static async void PostUserAsync(User user)
		{
			var client = new HttpClient();

			var content = JsonConvert.SerializeObject(user);            
			var stringPost = new StringContent(content,Encoding.UTF8,"application/json");
			
			await client.PostAsync("http://localhost:5000/api/user/userpost", stringPost);
		}

		public static BlackJackViewModel GetBlackjackViewModel()
		{
			BlackJackViewModel model = new BlackJackViewModel();
			model.Blackjack = GetBlackjackAsync().GetAwaiter().GetResult();
			model.User = GetUserAsync().GetAwaiter().GetResult();

			return model;
		}
		public static void PostBlackjackViewModel(BlackJackViewModel model)
		{
			PostBlackJackAsync(model.Blackjack);
			PostUserAsync(model.User);

		}
    }
}
