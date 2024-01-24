using ExampleAPI.Core;

namespace ExampleAPI.Entities;

public class OrderItem : Entity<Guid>
{
    public required Guid OrderId { get; set; }
    public required Guid ProductId { get; set; }
    public required ushort Quantity { get; set; }
    public virtual Product Product { get; set; }
}