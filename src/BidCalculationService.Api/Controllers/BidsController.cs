using Asp.Versioning;
using BidCalculationService.Application.DTOs;
using BidCalculationService.Application.Handlers.Commands;
using BidCalculationService.Application.Handlers.Queries;
using BidCalculationService.Domain.Helpers;
using BidCalculationService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculationService.Api.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BidsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<CreateAccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [HttpPost]
        public async Task<IResult> CreateBidAsync([FromBody] CreateBidRequest model)
        {
            var result = await _mediator.Send(model);
            if (!result.IsSuccess)
                return Results.BadRequest(result);

            return Results.Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<CreateAccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [HttpGet("{Id}")]
        public async Task<IResult> GetBidAsync([FromRoute] long Id)
        {
            var result = await _mediator.Send(new GetBidRequest
            {
                Id = Id
            });

            if (!result.IsSuccess)
                return Results.BadRequest(result);

            return Results.Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Pageable<CreateAccountResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [HttpGet]
        public async Task<IResult> GetBidsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _mediator.Send(new GetBidsRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return Results.Ok(result);
        }
    }
}
