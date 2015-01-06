namespace PollApi
{
    public class PollInput
    {
        public string Question { get; set; }

        public string[] Options { get; set; }

        public bool MultiChoice { get; set; }
    }
}