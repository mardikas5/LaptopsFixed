using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public SetVelocity velocity;
    public GameObject controller1;
    public GameObject controller2;
    [SerializeField]
    public bool controller1InTrigger { get; private set; }
    [SerializeField]
    public bool controller2InTrigger { get; private set; }


    void Start()
    {
        controller1.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        controller2.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        controller1.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
        controller2.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
    }

    private void OnControllerTriggerPress( Transform controller )
    {
        if( controller1InTrigger )
        {
            velocity = controller1.GetComponent<SetVelocity>();
        }

        if( controller2InTrigger )
        {
            velocity = controller2.GetComponent<SetVelocity>();
        }
    }

    private void OnControllerTriggerUnPress( Transform controller )
    {
        velocity = null;
    }

    public void OnTriggerExit( Collider other )
    {
        //other.transform.root.GetComponentInChildren<SetVelocity>();

        if( other != null )
        {
            if( other.gameObject == controller1 )
            {
                Debug.Log( "Trigger Exit" );
                controller1InTrigger = false;
            }
            else if( other.gameObject == controller2 )
            {
                Debug.Log( "Trigger Exit" );
                controller2InTrigger = false;
            }
        }
    }

    public void OnTriggerEnter( Collider other )
    {
        //if( other != null )
        //{
            if( other.gameObject == controller1 )
            {
                Debug.Log( "Trigger Enter" );
                controller1InTrigger = true;
            }
            else if( other.gameObject == controller2 )
            {
                Debug.Log( "Trigger Enter" );
                controller2InTrigger = true;
            }
       // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
