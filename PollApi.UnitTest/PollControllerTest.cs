using System.Web.Http.Results;
using Moq;
using Raven.Client;
using Xunit;

namespace PollApi.UnitTest
{
    public class PollControllerTest
    {
        [Fact]
        public void Get_NonExistentPoll_ReturnsNotFound()
        {
            var session = new Mock<IDocumentSession>();
            var sut = new PollController(session.Object);
            var actual = sut.Get(1);
            Assert.IsAssignableFrom<NotFoundResult>(actual);
        }
    }
}