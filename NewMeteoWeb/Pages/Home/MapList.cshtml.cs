using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace NewMeteoWeb.Pages.Home
{
    public class MapListModel : PageModel
    {
        [BindProperty]
        public List<MapDB> list { get; set; }

        public async Task<IActionResult> OnGet()
        {
            HttpClient client = new HttpClient();
            var webRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8888/getallmaps");
            var response = client.Send(webRequest);
            var respText = response.Content.ReadAsStringAsync().Result;
            list = JsonConvert.DeserializeObject<List<MapDB>>(respText);

            return Page();
        }
    }
}
