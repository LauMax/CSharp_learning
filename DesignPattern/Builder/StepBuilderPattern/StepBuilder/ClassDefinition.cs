namespace StepBuilder;

public enum CarType
{
    Sedan,
    Crossover
}

/// <summary>
/// 特定的车型，轮子大小有范围，所以构建时需要按步骤来构造
/// 
/// </summary>
public class Car
{
    public CarType Type;
    public int WheelSize;
}

public interface ISpecifyCarType
{
    ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    IBuildCar WithWheels(int size); 
}


public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl :
        ISpecifyCarType,
        ISpecifyWheelSize,
        IBuildCar  // 只添加所有实现，使用默认成员
    {
        private Car car = new Car();
        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this;
        }

        public IBuildCar WithWheels(int size)
        {
            switch (car.Type)
            {
                case CarType.Crossover when size < 17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheel for {car.Type}");
            }

            car.WheelSize = size;
            return this;
        }

        public Car Build()
        {
            return car;
        }
    }
    
    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}
