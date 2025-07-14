namespace RoadNetworkService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return services
                    .AddValidatorsFromAssembly(assembly)
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
                    .AddMediatR(cfg =>
                    {
                        cfg.RegisterServicesFromAssembly(assembly);
                        cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                    });
        }
    }
}
