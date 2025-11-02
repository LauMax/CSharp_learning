using System;

namespace Builder
{
    /// <summary>
    /// 流畅构建者继承与递归泛型
    /// Fluent Builder Inheritance, Recursive Generics
    /// 如果在有了一个PsersonInfoBuilder以后 
    /// 还要再创建一个新的构建者，拥有已有的属性功能，还要多加新属性功能
    /// 
    /// 核心问题： 派生类如何将有关返回类型的信息传播到自己的基类
    ///     => 递归泛型
    /// Target: 
    /// 1. 实现链式调用（fluent API）：像 .SetName("Max").SetAge(30).Build() 这样优雅地构建对象。
    /// 2. 支持继承：子类可以扩展构建器，但仍然保留链式调用。
    /// 3. 保持类型安全：每一步返回的是当前构建器的类型，而不是父类的类型。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New
                .called("Max")
                .WorksAsA("Software Engineer")
                .Build();
            System.Console.WriteLine(me);
        }
    }

    public class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {

        }
        
        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    // class Foo : Bar<Foo>
    public class PersonInfoBuilder<SELF>
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF>
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF: PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    // 不能直接调用， 因为PersonInfoBuilder的索引对象，对WorksAsA这个方法一无所知
    // public class PersonJobBuilder: PersonInfoBuilder
    // {
    //     public PersonJobBuilder WorksAsA(string position)
    //     {
    //         person.Position = position;
    //         return this;
    //     }
    // }
}
