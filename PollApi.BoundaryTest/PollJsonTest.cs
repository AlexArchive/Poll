using System.Net.Http;
using Newtonsoft.Json;
using Raven.Client.Document;
using Xunit;

namespace Poll.BoundaryTest
{
    public class PollJsonTest
    {
        [Fact]
        public void PostPollSucceeds()
        {
            using (var client = HttpClientFactory.Create())
            {
                var poll = new
                {
                    Question = "Who is the best coder?",
                    Options = new[] { "Jon Skeet", "Mark Seemann", "Ayende" }
                };
                var response = client.PostAsJsonAsync("", poll).Result;
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
                var poll = new
                {
                    Question = "Who is the best coder?",
                    Options = new[] { "Jon Skeet", "Mark Seemann", "Ayende" }
                };
                var postResponse = client.PostAsJsonAsync("", poll).Result;
                var pollLocation = postResponse.Headers.Location;
                var getResponse = client.GetAsync(pollLocation).Result;
                var actual = JsonConvert.DeserializeObject<Poll>(
                    getResponse.Content
                        .ReadAsStringAsync()
                        .Result);
                Assert.Equal(poll.Question, actual.Question);
                Assert.Equal(poll.Options, actual.Options);
            }
        }

        [Fact]
        public void GetReturnsCorrectPoll()
        {
            int pollId;
            var poll = new Poll
            {
                Question = "Who is the best coder?",
                Options = new[] { "Jon Skeet", "Mark Seemann", "Ayende" }
            };
            using (var documentStore = new DocumentStore { ConnectionStringName = "Poll" }.Initialize())
            using (var session = documentStore.OpenSession())
            {
                session.Store(poll);
                pollId = poll.Id;
                session.SaveChanges();
            }
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync(pollId.ToString()).Result;
                var actual = JsonConvert.DeserializeObject<Poll>(
                    response.Content
                        .ReadAsStringAsync()
                        .Result);
                Assert.Equal(poll.Id, actual.Id);
                Assert.Equal(poll.Question, actual.Question);
                Assert.Equal(poll.Options, actual.Options);
            }
        }
    }
}