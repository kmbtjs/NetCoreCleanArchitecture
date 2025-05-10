using App.Application.Contracts.ServiceBus;
using App.Bus.Consumers;
using App.Domain.Constants;
using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Bus
{
    public static class BusExtensions
    {
        public static void AddBusExt(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

            services.AddScoped<IServiceBus, ServiceBus>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductAddedEventConsumer>();
                //config => {config.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(5)));}

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url), h => { });
                    cfg.ReceiveEndpoint(ServiceBusConstants.ProductAddedEventQueue, e =>
                    {
                        e.ConfigureConsumer<ProductAddedEventConsumer>(context);
                    });
                });
            });
        }
    }
}
