namespace Basket.API.Entities;

public class Cart
{
    public string Username { get; set; }
    public List<CartItem> Items { get; set; }

    public Cart()
    {
    }

    public Cart(string username) => Username = username;
    public decimal ToTalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);
}
