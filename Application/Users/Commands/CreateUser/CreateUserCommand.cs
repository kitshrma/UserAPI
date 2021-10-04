using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public double MonthlySalary { get; set; }
        public double MonthlyExpenses { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            
            var user = _mapper.Map<User>(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new Response<int>(user.Id,"User Created successfully");
        }
    }
}
