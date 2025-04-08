namespace FelipeApiDapperTemplate.Domain.Models.Entities;

public class Customer
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public int Age { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

