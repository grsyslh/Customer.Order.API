namespace Order.Domain.Entity.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; }
        DateTime CreatedDate { get; set; }

        DateTime? ModifiedDate { get; set; }

        bool IsDeleted { get; set; }
    }
}
