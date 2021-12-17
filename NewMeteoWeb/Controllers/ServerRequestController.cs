using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static NewMeteoWeb.Pages.AuthorizeModel;
using static NewMeteoWeb.ServerRequestForms;

namespace NewMeteoWeb.Controllers
{

    public class ServerRequestController : Controller
    {
        [AllowAnonymous]
        public async Task<ActionResult> Authorize(string name, string password)
        {
            HttpClient client = new HttpClient();
            var u = new AuthRequestForm { Name = name, Password = password, Type = "Sign in" };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var webRequest = new HttpRequestMessage {
                RequestUri = new Uri("http://localhost:8888/auth"),
                Content = data
            };
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            if (respText != "ok")
            {
                
                return new ContentResult { Content = respText };
            }
            else
            {
                SessionManager.CurrentUser = name;
                Response.Cookies.Append("currentUser", name);
                return new ContentResult { Content = "https://" + Request.Host.Value };
            }
        }

        public async Task<ActionResult> LogOut()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                if (cookie.Key == "currentUser")
                    Response.Cookies.Delete(cookie.Key);
            }
            SessionManager.CurrentUser = "";
            return new ContentResult { Content = "https://" + Request.Host.Value };
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(string name, string password, string type)
        {
            if (!Request.Cookies.ContainsKey("currentUser"))
            {
                return new ContentResult { Content = "Authorize" };
            }
            HttpClient client = new HttpClient();
            var u = new AuthRequestForm { Name = name, Password = password, Type = type };
            var json = JsonConvert.SerializeObject(u);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var webRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:8888/auth"),
                Content = data
            };
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            return new ContentResult { Content = respText };
        }

        [HttpGet]
        public async Task<ActionResult> DeleteUser()
        {
            if (!Request.Cookies.ContainsKey("currentUser"))
            {
                return new ContentResult { Content = "Authorize" };
            }
            HttpClient client = new HttpClient();
            var webRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:8888/deluser/" + Request.Query.Keys.First())
            };
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            if (respText != "ok")
            {
                return new ContentResult { Content = respText };
            }
            else
            {
                return new ContentResult { Content = "https://" + Request.Host.Value };
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteMap()
        {
            if (!Request.Cookies.ContainsKey("currentUser"))
            {
                return new ContentResult { Content = "Authorize" };
            }
            HttpClient client = new HttpClient();
            var webRequest = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:8888/delmap/" + Request.Query.Keys.First())
            };
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            if (respText != "ok")
            {
                return new ContentResult { Content = respText };
            }
            else
            {
                return new ContentResult { Content = "https://" + Request.Host.Value };
            }
        }

    }
}
