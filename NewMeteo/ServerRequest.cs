using Newtonsoft.Json;
using OpenCvSharp;
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

        /**
		 * <summary>
		 * Send current map to server
		 * </summary>
		 */
        public async Task<string> SendMap(string _name, Map _map)
        {
            Mat mat = _map.Image;
            var pixels = MatToBytes(mat);

            HttpClient client = new HttpClient();
            var u = new MapRequestForm { Name = _name, Bytes = pixels, Values = _map.values };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:8888/sendmap", data);
            var respText = response.Content.ReadAsStringAsync().Result;

            return respText;
        }

        /**
		 * <summary>
		 * Return desired map from server
		 * </summary>
		 */
        public async Task<Map> GetMap(string _name)
        {
            var result = new Map();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:8888/getmap" + "/" + _name);
            var respText = response.Content.ReadAsStringAsync().Result;
            
            if (respText != "Not found")
            {
                var reqData = JsonConvert.DeserializeObject<MapRequestForm>(respText);
                var x = reqData.Values.GetUpperBound(0) + 1;
                var y = reqData.Values.GetUpperBound(1) + 1;
                result = new Map(new Mat(y, x, MatType.CV_8UC3, reqData.Bytes), reqData.Name, reqData.Values);
            }

            return result;
        }

        public byte[] MatToBytes(Mat mat)
        {
            Vec3b[] pixels;
            mat.GetArray(out pixels);
            byte[] result = new byte[pixels.Length * 3];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = pixels[i / 3][i % 3];
            }

            return result;
        }
    }
}
