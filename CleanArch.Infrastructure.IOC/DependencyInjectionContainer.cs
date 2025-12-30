using CleanArch.Domain.Behaviours;
using CleanArch.Domain.SeedWork;
using CleanArch.Infrastructure.Data.DBContext;
using CleanArch.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CleanArch.ApplicationCore.Usecases.BankUsecases;
using FluentValidation;
using MediatR;
using System.Reflection;
using CleanArch.ApplicationCore.Behaviours;
using CleanArch.ApplicationCore.Responses;
using CleanArch.Domain.DomainEvents;

namespace CleanArch.Infrastructure.IOC;

public static class DependencyInjectionContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanArchContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContextConnection")));

        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        });
        services.AddScoped<IValidator<AddBankAccountCommand>, AddBankAccountCommandValidator>();


        // Command Handlers (CQRS)
        services.AddScoped<IRequestHandler<AddBankAccountCommand, HandlerResponse>, AddBankAccountCommandHandler>();

        return services;
    }

    
}
