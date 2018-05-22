#pragma warning disable 1591
using System;
using Microsoft.Quantum.Primitive;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.MetaData.Attributes;

[assembly: OperationDeclaration("Quantum.Teleportation", "Teleport (msg : Qubit, there : Qubit) : ()", new string[] { }, "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs", 269L, 9L, 57L)]
[assembly: OperationDeclaration("Quantum.Teleportation", "TeleportArbitraryState (u : (Qubit => () : Adjoint)) : ()", new string[] { }, "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs", 1139L, 36L, 73L)]
#line hidden
namespace Quantum.Teleportation
{
    public class Teleport : Operation<(Qubit,Qubit), QVoid>
    {
        public Teleport(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.Allocate), typeof(Microsoft.Quantum.Primitive.CNOT), typeof(Microsoft.Quantum.Primitive.H), typeof(Microsoft.Quantum.Primitive.M), typeof(Microsoft.Quantum.Primitive.Release), typeof(Microsoft.Quantum.Primitive.Reset), typeof(Microsoft.Quantum.Primitive.X), typeof(Microsoft.Quantum.Primitive.Z) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected Allocate Allocate
        {
            get
            {
                return this.Factory.Get<Allocate, Microsoft.Quantum.Primitive.Allocate>();
            }
        }

        protected IUnitary<(Qubit,Qubit)> MicrosoftQuantumPrimitiveCNOT
        {
            get
            {
                return this.Factory.Get<IUnitary<(Qubit,Qubit)>, Microsoft.Quantum.Primitive.CNOT>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveH
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.H>();
            }
        }

        protected ICallable<Qubit, Result> M
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, Result>, Microsoft.Quantum.Primitive.M>();
            }
        }

        protected Release Release
        {
            get
            {
                return this.Factory.Get<Release, Microsoft.Quantum.Primitive.Release>();
            }
        }

        protected ICallable<Qubit, QVoid> Reset
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, QVoid>, Microsoft.Quantum.Primitive.Reset>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveX
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.X>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveZ
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.Z>();
            }
        }

        public override Func<(Qubit,Qubit), QVoid> Body
        {
            get => (_args) =>
            {
#line hidden
                this.Factory.StartOperation("Quantum.Teleportation.Teleport", OperationFunctor.Body, _args);
                var __result__ = default(QVoid);
                try
                {
                    var (msg,there) = _args;
#line 12 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    var register = Allocate.Apply(1L);
                    // Ask for an auxillary qubit that we can use to prepare
                    // for teleportation.
#line 15 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    var here = register[0L];
                    // Create some entanglement that we can use to send our message.
#line 18 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    MicrosoftQuantumPrimitiveH.Apply(here);
#line 19 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    MicrosoftQuantumPrimitiveCNOT.Apply((here, there));
                    // Move our message into the entangled pair.
#line 22 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    MicrosoftQuantumPrimitiveCNOT.Apply((msg, here));
#line 23 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    MicrosoftQuantumPrimitiveH.Apply(msg);
                    // Measure out the entanglement.
#line 26 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    if ((M.Apply<Result>(msg) == Result.One))
                    {
#line 26 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                        MicrosoftQuantumPrimitiveZ.Apply(there);
                    }

#line 27 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    if ((M.Apply<Result>(here) == Result.One))
                    {
#line 27 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                        MicrosoftQuantumPrimitiveX.Apply(there);
                    }

                    // Reset our "here" qubit before releasing it.
#line 30 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    Reset.Apply(here);
#line hidden
                    Release.Apply(register);
#line hidden
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Quantum.Teleportation.Teleport", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, Qubit msg, Qubit there)
        {
            return __m__.Run<Teleport, (Qubit,Qubit), QVoid>((msg, there));
        }
    }

    public class TeleportArbitraryState : Operation<IAdjointable, QVoid>
    {
        public TeleportArbitraryState(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.Allocate), typeof(Microsoft.Quantum.Canon.ApplyToEach<>), typeof(Microsoft.Quantum.Primitive.Release), typeof(Microsoft.Quantum.Primitive.Reset), typeof(Quantum.Teleportation.Teleport) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected Allocate Allocate
        {
            get
            {
                return this.Factory.Get<Allocate, Microsoft.Quantum.Primitive.Allocate>();
            }
        }

        protected ICallable MicrosoftQuantumCanonApplyToEach
        {
            get
            {
                return new GenericOperation(this.Factory, typeof(Microsoft.Quantum.Canon.ApplyToEach<>));
            }
        }

        protected Release Release
        {
            get
            {
                return this.Factory.Get<Release, Microsoft.Quantum.Primitive.Release>();
            }
        }

        protected ICallable<Qubit, QVoid> Reset
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, QVoid>, Microsoft.Quantum.Primitive.Reset>();
            }
        }

        protected ICallable<(Qubit,Qubit), QVoid> Teleport
        {
            get
            {
                return this.Factory.Get<ICallable<(Qubit,Qubit), QVoid>, Quantum.Teleportation.Teleport>();
            }
        }

        public override Func<IAdjointable, QVoid> Body
        {
            get => (u) =>
            {
#line hidden
                this.Factory.StartOperation("Quantum.Teleportation.TeleportArbitraryState", OperationFunctor.Body, u);
                var __result__ = default(QVoid);
                try
                {
#line 38 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    var register = Allocate.Apply(2L);
#line 39 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    var msg = register[0L];
#line 40 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    var there = register[1L];
                    // apply the unitary to prepare state on the message qubit
#line 43 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    u.Apply(msg);
#line 45 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    Teleport.Apply((msg, there));
                    // apply the inverse of the unitary on the target qubit
#line 48 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    u.Adjoint.Apply(there);
#line 50 "C:\\Users\\frtibble\\Documents\\Quantum\\Imperial\\QuantumTeleportation\\Operation.qs"
                    MicrosoftQuantumCanonApplyToEach.Apply((((ICallable)Reset), register));
#line hidden
                    Release.Apply(register);
#line hidden
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Quantum.Teleportation.TeleportArbitraryState", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, IAdjointable u)
        {
            return __m__.Run<TeleportArbitraryState, IAdjointable, QVoid>(u);
        }
    }
}