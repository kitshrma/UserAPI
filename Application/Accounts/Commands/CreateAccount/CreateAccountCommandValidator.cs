using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateAccountCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(a => a.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(1).WithMessage("User Id should be greater than 1")
                .MustAsync(NoExistingAccount).WithMessage("User should not already have an account")
                .MustAsync(CreditCheck).WithMessage("monthly salary - monthly expenses is less than $1000");
        }

        private async Task<bool> NoExistingAccount(int UserId, CancellationToken cancellationToken)
        {
            return  await _context.Accounts
                .AnyAsync(x => x.UserID != UserId);

            
        }

        private async Task<bool> CreditCheck(int UserId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
                throw new NotFoundException($"User with Id {UserId} Not Found.");
            var credit = user.MonthlySalary - user.MonthlyExpenses;
            return credit > 1000;
        }
    }
}
