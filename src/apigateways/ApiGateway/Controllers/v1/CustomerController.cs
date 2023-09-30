using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Services;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<CustomerData>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAll();

        return Ok(customers);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<CustomerData>>> GetCustomerById(int id)
    {
        var customer = await _customerService.GetById(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateCustomer(CreateCustomerRequest createCustomerRequest)
    {
        var customer = await _customerService.Create(createCustomerRequest);

        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, null);
    }
}