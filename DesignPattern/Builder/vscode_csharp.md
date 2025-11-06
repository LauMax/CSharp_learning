

# VS Code for C#

####  如何创建一个solution
```powershell
dotnet new sln -n MySolution
```
这会创建一个名为 `MySolution.sln` 的解决方案文件。

#### 创建 Console 项目
```powershell
dotnet new console -n MyConsoleApp
```
这会创建一个包含 `.csproj` 和 `Program.cs` 的控制台项目文件夹。

#### 🔗 步骤三：将项目添加到解决方案
```powershell
dotnet sln MySolution.sln add MyConsoleApp/MyConsoleApp.csproj
```

### 最终结构
```
MySolution/
├── MySolution.sln
├── MyConsoleApp/
│   ├── MyConsoleApp.csproj
│   └── Program.cs

```

#### 🧠 可选扩展：添加类库项目
```
dotnet new classlib -n MyLibrary
dotnet sln add MyLibrary/MyLibrary.csproj
dotnet add MyConsoleApp/MyConsoleApp.csproj reference MyLibrary/MyLibrary.csproj
```


| 类型             | 命令示例                              | 说明                           |
|------------------|----------------------------------------|--------------------------------|
| 控制台应用       | `dotnet new console`                   | 默认使用顶级语句               |
| 类库             | `dotnet new classlib`                  | 创建 `.dll` 项目               |
| Web API          | `dotnet new webapi`                    | RESTful API 项目               |
| ASP.NET MVC      | `dotnet new mvc`                       | 带视图的 Web 项目              |
| Blazor Server    | `dotnet new blazorserver`              | 服务器端 Blazor                |
| Blazor WebAssembly| `dotnet new blazorwasm`               | 客户端 Blazor                  |
| 单元测试         | `dotnet new xunit`                     | 使用 xUnit 的测试项目          |
| NUnit 测试       | `dotnet new nunit`                     | 使用 NUnit 的测试项目          |
| MSTest 测试      | `dotnet new mstest`                    | 使用 MSTest 的测试项目         |
| 空项目           | `dotnet new project`                   | 最小化结构                     |
| 解决方案         | `dotnet new sln`                       | 创建 `.sln` 文件               |


## 🧠 什么是 Top-level Statements？

Top-level statements 是从 C# 9.0 开始引入的一种简化语法，允许你在不显式声明 Main 方法、类或命名空间的情况下直接编写代码。

它的目标是让简单程序（比如控制台应用）更简洁、更易读，尤其适合入门者或脚本式开发。

🧱 传统写法（C# 8 及以前）

```csharp
using System;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Max!");
        }
    }
}
```

✨ 顶级语句写法（C# 9+）
```csharp
using System;

Console.WriteLine("Hello, Max!");
```
没有 namespace、class、Main() 方法

所有代码直接写在文件顶层

编译器会自动生成 Main() 方法并包装这些语句

### 如何禁用顶级语句
在 .csproj 中添加
```csharp
<UseTopLevelStatements>false</UseTopLevelStatements>
```

然后你就可以恢复传统的 Main() 方法结构。