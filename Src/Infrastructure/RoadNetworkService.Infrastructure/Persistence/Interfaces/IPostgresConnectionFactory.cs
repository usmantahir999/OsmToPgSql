using Npgsql;

namespace RoadNetworkService.Infrastructure.Persistence.Interfaces
{
    public interface IPostgresConnectionFactory
    {
        Task<NpgsqlConnection> CreateAsync(CancellationToken cancellationToken = default);
    }
}
