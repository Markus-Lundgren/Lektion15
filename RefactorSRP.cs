// class Program
// {
// 	static void Main()
// 	{
// 		var items = new List<OrderItem>
// 		{
// 			new("Bok", 2, 120m),
// 			new("Penna", 5, 10m)
// 		};

// 		var order = new Order();
// 		order.Add(items);
// 		ReceiptPrinter.PrintReceipt(order);
// 	}
// }

// class OrderItem
// {
// 	public string Name { get; }
// 	public int Quantity { get; }
// 	public decimal UnitPrice { get; }
// 	public OrderItem(string name, int qty, decimal price)
// 		=> (Name, Quantity, UnitPrice) = (name, qty, price);
// }

// class Order
// {
// 	private readonly List<OrderItem> _items = new();
// 	public List<OrderItem> GetList => _items;
// 	public void Add(IEnumerable<OrderItem> items) => _items.AddRange(items);

// 	// ANSVAR 1: beräkningar
// 	public decimal CalculateSubtotal()
// 	{
// 		decimal sum = 0;
// 		foreach (var i in _items) sum += i.UnitPrice * i.Quantity;
// 		return sum;
// 	}

// 	public decimal CalculateVat(decimal rate = 0.25m)
// 	{
// 		return CalculateSubtotal() * rate;
// 	}

// 	public decimal CalculateTotal(decimal rate = 0.25m)
// 	{
// 		return CalculateSubtotal() + CalculateVat(rate);
// 	}
// }

// class ReceiptPrinter
// {
// 	public static void PrintReceipt(Order order)
// 	{
// 		Console.WriteLine("KVITTO");
// 		foreach (var i in order.GetList)
// 			Console.WriteLine($"{i.Name} x{i.Quantity} á {i.UnitPrice:C} = {(i.UnitPrice * i.Quantity):C}");
// 		Console.WriteLine($"Delsumma: {order.CalculateSubtotal():C}");
// 		Console.WriteLine($"Moms (25%): {order.CalculateVat():C}");
// 		Console.WriteLine($"Totalt: {order.CalculateTotal():C}");
// 	}
// }
