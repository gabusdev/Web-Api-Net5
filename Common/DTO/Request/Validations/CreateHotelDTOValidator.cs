using Common.Response;
using FluentValidation;

namespace Common.DTO.Request.Validations
{
    public class CreateHotelDTOValidator : AbstractValidator<CreateHotelDTO>
    {
        public CreateHotelDTOValidator()
        {
            RuleFor(ch => ch.CountryId).NotEmpty();
            RuleFor(ch => ch.Rating).InclusiveBetween(0, 5);
            RuleFor(ch => ch.Name).NotEmpty();
            RuleFor(ch => ch.Address).NotEmpty();
        }
    }
}
