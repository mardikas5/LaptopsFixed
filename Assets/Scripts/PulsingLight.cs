using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour
{
    public float minRange;
    public float maxRange;
    // Use this for initialization
    void Start()
    {
        StartCoroutine( startPulsing( minRange, maxRange, .005f ) );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator startPulsing( float minRange, float maxRange, float sizePerFixedFrame )
    {
        yield return null;
        Vector3 size = transform.localScale;
        float current = transform.localScale.x;
        while( current > minRange )
        {
            yield return new WaitForFixedUpdate();
            for( int i = 0; i < 3; i++ )
            {
                size[i] = current;
            }
            current -= sizePerFixedFrame;
            transform.localScale = size;
        }
        while( current < maxRange )
        {
            yield return new WaitForFixedUpdate();
            for( int i = 0; i < 3; i++ )
            {
                size[i] = current;
            }
            current += sizePerFixedFrame;
            transform.localScale = size;
        }
        yield return StartCoroutine( startPulsing( minRange, maxRange, sizePerFixedFrame ) );
    }
}
