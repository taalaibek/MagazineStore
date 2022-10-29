using System.Text;
using Newtonsoft.Json;

namespace MagazineStore
{
    class Program
    {
        private static readonly List<List<int>> categoryMagazines = new List<List<int>>();

        static async Task Main(string[] args)
        {
            ApiHelper.Init();

            var token = await GetToken();

            var categories = await GetCategories(token);

            var getMagazinesTask = new List<Task>();

            foreach (string category in categories)
            {
                getMagazinesTask.Add(Task.Run(async () =>
                {
                    var magazines = await GetMagazines(token, category);
                    categoryMagazines.Add(magazines);
                }));
            }

            await Task.WhenAll(getMagazinesTask);

            List<string> subscribedToAllCategories = new List<string>();

            var subscribers = await GetSubscribers(token);

            foreach (Subscriber subscriber in subscribers)
            {
                bool isSubscribedToAllCategories = true;

                foreach (var magazines in categoryMagazines)
                {
                    if (!magazines.Any(x => subscriber.MagazineIds.Contains(x)))
                    {
                        isSubscribedToAllCategories = false;
                        break;
                    }
                }

                if (isSubscribedToAllCategories)
                {
                    subscribedToAllCategories.Add(subscriber.Id);
                }
            }

            var answerResponse = await PostAnswer(token, subscribedToAllCategories);

            Console.WriteLine(answerResponse.TotalTime);
            Console.WriteLine(answerResponse.AnswerCorrect);
            Console.WriteLine(answerResponse.ShouldBe);
        }

        #region Request methods

        private static async Task<string> GetToken()
        {
            using (HttpResponseMessage response = await ApiHelper.httpClient.GetAsync("api/token"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ApiResponse>();

                    if (apiResponse.Success)
                    {
                        return apiResponse.Token;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private static async Task<List<string>> GetCategories(string token)
        {
            using (HttpResponseMessage response = await ApiHelper.httpClient.GetAsync($"api/categories/{token}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ApiResponseString>();

                    if (apiResponse.Success)
                    {
                        return apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private static async Task<List<int>> GetMagazines(string token, string category)
        {
            using (HttpResponseMessage response = await ApiHelper.httpClient.GetAsync($"api/magazines/{token}/{category}/"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ApiResponseMagazine>();

                    if (apiResponse.Success)
                    {
                        List<int> magazines = new List<int>();

                        foreach (Magazine magazine in apiResponse.Data)
                        {
                            magazines.Add(magazine.Id);
                        }

                        return magazines;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private static async Task<List<Subscriber>> GetSubscribers(string token)
        {
            using (HttpResponseMessage response = await ApiHelper.httpClient.GetAsync($"api/subscribers/{token}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ApiResponseSubscriber>();

                    if (apiResponse.Success)
                    {
                        return apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private static async Task<AnswerResponse> PostAnswer(string token, List<string> content)
        {
            var contentObject = new Dictionary<string, List<string>>
            {
                {
                    "subscribers", content
                },
            };

            string contentObjectSerialized = JsonConvert.SerializeObject(contentObject);
            using (HttpResponseMessage response = await ApiHelper.httpClient.PostAsync($"api/answer/{token}", new StringContent(contentObjectSerialized, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsAsync<ApiResponseAnswerResponse>();

                    if (apiResponse.Success)
                    {
                        return apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        #endregion
    }
}