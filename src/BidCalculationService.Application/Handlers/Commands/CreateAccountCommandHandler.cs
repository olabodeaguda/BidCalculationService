using BidCalculationService.Application.DTOs;
using MediatR;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Entities;
using BidCalculationService.Domain.Exceptions;
using AutoMapper;

namespace BidCalculationService.Application.Handlers.Commands
{
    public class CreateAccountRequest : IRequest<Result<CreateAccountResponseDto>>
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountRequest, Result<CreateAccountResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;
        public CreateAccountCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<CreateAccountResponseDto>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userRepository.GetByEmailAsync(request.Email);
            if (user != null)
                return Result<CreateAccountResponseDto>.Failure(CreateAccountError.AccountAlreadyExist());

            user = await _userRepository.CreateAsync(_mapper.Map<ApplicationUser>(request));

            if (user == null)
                return Result<CreateAccountResponseDto>.Failure(CreateAccountError.AccountCreationFailed());

            return Result<CreateAccountResponseDto>.Success(_mapper.Map<CreateAccountResponseDto>(user));
        }
    }
}
