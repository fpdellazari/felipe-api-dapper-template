using AutoMapper;
using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FelipeApiDapperTemplate.Domain.Repositories;
using FelipeApiDapperTemplate.Domain.Services;

namespace FelipeApiDapperTemplate.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDTO> CreateAsync(CreateCustomerDTO createCustomer)
    {
        var customer = await _customerRepository.CreateAsync(createCustomer);

        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<IEnumerable<CustomerDTO>> GetAsync()
    {
        var customers = await _customerRepository.GetAsync();

        return _mapper.Map<List<CustomerDTO>>(customers);
    }

    public async Task<CustomerDTO> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer is null) throw new KeyNotFoundException("Cliente não encontrado.");

        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<CustomerDTO> UpdateAsync(int id, UpdateCustomerDTO updateCustomer)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer is null) throw new KeyNotFoundException("Cliente não encontrado.");

        var updatedCustomer = await _customerRepository.UpdateAsync(id, updateCustomer);

        return _mapper.Map<CustomerDTO>(updatedCustomer);
    }
}

