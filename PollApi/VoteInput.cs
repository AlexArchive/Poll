using System.ComponentModel.DataAnnotations;

namespace PollApi
{
    public class VoteInput
    {
        [MinLength(1)]
        public int[] Options { get; set; } 
    }
}