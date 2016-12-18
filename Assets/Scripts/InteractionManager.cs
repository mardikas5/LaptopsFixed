using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    public AudioClip BestComponentSound;
    public GameObject VisionAttractionObject;

    private List<GameObject> AttentionSpots = new List<GameObject>();
    private List<LaptopComponent> attachedTo = new List<LaptopComponent>();

    private bool InteractionTaught = false;
    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInteractionTaught()
    {
        InteractionTaught = true;

        foreach( GameObject p in AttentionSpots )
        {
            if( p != null )
            {
                if( p.GetComponent<AudioSource>() )
                {
                    p.GetComponent<AudioSource>().enabled = false;
                }
            }
        }
    }

    public void ShowBestComponent( LaptopComponent attachTo )
    {
        GameObject p = new GameObject();
        if( !InteractionTaught && AttentionSpots.Count == 0 )
        {
            p.AddComponent<AudioSource>().clip = BestComponentSound;
            p.GetComponent<AudioSource>().spatialBlend = 1f;
            p.GetComponent<AudioSource>().volume = .5f;
            p.GetComponent<AudioSource>().loop = true;
            p.GetComponent<AudioSource>().Play( 0 );
        }

        GameObject attraction = Instantiate( VisionAttractionObject );
        attraction.transform.position = p.transform.position;
        attraction.transform.parent = p.transform;

        if( attraction.GetComponentInChildren<BestComponent>() )
        {
            attraction.GetComponentInChildren<BestComponent>().Initialize( attachTo );
        }

        p.transform.position = attachTo.transform.position;
        p.transform.parent = attachTo.transform;

        AttentionSpots.Add( p );
        attachedTo.Add( attachTo );
    }

    public void RemoveBestComponent( LaptopComponent p )
    {

        int index = attachedTo.IndexOf( p );

        if( index == -1 )
        {
            return;
        }
        GameObject attentionObj = AttentionSpots[index];
        Destroy( attentionObj );
        AttentionSpots.RemoveAt( index );
        attachedTo.RemoveAt( index );
    }
}
