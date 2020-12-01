using System;

namespace TemplateMethod
{
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
}