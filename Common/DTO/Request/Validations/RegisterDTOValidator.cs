using Common.Request;
using FluentValidation;

namespace Common.DTO.Request.Validations
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(r => r.LastName).NotEmpty().MaximumLength(20);
            RuleFor(r => r.Username).NotEmpty().MaximumLength(20);
            RuleFor(r => r.Email).EmailAddress().MaximumLength(20);
            RuleFor(r => r.PhoneNumber).NotEmpty().Length(8, 15);
            RuleFor(r => r.Password).Length(6, 30);
            RuleFor(r => r.ConfirmationPassword).Equal(r => r.Password).WithMessage("Password and Confirmation Password do not match.");
        }
    }
}
