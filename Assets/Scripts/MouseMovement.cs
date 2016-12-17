using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        var z = Input.GetAxis("Mouse ScrollWheel") * 10;
        var change = new Vector3(x, y, z);
        this.transform.Translate(change * 0.1f);
    }
}
