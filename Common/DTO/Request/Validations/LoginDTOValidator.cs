using Common.Request;
using FluentValidation;

namespace Common.DTO.Request.Validations
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(login => login.Email).EmailAddress();
            RuleFor(login => login.Password).NotEmpty();
        }
    }
}
