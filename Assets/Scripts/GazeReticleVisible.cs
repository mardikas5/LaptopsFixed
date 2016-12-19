using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeReticleVisible : MonoBehaviour {

    private int count;
    public GameObject Reticle;
    bool show = false;
    Vector3 startpos;
	// Use this for initialization
	void Start () {
       // startpos = Reticle.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        
        Reticle.SetActive(show);
        show = false;
        if (!show)
        {
           // Reticle.transform.localPosition = startpos;
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        count++;
        Debug.Log("Gazable object entered range");
        
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 otherpos = Reticle.transform.position;
        otherpos.z = other.transform.position.z;
       // Reticle.transform.position = otherpos;
        show = true;
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
        Debug.Log("Gazable object left range");
    }
}
