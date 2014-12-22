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
    }
}