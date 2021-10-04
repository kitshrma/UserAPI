using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;

namespace Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<Response<int>>
    {
        public int UserId;
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateAccountCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            
            var account = new Account {
                UserID = request.UserId,
                Balance = 1000
            };
            
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<int>(account.Id, "Account Created successfully");
        }
    }
}
