#pragma warning disable 1591
using System;
using Microsoft.Quantum.Primitive;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.MetaData.Attributes;

[assembly: OperationDeclaration("HelloWorld", "Greet (who : String) : String", new string[] { }, "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\HelloWorld\\Operation.qs", 152L, 7L, 5L)]
#line hidden
namespace HelloWorld
{
    public class Greet : Operation<String, String>
    {
        public Greet(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { };
        }

        public override Type[] Dependencies
        {
            get;
        }

        public override Func<String, String> Body
        {
            get => (who) =>
            {
#line hidden
                this.Factory.StartOperation("HelloWorld.Greet", OperationFunctor.Body, who);
                var __result__ = default(String);
                try
                {
#line hidden
                    __result__ = $"Hello, {who}!";
#line 10 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\HelloWorld\\Operation.qs"
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("HelloWorld.Greet", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<String> Run(IOperationFactory __m__, String who)
        {
            return __m__.Run<Greet, String, String>(who);
        }
    }
}