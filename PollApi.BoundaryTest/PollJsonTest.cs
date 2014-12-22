using System.Net.Http;
using Newtonsoft.Json;
using Xunit;

namespace PollApi.BoundaryTest
{
    public class PollJsonTest
    {
        [Fact]
        public void GetReturnsJson()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("/1").Result;
                Assert.Equal(
                    "application/json",
                    response.Content.Headers.ContentType.MediaType);
                var json = response.Content
                    .ReadAsStringAsync()
                    .ContinueWith(t => JsonConvert.DeserializeObject(t.Result)).Result;
                Assert.NotNull(json);
            }
        }

        [Fact]
        public void PostPollSucceeds()
        {
            using (var client = HttpClientFactory.Create())
            {
                var data = new
                {
                    Question = "Who is the best coder?",
                    Options = new[] { "Jon Skeet", "Mark Seemann", "Ayende" }
                };
                var response = client.PostAsJsonAsync("", data).Result;
                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }

        [Fact]
        public void GetAfterPostingPollReturnsPoll()
        {
            using (var client = HttpClientFactory.Create())
            {
                var data = new
                {
                    Question = "Who is the best coder?",
                    Options = new[] { "Jon Skeet", "Mark Seemann", "Ayende" }
                };
                var expected = JsonConvert.DeserializeObject(
                    JsonConvert.SerializeObject(data));
                var postResponse = client.PostAsJsonAsync("", data).Result;
                var pollLocation = postResponse.Headers.Location;
                var getResponse = client.GetAsync(pollLocation).Result;
                dynamic actual = getResponse.Content
                    .ReadAsStringAsync()
                    .ContinueWith(t => JsonConvert.DeserializeObject(t.Result)).Result;
                Assert.Equal(expected, actual);
            }
        }
    }
}