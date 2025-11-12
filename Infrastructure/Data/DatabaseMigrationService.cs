using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Studio.Barber.Backend.Infrastructure.Data;

public class DatabaseMigrationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        try
        {
            // Verificar se a coluna barbershopId permite NULL, se não, alterar
            var connection = context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);
            
            using var command = connection.CreateCommand();
            command.CommandText = @"
                DO $$
                BEGIN
                    -- Verificar se a coluna existe e se não permite NULL
                    IF EXISTS (
                        SELECT 1 
                        FROM information_schema.columns 
                        WHERE table_name = 'Barber' 
                        AND column_name = 'barbershopId'
                        AND is_nullable = 'NO'
                    ) THEN
                        -- Alterar a coluna para permitir NULL
                        ALTER TABLE ""Barber"" ALTER COLUMN ""barbershopId"" DROP NOT NULL;
                    END IF;
                END $$;
            ";
            
            await command.ExecuteNonQueryAsync(cancellationToken);
            await connection.CloseAsync();
        }
        catch (Exception ex)
        {
            // Log do erro, mas não impede a aplicação de iniciar
            Console.WriteLine($"Aviso: Não foi possível alterar a coluna barbershopId: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

