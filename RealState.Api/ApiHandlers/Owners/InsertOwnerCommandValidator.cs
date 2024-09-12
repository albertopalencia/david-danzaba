using FluentValidation;
using RealState.Application.Owners.Command;

namespace RealState.Api.ApiHandlers.Holidays
{
    public class InsertOwnerCommandValidator: AbstractValidator<InsertOwnerCommand>
    {
        public InsertOwnerCommandValidator()
        {
            RuleFor(o => o.Birthday)
                .NotEmpty()
                .Must(BeAValidDate);

                RuleFor(o => o.Name) 
                .NotEmpty();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
         
    }
}
