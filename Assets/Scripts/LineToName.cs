using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( LineRenderer ), typeof( Laptop ) )]
public class LineToName : MonoBehaviour
{
    public Canvas textCanvas;
    bool enabled = true;
    // Use this for initialization
    void Start()
    {
        IGrabbableExtentions.currentGrabbedChanged += UpdateLaptopName;
    }

    void UpdateLaptopName( IGrabbable Laptop )
    {
        if( GetComponent<Laptop>().gameObject == Laptop.gameObject )
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( enabled )
        {
            DrawLine();
        }
        else
        {
            GetComponent<LineRenderer>().numPositions = 0;
        }
    }

    void DrawLine()
    {
        Debug.Log( textCanvas.GetComponent<RectTransform>().rect.size );
        RectTransform t = textCanvas.GetComponent<RectTransform>();
        Vector3 endPoint = textCanvas.transform.position - new Vector3( 0, (t.rect.size.y * t.lossyScale.y ) / 2f, 0 );
        GetComponent<LineRenderer>().numPositions = 2;
        GetComponent<LineRenderer>().SetPosition( 0, transform.position );
        GetComponent<LineRenderer>().SetPosition( 1, endPoint );
    }
}
