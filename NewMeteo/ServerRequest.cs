using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static NewMeteo.ServerRequestForms;

namespace NewMeteo
{
    class ServerRequest
    {
        public string ErrorText = "";

        public async Task<string> Auth(string _name, string _password, string _type)
        {
            HttpClient client = new HttpClient();
            var u = new AuthRequestForm { Name = _name, Password = _password, Type = _type };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:8888/auth", data);
            var respText = response.Content.ReadAsStringAsync().Result;

            return respText;
        }

        public async Task<string> SendMap(string _name, Map _map)
        {
            HttpClient client = new HttpClient();
            var u = new MapRequestForm { Name = _name, Bytes = _map.Image.ToBytes(), Values = _map.values };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:8888/sendmap", data);
            var respText = response.Content.ReadAsStringAsync().Result;

            return respText;
        }

        public async Task<string> GetMap(string _name)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:8888/getmap" + "/" + _name);
            var respText = response.Content.ReadAsStringAsync().Result;

            return respText;
        }
    }
}
