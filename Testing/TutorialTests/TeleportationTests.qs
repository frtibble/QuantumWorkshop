namespace TutorialTests
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Primitive;
	open Tutorial;

	operation AssertAllZero(qs : Qubit[]) : (){
		body {
			for (i in 0 .. Length(qs) - 1){
				AssertZero(qs[i]);
			}
		}
	}

    // Test our Bell state preparation works - measurements should be correlated
	operation BellMeasurements(nrTests : Int) : Int {
		body {
			
			mutable success = 0;
			using (qs = Qubit[2]) {

				for (rep in 1 .. nrTests){

					PrepareBellState(qs[0], qs[1]);

                    // Measure the two qubits after entanglement
					let (m1, m2) = (M(qs[0]), M(qs[1]));

                    // Measurements should correlate
					if (m1 == m2) {
						set success = success + 1; 
					}
					ResetAll(qs);
				}
			}
			return success;
		}
	}

    // Teleport qubit prepared using the operation passed as a parameter (msgPrep)
    operation TeleportAndReset (msgPrep : (Qubit => () : Adjoint)) : () {
		body {

			using (qs = Qubit[3]) {

				let source = qs[0];
				let target = qs[1];
				let msg = qs[2];

                // Apply specified operation to msg qubit (initially in state |0>)
				msgPrep(msg);

				// Teleport state
				Teleport(source, target, msg);

                // Undo the transformation on the target qubit by applying the adjoint operator
				// This should return the state of the target qubit to |0>
				(Adjoint msgPrep) (target);

                // Check this yields the Zero state
				AssertAllZero(qs);
			}
		}	
	}

	operation TeleportInversion(msgPrep : (Qubit => () : Adjoint)) : () {
		body {

			using (qs = Qubit[3]) {

				let source = qs[0];
				let target = qs[1];
				let msg = qs[2];

				msgPrep(msg);

                // Teleport state
				Teleport(source, target, msg);

                // Teleport it back
				Teleport(source, msg, target);

				(Adjoint msgPrep) (msg);

				AssertAllZero(qs);
			}
		}	
	}

    // Test with a number of different qubit transformations applied
	operation TeleportTests() : () {
		body {

			TeleportAndReset(Rx(0.,_));
			TeleportInversion(Rx(0.,_));

			TeleportAndReset(X);
			TeleportInversion(X);

			TeleportAndReset(H);
			TeleportInversion(H);
		}	
	}
}