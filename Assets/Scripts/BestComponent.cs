using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializePrivateVariables]
public class BestComponent : MonoBehaviour, IGazeInteraction
{
    public Transform visuals;
    public GameObject onDestorySpawn;
    //[SerializeField]
    float inGazeTimer = 0f;

    //[SerializeField]
    float inGazeLimit = 5f;

    //[SerializeField]
    LaptopComponent parent;

    public void OnGaze()
    {
        inGazeTimer += Time.deltaTime;
        GazeProgress.Instance.SetProgress( inGazeTimer / inGazeLimit );
        if( inGazeTimer > inGazeLimit )
        {
            if( parent != null )
            {
                InteractionManager.Instance.SetInteractionTaught();
                InteractionManager.Instance.RemoveBestComponent( parent );
                parent = null;
            }
        }
    }

    public void OnLeaveGaze()
    {
        GazeProgress.Instance.SetProgress( 0f );
        inGazeTimer = 0f;
    }

    // Use this for initialization
    public void Initialize( LaptopComponent p )
    {
        parent = p;
    }

    void OnDestroy()
    {
        if( onDestorySpawn != null )
        {
            GameObject onDestroy = Instantiate( onDestorySpawn, transform.position, Quaternion.identity, null );
            if( visuals != null )
            {
                onDestroy.transform.position = visuals.transform.position;
            }
        }
    }
}
