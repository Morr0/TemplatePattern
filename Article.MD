Let's start by saying that design patterns are not how to implement things for the better. They are a concept of how to structure for the better. What is meant by "better"? Better may mean ease of understanding of code, standard others know to use, an efficient conceptual grouping of things or something else a project demands.

The template design pattern is rather simple, it is the idea that the user of a class uses a method that is not to be called directly. This seems confusing, why would I involve my mind through this confusion? Think of two classes, one is abstract and the other implements from it (concrete class). The concrete class overrides a method a `DoSomething()` method and is supposed to call the abstract's implementation of it before implementing it's own. Ok now that is good what might be the problem. One problem is the fact you must call the base class implementation of the method which may cause bugs if not called. Another problem maybe the caller should not call this method directly, in other words, maybe the caller has to call another method first then this one. A third problem maybe what if the implemented method is doing everything for the class? A fourth problem may be you need to pass encapsulated data the public doesn't have access to. The author of the abstract class may have designed the method to only be concerned about one thing but the implementor went ahead and spiced up the method with calls to database, business services and more. This became a mess. 

It may not be clear yet. Just before demonstrating with code, this pattern is a [behavioral](https://en.wikipedia.org/wiki/Behavioral_pattern) design pattern that is concerned with how classes behave with each other (communication).

Will demonstrate a demo in [C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)) however the concept is object oriented language agnostic. Any capable OOP language will support such design.

### Creating the project:
Will use the [dotnet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) to make a new project called `TemplateMethod`.

```bash
dotnet new console --name TemplateMethod
```

Now that I've created the project, if your one directory outside you can run the command below to ensure dotnet made the program correctly along with a default "Hello World" console call.

```bash
dotnet run --project TemplateMethod
```

If you are inside the same directory as the project, you can just run:
```bash
dotnet run
```

### A scenario where one needs the template pattern:
So below there are two classes, one abstract and other implements from it. The only method there is is `DoSomething()`. Notice that it is `virtual`. This makes it overridable by child classes. I have put an implementation in the abstract class. You may not want to implement a default implementation and just make it an `abstract` method which will force all concrete classes to provide an implementation.

```c#
internal abstract class AbstractClass
{
    public virtual void DoSomething(){
        Console.WriteLine("Hello World");
    }
}
```

```c#
internal class ConcreteClass : AbstractClass
{
    public override void DoSomething(){
        base.DoSomething();
        Console.WriteLine("Hello");
    }
}
```

Here I implement the abstract class. I call `base.DoSomething()`. Ok where is the fault? Nothing. Let's say that the base class method call was not there. Now you have a bug assuming you had to call it. Let's say you had more classes implementing the same abstract class and all have that bug, how are you supposed to know about it? Syntax is fine! Let's say the classes are huge. Then you have a problem. This is as an anti-pattern or more specifcally anti-template pattern.

### Solving the problem above:
The fix is simple, just make a new method in the abstract class that I will call `ProcessDoSomething`. It shouldn't be overridable and is to be consumer facing. Now make the `DoSomething` method `protected` to prevent it from being called directly. Making the method abstract is even better but if you are advocate of default implementations then provide one. This is done below:

```c#
internal abstract class AbstractClass
{
    protected virtual void DoSomething(){
        Console.WriteLine("Hello World");
    }

    public void ProcessDoSomething(){
        DoSomething();
        // Do other things
    }
}
```

```c#
internal class ConcreteClass : AbstractClass
{
    protected override void DoSomething(){
        Console.WriteLine("Hello World");
        Console.WriteLine("Hello");
    }
}
```

Notice that we fixed the problem we had above but we have another? We still can call `base.DoSomething()` if was not abstract method. I will argue that this is not a concern because by default people don't call `base` unless they were told to. To ensure against this possibility, making the method `abstract` will fix this issue.

### What we benefited from this little overhead:
- The consumer cannot call the implementation directly, you have to call the public equivalent.
- The consumer facing `ProcessDoSomething` can call **many template methods in the order intended**.
- Leave the concern of the implementation to the implementor and leave nothing to worry about for the consumer.
- Pass encapsulated data to the template methods without the consumer worrying about it.

The scenario above can be found here:
https://github.com/Morr0/TemplatePattern

### Real life scenario where the template pattern is used:
Imagine you are a cook, you want to make a bread. A bread is made in different cycles first mix up the reciple then make the dough then cook it. Ok that can be done with 3 template methods below:
```c#
protected abstract void MixUpRecipe();
protected abstract void MakeDough();
protected abstract void Cook();
```

You then can have a single consumer facing caller:
```c#
public void MakeBread(){
    MixUpRecipe();
    MakeDough();
    Cook();
}
```

You make the cook class abstract then people can implement it with different breads.

Notice here the template pattern was not the only pattern used. The [strategy](https://en.wikipedia.org/wiki/Strategy_pattern) pattern was used because you can have a mexican bread strategy, french bread strategy or a whole lot more. You might say that is not really a strategy pattern with template pattern. In fact it is, you got a template which can be for any strategy you like. In the end the product is bread. The way to make the bread is the strategy.

## Conclusion:
Design patterns are not recipes but more of guidelines. You should not always use design patterns whenever you can, you may want your own design. The [null](https://en.wikipedia.org/wiki/Null_object_pattern) object pattern maybe useful if you want to implement default implementations and ignore some template methods.

Thanks for reading through.