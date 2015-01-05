using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using Raven.Client;
using System.Linq;
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
                return NotFound();

            return Ok(poll);
        }

        public IHttpActionResult Post(PollInput pollInput)
        {
            if (pollInput == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var poll = new Poll
            {
                Question = pollInput.Question,
                MultiChoice = pollInput.MultiChoice,
                Options = pollInput.Options
                    .Select((option, index) => new PollOption { Id = index, Text = option })
                    .ToArray()
            };

            _session.Store(poll);

            return Created(poll.Id.ToString(), new { pollId = poll.Id });
        }

        public IHttpActionResult Put(int pollId, VoteInput voteInput)
        {
            if (voteInput == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var poll = _session.Load<Poll>(pollId);

            if (poll == null)
            {
                return NotFound();
            }

            if (!poll.MultiChoice && voteInput.Options.Length > 1)
            {
                return BadRequest();
            }

            string clientIp = Request.GetOwinContext().Request.RemoteIpAddress;

            if (poll.VoterIps.Contains(clientIp))
            {
                return BadRequest("you already voted.");
            }

            foreach (var option in poll.Options.Where(option => voteInput.Options.Contains(option.Id)))
            {
                option.Votes += 1;
            }

            poll.VoterIps.Add(clientIp);


            poll.VoterIps.Add("127.0.0.1");

            return Ok();
        }
    }
}