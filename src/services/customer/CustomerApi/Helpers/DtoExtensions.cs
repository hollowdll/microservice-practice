using CustomerApi.Models;
using CustomerApi.Dtos;

namespace CustomerApi.Helpers;

public static class DtoExtensions
{
    /// <summary>
    /// Creates a new <see cref="CustomerDto" /> from <see cref="Customer" />.
    /// </summary>
    public static CustomerDto ToDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            CreatedAt = customer.CreatedAt
        };
    }
}
