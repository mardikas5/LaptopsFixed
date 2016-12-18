using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeManager : MonoBehaviour
{

    public IGazeInteraction inGaze;

    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray( Camera.main.transform.position, Camera.main.transform.forward );

        RaycastHit rcH;
        //Debug.DrawRay( r.origin, r.direction * 5f, Color.red, .1f );
        if( Physics.Raycast( r, out rcH, 20f, 1 << LayerMask.NameToLayer( "GazeRay" ) ) )
        {
            if( inGaze != null )
            {
                if( inGaze != rcH.transform.GetComponentInChildren<IGazeInteraction>() )
                {
                    inGaze.OnLeaveGaze();
                }
            }
            inGaze = rcH.transform.GetComponentInChildren<IGazeInteraction>();
            inGaze.OnGaze();
        }
        else if( inGaze != null )
        {
            inGaze.OnLeaveGaze();
            inGaze = null;
        }
    }
}
