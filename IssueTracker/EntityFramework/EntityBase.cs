public abstract class EntityBase<PrimaryKeyType>: IAuditable, ISoftDelete
{
    public PrimaryKeyType Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime UpdatedDate { get; set; }
    public virtual bool IsDeleted { get; set; }
}