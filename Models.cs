using System.Text.Json.Serialization;

namespace MagazineStore
{
    public class ApiResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class ApiResponseString : ApiResponse
    {
        [JsonPropertyName("data")]
        public List<string> Data { get; set; }
    }

    public class ApiResponseMagazine : ApiResponse
    {
        [JsonPropertyName("data")]
        public List<Magazine> Data { get; set; }
    }

    public class ApiResponseSubscriber : ApiResponse
    {
        [JsonPropertyName("data")]
        public List<Subscriber> Data { get; set; }
    }

    public class ApiResponseAnswerResponse : ApiResponse
    {
        [JsonPropertyName("data")]
        public AnswerResponse Data { get; set; }
    }

    public class Magazine
    {
        [JsonPropertyName("id")]
        public Int32 Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; }
    }

    public class Subscriber
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("magazineIds")]
        public List<int> MagazineIds { get; set; }
    }

    public class Answer
    {
        [JsonPropertyName("subscribers")]
        public List<string> Subscribers { get; set; }
    }

    public class AnswerResponse
    {
        [JsonPropertyName("totalTime")]
        public string TotalTime { get; set; }

        [JsonPropertyName("answerCorrect")]
        public bool AnswerCorrect { get; set; }

        [JsonPropertyName("shouldBe")]
        public List<string> ShouldBe { get; set; }
    }

}

