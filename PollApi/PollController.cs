using Raven.Client;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace PollApi
{
    public class PollController : ApiController
    {
        private readonly IDocumentSession _session;

        public PollController(IDocumentSession session)
        {
            _session = session;
        }

        public IHttpActionResult Get(int pollId)
        {
            var poll = _session.Load<Poll>(pollId);

            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        public IHttpActionResult Put(int pollId, VoteInput voteInput)
        {
            var poll = _session.Load<Poll>(pollId);
            var voterIp = Request.GetOwinContext().Request.RemoteIpAddress;

            if (poll == null)
            {
                return BadRequest("you cannot vote on a poll that does not exist.");
            }

            if (poll.VoterIps.Contains(voterIp))
            {
                return BadRequest("you cannot vote on this poll because you have already voted.");
            }

            if (voteInput.Options.Length > 1 && !poll.MultiChoice)
            {
                return BadRequest("you cannot vote for more than one option. noob.");
            }

            foreach (var option in poll.Options.Where(option => voteInput.Options.Contains(option.Id)))
            {
                option.Votes += 1;
            }

            poll.VoterIps.Add(voterIp);
            _session.SaveChanges();

            return Ok();
        }

        public IHttpActionResult Post(PollInput pollInput)
        {
            var poll = new Poll
            {
                Question = pollInput.Question,
                MultiChoice = pollInput.MultiChoice
            };

            poll.Options = pollInput.Options
                .Select((option, index) => new PollOption { Id = index, Text = option })
                .ToArray();

            _session.Store(poll);
            _session.SaveChanges();

            return Created("", new { pollId = poll.Id });
        }
    }
}