using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumers
{
    public class ProductAddedEventConsumer() : IConsumer<ProductAddedEvent>
    {
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            Console.WriteLine($"Product added: {context.Message.Name} with ID: {context.Message.Id} and Price: {context.Message.Price}");
            return Task.CompletedTask;
        }
    }
}
