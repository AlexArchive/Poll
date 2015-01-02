﻿using System.Linq;
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

        public IHttpActionResult Get(int pollId)
        {
            var question = _session.Load<Poll>(pollId);

            return Ok(question);
        }

        public IHttpActionResult Post(PollInput pollInput)
        {
            var poll = new Poll
            {
                Question = pollInput.Question,
                Options = pollInput.Options.Select((option, index) => new PollOption { Id = index, Text = option }).ToArray()
            };

            _session.Store(poll);

            return Created(poll.Id.ToString(), "");
        }
    }
}