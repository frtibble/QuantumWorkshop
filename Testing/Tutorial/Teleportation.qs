namespace Tutorial
{
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;

	// Check a qubit returns state |0> with the given probability when measured
    operation AssertZeroProb (q : Qubit, prob : Double) : () {
		body {
            // Arguments: Measurement basis, qubit, expected probability, error message, error tolerance
			AssertProb ([PauliZ], [q], Zero, prob, $"Measurement should result in a collapse to |0> with probability {prob}", 1e-5);		
		}
	}

	// Check a qubit returns state |0> when measured
    operation AssertZero (q: Qubit) : () {
        body {
            Assert ([PauliZ], [q], Zero, "Expecting the qubit to be in the |0> state");
        }
    }

    // Entangle two qubits, producing a Bell state
    operation PrepareBellState(q1 : Qubit, q2 : Qubit) : () {
		body {
			H(q1);
			CNOT(q1, q2);
		}
        adjoint auto;
		controlled auto;
		controlled adjoint auto;
	}

    operation Teleport(source : Qubit, target : Qubit, msg : Qubit) : () {
        body {
            // Ensure target qubit state is |0> as expected
            AssertZero(target);
			AssertZero(source);

            // Create some entanglement that we can use to send our message
            PrepareBellState(source, target);
        
			// Move our message into the entangled pair
			(Adjoint PrepareBellState)(source, msg);

            // This is equivalent to the following code:
            //CNOT(source, msg);
            //H(source);

            // Check our states are as expected, 50% probability of measuring to find |0> and 50% to find |1>
            AssertZeroProb(source, 0.5);
            AssertZeroProb(msg, 0.5);

            // Measure out the entanglement
			// MResetZ measures a single qubit in the Z basis, and resets it to the standard basis state |0> following the measurement
            if (MResetZ(source)  == One) { Z(target); }
            if (MResetZ(msg) == One) { X(target); }
			// This is equivalent to the following code:
            //if (M(source)  == One) { Z(target); }
            //if (M(msg) == One) { X(target); }
			//Reset(source);
			//Reset(msg);
        }
    }

	operation TeleportPlusState () : Result {
        body {
            mutable result = Zero;

            // We will teleport the |+> state = (|0> + |1>)/SQRT(2)
            using (register = Qubit[3]) {

                let source = register[0];
                let target = register[1];
				let msg = register[2];

				AssertZero(target);

                // Generate the |+> state by applying a Hadamard gate to our source qubit (initialised as |0> by default)
                H(msg);

                // Perform the Teleport operation
                Teleport(source, target, msg);

                // Measure the target qubit - if teleportation has been successful this should give |0> with 50% probability and |1> with 50% probability
                set result = M(target);

                // Reset qubits before releasing them
                ResetAll(register);
            }

            return result;
        }
    }
}
