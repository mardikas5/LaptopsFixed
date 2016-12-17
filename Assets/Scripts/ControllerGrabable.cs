using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabable : MonoBehaviour
{
    public void OnTriggerExit( Collider other )
    {
        if( Controllers.Contains( other.transform ) )
        {
            Debug.Log( "exit" );
        }
    }

    public void OnTriggerStay( Collider other )
    {
        if( Controllers.Contains( other.transform ) )
        {
            Debug.Log( "stay" );
        }
    }

    public List<Transform> Controllers = new List<Transform>();

    bool isGrabbed;
    Transform Grabbing;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach( Transform c in Controllers )
        {
            ControllerInteraction( c );
        }
    }

    void ControllerInteraction( Transform t )
    {
        
    }
}
