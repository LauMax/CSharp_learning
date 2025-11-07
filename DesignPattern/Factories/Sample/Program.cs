namespace Sample;

/// <summary>
/// 工厂：知道如何以特定方式初始化类型的单独组件，目前构造函数必须是public的
/// </summary>
public static class PointFactory
{
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}

public class Point
{
    private double x, y;

    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{x} and {y}";
    }
}

/// <summary>
/// 工厂方法的优点：
/// 1. 对具有相同参数集的方法进行重载
/// 2. 方法名称独特，在名称中对要创建的点的类型提出了建议
/// </summary>
public class FactoryMethod
{
    public static void Main(string[] args)
    {
        var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}
