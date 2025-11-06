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

    /// <summary>
    /// 这是对外暴露的构造步骤添加器：
    /// 接收一个 Action<Person>，表示“对 Person 做某事”
    /// 实际逻辑由 AddAction() 完成
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public TSelf Do(Action<Person> action)
        => AddAction(action);

    /// <summary>
    /// 这是构造链的执行器：
    /// 创建一个新的 Person 对象
    /// 依次应用所有变换函数，返回最终构造好的对象
    /// 📌 意义：将所有构造步骤一次性应用，生成完整对象。
    /// </summary>
    /// <returns></returns>
    public Person Build()
        // 将列表压缩成一个
        => actions.Aggregate(new Person(), (p, f) => f(p));

    /// <summary>
    /// 因为是私有的，所以需要围绕他构建某种包装器来执行一些实际有用的工作。
    /// 🧱 背景结构
    /// actions 是一个 List<Func<Person, Person>>，即一个“变换函数列表”。
    ///每个 Func<Person, Person> 表示一个对 Person 对象的变换操作。
    ///最终通过 Build() 方法将这些变换函数依次应用，构造出完整的 Person 对象。
    /// 🧩 功能实现细节逐步拆解
    // ① Action<Person> 是什么？
    // Action<Person> 是一个委托类型，表示一个接受 Person 参数但不返回值的方法。
    // 示例：p => p.Name = "Max" 就是一个 Action<Person>。
    // 📌 问题：Action 不能直接参与函数式组合，因为它没有返回值。
    // 这是一个 lambda 表达式，接受一个 Person 对象 p。
    // 它内部执行 action(p)，即对 p 做某种修改。
    // 然后返回修改后的 p，使其符合 Func<Person, Person> 的签名。
    // 📌 意义：将不可组合的 Action 转化为可组合的变换函数。
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
