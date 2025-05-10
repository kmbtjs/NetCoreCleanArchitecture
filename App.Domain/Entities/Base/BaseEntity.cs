namespace App.Domain.Entities.Base
{
    public class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}
