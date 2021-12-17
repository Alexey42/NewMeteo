using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace NewMeteoWeb.Pages
{
    public class UserListModel : PageModel
    {
        [BindProperty]
        public List<User> list { get; set; }

        public async Task<IActionResult> OnGet()
        {
            HttpClient client = new HttpClient();
            var webRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8888/getallusers");
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            list = JsonConvert.DeserializeObject<List<User>>(respText);

            return Page();
        }
    }
}
