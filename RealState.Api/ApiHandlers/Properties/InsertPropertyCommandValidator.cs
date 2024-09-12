using FluentValidation;
using RealState.Application.Properties.Command;

namespace RealState.Api.ApiHandlers.Properties
{
    public class InsertPropertyCommandValidator : AbstractValidator<InsertPropertyCommand>
    {
        public InsertPropertyCommandValidator()
        {
            RuleFor(c => c.IdOwner).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Address).NotNull();
            RuleFor(c => c.Price).GreaterThan(0);
            RuleFor(c => c.Year)
                .GreaterThanOrEqualTo(1900)
                .LessThanOrEqualTo(DateTime.Now.Year);
        }
    }
}
