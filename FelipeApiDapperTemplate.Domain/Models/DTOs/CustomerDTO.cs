namespace FelipeApiDapperTemplate.Domain.Models.DTOs;

public record CustomerDTO(int Id, string Name, int Age, string Phone, string? Email, DateTime CreatedAt, DateTime? UpdatedAt);

