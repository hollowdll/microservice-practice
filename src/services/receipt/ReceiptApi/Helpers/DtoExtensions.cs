using ReceiptApi.Models;
using ReceiptApi.Dtos;

namespace ReceiptApi.Helpers;

public static class DtoExtensions
{
    /// <summary>
    /// Creates a new <see cref="ReceiptDto" /> from <see cref="Receipt" />.
    /// </summary>
    public static ReceiptDto ToDto(this Receipt receipt)
    {
        return new ReceiptDto
        {
            Id = receipt.Id,
            Message = receipt.Message,
            CustomerId = receipt.CustomerId,
            TicketId = receipt.TicketId,
            CreatedAt = receipt.CreatedAt,
            CustomerTicketCount = receipt.CustomerTicketCount,
        };
    }
}