using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(u => u.EmailAddress)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("A valid email address is required.")
                .MustAsync(BeUniqueEmailAddress).WithMessage("User with provided {PropertyName} already exists.");

            RuleFor(u => u.MonthlySalary)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(1).WithMessage("Monthly Salary should be at least A$ 1");

            RuleFor(u => u.MonthlyExpenses)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(1).WithMessage("Monthly Salary should be at least A$ 1");
        }

        private async Task<bool> BeUniqueEmailAddress(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AllAsync(x => x.EmailAddress != email);
        }
    }
}
