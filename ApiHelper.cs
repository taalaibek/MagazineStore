using System.Net.Http.Headers;

namespace MagazineStore
{
    public class ApiHelper
    {
        public static HttpClient httpClient { get; set; }

        public static void Init()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://magazinestore.azurewebsites.net/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

