using Microsoft.Quantum.Primitive;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System.Linq;

namespace Quantum.Teleportation {
    class Program
    {
        static void Main(string[] args)
        {
            using (var sim = new QuantumSimulator())
            {
                System.Console.WriteLine("BEGIN Running Teleportation - Arbitrary Unitary");
                TeleportArbitraryState.Run(sim, sim.Get<H, H> ()).Wait();
                System.Console.WriteLine("END Running Teleportation - Arbitrary Unitary\n\n");
            }

        }
    }
}