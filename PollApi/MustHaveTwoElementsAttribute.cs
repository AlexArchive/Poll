using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PollApi
{
    public class MustHaveTwoElementsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var sequence = value as IList<string>;

            if (sequence == null)
            {
                return false;
            }

            if (sequence.Count < 2)
            {
                return false;
            }

            if (sequence.Any(string.IsNullOrEmpty))
            {
                return false;
            }

            return true;
        }
    }
}