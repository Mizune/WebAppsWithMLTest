using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.IO;
using System.Threading.Tasks;


namespace MLTest2.Controllers
{
    public class MovieRecommendController : Controller
    {
        public static string Result;
        // GET: MovieRecommand
        public ActionResult Index()
        {
            InvokeRequestResponseService().Wait();
            ViewBag.Result = Result;
            return View();
        }

        public ActionResult Normal()
        {
            return View("Index");
        }
        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() { 
                        { 
                            "input1", 
                            new StringTable() 
                            {
                                ColumnNames = new string[] {"UserId"},
                                Values = new string[,] {  { "0" },  { "0" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>() { }
                };
                const string apiKey = "W2z1n1HM1OUMhTIQ3h7D0FjStDSrdAkEwLQUm+fKX+zOwU5RMG5umHsC4UBip9KCfmn+ZQA6kUdJ95VmYPwaFg=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/e269acd00cd84dc19871e3104ee10b2d/services/91886e7ab926423f95c33b0a91e45a1e/execute?api-version=2.0&details=true");
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);


                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    Result = responseContent;
                }
            }
        }

        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }
    }

}