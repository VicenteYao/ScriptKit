using System;
using System.Linq;
using System.Threading;

namespace ScriptKit
{
    class Program
    {
        static void Main(string[] args)
        {
            JsRuntime runtime = JsRuntime.CreateRuntime();
            var context= runtime.CreateContext();
            var context2 = runtime.CreateContext();
            runtime.CurrentContext = context;
            runtime.CurrentContext.Global["print"] = new JsFunction(HandleJsFunctionCallback);
            runtime.CurrentContext.Global["idle"] = new JsFunction((calle, self, arguments) => {
                runtime.Idle();
                return null;
            });
            runtime.CurrentContext = context2;
            runtime.CurrentContext.Global["print"] = new JsFunction(HandleJsFunctionCallback2);
            runtime.CurrentContext.Global["idle"] = new JsFunction((calle, self, arguments) => {
                runtime.Idle();
                return null;
            });
            int i = 0;
            while (true)
            {
                if (i%2==0)
                {
                    runtime.CurrentContext = context2;
                    runtime.CurrentContext.Run("print(new String('1234'));");
                }
                else
                {
                    runtime.CurrentContext = context;
                    runtime.CurrentContext.Run("print(new String('234'));");
                }

                i++;
            }

            runtime.CurrentContext = JsContext.Invalid;
            runtime.IsEnabled = false;
            runtime.Dispose();
        }



        static JsValue HandleJsFunctionCallback(JsFunction calle, JsValue self, System.Collections.ObjectModel.ReadOnlyCollection<JsValue> arguments)
        {
            Console.WriteLine("ScriptKit:{0}:{1}", DateTime.Now, string.Join(",", arguments));
            return null;
        }


        static JsValue HandleJsFunctionCallback2(JsFunction calle, JsValue self, System.Collections.ObjectModel.ReadOnlyCollection<JsValue> arguments)
        {
            Console.WriteLine("ScriptKit1:{0}:{1}", DateTime.Now, string.Join(",", arguments));
            return null;
        }



    }
}
