using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ISC_app
{
    class Service
    {
        private HttpClient client = new HttpClient();
        private string url = "https://retoolapi.dev/oahQtd/data";

        public List<User> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public User Add(User u)
        {
			StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
			HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
			Debug.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
			return JsonConvert.DeserializeObject<User>(responseMessage.Content.ReadAsStringAsync().Result);
		}

        public bool Delete(User u)
        {
			int id = u.Id;
			HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
			return response.IsSuccessStatusCode;
		}

        public User Update(int id, User u)
        {
			StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
			HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

			string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<User>(responseContent);
		}
	}
}
