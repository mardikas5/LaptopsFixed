using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    bool useLookAt = false;
    public bool isEnabled;
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

        if( useLookAt )
        {
            transform.LookAt( Camera.main.transform );
            transform.eulerAngles += rotOffset;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation( transform.position - Camera.main.transform.position );
        }
    }
}
