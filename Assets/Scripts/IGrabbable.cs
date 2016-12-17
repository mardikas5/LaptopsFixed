using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dont mind this isnt used for the grabbable objects, look at Grab.cs
public interface IGrabbable
{
    GameObject gameObject { get; }

    bool isGrabbable { get; set; }

    void Grab();

    void Drop();
}

public static class IGrabbableExtentions
{


    private static IGrabbable CurrentGrabbed;

    public static IGrabbable currentGrabbed
    {
        get { return CurrentGrabbed; }
        set
        {
            CurrentGrabbed = value;
            currentGrabbedChanged( CurrentGrabbed );
        }
    }

    public static Action<IGrabbable> currentGrabbedChanged = delegate { };
}
