

using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace ObjectTracking;

public class Demo
{
    /// <summary>
    /// 工厂可以追踪他创建的每个对象，
    /// 可以使用弱引用来避免延长已构建对象的生命周期
    /// 否则对象将与工厂一起存活，会干扰GC。
    /// 并且可以用技巧使得对象可替换，可以执行批量替换，这个在动态编程或者脚本编写时很有用
    /// 当用户更新了他们的脚本，想编译他们的更改并在系统内部进行修改，这是一个好方法
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(false);
        var theme2 = factory.CreateTheme(true);
        Console.WriteLine(factory.Info);

        var factory2 = new ReplaceableThemeFactory();
        var magicTheme = factory2.CreateTheme(true);
        Console.WriteLine(magicTheme.Value.BgrColor);
        factory2.ReplaceTheme(false);
        Console.WriteLine(magicTheme.Value.BgrColor);
    }
}

public class Ref<T> where T : class
{
    public T Value;
    public Ref(T value)
    {
        Value = value;
    }
}

public class ReplaceableThemeFactory
{
    private readonly List<WeakReference<Ref<ITheme>>> themes = new();
    private ITheme createThemeImpl(bool dark)
    {
        return dark ? new DarkTheme() : new LightTheme();
    }

    public Ref<ITheme> CreateTheme(bool dark)
    {
        var r = new Ref<ITheme>(createThemeImpl(dark));
        themes.Add(new(r));
        return r;
    }
    
    public void ReplaceTheme(bool dark)
    {
        foreach(var wv in themes)
        {
            if(WriteOnceBlock.TryGetTarget(out var reference))
            {
                reference.Value = createThemeImpl(dark);
            }
        }
    }
}

public class TrackingThemeFactory
{
    private readonly List<WeakReference<ITheme>> themes = new();
    public ITheme CreateTheme(bool dark)
    {
        ITheme theme = dark ? new DarkTheme() : new LightTheme();
        themes.Add(new WeakReference<ITheme>(theme));
        return theme;
    }

    public string Info
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var reference in themes)
            {
                if (reference.TryGetTarget(out var theme))
                {
                    bool dark = theme is DarkTheme;
                    sb.Append(dark ? "Dark" : "Light")
                        .AppendLine("theme");
                }
            }

            return sb.ToString();
        }
    }
}

public interface ITheme
{
    string TextColor { get; }
    string BgrColor { get; }
}

class LightTheme : ITheme
{
    public string TextColor => "black";
    public string BgrColor => "white";
}

class DarkTheme : ITheme
{
    public string TextColor => "white";
    public string BgrColor => "dark gray";
}