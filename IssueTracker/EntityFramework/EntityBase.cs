public abstract class EntityBase<PrimaryKeyType>: IAuditable, ISoftDelete
{
    public PrimaryKeyType Id { get; set; }
    public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
    public virtual DateTime UpdatedDate { get; set; } = DateTime.Now;
    public virtual bool IsDeleted { get; set; } = false;
}