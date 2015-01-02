using System.Linq;
using System.Web.Http;
using Raven.Client;

namespace PollApi
{
    public class VoteController : ApiController
    {
        private readonly IDocumentSession _session;

        public VoteController(IDocumentSession session)
        {
            _session = session;
        }

        public IHttpActionResult Post(VoteInput voteInput)
        {
            var poll = _session.Load<Poll>(voteInput.PollId);

            foreach (var option in poll.Options.Where(option => voteInput.OptionIds.Contains(option.Id)))
            {
                option.Votes += 1;
            }

            return Ok();
        }
    }
}