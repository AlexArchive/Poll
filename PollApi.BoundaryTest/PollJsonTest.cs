using Newtonsoft.Json;
using PollApi.BoundaryTest.Support;
using System;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace PollApi.BoundaryTest
{
    public class PollJsonTest : IDisposable
    {
        #region Setup & Teardown
        private readonly HttpClient _client;

        public PollJsonTest()
        {
            _client = Support.HttpClientFactory.Create();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
        #endregion

        [Fact]
        public void PostPollSucceeds()
        {
            var input = new
            {
                Question = "question",
                Options = new[] { "option 1", "option 2" }
            };

            var response = _client.PostAsJsonAsync("api/poll", input).Result;
            
            Assert.True(
                response.IsSuccessStatusCode,
                "Actual status code: " + response.StatusCode);
        }

        [Fact]
        public void GetAfterPostingPollReturnsPoll()
        {
            var input = new
            {
                Question = "question",
                Options = new[] { "option 1", "option 2" }
            };

            var postResponse = _client.PostAsJsonAsync("api/poll", input).Result;
            dynamic postResponseContent = JsonConvert.DeserializeObject(
                postResponse.Content
                    .ReadAsStringAsync()
                    .Result);
            var pollLocation = (string)postResponseContent.pollLocation;
            var getResponse = _client.GetAsync(pollLocation).Result;
            var output = JsonConvert.DeserializeObject<Poll>(
                getResponse.Content
                    .ReadAsStringAsync()
                    .Result);

            Assert.True(output.Id > 0);
            Assert.Equal(false, output.MultiChoice);
            Assert.Equal(input.Question, output.Question);
            Assert.Equal(input.Options, output.Options.Select(option => option.Text));
        }


        [Fact]
        public void GetReturnsCorrectPoll()
        {
            var poll = new Poll
            {
                Question = "question",
                Options = new[]
                {
                    new PollOption { Id = 0, Text = "option one"}, 
                    new PollOption{ Id = 1, Text = "option two" }
                }
            };
            using (var session = EmbeddableSessionFactory.Create())
            {
                session.Store(poll);
                session.SaveChanges();
            }

            var response = _client.GetAsync("api/Poll/" + poll.Id).Result;
            var actual = JsonConvert.DeserializeObject<Poll>(
                response.Content
                    .ReadAsStringAsync()
                    .Result);

            Assert.Equal(poll.Id, actual.Id);
            Assert.Equal(poll.Question, actual.Question);
            Assert.Equal(
                poll.Options.Select(option => option.Text), 
                actual.Options.Select(option => option.Text));
        }
    }
}