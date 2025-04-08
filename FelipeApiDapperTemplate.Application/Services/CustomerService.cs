using FelipeApiDapperTemplate.Application.Mappers;
using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FelipeApiDapperTemplate.Domain.Repositories;
using FelipeApiDapperTemplate.Domain.Services;

namespace FelipeApiDapperTemplate.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDTO> CreateAsync(CreateUpdateCustomerDTO createUpdateCustomer)
    {
        var customer = await _customerRepository.CreateAsync(createUpdateCustomer);

        return customer.MapToDTO();
    }

    public async Task<IEnumerable<CustomerDTO>> GetAsync()
    {
        var customers = await _customerRepository.GetAsync();

        return customers.MapToDTOList();
    }

    public async Task<CustomerDTO> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer is null) throw new KeyNotFoundException("Cliente não encontrado.");

        return customer.MapToDTO();
    }

    public async Task<CustomerDTO> UpdateAsync(int id, CreateUpdateCustomerDTO createUpdateCustomer)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer is null) throw new KeyNotFoundException("Cliente não encontrado.");

        var updatedCustomer = await _customerRepository.UpdateAsync(id, createUpdateCustomer);

        return updatedCustomer.MapToDTO();
    }
}

