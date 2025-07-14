namespace RoadNetworkService.Infrastructure.Persistence.Connections
{
    public class PostgresConnectionFactory : IPostgresConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public PostgresConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<NpgsqlConnection> CreateAsync(CancellationToken cancellationToken = default)
        {
            var connectionString = _configuration.GetConnectionString("Database");
            var connection = new NpgsqlConnection(connectionString);
            return Task.FromResult(connection);
        }
    }

}
