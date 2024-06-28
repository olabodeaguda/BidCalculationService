using AutoMapper;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Interfaces.Repositories;
using BidCalculationService.Domain.Interfaces.Services;
using MediatR;

namespace BidCalculationService.Application.Handlers.Commands
{
    public class LoginRequest : IRequest<Result<LoginResponseDto>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class LoginCommandHandler : IRequestHandler<LoginRequest, Result<LoginResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponseDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Result<LoginResponseDto>.Failure(LoginError.InvalidCredentials());
            if (!user.Password.Equals(request.Password.ToSha256()))
                return Result<LoginResponseDto>.Failure(LoginError.InvalidCredentials());

            var token = _tokenService.GenerateToken(user);
            if (!token.IsSuccess)
                return Result<LoginResponseDto>.Failure(LoginError.InvalidCredentials());

            LoginResponseDto result = new LoginResponseDto
            {
                UserProfile = _mapper.Map<UserProfile>(user),
                Expires = token.Data.expires,
                Token = token.Data.token
            };

            return Result<LoginResponseDto>.Success(result);
        }
    }
}
