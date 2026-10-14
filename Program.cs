namespace Lektion15;

class Program
{
	static void Main(string[] args)
	{
		List<Animal> animals = [new Dog("Grace"), new Cat("Barbro")];
		List<Sensor> sensors = [new TemperatureSensor(), new HumiditySensor(), new LightSensor()];
		foreach (Animal a in animals)
		{
			Console.WriteLine($"{a.Name} säger {a.MakeSound()}");
		}
		foreach (Sensor s in sensors)
		{
			Console.WriteLine($"{s.ReadValue()}{s.UnitSymbol}");
		}
	}
}
public abstract class Animal
{
	public abstract string MakeSound();
	public string Name { get; protected set; }
	protected Animal(string name)
	{
		Name = name;
	}
}
public class Dog : Animal
{
	public Dog(string name) : base(name) { }
	public override string MakeSound() => "Woof!";
}
public class Cat : Animal
{
	public Cat(string name) : base(name) { }
	public override string MakeSound() => "Meow!";
}
public abstract class Sensor
{
	public enum Unit
	{
		Celcius, Fahrenheit, Kelvin, Percentage, Lux
	}
	public Unit SensorUnit { get; protected set; }

	public string UnitSymbol => SensorUnit switch
	{
		Unit.Celcius => "°C",
		Unit.Fahrenheit => "°F",
		Unit.Kelvin => "°K",
		Unit.Percentage => "%",
		Unit.Lux => "lx",
		_ => "Unknown"
	};
	public abstract double ReadValue();
}
public class TemperatureSensor : Sensor
{

	public TemperatureSensor()
	{
		SensorUnit = Unit.Celcius;
	}
	public override double ReadValue() => Random.Shared.Next(-10, 51);
}

public class HumiditySensor : Sensor
{
	public HumiditySensor()
	{
		SensorUnit = Unit.Percentage;
	}
	public override double ReadValue() => Random.Shared.Next(0, 101);
}
public class LightSensor : Sensor
{
	public LightSensor()
	{
		SensorUnit = Unit.Lux;
	}
	public override double ReadValue() => Random.Shared.Next(0, 1001);
}