using ExampleAPI.Core;

namespace ExampleAPI.Entities;
public class Stock : Entity<Guid>
{
    public Guid ProductId { get; set; }
    public ushort Count { get; set; }
    public virtual Product Product { get; set; }
}
