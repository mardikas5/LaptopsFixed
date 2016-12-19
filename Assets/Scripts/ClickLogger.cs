using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClickLogger : MonoBehaviour
{

    public ControllerInput firstController;
    public ControllerInput secondController;
    string filePath;

    // Use this for initialization
    void Start()
    {
        string fileName = "LaptopComparison_" + DateTime.Now.ToString("HH-mm-ss") + ".log";
        string desktopDir = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

        filePath = Path.Combine(desktopDir, fileName);
        Debug.Log(filePath);

        if (firstController != null)
        {
            firstController.OnTriggerPress += ControllerOnePress;
            firstController.OnTriggerUnPress += ControllerOneRelease;
        }

        if (secondController != null)
        {
            secondController.OnTriggerUnPress += ControllerTwoRelease;
            secondController.OnTriggerPress += ControllerTwoPress;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void log(string message)
    {
        using (TextWriter tw = new StreamWriter(filePath, true))
        {
            tw.WriteLine(message);
        }
    }

    public void ControllerOnePress(Transform t)
    {
        log("Controller 1 Pressed: " + Time.time);
    }

    public void ControllerTwoPress(Transform t)
    {
        log("Controller 2 Pressed: " + Time.time);
    }

    public void ControllerOneRelease(Transform t)
    {
        log("Controller 1 Released: " + Time.time);
    }

    public void ControllerTwoRelease(Transform t)
    {
        log("Controller 2 Released: " + Time.time);
    }
}
