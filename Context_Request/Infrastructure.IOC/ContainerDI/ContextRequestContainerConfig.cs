﻿using Application.DTOs;
using Application.Request.Events.EventBus;
using Application.Request.Events.Request.Handlers.CreatedRequestEventHandlers;
using Application.Request.Events.Request.Handlers.CreatePruductEvents;
using Application.Request.Services.Request;
using Application.Request.Tools.Notifiers;
using Application.Services.Request.Interfaces;
using Domain.Request.Repositories;
using Domain.Request.Repositories.Request;
using Domain.Request.Services;
using Domain.Request.Services.Request.Interfaces;
using Infrastructure.Request.Database.EntityFramework;
using Infrastructure.Request.Repositories;
using Infrastructure.Request.Repositories.Request;
using Infrastructure.Request.Tools.Notifiers;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.IOC.Request.ContainerDI
{
    public class ContextRequestContainerConfig
    {
        public static void AddContextRequestServices(IServiceCollection services) // Tornar público
        {
            services.AddScoped<DataContext>();
            services.AddScoped<RequestApplicationServiceDependencies>();
            services.AddScoped<IRequestApplicationService, RequestApplicationService>();
            services.AddScoped<IRequestDomainService, RequestDomainService>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(cfg =>
             {
                 //É necessário somente 1 instancia e o mediatr localiza os outros, mas por organizacao, coloquei todos.
                 cfg.RegisterServicesFromAssembly(typeof(CreatedRequestEmailNotificationEventHandler).Assembly);
                 cfg.RegisterServicesFromAssembly(typeof(NotificationStockContextUpdateStockItemEventHandler).Assembly);
             });

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
