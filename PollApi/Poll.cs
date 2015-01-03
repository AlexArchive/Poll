namespace PollApi
{
    public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public PollOption[] Options { get; set; }
        public bool MultiChoice { get; set; }
    }
}