﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopCompareArea : MonoBehaviour
{

    private List<Laptop> laptops = new List<Laptop>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.tag == "Laptop" )
        {
            laptops.Add( other.gameObject.GetComponent<Laptop>() );
        }
    }

    void OnTriggerExit( Collider other )
    {
        if( other.gameObject.tag == "Laptop" )
        {
            laptops.Remove( other.gameObject.GetComponent<Laptop>() );
        }
    }

    public void CompareType( LaptopComponentType componentType )
    {
        var components = new List<LaptopComponent>();
        foreach( var laptop in this.laptops )
        {
            LaptopComponent component;
            if( laptop.Components.TryGetValue( componentType, out component ) )
            {
                components.Add( component );
            }
        }

        compareComponents( components );

        //Best component
        if( LaptopComponent.bestComponents.ContainsKey( componentType ) )
        {
            LaptopComponent T = LaptopComponent.bestComponents[componentType];
            T.SetBackgroundColor( new Color( 1f, .85f, 0, 1f ) );
            InteractionManager.Instance.ShowBestComponent( T );
        }
    }

    private void compareComponents( List<LaptopComponent> components )
    {
        Debug.Log( "Comparing " + components.Count + " objects" );

        LaptopComponent previousComponent = null;
        foreach( var component in components )
        {
            component.SetBackgroundColor( Color.red );

            if( previousComponent != null )
            {
                if( component.Value > previousComponent.Value )
                {
                    component.SetBackgroundColor( Color.green );
                }
                else
                {
                    previousComponent.SetBackgroundColor( Color.green );
                }
            }

            previousComponent = component;
        }
    }

    public void StopCompareType( LaptopComponentType componentType )
    {
        if( LaptopComponent.bestComponents.ContainsKey( componentType ) )
        {
            LaptopComponent T = LaptopComponent.bestComponents[componentType];
            //T.SetBackgroundColor( new Color( 1f, .85f, 0, 1f ) );
            InteractionManager.Instance.RemoveBestComponent( T );
        }
    }
}
