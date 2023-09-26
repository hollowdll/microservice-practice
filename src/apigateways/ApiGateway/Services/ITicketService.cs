using ApiGateway.Models;

namespace ApiGateway.Services;

public interface ITicketService
{
    Task<IList<TicketData>> GetAll();
    Task<TicketData?> GetById(int id);
    Task<TicketData> Create(CreateTicketRequest createTicketRequest);
    Task<IList<TicketData>> GetAllByCustomerId(int customerId);
}