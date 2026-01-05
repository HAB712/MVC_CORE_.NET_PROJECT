namespace Grocery.Models
{
    public class HomeView
    {
        public List<Product> Product = new();
        public List<Category> Category = new();
        public List<CartItem> CartItems = new();
        public List<Customer> Customers = new();
        public List<Order> Orders = new();
        public List<OrderItem> OrderItems = new();
        public List<Cart> Carts = new();
    }
}
