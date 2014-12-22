using System.Web.Http;
using Raven.Client;

namespace Poll
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
            var question = _session.Load<Poll>(pollId);

            return Ok(question);
        }

        public IHttpActionResult Post(Poll question)
        {
            _session.Store(question);

            return Created(question.Id.ToString(), "");
        }
    }
}