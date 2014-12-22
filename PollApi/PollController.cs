using System.Web.Http;

namespace PollApi
{
    public class PollController : ApiController
    {
        private static QuestionModel question;

        public IHttpActionResult Get(int pollId)
        {
            return Ok(question);
        }

        public IHttpActionResult Post(QuestionModel question)
        {
            PollController.question = question;
            return Created("1", "");
        }
    }
}