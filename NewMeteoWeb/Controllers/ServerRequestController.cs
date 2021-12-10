using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static NewMeteoWeb.ServerRequestForms;

namespace NewMeteoWeb.Controllers
{

    public class ServerRequestController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Auth(string name, string password, string type)
        {
            HttpClient client = new HttpClient();
            var u = new AuthRequestForm { Name = name, Password = password, Type = type };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var webRequest = new HttpRequestMessage {
                RequestUri = new Uri("http://localhost:8888/auth"),
                Content = data
            };
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            if (respText  == "ok")
            {
                await SetCookieAuth(name);
                return View("Authorize");
            }
            else 
                return View();
        }

        private async Task SetCookieAuth(string name)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, name)
                };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetMap(string name)
        {
            //var result = new Map();
            string result = "";
            HttpClient client = new HttpClient();
            var webRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8888/getmap" + "/" + name);
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            result = respText;
            if (respText != "Not found")
            {
                var reqData = JsonConvert.DeserializeObject<MapRequestForm>(respText);
                var x = reqData.Values.GetUpperBound(0) + 1;
                var y = reqData.Values.GetUpperBound(1) + 1;
                //result = new Map(new Mat(y, x, MatType.CV_8UC3, reqData.Bytes), reqData.Name, reqData.Values);
            }
            ContentResult res = new ContentResult();
            res.Content = result;
            return res;
        }
    }
}
