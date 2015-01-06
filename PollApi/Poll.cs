using System.Collections.Generic;

namespace PollApi
{
    public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool MultiChoice { get; set; }
        public PollOption[] Options { get; set; }
        public List<string> VoterIps { get; set; }

        public Poll()
        {
            VoterIps = new List<string>();
        }
    }
}