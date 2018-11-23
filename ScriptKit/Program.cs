using System;
using System.Linq;

namespace ScriptKit
{
    class Program
    {
        static void Main(string[] args)
        {
            JsRuntime runtime = JsRuntime.CreateRuntime();
            var context= runtime.CreateContext();
            runtime.CurrentContext = context;
            context.AddRef();
          
            runtime.CurrentContext.Global["print"] = new JsFunction((calle,self, arguments) =>
            {
                Console.WriteLine("ScriptKit:{0}:{1}",self,string.Join(string.Empty, arguments));
                return null;
            });
            runtime.CurrentContext.Global["Worker"] = new JsFunction((calle, self, arguments) =>
            {

                return self;
            });
            runtime.CurrentContext.Run("this.Test=function(...a){print(this); print(...a);}");
            var test = runtime.CurrentContext.Global["Test"];
            (test as JsFunction).Call(new JsNumber(123), new JsNumber(23456), new JsNumber(123456));
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
