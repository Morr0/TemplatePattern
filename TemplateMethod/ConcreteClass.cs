using System;

namespace TemplateMethod
{
    internal class ConcreteClass : AbstractClass
    {
        protected override void DoSomething(){
            Console.WriteLine("Hello World");
            Console.WriteLine("Hello");
        }
    }
}