namespace RoadNetworkService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPostgresConnectionFactory, PostgresConnectionFactory>();
            services.AddScoped<IRoadNetworkRepository, RoadNetworkRepository>();
            return services;
        }
    }
}
