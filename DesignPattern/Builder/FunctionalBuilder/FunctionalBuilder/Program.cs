using System;

namespace FunctionalBuilder;

public class Person
{
    public string Name, Position;
}

public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<Person, Person>> actions
        = new List<Func<Person, Person>>();

    public  TSelf Do(Action<Person> action)
        => AddAction(action);

    public Person Build()
        // 将列表压缩成一个
        => actions.Aggregate(new Person(), (p, f) => f(p));

    /// <summary>
    /// 因为是私有的，所以需要围绕他构建某种包装器来执行一些实际有用的工作。
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private TSelf AddAction(Action<Person> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });

        return (TSelf)this;
    }
}

/// <summary>
/// 特别使用Sealed， 为了展示扩展方法。 
/// </summary>
public sealed class PersonBuilder
    : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name)
        => Do(p => p.Name = name);
}

public static class PersonBuilderExtentions
{
    public static PersonBuilder WorksAs (this PersonBuilder builder, string position)
            =>  builder.Do(p => p.Position = position);
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
            .WorksAs("Software Developer")
            .Build();
    }
}
