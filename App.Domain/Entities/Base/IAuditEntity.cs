namespace App.Domain.Entities.Base
{
    public interface IAuditEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
