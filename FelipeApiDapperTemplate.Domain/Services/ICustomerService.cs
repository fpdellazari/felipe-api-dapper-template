using FelipeApiDapperTemplate.Domain.Models.DTOs;

namespace FelipeApiDapperTemplate.Domain.Services;

public interface ICustomerService
{
    Task<CustomerDTO> CreateAsync(CreateCustomerDTO createCustomer);
    Task<IEnumerable<CustomerDTO>> GetAsync();
    Task<CustomerDTO> GetByIdAsync(int id);
    Task<CustomerDTO> UpdateAsync(int id, UpdateCustomerDTO updateCustomer);
}

