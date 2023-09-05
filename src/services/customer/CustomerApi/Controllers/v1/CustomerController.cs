using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CustomerApi.Data;
using CustomerApi.Models;
using CustomerApi.Dtos;
using CustomerApi.Helpers;

namespace CustomerApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly CustomerDbContext _customerContext;

    public CustomerController(
        ILogger<CustomerController> logger,
        CustomerDbContext customerDbContext)
    {
        _logger = logger;
        _customerContext = customerDbContext;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IList<CustomerDto>>> GetAllCustomersAsync()
    {
        return Ok(await _customerContext.Customers.ToListAsync());
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerContext.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer.ToDto());
    }
}
