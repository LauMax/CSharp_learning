

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