using Microsoft.AspNetCore.Mvc;

using Monad;

using TransactionService.Domain;
using TransactionService.Facade.Interfaces;
using TransactionService.Models.Response;

namespace TransactionService.Controllers
{
    [Route("api/v1/balance")]
    [ApiController]
    public class UserBalanceController(IUserBalanceFacade facade) : ControllerBase
    {
        private readonly IUserBalanceFacade _facade = facade;

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBalanceResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> GetUserBalance([FromRoute] long userId)
        {
            var userBalanceResult = await _facade.GetUserBalance(userId);
            return HandleResponse(userBalanceResult);
        }
        private ActionResult HandleResponse(Either<UserBalance, Error> response)
        {
            if (response.IsRight()) // error thrown
            {
                var error = response.Right();

                if (error.Code == ErrorCodes.InternalServerError)
                {
                    return StatusCode(500, MapErrorResponse(error));
                }
                else
                {
                    return BadRequest(
                    new ErrorResponse()
                    {
                        Code = error.Code,
                        Message = error.Message
                    });
                }
            }

            else
            {
                var userBalance = response.Left();
                return Ok(MapUserBalanceResponse(userBalance));
            }
        }

        private static UserBalanceResponse MapUserBalanceResponse(UserBalance userBalance)
        {
            return new UserBalanceResponse()
            {
                UserId = userBalance.UserId,
                Balance = userBalance.Balance,
                Currency = userBalance.Currency
            };
        }
        private static ErrorResponse MapErrorResponse(Error error)
        {
            return new ErrorResponse()
            {
                Code = error.Code,
                Message = error.Message
            };
        }
    }
}
