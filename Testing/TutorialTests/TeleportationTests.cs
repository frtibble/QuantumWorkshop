using Xunit;
using Microsoft.Quantum.Simulation.Simulators.QCTraceSimulators;
using Microsoft.Quantum.Simulation.QCTraceSimulatorRuntime;
using Tutorial;

namespace TutorialTests
{
    // Run tests using the circuit simulator
    public partial class CircuitSimulator
    {
        [Fact(DisplayName = "Tutorial.Bell")]
        public void BellTest()
        {
            RunTest(sim =>
            {
                var nrTests = 1000;
                var res = BellMeasurements.Run(sim, nrTests).Result;
                if (res == nrTests) output.WriteLine($"All Bell tests passed.");
                else output.WriteLine($"{nrTests - res} out of {nrTests} Bell tests failed.");
                Xunit.Assert.Equal(res, nrTests);
            });
        }

        [Fact(DisplayName = "Tutorial.Teleport")]
        public void TeleportTest()
        {
            RunTest(sim =>
            {
                TeleportTests.Run(sim).Wait();
                output.WriteLine($"All teleport tests passed.");
            });
        }
    }

    // Run tests using the trace simulator
    public partial class TraceSimulator
    {
        [Fact(DisplayName = "Tutorial.Teleport.GateCount")]
        public void TeleportGateCountTest()
        {
            RunTest(sim =>
            {
                TeleportTests.Run(sim).Wait();

                var totNrMs = sim.GetMetric<TeleportTests>(PrimitiveOperationsGroupsNames.Measure);
                var totNrRs = sim.GetMetric<TeleportTests>(PrimitiveOperationsGroupsNames.R);
                var totNrTs = sim.GetMetric<TeleportTests>(PrimitiveOperationsGroupsNames.T);
                var totNrCliffords = sim.GetMetric<TeleportTests>(PrimitiveOperationsGroupsNames.QubitClifford);
                var totNrCNOTs = sim.GetMetric<TeleportTests>(PrimitiveOperationsGroupsNames.CNOT);

                var MsPerTest = sim.GetMetric<TeleportAndReset, TeleportTests>(PrimitiveOperationsGroupsNames.Measure);
                var TsPerTest = sim.GetMetric<TeleportAndReset, TeleportTests>(PrimitiveOperationsGroupsNames.T);
                var CNOTsPerTest = sim.GetMetric<TeleportAndReset, TeleportTests>(PrimitiveOperationsGroupsNames.CNOT);

                var summary = sim.ToCSV()[MetricsCountersNames.primitiveOperationsCounter];

                output.WriteLine("\nGate counts for all tests:");
                output.WriteLine($"Total number of measurements for all tests: {totNrMs}");
                output.WriteLine($"Total number of rotation gates for all tests: {totNrRs}");
                output.WriteLine($"Total number of T gates for all tests: {totNrTs}");
                output.WriteLine($"Total number of Clifford gates for all tests: {totNrCliffords}");
                output.WriteLine($"Total number of CNOT gates for all tests: {totNrCNOTs}");

                output.WriteLine("\nGate counts for a single test:");
                output.WriteLine($"Number of measurements within a single test: {MsPerTest}");
                output.WriteLine($"Number of T gates within a single test: {TsPerTest}");
                output.WriteLine($"Number of CNOT gates within a single test: {CNOTsPerTest}");

                output.WriteLine("\nSummary in CSV format:");
                output.WriteLine(summary);
            });
        }

        [Fact(DisplayName = "Tutorial.Teleport.QubitUsage")]
        public void TeleportQubitAllocationTest()
        {
            RunTest(sim =>
            {
                var qs = new QCTraceSimulator.TracerAllocate(sim).Apply(3);
                Teleport.Run(sim, qs[0], qs[1], qs[2]).Wait();

                var received = sim.GetMetric<Teleport>(WidthCounter.Metrics.InputWidth);
                var returned = sim.GetMetric<Teleport>(WidthCounter.Metrics.ReturnWidth);
                var allocated = sim.GetMetric<Teleport>(WidthCounter.Metrics.ExtraWidth);
                var borrowed = sim.GetMetric<Teleport>(WidthCounter.Metrics.BorrowedWith);

                var summary = sim.ToCSV()[MetricsCountersNames.widthCounter];

                output.WriteLine("\nQubit usage for Teleport:");
                output.WriteLine($"Number of received qubits: {received}");
                output.WriteLine($"Number of returned qubits: {returned}");
                output.WriteLine($"Number of allocated qubits: {allocated}");
                output.WriteLine($"Number of borrowed qubits: {borrowed}");

                output.WriteLine("\nSummary in CSV format:");
                output.WriteLine(summary);
            });
        }
    }
}
