

## 1. A builder is a separate component for building an object

✅ 中文翻译：
Builder 是一个用于构建对象的独立组件。

📘 解释：
Builder 模式的核心思想是将对象的构建过程从对象本身中分离出来。

这样可以避免构造函数过于复杂，尤其是当对象有很多可选参数或构造步骤时。

例如，在构建一个 Person 对象时，我们不直接在 Person 类中写构造逻辑，而是通过 PersonBuilder 来完成

## 2. Can either give builder a constrctor or return it via a static function.

✅ 中文翻译：
可以通过构造函数创建 Builder，也可以通过静态方法返回 Builder 实例。

📘 解释：
有两种常见方式来获取 Builder 实例：

构造函数方式：var builder = new PersonBuilder();

静态方法方式：var builder = PersonBuilder.Create();（例如用于隐藏构造细节或提供默认配置）

静态方法可以增强语义清晰度，也可以用于工厂模式的结合。

## 3. To make builder fluent, return this

✅ 中文翻译：
为了实现链式调用（流式接口），需要返回 this。

📘 解释：
Builder 模式常常使用链式调用（Fluent Interface）来提高可读性：

```csharp
var person = new PersonBuilder()
    .Called("Max")
    .WorksAs("Engineer")
    .Build();
```
每个方法返回 this（或 CRTP 中的 TSelf），使得调用可以连续进行。

这不仅语义清晰，还能模拟 DSL（领域特定语言）风格。

## 4. Different facets of an object can be built with different builders working in tandem via a base class

✅ 中文翻译：
对象的不同方面可以通过多个 Builder 协同构建，并通过一个基类协调。

📘 解释：
当对象较复杂时，可以将构建逻辑拆分为多个“子 Builder”：

例如：PersonAddressBuilder、PersonJobBuilder、PersonContactBuilder

这些子 Builder 可以继承一个共同的基类（如 FunctionalBuilder<TSubject, TSelf>），共享构造链。

最终通过统一的 Build() 方法聚合所有构造步骤。