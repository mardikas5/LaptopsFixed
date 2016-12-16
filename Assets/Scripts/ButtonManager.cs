using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public List<Laptop> Laptops;

    public Transform button;
    // Use this for initialization
    void Start()
    {
        var components = Enum.GetValues(typeof(LaptopComponentType));

        const float offset = 0.2f;

        int count = 0;
        foreach (LaptopComponentType component in components)
        {
            var newButton = Instantiate(button, new Vector3(count * offset, 0, 0), Quaternion.identity);
            newButton.GetComponentInChildren<Text>().text = Enum.GetName(typeof(LaptopComponentType), component);
            var pushableButton = newButton.GetComponentInChildren<PushButton>();
            pushableButton.OnButtonChanged += onButtonChanged;
            pushableButton.Type = component;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onButtonChanged(bool pressed, LaptopComponentType component)
    {
        if (pressed)
        {
            foreach (var laptop in this.Laptops)
            {
                if (laptop != null)
                    laptop.ShowInfo(component);
            }
        }
        else
        {
            foreach (var laptop in this.Laptops)
            {
                if (laptop != null)
                    laptop.HideInfo(component);
            }
        }
    }

}
