using Dapper;
using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FelipeApiDapperTemplate.Domain.Models.Entities;
using FelipeApiDapperTemplate.Domain.Repositories;
using System.Data;

namespace FelipeApiDapperTemplate.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnection _dbConnection;

    public CustomerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Customer> CreateAsync(CreateCustomerDTO createCustomer)
    {
        string query = @" INSERT INTO Customer (Name, 
                                                Age, 
                                                Phone, 
                                                Email)
                          OUTPUT INSERTED.*
                          VALUES (@Name, 
                                  @Age, 
                                  @Phone, 
                                  @Email); ";

        var customer = await _dbConnection.QuerySingleAsync<Customer>(query, createCustomer);

        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAsync()
    {
        string query = @" SELECT    Id, 
                                    Name, 
                                    Age, 
                                    Phone, 
                                    Email, 
                                    CreatedAt, 
                                    UpdatedAt 
                          FROM Customer; ";

        var customers = await _dbConnection.QueryAsync<Customer>(query);

        return customers;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        string query = @" SELECT    Id, 
                                    Name, 
                                    Age, 
                                    Phone, 
                                    Email, 
                                    CreatedAt, 
                                    UpdatedAt 
                          FROM Customer
                          WHERE Id = @Id;";

        var customer = await _dbConnection.QueryFirstOrDefaultAsync<Customer>(query, new { id });

        return customer;
    }

    public async Task<Customer> UpdateAsync(int id, UpdateCustomerDTO updateCustomer)
    {
        string query = @" UPDATE Customer SET Name = @Name,
				                                  Age = @Age,
				                                  Phone = @Phone,
				                                  Email = @Email,
				                                  UpdatedAt = GETDATE()
                                    OUTPUT INSERTED.*
                                    WHERE Id = @Id; ";

        var parameters = new 
        { 
            Id = id,
            Name = updateCustomer.Name,
            Age = updateCustomer.Age,
            Phone = updateCustomer.Phone,
            Email = updateCustomer.Email
        };

        var customer = await _dbConnection.QuerySingleAsync<Customer>(query, parameters);

        return customer;
    }
}

