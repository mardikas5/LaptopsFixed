﻿using System.Collections;
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
    public Material outlineMat;
    public TriggerArea dragged = null;

    // Use this for initialization
    void Start()
    {
        TriggerAreas = FindObjectsOfType<TriggerArea>().ToList();
        foreach( TriggerArea p in TriggerAreas )
        {
            p.parent = this;
        }
        affected = FindObjectOfType<ControllerGrabable>();
        controller1.gameObject.AddComponent<SetVelocity>();
        controller2.gameObject.AddComponent<SetVelocity>();
        //p = FindObjectOfType<BoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        dragged = null;
        if( affected != null )
        {
            foreach( TriggerArea k in TriggerAreas )
            {
                if( k.velocity != null )
                {
                    dragged = k;
                    affected.GetComponent<Rigidbody>().isKinematic = false;
                    affected.GetComponent<Rigidbody>().angularVelocity = new Vector3( 0, -k.velocity.VelocityUnscaled.x * 55f, 0 );
#if velocity
                    affected.transform.TransformDirection(-k.velocity.Velocity);
                    if (affected.GetComponent<Rigidbody>().velocity.magnitude < .5f)
                    {
                        affected.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    }
#endif
                }
            }
        }
        Colors();
    }

    void Colors()
    {
        if( outlineMat != null )
        {
            if( dragged == null )
            {
                outlineMat.SetColor( "_Color", Color.white );
            }
            else
            {
                 outlineMat.SetColor( "_Color", Color.green);
            }
        }
    }

}
