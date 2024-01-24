using ExampleAPI.Core;
using System.ComponentModel.DataAnnotations;

namespace ExampleAPI.Entities;
public class Order : Entity<Guid>
{
    public required Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public Order()
    {
        OrderItems = new HashSet<OrderItem>();
    }
}