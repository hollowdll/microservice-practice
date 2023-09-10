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
        var receipts = await _receiptContext.Receipts.ToListAsync();
        
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

    
}