using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MigrationsTools.WebApi.Data;
using MigrationsTools.WebApi.Dtos;

namespace MigrationsTools.WebApi.Controllers;

[ApiController]
[Route("Migrations")]
public class MigrationsController : ControllerBase
{
    private readonly SchoolDbContext _dbContext;

    public MigrationsController(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<MigrationDto[]>> GetMigrationsAsync(CancellationToken ct)
    {
        var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
        var appliedMigrations = await _dbContext.Database.GetAppliedMigrationsAsync(ct);

        MigrationDto[] allMigrations = pendingMigrations.Select(m => new MigrationDto(m, MigrationState.Pending))
            .Concat(appliedMigrations.Select(m => new MigrationDto(m, MigrationState.Applied)))
            .ToArray();

        return Ok(allMigrations);
    }

    [HttpPost("Migrate")]
    public async Task<IActionResult> MigrationUpAsync([FromBody] MigrateRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.MigrationName))
        {
            return BadRequest();
        }
        
        await _dbContext.GetService<IMigrator>().MigrateAsync(request.MigrationName, ct);
        return Ok();
        //await _dbContext.Database.MigrateAsync(ct);
    }

    [HttpGet("GenerateScript")]
    public IActionResult GenerateScript([FromQuery] GenerateSqlRequest request)
    {
        return Ok(_dbContext.GetService<IMigrator>().GenerateScript(request.FromMigration, request.ToMigration));
    }
}