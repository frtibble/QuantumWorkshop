using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System;
using System.Linq;

namespace Tutorial
{
    class Driver
    {
        static void Main(string[] args)
        {
            using (var sim = new QuantumSimulator())
            {
                var numZeroes = 0;
                var count = 100;

                // Run many times to ensure our target qubit is in the |+> state after teleporting, as desired
                foreach (var run in Enumerable.Range(0, count))
                {
                    var res = TeleportPlusState.Run(sim).Result;

                    if (res == Result.Zero)
                    {
                        numZeroes = numZeroes + 1;
                    }
                }

                Console.WriteLine($"Ran {count} times\nNumber of Zeroes measured: {numZeroes}\nNumber of Ones measured: {count - numZeroes}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}