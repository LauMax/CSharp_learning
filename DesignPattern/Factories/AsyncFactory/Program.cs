using System.Threading.Tasks;

namespace AsyncFactory;

public class Foo
{
    private Foo()
    {
        // 
    }

    public async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }
    
    public static Task<Foo> CreateAsync()
    {
        var result = new Foo();
        return result.InitAsync();   
    }
}

public class Demo
{
    public static async Task Main(string[] args)
    {
        Foo x  =  await Foo.CreateAsync(); 
    }
}
