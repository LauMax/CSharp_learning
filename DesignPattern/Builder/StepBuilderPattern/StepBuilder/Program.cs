using System.Globalization;
using StepBuilder;

namespace StepBuilder;

public class Program
{
    /// <summary>
    /// Question: 如何让构建器按特定顺序一个接一个执行一系列步骤
    /// 
    /// 利用原理：接口隔离原则
    ///     将接口当成向导 一步一步构建
    /// 
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var car = CarBuilder.Create()  // ISpecifyCarType
            .OfType(CarType.Crossover) // ISpecifyWheelSize
            .WithWheels(18)             // IBuildCar
            .Build();
    }
}