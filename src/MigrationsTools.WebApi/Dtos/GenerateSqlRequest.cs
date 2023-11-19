namespace MigrationsTools.WebApi.Dtos;

public class GenerateSqlRequest
{
    public string? FromMigration { get; set; }

    public string? ToMigration { get; set; }
}