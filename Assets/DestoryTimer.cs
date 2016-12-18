using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimer : MonoBehaviour
{

    public float DestroyAfter = 1f;
    // Use this for initialization
    void Start()
    {
        Invoke( "DestroyObj", DestroyAfter );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyObj()
    {
        Destroy( gameObject );
    }
}
