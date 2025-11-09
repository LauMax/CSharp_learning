# launch.json 文件

## 字段详解与类比


| 字段名            | 含义 | 类比说明 |
|------------------|------|-----------|
| `name`           | 配置名称，显示在调试菜单中 | 就像你给调试按钮贴了个标签：“启动控制台”或“运行对象追踪” |
| `type`           | 调试器类型，这里是 `coreclr` 表示 .NET Core | 告诉 VS Code：“我要调试的是 .NET Core 程序” |
| `request`        | 调试请求类型：`launch` 表示启动程序；`attach` 表示附加到已有进程 | “我要亲自启动这个程序，而不是中途插队进去” |
| `preLaunchTask`  | 启动前要执行的任务，比如编译项目 | “出发前先打包行李”——这里是 `build` |
| `program`        | 要调试的程序路径（DLL 文件） | “这是我要调试的主角”——路径指向编译后的可执行 DLL |
| `args`           | 启动程序时传入的命令行参数 | “给主角一些台词”——目前是空的，表示不传参数 |
| `cwd`            | 当前工作目录（Current Working Directory） | “从哪个房间开始演出”——通常是项目根目录 |
| `console`        | 使用哪种控制台显示输出：`internalConsole` 表示 VS Code 内部控制台 | “观众坐在哪看演出”——这里是 VS Code 自带的控制台 |
| `stopAtEntry`    | 是否在程序入口处暂停 | `false` 表示直接开演，不在入口打断点 |

```json
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (console)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/Factories/Sample/bin/Debug/net9.0/Sample.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole",
            "stopAtEntry": false
        },
        {
            "name": "run object tracking",
            "type": "coreclr",
            "request": "launch",
            "program": "${workspaceFolder}/Factories/ObjectTrackingAndBatchReplacement/bin/Debug/net9.0/ObjectTrackingAndBatchReplacement.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole",
            "stopAtEntry": false
        }
    ]
}
```

### console
| 值                 | 说明 |
|--------------------|------|
| `"internalConsole"` | 使用 VS Code 内置的调试控制台（默认） |
| `"integratedTerminal"` | 使用 VS Code 的集成终端窗口 |
| `"externalTerminal"` | 启动一个系统级的外部终端窗口（如 cmd、PowerShell、Terminal） |

✅ **使用建议**  
- 使用 `internalConsole` 时适合快速查看输出，但不能进行交互输入。  
- 使用 `integratedTerminal` 时适合需要用户输入的程序（如 `Console.ReadLine()`）。  
- 使用 `externalTerminal` 时适合模拟真实终端环境或调试 GUI 程序。


### stopAtEntry
### ⏸️ `stopAtEntry` 的可选值

| 值     | 说明                                       |
|--------|--------------------------------------------|
| `true` | 程序启动后在入口点（通常是 Main 方法）暂停 |
| `false`| 程序直接运行，不在入口处打断点             |

✅ **使用建议**  
- 设置为 `true` 可以让你在程序刚启动时就开始调试。  
- 设置为 `false` 更适合快速运行，除非你手动设置断点。

