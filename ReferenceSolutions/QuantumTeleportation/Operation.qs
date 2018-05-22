// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


namespace Quantum.Teleportation {
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;

    operation Teleport(msg : Qubit, there : Qubit) : () {
        body {

            using (register = Qubit[1]) {
                let here = register[0];
            
                // Create some entanglement that we can use to send our message.
                H(here);
                CNOT(here, there);
            
                // Move our message into the entangled pair.
                CNOT(msg, here);
                H(msg);

                // Measure out the entanglement.
                if (M(msg) == One)  { Z(there); }
                if (M(here) == One) { X(there); }

                // Reset our "here" qubit before releasing it.
                Reset(here);
            }

        }
    }

    operation TeleportArbitraryState (u : (Qubit => () : Adjoint)) : () {
		body {
			using (register = Qubit[2]) {
				let msg   = register [0];
				let there = register [1];

				// apply the unitary to prepare state on the message qubit
				u (msg);

				Teleport (msg, there);

				// apply the inverse of the unitary on the target qubit
				(Adjoint u)(there);

				ApplyToEach(Reset, register);
			}
		}
	}

}