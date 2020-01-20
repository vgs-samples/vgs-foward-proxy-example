using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vgs_card_example_asp_net.Controllers
{
    [Route("[controller]")]
    public class PostController : Controller
    {
        // GET: post/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET post/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        /// Forward proxy example, requires the cert.pem be installed on the operation system (see Dockerfile example)
        // POST post/
        [HttpPost]
        public async Task<string> Outbound()
        {
            string aliasData = "no payload in body!";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                aliasData = await reader.ReadToEndAsync();
            }

            NetworkCredential credentials = new NetworkCredential("<username>", "<password>");
            WebProxy proxy = new WebProxy("http://<tenant id>.sandbox.verygoodproxy.com:8080", false)
            {
                UseDefaultCredentials = false,
                Credentials = credentials,
            };

            var handler = new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };

            HttpClient httpClient = new HttpClient(handler);
            StringContent content = new StringContent(aliasData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://echo.apps.verygood.systems/post", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody)["data"].ToObject<string>();
        }


        // PUT post/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE post/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
