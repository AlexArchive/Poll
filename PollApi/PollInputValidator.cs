using System.Linq;
using FluentValidation;

namespace PollApi
{
    public class PollInputValidator : AbstractValidator<PollInput>
    {
        public PollInputValidator()
        {
            RuleFor(poll => poll.Question).NotEmpty();
            RuleFor(poll => poll.Options).Must(HaveTwoOptions);
        }

        private static bool HaveTwoOptions(string[] options)
        {
            return 
                options != null && 
                options.Any(option => !string.IsNullOrEmpty(option));
        }
    }
}