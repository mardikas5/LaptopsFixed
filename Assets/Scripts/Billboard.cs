using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    bool useLookAt = false;
    public bool isEnabled;
    public bool OnlyYrot;
    public bool StopMovingCloseUp;
    public Vector3 rotOffset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( !isEnabled )
        {
            return;
        }

        float distance = Vector3.Distance( transform.position.xz(), Camera.main.transform.position.xz() );
        if( !( StopMovingCloseUp && distance < .5f ) )
        {
            if( useLookAt )
            {
                Vector3 previous = transform.eulerAngles;

                transform.LookAt( Camera.main.transform );
                transform.eulerAngles += rotOffset;

                if( OnlyYrot )
                {
                    previous.y = transform.eulerAngles.y;
                    transform.eulerAngles = previous;
                }
            }
            else
            {
                transform.rotation = Quaternion.LookRotation( transform.position - Camera.main.transform.position );
            }
        }
    }
}
