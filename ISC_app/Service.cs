using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
	}
}
