using System.ComponentModel;

namespace Lektion15;

class Program
{
	static void Main(string[] args)
	{
		List<Animal> animals = [new Dog("Grace"), new Cat("Barbro")];
		List<Sensor> sensors = [new TemperatureSensor(), new HumiditySensor()];
		foreach (Animal a in animals)
		{
			Console.WriteLine($"{a.Name} säger {a.MakeSound()}");
		}
		foreach (Sensor s in sensors)
		{
			Console.WriteLine($"{s.ReadValue()}{s.GetUnitSymbol()}");
		}
	}
}

public abstract class Animal
{
	public abstract string MakeSound();
	public abstract string Name { get; protected set; }
}

public class Dog : Animal
{
	public override string Name { get; protected set; }
	public Dog(string name)
	{
		Name = name;
	}
	public override string MakeSound() => "Woof!";
}

public class Cat : Animal
{
	public override string Name { get; protected set; }
	public Cat(string name)
	{
		Name = name;
	}
	public override string MakeSound() => "Meow!";
}

public abstract class Sensor
{
	public enum Unit
	{
		Celcius, Fahrenheit, Kelvin, Percentage
	}
	public Unit SensorUnit { get; protected set; }

	public string GetUnitSymbol()
	{
		string symbol = "";
		switch (SensorUnit)
		{
			case Unit.Celcius:
				symbol = "C";
				break;
			case Unit.Fahrenheit:
				symbol = "F";
				break;
			case Unit.Kelvin:
				symbol = "K";
				break;
			case Unit.Percentage:
				symbol = "%";
				break;
		}
		return symbol;
	}
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