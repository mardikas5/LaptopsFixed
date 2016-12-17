using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAngularVelocity : MonoBehaviour {

public Vector3 AngularVelocity;

	// Use this for initialization
	void Start () {
		var rb = this.GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.AddTorque(AngularVelocity, ForceMode.VelocityChange);
		}
	}
}
