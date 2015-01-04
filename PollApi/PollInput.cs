using System.ComponentModel.DataAnnotations;

namespace PollApi
{
    public class PollInput
    {
        [Required]
        public string Question { get; set; }

        [MustHaveTwoElements]
        public string[] Options { get; set; }

        public bool MultiChoice { get; set; }
    }
}