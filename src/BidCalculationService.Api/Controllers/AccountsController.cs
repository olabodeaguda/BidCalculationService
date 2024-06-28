using Asp.Versioning;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculationService.Api.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<LoginResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [HttpPost("login")]
        public async Task<IResult> LoginAsync([FromBody] LoginRequest model)
        {
            var result = await _mediator.Send(model);
            if (!result.IsSuccess)
                return Results.BadRequest(result);

            return Results.Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<CreateAccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [HttpPost]
        public async Task<IResult> CreateAccountAsync([FromBody] CreateAccountRequest model)
        {
            var u = User;
            var result = await _mediator.Send(model);
            if (!result.IsSuccess)
                return Results.BadRequest(result);

            return Results.Created($"api/Accounts", result);
        }
    }
}
