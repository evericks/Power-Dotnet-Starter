using Domain.Models.Creates;
using FluentValidation;

namespace Domain.Validators;

public class StudentCreateValidator: AbstractValidator<StudentCreateModel>
{
    public StudentCreateValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is invalid.");
    }
}