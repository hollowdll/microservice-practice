using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ReceiptApi.Data;
using ReceiptApi.Models;
using ReceiptApi.Dtos;
using ReceiptApi.Helpers;

namespace ReceiptApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReceiptController : ControllerBase
{
    private readonly ILogger<ReceiptController> _logger;
    private readonly ReceiptDbContext _receiptContext;

    public ReceiptController(
        ILogger<ReceiptController> logger,
        ReceiptDbContext receiptContext)
    {
        _logger = logger;
        _receiptContext = receiptContext;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IList<ReceiptDto>>> GetAllReceipts()
    {
        var receipts = await _receiptContext.Receipts
            .Select(i => i.ToDto())
            .ToListAsync();
        
        return Ok(receipts);
    }

    [HttpGet()]
    [Route("id/{id}")]
    public async Task<ActionResult<ReceiptDto>> GetReceiptById(int id)
    {
        var receipt = await _receiptContext.Receipts.FindAsync(id);
        if (receipt == null)
        {
            return NotFound();
        }

        return Ok(receipt.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<ReceiptDto>> CreateReceipt(ReceiptCreateDto receiptCreateDto)
    {
        var receipt = new Receipt(
            receiptCreateDto.CustomerId,
            receiptCreateDto.TicketId,
            receiptCreateDto.CustomerTicketCount);

        _receiptContext.Receipts.Add(receipt);
        await _receiptContext.SaveChangesAsync();

        _logger.LogInformation("Created a new receipt with ID '{ReceiptId}' via HTTP API", receipt.Id);

        return CreatedAtAction(nameof(GetReceiptById), new { id = receipt.Id }, receipt.ToDto());
    }

    // Gets all customer's receipts.
    [HttpGet]
    [Route("all/customer/{customerId}")]
    public async Task<ActionResult<IList<ReceiptDto>>> GetCustomerReceipts(int customerId)
    {
        var receipts = await _receiptContext.Receipts
            .Where(i => i.CustomerId == customerId)
            .Select(i => i.ToDto())
            .ToListAsync();

        return Ok(receipts);
    }
}