namespace Lektion15;

class Program
{
	static void Main(string[] args)
	{
		List<Animal> animals = [new Dog(), new Cat()];
		foreach (Animal a in animals)
		{
			Console.WriteLine(a.MakeSound());
		}
	}
}

public abstract class Animal
{
	public abstract string MakeSound();
}

public class Dog : Animal
{
	public override string MakeSound() => "Woof!";
}

public class Cat : Animal
{
	public override string MakeSound() => "Meow!";
}
