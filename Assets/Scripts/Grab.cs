using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    List<GameObject> objectsInRange = new List<GameObject>();
    List<GameObject> parented = new List<GameObject>();

    public List<Transform> Affected = new List<Transform>();
    public List<Material> RestoreTo = new List<Material>();

    public List<Transform> Visuals = new List<Transform>();

    public bool doMaterialChange = true;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        this.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown( KeyCode.R ) )
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            RaycastHit hit;
            Debug.DrawRay( ray.origin, ray.direction, Color.red, 10 );
            if( Physics.Raycast( ray, out hit ) )
            {
                this.transform.position = hit.point;
            }
        }
    }

    private void OnControllerTriggerPress( Transform controller )
    {
        foreach( GameObject other in objectsInRange )
        {
            if( other.GetComponent<Rigidbody>() )
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.parent = this.transform;
                parented.Add( other );
            }
        }
    }

    private void OnControllerTriggerUnPress( Transform controller )
    {
        foreach( GameObject other in objectsInRange )
        {
            if( other.GetComponent<Rigidbody>() )
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Rigidbody>().velocity = this.GetComponent<SetVelocity>().VelocityUnscaled;
                other.transform.parent = null;
            }
        }

        foreach( GameObject p in parented )
        {
            p.transform.parent = null;
        }

        parented.Clear();
        objectsInRange.Clear();
    }

    public void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == ( "Grabable" ) || other.gameObject.tag == ( "Laptop" ) )
        {
            //Debug.Log("tag c");
            foreach( MeshRenderer r in other.GetComponentsInChildren<MeshRenderer>() )
            {
                if( doMaterialChange )
                {
                    CheckObject( r );
                }
                r.gameObject.layer = LayerMask.NameToLayer( "Outline" );
            }
            other.gameObject.layer = LayerMask.NameToLayer( "Outline" );
            objectsInRange.Add( other.gameObject );

        }
    }

    public void OnTriggerExit( Collider other )
    {
        if( other.gameObject.tag == ( "Grabable" ) || other.gameObject.tag == ( "Laptop" ) )
        {
            foreach( MeshRenderer r in other.GetComponentsInChildren<MeshRenderer>() )
            {
                r.gameObject.layer = LayerMask.NameToLayer( "Default" );
                RemoveFromAffected( r.transform );
            }
            other.gameObject.layer = LayerMask.NameToLayer( "Default" );
            objectsInRange.Remove( other.gameObject );

        }
    }

    void RemoveAtIndex( int i )
    {
        if( Affected[i] != null )
        {
            Affected[i].GetComponent<MeshRenderer>().material = RestoreTo[i];
        }
        Affected.RemoveAt( i );
        RestoreTo.RemoveAt( i );
    }

    void RemoveFromAffected( Transform t )
    {
        int index = Affected.FindIndex( ( x ) => x == t );
        if( index != -1 )
        {
            RemoveAtIndex( index );
        }
    }

    void AddToAffected( Transform t )
    {
        Affected.Add( t );
        string s = t.GetComponent<MeshRenderer>().material.name;
        s = s.Trim( ")ecnatsnI(".ToCharArray() );
        Material m = new Material( t.GetComponent<MeshRenderer>().material );
        m.name = s;
        RestoreTo.Add( m );
    }

    void CheckObject( MeshRenderer hit )
    {
        if( !Affected.Contains( hit.transform ) )
        {
            if( hit.transform.GetComponent<MeshRenderer>() )
            {
                AddToAffected( hit.transform );
            }
        }

        Material m = hit.material;

        m.SetOverrideTag( "RenderType", "Transparent" );
        m.EnableKeyword( "_ALPHAPREMULTIPLY_ON" );

        m.renderQueue = 3000;

        m.EnableKeyword( "Rendering Mode" );
        m.SetFloat( "_Mode", 3 );

        m.SetFloat( "_DstBlend", 10 );
        m.SetFloat( "_ZWrite", 0 );

        m.SetColor( "_Color", new Color( .9f, .9f, .9f, .5f ) );

        m.EnableKeyword( "_EMISSION" );

        hit.material.SetColor( "_EmissionColor", new Color( 0f, 0f, 0f, 0f ) );

    }

}

