using App.Application.Contracts.ServiceBus;
using App.Domain.Events;
using MassTransit;

namespace App.Bus
{
    public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
    {
        public async Task PublishAsync<T>(T publishedEvent, CancellationToken cancellation = default) where T : IEventOrMessage
        {
            await publishEndpoint.Publish(publishedEvent, cancellation);
        }

        public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellation = default) where T : IEventOrMessage
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await endpoint.Send(message, cancellation);
        }
    }
}
