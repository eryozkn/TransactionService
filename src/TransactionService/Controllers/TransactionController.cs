using Microsoft.AspNetCore.Mvc;

using Monad;

using TransactionService.Domain;
using TransactionService.Facade.Interfaces;
using TransactionService.Models.Request;
using TransactionService.Models.Response;

namespace TransactionService.Controllers
{
    [Route("api/v1/transaction")]
    [ApiController]
    public class TransactionController(ITransactionFacade facade) : ControllerBase
    {
        private readonly ITransactionFacade _facade = facade;

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> CreateTransaction([FromBody] TransactionRequest request)
        {
            var transactionModel = new Transaction()
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Currency = request.Currency,
                TransactionType = request.TransactionType,
            };

            var transactionResult = await _facade.CreateTransaction(transactionModel);

            if (transactionResult.IsRight()) // Error thrown to handle
            {
                var error = transactionResult.Right();

                if (error.Code == ErrorCodes.InternalServerError)
                {
                    return StatusCode(500, new ErrorResponse()
                    {
                        Code = error.Code,
                        Message = error.Message
                    });
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
            else // tx created successfully
            {
                var transaction = transactionResult.Left();

                return Ok(new TransactionResponse()
                {
                    UserId = transaction.UserId,
                    TransactionReference = transaction.Reference,
                    Amount = transaction.Amount,
                    Currency = transaction.Currency,
                    CreatedAt = transaction.CreatedAt,
                });
            }
        }
    }
}
