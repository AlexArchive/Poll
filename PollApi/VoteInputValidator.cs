using FluentValidation;

namespace PollApi
{
    public class VoteInputValidator : AbstractValidator<VoteInput>
    {
        public VoteInputValidator()
        {
            RuleFor(vote => vote.Options).NotEmpty();
        }
    }
}