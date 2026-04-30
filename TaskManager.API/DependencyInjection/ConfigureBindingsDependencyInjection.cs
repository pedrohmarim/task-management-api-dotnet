namespace TaskManager.API.DependencyInjection
{
    public static class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            // Centraliza o registro de dependências da aplicação :)
            // organizando por responsabilidade (Database, Application, Repository)

            ConfigureBindingsDatabase.Register(services, configuration);

            ConfigureBindingsApplication.Register(services);

            ConfigureBindingsRepository.Register(services);

            ConfigureBindingsUnitOfWork.Register(services);
        }
    }
}