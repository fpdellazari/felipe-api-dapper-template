using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FelipeApiDapperTemplate.Domain.Models.Entities;

namespace FelipeApiDapperTemplate.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(CreateUpdateCustomerDTO createUpdateCustomer);
    Task<IEnumerable<Customer>> GetAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> UpdateAsync(int id, CreateUpdateCustomerDTO createUpdateCustomer);
}
