using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FelipeApiDapperTemplate.Domain.Services;
using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FluentValidation;

namespace FelipeApiDapperTemplate.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _customerService.GetAsync();
        return Ok(customers);
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetCustomersById(int id)
    {
        try
        {
            var customer = await _customerService.GetByIdAsync(id);
            return Ok(customer);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO createCustomer, [FromServices] IValidator<CreateCustomerDTO> validator)
    {
        var validationResult = await validator.ValidateAsync(createCustomer);

        if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

        var customer = await _customerService.CreateAsync(createCustomer);

        return Ok(customer);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDTO updateCustomer, [FromServices] IValidator<UpdateCustomerDTO> validator)
    {
        var validationResult = await validator.ValidateAsync(updateCustomer);

        if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

        try
        {
            var customer = await _customerService.UpdateAsync(id, updateCustomer);

            return Ok(customer);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}

