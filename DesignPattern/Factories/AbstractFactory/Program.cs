using static System.Console;

namespace AbstractFactory;

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        WriteLine("This tea is nice but I'd prefer it with milk");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        WriteLine("This coffee is sensational!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    public enum AvailableDrink
    {
        Coffee, Tea
    }

    private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
        new Dictionary<AvailableDrink, IHotDrinkFactory>();

    public HotDrinkMachine()
    {
        foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        {
            var factory = (IHotDrinkFactory)Activator.CreateInstance(
                Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
            );

            factories.Add(drink, factory);
        }
    }
    
    public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    {
        return factories[drink].Prepare(amount);
    }
}

/// <summary>
/// 抽象工厂唯一用途是提供抽象对象 而不是具体对象。
/// </summary>
public class Demo
{
    public static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
        drink.Consume();
    }
}
