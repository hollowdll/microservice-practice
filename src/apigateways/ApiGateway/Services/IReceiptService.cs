using ApiGateway.Models;

namespace ApiGateway.Services;

public interface IReceiptService
{
    Task<IList<ReceiptData>> GetAll();
    Task<ReceiptData?> GetById(int id);
    Task<ReceiptData> Create(CreateReceiptRequest createReceiptRequest);
}