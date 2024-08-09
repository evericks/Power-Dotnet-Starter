using Domain.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class FluentValidationConfiguration
{
    public static void AddValidators(this IMvcBuilder builder)
    {
        builder.AddFluentValidation(fv => 
        {
            fv.RegisterValidatorsFromAssemblyContaining<StudentCreateValidator>();
        });
    }
}