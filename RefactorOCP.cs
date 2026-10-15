class Program
{
	static void Main()
	{
		var items = new List<OrderItem>
		{
			new("Bok", 2, 120m),
			new("Penna", 5, 10m),
			new("Anteckningsblock", 3, 45m),
			new("Suddgummi", 10, 5m),
			new("Linjal", 4, 15m),
			new("Ryggsäck", 1, 499m),
			new("Vattenflaska", 2, 150m),
			new("Överstrykningspenna", 6, 22m),
			new("Sax", 2, 35m),
			new("Häftapparat", 1, 89m),
			new("Kalkylator", 1, 299m)
		};
		DiscountPolicy discount = new PaltkomaDiscount(DateTime.Now); // byt här till andra rabatter
		DiscountPolicy discount1 = new ItemCountDiscount(items);
		var checkout = new Checkout(discount);
		var checkout2 = new Checkout(discount1);

		var total = checkout.Total(items);
		var total2 = checkout2.Total(items);
		Console.WriteLine($"Totalt att betala: {total:C}");

		Console.WriteLine($"Totalt att betala: {total2:C}");
	}
}

class OrderItem
{
	public string Name { get; }
	public int Quantity { get; }
	public decimal UnitPrice { get; }

	public OrderItem(string name, int qty, decimal price)
		=> (Name, Quantity, UnitPrice) = (name, qty, price);
}

// Abstrakt basklass för rabatter
abstract class DiscountPolicy
{
	// Virtuell metod som kan overridas i subklasser
	public virtual decimal Apply(decimal subtotal) => subtotal;

	// Hjälpmetod som kan användas av alla subklasser
	protected decimal PercentageOff(decimal subtotal, decimal percent)
		=> subtotal * (1 - percent);
}

// --- Konkreta rabattklasser ---
class NoDiscount : DiscountPolicy
{
	// Ärver standardbeteende (ingen rabatt)
}

class PercentageDiscount : DiscountPolicy
{
	private readonly decimal _percent;
	public PercentageDiscount(decimal percent) => _percent = percent;

	public override decimal Apply(decimal subtotal) => PercentageOff(subtotal, _percent);
}

class ThresholdDiscount : DiscountPolicy
{
	private readonly decimal _threshold;
	private readonly decimal _percent;

	public ThresholdDiscount(decimal threshold, decimal percent)
	{
		_threshold = threshold;
		_percent = percent;
	}

	public override decimal Apply(decimal subtotal)
	{
		if (subtotal >= _threshold)
			return PercentageOff(subtotal, _percent);
		return subtotal;
	}
}

class ItemCountDiscount : DiscountPolicy
{
	private List<OrderItem> _items;
	public ItemCountDiscount(List<OrderItem> items) => _items = items;

	public override decimal Apply(decimal subtotal) => (_items.Count > 10) ? PercentageOff(subtotal, 0.1m) : subtotal;
}

class PaltkomaDiscount : DiscountPolicy
{
	private int _timeThreshold = 12;
	private DateTime _time;
	public PaltkomaDiscount(DateTime time) => _time = time;
	public override decimal Apply(decimal subtotal) => (_time.Hour > _timeThreshold) ? PercentageOff(subtotal, 0.15m) : subtotal;
}

// --- Checkoutklass som är OCP-kompatibel ---
class Checkout
{
	private readonly DiscountPolicy _discount;
	public Checkout(DiscountPolicy discount) => _discount = discount;

	public decimal Total(IEnumerable<OrderItem> items, decimal vatRate = 0.25m)
	{
		decimal subtotal = 0;
		foreach (var i in items)
			subtotal += i.UnitPrice * i.Quantity;

		var discounted = _discount.Apply(subtotal);
		var vat = discounted * vatRate;
		return discounted + vat;
	}
}
