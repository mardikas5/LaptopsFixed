using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public SetVelocity velocity;
    public GameObject controller1;
    public GameObject controller2;
    bool controller1InTrigger;
    bool controller2InTrigger;


    void Start()
    {
        controller1.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        controller2.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        controller1.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
        controller2.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
    }

    private void OnControllerTriggerPress(Transform controller)
    {
        if (controller1InTrigger)
        {
            velocity = controller1.GetComponent<SetVelocity>();
        }

        if (controller2InTrigger)
        {
            velocity = controller2.GetComponent<SetVelocity>();
        }
    }

    private void OnControllerTriggerUnPress(Transform controller)
    {
        velocity = null;
    }

    public void OnTriggerExit( Collider other )
    {
        var parent = other.gameObject.transform.parent;
        if (parent != null)
        {
            if (parent.gameObject == controller1)
            {
                Debug.Log("Trigger Exit");
                controller1InTrigger = false;
            }
            else if (parent.gameObject == controller2)
            {
                Debug.Log("Trigger Exit");
                controller2InTrigger = false;
            }
        }
    }

    public void OnTriggerEnter( Collider other )
    {
        var parent = other.gameObject.transform.parent;
        if (parent != null)
        {
            if (parent.gameObject == controller1)
            {
                Debug.Log("Trigger Enter");
                controller1InTrigger = true;
            }
            else if (parent.gameObject == controller2)
            {
                Debug.Log("Trigger Enter");
                controller2InTrigger = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
