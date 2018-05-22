using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace HelloWorld
{
    class Driver
    {
        static void Main(string[] args)
        {
            using (var sim = new QuantumSimulator())
            {
                var result = Greet.Run(sim, "Imperial").Result;
                System.Console.WriteLine(result);
            }

        }
    }
}