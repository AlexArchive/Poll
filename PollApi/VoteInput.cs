namespace PollApi
{
    public class VoteInput
    {
        public int PollId { get; set; }
        public int[] OptionIds { get; set; }
    }
}