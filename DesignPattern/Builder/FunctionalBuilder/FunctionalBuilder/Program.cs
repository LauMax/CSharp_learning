using System;

namespace FunctionalBuilder;

public class Person
{
    public string Name, Position;
}

public sealed class PersonBuilder
{
    private readonly List<Func<Person, Person>> actions
        = new List<Func<Person, Person>>();

    public PersonBuilder Called(string name)
        => Do(p => p.Name = name);

    public PersonBuilder Do(Action<Person> action)
        => AddAction(action);

    /// <summary>
    /// 因为是私有的，所以需要围绕他构建某种包装器来执行一些实际有用的工作。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private PersonBuilder AddAction(Action<Person> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });

        return this;
    }
}

public class Program
{
    /// <summary>
    /// 用函数式的方式来创建，同时遵守开闭原则
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var person = new PersonBuilder()
            .Called("Max")
            .Build(); 
    }
}
