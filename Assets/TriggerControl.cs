using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerControl : MonoBehaviour
{

    public List<TriggerArea> TriggerAreas = new List<TriggerArea>();
    [SerializeField]
    ControllerGrabable affected;
    public Transform controller1;
    public Transform controller2;
    // Use this for initialization
    void Start()
    {
        TriggerAreas = FindObjectsOfType<TriggerArea>().ToList();
        affected = FindObjectOfType<ControllerGrabable>();
        controller1.gameObject.AddComponent<SetVelocity>();
        controller2.gameObject.AddComponent<SetVelocity>();
        //p = FindObjectOfType<BoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (affected != null)
        {
            foreach (TriggerArea k in TriggerAreas)
            {
                if (k.velocity != null)
                {
                    Debug.Log("Dragging turntable");
                    affected.GetComponent<Rigidbody>().velocity = affected.transform.TransformDirection(-k.velocity.Velocity);
                }
            }
        }
    }

}
