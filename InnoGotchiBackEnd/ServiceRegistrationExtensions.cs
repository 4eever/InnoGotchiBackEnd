using BusinessAccessLayer.Mappings;
using BusinessAccessLayer.Services;
using BusinessAccessLayer.Validators;
using DataAccessLayer.Repositories;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistrationExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        AddScopedServices(services);
        AddRepositories(services);
        AddAutoMapperProfiles(services);
        AddValidators(services);
    }

    private static void AddScopedServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFarmService, FarmService>();
        services.AddScoped<IUserFarmService, UserFarmService>();
        services.AddScoped<IInnogotchiService, InnogotchiService>();
        services.AddScoped<IInnogotchiBodyPartService, InnogotchiBodyPartService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFarmRepository, FarmRepository>();
        services.AddScoped<IUserFarmRepository, UserFarmRepository>();
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IInnogotchiRepository, InnogotchiRepository>();
        services.AddScoped<IDeadInnogotchiRepository, DeadInnogotchiRepository>();
        services.AddScoped<IInnogotchiBodyPartRepository, InnogotchiBodyPartRepository>();
        services.AddScoped<IBodyPartRepository, BodyPartRepository>();
    }

    private static void AddAutoMapperProfiles(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperUserProfile));
        services.AddAutoMapper(typeof(AutoMapperFarmProfile));
        services.AddAutoMapper(typeof(AutoMapperUserFarmProfile));
        services.AddAutoMapper(typeof(AutoMapperInnogotchiProfile));
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddScoped<IUserValidatorFactory, UserValidatorFactory>();
        services.AddScoped<IFarmValidatorFactory, FarmValidatorFactory>();
        services.AddScoped<IInnogotchiValidatorFactory, InnogotchiValidatorFactory>();
    }
}
