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

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IList<CustomerDto>>> GetAllCustomers()
    {
        var customers = await _customerContext.Customers
            .Select(i => i.ToDto())
            .ToListAsync();
        
        return Ok(customers);
    }

    [HttpGet()]
    [Route("id/{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
    {
        var customer = await _customerContext.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
    {
        var customer = new Customer(
            customerCreateDto.FirstName,
            customerCreateDto.LastName,
            customerCreateDto.Email);

        _customerContext.Customers.Add(customer);
        await _customerContext.SaveChangesAsync();

        _logger.LogInformation("Created a new customer with ID '{CustomerId}' via HTTP API", customer.Id);

        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer.ToDto());
    }
}
