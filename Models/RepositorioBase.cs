using Microsoft.Extensions.Configuration;

namespace Inmobiliaria.Models;

public abstract class RepositorioBase
{
    protected readonly string connectionString;

    public RepositorioBase(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
}