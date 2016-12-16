using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopComponent : MonoBehaviour
{
    [Multiline]
    public string Text;
    public LaptopComponentType Type;
    private Coroutine resizeRoutine;
    private Vector3 intitialPosition;
    private Vector3 shownPosition;
    private float time = 1f;

    public Vector3 LocalRot;

    public void Start()
    {
        intitialPosition = this.transform.localPosition;
        LocalRot = transform.localEulerAngles;
        shownPosition = intitialPosition + new Vector3(0,1f,0);
    }

    public void Show()
    {
        Debug.Log( "Show: " + Type );
        if( resizeRoutine != null )
        {
            StopCoroutine( resizeRoutine );
        }

        resizeRoutine = StartCoroutine( moveYAxis( shownPosition, time, Vector3.zero ));
    }

    public void Hide()
    {
        Debug.Log( "Hide: " + Type );
        if( resizeRoutine != null )
        {
            StopCoroutine( resizeRoutine );
        }

        resizeRoutine = StartCoroutine( moveYAxis( intitialPosition, time, Vector3.zero ) );
    }

    private IEnumerator moveYAxis( Vector3 targetPos, float time, Vector3 rot)
    {
        Vector3 startPos = this.transform.localPosition;
        Vector3 endPos = targetPos;
        //endPos = targetPos;
        float currentTime = 0.0f;

        do
        {
            this.transform.localPosition = Vector3.Lerp( startPos, endPos, currentTime / time );
            currentTime += Time.deltaTime;

            
            //this.transform.localEulerAngles = Vector3.Lerp( LocalRot,  -( targetPos - Camera.current.transform.position ), currentTime / time);
            //transform.localEulerAngles = new Vector3( -90, transform.eulerAngles.y, transform.eulerAngles.z );

            yield return null;
        } while( currentTime <= time );

       

        //OnDone();

        this.transform.localPosition = endPos;
    }
}
