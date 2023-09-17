using ApiGateway.Models;

namespace ApiGateway.Services;

public interface ICustomerService
{
    Task<CustomerData> GetById(int id);
    Task<IList<CustomerData>> GetAll();
    Task Create(CreateCustomerRequest createCustomerRequest);
}