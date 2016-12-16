using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour {


    Vector3 lastPos = Vector3.zero;
    Vector3 CurrentPos = Vector3.zero;
    public Vector3 Velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CurrentPos = transform.position;
        Velocity = (CurrentPos - lastPos) * (1f/ Time.deltaTime) * 200f;
        lastPos = CurrentPos;
	}
}
