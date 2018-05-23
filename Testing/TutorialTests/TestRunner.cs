using Microsoft.Quantum.Simulation.XUnit;
using Microsoft.Quantum.Simulation.Simulators;
using Xunit.Abstractions;
using Microsoft.Quantum.Simulation.Simulators.QCTraceSimulators;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace TutorialTests
{
    // Tests using the circuit simulator
    public partial class CircuitSimulator
    {
        private readonly ITestOutputHelper output;

        public CircuitSimulator(ITestOutputHelper output)
        { this.output = output; }

        public void RunTest(Action<QuantumSimulator> test)
        {
            using (var sim = new QuantumSimulator())
            {
                sim.OnLog += (msg) => { output.WriteLine(msg); };
                sim.OnLog += (msg) => { Debug.WriteLine(msg); };
                test(sim);
            }
        }
    }

    // Tests using the trace simulator
    public partial class TraceSimulator
    {
        private readonly ITestOutputHelper output;

        public TraceSimulator(ITestOutputHelper output)
        { this.output = output; }

        public void RunTest(Action<QCTraceSimulator> test)
        {
            // Set gate times (AKA depth) to match expected physical implementation. Defaults to T = 1.0, all else 0.0
            // More info: https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-depth-counter?view=qsharp-preview
            var gateTimes = new Dictionary<PrimitiveOperationsGroups, double>
            {
                [PrimitiveOperationsGroups.Measure] = 2.0,
                [PrimitiveOperationsGroups.T] = 1.0,
                [PrimitiveOperationsGroups.R] = 0.0,
                [PrimitiveOperationsGroups.QubitClifford] = 0.0,
                [PrimitiveOperationsGroups.CNOT] = 0.0,
            };

            // Set config for trace simulator 
            // More info: https://docs.microsoft.com/en-us/quantum/quantum-computer-trace-simulator-1?view=qsharp-preview
            var config = new QCTraceSimulatorConfiguration
            {
                usePrimitiveOperationsCounter = true, // Counts the number of primitive executions used by every operation invoked in a quantum program
                useWidthCounter = true, // Counts the number of qubits allocated and borrowed by each operation
                useDepthCounter = true, // Gathers counts of the depth of every operation in the program (depth represents the number of time steps required to complete the program)
                gateTimes = gateTimes, // Set custom gate times for depth counter
                useDistinctInputsChecker = true, // Prevents bugs e.g. using the same qubit as control and target in a CNOT gate 
                useInvalidatedQubitsUseChecker = true, // Detects e.g. usage of the qubits of a mutable Qubit[] after the qubits have been released 
                throwOnUnconstraintMeasurement = true // Will fail if there is at least one measurement not annotated using AssertProb or ForceMeasure
            };

            var sim = new QCTraceSimulator(config);

            sim.OnLog += (msg) => { output.WriteLine(msg); };
            sim.OnLog += (msg) => { Debug.WriteLine(msg); };
            test(sim);
        }
    }
}
