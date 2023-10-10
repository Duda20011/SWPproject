

namespace Services.Entity
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool isPaid { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
