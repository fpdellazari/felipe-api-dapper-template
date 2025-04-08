using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FelipeApiDapperTemplate.Domain.Models.Entities;

namespace FelipeApiDapperTemplate.Application.Mappers;

public static class CustomerMapper
{
    public static CustomerDTO MapToDTO(this Customer customer)
    {
        return new CustomerDTO(
            Id: customer.Id,
            Name: customer.Name,
            Age: customer.Age,
            Phone: customer.Phone,
            Email: customer.Email,
            CreatedAt: customer.CreatedAt,
            UpdatedAt: customer.UpdatedAt
        );
    }

    public static IEnumerable<CustomerDTO> MapToDTOList(this IEnumerable<Customer> customers)
    {
        return customers.Select(customer => customer.MapToDTO());
    }
}

