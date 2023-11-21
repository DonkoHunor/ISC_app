using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISC_app
{
    class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("CVV")]
		public string CVV { get; set; }

		[JsonProperty("Name")]
		public string Name { get; set; }

		[JsonProperty("Balance")]
		public int Balance { get; set; }

		[JsonProperty("Credit_card")]
		public string Credit_card { get; set; }
    }
}
