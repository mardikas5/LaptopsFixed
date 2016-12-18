using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeReticleVisible : MonoBehaviour {

    private int count;
    public GameObject Reticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Reticle.SetActive(count != 0);
	}

    private void OnTriggerEnter(Collider other)
    {
        count++;
        Debug.Log("Gazable object entered range");
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
        Debug.Log("Gazable object left range");
    }
}
