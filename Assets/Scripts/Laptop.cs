using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IGrabbable
{
    private Dictionary<LaptopComponentType, LaptopComponent> components = new Dictionary<LaptopComponentType, LaptopComponent>();

    bool IsGrabbable = true;

    // Use this for initialization
    void Start()
    {
        ButtonManager.Instance.Laptops.Add( this );

        var childComponents = this.GetComponentsInChildren<LaptopComponent>();

        foreach (var component in childComponents)
        {
            this.components.Add(component.Type, component);
        }
    }

    public void ShowInfo(LaptopComponentType componentType)
    {
        LaptopComponent component;

        if (components.TryGetValue(componentType, out component))
        {
            component.Show();
        }
    }

    public void HideInfo(LaptopComponentType componentType)
    {
        LaptopComponent component;

        if (components.TryGetValue(componentType, out component))
        {
            component.Hide();
        }
    }



    //IGrabbable Interface ------------------------
    public bool isGrabbable
    {
        get
        {
            return IsGrabbable;
        }

        set
        {
            IsGrabbable = value;
        }
    }

    public void Grab()
    {
        IGrabbableExtentions.currentGrabbed = this;


        //throw new NotImplementedException();
    }

    public void Drop()
    {

        //throw new NotImplementedException();
    }
}
