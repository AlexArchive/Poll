using System.Linq;
using System.Web.Http;
using Raven.Client;

namespace PollApi
{
    public class PollController : ApiController
    {
        private readonly IDocumentSession _session;

        public PollController(IDocumentSession session)
        {
            _session = session;
        }

        [Route("api/poll/{pollId}")]
        public IHttpActionResult Get(int pollId)
        {
            var question = _session.Load<Poll>(pollId);

            return Ok(question);
        }

        [Route("api/poll")]
        public IHttpActionResult Post(PollInput pollInput)
        {
            var poll = new Poll
            {
                Question = pollInput.Question,
                MultiChoice = pollInput.MultiChoice,
                Options = pollInput.Options.Select((option, index) => new PollOption { Id = index, Text = option }).ToArray()
            };

            _session.Store(poll);

            return Ok(new { pollId = poll.Id, pollLocation = "http://localhost:63382/api/Poll/" + poll.Id });
        }
    }
}