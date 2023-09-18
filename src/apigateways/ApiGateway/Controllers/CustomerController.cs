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

    
}