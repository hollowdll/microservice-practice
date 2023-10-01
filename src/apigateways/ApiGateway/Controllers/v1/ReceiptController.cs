using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Services;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceiptController : ControllerBase
{
    private readonly IReceiptService _receiptService;
    
    public ReceiptController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    // Gets all receipts if no query parameters.
    // If customer id is given in query param, returns receipts for that customer.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ReceiptData>>> GetAllReceipts([FromQuery(Name = "customer")] int? customerId)
    {
        if (customerId.HasValue)
        {
            var customerReceipts = await _receiptService.GetAllByCustomerId((int) customerId);

            return Ok(customerReceipts);
        }

        var receipts = await _receiptService.GetAll();

        return Ok(receipts);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ReceiptData>>> GetReceiptById(int id)
    {
        var receipt = await _receiptService.GetById(id);
        if (receipt == null)
        {
            return NotFound();
        }

        return Ok(receipt);
    }
}